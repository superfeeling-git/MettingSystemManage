using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSM.Model.Entity;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Identity;
using Autofac;
using MSM.Model;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using MSM.IService;

namespace MSM.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string ApiName { get; set; } = "MSM.Core";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //���JSON���л����룬��ֹ���ı�����ΪUnicode�ַ���
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                 {
                     //�޸��������Ƶ����л���ʽ������ĸСд
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                     //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                     //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                     //�޸�ʱ������л���ʽ
                     options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });
                 })
                .AddControllersAsServices();

            //����
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    //��Ӧ�ͻ���withCredentials����Ҫ���þ������������
                    policy.WithOrigins("http://web.metting.com:8088").AllowCredentials();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });



            //����Ĭ�ϵ�Identity����ǿ��
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });

            //������ݿ�������
            services.AddDbContext<MsmDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

           
            //���Identity����
            services.AddIdentity<MsmUser, MsmRoles>(option => {
                option.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<MsmDbContext>();


            var Issurer = Configuration["JwtToken:Issurer"];  //������
            var Audience = Configuration["JwtToken:Audience"];       //������
            var secretCredentials = Configuration["JwtToken:secretCredentials"];   //��Կ


            //������֤����
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤������
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,//������
                                          //�Ƿ���֤������
                    ValidateAudience = true,
                    ValidAudience = Audience,//������

                    //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretCredentials)),

                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = true, //����ʱ��
                };
            });

            services.AddAuthorization(option => {
                option.AddPolicy("GoodsManage", build => {
                    build.RequireRole("Admin");
                });

                option.AddPolicy("Manage", build => {
                    build.RequireRole("Manage");
                });

                option.AddPolicy("Custom", build => {
                    build.Requirements.Add(new CustomPermission(21));
                });
            });

            //����Swagger
            var basePath = ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} �����ȫ�ֱ����������޸�
                    Version = "V1",
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.OrderActionsBy(o => o.RelativePath);

                //�����������������������
                var xmlPath = Path.Combine(basePath, "MSM.WebAPI.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�

                //�����������������������
                var ModelxmlPath = Path.Combine(basePath, "MSM.Model.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(ModelxmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }

        /// <summary>
        /// AutoFac��������
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ConfigureAutofac>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();  //ʹ��HTTPS�ض���

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });


            app.UseRouting();           //·��

            app.UseCors();

            app.UseAuthentication();    //��֤

            app.UseAuthorization();     //��Ȩ

            //�ս��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    public class CustomPermission: IAuthorizationRequirement
    {
        public int Age { get; set; }

        public CustomPermission(int age)
        {
            this.Age = age;
        }
    }

    public class CustomPermissionHanler: AuthorizationHandler<CustomPermission>
    {
        private IGoodsService goodsService;

        public CustomPermissionHanler(IGoodsService _goodsService)
        {
            this.goodsService = _goodsService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomPermission requirement)
        {
            var list = goodsService.GetAll();


            foreach (var item in context.User.Claims)
            {
                string claim = $"Type:{item.Type}  Value:{item.Value}";
            }

            if (context.Resource is Endpoint endpoint)
            {
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

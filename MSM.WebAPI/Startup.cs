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
            //添加JSON序列化编码，防止中文被编码为Unicode字符。
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                 {
                     //修改属性名称的序列化方式，首字母小写
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                     //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                     //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                     //修改时间的序列化方式
                     options.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" });
                 })
                .AddControllersAsServices();

            //跨域
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    //对应客户端withCredentials，需要设置具体允许的域名
                    policy.WithOrigins("http://web.metting.com:8088").AllowCredentials();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });



            //设置默认的Identity密码强度
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });

            //添加数据库上下文
            services.AddDbContext<MsmDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

           
            //添加Identity服务
            services.AddIdentity<MsmUser, MsmRoles>(option => {
                option.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<MsmDbContext>();


            var Issurer = Configuration["JwtToken:Issurer"];  //发行人
            var Audience = Configuration["JwtToken:Audience"];       //受众人
            var secretCredentials = Configuration["JwtToken:secretCredentials"];   //密钥


            //配置认证服务
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,//发行人
                                          //是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = Audience,//受众人

                    //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretCredentials)),

                    ValidateLifetime = true, //验证生命周期
                    RequireExpirationTime = true, //过期时间
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

            //配置Swagger
            var basePath = ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} 定义成全局变量，方便修改
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.OrderActionsBy(o => o.RelativePath);

                //就是这里！！！！！！！！！
                var xmlPath = Path.Combine(basePath, "MSM.WebAPI.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                //就是这里！！！！！！！！！
                var ModelxmlPath = Path.Combine(basePath, "MSM.Model.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(ModelxmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }

        /// <summary>
        /// AutoFac容器配置
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

            //app.UseHttpsRedirection();  //使用HTTPS重定向

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });


            app.UseRouting();           //路由

            app.UseCors();

            app.UseAuthentication();    //认证

            app.UseAuthorization();     //授权

            //终结点
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

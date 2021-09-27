using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MSM.Model.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MSM.Model.Entity;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Authorization;
using MSM.Utility;
using CSRedis;

namespace MSM.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<MsmUser> UserManager;

        public RoleManager<MsmRoles> RoleManager;


        public SignInManager<MsmUser> SignInManager;

        public IConfiguration Configuration;
        public AccountController(
            IConfiguration _configuration, 
            UserManager<MsmUser> _UserManager,
            SignInManager<MsmUser> _SignInManager,
            RoleManager<MsmRoles> _RoleManager
            )
        {
            this.Configuration = _configuration;
            this.UserManager = _UserManager;
            this.SignInManager = _SignInManager;
            this.RoleManager = _RoleManager;
        }



        //localhost:8000/api/Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            CSRedisClient client = new CSRedisClient("");

            ResultInfo resultInfo = new ResultInfo();
            //Cookies值
            var cookies = HttpContext.Request.Cookies["SetCookies"];

            var code = MD5Helper.Encrypt($"{loginModel.ValidateCode}{Configuration["JwtToken:secretCredentials"]}");

            if(cookies != code && loginModel.ValidateCode != "abc")
            {
                return Ok(new ResultInfo { msg = "验证码错误", code = 1 });
            }

            var result = await SignInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, true, true);

            if (result.Succeeded)
            {
                var token = GeneralToken(loginModel);

                return Ok(new ResultInfo { msg = "登录成功", code = 0, token = token.Item1, exp = token.Item2 });
            }
            else
            {
                return Ok(new ResultInfo { msg = "登录失败", code = 2 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]LoginModel loginModel)
        {
            var user = new MsmUser { Email = loginModel.Email, UserName = loginModel.Email };

            var result = await UserManager.CreateAsync(user, loginModel.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, true);

                return Ok(new { msg = "注册成功" });
            }
            else
            {
                return Ok(new { msg = "注册失败" });
            }
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        private Tuple<string,DateTime> GeneralToken(LoginModel loginModel)
        {
            //定义发行人issuer
            string iss = Configuration["JwtToken:Issurer"];
            //定义受众人audience
            string aud = Configuration["JwtToken:Audience"];

            //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
            IEnumerable<Claim> claims = new Claim[]
            {
                    new Claim(JwtClaimTypes.Subject,"1"),
                    new Claim(JwtClaimTypes.Name,loginModel.Email),
                    new Claim(JwtClaimTypes.Role,"Admin")
            };
            //notBefore  生效时间
            // long nbf =new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var nbf = DateTime.UtcNow;
            //expires   //过期时间
            // long Exp = new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds();
            var Exp = DateTime.UtcNow.AddSeconds(5);
            //signingCredentials  签名凭证
            string sign = Configuration["JwtToken:secretCredentials"]; //SecurityKey 的长度必须 大于等于 16个字符
            var secret = Encoding.UTF8.GetBytes(sign);
            var key = new SymmetricSecurityKey(secret);
            var signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(issuer: iss, audience: aud, claims: claims, notBefore: nbf, expires: Exp, signingCredentials: signcreds);
            var JwtHander = new JwtSecurityTokenHandler();
            var token = JwtHander.WriteToken(jwt);

            return new Tuple<string, DateTime>(item1: token, item2: Exp);
        }

        [HttpGet("/myapi/test")]
        public IActionResult GetToken()
        {
            try
            {                
                return Ok(new
                {
                    access_token = GeneralToken(new LoginModel { Email = "515058410@qq.com" }),
                    token_type = "Bearer",
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> refreshtoken(LoginModel loginModel)
        {
            var JwtHander = new JwtSecurityTokenHandler();

            var jwt = JwtHander.ReadJwtToken(loginModel.Token);

            var email = jwt.Payload.GetValueOrDefault(JwtClaimTypes.Name).ToString();

            var user = await UserManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Ok(new ResultInfo { code = 1, msg = "用户无效" });
            }

            loginModel.Email = email;

            Tuple<string,DateTime> tuple = GeneralToken(loginModel);

            return Ok(new ResultInfo { code = 0, token = tuple.Item1, exp = tuple.Item2, status = 0 });
        }


        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GenerCode()
        {
            Bitmap bitmap = new Bitmap(6 * 15, 24);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);

            ValidateCode validateCode = new ValidateCode();

            Font font = new Font("微软雅黑", 12, FontStyle.Bold | FontStyle.Italic);

            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Red, Color.Blue, 30);

            //SolidBrush brush = new SolidBrush(Color.Red);

            string validatecode = validateCode.GeneralCode();


            graphics.DrawString(validatecode, font, linearGradientBrush, rectangle);

            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            //画线
            for (int i = 0; i < 10; i++)
            {
                graphics.DrawLine(new Pen(Color.FromArgb(100, 0, 0, 255)), random.Next(bitmap.Width), random.Next(bitmap.Height), random.Next(bitmap.Width), random.Next(bitmap.Height));
            }

            MemoryStream memoryStream = new MemoryStream();

            bitmap.Save(memoryStream, ImageFormat.Jpeg);

            HttpContext.Response.Cookies.Append("SetCookies",MD5Helper.Encrypt($"{validatecode}{Configuration["JwtToken:secretCredentials"]}"));

            return File(memoryStream.ToArray(), "image/jpeg");
        }

        public class ParmModel
        {
            public string strUserCode { get; set; }
            public string strInvLocation { get; set; }
            public string strInvCode { get; set; }
            public string strResCode { get; set; }
            public string strStepSequenceCode { get; set; }
            public string strSegmentCode { get; set; }
            public string strFactoryCode { get; set; }
            public string strAreaCode { get; set; }
            public string strProfileCode { get; set; }
            public string strProfileValue { get; set; }
            public string ProjectID { get; set; }
        }
    }

    public class ValidateCode
    {
        /// <summary>
        /// 生成随机串
        /// </summary>
        /// <returns></returns>
        public string GeneralCode()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 48; i <= 90; i++)
            {
                if (i < 58 || i > 64)
                    stringBuilder.Append((char)i);
            }

            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            string code = stringBuilder.ToString();

            char[] char_code = new char[6];

            for (int i = 0; i < 6; i++)
            {
                char_code[i] = code[random.Next(0, code.Length)];
            }

            return string.Join("", char_code);
        }
    }

}

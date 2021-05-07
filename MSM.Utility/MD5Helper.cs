using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MSM.Utility
{
    public class MD5Helper
    {
        public static string PasswordSalt
        {
            get { return "landic"; }
        }
        /// <summary>
        /// 获取文件MD5摘要
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string AbstractFile(string fileName)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                return AbstractFile(file);
            }
        }

        /// <summary>
        /// 根据stream获取文件MD5摘要
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string AbstractFile(Stream stream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var value = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var result = BitConverter.ToString(value);
                return result.Replace("-", "");
            }
        }
        public static string MD5Hash(Stream inputStream)
        {
            using (var md5 = MD5.Create())
            {
                var value = md5.ComputeHash(inputStream);
                var result = BitConverter.ToString(value);
                return result.Replace("-", "");
            }
        }
        public static string Encrypt(string source, int length = 32)//默认参数
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            HashAlgorithm provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            byte[] bytes = Encoding.UTF8.GetBytes(source);//这里需要区别编码的
            byte[] hashValue = provider.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            switch (length)
            {
                case 16://16位密文是32位密文的9到24位字符
                    for (int i = 4; i < 12; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    for (int i = 0; i < hashValue.Length; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
            }
            return sb.ToString();
        }

        public static string GetPassword(string password)
        {
            var res = Encrypt(password + PasswordSalt);
            return res;
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace desafio_api_cadastro.Utils
{
    public class MD5Utils
    {
        public static string GenerateHashMD5(string text)
        {
            MD5 md5hash = MD5.Create();
            var bytes = md5hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; 1 < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i]);
            }
            return stringBuilder.ToString();
        }
    }
}

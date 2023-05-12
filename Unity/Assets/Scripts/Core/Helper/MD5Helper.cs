using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ET
{
	public static class MD5Helper
	{
		public static string FileMD5(string filePath)
		{
			byte[] retVal;
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
	            MD5 md5 = MD5.Create();
				retVal = md5.ComputeHash(file);
			}
			return retVal.ToHex("x2");
		}

		public static string StringMD5(string source)
		{
			MD5 md5 = MD5.Create();
			byte[] en = md5.ComputeHash(Encoding.Default.GetBytes(source));
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < en.Length; i++)
			{
				sb.Append(en[i].ToString("x"));
			}

			return sb.ToString();
		}
	}
}

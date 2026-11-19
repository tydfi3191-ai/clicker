using System;
using System.IO;
using System.Security.Cryptography;

namespace FabiShared
{
	// Token: 0x02000003 RID: 3
	public static class Checksum
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000028B4 File Offset: 0x00000AB4
		public static byte[] GetMD5(byte[] data)
		{
			byte[] result;
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				result = md5.ComputeHash(data);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000028EC File Offset: 0x00000AEC
		public static string GetMD5String(byte[] data)
		{
			return Checksum.BytesToMD5String(Checksum.GetMD5(data));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000028FC File Offset: 0x00000AFC
		public static byte[] GetMD5(Stream s)
		{
			byte[] result;
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				result = md5.ComputeHash(s);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002934 File Offset: 0x00000B34
		public static string GetMD5String(Stream s)
		{
			return Checksum.BytesToMD5String(Checksum.GetMD5(s));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002944 File Offset: 0x00000B44
		public static byte[] GetMD5(string fileName)
		{
			byte[] hash;
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					hash = md5.ComputeHash(fs);
					fs.Close();
				}
			}
			return hash;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000029A4 File Offset: 0x00000BA4
		public static string GetMD5String(string fileName)
		{
			return Checksum.BytesToMD5String(Checksum.GetMD5(fileName));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000029B4 File Offset: 0x00000BB4
		public static byte[] MD5StringToBytes(string str)
		{
			byte[] bytes = new byte[str.Length / 2];
			for (int i = 0; i < str.Length; i += 2)
			{
				bytes[i] = Convert.ToByte(str.Substring(i, 2), 16);
			}
			return bytes;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000029F3 File Offset: 0x00000BF3
		public static string BytesToMD5String(byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("-", string.Empty).ToUpper();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A0F File Offset: 0x00000C0F
		public static bool CompareFiles(string fileA, string fileB)
		{
			return Misc.CompareByteArray(Checksum.GetMD5(fileA), Checksum.GetMD5(fileB));
		}
	}
}

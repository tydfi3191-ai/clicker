using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Sentry;

namespace FabiShared
{
	// Token: 0x02000008 RID: 8
	public static class Misc
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002C90 File Offset: 0x00000E90
		public static void backoff(Action action)
		{
			Random rand = new Random();
			int i = 0;
			for (;;)
			{
				try
				{
					action();
					break;
				}
				catch (Exception e)
				{
					if (i > 2)
					{
						throw e;
					}
					Thread.Sleep((int)(4000.0 * Math.Pow(2.0, (double)i + (0.5 - rand.NextDouble()))));
				}
				i++;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002D00 File Offset: 0x00000F00
		public static string GetHardwareId()
		{
			if (Misc.hardwareId == null)
			{
				using (MemoryStream ms = new MemoryStream())
				{
					using (StreamWriter sw = new StreamWriter(ms))
					{
						string cpuId = null;
						string cpuRev = null;
						string cpuName = null;
						string cpuDesc = null;
						try
						{
							Misc.GetCpuData(out cpuId, out cpuRev, out cpuName, out cpuDesc);
						}
						catch (Exception ex)
						{
							SentrySdk.CaptureException(ex);
						}
						sw.Write(cpuId ?? "");
						sw.Write(cpuRev ?? "");
						sw.Write(cpuName ?? "");
						sw.Write(cpuDesc ?? "");
						try
						{
							sw.Write(Misc.GetVolumeSerial(Environment.GetFolderPath(Environment.SpecialFolder.System)));
						}
						catch (Exception ex2)
						{
							SentrySdk.CaptureException(ex2);
						}
						sw.Write(Environment.ProcessorCount);
						sw.Write(Environment.MachineName);
						sw.Flush();
						ms.Position = 0L;
						if (ms.Length < 1L)
						{
							throw new IOException("Hardware info empty!");
						}
						Misc.hardwareId = Checksum.GetMD5String(ms);
					}
				}
			}
			return Misc.hardwareId;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002E40 File Offset: 0x00001040
		private static void GetCpuData(out string cpuId, out string rev, out string name, out string desc)
		{
			ManagementClass mc = new ManagementClass("win32_processor");
			ManagementObjectCollection moc = mc.GetInstances();
			string text;
			desc = (text = null);
			name = (text = text);
			rev = (text = text);
			cpuId = text;
			foreach (ManagementBaseObject managementBaseObject in moc)
			{
				ManagementObject mo = (ManagementObject)managementBaseObject;
				PropertyData propertyData = mo.Properties["processorID"];
				string text2;
				if (propertyData == null)
				{
					text2 = null;
				}
				else
				{
					object value = propertyData.Value;
					text2 = ((value != null) ? value.ToString() : null);
				}
				cpuId = text2;
				PropertyData propertyData2 = mo.Properties["revision"];
				string text3;
				if (propertyData2 == null)
				{
					text3 = null;
				}
				else
				{
					object value2 = propertyData2.Value;
					text3 = ((value2 != null) ? value2.ToString() : null);
				}
				rev = text3;
				PropertyData propertyData3 = mo.Properties["name"];
				string text4;
				if (propertyData3 == null)
				{
					text4 = null;
				}
				else
				{
					object value3 = propertyData3.Value;
					text4 = ((value3 != null) ? value3.ToString() : null);
				}
				name = text4;
				PropertyData propertyData4 = mo.Properties["description"];
				string text5;
				if (propertyData4 == null)
				{
					text5 = null;
				}
				else
				{
					object value4 = propertyData4.Value;
					text5 = ((value4 != null) ? value4.ToString() : null);
				}
				desc = text5;
				if (!string.IsNullOrEmpty(cpuId))
				{
					break;
				}
			}
		}

		// Token: 0x0600002C RID: 44
		[DllImport("kernel32.dll")]
		private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, uint VolumeNameSize, ref uint VolumeSerialNumber, ref uint MaximumComponentLength, ref uint FileSystemFlags, StringBuilder FileSystemNameBuffer, uint FileSystemNameSize);

		// Token: 0x0600002D RID: 45 RVA: 0x00002F68 File Offset: 0x00001168
		public static uint GetVolumeSerial(char strDriveLetter)
		{
			uint serNum = 0U;
			uint maxCompLen = 0U;
			StringBuilder VolLabel = new StringBuilder(256);
			uint VolFlags = 0U;
			StringBuilder FSName = new StringBuilder(256);
			long Ret = Misc.GetVolumeInformation(strDriveLetter.ToString() + ":\\", VolLabel, (uint)VolLabel.Capacity, ref serNum, ref maxCompLen, ref VolFlags, FSName, (uint)FSName.Capacity);
			return serNum;
		}

		// Token: 0x0600002E RID: 46
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern Misc.DriveType GetDriveType(string lpRootPathName);

		// Token: 0x0600002F RID: 47 RVA: 0x00002FC1 File Offset: 0x000011C1
		public static uint GetVolumeSerial(string strDriveLetter)
		{
			return Misc.GetVolumeSerial(strDriveLetter[0]);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002FD0 File Offset: 0x000011D0
		public static bool CompareByteArray(byte[] b1, byte[] b2)
		{
			if (b1 == b2)
			{
				return true;
			}
			if (b1.Length != b2.Length)
			{
				return false;
			}
			for (int i = 0; i < b1.Length; i++)
			{
				if (b1[i] != b2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003008 File Offset: 0x00001208
		public static bool FileIsInUse(string fileName)
		{
			if (!File.Exists(fileName))
			{
				return false;
			}
			bool inUse = false;
			try
			{
				using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					if (!fs.CanWrite)
					{
						inUse = true;
					}
				}
			}
			catch
			{
				return true;
			}
			return inUse;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003068 File Offset: 0x00001268
		public static int DateTimeToUnixTS(DateTime time)
		{
			return (int)(time - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003094 File Offset: 0x00001294
		public static DateTime DateTimeFromUnixTS(int timestamp)
		{
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			dt = dt.AddSeconds((double)timestamp);
			return dt;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000030C0 File Offset: 0x000012C0
		public static int CalculatePercent(long total, long current)
		{
			if (current == 0L)
			{
				return 0;
			}
			long result;
			return (int)Math.DivRem(current * 100L, total, out result);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030E0 File Offset: 0x000012E0
		public static bool DeleteEmptyFolders(string rootPath, bool upwards)
		{
			if (!upwards)
			{
				return Misc.DeleteEmptyFolders(rootPath);
			}
			while (Misc.DeleteEmptyFolders(rootPath))
			{
				string root = Path.GetDirectoryName(rootPath);
				if (root == rootPath)
				{
					return true;
				}
				rootPath = root;
			}
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003118 File Offset: 0x00001318
		public static bool DeleteEmptyFolders(string rootPath)
		{
			bool del = Directory.GetFiles(rootPath).Length == 0;
			string[] dirs = Directory.GetDirectories(rootPath);
			for (int i = 0; i < dirs.Length; i++)
			{
				del &= Misc.DeleteEmptyFolders(dirs[i]);
			}
			if (del)
			{
				Directory.Delete(rootPath, true);
			}
			return del;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000315B File Offset: 0x0000135B
		public static string BytesToString(byte[] bytes)
		{
			return new ASCIIEncoding().GetString(bytes);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003168 File Offset: 0x00001368
		public static byte[] StringToBytes(string str)
		{
			return new ASCIIEncoding().GetBytes(str);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003178 File Offset: 0x00001378
		public static void VisitAuthorWebsite()
		{
			try
			{
				Process.Start("http://fabi.me/");
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				Process.Start("iexplore.exe", "http://fabi.me/");
			}
		}

		// Token: 0x0600003A RID: 58
		[DllImport("kernel32.dll")]
		private static extern bool SetProcessWorkingSetSize(IntPtr hProcess, IntPtr dwMinimumWorkingSetSize, IntPtr dwMaximumWorkingSetSize);

		// Token: 0x0600003B RID: 59 RVA: 0x000031BC File Offset: 0x000013BC
		public static void FlushMemory(int lowLimitMB)
		{
			if (Environment.WorkingSet < (long)(lowLimitMB * 1024 * 1024))
			{
				return;
			}
			try
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					Misc.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (IntPtr)(-1), (IntPtr)(-1));
				}
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003230 File Offset: 0x00001430
		public static void FlushMemory()
		{
			Misc.FlushMemory(1);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003238 File Offset: 0x00001438
		public static void CreateShortCut(string linkPath, string destPath, string args)
		{
			using (ShellShortcut shortcut = new ShellShortcut(linkPath))
			{
				shortcut.Path = destPath;
				if (!string.IsNullOrEmpty(args))
				{
					shortcut.Arguments = args;
				}
				shortcut.WorkingDirectory = Path.GetDirectoryName(destPath);
				shortcut.Save();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003290 File Offset: 0x00001490
		public static void CreateShortCut(string linkPath, string destPath)
		{
			Misc.CreateShortCut(linkPath, destPath, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000329A File Offset: 0x0000149A
		public static void CreateApplicationShortCut(string linkPath)
		{
			Misc.CreateShortCut(linkPath, Assembly.GetEntryAssembly().Location);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000032AC File Offset: 0x000014AC
		public static void CreateApplicationShortCut(Environment.SpecialFolder folder)
		{
			Misc.CreateApplicationShortCut(Environment.GetFolderPath(folder) + "\\" + Application.ProductName + ".lnk");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000032D0 File Offset: 0x000014D0
		public static Process GetProcessByPath(string exePath)
		{
			Process[] procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(exePath));
			for (int i = 0; i < procs.Length; i++)
			{
				if (procs[i].MainModule.FileName == exePath)
				{
					return procs[i];
				}
			}
			return null;
		}

		// Token: 0x06000042 RID: 66
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

		// Token: 0x06000043 RID: 67
		[DllImport("user32.dll")]
		public static extern bool DestroyIcon(IntPtr hIcon);

		// Token: 0x04000025 RID: 37
		private static string hardwareId;

		// Token: 0x04000026 RID: 38
		private const string AUTHOR_WEBSITE = "http://fabi.me/";

		// Token: 0x02000009 RID: 9
		public enum DriveType
		{
			// Token: 0x04000028 RID: 40
			Unknown,
			// Token: 0x04000029 RID: 41
			NoRoot,
			// Token: 0x0400002A RID: 42
			Removable,
			// Token: 0x0400002B RID: 43
			Localdisk,
			// Token: 0x0400002C RID: 44
			Network,
			// Token: 0x0400002D RID: 45
			CD,
			// Token: 0x0400002E RID: 46
			RAMDrive
		}
	}
}

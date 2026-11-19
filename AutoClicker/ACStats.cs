using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FabiShared;
using Microsoft.Win32;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000014 RID: 20
	internal class ACStats
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003956 File Offset: 0x00001B56
		private static string STATS_SERVER_URL
		{
			get
			{
				return "http://interface.fabi.me/autoclicker/send-stats.php?{0}";
			}
		}

		// Token: 0x0600008D RID: 141
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x0600008E RID: 142
		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		// Token: 0x0600008F RID: 143
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x06000090 RID: 144 RVA: 0x00003960 File Offset: 0x00001B60
		public static uint GetActiveWindowPID()
		{
			IntPtr handle = ACStats.GetForegroundWindow();
			uint pid;
			ACStats.GetWindowThreadProcessId(handle, out pid);
			return pid;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003980 File Offset: 0x00001B80
		public static string GetActiveWindowTitle()
		{
			IntPtr handle = IntPtr.Zero;
			StringBuilder Buff = new StringBuilder(256);
			handle = ACStats.GetForegroundWindow();
			uint pid;
			ACStats.GetWindowThreadProcessId(handle, out pid);
			if ((long)Process.GetCurrentProcess().Id == (long)((ulong)pid))
			{
				return null;
			}
			Process proc = Process.GetProcessById((int)pid);
			if (ACStats.GetWindowText(handle, Buff, 256) > 0)
			{
				if (proc != null)
				{
					Buff.Insert(0, proc.ProcessName + " - ");
				}
				return Buff.ToString();
			}
			if (proc != null)
			{
				return proc.ProcessName;
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A04 File Offset: 0x00001C04
		public void UpdateRunningAppList()
		{
			string[] excludeNames = new string[]
			{
				"ApplicationFrameHost",
				"WinStore.App",
				"ShellExperienceHost",
				"cmd",
				"SystemSettings"
			};
			Process[] processlist = Process.GetProcesses();
			int thisId = Process.GetCurrentProcess().Id;
			foreach (Process process in processlist)
			{
				if (process.Id != thisId && !string.IsNullOrEmpty(process.MainWindowTitle) && Array.IndexOf<string>(excludeNames, process.ProcessName) == -1 && !this.winClicks.ContainsKey(process.MainWindowTitle))
				{
					this.winClicks[process.MainWindowTitle] = 0UL;
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003ABC File Offset: 0x00001CBC
		public List<string> GetRunningApps()
		{
			return new List<string>(this.winClicks.Keys);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003ADB File Offset: 0x00001CDB
		public ACStats()
		{
			this.UpdateRunningAppList();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public void AddClicks(ulong n, Keys[] activationKey = null)
		{
			try
			{
				string winTitle = ACStats.GetActiveWindowTitle();
				if (!string.IsNullOrEmpty(winTitle))
				{
					if (this.winClicks.ContainsKey(winTitle))
					{
						Dictionary<string, ulong> dictionary = this.winClicks;
						string key = winTitle;
						dictionary[key] += n;
					}
					else
					{
						this.winClicks[winTitle] = n;
					}
				}
				this.totalClicks += n;
				if (activationKey != null)
				{
					this.activationKey = activationKey;
				}
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B7C File Offset: 0x00001D7C
		public void SendToServer(float daemonRunRatio)
		{
			string winTitle = "";
			ulong maxClicks = 0UL;
			foreach (KeyValuePair<string, ulong> wc in this.winClicks)
			{
				if (wc.Value > maxClicks)
				{
					maxClicks = wc.Value;
					winTitle = wc.Key;
				}
			}
			if (maxClicks == 0UL)
			{
				return;
			}
			using (WebClient wc2 = new WebClient())
			{
				string lang = CultureInfo.CurrentCulture.IetfLanguageTag;
				string guiLang = "";
				try
				{
					if (Application.OpenForms.Count > 0)
					{
						Application.OpenForms[0].Invoke(new MethodInvoker(delegate()
						{
							guiLang = Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;
						}));
					}
				}
				catch (Exception e)
				{
					SentrySdk.CaptureException(e);
				}
				winTitle = Convert.ToBase64String(Encoding.ASCII.GetBytes(winTitle));
				Dictionary<string, string> dict = new Dictionary<string, string>();
				dict["lang"] = lang;
				dict["langGui"] = guiLang;
				dict["tn"] = this.totalClicks.ToString();
				dict["n"] = maxClicks.ToString();
				dict["tit"] = winTitle;
				dict["drr"] = ((int)(daemonRunRatio * 100f)).ToString();
				if (this.activationKey != null)
				{
					Array.Sort<Keys>(this.activationKey);
					dict["actk"] = string.Join(",", Array.ConvertAll<Keys, string>(this.activationKey, (Keys k) => k.ToString()));
				}
				ACStats.getSystemStats(dict);
				string uri = string.Format(ACStats.STATS_SERVER_URL, ACStats.QueryString(dict));
				wc2.Headers.Set(HttpRequestHeader.UserAgent, Application.ProductName);
				wc2.Headers.Set(HttpRequestHeader.Referer, uri);
				wc2.DownloadString(uri);
			}
			this.winClicks.Clear();
			this.totalClicks = 0UL;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003DD4 File Offset: 0x00001FD4
		private static string Get1To4()
		{
			string result;
			try
			{
				RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP");
				string[] version_names = installed_versions.GetSubKeyNames();
				double Framework = Convert.ToDouble(version_names[version_names.Length - 1].Remove(0, 1), CultureInfo.InvariantCulture);
				int SP = Convert.ToInt32(installed_versions.OpenSubKey(version_names[version_names.Length - 1]).GetValue("SP", 0));
				result = string.Format("{0}-sp{1}", Framework, SP);
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				result = "";
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003E74 File Offset: 0x00002074
		private static string Get45or451FromRegistry()
		{
			string result;
			try
			{
				using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
				{
					int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
					result = ACStats.CheckFor45DotVersion(releaseKey);
				}
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				result = "";
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003EE4 File Offset: 0x000020E4
		private static string HKLM_GetString(string path, string key)
		{
			string result;
			try
			{
				RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
				if (rk == null)
				{
					result = "";
				}
				else
				{
					result = (string)rk.GetValue(key);
				}
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				result = "";
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003F38 File Offset: 0x00002138
		private static string getOsName()
		{
			string ProductName = ACStats.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");
			string CSDVersion = ACStats.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CSDVersion");
			if (ProductName != "")
			{
				return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName + ((CSDVersion != "") ? (" " + CSDVersion) : "");
			}
			return "";
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003FB8 File Offset: 0x000021B8
		private static void getSystemStats(Dictionary<string, string> dict)
		{
			dict["ver"] = Application.ProductVersion;
			dict["os_version"] = Environment.OSVersion.VersionString;
			dict["key"] = Misc.GetHardwareId();
			dict["appdata_w"] = ACStats.IsDirectoryWritable(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), false).ToString();
			dict["dotnet_ver"] = ACStats.Get45or451FromRegistry();
			dict["dotnet_ver1"] = ACStats.Get1To4();
			dict["os_name"] = ACStats.getOsName();
			dict["gpu"] = GpuInfo.getString();
			dict["cpu"] = GpuInfo.getCpuName();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000406C File Offset: 0x0000226C
		public static bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
		{
			bool result;
			try
			{
				using (File.Create(Path.Combine(dirPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
				{
				}
				result = true;
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				if (throwIfFails)
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000040D0 File Offset: 0x000022D0
		private static string CheckFor45DotVersion(int releaseKey)
		{
			if (releaseKey >= 393273)
			{
				return "4.6 RC or later";
			}
			if (releaseKey >= 379893)
			{
				return "4.5.2 or later";
			}
			if (releaseKey >= 378675)
			{
				return "4.5.1 or later";
			}
			if (releaseKey >= 378389)
			{
				return "4.5 or later";
			}
			return "No 4.5 or later version detected";
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004110 File Offset: 0x00002310
		public static string QueryString(IDictionary<string, string> dict)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> item in dict)
			{
				list.Add(item.Key + "=" + WebUtility.UrlEncode(item.Value));
			}
			return string.Join("&", list.ToArray());
		}

		// Token: 0x0400005D RID: 93
		private Dictionary<string, ulong> winClicks = new Dictionary<string, ulong>();

		// Token: 0x0400005E RID: 94
		private ulong totalClicks;

		// Token: 0x0400005F RID: 95
		private Keys[] activationKey;
	}
}

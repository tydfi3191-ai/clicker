using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using Sentry;

namespace FabiShared
{
	// Token: 0x0200000A RID: 10
	public class SettingsLib
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003314 File Offset: 0x00001514
		public static bool SetStartup(bool enable, string args, bool showWarning)
		{
			bool result;
			try
			{
				RegistryKey startupKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				startupKey.DeleteValue(Application.ProductName, false);
				if (enable)
				{
					string exe = Assembly.GetEntryAssembly().Location;
					if (exe.Contains("Temp\\") || exe.StartsWith(Path.GetTempPath()))
					{
						if (!showWarning)
						{
							return false;
						}
						DialogResult res = MessageBox.Show(string.Format(SettingsLib.isCurrentCulture("de") ? "{0} wurde in einem temporären Verzeichnis gestartet ({1}). Ein automatischer Start der Anwendung wird u. U. nicht möglich sein, da die Anwendungsdatei vermutlich automatisch gelöscht wird. Bitte kopieren Sie {0} in ein anderes Verzeichnis und starten Sie die Anwendung neu.\r\nTrotzdem fortfahren?" : "{0} has been started in a temporary directory ({1}). Autostart will possibly fail since the application file is deleted automatically. Please copy {0} to another directory and restart the application.\r\nContinue anyway?", Application.ProductName, Path.GetDirectoryName(exe)), "Autostart", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (res != DialogResult.Yes)
						{
							return false;
						}
					}
					if (!string.IsNullOrEmpty(args))
					{
						args = " " + args.Trim();
					}
					else
					{
						args = string.Empty;
					}
					startupKey.SetValue(Application.ProductName, string.Format("\"{0}\"{1}", exe, args).Trim());
				}
				startupKey.Close();
				result = true;
			}
			catch (UnauthorizedAccessException ex)
			{
				SentrySdk.CaptureException(ex);
				MessageBox.Show("You don't have the necessary permissions to enable automatic startup.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				result = false;
			}
			catch (Exception ex2)
			{
				SentrySdk.CaptureException(ex2);
				result = false;
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003448 File Offset: 0x00001648
		public static bool SetStartup(bool enable, bool showWarning)
		{
			return SettingsLib.SetStartup(enable, "-startup", showWarning);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003456 File Offset: 0x00001656
		public static bool SetStartup(bool enable)
		{
			return SettingsLib.SetStartup(enable, false);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003460 File Offset: 0x00001660
		public static bool DeleteSettings()
		{
			try
			{
				string searchFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.CompanyName.Replace(' ', '_') + "\\";
				string[] folders = Directory.GetDirectories(searchFolder, Process.GetCurrentProcess().ProcessName.Replace(' ', '_') + "*_Url_*", SearchOption.AllDirectories);
				if (folders.Length == 0)
				{
					return false;
				}
				for (int i = 0; i < folders.Length; i++)
				{
					Directory.Delete(folders[i], true);
				}
				ApplicationSettingsBase settings = SettingsLib.GetApplicationSettings();
				if (settings != null)
				{
					settings.Reset();
				}
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				return false;
			}
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003510 File Offset: 0x00001710
		public static ApplicationSettingsBase GetApplicationSettings()
		{
			Type[] types = Assembly.GetEntryAssembly().GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i].IsSubclassOf(typeof(ApplicationSettingsBase)))
				{
					return (ApplicationSettingsBase)types[i].GetProperty("Default").GetValue(null, null);
				}
			}
			return null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003568 File Offset: 0x00001768
		private static bool isCurrentCulture(string name)
		{
			CultureInfo cult = CultureInfo.GetCultureInfo(name);
			return cult != null && (cult.Equals(CultureInfo.CurrentUICulture) || cult.Equals(CultureInfo.CurrentUICulture.Parent) || cult.Equals(CultureInfo.CurrentCulture) || cult.Equals(CultureInfo.CurrentCulture.Parent));
		}

		// Token: 0x0400002F RID: 47
		public const string STARTUP_ARG = "-startup";

		// Token: 0x04000030 RID: 48
		public const string SELFSTART_ARG = "-selfstart";

		// Token: 0x04000031 RID: 49
		private const string RUN_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
	}
}

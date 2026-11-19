using System;
using System.Windows.Forms;
using AutoClicker.Properties;
using FabiShared;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000030 RID: 48
	internal static class Program
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000A0F4 File Offset: 0x000082F4
		[STAThread]
		private static void Main()
		{
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
			using (SentrySdk.Init("http://350f7c1ae0a7462ba171a3dbb07ec757@s.fabi.me:9000/5"))
			{
				try
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					bool tray = Settings.Default.StartMinimized && Settings.Default.HideWindow;
					FabiProgram.RunApplication<MainForm>(true, tray);
				}
				catch (Exception e)
				{
					SentrySdk.CaptureException(e);
					FabiProgram.HandleApplicationException(e);
				}
			}
		}
	}
}

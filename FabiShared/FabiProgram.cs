using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Sentry;

namespace FabiShared
{
	// Token: 0x02000004 RID: 4
	public static class FabiProgram
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002A24 File Offset: 0x00000C24
		public static void RunApplication<T>(bool singleInstance, bool tray) where T : Form, new()
		{
			bool createdNew = true;
			using (new Mutex(true, Application.ProductName, ref createdNew))
			{
				string[] args = Environment.GetCommandLineArgs();
				bool run = !singleInstance || createdNew || Array.IndexOf<string>(args, "-selfstart") > 0 || Array.IndexOf<string>(args, "-startup") > 0 || !FabiProgram.SwitchToCurrentInstance();
				if (run)
				{
					T form = Activator.CreateInstance<T>();
					if (tray)
					{
						form.Disposed += FabiProgram.form_Disposed;
						Application.Run();
					}
					else
					{
						Application.Run(form);
					}
				}
				else
				{
					Application.Exit();
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private static void form_Disposed(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002ADC File Offset: 0x00000CDC
		public static void HandleApplicationException(Exception e)
		{
			DialogResult dr = MessageBox.Show(string.Format("{0} hat einen Fehler verursacht und muss beendet werden. Soll die Anwendung neugestartet werden?\r\n______________________________\r\nFehlerdetails:\r\n{1}", Application.ProductName, e), Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Hand);
			if (dr == DialogResult.Cancel)
			{
				throw e;
			}
			if (dr == DialogResult.Yes)
			{
				Application.Restart();
				return;
			}
			if (dr != DialogResult.No)
			{
				return;
			}
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06000015 RID: 21
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowTitle);

		// Token: 0x06000016 RID: 22
		[DllImport("user32.dll")]
		private static extern int ShowWindow(IntPtr hWnd, FabiProgram.ShowWindowCmd nCmdShow);

		// Token: 0x06000017 RID: 23
		[DllImport("user32.dll")]
		private static extern int SetForegroundWindow(IntPtr hWnd);

		// Token: 0x06000018 RID: 24
		[DllImport("user32.dll")]
		private static extern int IsIconic(IntPtr hWnd);

		// Token: 0x06000019 RID: 25
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x0600001A RID: 26
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		// Token: 0x0600001B RID: 27
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumWindows(FabiProgram.EnumWindowsProc lpEnumFunc, IntPtr lParam);

		// Token: 0x0600001C RID: 28
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600001D RID: 29
		[DllImport("user32.DLL")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600001E RID: 30
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetParent(IntPtr hWnd);

		// Token: 0x0600001F RID: 31 RVA: 0x00002B28 File Offset: 0x00000D28
		public static IntPtr GetCurrentInstanceWindowHandle()
		{
			IntPtr hWnd = IntPtr.Zero;
			Process curProc = Process.GetCurrentProcess();
			Process[] procs = Process.GetProcessesByName(curProc.ProcessName);
			for (int i = 0; i < procs.Length; i++)
			{
				if (procs[i].Id != curProc.Id && procs[i].SessionId == curProc.SessionId)
				{
					if (procs[i].MainWindowHandle != IntPtr.Zero)
					{
						hWnd = procs[i].MainWindowHandle;
					}
					else
					{
						hWnd = FabiProgram.GetProcessWindowHandle((uint)procs[i].Id);
					}
					if (hWnd != IntPtr.Zero)
					{
						break;
					}
				}
			}
			IntPtr p = FabiProgram.FindWindow(null, "Speed AutoClicker");
			return hWnd;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BCB File Offset: 0x00000DCB
		private static IntPtr GetProcessWindowHandle(uint pid)
		{
			FabiProgram.winProcId = pid;
			FabiProgram.winProcHWnd = IntPtr.Zero;
			FabiProgram.EnumWindows(new FabiProgram.EnumWindowsProc(FabiProgram.evalWindow), IntPtr.Zero);
			return FabiProgram.winProcHWnd;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BFC File Offset: 0x00000DFC
		private static bool evalWindow(IntPtr hWnd, IntPtr lParam)
		{
			uint pid;
			FabiProgram.GetWindowThreadProcessId(hWnd, out pid);
			if (pid == FabiProgram.winProcId && FabiProgram.GetParent(hWnd) == IntPtr.Zero)
			{
				FabiProgram.winProcHWnd = hWnd;
				return false;
			}
			return true;
		}

		// Token: 0x06000022 RID: 34
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowPlacement(IntPtr hWnd, ref FabiProgram.Windowplacement lpwndpl);

		// Token: 0x06000023 RID: 35 RVA: 0x00002C38 File Offset: 0x00000E38
		public static bool SwitchToCurrentInstance()
		{
			try
			{
				IntPtr hWnd = FabiProgram.GetCurrentInstanceWindowHandle();
				if (hWnd != IntPtr.Zero)
				{
					FabiProgram.BringToFront(hWnd);
				}
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C7C File Offset: 0x00000E7C
		public static bool BringToFront(IntPtr hWnd)
		{
			FabiProgram.ShowWindow(hWnd, FabiProgram.ShowWindowCmd.SW_RESTORE);
			return FabiProgram.SetForegroundWindow(hWnd) != 0;
		}

		// Token: 0x0400000C RID: 12
		private static uint winProcId;

		// Token: 0x0400000D RID: 13
		private static IntPtr winProcHWnd;

		// Token: 0x0400000E RID: 14
		private const int WM_USER = 1024;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000026 RID: 38
		private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		// Token: 0x02000006 RID: 6
		private enum ShowWindowCmd
		{
			// Token: 0x04000010 RID: 16
			SW_HIDE,
			// Token: 0x04000011 RID: 17
			SW_SHOWNORMAL,
			// Token: 0x04000012 RID: 18
			SW_NORMAL = 1,
			// Token: 0x04000013 RID: 19
			SW_SHOWMINIMIZED,
			// Token: 0x04000014 RID: 20
			SW_SHOWMAXIMIZED,
			// Token: 0x04000015 RID: 21
			SW_MAXIMIZE = 3,
			// Token: 0x04000016 RID: 22
			SW_SHOWNOACTIVATE,
			// Token: 0x04000017 RID: 23
			SW_SHOW,
			// Token: 0x04000018 RID: 24
			SW_MINIMIZE,
			// Token: 0x04000019 RID: 25
			SW_SHOWMINNOACTIVE,
			// Token: 0x0400001A RID: 26
			SW_SHOWNA,
			// Token: 0x0400001B RID: 27
			SW_RESTORE,
			// Token: 0x0400001C RID: 28
			SW_SHOWDEFAULT,
			// Token: 0x0400001D RID: 29
			SW_FORCEMINIMIZE,
			// Token: 0x0400001E RID: 30
			SW_MAX = 11
		}

		// Token: 0x02000007 RID: 7
		private struct Windowplacement
		{
			// Token: 0x0400001F RID: 31
			public int length;

			// Token: 0x04000020 RID: 32
			public int flags;

			// Token: 0x04000021 RID: 33
			public int showCmd;

			// Token: 0x04000022 RID: 34
			public Point ptMinPosition;

			// Token: 0x04000023 RID: 35
			public Point ptMaxPosition;

			// Token: 0x04000024 RID: 36
			public Rectangle rcNormalPosition;
		}
	}
}

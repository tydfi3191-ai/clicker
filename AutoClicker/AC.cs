using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000018 RID: 24
	internal class AC
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A4 RID: 164 RVA: 0x000041C0 File Offset: 0x000023C0
		// (remove) Token: 0x060000A5 RID: 165 RVA: 0x000041F8 File Offset: 0x000023F8
		public event AC.StateChangedEventHandler StateChanged;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000422D File Offset: 0x0000242D
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004235 File Offset: 0x00002435
		public Keys[] ActivationKey
		{
			get
			{
				return this.hotkey;
			}
			set
			{
				this.hotkey = value;
				this.Restart();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000424C File Offset: 0x0000244C
		public ActivationMode ActivationMode
		{
			get
			{
				return this.actMode;
			}
			set
			{
				this.actMode = value;
				this.Restart();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000425B File Offset: 0x0000245B
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004263 File Offset: 0x00002463
		public MouseBtns MouseButton
		{
			get
			{
				return this.button;
			}
			set
			{
				this.button = value;
				this.Restart();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000429D File Offset: 0x0000249D
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004272 File Offset: 0x00002472
		public float ClickRate
		{
			get
			{
				return this.clickRate;
			}
			set
			{
				if (this.UnlimitedClickRate)
				{
					this.UnlimitedClickRate = false;
				}
				this.clickRate = value;
				this.clickLatency = 1000f / value;
				this.Restart();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000042A5 File Offset: 0x000024A5
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000042B4 File Offset: 0x000024B4
		public bool UnlimitedClickRate
		{
			get
			{
				return this.clickLatency == 0f;
			}
			set
			{
				if (value)
				{
					this.clickLatency = 0f;
				}
				else
				{
					this.clickLatency = 1000f / this.clickRate;
				}
				this.Restart();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000042DE File Offset: 0x000024DE
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000042E6 File Offset: 0x000024E6
		public bool RandomizeClickRate
		{
			get
			{
				return this.randClickRate;
			}
			set
			{
				if (this.randClickRate != value)
				{
					this.randClickRate = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000042FE File Offset: 0x000024FE
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004306 File Offset: 0x00002506
		public uint ClickLimit
		{
			get
			{
				return this.clickLimit;
			}
			set
			{
				if (this.clickLimit == value)
				{
					return;
				}
				this.clickLimit = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004319 File Offset: 0x00002519
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004327 File Offset: 0x00002527
		public float ClickCycle
		{
			get
			{
				return this.clickCycle * 100f;
			}
			set
			{
				this.clickCycle = value / 100f;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004336 File Offset: 0x00002536
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000433E File Offset: 0x0000253E
		public bool SendStats
		{
			get
			{
				return this.sendStats;
			}
			set
			{
				this.sendStats = value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004348 File Offset: 0x00002548
		public static string normalizeWindowTitle(string title)
		{
			title = Regex.Replace(title, "\\d[\\d.a-zA-Z-]+", string.Empty);
			title = Regex.Replace(title, "\\([^\\)]+\\)", string.Empty);
			string title2 = Regex.Replace(title, "[a-zA-Z]:\\\\.+\\\\[a-zA-Z0-9-_.()]+", string.Empty);
			if (title2.Length < 4)
			{
				title2 = title;
			}
			title = Regex.Replace(title2, "[-* ]", string.Empty);
			return title;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000043AA File Offset: 0x000025AA
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000043B4 File Offset: 0x000025B4
		public string[] WindowTitles
		{
			get
			{
				return this.windowTitles;
			}
			set
			{
				this.windowTitles = new string[value.Length];
				value.CopyTo(this.windowTitles, 0);
				for (int i = 0; i < value.Length; i++)
				{
					this.windowTitles[i] = AC.normalizeWindowTitle(value[i]);
				}
				this.Restart();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004400 File Offset: 0x00002600
		public ACStats Stats
		{
			get
			{
				return this.stats;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004408 File Offset: 0x00002608
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004414 File Offset: 0x00002614
		private bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				if (this._active == value)
				{
					return;
				}
				if (!value)
				{
					this.stats.AddClicks(this.clickCounter, this.hotkey);
				}
				this.clickCounter = 0UL;
				this._active = value;
				bool active = this._active;
				if (this.StateChanged != null)
				{
					this.StateChanged(this._active, this.enabled);
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004484 File Offset: 0x00002684
		public AC()
		{
			this.rand = new Random();
			this.stats = new ACStats();
			Application.ApplicationExit += this.Application_ApplicationExit;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000044D8 File Offset: 0x000026D8
		public void SubmitStats(float daemonRunRatio)
		{
			if (this.SendStats)
			{
				try
				{
					this.stats.SendToServer(daemonRunRatio);
				}
				catch (Exception e)
				{
					SentrySdk.CaptureException(e);
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004518 File Offset: 0x00002718
		private void Application_ApplicationExit(object sender, EventArgs e)
		{
			this.Stop();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004520 File Offset: 0x00002720
		~AC()
		{
			this.Stop();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000454C File Offset: 0x0000274C
		public void Start()
		{
			if (this.enabled)
			{
				return;
			}
			this.enabled = true;
			Thread.Sleep(20);
			if (this.hotkey == null)
			{
				this.hotkey = new Keys[0];
			}
			bool same = Array.IndexOf<Keys>(this.hotkey, AC.mouseBtnToKey(this.button)) != -1;
			this.clickThread = new Thread(same ? new ThreadStart(this.runClickSame) : new ThreadStart(this.runClick));
			this.clickThread.Priority = ThreadPriority.Highest;
			this.clickThread.Name = "click";
			this.clickThread.Start();
			if (!same)
			{
				this.inputThread = new Thread(delegate()
				{
					bool wasReleased = false;
					while (this.enabled)
					{
						bool isPressed = InputLib.KeysArePressed(this.hotkey);
						if (this.actMode == ActivationMode.Switch)
						{
							if (isPressed && wasReleased)
							{
								this.active = !this.active;
								wasReleased = false;
							}
							wasReleased = (wasReleased || !isPressed);
						}
						else if (!isPressed)
						{
							this.active = false;
						}
						Thread.Sleep(10);
					}
				});
				this.inputThread.Name = "input";
				this.inputThread.Start();
			}
			AC.StateChangedEventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this.active, this.enabled);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004647 File Offset: 0x00002847
		public void Restart()
		{
			if (!this.enabled)
			{
				return;
			}
			this.Stop();
			this.Start();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004660 File Offset: 0x00002860
		public void Stop()
		{
			this.enabled = (this._active = false);
			int i = 0;
			while ((this.clickThread != null && this.clickThread.IsAlive) || (this.inputThread != null && this.inputThread.IsAlive))
			{
				Thread.Sleep(5);
				if (i++ > 100)
				{
					break;
				}
			}
			this.clickThread = null;
			this.inputThread = null;
			if (this.StateChanged != null)
			{
				this.StateChanged(this.active, this.enabled);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000046ED File Offset: 0x000028ED
		private static bool isHighLatency(float ms)
		{
			return ms > 10f;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000046F7 File Offset: 0x000028F7
		private float getNextRandLatency()
		{
			return (float)this.rand.NextDouble() * 2f * this.clickLatency;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004714 File Offset: 0x00002914
		private void runClick()
		{
			int cbSize = Marshal.SizeOf(typeof(InputLib.INPUT));
			if (this.UnlimitedClickRate)
			{
				InputLib.INPUT inp = AC.genMI(this.button, true, true);
				InputLib.INPUT[] inps = new InputLib.INPUT[]
				{
					inp,
					inp,
					inp,
					inp,
					inp,
					inp,
					inp,
					inp
				};
				while (this.enabled)
				{
					if (!this.active)
					{
						this.pollInput();
					}
					else
					{
						InputLib.SendInput(8U, inps, cbSize);
						this.clickCounter += 8UL;
					}
				}
				return;
			}
			InputLib.INPUT[] miDU = new InputLib.INPUT[]
			{
				AC.genMI(this.button, true, true)
			};
			InputLib.INPUT[] miD = new InputLib.INPUT[]
			{
				AC.genMI(this.button, true, false)
			};
			InputLib.INPUT[] miU = new InputLib.INPUT[]
			{
				AC.genMI(this.button, false, true)
			};
			Keys[] array = new Keys[]
			{
				AC.mouseBtnToKey(this.button)
			};
			bool longSleep = AC.isHighLatency(this.clickLatency);
			float tmpSleepTime = 0f;
			while (this.enabled)
			{
				if (!this.active)
				{
					this.pollInput();
				}
				else
				{
					tmpSleepTime += (this.randClickRate ? this.getNextRandLatency() : this.clickLatency);
					int period = (int)Math.Round((double)tmpSleepTime);
					int clickTime = (int)((float)period * this.clickCycle);
					if (clickTime > 0)
					{
						InputLib.SendInput(1U, miD, cbSize);
						if (longSleep)
						{
							this.sleepWhileActive(clickTime);
						}
						else
						{
							Thread.Sleep(clickTime);
						}
						InputLib.SendInput(1U, miU, cbSize);
					}
					else
					{
						InputLib.SendInput(1U, miDU, cbSize);
					}
					this.clickCounter += 1UL;
					if (this.clickLimit > 0U && this.clickCounter >= (ulong)this.clickLimit)
					{
						this.active = false;
					}
					else
					{
						if (longSleep)
						{
							this.sleepWhileActive(period - clickTime);
						}
						else
						{
							Thread.Sleep(period - clickTime);
						}
						tmpSleepTime -= (float)period;
					}
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004930 File Offset: 0x00002B30
		private void pollInput()
		{
			Thread.Sleep(5);
			if (this.actMode == ActivationMode.Switch)
			{
				return;
			}
			if (!InputLib.KeysArePressed(this.hotkey))
			{
				return;
			}
			uint apid = ACStats.GetActiveWindowPID();
			if ((ulong)apid == (ulong)((long)Process.GetCurrentProcess().Id))
			{
				InputLib.INPUT[] mi = new InputLib.INPUT[]
				{
					AC.genMI(this.button, true, true)
				};
				InputLib.SendInput(1U, mi, Marshal.SizeOf(typeof(InputLib.INPUT)));
				Thread.Sleep(40);
				apid = ACStats.GetActiveWindowPID();
				if ((ulong)apid == (ulong)((long)Process.GetCurrentProcess().Id))
				{
					return;
				}
			}
			if (this.windowTitles != null && this.windowTitles.Length != 0)
			{
				Process ap = Process.GetProcessById((int)apid);
				if (ap == null)
				{
					return;
				}
				if (Array.IndexOf<string>(this.windowTitles, AC.normalizeWindowTitle(ap.MainWindowTitle)) == -1)
				{
					return;
				}
			}
			this.active = true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000049FC File Offset: 0x00002BFC
		private void runClickSame()
		{
			int cbSize = Marshal.SizeOf(typeof(InputLib.INPUT));
			InputLib.INPUT[] miUD = new InputLib.INPUT[]
			{
				AC.genMI(this.button, false, true),
				AC.genMI(this.button, true, false)
			};
			Keys watchB = AC.mouseBtnToKey(this.button);
			float tmpSleepTime = 0f;
			while (this.enabled)
			{
				if (!this.active)
				{
					this.pollInput();
				}
				else if (!InputLib.KeyIsPressed(watchB))
				{
					this.active = false;
				}
				else
				{
					InputLib.SendInput((uint)miUD.Length, miUD, cbSize);
					this.clickCounter += 1UL;
					if (this.clickLimit > 0U && this.clickCounter >= (ulong)this.clickLimit)
					{
						this.active = false;
					}
					else
					{
						tmpSleepTime += Math.Min(100f, this.randClickRate ? this.getNextRandLatency() : this.clickLatency);
						int period = (int)Math.Round((double)tmpSleepTime);
						this.sleepWhileActive(period);
						tmpSleepTime -= (float)period;
					}
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004B08 File Offset: 0x00002D08
		private bool sleepWhileActive(int ms)
		{
			if (!this.enabled || !this.active)
			{
				return false;
			}
			while (ms >= 10)
			{
				Thread.Sleep(10);
				ms -= 10;
				if (!this.enabled || !this.active)
				{
					return false;
				}
			}
			if (ms > 0)
			{
				Thread.Sleep(ms);
			}
			return true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004B5C File Offset: 0x00002D5C
		private static InputLib.INPUT genMI(MouseBtns btn, bool down, bool up)
		{
			InputLib.INPUT inp = default(InputLib.INPUT);
			inp.type = 0;
			inp.mi.dx = 0;
			inp.mi.dy = 0;
			inp.mi.dwFlags = 0U;
			if (down)
			{
				inp.mi.dwFlags = (uint)((int)inp.mi.dwFlags + btn);
			}
			if (up)
			{
				inp.mi.dwFlags = (uint)((int)inp.mi.dwFlags + btn * MouseBtns.Left);
			}
			return inp;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004BCB File Offset: 0x00002DCB
		private static Keys mouseBtnToKey(MouseBtns btn)
		{
			if (btn <= MouseBtns.Right)
			{
				if (btn == MouseBtns.Left)
				{
					return Keys.LButton;
				}
				if (btn == MouseBtns.Right)
				{
					return Keys.RButton;
				}
			}
			else
			{
				if (btn == MouseBtns.Middle)
				{
					return Keys.MButton;
				}
				if (btn == MouseBtns.X)
				{
					return Keys.XButton1;
				}
			}
			return Keys.None;
		}

		// Token: 0x04000066 RID: 102
		private const int UPDATE_KEYSTATE_LATENCY = 10;

		// Token: 0x04000067 RID: 103
		private const int AA_MAX_CLICK_RATE = 10;

		// Token: 0x04000069 RID: 105
		private volatile bool enabled;

		// Token: 0x0400006A RID: 106
		private volatile bool _active;

		// Token: 0x0400006B RID: 107
		private Thread clickThread;

		// Token: 0x0400006C RID: 108
		private Thread inputThread;

		// Token: 0x0400006D RID: 109
		private MouseBtns button = MouseBtns.Left;

		// Token: 0x0400006E RID: 110
		private Random rand;

		// Token: 0x0400006F RID: 111
		private float clickLatency = 20f;

		// Token: 0x04000070 RID: 112
		private ulong clickCounter;

		// Token: 0x04000071 RID: 113
		private Keys[] hotkey;

		// Token: 0x04000072 RID: 114
		private ActivationMode actMode = ActivationMode.Hold;

		// Token: 0x04000073 RID: 115
		private float clickRate;

		// Token: 0x04000074 RID: 116
		private bool randClickRate;

		// Token: 0x04000075 RID: 117
		private uint clickLimit;

		// Token: 0x04000076 RID: 118
		private float clickCycle;

		// Token: 0x04000077 RID: 119
		private ACStats stats;

		// Token: 0x04000078 RID: 120
		private bool sendStats;

		// Token: 0x04000079 RID: 121
		private string[] windowTitles;

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x060000CF RID: 207
		public delegate void StateChangedEventHandler(bool active, bool enabled);
	}
}

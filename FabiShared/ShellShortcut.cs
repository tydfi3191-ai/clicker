using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FabiShared
{
	// Token: 0x02000013 RID: 19
	public class ShellShortcut : IDisposable
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000035C8 File Offset: 0x000017C8
		public ShellShortcut(string linkPath)
		{
			this.m_sPath = linkPath;
			this.m_Link = (IShellLinkA)new ShellLink();
			if (File.Exists(linkPath))
			{
				IPersistFile pf = (IPersistFile)this.m_Link;
				pf.Load(linkPath, 0);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000360E File Offset: 0x0000180E
		public void Dispose()
		{
			if (this.m_Link != null)
			{
				Marshal.ReleaseComObject(this.m_Link);
				this.m_Link = null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000362C File Offset: 0x0000182C
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000365C File Offset: 0x0000185C
		public string Arguments
		{
			get
			{
				StringBuilder sb = new StringBuilder(1024);
				this.m_Link.GetArguments(sb, sb.Capacity);
				return sb.ToString();
			}
			set
			{
				this.m_Link.SetArguments(value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000366C File Offset: 0x0000186C
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000369C File Offset: 0x0000189C
		public string Description
		{
			get
			{
				StringBuilder sb = new StringBuilder(1024);
				this.m_Link.GetDescription(sb, sb.Capacity);
				return sb.ToString();
			}
			set
			{
				this.m_Link.SetDescription(value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000036AC File Offset: 0x000018AC
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000036DC File Offset: 0x000018DC
		public string WorkingDirectory
		{
			get
			{
				StringBuilder sb = new StringBuilder(260);
				this.m_Link.GetWorkingDirectory(sb, sb.Capacity);
				return sb.ToString();
			}
			set
			{
				this.m_Link.SetWorkingDirectory(value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000036EC File Offset: 0x000018EC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003727 File Offset: 0x00001927
		public string Path
		{
			get
			{
				WIN32_FIND_DATAA wfd = default(WIN32_FIND_DATAA);
				StringBuilder sb = new StringBuilder(260);
				this.m_Link.GetPath(sb, sb.Capacity, out wfd, SLGP_FLAGS.SLGP_UNCPRIORITY);
				return sb.ToString();
			}
			set
			{
				this.m_Link.SetPath(value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003738 File Offset: 0x00001938
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000376A File Offset: 0x0000196A
		public string IconPath
		{
			get
			{
				StringBuilder sb = new StringBuilder(260);
				int nIconIdx;
				this.m_Link.GetIconLocation(sb, sb.Capacity, out nIconIdx);
				return sb.ToString();
			}
			set
			{
				this.m_Link.SetIconLocation(value, this.IconIndex);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003780 File Offset: 0x00001980
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000037AD File Offset: 0x000019AD
		public int IconIndex
		{
			get
			{
				StringBuilder sb = new StringBuilder(260);
				int nIconIdx;
				this.m_Link.GetIconLocation(sb, sb.Capacity, out nIconIdx);
				return nIconIdx;
			}
			set
			{
				this.m_Link.SetIconLocation(this.IconPath, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000037C4 File Offset: 0x000019C4
		public Icon Icon
		{
			get
			{
				StringBuilder sb = new StringBuilder(260);
				int nIconIdx;
				this.m_Link.GetIconLocation(sb, sb.Capacity, out nIconIdx);
				IntPtr hInst = Marshal.GetHINSTANCE(base.GetType().Module);
				IntPtr hIcon = Misc.ExtractIcon(hInst, sb.ToString(), nIconIdx);
				if (hIcon == IntPtr.Zero)
				{
					return null;
				}
				Icon ico = Icon.FromHandle(hIcon);
				Icon clone = (Icon)ico.Clone();
				ico.Dispose();
				Misc.DestroyIcon(hIcon);
				return clone;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003844 File Offset: 0x00001A44
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003870 File Offset: 0x00001A70
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				int nWS;
				this.m_Link.GetShowCmd(out nWS);
				if (nWS != 2)
				{
					if (nWS == 3)
					{
						return ProcessWindowStyle.Maximized;
					}
					if (nWS != 7)
					{
						return ProcessWindowStyle.Normal;
					}
				}
				return ProcessWindowStyle.Minimized;
			}
			set
			{
				int nWS;
				switch (value)
				{
				case ProcessWindowStyle.Normal:
					nWS = 1;
					goto IL_2F;
				case ProcessWindowStyle.Minimized:
					nWS = 7;
					goto IL_2F;
				case ProcessWindowStyle.Maximized:
					nWS = 3;
					goto IL_2F;
				}
				throw new ArgumentException("Unsupported ProcessWindowStyle value.");
				IL_2F:
				this.m_Link.SetShowCmd(nWS);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000038B8 File Offset: 0x00001AB8
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000038E8 File Offset: 0x00001AE8
		public Keys Hotkey
		{
			get
			{
				short wHotkey;
				this.m_Link.GetHotkey(out wHotkey);
				return (Keys)(((int)wHotkey & 65280) << 8 | (int)(wHotkey & 255));
			}
			set
			{
				if ((value & Keys.Modifiers) == Keys.None)
				{
					throw new ArgumentException("Hotkey must include a modifier key.");
				}
				short wHotkey = (short)((value & Keys.Modifiers) >> 8 | (value & Keys.KeyCode));
				this.m_Link.SetHotkey(wHotkey);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003928 File Offset: 0x00001B28
		public void Save()
		{
			IPersistFile pf = (IPersistFile)this.m_Link;
			pf.Save(this.m_sPath, true);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000394E File Offset: 0x00001B4E
		public object ShellLink
		{
			get
			{
				return this.m_Link;
			}
		}

		// Token: 0x04000055 RID: 85
		private const int INFOTIPSIZE = 1024;

		// Token: 0x04000056 RID: 86
		private const int MAX_PATH = 260;

		// Token: 0x04000057 RID: 87
		private const int SW_SHOWNORMAL = 1;

		// Token: 0x04000058 RID: 88
		private const int SW_SHOWMINIMIZED = 2;

		// Token: 0x04000059 RID: 89
		private const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x0400005A RID: 90
		private const int SW_SHOWMINNOACTIVE = 7;

		// Token: 0x0400005B RID: 91
		private IShellLinkA m_Link;

		// Token: 0x0400005C RID: 92
		private string m_sPath;
	}
}

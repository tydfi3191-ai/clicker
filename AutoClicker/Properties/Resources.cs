using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AutoClicker.Properties
{
	// Token: 0x02000036 RID: 54
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000035BF File Offset: 0x000017BF
		internal Resources()
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000A968 File Offset: 0x00008B68
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("AutoClicker.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000A9A1 File Offset: 0x00008BA1
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		internal static Bitmap Gear
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("Gear", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		internal static Bitmap Mouse
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("Mouse", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000AA00 File Offset: 0x00008C00
		internal static Bitmap MouseBtnLeft
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("MouseBtnLeft", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000AA28 File Offset: 0x00008C28
		internal static Bitmap MouseBtnMiddle
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("MouseBtnMiddle", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000AA50 File Offset: 0x00008C50
		internal static Bitmap MouseBtnRight
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("MouseBtnRight", Resources.resourceCulture);
				return (Bitmap)obj;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000AA78 File Offset: 0x00008C78
		internal static Icon TrayIcon
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("TrayIcon", Resources.resourceCulture);
				return (Icon)obj;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		internal static Icon TrayIconActive
		{
			get
			{
				object obj = Resources.ResourceManager.GetObject("TrayIconActive", Resources.resourceCulture);
				return (Icon)obj;
			}
		}

		// Token: 0x04000128 RID: 296
		private static ResourceManager resourceMan;

		// Token: 0x04000129 RID: 297
		private static CultureInfo resourceCulture;
	}
}

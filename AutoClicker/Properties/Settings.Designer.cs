using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AutoClicker.Properties
{
	// Token: 0x02000037 RID: 55
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000AACF File Offset: 0x00008CCF
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000AAE1 File Offset: 0x00008CE1
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Hold")]
		public ActivationMode ActivationMode
		{
			get
			{
				return (ActivationMode)this["ActivationMode"];
			}
			set
			{
				this["ActivationMode"] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000AB06 File Offset: 0x00008D06
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("Left")]
		public MouseBtns MouseButton
		{
			get
			{
				return (MouseBtns)this["MouseButton"];
			}
			set
			{
				this["MouseButton"] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000AB19 File Offset: 0x00008D19
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000AB2B File Offset: 0x00008D2B
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("10")]
		public float ClickRate
		{
			get
			{
				return (float)this["ClickRate"];
			}
			set
			{
				this["ClickRate"] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000AB3E File Offset: 0x00008D3E
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000AB50 File Offset: 0x00008D50
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool UnlimitedClickRate
		{
			get
			{
				return (bool)this["UnlimitedClickRate"];
			}
			set
			{
				this["UnlimitedClickRate"] = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000AB63 File Offset: 0x00008D63
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000AB75 File Offset: 0x00008D75
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool Startup
		{
			get
			{
				return (bool)this["Startup"];
			}
			set
			{
				this["Startup"] = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000AB88 File Offset: 0x00008D88
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000AB9A File Offset: 0x00008D9A
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool EnableClickLimit
		{
			get
			{
				return (bool)this["EnableClickLimit"];
			}
			set
			{
				this["EnableClickLimit"] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000ABAD File Offset: 0x00008DAD
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000ABBF File Offset: 0x00008DBF
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("1000")]
		public uint ClickLimit
		{
			get
			{
				return (uint)this["ClickLimit"];
			}
			set
			{
				this["ClickLimit"] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000ABD2 File Offset: 0x00008DD2
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool RandomizeClickRate
		{
			get
			{
				return (bool)this["RandomizeClickRate"];
			}
			set
			{
				this["RandomizeClickRate"] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000AC09 File Offset: 0x00008E09
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("50")]
		public float ClickCycle
		{
			get
			{
				return (float)this["ClickCycle"];
			}
			set
			{
				this["ClickCycle"] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000AC1C File Offset: 0x00008E1C
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000AC2E File Offset: 0x00008E2E
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool SendStats
		{
			get
			{
				return (bool)this["SendStats"];
			}
			set
			{
				this["SendStats"] = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000AC41 File Offset: 0x00008E41
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000AC53 File Offset: 0x00008E53
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool ShowAdvSettings
		{
			get
			{
				return (bool)this["ShowAdvSettings"];
			}
			set
			{
				this["ShowAdvSettings"] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000AC66 File Offset: 0x00008E66
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000AC78 File Offset: 0x00008E78
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool UpgradeRequired
		{
			get
			{
				return (bool)this["UpgradeRequired"];
			}
			set
			{
				this["UpgradeRequired"] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000AC8B File Offset: 0x00008E8B
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000AC9D File Offset: 0x00008E9D
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Language
		{
			get
			{
				return (string)this["Language"];
			}
			set
			{
				this["Language"] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000ACAB File Offset: 0x00008EAB
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000ACBD File Offset: 0x00008EBD
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool StartMinimized
		{
			get
			{
				return (bool)this["StartMinimized"];
			}
			set
			{
				this["StartMinimized"] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000ACE2 File Offset: 0x00008EE2
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>MButton</string>\r\n</ArrayOfString>")]
		public StringCollection Hotkey
		{
			get
			{
				return (StringCollection)this["Hotkey"];
			}
			set
			{
				this["Hotkey"] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000AD02 File Offset: 0x00008F02
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool HideWindow
		{
			get
			{
				return (bool)this["HideWindow"];
			}
			set
			{
				this["HideWindow"] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000AD35 File Offset: 0x00008F35
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000AD47 File Offset: 0x00008F47
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool AllWindows
		{
			get
			{
				return (bool)this["AllWindows"];
			}
			set
			{
				this["AllWindows"] = value;
			}
		}

		// Token: 0x0400012A RID: 298
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}

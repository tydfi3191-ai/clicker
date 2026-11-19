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
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000AD15 File Offset: 0x00008F15
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000AD27 File Offset: 0x00008F27
		[UserScopedSetting]
		[SettingsProvider(typeof(ACSettingsProvider))]
		[DebuggerNonUserCode]
		public StringCollection WindowTitles
		{
			get
			{
				return (StringCollection)this["WindowTitles"];
			}
			set
			{
				this["WindowTitles"] = value;
			}
		}
	}
}

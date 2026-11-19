using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace AutoClicker.Wizard
{
	// Token: 0x0200003C RID: 60
	public class WizardFormTypeEditor : UITypeEditor
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000B3AD File Offset: 0x000095AD
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			return base.EditValue(context, provider, value);
		}
	}
}

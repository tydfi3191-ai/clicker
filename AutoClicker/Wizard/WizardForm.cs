using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoClicker.Properties;

namespace AutoClicker.Wizard
{
	// Token: 0x0200003A RID: 58
	public partial class WizardForm : Form
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000AE65 File Offset: 0x00009065
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000AE6D File Offset: 0x0000906D
		[Description("Next Wizard Form")]
		[Category("Wizard")]
		[DefaultValue(null)]
		[Browsable(true)]
		[TypeConverter(typeof(WizardFormTypeConverter))]
		public Type NextForm
		{
			get
			{
				return this.nextForm;
			}
			set
			{
				this.nextForm = value;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AE76 File Offset: 0x00009076
		public WizardForm()
		{
			this.InitializeComponent();
			base.Icon = Resources.TrayIcon;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000AE8F File Offset: 0x0000908F
		private WizardForm(WizardResult wr)
		{
			this.wizardResult = wr;
			this.InitializeComponent();
			base.Icon = Resources.TrayIcon;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000AEAF File Offset: 0x000090AF
		public WizardResult RunWizard()
		{
			this.wizardResult = new WizardResult();
			base.ShowDialog();
			this.btnPrev.Enabled = false;
			return this.wizardResult;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003311 File Offset: 0x00001511
		private void WizardForm_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000AED5 File Offset: 0x000090D5
		private void btnPrev_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Retry;
			base.Close();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000AEE4 File Offset: 0x000090E4
		private void btnNext_Click(object sender, EventArgs e)
		{
			if (this.nextForm != null)
			{
				base.Hide();
				using (WizardForm form = (WizardForm)Activator.CreateInstance(this.nextForm, new object[]
				{
					this.wizardResult
				}))
				{
					DialogResult dr = form.ShowDialog(this);
					if (dr == DialogResult.Cancel)
					{
						base.DialogResult = dr;
						base.Close();
						return;
					}
					if (dr == DialogResult.Retry)
					{
						base.Show();
					}
					return;
				}
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00003311 File Offset: 0x00001511
		private void btnCancel_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00003311 File Offset: 0x00001511
		private void WizardForm_Shown(object sender, EventArgs e)
		{
		}

		// Token: 0x0400012D RID: 301
		private Type nextForm;

		// Token: 0x0400012E RID: 302
		private WizardResult wizardResult;
	}
}

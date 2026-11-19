using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace FabiShared
{
	// Token: 0x02000002 RID: 2
	public partial class AboutForm : Form
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return string.Empty;
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return string.Empty;
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public AboutForm()
		{
			this.InitializeComponent();
			base.Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
			this.picIcon.Image = base.Icon.ToBitmap();
			this.Text = Application.ProductName;
			this.lbAppName.Text = Application.ProductName;
			this.lbAppVersionVal.Text = Application.ProductVersion;
			this.lbCopyrightVal.Text = AboutForm.AssemblyCopyright;
			this.llbWebsite.Text = string.Format("http://{0}/", this.AssemblyCompany);
			this.llbWebsite.Visible = !string.IsNullOrEmpty(this.AssemblyCompany);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002183 File Offset: 0x00000383
		private void llbWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(this.llbWebsite.Text);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002196 File Offset: 0x00000396
		private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (base.Icon != null)
			{
				base.Icon.Dispose();
			}
		}
	}
}

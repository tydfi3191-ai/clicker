using System;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000033 RID: 51
	internal class Updater
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000A844 File Offset: 0x00008A44
		public Updater(string apiUrl, long postId)
		{
			this.url = apiUrl + "wp/v2/wpfb-files/" + postId;
			this.client = new WebClient();
			this.client.DownloadStringCompleted += this.Client_DownloadStringCompleted;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A890 File Offset: 0x00008A90
		private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			try
			{
				Updater.apiFile file = new JavaScriptSerializer().Deserialize<Updater.apiFile>(e.Result);
				Updater.CheckResponse resp;
				resp.articleUrl = file.file_article_link;
				resp.articleUrl = resp.articleUrl + (resp.articleUrl.Contains('?') ? "&" : "?") + "utm_source=updater&utm_medium=app&utm_campaign=update";
				resp.latestVersion = file.file_version;
				Version v = new Version(file.file_version);
				resp.updateAvailable = (v > new Version(Application.ProductVersion));
				Action<Updater.CheckResponse> callback = e.UserState as Action<Updater.CheckResponse>;
				callback(resp);
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A94C File Offset: 0x00008B4C
		public void Check(Action<Updater.CheckResponse> callback)
		{
			this.client.DownloadStringAsync(new Uri(this.url), callback);
		}

		// Token: 0x04000120 RID: 288
		private WebClient client;

		// Token: 0x04000121 RID: 289
		private string url;

		// Token: 0x02000034 RID: 52
		public struct CheckResponse
		{
			// Token: 0x04000122 RID: 290
			public string latestVersion;

			// Token: 0x04000123 RID: 291
			public bool updateAvailable;

			// Token: 0x04000124 RID: 292
			public string articleUrl;
		}

		// Token: 0x02000035 RID: 53
		private struct apiFile
		{
			// Token: 0x04000125 RID: 293
			public string file_link;

			// Token: 0x04000126 RID: 294
			public string file_article_link;

			// Token: 0x04000127 RID: 295
			public string file_version;
		}
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Sentry;

namespace AutoClicker
{
	// Token: 0x02000020 RID: 32
	internal class Loader
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00005D08 File Offset: 0x00003F08
		public Loader(string apiUrl, long postId, string localName)
		{
			this.binName = localName;
			string binDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "fabi.me");
			this.binPath = Path.Combine(binDir, this.binName);
			if (!Directory.Exists(binDir))
			{
				Directory.CreateDirectory(binDir);
			}
			this.localMtime = 0L;
			try
			{
				this.localMtime = (File.GetLastWriteTime(this.binPath) - new DateTime(1970, 1, 1)).Ticks / 10000000L;
			}
			catch (Exception e)
			{
				SentrySdk.CaptureException(e);
			}
			this.fileEndpoint = apiUrl + "wp/v2/wpfb-files/" + postId;
			this.client = new WebClient();
			this.client.DownloadStringCompleted += this.Client_DownloadStringCompleted;
			this.client.DownloadFileCompleted += this.Client_DownloadFileCompleted;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005DFC File Offset: 0x00003FFC
		~Loader()
		{
			if (!string.IsNullOrEmpty(this.tmpFile) && File.Exists(this.tmpFile))
			{
				File.Delete(this.tmpFile);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005E48 File Offset: 0x00004048
		private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			Loader.RequestUserData<Loader.DownloadResult> req = e.UserState as Loader.RequestUserData<Loader.DownloadResult>;
			try
			{
				if (e.Error != null)
				{
					throw e.Error;
				}
				if (File.Exists(this.binPath))
				{
					File.Delete(this.binPath);
				}
				File.Move(this.tmpFile, this.binPath);
				File.SetLastWriteTime(this.binPath, new DateTime(1970, 1, 1).AddSeconds((double)this.serverFile.file_mtime));
				Action<Loader.DownloadResult> callback = req.callback;
				if (callback != null)
				{
					callback(default(Loader.DownloadResult));
				}
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				Action<Loader.DownloadResult> callback2 = req.callback;
				if (callback2 != null)
				{
					callback2(new Loader.DownloadResult(ex));
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005F14 File Offset: 0x00004114
		public void CheckNeedDownload(Action<Loader.CheckResponse> callback)
		{
			try
			{
				SentrySdk.AddBreadcrumb(this.fileEndpoint, "fileEndpoint", null, null, 0);
				this.client.DownloadStringAsync(new Uri(this.fileEndpoint), new Loader.RequestUserData<Loader.CheckResponse>
				{
					url = this.fileEndpoint,
					callback = callback
				});
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
				callback(new Loader.CheckResponse(ex.Message));
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005F90 File Offset: 0x00004190
		public void Download(Action<Loader.DownloadResult> callback)
		{
			try
			{
				Process cp = Process.GetCurrentProcess();
				Process[] procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(this.binName));
				foreach (Process p in procs)
				{
					if (cp.Id != p.Id)
					{
						p.Kill();
					}
				}
			}
			catch (Exception ex)
			{
				SentrySdk.CaptureException(ex);
			}
			if (string.IsNullOrEmpty(this.serverFile.file_link))
			{
				callback(new Loader.DownloadResult(new Exception("Server data error! Please check your internet connection.")));
				return;
			}
			this.tmpFile = Path.GetTempFileName();
			SentrySdk.AddBreadcrumb(this.serverFile.file_link, "serverFile.file_link", null, null, 0);
			this.client.DownloadFileAsync(new Uri(this.serverFile.file_link), this.tmpFile, new Loader.RequestUserData<Loader.DownloadResult>
			{
				url = this.serverFile.file_link,
				callback = callback
			});
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006088 File Offset: 0x00004288
		private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			Loader.RequestUserData<Loader.CheckResponse> req = e.UserState as Loader.RequestUserData<Loader.CheckResponse>;
			try
			{
				if (e.Error != null)
				{
					throw e.Error;
				}
				this.serverFile = new JavaScriptSerializer().Deserialize<Loader.apiFile>(e.Result);
				Loader.CheckResponse res = new Loader.CheckResponse(this.localMtime, this.serverFile.file_mtime);
				Action<Loader.CheckResponse> callback = req.callback;
				if (callback != null)
				{
					callback(res);
				}
			}
			catch (Exception ex)
			{
				Loader.CheckResponse res2 = (this.localMtime == 0L) ? new Loader.CheckResponse(ex.Message) : new Loader.CheckResponse(this.localMtime, 0L);
				Action<Loader.CheckResponse> callback2 = req.callback;
				if (callback2 != null)
				{
					callback2(res2);
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000613C File Offset: 0x0000433C
		public Process Start()
		{
			return Process.Start(this.binPath);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006149 File Offset: 0x00004349
		public string GetBinPath()
		{
			return this.binPath;
		}

		// Token: 0x040000A1 RID: 161
		private string binName;

		// Token: 0x040000A2 RID: 162
		private string binPath;

		// Token: 0x040000A3 RID: 163
		private long localMtime;

		// Token: 0x040000A4 RID: 164
		private WebClient client;

		// Token: 0x040000A5 RID: 165
		private string fileEndpoint;

		// Token: 0x040000A6 RID: 166
		private string tmpFile;

		// Token: 0x040000A7 RID: 167
		private Loader.apiFile serverFile;

		// Token: 0x02000021 RID: 33
		public struct CheckResponse
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000105 RID: 261 RVA: 0x00006151 File Offset: 0x00004351
			public bool needDownload
			{
				get
				{
					return this.serverMtime != this.localMtime;
				}
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00006164 File Offset: 0x00004364
			public CheckResponse(long localMtime, long serverMtime)
			{
				this.localMtime = localMtime;
				this.serverMtime = serverMtime;
				this.errorMessage = null;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x0000617B File Offset: 0x0000437B
			public CheckResponse(string errMsg)
			{
				this.serverMtime = 0L;
				this.localMtime = 0L;
				this.errorMessage = errMsg;
			}

			// Token: 0x040000A8 RID: 168
			public long serverMtime;

			// Token: 0x040000A9 RID: 169
			public long localMtime;

			// Token: 0x040000AA RID: 170
			public string errorMessage;
		}

		// Token: 0x02000022 RID: 34
		private class RequestUserData<T>
		{
			// Token: 0x040000AB RID: 171
			public string url;

			// Token: 0x040000AC RID: 172
			public Action<T> callback;
		}

		// Token: 0x02000023 RID: 35
		public struct DownloadResult
		{
			// Token: 0x06000109 RID: 265 RVA: 0x00006194 File Offset: 0x00004394
			public DownloadResult(Exception ex)
			{
				this.errorMessage = ex.Message;
			}

			// Token: 0x040000AD RID: 173
			public string errorMessage;
		}

		// Token: 0x02000024 RID: 36
		private struct apiFile
		{
			// Token: 0x040000AE RID: 174
			public string file_link;

			// Token: 0x040000AF RID: 175
			public string file_article_link;

			// Token: 0x040000B0 RID: 176
			public string file_version;

			// Token: 0x040000B1 RID: 177
			public long file_mtime;
		}
	}
}

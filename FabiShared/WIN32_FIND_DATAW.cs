using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace FabiShared
{
	// Token: 0x0200000E RID: 14
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WIN32_FIND_DATAW
	{
		// Token: 0x0400004A RID: 74
		public int dwFileAttributes;

		// Token: 0x0400004B RID: 75
		public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

		// Token: 0x0400004C RID: 76
		public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

		// Token: 0x0400004D RID: 77
		public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

		// Token: 0x0400004E RID: 78
		public int nFileSizeHigh;

		// Token: 0x0400004F RID: 79
		public int nFileSizeLow;

		// Token: 0x04000050 RID: 80
		public int dwReserved0;

		// Token: 0x04000051 RID: 81
		public int dwReserved1;

		// Token: 0x04000052 RID: 82
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string cFileName;

		// Token: 0x04000053 RID: 83
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
		public string cAlternateFileName;

		// Token: 0x04000054 RID: 84
		private const int MAX_PATH = 260;
	}
}

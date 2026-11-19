using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace FabiShared
{
	// Token: 0x0200000D RID: 13
	public struct WIN32_FIND_DATAA
	{
		// Token: 0x0400003F RID: 63
		public int dwFileAttributes;

		// Token: 0x04000040 RID: 64
		public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

		// Token: 0x04000041 RID: 65
		public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

		// Token: 0x04000042 RID: 66
		public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

		// Token: 0x04000043 RID: 67
		public int nFileSizeHigh;

		// Token: 0x04000044 RID: 68
		public int nFileSizeLow;

		// Token: 0x04000045 RID: 69
		public int dwReserved0;

		// Token: 0x04000046 RID: 70
		public int dwReserved1;

		// Token: 0x04000047 RID: 71
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string cFileName;

		// Token: 0x04000048 RID: 72
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
		public string cAlternateFileName;

		// Token: 0x04000049 RID: 73
		private const int MAX_PATH = 260;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
	// Token: 0x02000027 RID: 39
	public static class InputLib
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00009F28 File Offset: 0x00008128
		public static bool KeyWasPressed(Keys key)
		{
			short state = InputLib.GetAsyncKeyState(key);
			return state != 0;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009F40 File Offset: 0x00008140
		public static bool KeyIsPressed(Keys key)
		{
			short state = InputLib.GetAsyncKeyState(key);
			return state < 0;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009F58 File Offset: 0x00008158
		public static bool KeyIsPressed2(Keys key)
		{
			short state = InputLib.GetKeyState(key);
			return state < 0;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009F70 File Offset: 0x00008170
		public static bool KeysArePressed(Keys[] key)
		{
			if (key == null || key.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < key.Length; i++)
			{
				if (!InputLib.KeyIsPressed(key[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009FA4 File Offset: 0x000081A4
		public static Keys WaitForKeyPress()
		{
			int i;
			for (;;)
			{
				for (i = 0; i < InputLib.keyCodes.Length; i++)
				{
					if (InputLib.GetAsyncKeyState(InputLib.keyCodes[i]) == -32767)
					{
						goto Block_0;
					}
				}
				Thread.Sleep(10);
			}
			Block_0:
			return InputLib.keyCodes[i];
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009FE8 File Offset: 0x000081E8
		public static Keys KeyArrayToKeys(Keys[] keyArray)
		{
			Keys i = Keys.None;
			for (int j = 0; j < keyArray.Length; j++)
			{
				i |= keyArray[j];
			}
			return i;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A00C File Offset: 0x0000820C
		public static Keys[] KeysToKeyArray(Keys keys)
		{
			List<Keys> keyList = new List<Keys>();
			for (int i = 0; i < InputLib.keyCodes.Length; i++)
			{
				if ((keys & InputLib.keyCodes[i]) == InputLib.keyCodes[i])
				{
					keyList.Add(InputLib.keyCodes[i]);
				}
			}
			return keyList.ToArray();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A056 File Offset: 0x00008256
		public static void WaitForKeyPressAsync(InputLib.KeyPressedHandler pressed)
		{
			InputLib.WaitForKeyPressAsync(pressed, false);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A060 File Offset: 0x00008260
		public static Thread WaitForKeyPressAsync(InputLib.KeyPressedHandler pressed, bool continuous)
		{
			Thread t = new Thread(new ParameterizedThreadStart(InputLib.WaitForKeyPressAsyncRun));
			t.Priority = ThreadPriority.BelowNormal;
			t.Start(new object[]
			{
				pressed,
				continuous
			});
			return t;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A0A0 File Offset: 0x000082A0
		private static void WaitForKeyPressAsyncRun(object obj)
		{
			object[] objs = (object[])obj;
			InputLib.KeyPressedHandler pressed = (InputLib.KeyPressedHandler)objs[0];
			bool continuous = (bool)objs[1];
			do
			{
				Keys key = InputLib.WaitForKeyPress();
				pressed(key);
			}
			while (continuous);
		}

		// Token: 0x06000148 RID: 328
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint nInputs, InputLib.INPUT[] pInputs, int cbSize);

		// Token: 0x06000149 RID: 329
		[DllImport("user32.dll")]
		private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

		// Token: 0x0600014A RID: 330
		[DllImport("user32.dll")]
		public static extern short GetKeyState(Keys nVirtKey);

		// Token: 0x0600014B RID: 331
		[DllImport("user32.dll")]
		private static extern IntPtr GetMessageExtraInfo();

		// Token: 0x0600014C RID: 332
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(Keys vKey);

		// Token: 0x0600014D RID: 333
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);

		// Token: 0x040000EE RID: 238
		private static Keys[] keyCodes = (Keys[])Enum.GetValues(typeof(Keys));

		// Token: 0x040000EF RID: 239
		private const uint KEYEVENTF_EXTENDEDKEY = 1U;

		// Token: 0x040000F0 RID: 240
		private const uint KEYEVENTF_KEYUP = 2U;

		// Token: 0x040000F1 RID: 241
		private const uint KEYEVENTF_UNICODE = 4U;

		// Token: 0x040000F2 RID: 242
		private const uint KEYEVENTF_SCANCODE = 8U;

		// Token: 0x040000F3 RID: 243
		private const uint XBUTTON1 = 1U;

		// Token: 0x040000F4 RID: 244
		private const uint XBUTTON2 = 2U;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000150 RID: 336
		public delegate void KeyPressedHandler(Keys key);

		// Token: 0x02000029 RID: 41
		[Flags]
		public enum MouseEvents
		{
			// Token: 0x040000F6 RID: 246
			Move = 1,
			// Token: 0x040000F7 RID: 247
			LeftDown = 2,
			// Token: 0x040000F8 RID: 248
			LeftUp = 4,
			// Token: 0x040000F9 RID: 249
			RightDown = 8,
			// Token: 0x040000FA RID: 250
			RightUp = 16,
			// Token: 0x040000FB RID: 251
			MiddleDown = 32,
			// Token: 0x040000FC RID: 252
			MiddleUP = 64,
			// Token: 0x040000FD RID: 253
			XDown = 128,
			// Token: 0x040000FE RID: 254
			XUp = 256,
			// Token: 0x040000FF RID: 255
			Wheel = 2048,
			// Token: 0x04000100 RID: 256
			VirtualDesk = 16384,
			// Token: 0x04000101 RID: 257
			Absolute = 32768
		}

		// Token: 0x0200002A RID: 42
		public enum InputType
		{
			// Token: 0x04000103 RID: 259
			Mouse,
			// Token: 0x04000104 RID: 260
			Keyboard,
			// Token: 0x04000105 RID: 261
			Hardware
		}

		// Token: 0x0200002B RID: 43
		public struct MOUSEINPUT
		{
			// Token: 0x04000106 RID: 262
			public int dx;

			// Token: 0x04000107 RID: 263
			public int dy;

			// Token: 0x04000108 RID: 264
			public uint mouseData;

			// Token: 0x04000109 RID: 265
			public uint dwFlags;

			// Token: 0x0400010A RID: 266
			public uint time;

			// Token: 0x0400010B RID: 267
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200002C RID: 44
		public struct KEYBDINPUT
		{
			// Token: 0x0400010C RID: 268
			public ushort wVk;

			// Token: 0x0400010D RID: 269
			public ushort wScan;

			// Token: 0x0400010E RID: 270
			public uint dwFlags;

			// Token: 0x0400010F RID: 271
			public uint time;

			// Token: 0x04000110 RID: 272
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200002D RID: 45
		public struct HARDWAREINPUT
		{
			// Token: 0x04000111 RID: 273
			public uint uMsg;

			// Token: 0x04000112 RID: 274
			public ushort wParamL;

			// Token: 0x04000113 RID: 275
			public ushort wParamH;
		}

		// Token: 0x0200002E RID: 46
		[StructLayout(LayoutKind.Explicit)]
		public struct INPUT
		{
			// Token: 0x04000114 RID: 276
			[FieldOffset(0)]
			public int type;

			// Token: 0x04000115 RID: 277
			[FieldOffset(4)]
			public InputLib.MOUSEINPUT mi;

			// Token: 0x04000116 RID: 278
			[FieldOffset(4)]
			public InputLib.KEYBDINPUT ki;

			// Token: 0x04000117 RID: 279
			[FieldOffset(4)]
			public InputLib.HARDWAREINPUT hi;
		}
	}
}

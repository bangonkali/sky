using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Libs
{
	public class Desktop : IDisposable
	{
		#region DLLs
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("user32.dll")]
		private static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags, long dwDesiredAccess, IntPtr lpsa);

		[DllImport("user32.dll")]
		private static extern bool SwitchDesktop(IntPtr hDesktop);

		[DllImport("user32.dll", EntryPoint = "CloseDesktop", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseDesktop(IntPtr handle);

		[DllImport("user32.dll")]
		public static extern bool SetThreadDesktop(IntPtr hDesktop);

		[DllImport("user32.dll")]
		public static extern IntPtr GetThreadDesktop(int dwThreadId);

		[DllImport("kernel32.dll")]
		public static extern int GetCurrentThreadId();
		#endregion

		#region Enumeratoren
		[Flags]
		internal enum DESKTOP_ACCESS_MASK : uint
		{
			DESKTOP_NONE = 0,
			DESKTOP_READOBJECTS = 0x0001,
			DESKTOP_CREATEWINDOW = 0x0002,
			DESKTOP_CREATEMENU = 0x0004,
			DESKTOP_HOOKCONTROL = 0x0008,
			DESKTOP_JOURNALRECORD = 0x0010,
			DESKTOP_JOURNALPLAYBACK = 0x0020,
			DESKTOP_ENUMERATE = 0x0040,
			DESKTOP_WRITEOBJECTS = 0x0080,
			DESKTOP_SWITCHDESKTOP = 0x0100,

			GENERIC_ALL = (DESKTOP_READOBJECTS | DESKTOP_CREATEWINDOW | DESKTOP_CREATEMENU |
											DESKTOP_HOOKCONTROL | DESKTOP_JOURNALRECORD | DESKTOP_JOURNALPLAYBACK |
											DESKTOP_ENUMERATE | DESKTOP_WRITEOBJECTS | DESKTOP_SWITCHDESKTOP),
		}
		#endregion

		#region Dispose 
		public void Dispose()
		{
			SwitchToOrginal();
			((IDisposable)this).Dispose();
		}

		/// <summary>
		/// Unterklassen können hier die Funktionalität der Objektzerstörung erweitern. 
		/// </summary>
		/// <param name="fDisposing"></param>
		protected virtual void Dispose(bool fDisposing)
		{
			if (fDisposing)
			{
				// Hier die verwalteten Ressourcen freigeben
				//BspVariable1 = null;
				CloseDesktop(DesktopPtr);
			}
			// Hier die unverwalteten Ressourcen freigeben
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); //Fordert das System auf, den Finalizer für das angegebenen Objekt nicht aufzurufen
		}
		#endregion

		#region Variablen
		IntPtr _hOrigDesktop;
		public IntPtr DesktopPtr;
		private string _sMyDesk;
		public string DesktopName
		{
			get
			{
				return (_sMyDesk);
			}
			set
			{
				_sMyDesk = value;
			}
		}
		#endregion

		#region Konstruktoren
		public Desktop()
		{
			_sMyDesk = "";
		}

		public Desktop(string sDesktopName)
		{
			_hOrigDesktop = GetCurrentDesktopPtr();
			_sMyDesk = sDesktopName;
			DesktopPtr = CreateMyDesktop();
		}
		#endregion

		#region Methoden
		public void show()
		{
			SetThreadDesktop(DesktopPtr);
			SwitchDesktop(DesktopPtr);
		}

		public void SwitchToOrginal()
		{
			SwitchDesktop(_hOrigDesktop);
			SetThreadDesktop(_hOrigDesktop);
		}

		private IntPtr CreateMyDesktop()
		{
			return CreateDesktop(_sMyDesk, IntPtr.Zero, IntPtr.Zero, 0, (long)DESKTOP_ACCESS_MASK.GENERIC_ALL, IntPtr.Zero);
		}

		public IntPtr GetCurrentDesktopPtr()
		{
			return GetThreadDesktop(GetCurrentThreadId());
		}
		#endregion

	}
}

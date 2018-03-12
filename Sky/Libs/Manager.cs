using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sky.Libs
{
	class Manager
	{
		private Template _template;

		public Manager(Template t)
		{
			_template = t;
		}

		private bool IsProcess(TemplateItem item, Process p, List<IntPtr> collected)
		{
			var q = true;
			q &= p.MainWindowHandle != IntPtr.Zero;
			if (!q) return false;
			
			q &= p.MainWindowHandle != null;
			if (!q) return false;

			switch (item.NameType)
			{
				case NameType.WindowName:
					q &= p.MainWindowTitle.ToLower().Contains(item.Name.ToLower());
					if (!q) return false;
					break;
				case NameType.ProcessName:
					q &= p.ProcessName.ToLower() == item.Name.ToLower();
					if (!q) return false;
					break;
				case NameType.ProgramPath:
					q &= p.StartInfo.FileName.ToLower().Contains(item.Name.ToLower());
					if (!q) return false;
					break;
			}

			q &= collected == null ? true : !collected.Contains(p.MainWindowHandle);
			if (!q) return false;

			return true;
		}

		private Rectangle GetBoundsFromScreen(Screen screen, TemplateItem item)
		{
			var width = Screen.PrimaryScreen.Bounds.Width;
			var height = Screen.PrimaryScreen.Bounds.Height;
			var top = Screen.PrimaryScreen.Bounds.Top;
			var left = Screen.PrimaryScreen.Bounds.Left;

			var itemRealHeight = (int)Math.Ceiling(height * (item.Height / item.HeightGrid) - item.Padding.Bottom);
			var itemRealWidth = (int)Math.Ceiling((width * (item.Width / item.WidthGrid)) - item.Padding.Right);
			var itemRealTop = (int)Math.Floor(top + (height * (item.Top / item.HeightGrid)) + item.Padding.Top);
			var itemRealLeft = (int)Math.Floor(left + (width * (item.Left / item.WidthGrid)) + item.Padding.Left);

			return new Rectangle(itemRealLeft, itemRealTop, itemRealWidth, itemRealHeight);
		}

		private Screen GetDisplay(string d)
		{
			var display = Screen.AllScreens.Where(x => x.DeviceName == d).FirstOrDefault();
			return display == null ? Screen.PrimaryScreen : display;
		}

		public void Run()
		{
			var collected = new List<IntPtr>();

			foreach (var item in _template.Items)
			{
				if (string.IsNullOrEmpty(item.DesktopGroup))
				{
					var screen = GetDisplay(item.Display);
					var bounds = GetBoundsFromScreen(screen, item);

					var proc = Process.GetProcesses()
						.Where(p => IsProcess(item, p, collected))
						.FirstOrDefault();

					var g = Process.GetProcesses();
					foreach (var h in g)
					{
						Debug.WriteLine(h.ProcessName);
					}

					if (proc == null)
						break;

					var hwnd = proc.MainWindowHandle;

					if (hwnd != IntPtr.Zero && hwnd != IntPtr.Zero)
					{
						Debug.WriteLine($"${proc.ProcessName} {proc.Id} {hwnd}, {bounds.X}, {bounds.Top}, {bounds.Width}, {bounds.Height}");
						Desktop.MoveWindow(hwnd, bounds.X, bounds.Top, bounds.Width, bounds.Height, true);
						collected.Add(hwnd);
						Thread.Sleep(200);
					}

					#region Deprecated
					//if (item.NameType == NameType.ProgramPath)
					//{
					//	if (!File.Exists(item.Name))
					//	{
					//		Debug.WriteLine($"process path not found {item.Name}");
					//		break;
					//	}

					//	var p = new Process();
					//	p.StartInfo = new ProcessStartInfo();
					//	p.StartInfo.FileName = item.Name;
					//	p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
					//	p.Start();

					//	DateTime start = DateTime.Now;
					//	bool handleFound = false;
					//	Thread.Sleep(MajorStep);
					//	while ((DateTime.Now - start).TotalMilliseconds < MajorDuration)
					//	{
					//		if (p.MainWindowHandle != IntPtr.Zero)
					//		{
					//			handleFound = true;
					//			break;
					//		}
					//		Thread.Sleep(MajorStep);
					//	}

					//	// Enumerate all windows and get the process id.
					//	IntPtr useInstead = IntPtr.Zero;
					//	if (p.MainWindowHandle == IntPtr.Zero)
					//	{
					//		foreach (Process ps in Process.GetProcesses().Where(x => x.ProcessName == p.ProcessName))
					//		{
					//			if (ps.MainWindowTitle.Length > 0)
					//			{
					//				uint pid;
					//				DateTime bigger, smaller;
					//				if (ps.StartTime > p.StartTime)
					//				{
					//					bigger = ps.StartTime;
					//					smaller = p.StartTime;
					//				}
					//				else
					//				{
					//					bigger = p.StartTime;
					//					smaller = ps.StartTime;
					//				}
					//				var duration = (bigger - smaller).TotalMilliseconds;
					//				if (duration < MajorDuration)
					//				{
					//					useInstead = ps.MainWindowHandle;
					//					handleFound = true;
					//					Thread.Sleep(MajorDuration - (int)duration);
					//					break;
					//				}
					//			}
					//		}
					//	}
					//	else
					//	{
					//		useInstead = p.MainWindowHandle;
					//	}

					//	if (handleFound)
					//		Desktop.MoveWindow(useInstead, itemRealLeft, itemRealTop, itemRealWidth, itemRealHeight, true);
					//	else
					//		Debug.WriteLine($"handle not found {item.Name}");
					//}

					//Debug.WriteLine(""); 
					#endregion
				}
			}
		}
	}
}

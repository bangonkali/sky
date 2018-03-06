using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky
{
	public class Manager
	{
		private Template _template;

		public Manager()
		{

		}

		public void Run()
		{
			const int MajorDuration = 5000;
			const int MajorStep = 200;

			foreach (var item in _template.Items)
			{
				// run on main desktop
				if (string.IsNullOrEmpty(item.DesktopGroup))
				{
					var width = Screen.PrimaryScreen.Bounds.Width;
					var height = Screen.PrimaryScreen.Bounds.Height;
					var top = Screen.PrimaryScreen.Bounds.Top;
					var left = Screen.PrimaryScreen.Bounds.Left;

					var itemRealHeight = (int)Math.Ceiling(height * (item.Height / item.HeightGrid));
					var itemRealWidth = (int)Math.Ceiling((width * (item.Width / item.WidthGrid)));
					var itemRealTop = (int)Math.Floor(top + (height * (item.Top / item.HeightGrid)));
					var itemRealLeft = (int)Math.Floor(left + (width * (item.Left / item.WidthGrid)));

					if (item.NameType == NameType.ProgramPath)
					{
						if (!File.Exists(item.Name))
						{
							Debug.WriteLine($"process path not found {item.Name}");
							break;
						}

						var p = new Process();
						p.StartInfo = new ProcessStartInfo();
						p.StartInfo.FileName = item.Name;
						p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
						p.Start();

						DateTime start = DateTime.Now;
						bool handleFound = false;
						Thread.Sleep(MajorStep);
						while ((DateTime.Now - start).TotalMilliseconds < MajorDuration)
						{
							if (p.MainWindowHandle != IntPtr.Zero)
							{
								handleFound = true;
								break;
							}
							Thread.Sleep(MajorStep);
						}

						// Enumerate all windows and get the process id.
						IntPtr useInstead = IntPtr.Zero;
						if (p.MainWindowHandle == IntPtr.Zero)
						{
							foreach (Process ps in Process.GetProcesses().Where(x => x.ProcessName == p.ProcessName))
							{
								if (ps.MainWindowTitle.Length > 0)
								{
									uint pid;
									DateTime bigger, smaller;
									if (ps.StartTime > p.StartTime)
									{
										bigger = ps.StartTime;
										smaller = p.StartTime;
									}
									else
									{
										bigger = p.StartTime;
										smaller = ps.StartTime;
									}
									var duration = (bigger - smaller).TotalMilliseconds;
									if (duration < MajorDuration)
									{
										useInstead = ps.MainWindowHandle;
										handleFound = true;
										Thread.Sleep(MajorDuration - (int)duration);
										break;
									}
								}
							}
						}
						else
						{
							useInstead = p.MainWindowHandle;
						}

						if (handleFound)
							Desktop.MoveWindow(useInstead, itemRealLeft, itemRealTop, itemRealWidth, itemRealHeight, true);
						else
							Debug.WriteLine($"handle not found {item.Name}");
					}

					Debug.WriteLine("");
				}
			}
		}
	}
}

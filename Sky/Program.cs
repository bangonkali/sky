using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;
using Newtonsoft.Json;
using Sky.Libs;

namespace Sky
{
	class Program
	{
		private static string _configPath;
		private static Options _options;

		static void Main(string[] args)
		{
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
				.WithNotParsed<Options>((errs) => HandleParseError(errs));
		}

		private static void SaveConfiguration(string path, Template template)
		{
			using (StreamWriter sw = new StreamWriter(path))
			{
				sw.Write(JsonConvert.SerializeObject(template, Formatting.Indented));
			}
		}

		private static Template CreateSampleConfiguration()
		{
			Libs.Padding defaultPadding = new Libs.Padding(-14, -14, -8, -8);
			return new Template()
			{
				Items = new List<TemplateItem>() {
					new TemplateItem(3,3,1,1,0,0,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },
					new TemplateItem(3,3,1,1,1,0,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },
					new TemplateItem(3,3,1,1,2,0,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },

					new TemplateItem(3,3,1,1,0,1,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },
					new TemplateItem(3,3,1,1,1,1,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },
					new TemplateItem(3,3,1,1,2,1,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },

					new TemplateItem(3,3,1,1,0,2,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },
					new TemplateItem(3,3,1,1,1,2,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },
					new TemplateItem(3,3,1,1,2,2,"notepad","",NameType.ProcessName) { Display = Screen.PrimaryScreen.DeviceName, Padding = defaultPadding },

				}
			};
		}

		private static void Log(Options opts, string message)
		{
			if (opts.Verbose)
				Console.WriteLine(message);
		}

		private static void RunOptionsAndReturnExitCode(Options opts)
		{
			_options = opts;
			_configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".sky");
			Log(opts, $"Default configuration path at {_configPath}");

			if (opts.Screens)
			{
				var primary = "Primary Display";
				var secondary = "";

				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(string.Format("{0, -10} ", "Left"));
				Console.Write(string.Format("{0, -10} ", "Top"));
				Console.Write(string.Format("{0, -10} ", "Width"));
				Console.Write(string.Format("{0, -10} ", "Height"));
				Console.Write(string.Format("{0, -16} ", "Display"));
				Console.Write(string.Format("{0, -10}\n", "?"));
				Console.ResetColor();
				foreach (var screen in Screen.AllScreens)
				{
					var isPrimary = screen.Primary ? primary : secondary;
					Console.Write(string.Format("{0, -10} ", screen.Bounds.Left.ToString()));
					Console.Write(string.Format("{0, -10} ", screen.Bounds.Top.ToString()));
					Console.Write(string.Format("{0, -10} ", screen.Bounds.Width.ToString()));
					Console.Write(string.Format("{0, -10} ", screen.Bounds.Height.ToString()));
					Console.Write(string.Format("{0, -16} ", screen.DeviceName.ToString()));
					if (screen.Primary) Console.ForegroundColor = ConsoleColor.Blue;
					Console.Write(string.Format("{0, -10}\n", isPrimary));
					Console.ResetColor();
				}
				return;
			}

			if (opts.Windows)
			{
				var ps = Process.GetProcesses()
					.Where(p => p.MainWindowHandle != IntPtr.Zero)
					.Where(p => p.MainWindowTitle.Trim().Length > 0);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(string.Format("{0, -30} ", "Process Name"));
				Console.Write(string.Format("{0, -60}\n", "Window Title"));
				Console.ResetColor();
				foreach (var p in ps)
				{
					Console.Write(string.Format("{0, -30} ", p.ProcessName));
					Console.Write(string.Format("{0, -60}\n", p.MainWindowTitle));
				}
				return;
			}

			if (opts.CreateSampleConfig && !File.Exists(opts.ConfigFile) && !string.IsNullOrEmpty(opts.ConfigFile))
			{
				_configPath = opts.ConfigFile;
				SaveConfiguration(_configPath, CreateSampleConfiguration());
				Log(opts, $"Written sample configuration at {_configPath}");
				return;
			}

			if (opts.CreateSampleConfig && !File.Exists(_configPath))
			{
				SaveConfiguration(_configPath, CreateSampleConfiguration());
				Log(opts, $"Written sample configuration at {_configPath}");
				return;
			}

			if (!opts.CreateSampleConfig)
			{
				_configPath = File.Exists(opts.ConfigFile) ? opts.ConfigFile : _configPath;
				ParseAndDo(_configPath);
				return;
			}
		}

		private static void HandleParseError(IEnumerable<Error> errors)
		{
			foreach (var item in errors)
			{
				Console.Error.WriteLine(item.ToString());
			}
		}

		private static void ParseAndDo(string path)
		{
			using (StreamReader sr = new StreamReader(path, true))
			{
				var config = JsonConvert.DeserializeObject<Template>(sr.ReadToEnd());
				var manager = new Manager(config);
				manager.Run();
			}
			return;
		}
	}
}

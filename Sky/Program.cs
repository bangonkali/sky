using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			return new Template()
			{
				Items = new List<TemplateItem>() {

					new TemplateItem(3,3,1,1,0,0,"notepad","",NameType.ProcessName),
					new TemplateItem(3,3,1,1,1,0,"notepad","",NameType.ProcessName),
					new TemplateItem(3,3,1,1,2,0,"notepad","",NameType.ProcessName),

					new TemplateItem(3,3,1,1,0,1,"notepad","",NameType.ProcessName),
					new TemplateItem(3,3,1,1,1,1,"notepad","",NameType.ProcessName),
					new TemplateItem(3,3,1,1,2,1,"notepad","",NameType.ProcessName),

					new TemplateItem(3,3,1,1,0,2,"notepad","",NameType.ProcessName),
					new TemplateItem(3,3,1,1,1,2,"notepad","",NameType.ProcessName),
					new TemplateItem(3,3,1,1,2,2,"notepad","",NameType.ProcessName),

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

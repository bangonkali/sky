﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky
{
	public class Options
	{
		[Option(Default = false, HelpText = "Prints all messages to standard output.")]
		public bool Verbose { get; set; }

		[Option(Default = false, HelpText = "Create sample configuration files on config path if not exist.")]
		public bool CreateSampleConfig { get; set; }

		[Option(Default = false, HelpText = "Lists all the screens/displays.")]
		public bool Screens { get; set; }

		[Option(Default = false, HelpText = "Lists the process name of all the currently opened windows.")]
		public bool Windows { get; set; }

		[Option('c', "config", Required = false, HelpText = "Configuration file.")]
		public string ConfigFile { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Libs
{
	[Serializable]
	public class Template
	{
		public List<TemplateItem> Items { get; set; }

		public Template()
		{
			Items = new List<TemplateItem>();
		}
	}
}

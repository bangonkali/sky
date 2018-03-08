using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Libs
{
	[Serializable]
	public class TemplateItem
	{
		public TemplateItem()
		{
			NameType = NameType.ProcessName;
		}

		public TemplateItem(double widthGrid, double heightGrid, double width, double height, double top, double left, string name, string desktopGroup, NameType nameType)
		{
			WidthGrid = widthGrid;
			HeightGrid = heightGrid;
			Width = width;
			Height = height;
			Top = top;
			Left = left;
			Name = name;
			DesktopGroup = desktopGroup;
			NameType = nameType;
		}

		public double WidthGrid { get; set; }

		public double HeightGrid { get; set; }

		public double Width { get; set; }

		public double Height { get; set; }

		public double Top { get; set; }

		public double Left { get; set; }

		public string Name { get; set; }

		public string DesktopGroup { get; set; }

		public NameType NameType { get; set; }

		public string Display { get; set; }
	}

	[Serializable]
	public enum NameType
	{
		WindowName,
		ProcessName,
		ProgramPath
	}
}

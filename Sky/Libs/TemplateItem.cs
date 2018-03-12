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

		public Padding Padding { get; set; }

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

	[Serializable]
	public class Padding
	{
		public int Left { get; set; }
		public int Right { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }

		public Padding()
		{
			Left = Right = Top = Bottom = 0;
		}

		public Padding(int padding)
		{
			Left = Right = Top = Bottom = padding;
		}

		public Padding(int paddingLeft, int paddingRight, int paddingTop, int paddingBottom)
		{
			Left = paddingLeft;
			Right = paddingRight;
			Top = paddingTop;
			Bottom = paddingBottom;
		}
	}
}

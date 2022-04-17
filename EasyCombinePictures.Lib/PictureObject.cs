using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasyCombinePictures
{
	public class PictureObject
	{
		[DefaultValue(null)]
		public string path { get; set; }
		[DefaultValue(null)]
		public string text { get; set; }
	}
}

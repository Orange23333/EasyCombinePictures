using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

using YamlDotNet.Serialization;

namespace EasyCombinePictures
{
	public class PictureConfigObject
	{
		[DefaultValue(null)]
		public string pic_arrangement { get; set; }

		[DefaultValue(null)]
		public string set_callback_file { get; set; }

		[YamlIgnore]
		public LineOrRowAmount PictureArrangement
		{
			get
			{
				return LineOrRowAmount.Parse(pic_arrangement);
			}
			set
			{
				pic_arrangement = value.ToString();
			}
		}

		[YamlIgnore]
		public bool SetCallbackFile
		{
			get
			{
				return bool.Parse(set_callback_file);
			}
			set
			{
				set_callback_file = value.ToString();
			}
		}
	}
}

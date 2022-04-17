using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

using YamlDotNet.Serialization;

namespace EasyCombinePictures
{
	public class PictureConfig
	{
		[DefaultValue(null)]
		public PictureConfigObject pic_config { get; set; }
		[DefaultValue(null)]
		public PictureObject[] pics { get; set; }

		public static PictureConfig Load(string path)
		{
			string text = File.ReadAllText(path, Encoding.UTF8);

			IDeserializer deserializer = new DeserializerBuilder().Build();
			return deserializer.Deserialize<PictureConfig>(text);
		}
	}
}

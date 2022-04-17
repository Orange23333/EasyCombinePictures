using System;
using System.Drawing;
using System.IO;

namespace EasyCombinePictures
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Command format: EasyCombinePicture <Picture config path> <Work ID>.");
				Environment.Exit(-1);
			}

			try
			{
				PictureConfig pictureConfig = PictureConfig.Load(args[0]);

				Bitmap outputBitmap = null;
				try
				{
					outputBitmap = Make.CombinePictures(pictureConfig);
					outputBitmap.Save(args[1]+".bmp");
					outputBitmap.Dispose();
					outputBitmap = null;
				}
				catch (Exception ex)
				{
					outputBitmap?.Dispose();
					if (pictureConfig.pic_config.SetCallbackFile)
					{
						WriteCallbackFile(args[1], "-1\n" + ex.ToString());
					}
					Console.WriteLine(ex.ToString());
					Environment.Exit(-1);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Environment.Exit(-1);
			}

			WriteCallbackFile(args[1], "0\n");
			Environment.Exit(0);
		}

		static void WriteCallbackFile(string workId, string text)
		{
			File.WriteAllText(workId + ".out", text, System.Text.Encoding.UTF8);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EasyCombinePictures
{
	public class Make
	{
		public static Bitmap CombinePictures(PictureConfig config)
		{
			return CombinePictures(config.pic_config, config.pics);
		}

		public static Bitmap CombinePictures(PictureConfigObject config, PictureObject[] pictures)
		{
			//加载图片
			int i;
			Image[] images = new Image[pictures.Length];
			for (i = 0; i < pictures.Length; i++)
			{
				images[i] = Image.FromFile(pictures[i].path);
			}

			LineOrRowAmount pictureArrangement = config.PictureArrangement;
			if (pictureArrangement.LineOrRow == LineOrRow.Default)
			{
				pictureArrangement = new LineOrRowAmount()
				{
					LineOrRow = LineOrRow.Row,
					Amount = 1
				};
			}
			//int l = (pictures.Length + pictureArrangement.Amount - 1) / pictureArrangement.Amount; //算另一边单位长度
			if (pictureArrangement.Amount != 1)
			{
				throw new NotSupportedException("It doesn't support multi-line now.");
			}

			//计算图片大小
			Size size = new Size(0, 0);
			switch (pictureArrangement.LineOrRow)
			{
				case LineOrRow.Line:
					foreach(Image image in images)
					{
						if (image.Height > size.Height)
						{
							size.Height = image.Height;
						}
						size.Width += image.Width;
					}
					break;
				case LineOrRow.Row:
					foreach (Image image in images)
					{
						if (image.Width > size.Width)
						{
							size.Width = image.Width;
						}
						size.Height += image.Height;
					}
					break;
				default:
					throw new Exception("Unknown error.");
			}

			Bitmap canvasBitmap = new Bitmap(size.Width, size.Height);
			Graphics canvas = Graphics.FromImage(canvasBitmap);

			Point point = new Point(0, 0);
			switch (pictureArrangement.LineOrRow)
			{
				case LineOrRow.Line:
					for (i = 0; i < pictures.Length; i++)
					{
						Rectangle dstRectangle = new Rectangle(point, images[i].Size);
						Rectangle srcRectangle = new Rectangle(Point.Empty, images[i].Size);
						canvas.DrawImage(images[i], dstRectangle, srcRectangle, GraphicsUnit.Pixel);
						if (pictures[i].text != null)
						{
							AddAutosizeText(canvas, AutoSizeTextBoxOnImage(dstRectangle), pictures[i].text);
						}
						point.X += images[i].Width;
					}
					break;
				case LineOrRow.Row:
					for (i = 0; i < pictures.Length; i++)
					{
						Rectangle dstRectangle = new Rectangle(point, images[i].Size);
						Rectangle srcRectangle = new Rectangle(Point.Empty, images[i].Size);
						canvas.DrawImage(images[i], dstRectangle, srcRectangle, GraphicsUnit.Pixel);
						if (pictures[i].text != null)
						{
							AddAutosizeText(canvas, AutoSizeTextBoxOnImage(dstRectangle), pictures[i].text);
						}
						point.Y += images[i].Height;
					}
					break;
				default:
					throw new Exception("Unknown error.");
			}

			canvas.Flush();
			foreach (Image image in images)
			{
				image.Dispose();
			}
			canvas.Dispose();
			return canvasBitmap;
		}

		public static readonly float AutoSizeTextBoxOnImageScale = 1F;
		private static Rectangle AutoSizeTextBoxOnImage(Rectangle imageRectangle)
		{
			SizeF newSize = new SizeF(imageRectangle.Width * AutoSizeTextBoxOnImageScale, imageRectangle.Height * AutoSizeTextBoxOnImageScale);
			PointF newPoint = new PointF(imageRectangle.X + imageRectangle.Width * (1 - AutoSizeTextBoxOnImageScale), imageRectangle.Y + imageRectangle.Height * (1 - AutoSizeTextBoxOnImageScale));
			Rectangle r = new Rectangle(Point.Round(newPoint), Size.Round(newSize));
			return r;
		}
		//以下添加自适应文字的方法遵循CC协议，请注明出处来自：https://www.cnblogs.com/dandelion-drq/p/csharp_use_gdiplus_to_add_text.html 。
		private static void AddAutosizeText(Graphics graphics, Rectangle rectangle, string text)
		{
			FontFamily ff = FontFamily.GenericSansSerif;
			float fontSize = rectangle.Height;
			Font font = new Font(ff, fontSize, GraphicsUnit.Pixel);

			SizeF sf = graphics.MeasureString(text, font);
			// 调整字体大小以适应文字区域
			while (sf.Width < rectangle.Width)
			{
				fontSize += 0.1f;
				font = new Font(ff, fontSize, GraphicsUnit.Pixel);
				sf = graphics.MeasureString(text, font);
			}
			while (sf.Width > rectangle.Width)
			{
				fontSize -= 0.1f;
				font = new Font(ff, fontSize, GraphicsUnit.Pixel);
				sf = graphics.MeasureString(text, font);
			}

			rectangle = new Rectangle((int)(rectangle.X + (rectangle.Width - sf.Width) / 2), (int)(rectangle.Y + (rectangle.Height - sf.Height) / 2), rectangle.Width, rectangle.Height);
			graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(30, 255, 255, 255)), new Rectangle(rectangle.X + 5, rectangle.Y, rectangle.Width, rectangle.Height));
			graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(30, 255, 255, 255)), new Rectangle(rectangle.X - 5, rectangle.Y, rectangle.Width, rectangle.Height));
			graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(30, 255, 255, 255)), new Rectangle(rectangle.X, rectangle.Y + 5, rectangle.Width, rectangle.Height));
			graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(30, 255, 255, 255)), new Rectangle(rectangle.X, rectangle.Y - 5, rectangle.Width, rectangle.Height));
			graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(80, 0, 0, 0)), rectangle);
		}
	}
}

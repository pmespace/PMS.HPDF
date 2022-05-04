/*
 * << Haru Free PDF Library 2.0.5 >> -- ImageDemo.cs
 *
 * Copyright (c) 1999-2006 Takeshi Kanno <takeshi_kanno@est.hi-ho.ne.jp>
 *
 * Permission to use, copy, modify, distribute and sell this software
 * and its documentation for any purpose is hereby granted without fee,
 * provided that the above copyright notice appear in all copies and
 * that both that copyright notice and this permission notice appear
 * in supporting documentation.
 * It is provided "as is" without express or implied warranty.
 *
 */

using System;
using System.Diagnostics;
using System.IO;
using HPDF;

namespace HPDF.DEMO
{
	public class Image
	{
		private static void ShowDescription(HPDFPage page, float x, float y,
				  string text)
		{
			string buf;

			page.MoveTo(new HPDFPointStruct() { x = x, y = y - 10 });
			page.LineTo(new HPDFPointStruct() { x = x, y = y + 10 });
			page.MoveTo(new HPDFPointStruct() { x = x - 10, y = y });
			page.LineTo(new HPDFPointStruct() { x = x + 10, y = y });
			page.Stroke();

			page.SetFont(page.GetCurrentFont(), 8);
			page.SetRGBFill(new HPDFRGBColorStruct() { R = 0, G = 0, B = 0 });

			page.BeginText();

			buf = "(x=" + x + ",y=" + y + ")";
			page.MoveTextPos(new HPDFPointStruct() { x = x - page.GetTextWidth(buf) - 5, y = y - 10 });
			page.ShowText(buf);
			page.EndText();

			page.BeginText();
			page.MoveTextPos(new HPDFPointStruct() { x = x - 20, y = y - 25 });
			page.ShowText(text);
			page.EndText();
		}

		public static void Demo(FLibhpdf main)
		{
			try
			{
				HPDFDocument pdf = new HPDFDocument();

				pdf.SetCompressionMode(HPDFCompressionModes.All);

				/* create default-font */
				HPDFFont font = pdf.GetFont("Helvetica", null);

				/* add a new page object. */
				HPDFPage page = pdf.AddPage();

				page.SetWidth(550);
				page.SetHeight(500);

				HPDFDestination dst = page.CreateDestination();
				dst.SetXYZ(0, page.GetHeight(), 1);
				pdf.SetOpenAction(dst);

				page.BeginText();
				page.SetFont(font, 20);
				page.MoveTextPos(new HPDFPointStruct() { x = 220, y = page.GetHeight() - 70 });
				page.ShowText("ImageDemo");
				page.EndText();

				/* load image file. */
				HPDFImage image = pdf.LoadPNGImageFromFile(@".\basn3p02.png");

				/* image1 is masked by image2. */
				HPDFImage image1 = pdf.LoadPNGImageFromFile(@".\basn3p02.png");

				/* image2 is a mask image. */
				HPDFImage image2 = pdf.LoadPNGImageFromFile(@".\basn0g01.png");

				/* image3 is a RGB-color image. we use this image for color-mask
             * demo.
             */
				HPDFImage image3 = pdf.LoadPNGImageFromFile(@".\maskimage.png");

				float iw = image.GetWidth();
				float ih = image.GetHeight();

				page.SetLineWidth(0.5f);

				float x = 100;
				float y = page.GetHeight() - 150;

				/* Draw image to the canvas. (normal-mode with actual size.)*/
				page.DrawImage(image, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = iw, Height = ih });

				ShowDescription(page, x, y, "Actual Size");

				x += 150;

				/* Scalling image (X direction) */
				page.DrawImage(image, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = iw * 1.5f, Height = ih });

				ShowDescription(page, x, y, "Scalling image (X direction)");

				x += 150;

				/* Scalling image (Y direction). */
				page.DrawImage(image, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = iw, Height = ih * 1.5f });
				ShowDescription(page, x, y, "Scalling image (Y direction)");

				x = 100;
				y -= 120;

				/* Skewing image. */
				float angle1 = 10;
				float angle2 = 20;
				float rad1 = angle1 / 180 * 3.141592f;
				float rad2 = angle2 / 180 * 3.141592f;

				page.GSave();

				page.Concat(iw, (float)Math.Tan(rad1) * iw, (float)Math.Tan(rad2) * ih,
						  ih, x, y);

				page.ExecuteXObject(image);
				page.GRestore();

				ShowDescription(page, x, y, "Skewing image");

				x += 150;

				/* Rotating image */
				float angle = 30;     /* rotation of 30 degrees. */
				float rad = angle / 180 * 3.141592f; /* Calcurate the radian value. */

				page.GSave();

				page.Concat((float)(iw * Math.Cos(rad)),
					 (float)(iw * Math.Sin(rad)),
					 (float)(ih * -Math.Sin(rad)),
					 (float)(ih * Math.Cos(rad)),
					 x, y);

				page.ExecuteXObject(image);
				page.GRestore();

				ShowDescription(page, x, y, "Rotating image");

				x += 150;

				/* draw masked image. */

				/* Set image2 to the mask image of image1 */
				image1.SetMaskImage(image2);

				page.SetRGBFill(new HPDFRGBColorStruct() { R = 0, G = 0, B = 0 });
				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = x - 6, y = y + 14 });
				page.ShowText("MASKMASK");
				page.EndText();

				page.DrawImage(image1, new HPDFPointStruct() { x = x - 3, y = y - 3 }, new HPDFSizeStruct() { Width = iw + 6, Height = ih + 6 });

				ShowDescription(page, x, y, "masked image");

				x = 100;
				y -= 120;

				/* color mask. */
				page.SetRGBFill(new HPDFRGBColorStruct() { R = 0, G = 0, B = 0 });
				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = x - 6, y = y + 14 });
				page.ShowText("MASKMASK");
				page.EndText();

				image3.SetColorMask(0, 255, 0, 0, 0, 255);
				page.DrawImage(image3, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = iw, Height = ih });

				ShowDescription(page, x, y, "Color Mask");

				string s = "ImageDemo.pdf";
				main.ADD($"ready to save: {s}");
				Program.SaveAndStart(pdf, s);
			}
			catch (Exception e)
			{
				main.ADD(e.Message);
				Console.Error.WriteLine(e.Message);
			}
		}
	}
}
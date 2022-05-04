/*
 * << Haru Free PDF Library 2.0.5 >> -- RawImageDemo.cs
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
	public class RawImage
	{

		public static void Demo(FLibhpdf main)
		{
			byte[] raw_image_data = new byte[] {
				0xff, 0xff, 0xff, 0xfe, 0xff, 0xff, 0xff, 0xfc,
				0xff, 0xff, 0xff, 0xf8, 0xff, 0xff, 0xff, 0xf0,
				0xf3, 0xf3, 0xff, 0xe0, 0xf3, 0xf3, 0xff, 0xc0,
				0xf3, 0xf3, 0xff, 0x80, 0xf3, 0x33, 0xff, 0x00,
				0xf3, 0x33, 0xfe, 0x00, 0xf3, 0x33, 0xfc, 0x00,
				0xf8, 0x07, 0xf8, 0x00, 0xf8, 0x07, 0xf0, 0x00,
				0xfc, 0xcf, 0xe0, 0x00, 0xfc, 0xcf, 0xc0, 0x00,
				0xff, 0xff, 0x80, 0x00, 0xff, 0xff, 0x00, 0x00,
				0xff, 0xfe, 0x00, 0x00, 0xff, 0xfc, 0x00, 0x00,
				0xff, 0xf8, 0x0f, 0xe0, 0xff, 0xf0, 0x0f, 0xe0,
				0xff, 0xe0, 0x0c, 0x30, 0xff, 0xc0, 0x0c, 0x30,
				0xff, 0x80, 0x0f, 0xe0, 0xff, 0x00, 0x0f, 0xe0,
				0xfe, 0x00, 0x0c, 0x30, 0xfc, 0x00, 0x0c, 0x30,
				0xf8, 0x00, 0x0f, 0xe0, 0xf0, 0x00, 0x0f, 0xe0,
				0xe0, 0x00, 0x00, 0x00, 0xc0, 0x00, 0x00, 0x00,
				0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
		  };

			try
			{
				HPDFDocument pdf = new HPDFDocument();

				pdf.SetCompressionMode(HPDFCompressionModes.All);

				/* create default-font */
				HPDFFont font = pdf.GetFont("Helvetica", null);

				/* add a new page object. */
				HPDFPage page = pdf.AddPage();

				page.SetWidth(172);
				page.SetHeight(80);

				page.BeginText();
				page.SetFont(font, 20);
				page.MoveTextPos(new HPDFPointStruct() { x = 220, y = page.GetHeight() - 70 });
				page.ShowText("RawImageDemo");
				page.EndText();

				/* load RGB raw-image file. */
				HPDFImage image = pdf.LoadRawImageFromFile("32_32_rgb.dat", 32, 32, HPDFColorSpaces.DeviceRGB);

				float x = 20;
				float y = 20;

				/* Draw image to the canvas. (normal-mode with actual size.)*/
				page.DrawImage(image, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = 32, Height = 32 });

				/* load GrayScale raw-image file. */
				image = pdf.LoadRawImageFromFile("32_32_gray.dat", 32, 32, HPDFColorSpaces.DeviceGray);

				x = 70;
				y = 20;

				/* Draw image to the canvas. (normal-mode with actual size.)*/
				page.DrawImage(image, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = 32, Height = 32 });

				/* load GrayScale raw-image (1bit) file from memory. */
				image = pdf.LoadRawImageFromMem(raw_image_data, 32, 32, HPDFColorSpaces.DeviceGray, 1);

				x = 120;
				y = 20;

				/* Draw image to the canvas. (normal-mode with actual size.)*/
				page.DrawImage(image, new HPDFPointStruct() { x = x, y = y }, new HPDFSizeStruct() { Width = 32, Height = 32 });

				string s = "RawImageDemo.pdf";
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
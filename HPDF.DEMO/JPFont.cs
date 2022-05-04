/*
 * << Haru Free PDF Library 2.0.5 >> -- JPFontDemo.cs
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
using System.IO;
using HPDF;

namespace HPDF.DEMO
{
	public class JPFont
	{
		public static void Demo(FLibhpdf main)
		{
			try
			{
				const int PAGE_HEIGHT = 210;
				string samp_text = "ƒAƒƒ“ƒ{Ô‚¢‚È‚ ‚¢‚¤‚¦‚¨B•‚‚«‘”‚É¬ƒGƒr‚à‚¨‚æ‚¢‚Å‚éB";
				HPDFFont[] detail_font = new HPDFFont[16];

				HPDFDocument pdf = new HPDFDocument();

				/*declaration for using Japanese font, encoding. */
				pdf.UseJPEncodings();
				pdf.UseJPFonts();

				detail_font[0] = pdf.GetFont("MS-Mincyo", "90ms-RKSJ-H");
				detail_font[1] = pdf.GetFont("MS-Mincyo,Bold", "90ms-RKSJ-H");
				detail_font[2] = pdf.GetFont("MS-Mincyo,Italic", "90ms-RKSJ-H");
				detail_font[3] = pdf.GetFont("MS-Mincyo,BoldItalic", "90ms-RKSJ-H");
				detail_font[4] = pdf.GetFont("MS-PMincyo", "90msp-RKSJ-H");
				detail_font[5] = pdf.GetFont("MS-PMincyo,Bold", "90msp-RKSJ-H");
				detail_font[6] = pdf.GetFont("MS-PMincyo,Italic", "90msp-RKSJ-H");
				detail_font[7] = pdf.GetFont("MS-PMincyo,BoldItalic",
				"90msp-RKSJ-H");
				detail_font[8] = pdf.GetFont("MS-Gothic", "90ms-RKSJ-H");
				detail_font[9] = pdf.GetFont("MS-Gothic,Bold", "90ms-RKSJ-H");
				detail_font[10] = pdf.GetFont("MS-Gothic,Italic", "90ms-RKSJ-H");
				detail_font[11] = pdf.GetFont("MS-Gothic,BoldItalic", "90ms-RKSJ-H");
				detail_font[12] = pdf.GetFont("MS-PGothic", "90msp-RKSJ-H");
				detail_font[13] = pdf.GetFont("MS-PGothic,Bold", "90msp-RKSJ-H");
				detail_font[14] = pdf.GetFont("MS-PGothic,Italic", "90msp-RKSJ-H");
				detail_font[15] = pdf.GetFont("MS-PGothic,BoldItalic",
				"90msp-RKSJ-H");

				/*configure pdf-document to be compressed. */
				pdf.SetCompressionMode(HPDFCompressionModes.All);

				/*Set page mode to use outlines. */
				pdf.SetPageMode(HPDFPageModes.Outline);

				/*create outline root. */
				HPDFOutline root = pdf.CreateOutline(null, "JP font demo", null);
				root.SetOpened(true);

				for (int i = 0; i <= 15; i++)
				{
					float x_pos;

					/*add a new page object. */
					HPDFPage page = pdf.AddPage();

					/*create outline entry */
					HPDFOutline outline = pdf.CreateOutline(root,
							  detail_font[i].GetFontName(), null);
					HPDFDestination dst = page.CreateDestination();
					outline.SetDestination(dst);

					HPDFFont title_font = pdf.GetFont("Helvetica", null);
					page.SetFont(title_font, 10);

					page.BeginText();

					/*move the position of the text to top of the page. */
					page.MoveTextPos(new HPDFPointStruct() { x = 10, y = 190 });
					page.ShowText(detail_font[i].GetFontName());

					page.SetFont(detail_font[i], 15);
					page.MoveTextPos(new HPDFPointStruct() { x = 10, y = -20 });
					page.ShowText("abcdefghijklmnopqrstuvwxyz");
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -20 });
					page.ShowText("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -20 });
					page.ShowText("1234567890");
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -20 });

					page.SetFont(detail_font[i], 10);
					page.ShowText(samp_text);
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -18 });

					page.SetFont(detail_font[i], 16);
					page.ShowText(samp_text);
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -27 });

					page.SetFont(detail_font[i], 23);
					page.ShowText(samp_text);
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -36 });

					page.SetFont(detail_font[i], 30);
					page.ShowText(samp_text);

					HPDFPointStruct p = page.GetCurrentTextPos();

					/*finish to print text. */
					page.EndText();

					page.SetLineWidth(0.5f);

					x_pos = 20;
					for (int j = 0; j <= samp_text.Length / 2; j++)
					{
						page.MoveTo(new HPDFPointStruct() { x = x_pos, y = p.y - 10 });
						page.LineTo(new HPDFPointStruct() { x = x_pos, y = p.y - 12 });
						page.Stroke();
						x_pos = x_pos + 30;
					}

					page.SetWidth(p.x + 20);
					page.SetHeight(PAGE_HEIGHT);

					page.MoveTo(new HPDFPointStruct() { x = 10, y = PAGE_HEIGHT - 25 });
					page.LineTo(new HPDFPointStruct() { x = p.x + 10, y = PAGE_HEIGHT - 25 });
					page.Stroke();

					page.MoveTo(new HPDFPointStruct() { x = 10, y = PAGE_HEIGHT - 85 });
					page.LineTo(new HPDFPointStruct() { x = p.x + 10, y = PAGE_HEIGHT - 85 });
					page.Stroke();

					page.MoveTo(new HPDFPointStruct() { x = 10, y = p.y - 12 });
					page.LineTo(new HPDFPointStruct() { x = p.x + 10, y = p.y - 12 });
					page.Stroke();
				}

				string s = "JPFontDemo.pdf";
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
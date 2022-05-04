/*
 * << Haru Free PDF Library 2.0.5 >> -- FontDemo.cs
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
using System.Text;
using HPDF;


namespace HPDF.DEMO
{
	public class PDFFont
	{
		public static void Demo(FLibhpdf main)
		{
			string[] font_list = new string[] {
				"Courier",
				"Courier-Bold",
				"Courier-Oblique",
				"Courier-BoldOblique",
				"Helvetica",
				"Helvetica-Bold",
				"Helvetica-Oblique",
				"Helvetica-BoldOblique",
				"Times-Roman",
				"Times-Bold",
				"Times-Italic",
				"Times-BoldItalic",
				"Symbol",
				"ZapfDingbats"
		  };

			try
			{
				HPDFDocument pdf = new HPDFDocument();

				MyText(pdf, false);
				MyText(pdf, true);

				HPDFPage page = pdf.AddPage();

				float height = page.GetHeight();
				float width = page.GetWidth();

				HPDFPointStruct currentpt = page.CurrentPos;

				///* Print the lines of the page. */
				//page.SetLineWidth(1);
				//page.Rectangle(new HPDFPointStruct() { x = 50, y = 50 }, new HPDFSizeStruct() { width = width - 100, height = height - 110 });
				//page.Stroke();

				///* Print the title of the page (with positioning center). */
				//HPDFFont def_font = pdf.GetFont("Helvetica", null);
				//page.SetFont(def_font, 24);

				//float tw = page.GetTextWidth(page_title);
				//page.BeginText();
				//page.TextOut(new HPDFPointStruct() { x = (width - tw) / 2, y = height - 50 }, page_title);
				//page.EndText();

				///* output subtitle. */
				//page.BeginText();
				//page.SetFont(def_font, 16);
				//page.TextOut(new HPDFPointStruct() { x = 60, y = height - 80 }, "<Standerd Type1 fonts samples>");
				//page.EndText();

				//page.BeginText();
				//page.MoveTextPos(new HPDFPointStruct() { x = 60, y = height - 105 });

				//float textleading = page.TextLeading;
				//float textraise = page.TextRaise;

				//for (int i = 0; i < font_list.Length; i++)
				//{
				//	const string samp_text = "abcdefgABCDEFG12345!#$%&+-@?";
				//	HPDFFont font = pdf.GetFont(font_list[i], null);

				//	page.TextLeading = textleading + i * 10;
				//	page.TextRaise = textraise + i * 10;

				//	/* print a label of text */
				//	page.SetFont(def_font, 9);
				//	page.ShowText(font_list[i]);
				//	page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -18 });

				//	/* print a sample text. */
				//	page.SetFont(font, 20);
				//	page.ShowText(samp_text);
				//	page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -20 });
				//}

				//page.EndText();


				//pdf.UseUTF8Encoding();
				page = pdf.AddPage();
				page.SetSize(HPDFPageSizes.A5);

				//HPDFFont c = pdf.GetFont(StandardFonts.Courier, pdf.GetCurrentEncoder()?.Name);// HPDFSingleByteEncoders.WinAnsiEncoding.ToString());
				HPDFFont c = pdf.GetFont(StandardFonts.Courier, HPDFSingleByteEncoders.WinAnsiEncoding.ToString());

				HPDFEncoder enc = pdf.GetCurrentEncoder();
				string sq = c.GetFontName();
				sq = enc?.Name;

				//HPDFFont cb = pdf.GetFont(StandardFonts.CourierBold, pdf.GetCurrentEncoder()?.Name);//, "UTF-8");//HPDFSingleByteEncoders.WinAnsiEncoding.ToString());
				HPDFFont cb = pdf.GetFont(StandardFonts.CourierBold, HPDFSingleByteEncoders.WinAnsiEncoding.ToString());

				//page.BeginText();
				//page.SetFont(c, 15);
				//HPDFPointStruct st = new HPDFPointStruct() { x = 0, y = 30 };
				//page.ShowTextAt($"{st} le tout ensemble", st);

				//page.MoveTextPos(0, -15);
				//page.ShowText($"{page.CurrentTextPos} {DateTime.Now}");

				//page.MoveTextPos(0, -15);
				//page.ShowText(Encoding.UTF8.GetString(Encoding.Default.GetBytes($"{page.CurrentTextPos} Mon texte à moi personnellement")));
				//page.MoveTextPos(0, -15);
				//page.ShowText($"{page.CurrentTextPos} Mon texte à moi personnellement");

				//st.y = 60;
				//page.ShowTextAt($"{st} le tout ensemble", st);
				//page.EndText();

				page.BeginText();
				//page.TextRaise = 100;
				page.SetFont(c, 15);
				page.ShowText($"{page.CurrentTextPos} {DateTime.Now}");

				//page.SetFont(c, 15);
				page.MoveTextPos(new HPDFPointStruct() { x = 0, y = 15 });
				page.ShowText($"{page.CurrentTextPos} Mon texte à moi personnellement");

				page.MoveTextPos(0, 15);
				page.ShowText(Encoding.UTF8.GetString(Encoding.Default.GetBytes($"{page.CurrentTextPos} Mon texte à moi personnellement")));

				page.MoveTextPos(0, 15);
				page.ShowText($"{page.CurrentTextPos} Mon texte à moi personnellement");


				page.MoveTextPos(new HPDFPointStruct() { x = 0, y = 15 });
				page.EndText();


				string s = "PDFfonts.pdf";
				main.ADD($"ready to save: {s}");
				Program.SaveAndStart(pdf, s);
			}
			catch (Exception e)
			{
				main.ADD(e.Message);
				Console.Error.WriteLine(e.Message);
			}
		}
		static void MyText(HPDFDocument pdf, bool add = false)
		{
			string[] font_list = new string[] {
				"Courier",
				"Courier-Bold",
				"Courier-Oblique",
				"Courier-BoldOblique",
				"Helvetica",
				"Helvetica-Bold",
				"Helvetica-Oblique",
				"Helvetica-BoldOblique",
				"Times-Roman",
				"Times-Bold",
				"Times-Italic",
				"Times-BoldItalic",
				"Symbol",
				"ZapfDingbats"
		  };

			HPDFPage page = pdf.AddPage();

			string page_title = $"FontDemo {add}";

			float height = page.GetHeight();
			float width = page.GetWidth();

			HPDFPointStruct currentpt = page.CurrentPos;

			/* Print the lines of the page. */
			page.SetLineWidth(1);
			page.Rectangle(new HPDFPointStruct() { x = 50, y = 50 }, new HPDFSizeStruct() { Width = width - 100, Height = height - 110 });
			page.Stroke();

			/* Print the title of the page (with positioning center). */
			HPDFFont def_font = pdf.GetFont("Helvetica", null);
			page.SetFont(def_font, 24);

			float tw = page.GetTextWidth(page_title);
			page.BeginText();
			page.ShowTextAt(page_title, new HPDFPointStruct() { x = (width - tw) / 2, y = height - 50 });
			page.EndText();

			/* output subtitle. */
			page.BeginText();
			page.SetFont(def_font, 16);
			page.ShowTextAt("<Standerd Type1 fonts samples>", new HPDFPointStruct() { x = 60, y = height - 80 });
			page.EndText();

			page.BeginText();
			page.MoveTextPos(new HPDFPointStruct() { x = 60, y = height - 105 });

			float textleading = page.TextLeading;
			float textraise = page.TextRaise;

			for (int i = 0; i < font_list.Length; i++)
			{
				const string samp_text = "abcdefgABCDEFG12345!#$%&+-@?";
				HPDFFont font = pdf.GetFont(font_list[i], null);

				if (!add)
				{
					/* print a label of text */
					page.SetFont(def_font, 9);
					page.ShowText(font_list[i]);
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -18 });

					/* print a sample text. */
					page.SetFont(font, 20);
					page.ShowText(samp_text);
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -20 });
				}
				else
				{
					//page.TextLeading = textleading + (add ? 100 : 0);
					//page.TextRaise = textraise + (add ? 100 : 0);

					//page.TextLeading = textleading + (add ? 100 : 0);
					page.TextRaise = 50;// textraise + (add ? 100 : 0);

					/* print a label of text */
					page.SetFont(def_font, 9);
					page.ShowText(font_list[i] + font_list[i] + font_list[i] + font_list[i] + font_list[i]);
					//page.MoveTextPos(new HPDFPointStruct() { x = 0, y = add ? -page.TextLeading : -18 });
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -18 });

					/* print a sample text. */
					page.SetFont(font, 20);
					page.ShowText(samp_text + samp_text + samp_text + samp_text + samp_text + samp_text + samp_text);
					//page.MoveTextPos(new HPDFPointStruct() { x = 0, y = add ? -page.TextLeading : -20 });
					page.MoveTextPos(new HPDFPointStruct() { x = 0, y = -20 });
				}
			}

			string crlf = "\r\n";
			string[] blong_sentence =
				{
				"I'm currently using the libharu library in order to render some pdf in a C++ program." ,
				"I have no idea whether it is possible or not to know the size needed in order to draw a specific text with a specific font." ,
				"The HPDF_Page_TextRect drawing method will return an HPDF_PAGE_INSUFFICIENT_SPACE message if the text can't fit in the rect provided, but I'd like to know if there is a way to calculate the minimum size in which a text will fit with a specific font.",
				};
			string long_sentence = "I'm currently using the libharu library in order to render some pdf in a C++ program." + crlf +
				"I have no idea whether it is possible or not to know the size needed in order to draw a specific text with a specific font." + crlf +
				"The HPDF_Page_TextRect drawing method will return an HPDF_PAGE_INSUFFICIENT_SPACE message if the text can't fit in the rect provided, but I'd like to know if there is a way to calculate the minimum size in which a text will fit with a specific font.";

			page.SetFont(pdf.GetFont(font_list[0], null), 15);
			page.AdjustTextLeading();
			page.MoveTextPos(new HPDFPointStruct() { x = 0, y = page.Height / 2 });
			page.ShowText("azertyuiop");
			page.MoveTextPos(new HPDFPointStruct() { x = 0, y = page.Height / 2 - 20 });
			page.ShowText("poiuytreza");


			HPDFPointStruct currentpos = page.CurrentPos;
			page.ShowTextRect(new HPDFRectStruct() { Top = page.Height, Bottom = page.Height - 10, Left= 0, Right = page.Width / 2 }, long_sentence, HPDFTextAlignments.Left, out uint len);

			//float top = page.Height;
			//for (int i = 0; i < blong_sentence.Length; i++)
			//{
			//	HPDFTextWidthStruct textwidth = page.CurrentFont.GetTextWidth(blong_sentence[i]);
			//	float actualwidth = textwidth.ActualWidth(page.CurrentFontSize);
			//	if (!textwidth.IsNull)
			//	{
			//		page.ShowTextRect(new HPDFRectStruct() { top = page.Height - i * page.TextLeading, bottom = 0, left = 0, right = page.Width }, long_sentence, HPDFTextAlignments.Left, out len);
			//	}
			//}

			if (HPDFErrors.HPDF_NO_ERROR != (HPDFErrors)page.LastError)
			{
				string s = $"Error: {page.LastError}, {page.LastErrorAsString()}";
				tw = page.GetTextWidth(s);
				page.ShowTextRect(new HPDFRectStruct() { Top = page.CurrentFontSize + 2, Bottom = 0, Left = 0, Right = tw }, s, HPDFTextAlignments.Left, out len);
			}

			page.EndText();
		}
	}
}
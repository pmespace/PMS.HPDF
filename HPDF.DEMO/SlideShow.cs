/*
 * << Haru Free PDF Library 2.0.5 >> -- SlideShow.cs
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
	public class SlideShow
	{
		private static Random rand;

		private static void PrintPage(HPDFPage page, string caption, HPDFFont font, HPDFTransitionStyles type, HPDFPage prev, HPDFPage next)
		{
			float r = (float)rand.Next(255) / 255;
			float g = (float)rand.Next(255) / 255;
			float b = (float)rand.Next(255) / 255;
			HPDFRectStruct rect;
			HPDFDestination dst;
			HPDFAnnotation annot;

			page.SetWidth(800);
			page.SetHeight(600);

			page.SetRGBFill(new HPDFRGBColorStruct() { R = r, G = g, B = b });

			page.Rectangle(new HPDFPointStruct() { x = 0, y = 0 }, new HPDFSizeStruct() { Width = 800, Height = 600 });
			page.Fill();

			page.SetRGBFill(new HPDFRGBColorStruct() { R = 1.0f - r, G = 1.0f - g, B = 1.0f - b });

			page.SetFont(font, 30);

			page.BeginText();
			page.SetTextMatrix(0.8f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f);
			page.ShowTextAt(caption, new HPDFPointStruct() { x = 50, y = 530 });

			page.SetTextMatrix(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f);
			page.SetFont(font, 20);
			page.ShowTextAt("Type \"Ctrl+L\" in order to return from full screen mode.", new HPDFPointStruct() { x = 55, y = 300 });
			page.EndText();

			page.SetSlideShow(type, 5.0f, 1.0f);

			page.SetFont(font, 20);

			if (next != null)
			{
				page.BeginText();
				page.ShowTextAt("Next=>", new HPDFPointStruct() { x = 680, y = 50 });
				page.EndText();

				rect.Left = 680;
				rect.Right = 750;
				rect.Top = 70;
				rect.Bottom = 50;
				dst = next.CreateDestination();
				dst.SetFit();
				annot = page.CreateLinkAnnotation(rect, dst);
				annot.SetBorderStyle(0, 0, 0);
				annot.SetHighlightMode(HPDFAnnotationHighlightModes.InvertBox);
			}

			if (prev != null)
			{
				page.BeginText();
				page.ShowTextAt( "<=Prev",new HPDFPointStruct() { x = 50, y = 50 });
				page.EndText();

				rect.Left = 50;
				rect.Right = 110;
				rect.Top = 70;
				rect.Bottom = 50;
				dst = prev.CreateDestination();
				dst.SetFit();
				annot = page.CreateLinkAnnotation(rect, dst);
				annot.SetBorderStyle(0, 0, 0);
				annot.SetHighlightMode(HPDFAnnotationHighlightModes.InvertBox);
			}
		}


		public static void Demo(FLibhpdf main)
		{
			rand = new Random();

			try
			{
				HPDFDocument pdf = new HPDFDocument();
				HPDFPage[] page = new HPDFPage[17];

				/* create default-font */
				HPDFFont font = pdf.GetFont("Courier", null);

				/* Add 17 pages to the document. */
				page[0] = pdf.AddPage();
				page[1] = pdf.AddPage();
				page[2] = pdf.AddPage();
				page[3] = pdf.AddPage();
				page[4] = pdf.AddPage();
				page[5] = pdf.AddPage();
				page[6] = pdf.AddPage();
				page[7] = pdf.AddPage();
				page[8] = pdf.AddPage();
				page[9] = pdf.AddPage();
				page[10] = pdf.AddPage();
				page[11] = pdf.AddPage();
				page[12] = pdf.AddPage();
				page[13] = pdf.AddPage();
				page[14] = pdf.AddPage();
				page[15] = pdf.AddPage();
				page[16] = pdf.AddPage();

				PrintPage(page[0], "HPDF_TS_WIPE_RIGHT", font, HPDFTransitionStyles.WipeRight, null, page[1]);
				PrintPage(page[1], "HPDF_TS_WIPE_UP", font, HPDFTransitionStyles.WipeUp, page[0], page[2]);
				PrintPage(page[2], "HPDF_TS_WIPE_LEFT", font, HPDFTransitionStyles.WipeLeft, page[1], page[3]);
				PrintPage(page[3], "HPDF_TS_WIPE_DOWN", font, HPDFTransitionStyles.WipeDown, page[2], page[4]);
				PrintPage(page[4], "HPDF_TS_BARN_DOORS_HORIZONTAL_OUT", font, HPDFTransitionStyles.BarnDoorsHorizontalOut, page[3], page[5]);
				PrintPage(page[5], "HPDF_TS_BARN_DOORS_HORIZONTAL_IN", font, HPDFTransitionStyles.BarnDoorsHorizontalIn, page[4], page[6]);
				PrintPage(page[6], "HPDF_TS_BARN_DOORS_VERTICAL_OUT", font, HPDFTransitionStyles.BarnDoorsVerticalOut, page[5], page[7]);
				PrintPage(page[7], "HPDF_TS_BARN_DOORS_VERTICAL_IN", font, HPDFTransitionStyles.BarnDoorsVerticalIn, page[6], page[8]);
				PrintPage(page[8], "HPDF_TS_BOX_OUT", font, HPDFTransitionStyles.BoxOut, page[7], page[9]);
				PrintPage(page[9], "HPDF_TS_BOX_IN", font, HPDFTransitionStyles.BoxIn, page[8], page[10]);
				PrintPage(page[10], "HPDF_TS_BLINDS_HORIZONTAL", font, HPDFTransitionStyles.BlindsHorizontal, page[9], page[11]);
				PrintPage(page[11], "HPDF_TS_BLINDS_VERTICAL", font, HPDFTransitionStyles.BlindsVertical, page[10], page[12]);
				PrintPage(page[12], "HPDF_TS_DISSOLVE", font, HPDFTransitionStyles.Dissolve, page[11], page[13]);
				PrintPage(page[13], "HPDF_TS_GLITTER_RIGHT", font, HPDFTransitionStyles.GlitterRight, page[12], page[14]);
				PrintPage(page[14], "HPDF_TS_GLITTER_DOWN", font, HPDFTransitionStyles.GlitterDown, page[13], page[15]);
				PrintPage(page[15], "HPDF_TS_GLITTER_TOP_LEFT_TO_BOTTOM_RIGHT", font, HPDFTransitionStyles.GlitterTopLeftToBottomRight, page[14], page[16]);
				PrintPage(page[16], "HPDF_TS_REPLACE", font, HPDFTransitionStyles.Replace, page[15], null);

				pdf.SetPageMode(HPDFPageModes.Fullscreen);

				string s = "SlideShowDemo.pdf";
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
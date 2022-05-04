/*
 * << Haru Free PDF Library 2.0.5 >> -- OutlineDemo.cs
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
	public class Outline
	{
		private static void PrintPage(HPDFPage page, int page_num)
		{
			page.SetWidth(800);
			page.SetHeight(800);

			page.BeginText();
			page.MoveTextPos(new HPDFPointStruct() { x = 30, y = 740 });
			string buf = "Page:" + page_num;
			page.ShowText(buf);
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

				pdf.SetPageMode(HPDFPageModes.Outline);

				/* add a new page object. */
				/* Add 3 pages to the document. */
				HPDFPage page0 = pdf.AddPage();
				page0.SetFont(font, 30);
				PrintPage(page0, 1);

				HPDFPage page1 = pdf.AddPage();
				page1.SetFont(font, 30);
				PrintPage(page1, 2);

				HPDFPage page2 = pdf.AddPage();
				page2.SetFont(font, 30);
				PrintPage(page2, 3);

				/* create outline root. */
				HPDFOutline root = pdf.CreateOutline(null, "OutlineRoot", null);
				root.SetOpened(true);

				HPDFOutline outline0 = pdf.CreateOutline(root, "page1", null);
				HPDFOutline outline1 = pdf.CreateOutline(root, "page2", null);

				/* create outline with test which is ISO8859-2 encoding */
				HPDFOutline outline2 = pdf.CreateOutline(root, "ISO8859-2 text ÓÔÕÖ×ØÙ", pdf.GetEncoder("ISO8859-2"));

				/* create destination objects on each pages
				 * and link it to outline items.
				 */
				HPDFDestination dst = page0.CreateDestination();
				dst.SetXYZ(0, page0.GetHeight(), 1);
				outline0.SetDestination(dst);

				dst = page1.CreateDestination();
				dst.SetXYZ(0, page1.GetHeight(), 1);
				outline1.SetDestination(dst);

				dst = page2.CreateDestination();
				dst.SetFit();
				outline2.SetDestination(dst);

				string s = "OutlineDemo.pdf";
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
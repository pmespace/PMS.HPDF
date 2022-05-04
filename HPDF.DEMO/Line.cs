/*
 * << Haru Free PDF Library 2.0.5 >> -- LineDemo.cs
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
	public class Line
	{
		private static void DrawLine(HPDFPage page, float x, float y, string label)
		{
			page.BeginText();
			page.MoveTextPos(new HPDFPointStruct() { x = x, y = y - 10 });
			page.ShowText(label);
			page.EndText();

			page.MoveTo(new HPDFPointStruct() { x = x, y = y - 15 });
			page.LineTo(new HPDFPointStruct() { x = x + 220, y = y - 15 });
			page.Stroke();
		}

		private static void DrawLine2(HPDFPage page, float x, float y, string label)
		{
			page.BeginText();
			page.MoveTextPos(new HPDFPointStruct() { x = x, y = y });
			page.ShowText(label);
			page.EndText();

			page.MoveTo(new HPDFPointStruct() { x = x + 30, y = y - 25 });
			page.LineTo(new HPDFPointStruct() { x = x + 160, y = y - 25 });
			page.Stroke();
		}

		private static void DrawRect(HPDFPage page, float x, float y, string label)
		{
			page.BeginText();
			page.MoveTextPos(new HPDFPointStruct() { x = x, y = y - 10 });
			page.ShowText(label);
			page.EndText();

			page.Rectangle(new HPDFPointStruct() { x = x, y = y - 40 }, new HPDFSizeStruct() { Width = 220, Height = 25 });
		}

		public static void Demo(FLibhpdf main)
		{
			try
			{
				const string page_title = "LineDemo";

				HPDFDocument pdf = new HPDFDocument();

				/* create default-font */
				HPDFFont font = pdf.GetFont("Helvetica", null);

				/* add a new page object. */
				HPDFPage page = pdf.AddPage();

				/* print the lines of the page. */
				page.SetLineWidth(1);
				page.Rectangle(new HPDFPointStruct() { x = 50, y = 50 }, new HPDFSizeStruct() { Width = page.GetWidth() - 100, Height = page.GetHeight() - 110 });
				page.Stroke();

				/* print the title of the page (with positioning center). */
				//page.SetFont(font, 24);
				page.SetFont(font, 74);
				float tw = page.GetTextWidth(page_title);
				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = (page.GetWidth() - tw) / 2, y = page.GetHeight() - 50 });
				page.ShowText(page_title);
				page.EndText();

				page.SetFont(font, 24);

				page.SetFont(font, 10);

				/* Draw verious widths of lines. */
				page.SetLineWidth(0);
				DrawLine(page, 60, 770, "line width = 0");

				page.SetLineWidth(1.0f);
				DrawLine(page, 60, 740, "line width = 1.0");

				page.SetLineWidth(2.0f);
				DrawLine(page, 60, 710, "line width = 2.0");

				/* Line dash pattern */
				ushort[] dash_mode1 = new ushort[] { 3 };
				ushort[] dash_mode2 = new ushort[] { 3, 7 };
				ushort[] dash_mode3 = new ushort[] { 8, 7, 2, 7 };

				page.SetLineWidth(1.0f);

				page.SetDash(dash_mode1, 1);
				DrawLine(page, 60, 680, "dash_ptn=[3], phase=1 -- 2 on, 3 off, 3 on...");

				Console.Error.WriteLine("001");
				page.SetDash(dash_mode2, 2);
				DrawLine(page, 60, 650, "dash_ptn=[7, 3], phase=2 -- 5 on 3 off, 7 on,...");

				page.SetDash(dash_mode3, 0);
				DrawLine(page, 60, 620, "dash_ptn=[8, 7, 2, 7], phase=0");

				page.SetDash(null, 0);

				page.SetLineWidth(30);
				page.SetRGBStroke(new HPDFRGBColorStruct() { R = 0.0f, G = 0.5f, B = 0.0f });

				/* Line Cap Style */
				page.SetLineEndShape(HPDFLineEndShapes.Butt);
				DrawLine2(page, 60, 570, "HPDF_BUTT_END");

				page.SetLineEndShape(HPDFLineEndShapes.Round);
				DrawLine2(page, 60, 505, "HPDF_ROUND_END");

				page.SetLineEndShape(HPDFLineEndShapes.ProjectingSquare);
				DrawLine2(page, 60, 440, "HPDF_PROJECTING_SCUARE_END");

				/* Line Join Style */
				page.SetLineWidth(30);
				page.SetRGBStroke(new HPDFRGBColorStruct() { R = 0.0f, G = 0.0f, B = 0.5f });

				page.SetLineJoin(HPDFLineJoins.Miter);
				page.MoveTo(new HPDFPointStruct() { x = 120, y = 300 });
				page.LineTo(new HPDFPointStruct() { x = 160, y = 340 });
				page.LineTo(new HPDFPointStruct() { x = 200, y = 300 });
				page.Stroke();

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 60, y = 360 });
				page.ShowText("HPDF_MITER_JOIN");
				page.EndText();

				page.SetLineJoin(HPDFLineJoins.Round);
				page.MoveTo(new HPDFPointStruct() { x = 120, y = 195 });
				page.LineTo(new HPDFPointStruct() { x = 160, y = 235 });
				page.LineTo(new HPDFPointStruct() { x = 200, y = 195 });
				page.Stroke();

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 60, y = 255 });
				page.ShowText("HPDF_ROUND_JOIN");
				page.EndText();

				page.SetLineJoin(HPDFLineJoins.Bevel);
				page.MoveTo(new HPDFPointStruct() { x = 120, y = 90 });
				page.LineTo(new HPDFPointStruct() { x = 160, y = 130 });
				page.LineTo(new HPDFPointStruct() { x = 200, y = 90 });
				page.Stroke();

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 60, y = 150 });
				page.ShowText("HPDF_BEVEL_JOIN");
				page.EndText();

				/* Draw Rectangle */
				page.SetLineWidth(2);
				page.SetRGBStroke(new HPDFRGBColorStruct() { R = 0, G = 0, B = 0 });
				page.SetRGBFill(new HPDFRGBColorStruct() { R = 0.75f, G = 0.0f, B = 0.0f });

				DrawRect(page, 300, 770, "Stroke");
				page.Stroke();

				DrawRect(page, 300, 720, "Fill");
				page.Fill();

				DrawRect(page, 300, 670, "Fill then Stroke");
				page.FillStroke();

				/* Clip Rect */
				page.GSave();  /* Save the current graphic state */
				DrawRect(page, 300, 620, "Clip Rectangle");
				page.Clip();
				page.Stroke();
				page.SetFont(font, 13);

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 290, y = 600 });
				page.SetTextLeading(12);
				page.ShowText("Clip Clip Clip Clip Clip Clipi Clip Clip Clip");
				page.ShowTextNextLine("Clip Clip Clip Clip Clip Clip Clip Clip Clip");
				page.ShowTextNextLine("Clip Clip Clip Clip Clip Clip Clip Clip Clip");
				page.EndText();
				page.GRestore();

				/* Curve Example(CurveTo2) */
				float x = 330;
				float y = 440;
				float x1 = 430;
				float y1 = 530;
				float x2 = 480;
				float y2 = 470;
				float x3 = 480;
				float y3 = 90;

				page.SetRGBFill(new HPDFRGBColorStruct() { R = 0, G = 0, B = 0 });

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 300, y = 40 });
				page.ShowText("CurveTo2(x1, y1, x2. y2)");
				page.EndText();

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = x + 5, y = y - 5 });
				page.ShowText("Current point");
				page.MoveTextPos(new HPDFPointStruct() { x = x1 - x, y = y1 - y });
				page.ShowText("(x1, y1)");
				page.MoveTextPos(new HPDFPointStruct() { x = x2 - x1, y = y2 - y1 });
				page.ShowText("(x2, y2)");
				page.EndText();

				page.SetDash(dash_mode1, 0);

				page.SetLineWidth(0.5f);
				page.MoveTo(new HPDFPointStruct() { x = x1, y = y1 });
				page.LineTo(new HPDFPointStruct() { x = x2, y = y2 });
				page.Stroke();

				page.SetDash(null, 0);

				page.SetLineWidth(1.5f);

				page.MoveTo(new HPDFPointStruct() { x = x, y = y });
				page.CurveOnEndingPoint(new HPDFPointStruct() { x = x1, y = y1 }, new HPDFPointStruct() { x = x2, y = y2 });
				page.Stroke();

				/* Curve Example(CurveTo3) */
				y -= 150;
				y1 -= 150;
				y2 -= 150;

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 300, y = 390 });
				page.ShowText("CurveTo3(x1, y1, x2. y2)");
				page.EndText();

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = x + 5, y = y - 5 });
				page.ShowText("Current point");
				page.MoveTextPos(new HPDFPointStruct() { x = x1 - x, y = y1 - y });
				page.ShowText("(x1, y1)");
				page.MoveTextPos(new HPDFPointStruct() { x = x2 - x1, y = y2 - y1 });
				page.ShowText("(x2, y2)");
				page.EndText();

				page.SetDash(dash_mode1, 0);

				page.SetLineWidth(0.5f);
				page.MoveTo(new HPDFPointStruct() { x = x, y = y });
				page.LineTo(new HPDFPointStruct() { x = x1, y = y1 });
				page.Stroke();

				page.SetDash(null, 0);

				page.SetLineWidth(1.5f);
				page.MoveTo(new HPDFPointStruct() { x = x, y = y });
				page.CurveOnStartingPoint(new HPDFPointStruct() { x = x1, y = y1 }, new HPDFPointStruct() { x = x2, y = y2 });
				page.Stroke();

				/* Curve Example(CurveTo) */
				y -= 150;
				y1 -= 160;
				y2 -= 130;
				x2 += 10;

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = 300, y = 240 });
				page.ShowText("CurveTo(x1, y1, x2. y2, x3, y3)");
				page.EndText();

				page.BeginText();
				page.MoveTextPos(new HPDFPointStruct() { x = x + 5, y = y - 5 });
				page.ShowText("Current point");
				page.MoveTextPos(new HPDFPointStruct() { x = x1 - x, y = y1 - y });
				page.ShowText("(x1, y1)");
				page.MoveTextPos(new HPDFPointStruct() { x = x2 - x1, y = y2 - y1 });
				page.ShowText("(x2, y2)");
				page.MoveTextPos(new HPDFPointStruct() { x = x3 - x2, y = y3 - y2 });
				page.ShowText("(x3, y3)");
				page.EndText();

				page.SetDash(dash_mode1, 0);

				page.SetLineWidth(0.5f);
				page.MoveTo(new HPDFPointStruct() { x = x, y = y });
				page.LineTo(new HPDFPointStruct() { x = x1, y = y1 });
				page.Stroke();
				page.MoveTo(new HPDFPointStruct() { x = x2, y = y2 });
				page.LineTo(new HPDFPointStruct() { x = x3, y = y3 });
				page.Stroke();

				page.SetDash(null, 0);

				page.SetLineWidth(1.5f);
				page.MoveTo(new HPDFPointStruct() { x = x, y = y });
				page.CurveTo(new HPDFPointStruct() { x = x1, y = y1 }, new HPDFPointStruct() { x = x2, y = y2 }, new HPDFPointStruct() { x = x3, y = y3 });
				page.Stroke();

				string s = "LineDemo.pdf";
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
/*
 * << Haru Free PDF Library 2.0.5 >> -- Permission.cs
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
	public class Permission
	{

		public static void Demo(FLibhpdf main)
		{
			string owner_passwd = "owner";
			string user_passwd = "";
			string text = "User cannot print and copy this document.";

			try
			{
				HPDFDocument pdf = new HPDFDocument();

				/* create default-font */
				HPDFFont font = pdf.GetFont("Helvetica", null);

				/* add a new page object. */
				HPDFPage page = pdf.AddPage();

				page.SetSize(HPDFPageSizes.B5, HPDFPageDirections.Landscape);

				page.BeginText();
				page.SetFont(font, 20);
				float tw = page.GetTextWidth(text);
				page.MoveTextPos(new HPDFPointStruct() { x = (page.GetWidth() - tw) / 2, y = (page.GetHeight() - 20) / 2 });
				page.ShowText(text);
				page.EndText();

				pdf.SetPassword(owner_passwd, user_passwd);
				pdf.SetPermission(HPDFPermissions.Read);

				/* use 128 bit revision 3 encryption */
				pdf.SetEncryptionMode(HPDFEncryptModes.R3, 16);

				string s = "Permission.pdf";
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
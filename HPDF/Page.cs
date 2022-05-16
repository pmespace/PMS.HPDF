/*
 * << Haru Free PDF Library 2.0.6 >> -- hpdf.cs
 *
 * C# wrapper for libhpdf.dll
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
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using HPDF.Properties;
using System.IO;

namespace HPDF
{
	/// <summary>
	/// A page contaiined inside a PDF document
	/// </summary>
	public class HPDFPage : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetWidth(IntPtr page, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetHeight(IntPtr page, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetSize(IntPtr page, HPDFPageSizes size, HPDFPageDirections direction);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetRotate(IntPtr page, ushort angle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Page_CreateDestination(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Page_CreateTextAnnot(IntPtr page, HPDFRectStruct rect, string text, IntPtr encoder);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Page_CreateLinkAnnot(IntPtr page, HPDFRectStruct rect, IntPtr dst);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Page_CreateURILinkAnnot(IntPtr page, HPDFRectStruct rect, string url);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_TextWidth(IntPtr page, string text);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_MeasureText(IntPtr page, string text, float width, int wordwrap, ref float real_width);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetWidth(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetHeight(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern ushort HPDF_Page_GetGMode(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFPointStruct HPDF_Page_GetCurrentPos(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFPointStruct HPDF_Page_GetCurrentTextPos(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Page_GetCurrentFont(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetCurrentFontSize(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFTransformationMatrixStruct HPDF_Page_GetTransMatrix(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetLineWidth(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFLineEndShapes HPDF_Page_GetLineCap(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFLineJoins HPDF_Page_GetLineJoin(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetMiterLimit(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFDashModeStructInternalStruct HPDF_Page_GetDash(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetFlat(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetCharSpace(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetWordSpace(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetHorizontalScalling(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetTextLeading(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFTextRenderingModes HPDF_Page_GetTextRenderingMode(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetTextRaise(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFRGBColorStruct HPDF_Page_GetRGBFill(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFRGBColorStruct HPDF_Page_GetRGBStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFCMYKColorStruct HPDF_Page_GetCMYKFill(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFCMYKColorStruct HPDF_Page_GetCMYKStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetGrayFill(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern float HPDF_Page_GetGrayStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFColorSpaces HPDF_Page_GetStrokingColorSpace(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFColorSpaces HPDF_Page_GetFillingColorSpace(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFTransformationMatrixStruct HPDF_Page_GetTextMatrix(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_GetGStateDepth(IntPtr page);

		/*--- General graphics state -----------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetLineWidth(IntPtr page, float line_width);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetLineCap(IntPtr page, HPDFLineEndShapes line_cap);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetLineJoin(IntPtr page, HPDFLineJoins line_join);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetMiterLimit(IntPtr page, float miter_limit);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetDash(IntPtr page, ushort[] array, uint num_param, uint phase);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetFlat(IntPtr page, float flatness);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetExtGState(IntPtr page, IntPtr ext_gstate);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_GSave(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_GRestore(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Concat(IntPtr page, float a, float b, float c, float d, float x, float y);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_MoveTo(IntPtr page, float x, float y);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_LineTo(IntPtr page, float x, float y);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_CurveTo(IntPtr page, float x1, float y1, float x2, float y2, float x3, float y3);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_CurveTo2(IntPtr page, float x2, float y2, float x3, float y3);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_CurveTo3(IntPtr page, float x1, float y1, float x3, float y3);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ClosePath(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Rectangle(IntPtr page, float x, float y, float width, float height);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Stroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ClosePathStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Fill(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Eofill(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_FillStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_EofillStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ClosePathFillStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ClosePathEofillStroke(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_EndPath(IntPtr page);

		/*--- Clipping paths operator --------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Clip(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Eoclip(IntPtr page);

		/*--- Text object operator -----------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_BeginText(IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_EndText(IntPtr page);

		/*--- Text state ---------------------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetCharSpace(IntPtr page, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetWordSpace(IntPtr page, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetHorizontalScalling(IntPtr page, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetTextLeading(IntPtr page, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetFontAndSize(IntPtr page, IntPtr hfont, float size);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetTextRenderingMode(IntPtr page, HPDFTextRenderingModes mode);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetTextRaise(IntPtr page, float value);

		/*--- Text positioning ---------------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_MoveTextPos(IntPtr page, float x, float y);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_MoveTextPos2(IntPtr page, float x, float y);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetTextMatrix(IntPtr page, float a, float b, float c, float d, float x, float y);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_MoveToNextLine(IntPtr page);

		/*--- Text showing -------------------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ShowText(IntPtr page, string text);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ShowTextNextLine(IntPtr page, string text);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ShowTextNextLineEx(IntPtr page, float word_space, float char_space, string text);

		/*--- Color showing ------------------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetGrayFill(IntPtr page, float gray);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetGrayStroke(IntPtr page, float gray);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetRGBFill(IntPtr page, float r, float g, float b);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetRGBStroke(IntPtr page, float r, float g, float b);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetCMYKFill(IntPtr page, float c, float m, float y, float k);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetCMYKStroke(IntPtr page, float c, float m, float y, float k);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_ExecuteXObject(IntPtr page, IntPtr obj);

		/*---------------------------------------------------------------------*/

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_DrawImage(IntPtr page, IntPtr image, float x, float y, float width, float height);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Circle(IntPtr page, float x, float y, float ray);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Arc(IntPtr page, float x, float y, float ray, float ang1, float ang2);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_Ellipse(IntPtr page, float x, float y, float xray, float yray);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_TextOut(IntPtr page, float xpos, float ypos, string text);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_TextRect(IntPtr page, float left, float top, float right, float bottom, string text, HPDFTextAlignments align, ref uint len);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Page_SetSlideShow(IntPtr page, HPDFTransitionStyles type, float disp_time, float trans_time);
		#endregion

		#region properties
		// handle to an instance of a HPDF_Doc object.
		private IntPtr handle;
		/// <summary>
		/// Width of the page
		/// </summary>
		public float Width { get => GetWidth(); set { SetWidth(value); } }
		/// <summary>
		/// Height of the page
		/// </summary>
		public float Height { get => GetHeight(); set { SetHeight(value); } }
		/// <summary>
		/// Current word spacing
		/// </summary>
		public float WordSpacing { get => GetWordSpace(); set { SetWordSpace(value); } }
		/// <summary>
		/// Current char spacing
		/// </summary>
		public float CharSpacing { get => GetCharSpace(); set { SetCharSpace(value); } }
		/// <summary>
		/// Indicate how many points to put the text up (or down) the current text position on the page.
		/// Use with caution.
		/// </summary>
		public float TextRaise { get => GetTextRaise(); set { SetTextRaise(value); } }
		/// <summary>
		/// Current text leading (line spacing).
		/// This applies only if case positionning is done automatically.
		/// </summary>
		public float TextLeading { get => GetTextLeading(); set { SetTextLeading(value); } }
		/// <summary>
		/// Filling RGB color of a shape
		/// </summary>
		public HPDFRGBColorStruct RGBFill { get => GetRGBFill(); set { SetRGBFill(value); } }
		/// <summary>
		/// RGB color of a line around a shape
		/// </summary>
		public HPDFRGBColorStruct RGBStroke { get => GetRGBStroke(); set { SetRGBStroke(value); } }
		/// <summary>
		/// Filling CMYK color of a shape
		/// </summary>
		public HPDFCMYKColorStruct CMYKFill { get => GetCMYKFill(); set { SetCMYKFill(value); } }
		/// <summary>
		/// CMYK color of a line around a shape
		/// </summary>
		public HPDFCMYKColorStruct CMYKStroke { get => GetCMYKStroke(); set { SetCMYKStroke(value); } }
		/// <summary>
		/// Filling Gray color of a shape
		/// </summary>
		public float GrayFill { get => GetGrayFill(); set { SetGrayFill(value); } }
		/// <summary>
		/// Gray color of a line around a shape
		/// </summary>
		public float GrayStroke { get => GetGrayStroke(); set { SetGrayStroke(value); } }
		/// <summary>
		/// Current position on the page
		/// </summary>
		public HPDFPointStruct CurrentPos { get => GetCurrentPos(); }
		/// <summary>
		/// Current position on the page in text mode
		/// </summary>
		public HPDFPointStruct CurrentTextPos { get => GetCurrentTextPos(); }
		/// <summary>
		/// Current font
		/// </summary>
		public HPDFFont CurrentFont { get => GetCurrentFont(); }
		/// <summary>
		/// Current font name
		/// </summary>
		public string CurrentFontName { get => GetCurrentFontName(); }
		/// <summary>
		/// Current font size
		/// </summary>
		public float CurrentFontSize { get => GetCurrentFontSize(); }
		/// <summary>
		/// Current position is no longer inside the page
		/// </summary>
		public bool OutsideOfPage { get => OutsideOfPageX || OutsideOfPageY; }
		/// <summary>
		/// Current position is no loner inside the page width
		/// </summary>
		public bool OutsideOfPageX { get => CurrentPos.x > GetWidth(); }
		/// <summary>
		/// Current position is no longer page height
		/// </summary>
		public bool OutsideOfPageY { get => CurrentPos.y > GetHeight(); }
		#endregion

		#region constructor
		/// <summary>
		/// Create a page object
		/// Raises exeception if <paramref name="h"/> is <see cref="IntPtr.Zero"/>
		/// </summary>
		/// <param name="h">Pointer to the page to create an object for</param>
		public HPDFPage(IntPtr h)
		{
			if (IntPtr.Zero == h)
			{
				throw new Exception(Resources.FailedCreatingPage);
			}
			this.handle = h;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Get the width of a page
		/// </summary>
		/// <returns>The width of the page or 0 if an error has occurred</returns>
		public float GetWidth()
		{
			return HPDF_Page_GetWidth(handle);
		}
		/// <summary>
		/// changes the width of a page
		/// </summary>
		/// <param name="value">Specify the new width of a page. The valid value is between 3 and 14400.</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetWidth(float value)
		{
			LastError = HPDF_Page_SetWidth(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the height of a page
		/// </summary>
		/// <returns>The height of the page or 0 if an error has occurred</returns>
		public float GetHeight()
		{
			return HPDF_Page_GetHeight(handle);
		}
		/// <summary>
		/// changes the height of a page
		/// </summary>
		/// <param name="value">Specify the new height of a page. The valid value is between 3 and 14400.</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetHeight(float value)
		{
			LastError = HPDF_Page_SetHeight(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Change the size and direction of a page to a predefined size
		/// </summary>
		/// <param name="size"><see cref="HPDFPageSizes"/></param>
		/// <param name="direction"><see cref="HPDFPageDirections"/></param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetSize(HPDFPageSizes size, HPDFPageDirections direction = HPDFPageDirections.Portrait)
		{
			LastError = HPDF_Page_SetSize(handle, size, direction);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set rotation angle of the page
		/// </summary>
		/// <param name="angle">Specify the rotation angle of the page. It must be a multiple of 90 Degrees</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetRotate(ushort angle)
		{
			LastError = HPDF_Page_SetRotate(handle, angle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Create a new destination (<see cref="HPDFDestination"/>) object for the page
		/// </summary>
		/// <returns>A <see cref="HPDFDestination"/> object or null if an error has occurred</returns>
		public HPDFDestination CreateDestination()
		{
			IntPtr hdest = HPDF_Page_CreateDestination(handle);
			return (IntPtr.Zero == hdest ? null : new HPDFDestination(hdest));
		}
		/// <summary>
		/// Create a new text annotation object for the page
		/// </summary>
		/// <param name="rect">A Rectangle where the annotation is displayed</param>
		/// <param name="text">The text to be displayed</param>
		/// <param name="encoder">An encoder handle which is used to encode the text. If it is null a default encoder is used</param>
		/// <returns>A <see cref="HPDFAnnotation"/> object or null if an error has occurred</returns>
		public HPDFAnnotation CreateTextAnnotation(HPDFRectStruct rect, string text, HPDFEncoder encoder = null)
		{
			IntPtr hannot = HPDF_Page_CreateTextAnnot(handle, rect, text, (encoder == null ? IntPtr.Zero : encoder.GetHandle()));
			return (IntPtr.Zero == hannot ? null : new HPDFAnnotation(hannot));
		}
		/// <summary>
		/// Create a new link annotation object for the page
		/// </summary>
		/// <param name="rect">A Rectangle where the annotation is displayed</param>
		/// <param name="dst">handle of destination object to jump to</param>
		/// <returns>A <see cref="HPDFAnnotation"/> object or null if an error has occurred</returns>
		public HPDFAnnotation CreateLinkAnnotation(HPDFRectStruct rect, HPDFDestination dst)
		{
			try
			{
				IntPtr hannot = HPDF_Page_CreateLinkAnnot(handle, rect, dst.GetHandle());
				return (IntPtr.Zero == hannot ? null : new HPDFAnnotation(hannot));
			}
			catch (Exception) { }
			return null;
		}
		/// <summary>
		/// Create a new web link annotation object for the page
		/// </summary>
		/// <param name="rect">A Rectangle where the annotation is displayed</param>
		/// <param name="uri">URL of destination to jump to</param>
		/// <returns>A <see cref="HPDFAnnotation"/> object or null if an error has occurred</returns>
		public HPDFAnnotation CreateURILinkAnnotation(HPDFRectStruct rect, string uri)
		{
			if (string.IsNullOrEmpty(uri)) return null;
			IntPtr hannot = HPDF_Page_CreateURILinkAnnot(handle, rect, uri);
			return (IntPtr.Zero == hannot ? null : new HPDFAnnotation(hannot));
		}
		/// <summary>
		/// Get the width of the text in current fontsize, character spacing and word spacing
		/// </summary>
		/// <param name="text">Text to test</param>
		/// <returns>Text width</returns>
		public float GetTextWidth(string text)
		{
			return HPDF_Page_TextWidth(handle, text);
		}
		/// <summary>
		/// Calculate the byte length which can be included within the specified width.
		/// When there are three words of "ABCDE", "FGH", and "IJKL", and the substring until "J" can be included within the width.
		/// If <paramref name="word_wrap"/> is false it returns 12; If word_wrap parameter is true it returns 10 (the end of the previous word).
		///  00000000011111
		///  12345678901234
		///  ABCDE FGH IJKL
		///  *----------* word_wrap is false
		///  *--------*   word_wrap is true
		/// </summary>
		/// <param name="text">Text to measure</param>
		/// <param name="available_width">The available width of the area to put the text, this is the width the application allows for the text</param>
		/// <param name="word_wrap">True to wrap before a word, false to wrap inside a word</param>
		/// <param name="real_width">The real width of the text</param>
		/// <returns>The number of characters which can be included within the specified width in current fontsize, character spacing and word spacing or 0 if an error has occurred</returns>
		public uint GetNumberOfDisplayableCharacters(string text, float available_width, bool word_wrap, ref float real_width)
		{
			return HPDF_Page_MeasureText(handle, text, available_width, word_wrap ? HPDF_TRUE : HPDF_FALSE, ref real_width);
		}
		/// <summary>
		/// Get the current graphics mode
		/// </summary>
		/// <returns>Current graphic mode or 0 if an error has occurred</returns>
		public ushort GetGraphicMode()
		{
			return HPDF_Page_GetGMode(handle);
		}
		/// <summary>
		/// Get the current position for path painting.
		/// An application can invoke this function only when graphics mode is <see cref="HPDFGraphicModes.PathObject"/>
		/// </summary>
		/// <returns>A <see cref="HPDFPointStruct"/> indicating the current position for path painting on the page or <see cref="HPDFPointStruct"/> {0, 0} if any error occurs</returns>
		public HPDFPointStruct GetCurrentPos()
		{
			return HPDF_Page_GetCurrentPos(handle);
		}
		/// <summary>
		/// Get the current position for text showing.
		/// An application can invoke this function only when graphics mode is <see cref="HPDFGraphicModes.TextObject"/>
		/// </summary>
		/// <returns>A <see cref="HPDFPointStruct"/> indicating the current position for text showing on the page or <see cref="HPDFPointStruct"/> {0, 0} if any error occurs</returns>
		public HPDFPointStruct GetCurrentTextPos()
		{
			return HPDF_Page_GetCurrentTextPos(handle);
		}
		/// <summary>
		/// Get the current <see cref="HPDFFont"/> font if any
		/// </summary>
		/// <returns>A <see cref="HPDFFont"/> object if found, null otherwise</returns>
		public HPDFFont GetCurrentFont()
		{
			IntPtr h = HPDF_Page_GetCurrentFont(handle);
			return (IntPtr.Zero == h ? null : new HPDFFont(h));
		}
		/// <summary>
		/// Get the current font name if any
		/// </summary>
		/// <returns>The current font name or null</returns>
		public string GetCurrentFontName()
		{
			IntPtr h = HPDF_Page_GetCurrentFont(handle);
			if (IntPtr.Zero != h)
			{
				HPDFFont f = new HPDFFont(h);
				return f.GetFontName();
			}
			return null;
		}
		/// <summary>
		/// Get the current font size if any
		/// </summary>
		/// <returns>Size of font if found, 0 otherwise</returns>
		public float GetCurrentFontSize()
		{
			return HPDF_Page_GetCurrentFontSize(handle);
		}
		/// <summary>
		/// Get the current transformation matrix of the page
		/// For a complete description of matrix <see href="https://forum.patagames.com/posts/t501-What-Is-Transformation-Matrix-and-How-to-Use-It"/>
		/// </summary>
		/// <returns></returns>
		public HPDFTransformationMatrixStruct GetTransMatrix()
		{
			return HPDF_Page_GetTransMatrix(handle);
		}
		/// <summary>
		/// Get the current line width of the page
		/// </summary>
		/// <returns>the current line width for path painting of the page or . Otherwise it returns <see cref="HPDFClass.HPDF_DEF_LINEWIDTH"/></returns>
		public float GetLineWidth()
		{
			return HPDF_Page_GetLineWidth(handle);
		}
		/// <summary>
		/// Set the width of the line used to stroke a path.
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>
		/// </summary>
		/// <param name="line_width">Line width to use</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetLineWidth(float line_width)
		{
			LastError = HPDF_Page_SetLineWidth(handle, line_width);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the current line end style of the page
		/// </summary>
		/// <returns>The current line cap style of the page, <see cref="HPDFLineEndShapes.Butt"/> otherwise</returns>
		public HPDFLineEndShapes GetLineEndShape()
		{
			return HPDF_Page_GetLineCap(handle);
		}
		/// <summary>
		/// Set the shape to be used at the ends of line
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>
		/// </summary>
		/// <param name="line_cap">Shape to use</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetLineEndShape(HPDFLineEndShapes line_cap)
		{
			LastError = HPDF_Page_SetLineCap(handle, line_cap);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// get the current line join style of the page
		/// </summary>
		/// <returns>The current line join style of the page, <see cref="HPDFLineJoins.Miter"/> otherwise</returns>
		public HPDFLineJoins GetLineJoin()
		{
			return HPDF_Page_GetLineJoin(handle);
		}
		/// <summary>
		/// Set the line join style in the page
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>
		/// </summary>
		/// <param name="line_join">Line join</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetLineJoin(HPDFLineJoins line_join)
		{
			LastError = HPDF_Page_SetLineJoin(handle, line_join);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the current value of the page's miter limit
		/// </summary>
		/// <returns>The current value of the page's miter limit, <see cref="HPDFClass.HPDF_DEF_MITERLIMIT"/> otherwise</returns>
		public float GetMiterLimit()
		{
			return HPDF_Page_GetMiterLimit(handle);
		}
		/// <summary>
		/// Set the miter limit
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>
		/// </summary>
		/// <param name="miter_limit">Limit to use, it must be over <see cref="HPDFClass.HPDF_MIN_MITERLIMIT"/></param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetMiterLimit(float miter_limit)
		{
			LastError = HPDF_Page_SetMiterLimit(handle, miter_limit);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the current dash pattern of the page
		/// </summary>
		/// <returns>A <see cref="HPDFDashModeStruct"/> object</returns>
		public HPDFDashModeStruct GetDash()
		{
			HPDFDashModeStructInternalStruct mode1 = HPDF_Page_GetDash(handle);
			HPDFDashModeStruct mode2;

			mode2.phase = mode1.phase;
			mode2.ptn = new ushort[mode1.num_ptn];
			if (mode1.num_ptn >= 1) mode2.ptn[0] = mode1.ptn0;
			if (mode1.num_ptn >= 2) mode2.ptn[1] = mode1.ptn1;
			if (mode1.num_ptn >= 3) mode2.ptn[2] = mode1.ptn2;
			if (mode1.num_ptn >= 4) mode2.ptn[3] = mode1.ptn3;
			if (mode1.num_ptn >= 5) mode2.ptn[4] = mode1.ptn4;
			if (mode1.num_ptn >= 6) mode2.ptn[5] = mode1.ptn5;
			if (mode1.num_ptn >= 7) mode2.ptn[6] = mode1.ptn6;
			if (mode1.num_ptn >= 8) mode2.ptn[7] = mode1.ptn7;
			return mode2;
		}
		/// <summary>
		/// Sets the line dash pattern in the page
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>
		/// The <paramref name="dash_ptn"/> describes {#elem ON [, #elem off [, #elem ON [,#elem OFF ...]]]}, with a maximum of 8 elements
		/// - if null or zero element specified then no dash, solid line "---------..."
		/// - if 1 element is specified then it will be used to set the length of the ON dash and the OFF dash,...
		///   {3} "---   ---   ---..."
		/// - if 2 elements are specified the 1st is the ON dash, the 2nd is the OFF dash
		///   {3, 1} "--- --- ---..."
		/// - if 4 or more elements (number must always be even) every odd element is the ON dash, even OFF dash
		///   {3, 1, 5, 2} "--- -----  --- -----  ---..."
		/// The <paramref name="phase"/> indicates how many positioins to discard before starting drawing the dash line indicated by <paramref name="dash_ptn"/>
		///   {3} phase=0 (discard no point before drawing) "---   ---   ---..."
		///   {3} phase=2 (discard 2 points before drawing) "-   ---   ---..."
		///   {3, 1} phase=3 (discard 3 points before drawing) "   ---   ---..."
		///   {3, 1} phase=6 (discard 6 points before drawing) "-   ---   ---..."
		///   {3, 1} phase=7 (discard 7 points before drawing) "   ---   ---..."
		///   {3, 1, 5, 2} phase=15 (discard 7 points before drawing) "-----  --- -----  ---..."
		/// </summary>
		/// <param name="dash_ptn">Describe the dash pattern and gaps used to stroke paths; this value MUST either null or be a set value 1 or an even number of values (1, 2, 4, 6, 8,...)</param>
		/// <param name="phase">The phase to use to draw the dash line</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetDash(ushort[] dash_ptn, uint phase)
		{
			if (dash_ptn == null)
				LastError = HPDF_Page_SetDash(handle, dash_ptn, 0, 0);
			else
				LastError = HPDF_Page_SetDash(handle, dash_ptn, (uint)dash_ptn.Length, phase);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// get the current value of the page's flatness
		/// </summary>
		/// <returns>The current value of the page's flatness, <see cref="HPDFClass.HPDF_DEF_FLATNESS"/> otherwise</returns>
		public float GetFlat()
		{
			return HPDF_Page_GetFlat(handle);
		}
		/// <summary>
		/// Set page's flatness
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>
		/// </summary>
		/// <param name="flatness">Flatness to use, between 0 and 99 included</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetFlat(float flatness)
		{
			LastError = HPDF_Page_SetFlat(handle, flatness);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// The current value of the page's character spacing
		/// </summary>
		/// <returns>The current value of the page's character spacing, 0 otherwise</returns>
		public float GetCharSpace()
		{
			return HPDF_Page_GetCharSpace(handle);
		}
		/// <summary>
		/// Set the character spacing for text showing. The initial value of character spacing is 0, values are betwwen <see cref="HPDFClass.HPDF_MIN_CHARSPACE"/> and <see cref="HPDFClass.HPDF_MAX_CHARSPACE"/> included.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="value">Value to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetCharSpace(float value)
		{
			LastError = HPDF_Page_SetCharSpace(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// The current value of the page's word spacing
		/// </summary>
		/// <returns>The current value of the page's word spacing, 0 otherwise</returns>
		public float GetWordSpace()
		{
			return HPDF_Page_GetWordSpace(handle);
		}
		/// <summary>
		/// Set the  word spacing for text showing. The initial value of word spacing is 0, values are betwwen <see cref="HPDFClass.HPDF_MIN_WORDSPACE"/> and <see cref="HPDFClass.HPDF_MAX_WORDSPACE"/> included.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="value">Value to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetWordSpace(float value)
		{
			LastError = HPDF_Page_SetWordSpace(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the current value of the page's horizontal scalling for text showing
		/// </summary>
		/// <returns>The current value of the page's horizontal scalling, <see cref="HPDFClass.HPDF_DEF_HSCALING"/> otherwise</returns>
		public float GetHorizontalScalling()
		{
			return HPDF_Page_GetHorizontalScalling(handle);
		}
		/// <summary>
		/// The current value of the page's line spacing
		/// </summary>
		/// <returns>The current value of the page's line spacing, 0 otherwise</returns>
		public float GetTextLeading()
		{
			return HPDF_Page_GetTextLeading(handle);
		}
		/// <summary>
		/// The current value of the page's text rendering mode
		/// </summary>
		/// <returns>The current value of the text rendering mode, 0 otherwise</returns>
		public HPDFTextRenderingModes GetTextRenderingMode()
		{
			return HPDF_Page_GetTextRenderingMode(handle);
		}
		/// <summary>
		/// The current value of the page's text rising
		/// </summary>
		/// <returns>The current value of the page's text rising, 0 otherwise</returns>
		public float GetTextRaise()
		{
			return HPDF_Page_GetTextRaise(handle);
		}
		/// <summary>
		/// The current value of the page's filling color, valid only when the page's filling color space is <see cref="HPDFColorSpaces.DeviceRGB"/>
		/// </summary>
		/// <returns>The current <see cref="HPDFRGBColorStruct"/> of the page's filling color, {0, 0, 0} otherwise</returns>
		public HPDFRGBColorStruct GetRGBFill()
		{
			return HPDF_Page_GetRGBFill(handle);
		}
		/// <summary>
		/// The current value of the page's color stroke (line around), valid only when the page's filling color space is <see cref="HPDFColorSpaces.DeviceRGB"/>
		/// </summary>
		/// <returns>The current <see cref="HPDFRGBColorStruct"/> of the page's color stroke, {0, 0, 0} otherwise</returns>
		public HPDFRGBColorStruct GetRGBStroke()
		{
			return HPDF_Page_GetRGBStroke(handle);
		}
		/// <summary>
		/// The current value of the page's filling color, valid only when the page's filling color space is <see cref="HPDFColorSpaces.DeviceCMYK"/>
		/// </summary>
		/// <returns>The current <see cref="HPDFCMYKColorStruct"/> of the page's filling color, {0, 0, 0} otherwise</returns>
		public HPDFCMYKColorStruct GetCMYKFill()
		{
			return HPDF_Page_GetCMYKFill(handle);
		}
		/// <summary>
		/// Set the filling color.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="color">CMYK color to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetCMYKFill(HPDFCMYKColorStruct color)
		{
			LastError = HPDF_Page_SetCMYKFill(handle, color.C, color.M, color.Y, color.K);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// The current value of the page's color stroke (line around), valid only when the page's filling color space is <see cref="HPDFColorSpaces.DeviceCMYK"/>
		/// </summary>
		/// <returns>The current <see cref="HPDFRGBColorStruct"/> of the page's color stroke, {0, 0, 0} otherwise</returns>
		public HPDFCMYKColorStruct GetCMYKStroke()
		{
			return HPDF_Page_GetCMYKStroke(handle);
		}
		/// <summary>
		/// The current value of the page's filling color, valid only when the page's filling color space is <see cref="HPDFColorSpaces.DeviceGray"/>
		/// </summary>
		/// <returns>The current value of the page's filling color, 0 otherwise</returns>
		public float GetGrayFill()
		{
			return HPDF_Page_GetGrayFill(handle);
		}
		/// <summary>
		/// Set the filling color.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="gray">The value of the gray level between 0 and 1 included</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetGrayFill(float gray)
		{
			LastError = HPDF_Page_SetGrayFill(handle, gray);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// The current value of the page's color stroke (line around), valid only when the page's filling color space is <see cref="HPDFColorSpaces.DeviceGray"/>
		/// </summary>
		/// <returns>The current value of the page's color stroke, 0 otherwise</returns>
		public float GetGrayStroke()
		{
			return HPDF_Page_GetGrayStroke(handle);
		}
		/// <summary>
		/// Set the stroke color.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="gray">The value of the gray level between 0 and 1 included</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetGrayStroke(float gray)
		{
			LastError = HPDF_Page_SetGrayStroke(handle, gray);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the filling color.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="color">RGB color to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetRGBFill(HPDFRGBColorStruct color)
		{
			LastError = HPDF_Page_SetRGBFill(handle, color.R, color.G, color.B);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the stroke color.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="color">RGB color to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetRGBStroke(HPDFRGBColorStruct color)
		{
			LastError = HPDF_Page_SetRGBStroke(handle, color.R, color.G, color.B);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// The current value of the page's stroking color space
		/// </summary>
		/// <returns>The <see cref="HPDFColorSpaces"/> of the page,  <see cref="HPDFColorSpaces._eof"/> otherwise</returns>
		public HPDFColorSpaces GetStrokingColorSpace()
		{
			return HPDF_Page_GetStrokingColorSpace(handle);
		}
		/// <summary>
		/// The current value of the page's filling color space
		/// </summary>
		/// <returns>The <see cref="HPDFColorSpaces"/> of the page,  <see cref="HPDFColorSpaces._eof"/> otherwise</returns>
		public HPDFColorSpaces GetFillingColorSpace()
		{
			return HPDF_Page_GetFillingColorSpace(handle);
		}
		/// <summary>
		/// Get the current text transformation matrix of the page
		/// For a complete description of matrix <see href="https://forum.patagames.com/posts/t501-What-Is-Transformation-Matrix-and-How-to-Use-It"/>
		/// </summary>
		/// <returns>A <see cref="HPDFTransformationMatrixStruct"/> object</returns>
		public HPDFTransformationMatrixStruct GetTextMatrix()
		{
			return HPDF_Page_GetTextMatrix(handle);
		}
		/// <summary>
		/// Get the number of the page's graphics state stack
		/// </summary>
		/// <returns>The number of the page's graphics state stack, 0 otherwise</returns>
		public uint GetGraphicStateDepth()
		{
			return HPDF_Page_GetGStateDepth(handle);
		}
		/// <summary>
		/// Applies the graphics state to the page
		/// An application can set it when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/>
		/// </summary>
		/// <param name="gstate">Extended graphics state object</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetExtGState(HPDFExtGState gstate)
		{
			try
			{
				LastError = HPDF_Page_SetExtGState(handle, gstate.GetHandle());
				return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
			}
			catch (Exception) { }
			return false;
		}
		/// <summary>
		/// Saves the page's current graphics parameter to the stack.
		/// Restoring these parameters is done using <see cref="GRestore"/>.
		/// An application can invoke it only if the page is in <see cref="HPDFGraphicModes.PageDescription"/> and saves the following parameters:
		/// - Transformation Matrix
		/// - Line Width
		/// - Line Cap Style
		/// - Line Join Style
		/// - Miter Limit
		/// - Dash Mode
		/// - Flatness
		/// - Character Spacing
		/// - Word Spacing
		/// - Horizontal Scalling
		/// - Text Leading
		/// - Rendering Mode
		/// - Text Rise
		/// - Filling Color (RGB, CMYK and Gray)
		/// - Stroking Color (RGB, CMYK and Gray)
		/// - Font
		/// - Font Size
		/// - Writing mode
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool GSave()
		{
			LastError = HPDF_Page_GSave(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Restore the graphics state which is saved by <see cref="GSave"/>.
		/// An application can invoke it only if the page is in <see cref="HPDFGraphicModes.PageDescription"/> and restores the following parameters:
		/// - Transformation Matrix
		/// - Line Width
		/// - Line Cap Style
		/// - Line Join Style
		/// - Miter Limit
		/// - Dash Mode
		/// - Flatness
		/// - Character Spacing
		/// - Word Spacing
		/// - Horizontal Scalling
		/// - Text Leading
		/// - Rendering Mode
		/// - Text Rise
		/// - Filling Color (RGB, CMYK and Gray)
		/// - Stroking Color (RGB, CMYK and Gray)
		/// - Font
		/// - Font Size
		/// - Writing mode
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool GRestore()
		{
			LastError = HPDF_Page_GRestore(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		///  Concatenate the page's current transformation matrix and specified matrix.
		///  For example, if you want to rotate the coordinate system of the page by 45 degrees, use <see cref="Concat(float, float, float, float, float, float)"/> as follows:
		///    float rad1 = 45 / 180 * 3.141592;
		///    page.Concat(cos(rad1), sin(rad1), -sin(rad1), cos(rad1), 220, 350);
		///  Additionally to change the coordinate system of the page to 300dpi, use <see cref="Concat(float, float, float, float, float, float)"/> as follows:
		///    page.Concat(72.0f / 300.0f, 0, 0, 72.0f / 300.0f, 0, 0);
		///  Invoke <see cref="GSave"/> before invoking <see cref="Concat(float, float, float, float, float, float)"/>, then the modifications can be restored by invoking <see cref="GRestore"/>
		///    /* save the current graphics states */
		///    page.GSave();
		///    /* concatenate the transformation matrix */
		///    page.Concat(72.0f / 300.0f, 0, 0, 72.0f / 300.0f, 0, 0);
		///    /* show text on the translated coordinates */
		///    page.BeginText();
		///    page.MoveTextPos(50, 100);
		///    page.ShowText("Text on the translated coordinates");
		///    page.EndText();
		///    /* restore the graphics states */
		///    page.GRestore();
		/// For a complete description of matrix <see href="https://forum.patagames.com/posts/t501-What-Is-Transformation-Matrix-and-How-to-Use-It"/>
		/// </summary>
		/// <param name="a">The transformation matrix a data to concatenate</param>
		/// <param name="b">The transformation matrix b data to concatenate</param>
		/// <param name="c">The transformation matrix c data to concatenate</param>
		/// <param name="d">The transformation matrix d data to concatenate</param>
		/// <param name="x">The transformation matrix x data to concatenate</param>
		/// <param name="y">The transformation matrix y data to concatenate</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Concat(float a, float b, float c, float d, float x, float y)
		{
			LastError = HPDF_Page_Concat(handle, a, b, c, d, x, y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Start a new subpath and move the current point for drawing path,; set the start point for the path to the point (x, y) and change the graphics mode to <see cref="HPDFGraphicModes.PathObject"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="ending_point">The ending point of the drawing path</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool MoveTo(HPDFPointStruct ending_point)
		{
			LastError = HPDF_Page_MoveTo(handle, ending_point.x, ending_point.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Append a path from the current point to the specified point.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="ending_point">The ending point of the path</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool LineTo(HPDFPointStruct ending_point)
		{
			LastError = HPDF_Page_LineTo(handle, ending_point.x, ending_point.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Append a Bézier curve to the current path using two spesified points.
		/// The <paramref name="ctrl_on_starting_point"/> and the <paramref name="ctrl_on_ending_point"/> are used as the control points for a Bézier curve and current point is moved to the <paramref name="ending_point"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="ctrl_on_starting_point">Control point applying to the starting point for a Bézier curve.</param>
		/// <param name="ctrl_on_ending_point">Control point applying to the ending point for a Bézier curve.</param>
		/// <param name="ending_point">The ending point</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool CurveTo(HPDFPointStruct ctrl_on_starting_point, HPDFPointStruct ctrl_on_ending_point, HPDFPointStruct ending_point)
		{
			LastError = HPDF_Page_CurveTo(handle, ctrl_on_starting_point.x, ctrl_on_starting_point.y, ctrl_on_ending_point.x, ctrl_on_ending_point.y, ending_point.x, ending_point.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Append a Bézier curve to the current path using two spesified points.
		/// The <paramref name="ctrl_on_ending_point"/> is used as the control points for a Bézier curve and current point is moved to the <paramref name="ending_point"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="ctrl_on_ending_point">Control point applying to the ending point for a Bézier curve.</param>
		/// <param name="ending_point">The ending point</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool CurveOnEndingPoint(HPDFPointStruct ctrl_on_ending_point, HPDFPointStruct ending_point)
		{
			LastError = HPDF_Page_CurveTo2(handle, ctrl_on_ending_point.x, ctrl_on_ending_point.y, ending_point.x, ending_point.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Append a Bézier curve to the current path using two spesified points.
		/// The <paramref name="ctrl_on_starting_point"/> is used as the control points for a Bézier curve and current point is moved to the <paramref name="ending_point"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="ctrl_on_starting_point">Control point applying to the ending point for a Bézier curve.</param>
		/// <param name="ending_point">The ending point</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool CurveOnStartingPoint(HPDFPointStruct ctrl_on_starting_point, HPDFPointStruct ending_point)
		{
			LastError = HPDF_Page_CurveTo2(handle, ctrl_on_starting_point.x, ctrl_on_starting_point.y, ending_point.x, ending_point.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Appends a strait line from the current point to the start point of sub path. The current point is moved to the start point of sub path.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ClosePath()
		{
			LastError = HPDF_Page_ClosePath(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Append a rectangle to the current path and change the graphics mode to <see cref="HPDFGraphicModes.PathObject"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="lower_left_point_of_rectangle">The lower-left point of the rectangle</param>
		/// <param name="size">The size of the rectangle</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Rectangle(HPDFPointStruct lower_left_point_of_rectangle, HPDFSizeStruct size)
		{
			LastError = HPDF_Page_Rectangle(handle, lower_left_point_of_rectangle.x, lower_left_point_of_rectangle.y, size.Width, size.Height);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Paint the current path and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Stroke()
		{
			LastError = HPDF_Page_Stroke(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Close the current path, then it paints the path and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ClosePathStroke()
		{
			LastError = HPDF_Page_ClosePathStroke(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Fill the current path using the nonzero winding number rule and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Fill()
		{
			LastError = HPDF_Page_Fill(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Fill the current path using the even-odd rule and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Eofill()
		{
			LastError = HPDF_Page_Eofill(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Fill the current path using the nonzero winding number rule, then paint the path and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool FillStroke()
		{
			LastError = HPDF_Page_FillStroke(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Fill the current path using the even-odd rule, then paint the path and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool EofillStroke()
		{
			LastError = HPDF_Page_EofillStroke(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Close the current path, fills the current path using the nonzero winding number rule, then it paints the path and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ClosePathFillStroke()
		{
			LastError = HPDF_Page_ClosePathFillStroke(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Close the current path, fills the current path using the even-odd rule, then it paints the path and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ClosePathEofillStroke()
		{
			LastError = HPDF_Page_ClosePathEofillStroke(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// End the path object without filling and painting operation and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.ClippingPath"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool EndPath()
		{
			LastError = HPDF_Page_EndPath(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Clip (lines inside) the path object using the nonzero winding number rule and change the graphics mode to <see cref="HPDFGraphicModes.ClippingPath"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Clip()
		{
			LastError = HPDF_Page_Clip(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Clip (lines inside) the path object using the even-odd rule and change the graphics mode to <see cref="HPDFGraphicModes.ClippingPath"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Eoclip()
		{
			LastError = HPDF_Page_Eoclip(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Begin a text object and sets the current text position to the point (0, 0) and change the graphics mode to <see cref="HPDFGraphicModes.TextObject"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool BeginText()
		{
			LastError = HPDF_Page_BeginText(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// End a text object and change the graphics mode to <see cref="HPDFGraphicModes.PageDescription"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool EndText()
		{
			LastError = HPDF_Page_EndText(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the word spacing for text showing. The initial value of word spacing is 100, values are betwwen <see cref="HPDFClass.HPDF_MIN_HSCALING"/> and <see cref="HPDFClass.HPDF_MAX_HSCALING"/> included.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="value">Value to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetHorizontalScalling(float value)
		{
			LastError = HPDF_Page_SetHorizontalScalling(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the text leading (line spacing) for text showing. The initial value of leading is 0.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="value">Value to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetTextLeading(float value)
		{
			LastError = HPDF_Page_SetTextLeading(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the text leading (line spacing) for text showing using the current font size.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool AdjustTextLeading()
		{
			if (null == CurrentFont || 0 == CurrentFontSize)
				LastError = (uint)HPDFErrors.HPDF_INVALID_FONT;
			else
				TextLeading = CurrentFontSize;
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the word spacing for text showing. The initial value of word spacing is 100, values are between 1 and <see cref="HPDFClass.HPDF_MAX_FONTSIZE"/> included.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="size">Size of the font</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFont(HPDFFont font, float size)
		{
			if (null == font)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_FONT;
				return false;
			}
			LastError = HPDF_Page_SetFontAndSize(handle, font.GetHandle(), size);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the text rendering mode. The initial value of word spacing is <see cref="HPDFTextRenderingModes.Fill"/>, values are between 1 and <see cref="HPDFClass.HPDF_MAX_FONTSIZE"/> included.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="mode">Rendering mode to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetTextRenderingMode(HPDFTextRenderingModes mode)
		{
			LastError = HPDF_Page_SetTextRenderingMode(handle, mode);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set text raise.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="value">Value to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetTextRaise(float value)
		{
			LastError = HPDF_Page_SetTextRaise(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line using specified offset values.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1 + <paramref name="new_position"/>.x, y1 + <paramref name="new_position"/>.y). 
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="new_position">New position to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool MoveTextPos(HPDFPointStruct new_position)
		{
			LastError = HPDF_Page_MoveTextPos(handle, new_position.x, new_position.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line using specified offset values.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1 + <paramref name="x"/>, y1 + <paramref name="x"/>). 
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="x">X offset to use</param>
		/// <param name="y">Y offset to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool MoveTextPos(float x, float y)
		{
			LastError = HPDF_Page_MoveTextPos(handle, x, y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line using specified offset values, and sets the text leading.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1 + <paramref name="new_position"/>.x, y1 + <paramref name="new_position"/>.y).
		/// The new <see cref="TextLeading"/> value becomes -<paramref name="new_position"/>.y.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="new_position">New position to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool MoveTextPosAndChangeTextLeading(HPDFPointStruct new_position)
		{
			LastError = HPDF_Page_MoveTextPos2(handle, new_position.x, new_position.y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line using specified offset values, and sets the text leading.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1 + <paramref name="x"/>, y1 + <paramref name="y"/>).
		/// The new <see cref="TextLeading"/> value becomes -<paramref name="y"/>.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="x">X offset to use</param>
		/// <param name="y">Y offset to use</param>		
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool MoveTextPosAndChangeTextLeading(float x, float y)
		{
			LastError = HPDF_Page_MoveTextPos2(handle, x, y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set text matrix.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// For a complete description of matrix <see href="https://forum.patagames.com/posts/t501-What-Is-Transformation-Matrix-and-How-to-Use-It"/>
		/// </summary>
		/// <param name="a">New a text matrix</param>
		/// <param name="b">New b text matrix</param>
		/// <param name="c">New c text matrix</param>
		/// <param name="d">New d text matrix</param>
		/// <param name="x">New x text matrix</param>
		/// <param name="y">New y text matrix</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetTextMatrix(float a, float b, float c, float d, float x, float y)
		{
			LastError = HPDF_Page_SetTextMatrix(handle, a, b, c, d, x, y);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1, y1 - text leading).
		/// Since the default value of text leading is 0, an application has to set <see cref="TextLeading"/> before calling this function.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool MoveToNextLine()
		{
			LastError = HPDF_Page_MoveToNextLine(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Print the text at the current position on the page.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="text">Text to print</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ShowText(string text)
		{
			LastError = HPDF_Page_ShowText(handle, text);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line, then prints the text at the current position on the page.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1, y1 - text leading).
		/// Since the default value of text leading is 0, an application has to set <see cref="TextLeading"/> before calling this function.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="text">Text to print</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ShowTextNextLine(string text)
		{
			LastError = HPDF_Page_ShowTextNextLine(handle, text);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Move the current text position to the start of the next line, then sets the word spacing, character spacing and prints the text at the current position on the page.
		/// If the start position of the current line is (x1, y1), the start of the next line is (x1, y1 - text leading).
		/// Since the default value of text leading is 0, an application has to set <see cref="TextLeading"/> before calling this function.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="text">Text to print</param>
		/// <param name="char_space">Character spacing to use</param>
		/// <param name="word_space">Word spacing to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ShowTextNextLine(float word_space, float char_space, string text)
		{
			LastError = HPDF_Page_ShowTextNextLineEx(handle, word_space, char_space, text);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Print the text at the specified position on the page (unlike <see cref="ShowText(string)"/> there's no offset from the current position that is used).
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="point">The point to start printing the text</param>
		/// <param name="text">Text to print</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ShowTextAt(string text, HPDFPointStruct point)
		{
			LastError = HPDF_Page_TextOut(handle, point.x, point.y, text);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Print the text inside the specified region. The characters which is not used.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="rect">The rectangle inside which to print the text</param>
		/// <param name="text">Text to print</param>
		/// <param name="align"><see cref="HPDFTextAlignments"/></param>
		/// <param name="len">The number of characters which were printed in the area</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool ShowTextRect(HPDFRectStruct rect, string text, HPDFTextAlignments align, out uint len)
		{
			len = 0;
			if (AdjustTextLeading())
			{
				LastError = HPDF_Page_TextRect(handle, rect.Left, rect.Top, rect.Right, rect.Bottom, text, align, ref len);
			}
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the stroke color.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.TextObject"/>.
		/// </summary>
		/// <param name="color">RGB color to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetCMYKStroke(HPDFCMYKColorStruct color)
		{
			LastError = HPDF_Page_SetCMYKStroke(handle, color.C, color.M, color.Y, color.K);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}

		public bool ExecuteXObject(HPDFImage xobj)
		{
			if (null == xobj)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_IMAGE;
				return false;
			}
			LastError = HPDF_Page_ExecuteXObject(handle, xobj.GetHandle());
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Paint an image in one operation.
		/// </summary>
		/// <param name="image">Image to draw</param>
		/// <param name="lower_left_point">The lower-left point of the region where image is displayed</param>
		/// <param name="size">Size of image to display</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool DrawImage(HPDFImage image, HPDFPointStruct lower_left_point, HPDFSizeStruct size)
		{
			if (null == image)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_IMAGE;
				return false;
			}
			LastError = HPDF_Page_DrawImage(handle, image.GetHandle(), lower_left_point.x, lower_left_point.y, size.Width, size.Height);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Dran an circle.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="point">The center point of the circle</param>
		/// <param name="ray">Ray of the circle</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Circle(HPDFPointStruct point, float ray)
		{
			LastError = HPDF_Page_Circle(handle, point.x, point.y, ray);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Draw an arc.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="point">The center point of the circle</param>
		/// <param name="ray">Ray of the circle</param>
		/// <param name="ang1">Angle of the beginning of the arc</param>
		/// <param name="ang2">Angle of the end of the arc, it MUST be greater than <paramref name="ang1"/></param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Arc(HPDFPointStruct point, float ray, float ang1, float ang2)
		{
			if (ang2 <= ang1)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_PARAMETER;
				return false;
			}
			LastError = HPDF_Page_Arc(handle, point.x, point.y, ray, ang1, ang2);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Draw an ellipse.
		/// An application can invoke this function when the graphics mode of the page is in <see cref="HPDFGraphicModes.PageDescription"/> or <see cref="HPDFGraphicModes.PathObject"/>.
		/// </summary>
		/// <param name="point">The center point of the circle</param>
		/// <param name="xray">Ray of the ellipse on X axis</param>
		/// <param name="yray">Ray of the ellipse on Y axis</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool Ellipse(HPDFPointStruct point, float xray, float yray)
		{
			LastError = HPDF_Page_Ellipse(handle, point.x, point.y, xray, yray);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Configures the setting for slide transition of the page.
		/// </summary>
		/// <param name="type">Type of transition</param>
		/// <param name="disp_time">Display duration of the page in seconds. It must be greater than 0.</param>
		/// <param name="trans_time">Duration of the transition effect in seconds. It must be greater than 0. Default value is 1.</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetSlideShow(HPDFTransitionStyles type, float disp_time, float trans_time)
		{
			LastError = HPDF_Page_SetSlideShow(handle, type, disp_time, trans_time);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get object handle
		/// </summary>
		/// <returns>Handle of the underlying object</returns>
		public IntPtr GetHandle()
		{
			return handle;
		}
		#endregion
	}
}

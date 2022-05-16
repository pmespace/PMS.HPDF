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
	/// A fonc object
	/// </summary>
	public class HPDFFont : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Font_GetFontName(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Font_GetEncodingName(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern int HPDF_Font_GetUnicodeWidth(IntPtr handle, ushort code);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFRectStruct HPDF_Font_GetBBox(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern int HPDF_Font_GetAscent(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern int HPDF_Font_GetDescent(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Font_GetXHeight(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Font_GetCapHeight(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFTextWidthStruct HPDF_Font_TextWidth(IntPtr handle, string text, uint len);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Font_MeasureText(IntPtr handle, string text, uint len, float width, float font_size, float char_space, float word_space, int wordwrap, ref float real_width);
		#endregion

		#region properties
		IntPtr handle;
		public string Name { get => GetFontName(); }
		public string Encoding { get => GetEncodingName(); }
		#endregion

		#region  constructor
		public HPDFFont(IntPtr h)
		{
			if (h == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingFont);
			}
			this.handle = h;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Get object handle
		/// </summary>
		/// <returns>Handle of the underlying object</returns>
		public IntPtr GetHandle()
		{
			return handle;
		}
		/// <summary>
		/// Get the name of the font.
		/// </summary>
		/// <returns>The name of the font or null</returns>
		public string GetFontName()
		{
			IntPtr s = HPDF_Font_GetFontName(handle);
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		/// Get the encoding name of the font.
		/// </summary>
		/// <returns>The encoding name of the font or null</returns>
		public string GetEncodingName()
		{
			IntPtr s = HPDF_Font_GetEncodingName(handle);
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		/// Get the width of a charactor in the font.
		/// </summary>
		/// <param name="code">Unicode value of the character to measure</param>
		/// <returns>The width of the character or 0</returns>
		public int GetUnicodeCharacterWidth(ushort code)
		{
			return HPDF_Font_GetUnicodeWidth(handle, code);
		}
		/// <summary>
		/// Get the actual width of a charactor according to the page's current font size.
		/// </summary>
		/// <param name="code">Unicode value of the character to measure</param>
		/// <param name="page">Page to search font size from </param>
		/// <returns>The actual width of the character or 0F</returns>
		public float GetUnicodeCharacterActualWidth(ushort code, HPDFPage page)
		{
			return GetUnicodeCharacterActualWidth(code, (null != page ? page.GetCurrentFontSize() : 0F));
		}
		/// <summary>
		/// Get the actual width of a charactor according to the indicated font size.
		/// </summary>
		/// <param name="code">Unicode value of the character to measure</param>
		/// <param name="font_size">Font size </param>
		/// <returns>The actual width of the character or 0F</returns>
		public float GetUnicodeCharacterActualWidth(ushort code, float font_size)
		{
			if (0F != font_size)
				return GetUnicodeCharacterWidth(code) * font_size / 1000;
			return 0F;
		}
		/// <summary>
		/// Get the bounding box of the font.
		/// </summary>
		/// <returns>Font bounding box or a null box struct {0,0,0,0} (<see cref="HPDFRectStruct.IsNull"/>==true)</returns>
		public HPDFRectStruct GetBBox()
		{
			return HPDF_Font_GetBBox(handle);
		}
		/// <summary>
		/// Get the vertical ascent of the font.
		/// Ascent is the distance between the baseline and the top of the glyph (including diacritical marks) that reaches farthest from the baseline.
		/// Ascent goes with lowercase letters.
		/// <see href="https://stackoverflow.com/questions/42320887/hpdf-units-for-text-width-and-height"/>
		/// </summary>
		/// <returns>Vertical ascent of the font or 0</returns>
		public int GetAscent()
		{
			return HPDF_Font_GetAscent(handle);
		}
		/// <summary>
		/// Descent is the distance between the baseline and the lowest descending glyph (applies to f, g, j, p, q, y).
		/// descent goes with lowercase letters.
		/// <see href="https://stackoverflow.com/questions/42320887/hpdf-units-for-text-width-and-height"/>
		/// </summary>
		/// <returns>Vertical ascent of the font or 0</returns>
		public int GetDescent()
		{
			return HPDF_Font_GetDescent(handle);
		}
		/// <summary>
		/// Get the distance from the baseline to the mean line of lowercase letters (mean line is the top of a, c, e, g, i, j, m, n, o, p, q, r ,s u, v, w, x, y, z).
		/// <see href="https://stackoverflow.com/questions/42320887/hpdf-units-for-text-width-and-height"/>
		/// </summary>
		/// <returns>The XHeight of the font or 0</returns>
		public uint GetXHeight()
		{
			return HPDF_Font_GetXHeight(handle);
		}
		/// <summary>
		/// Get the distance from the baseline to the top of uppercase letters.
		/// <see href="https://stackoverflow.com/questions/42320887/hpdf-units-for-text-width-and-height"/>
		/// </summary>
		/// <returns>Capital height of the font or 0</returns>
		public uint GetCapHeight()
		{
			return HPDF_Font_GetCapHeight(handle);
		}
		/// <summary>
		/// Get the width of the text, number of charactors and number of the words
		/// </summary>
		/// <param name="text">Text to get width</param>
		/// <returns>Text width structure or a null structure (<see cref="HPDFTextWidthStruct.IsNull"/>==true)</returns>
		///// Note: do not use the <see cref="HPDFTextWidthStruct.numwords"/> as it may be unreliable, prefer using <see cref="HPDFTextWidthStruct.numspace"/>
		public HPDFTextWidthStruct GetTextWidth(string text)
		{
			return HPDF_Font_TextWidth(handle, text, (uint)text.Length);
		}
		/// <summary>
		/// Calculate the byte length which can be included within the specified width.
		/// </summary>
		/// <param name="text">Text that is used to calculate.</param>
		/// <param name="width">The width of the area to put the text.</param>
		/// <param name="font_size">The size of the font.</param>
		/// <param name="char_space">The character spacing.</param>
		/// <param name="word_space">The word spacing.</param>
		/// <param name="wordwrap">Indicate whether wrapping inside a word is allowed or not (<see cref="HPDFPage.GetNumberOfDisplayableCharacters(string, float, bool, ref float)"/>)</param>
		/// <param name="real_width">The real widths of the text</param>S
		/// <returns>The number of characters which can be included within the specified width in current fontsize, character spacing and word spacing or 0 if an error has occurred</returns>
		public uint GetNumberOfDisplayableCharacters(string text, float width, float font_size, float char_space, float word_space, int wordwrap, ref float real_width)
		{
			return HPDF_Font_MeasureText(handle, text, (uint)text.Length, width, font_size, char_space, word_space, wordwrap, ref real_width);
		}
		#endregion
	}
}

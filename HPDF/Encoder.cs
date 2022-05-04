﻿/*
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
	/// PDF encoder
	/// </summary>
	public class HPDFEncoder : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFEncoderTypes HPDF_Encoder_GetType(IntPtr hencoder);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFByteTypes HPDF_Encoder_GetByteType(IntPtr hencoder, string text, uint index);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern ushort HPDF_Encoder_GetUnicode(IntPtr hencoder, ushort code);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFWritingModes HPDF_Encoder_GetWritingMode(IntPtr hencoder);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Encoder_GetName(IntPtr hencoder);
		#endregion

		#region properties
		IntPtr hencoder;
		public string Name { get => GetName(); }
		#endregion

		#region constructor
		public HPDFEncoder(IntPtr hencoder)
		{
			if (hencoder == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingEncoder);
			}
			this.hencoder = hencoder;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Get object handle
		/// </summary>
		/// <returns>Handle of the underlying object</returns>
		public IntPtr GetHandle()
		{
			return hencoder;
		}
		/// <summary>
		/// Get Encoder type <see cref="HPDFEncoderTypes"/>
		/// </summary>
		/// <returns>The encoder type or <see cref="HPDFEncoderTypes.Unknown"/></returns>
		public HPDFEncoderTypes GetEncoderType()
		{
			return HPDF_Encoder_GetType(hencoder);
		}
		/// <summary>
		/// Get byte type <see cref="HPDFByteTypes"/>
		/// </summary>
		/// <returns>The byte type or <see cref="HPDFByteTypes.Unknown"/></returns>
		public HPDFByteTypes GetByteType(string text, uint index)
		{
			return HPDF_Encoder_GetByteType(hencoder, text, index);
		}
		/// <summary>
		/// Get encoded value of a unicode character
		/// </summary>
		/// <param name="code">Unicode character to encode</param>
		/// <returns>Encoded value of the unicode character or 0</returns>
		public ushort GetUnicode(ushort code)
		{
			return HPDF_Encoder_GetUnicode(hencoder, code);
		}
		/// <summary>
		/// Get writing mode <see cref="HPDFWritingModes"/>
		/// </summary>
		/// <returns>The writing mode or <see cref="HPDFWritingModes._eof"/></returns>
		public HPDFWritingModes GetWritingMode()
		{
			return HPDF_Encoder_GetWritingMode(hencoder);
		}
		/// <summary>
		/// Get encoder name
		/// </summary>
		/// <returns>Name of the encoder or null</returns>
		public string GetName()
		{
			IntPtr s = HPDF_Encoder_GetName(hencoder);
			return Marshal.PtrToStringAnsi(s);
		}
		#endregion
	}
}

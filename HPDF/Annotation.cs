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
	/// Define an annotation inside <see langword="abstract"/>page
	/// </summary>
	public class HPDFAnnotation : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_LinkAnnot_SetHighlightMode(IntPtr handle, HPDFAnnotationHighlightModes mode);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_LinkAnnot_SetBorderStyle(IntPtr handle, float width, ushort dash_on, ushort dash_off);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_TextAnnot_SetIcon(IntPtr handle, HPDFAnnotationIcons icon);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_TextAnnot_SetOpened(IntPtr handle, int opened);
		#endregion

		#region properties
		IntPtr handle;
		#endregion

		#region constructor
		public HPDFAnnotation(IntPtr h)
		{
			if (h== IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingAnnotation);
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
		/// Define the annotation apparearance when clicked on. This function can only be used with annotation links.
		/// </summary>
		/// <param name="mode"><see cref="HPDFAnnotationHighlightModes"/></param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetHighlightMode(HPDFAnnotationHighlightModes mode)
		{
			LastError = HPDF_LinkAnnot_SetHighlightMode(handle, mode);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the annotation apparearance when clicked on. This function can only be used with annotation links.
		/// </summary>
		/// <param name="width">The width of an annotation's border</param>
		/// <param name="dash_on">Number of elements for dash on</param>
		/// <param name="dash_off">Number of elements for dash off</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetBorderStyle(float width, ushort dash_on, ushort dash_off)
		{
			LastError = HPDF_LinkAnnot_SetBorderStyle(handle, width, dash_on, dash_off);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the icon for the annotation.
		/// </summary>
		/// <param name="icon">Icon to use</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetIcon(HPDFAnnotationIcons icon)
		{
			LastError = HPDF_TextAnnot_SetIcon(handle, icon);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define if the annotation is initially displayed open or not.
		/// </summary>
		/// <param name="opened">Initial state</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetOpened(bool opened)
		{
			LastError = HPDF_TextAnnot_SetOpened(handle, opened ? HPDF_TRUE : HPDF_FALSE);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		#endregion
	}
}

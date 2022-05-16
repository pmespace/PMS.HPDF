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
	/// A destination object specifying how the page is to be displayed.
	/// </summary>
	public class HPDFDestination : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetXYZ(IntPtr handle, float left, float top, float zoom);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFit(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFitH(IntPtr handle, float top);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFitV(IntPtr handle, float left);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFitR(IntPtr handle, float left, float bottom, float right, float top);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFitB(IntPtr handle);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFitBH(IntPtr handle, float top);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Destination_SetFitBV(IntPtr handle, float left);
		#endregion

		#region properties
		IntPtr handle;
		#endregion

		#region constructor
		public HPDFDestination(IntPtr h)
		{
			if (h == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingDestination);
			}
			this.handle = h;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Define the page apparearance
		/// </summary>
		/// <param name="left">Left position of the page</param>
		/// <param name="top">Top position of the page</param>
		/// <param name="zoom">Zoom factor, between 0.08 (8%) and 32 (320%)</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetXYZ(float left, float top, float zoom)
		{
			LastError = HPDF_Destination_SetXYZ(handle, left, top, zoom);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Définit the page appearance so as to fit in the window
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFit()
		{
			LastError = HPDF_Destination_SetFit(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the page appearance so as to fit horizontally in the window
		/// </summary>
		/// <param name="top">Top position of the page</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFitH(float top)
		{
			LastError = HPDF_Destination_SetFitH(handle, top);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the page appearance so as to fit vertically in the window
		/// </summary>
		/// <param name="left">Top position of the page</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFitV(float left)
		{
			LastError = HPDF_Destination_SetFitV(handle, left);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the page appearance so as to fit in the indicated rectangle
		/// </summary>
		/// <param name="rect">A rectangle to fit the page in</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFitR(HPDFRectStruct rect)
		{
			LastError = HPDF_Destination_SetFitR(handle, rect.Left, rect.Bottom, rect.Right, rect.Top);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the page appearance so as to fit in a box
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFitB()
		{
			LastError = HPDF_Destination_SetFitB(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the page appearance so as to fit horizontally in a box
		/// </summary>
		/// <param name="top">Top position of the page</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFitBH(float top)
		{
			LastError = HPDF_Destination_SetFitBH(handle, top);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the page appearance so as to fit vertically in a box
		/// </summary>
		/// <param name="left">Top position of the page</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetFitBV(float left)
		{
			LastError = HPDF_Destination_SetFitBV(handle, left);
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

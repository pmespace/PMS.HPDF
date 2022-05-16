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
	/// An object to operate an outline of a document.
	/// </summary>
	public class HPDFOutline : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Outline_SetOpened(IntPtr houtline, int opened);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Outline_SetDestination(IntPtr houtline, IntPtr hdest);
		#endregion

		#region properties
		IntPtr houtline;
		#endregion

		#region constructor
		public HPDFOutline(IntPtr h)
		{
			if (h == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingOutline);
			}
			this.houtline = h;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Get object handle
		/// </summary>
		/// <returns>Handle of the underlying object</returns>
		public IntPtr GetHandle()
		{
			return houtline;
		}
		/// <summary>
		/// Define if initially open when the outline is displayed for the first time.
		/// </summary>
		/// <param name="open">Initial state</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetOpened(bool open)
		{
			LastError = HPDF_Outline_SetOpened(houtline, open ? HPDF_TRUE : HPDF_FALSE);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the <see cref="HPDFDestination"/> to reach when the outline is clicked on.
		/// </summary>
		/// <param name="dest">Destination object</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetDestination(HPDFDestination dest)
		{
			if (null == dest)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_DESTINATION;
				return false;
			}
			LastError = HPDF_Outline_SetDestination(houtline, dest.GetHandle());
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		#endregion
	}
}

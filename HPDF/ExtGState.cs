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
using System.Runtime.InteropServices;
using System.Text;
using HPDF.Properties;
using System.IO;

namespace HPDF
{
	/// <summary>
	/// Extended graphics settings
	/// </summary>
	public class HPDFExtGState : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_ExtGState_SetAlphaStroke(IntPtr gstate, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_ExtGState_SetAlphaFill(IntPtr gstate, float value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_ExtGState_SetBlendMode(IntPtr gstate, HPDFBlendModes mode);
		#endregion

		#region properties
		IntPtr hgstate;
		#endregion

		#region constructor
		public HPDFExtGState(IntPtr hgstate)
		{
			if (hgstate == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingImage);
			}

			this.hgstate = hgstate;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Get object handle
		/// </summary>
		/// <returns>Handle of the underlying object</returns>
		public IntPtr GetHandle()
		{
			return hgstate;
		}
		/// <summary>
		/// Set the Alpha stroke
		/// </summary>
		/// <param name="value">Value to use, between 0 and 1 included</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetAlphaStroke(float value)
		{
			LastError = HPDF_ExtGState_SetAlphaStroke(hgstate, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the Alpha filling
		/// </summary>
		/// <param name="value">Value to use, between 0 and 1 included</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetAlphaFill(float value)
		{
			LastError = HPDF_ExtGState_SetAlphaFill(hgstate, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set blend mode
		/// </summary>
		/// <param name="mode"><see cref="HPDFBlendModes"/></param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetBlendMode(HPDFBlendModes mode)
		{
			LastError = HPDF_ExtGState_SetBlendMode(hgstate, mode);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		#endregion
	}
}

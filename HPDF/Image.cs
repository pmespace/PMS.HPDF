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
	/// Image class
	/// </summary>
	public class HPDFImage : HPDFClass
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFPointStruct HPDF_Image_GetSize(IntPtr image);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Image_GetWidth(IntPtr image);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Image_GetHeight(IntPtr image);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Image_GetBitsPerComponent(IntPtr image);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_Image_GetColorSpace(IntPtr image);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Image_SetColorMask(IntPtr image, uint rmin, uint rmax, uint gmin, uint gmax, uint bmin, uint bmax);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_Image_SetMaskImage(IntPtr image, IntPtr mask_image);
		#endregion

		#region properties
		IntPtr hobj;
		#endregion

		#region constructor
		public HPDFImage(IntPtr hobj)
		{
			if (hobj == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingImage);
			}
			this.hobj = hobj;
		}
		#endregion

		#region implementation
		/// <summary>
		/// Get object handle
		/// </summary>
		/// <returns>Handle of the underlying object</returns>
		public IntPtr GetHandle()
		{
			return hobj;
		}
		/// <summary>
		/// Get image size
		/// </summary>
		/// <returns>Image size or null size <see cref="HPDFPointStruct.IsNull"/>==true</returns>
		public HPDFPointStruct GetSize()
		{
			return HPDF_Image_GetSize(hobj);
		}
		/// <summary>
		/// Get image width
		/// </summary>
		/// <returns>Image width or 0</returns>
		public uint GetWidth()
		{
			return HPDF_Image_GetWidth(hobj);
		}
		/// <summary>
		/// Get image height
		/// </summary>
		/// <returns>Image height or 0</returns>
		public uint GetHeight()
		{
			return HPDF_Image_GetHeight(hobj);
		}
		/// <summary>
		/// Get the number of bytes used to describe each color component within the image
		/// </summary>
		/// <returns>Number of bytes defining the color or 0</returns>
		public uint GetBitsPerComponent()
		{
			return HPDF_Image_GetBitsPerComponent(hobj);
		}
		/// <summary>
		/// Get the name of the color space of the image. Possible values are:
		/// - DeviceGray
		/// - DeviceRGB
		/// - DeviceCMYK
		/// - Indexed
		/// </summary>
		/// <returns>The name of the color space or null</returns>
		public string GetColorSpace()
		{
			IntPtr s = HPDF_Image_GetColorSpace(hobj);
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		/// Define the transparency color of the image using RGB values. The color is displayed as transparent. The image color space must be RGB.
		/// </summary>
		/// <param name="red_min">Lower limit of red, between 0 and 255</param>
		/// <param name="red_max">Higher limit of red, between 0 and 255</param>
		/// <param name="green_min">Lower limit of green, between 0 and 255</param>
		/// <param name="green_max">Higher limit of green, between 0 and 255</param>
		/// <param name="blue_min">Lower limit of blue, between 0 and 255</param>
		/// <param name="blue_max">Higher limit of blue, between 0 and 255</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetColorMask(uint red_min, uint red_max, uint green_min, uint green_max, uint blue_min, uint blue_max)
		{
			LastError = HPDF_Image_SetColorMask(hobj, red_min, red_max, green_min, green_max, blue_min, blue_max);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Define the image to use as a mask. It must be a 1 byte gray image.
		/// </summary>
		/// <param name="mask_image">Image to use as a mask</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetMaskImage(HPDFImage mask_image)
		{
			if (null == mask_image)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_IMAGE;
				return false;
			}
			LastError = HPDF_Image_SetMaskImage(hobj, mask_image.GetHandle());
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		#endregion
	}
}

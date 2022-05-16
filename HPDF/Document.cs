/*
 * << Haru Free PDF Library 2.0.6 >> -- handle.cs
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
	/// A PDF document
	/// </summary>
	public class HPDFDocument : HPDFClass, IDisposable
	{
		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetVersion();

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_New(HPDFErrorHandler user_error_fn, IntPtr user_data);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern void HPDF_Free(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_NewDoc(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_FreeDoc(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_FreeDocAll(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_HasDoc(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SaveToFile(IntPtr pdf, StringBuilder file_name);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_GetError(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_GetErrorDetail(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern void HPDF_ResetError(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetPagesConfiguration(IntPtr pdf, uint page_per_pages);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetPageByIndex(IntPtr pdf, uint index);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFPageLayouts HPDF_GetPageLayout(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetPageLayout(IntPtr pdf, HPDFPageLayouts layout);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern HPDFPageModes HPDF_GetPageMode(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetPageMode(IntPtr pdf, HPDFPageModes mode);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetOpenAction(IntPtr pdf, IntPtr open_action);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_GetViewerPreference(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetViewerPreference(IntPtr pdf, uint value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetCurrentPage(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_AddPage(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_InsertPage(IntPtr pdf, IntPtr page);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetFont(IntPtr pdf, StringBuilder font_name, StringBuilder encoder_name);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadType1FontFromFile(IntPtr pdf, StringBuilder afmfilename, StringBuilder pfmfilename);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadTTFontFromFile(IntPtr pdf, StringBuilder file_name, int embedding);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadTTFontFromFile2(IntPtr pdf, StringBuilder file_name, uint index, int embedding);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_AddPageLabel(IntPtr pdf, uint page_num, HPDFPageNumberingStyles style, uint first_page, StringBuilder prefix);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseJPFonts(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseKRFonts(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseCNSFonts(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseCNTFonts(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_CreateOutline(IntPtr pdf, IntPtr parent, StringBuilder title, IntPtr encoder);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetEncoder(IntPtr pdf, StringBuilder encoder_name);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetCurrentEncoder(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetCurrentEncoder(IntPtr pdf, StringBuilder encoder_name);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseJPEncodings(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseKREncodings(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseCNSEncodings(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseCNTEncodings(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadPngImageFromFile(IntPtr pdf, StringBuilder filename);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadPngImageFromFile2(IntPtr pdf, StringBuilder filename);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadJpegImageFromFile(IntPtr pdf, StringBuilder filename);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadRawImageFromFile(IntPtr pdf, StringBuilder filename, uint width, uint height, HPDFColorSpaces color_space);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_LoadRawImageFromMem(IntPtr pdf, byte[] data, int width, int height, HPDFColorSpaces color_space, uint bits_per_component);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetInfoAttr(IntPtr pdf, HPDFInformationTypes type, StringBuilder value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetInfoDateAttr(IntPtr pdf, HPDFInformationTypes type, HPDFDateStruct value);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetInfoAttr(IntPtr pdf, HPDFInformationTypes type);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetPassword(IntPtr pdf, StringBuilder owner_passwd, StringBuilder user_passwd);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetPermission(IntPtr pdf, uint permission);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetEncryptionMode(IntPtr pdf, HPDFEncryptModes mode, uint key_len);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_SetCompressionMode(IntPtr pdf, uint mode);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_CreateExtGState(IntPtr pdf);

		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern uint HPDF_UseUTFEncodings(IntPtr pdf);
		#endregion

		#region constructor
		/// <summary>
		/// Allows creating a new PDF document
		/// </summary>
		public HPDFDocument()
		{
			Initialise();
		}
		private void Initialise()
		{
			HPDFErrorHandler error_handler = new HPDFErrorHandler(ErrorProc);
			handle = HPDF_New(error_handler, IntPtr.Zero);
			if (handle == IntPtr.Zero)
			{
				throw new Exception(Resources.FailedCreatingDocument);
			}
		}
		/// <summary>
		/// Dispose PDF document resources
		/// </summary>
		void IDisposable.Dispose()
		{
			if (handle != IntPtr.Zero)
			{
				HPDF_Free(handle);
			}
			handle = IntPtr.Zero;
		}
		/// <summary>
		/// Destroy the PDF object
		/// </summary>
		~HPDFDocument()
		{
			if (handle != IntPtr.Zero)
			{
				HPDF_Free(handle);
			}
		}
		#endregion

		#region properties
		IntPtr handle;
		public HPDFCallback Callback { get => _callback; set => _callback = value; }
		private HPDFCallback _callback = null;
		public bool ThrowException { get; set; }
		public HPDFException LastException { get; private set; }
		/// <summary>
		/// Indicate what page layout (number of pages) to use to first display the document
		/// </summary>
		public HPDFPageLayouts PageLayout { get => GetPageLayout(); set { SetPageLayout(value); } }
		/// <summary>
		/// Indicate how the document pages will be first displayed
		/// </summary>
		public HPDFPageModes PageMode { get => GetPageMode(); set { SetPageMode(value); } }

		public HPDFPage CurrentPage { get => GetCurrentPage(); }
		#endregion

		#region implementation
		/// <summary>
		/// Error processing function
		/// </summary>
		/// <param name="error_no">Main error</param>
		/// <param name="detail_no">Additional error details</param>
		/// <param name="user_data">Additional error data</param>
		public void ErrorProc(uint error_no, uint detail_no, IntPtr user_data)
		{
			string s = $"{Resources.ErrorCodeIs} = 0x{error_no.ToString("X")} - {Resources.DetailCodeIs} = {detail_no}";
			if (null != Callback)
				Callback(LastException = new HPDFException() { error_no = error_no, detail_no = detail_no, user_data = user_data, message = s });
			else if (ThrowException)
				throw new Exception(s);
		}
		/// <summary>
		///  Creates new document.
		///  If document object already has a document, the current document is revoked
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool NewDoc()
		{
			LastError = HPDF_NewDoc(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Revoke the current document keeping loaded resource (such as fonts and encodings) and these resources are recycled when new document required these resources.
		/// HPDF_FreeDocAll() revokes the current document and all resources
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool FreeDoc()
		{
			LastError = HPDF_FreeDoc(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Revoke the current document all resources
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool FreeDocAll()
		{
			LastError = HPDF_FreeDocAll(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Indicate whether a PDF document exists inside the object
		/// </summary>
		/// <returns>True if a PDF document exists inside the object, false otherwise</returns>
		public bool HasDoc()
		{
			return (HPDF_HasDoc(handle) != 0);
		}
		/// <summary>
		/// Save the document to a file.
		/// </summary>
		/// <param name="file_name">Name of the file to create, if the file exists it is replaced if <paramref name="replace"/> indicates so; on return the final name of the file (if amended by the process)</param>
		/// <param name="exists">[OUT] true if the file already exists, fals eotherwise</param>
		/// <param name="replace">If true and the file exists it is replaced, if false it is not</param>
		/// <returns>True if the file has been created, false if not</returns>
		public bool SaveToFile(ref string file_name, out bool exists, bool replace = false)
		{
			exists = false;
			try
			{
				FileInfo fi = new FileInfo(file_name);
				// let's just make sure the file is created with a pdf extension
				if (string.IsNullOrEmpty(fi.Extension) || 0 != string.Compare(".pdf", fi.Extension, true))
					file_name += ".pdf";
				fi = new FileInfo(file_name);
				exists = fi.Exists;
				if (exists && !replace)
				{
					// the file exists and won't be replaced
					LastError = (uint)HPDFErrors.HPDF_FILE_ALREADY_EXISTS;
					return false;
				}
				file_name = fi.FullName;
				LastError = HPDF_SaveToFile(handle, HPDFTools.StringBuilder(file_name));
				return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
			}
			catch (Exception) { }
			LastError = (uint)HPDFErrors.HPDF_UNKNOWN_ERROR;
			exists = false;
			return false;
		}
		/// <summary>
		/// Get the HPDF error code
		/// </summary>
		/// <returns>The error value <see cref="HPDFErrors"/> value</returns>
		public uint GetError()
		{
			return HPDF_GetError(handle);
		}
		/// <summary>
		/// Get the HPDF detailed error
		/// It can be the obkect which caused the error,...
		/// </summary>
		/// <returns>The object having caused the error</returns>
		public uint GetErrorDetail()
		{
			return HPDF_GetErrorDetail(handle);
		}
		/// <summary>
		/// Reset the error
		/// </summary>
		public void ResetError()
		{
			HPDF_ResetError(handle);
		}
		/// <summary>
		/// By default the document has one pages object as a root for all pages.
		/// All page objects are create as branches of this object. 
		/// One pages object can contain only 8191, therefore the maximum number of pages per document is 8191. 
		/// But you can change that fact by setting page_per_pages parameter, so that the root pages object contains 8191 more pages (not page) objects, which in turn contain 8191 pages each. So the maximum number of pages in the document becomes 8191*page_per_pages. 
		/// <param name="page_per_pages">Number of "Pages" objects to create, thus impacting the number of pages to be able to have inside the document</param>
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetPagesConfiguration(uint page_per_pages)
		{
			LastError = HPDF_SetPagesConfiguration(handle, page_per_pages);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get a page by its page number
		/// </summary>
		/// <param name="index">Number of page to get</param>
		/// <returns>a <see cref="HPDFPage"/> object to the page if it exists, null otherwise</returns>
		public HPDFPage GetPageByIndex(uint index)
		{
			IntPtr hpage = HPDF_GetPageByIndex(handle, index);
			return (IntPtr.Zero == hpage ? null : new HPDFPage(hpage));
		}
		/// <summary>
		/// Gets how the page is being displayed. 
		/// When HPDF_GetPageLayout() succeeds, it returns the current setting for page layout. If page layout is not set, it returns HPDF_PAGE_LAYOUT_EOF
		/// </summary>
		/// <returns>The current setting for page layout <see cref="HPDFPageLayouts"/>. If no page layout has been set returns <see cref="HPDFPageLayouts._eof	"/></returns>
		public HPDFPageLayouts GetPageLayout()
		{
			return HPDF_GetPageLayout(handle);
		}
		/// <summary>
		/// Sets how the page should be displayed. 
		/// If this attribute is not set, the setting of the viewer application is used
		/// </summary>
		/// <param name="layout"><see cref="HPDFPageLayouts"/></param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetPageLayout(HPDFPageLayouts layout)
		{
			LastError = HPDF_SetPageLayout(handle, layout);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get how the document is displayed
		/// </summary>
		/// <returns><see cref="HPDFPageModes"/></returns>
		public HPDFPageModes GetPageMode()
		{
			return HPDF_GetPageMode(handle);
		}
		/// <summary>
		/// Set how the document should be displayed
		/// </summary>
		/// <param name="mode"><see cref="HPDFPageModes"/></param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetPageMode(HPDFPageModes mode)
		{
			LastError = HPDF_SetPageMode(handle, mode);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set how the first page appears when a document is opened
		/// </summary>
		/// <param name="open_action"><see cref="HPDFDestination"/></param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetOpenAction(HPDFDestination open_action)
		{
			if (open_action == null)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_DESTINATION;
				return false;
			}
			LastError = HPDF_SetOpenAction(handle, open_action.GetHandle());
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the view preference to display the document
		/// </summary>
		/// <returns>A combination of flag values from <see cref="HPDFViewerPreferences"/></returns>
		public uint GetViewerPreference()
		{
			return HPDF_GetViewerPreference(handle);
		}
		/// <summary>
		/// Set how the viewer should display the document
		/// </summary>
		/// <param name="value">A combination of <see cref="HPDFViewerPreferences"/> flag values</param>
		/// <returns>True if set, false otherwise</returns>
		public bool SetViewerPreference(uint value)
		{
			LastError = HPDF_SetViewerPreference(handle, value);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Get the current page
		/// </summary>
		/// <returns>a <see cref="HPDFPage"/> object to the page if there's a current page, null otherwise</returns>
		public HPDFPage GetCurrentPage()
		{
			IntPtr hpage = HPDF_GetCurrentPage(handle);
			return (IntPtr.Zero == hpage ? null : new HPDFPage(hpage));
		}
		/// <summary>
		/// Add a page to the document
		/// </summary>
		/// <returns>A <see cref="HPDFPage"/> object if successful, null otherwise</returns>
		public HPDFPage AddPage()
		{
			IntPtr hpage = HPDF_AddPage(handle);
			return (IntPtr.Zero == hpage ? null : new HPDFPage(hpage));
		}
		/// <summary>
		/// Insert a page inside a document just before the specified page
		/// </summary>
		/// <param name="page">The page to insert the new page before</param>
		/// <returns>True if a new page has been inserted before the specified page, <see langword="null"/>otherwise</returns>
		public HPDFPage InsertPage(HPDFPage page)
		{
			IntPtr hpage = HPDF_InsertPage(handle, page.GetHandle());
			return (IntPtr.Zero == hpage ? null : new HPDFPage(hpage));
		}
		/// <summary>
		/// Get the handle of a corresponding font object by specifying name and encoding.
		/// 1. <see cref="HPDFFontTypes.Base14"/> fonts are built-in font of PDF and can be found in <see cref="StandardFonts"/>
		/// 2. <see cref="HPDFFontTypes.Type1"/> font is a format of outline fonts developed by Adobe. An AFM file is necessary to use an external type1 font.
		/// 3. <see cref="HPDFFontTypes.TrueType"/> font. A ".ttf" or ".ttc" file is needed.
		/// 4. <see cref="HPDFFontTypes.CID"/> font is a multi byte character developed by Adobe.
		///    Two simplified Chinese fonts, one traditional Chinese fonts, four Japanese fonts, and four Korean fonts are available on Haru.
		///    An application has to invoke the following functions once before using CID fonts:
		///    - <see cref="UseCNSFonts"/> makes simplified Chinese fonts(SimSun, SimHei) available.
		///    - <see cref="UseCNTFonts"/> makes traditional Chinese fonts(MingLiU) available.
		///    - <see cref="UseJPFonts"/> makes Japanese fonts(MS-Mincyo, MS-Gothic, MS-PMincyo, MS-PGothic) available.
		///    - <see cref="UseKRFonts"/> makes Korean fonts (Batang, Dotum, BatangChe, DotumChe) available.
		/// </summary>
		/// <param name="font_name">This must be a valid font name</param>
		/// <param name="encoder_name">This must be a valid encoder name or null</param>
		/// <returns>A <see cref="HPDFFont"/> object if found, null otherwise</returns>
		public HPDFFont GetFont(string font_name, string encoder_name = null)
		{
			// The application invokes <see cref="GetFont(string, string)"/> to get font-hangle. Except <see cref="HPDFFontTypes.Base14"/> fonts, an application have to invoke the following functions to load font before invoking <see cref="GetFont(string, string)"/>:
			// - <see cref="LoadType1FontFromFile(string, string)"/>
			// - <see cref="LoadTTFontFromFile(string, bool)"/>
			// - <see cref="LoadTTFontFromFile(string, bool, uint)"/>		
			// - <see cref="UseCNSFonts"/>
			// - <see cref="UseCNTFonts"/>
			// - <see cref="UseKRFonts"/>
			// - <see cref="UseJPFonts"/>
			// 1. <see cref="HPDFFontTypes.Base14"/> fonts are built-in font of PDF, and all the Viewer applications can display these fonts.
			//    An application can get a font-handle of a <see cref="HPDFFontTypes.Base14"/> font any time by invoking <see cref="GetFont(string, string)"/>.
			//    The size of pdf files using <see cref="HPDFFontTypes.Base14"/> font is smaller than those which use other type of fonts. Moreover, a processing that creates the PDF file is faster because there is no overhead loading the font.
			//    However, <see cref="HPDFFontTypes.Base14"/> fonts are only available to display Latin1 character set. To use other character set, an application have to use other type of font.
			//    <see cref="HPDFFontTypes.Base14"/> fonts are declared in <see cref="StandardFonts"/>
			// 2. Type1 font is a fotmat of outline fonts developed by Adobe. An AFM file is necessary to use an external type1 font on Haru.
			//    When an application uses an external Type1 font, an application has to invoke <see cref="LoadType1FontFromFile(string, string)"/> before invoking <see cref="GetFont(string, string)"/>.
			//    The return value of <see cref="LoadType1FontFromFile(string, string)"/> is used as font-name parameter of  <see cref="GetFont(string, string)"/>.
			//    If a PFA/PFB file is specified at invoking <see cref="LoadType1FontFromFile(string, string)"/> the glyf data of the font is embedded to the PDF file. Otherwise, only metrics data in AFM file is embedded.
			//       font_name = document.LoadType1FontFromFile("a010013l.afm", "a010013l.pfb");
			//       hfont = document.GetFont(font_name, "CP1250");
			//       page.SetFontAndSize(hfont, 10.5);
			// 3. Haru can use TrueType font. There are two types of format of truetype font.
			//    3.1 The first format whose extension is ".ttf" includes only one font-data in the file. The function <see cref="LoadTTFontFromFile(string, bool)"/> is available to load this type of font.
			//    3.2 The second format whose extension is ".ttc" includes multiple font-data in the file. The function <see cref="LoadTTFontFromFile(string, bool, uint)"/> is available to load this type of font.
			//    If the "embedding" parameter (bool) is set to true when invoking  <see cref="LoadTTFontFromFile(string, bool)"/> or  <see cref="LoadTTFontFromFile(string, bool, uint)"/> the subset of a font is embeded into a PDF file. If not, only the matrix data is stored into a PDF file. In this case a viewer application may use an alternative font if it cannot find font.
			//       font_name = document.LoadTTFontFromFile(”/usr/local/fonts/arial.ttf”, true);
			//       hfont = document.GetFont(font_name, "CP1250");
			//       page.SetFontAndSize(page, hfont, 10.5);
			//    Note: only TrueType fonts which have cmap of unicode and following tables can be used:
			//       "OS/2", "cmap", "cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "name", "post", "prep"
			// 4. The CID font is a font for multi byte character developed by Adobe.
			//    Two simplified Chinese fonts, one traditional Chinese fonts, four Japanese fonts, and four Korean fonts are available on Haru.
			//    An application have to invoke the following functions once before using CID fonts:
			//    - <see cref="UseCNSFonts"/> makes simplified Chinese fonts(SimSun, SimHei) available.
			//    - <see cref="UseCNTFonts"/> makes traditional Chinese fonts(MingLiU) available.
			//    - <see cref="UseJPFonts"/> makes Japanese fonts(MS-Mincyo, MS-Gothic, MS-PMincyo, MS-PGothic) available.
			//    - <see cref="UseKRFonts"/> makes Korean fonts (Batang, Dotum, BatangChe, DotumChe) available.
			//       document.UseJPFonts();
			//       document.UseJPEncodings();
			//       hfont = document.GetFont("MS-Mincyo", "90ms-RKSJ-H");
			//       page.SetFontAndSize(hfont, 10.5);
			IntPtr hfont = HPDF_GetFont(handle, HPDFTools.StringBuilder(font_name), HPDFTools.StringBuilder(encoder_name));
			return (IntPtr.Zero == hfont ? null : new HPDFFont(hfont));
		}
		/// <summary>
		/// Load a type1 font from an external file and register it to a document object.
		/// The font is stored in an AFM file.
		/// If a PFA/PFB file is specified the glyf data of the font is embedded to the PDF file. Otherwise, only metrics data in AFM file is embedded.
		/// </summary>
		/// <param name="afmfilename">A path of an AFM file</param>
		/// <param name="pfmfilename">A path of a PFA/PFB file. If it is NULL, the gryph data of font file is not embedded to a PDF file.</param>
		/// <returns>The name of the font if there's one, null otherwise</returns>
		public string LoadType1FontFromFile(string afmfilename, string pfmfilename = null)
		{
			IntPtr s = HPDF_LoadType1FontFromFile(handle, HPDFTools.StringBuilder(afmfilename), HPDFTools.StringBuilder(pfmfilename));
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		///  Load a TrueType font from an external ".ttf" file and register it to a document object.
		///  If <paramref name="embedding"/> is true subset of a font is embeded into a PDF file. If not, only the matrix data is stored into a PDF file. In this case a viewer application may use an alternative font if it cannot find font.
		///  Note that only TrueType fonts which have cmap of unicode and following tables can be used: "OS/2", "cmap", "cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "name", "post", "prep"
		/// </summary>
		/// <param name="file_name">A path of a TrueType font file (.ttf).</param>
		/// <param name="embedding">If this parameter is set to true, the glyph data of the font is embedded, otherwise only the matrix data is included in PDF file.</param>
		/// <returns>The name of the font if there's one, null otherwise</returns>
		public string LoadTTFontFromFile(string file_name, bool embedding)
		{
			IntPtr s = HPDF_LoadTTFontFromFile(handle, HPDFTools.StringBuilder(file_name), (embedding ? HPDF_TRUE : HPDF_FALSE));
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		///  Load a TrueType font from an external .ttc" file and register it to a document object.
		///  A ".ttc" file includes multiple font-data in the file, the <paramref name="index"/> indicates which font to use.
		///  If <paramref name="embedding"/> is true subset of a font is embeded into a PDF file. If not, only the matrix data is stored into a PDF file. In this case a viewer application may use an alternative font if it cannot find font.
		///  Note that only TrueType fonts which have cmap of unicode and following tables can be used: "OS/2", "cmap", "cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "name", "post", "prep"
		/// </summary>
		/// <param name="file_name">A path of a TrueType font file (.ttf).</param>
		/// <param name="index">Index of the font to load</param>
		/// <param name="embedding">If this parameter is set to true, the glyph data of the font is embedded, otherwise only the matrix data is included in PDF file.</param>
		/// <returns>The name of the font if there's one, null otherwise</returns>
		public string LoadTTFontFromFile(string file_name, bool embedding, uint index = 0)
		{
			IntPtr s = HPDF_LoadTTFontFromFile2(handle, HPDFTools.StringBuilder(file_name), index, (embedding ? HPDF_TRUE : HPDF_FALSE));
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		/// Add a page labeling range for the document
		/// </summary>
		/// <param name="page_num">The first page that applies this labeling range</param>
		/// <param name="style"><see cref="HPDFPageNumberingStyles"/></param>
		/// <param name="first_page">The first page number in this range</param>
		/// <param name="prefix">The prefix for the page label</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool AddPageLabel(uint page_num, HPDFPageNumberingStyles style, uint first_page = 1, string prefix = null)
		{
			LastError = HPDF_AddPageLabel(handle, page_num, style, first_page, HPDFTools.StringBuilder(prefix));
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable Japanese fonts.
		/// After having invoked this function an application can use the following Japanese fonts:
		/// - MS-Mincyo
		/// - MS-Mincyo,Bold
		/// - MS-Mincyo,Italic
		/// - MS-Mincyo,BoldItalic
		/// - MS-Gothic
		/// - MS-Gothic,Bold
		/// - MS-Gothic,Italic
		/// - MS-Gothic,BoldItalic
		/// - MS-PMincyo
		/// - MS-PMincyo,Bold
		/// - MS-PMincyo,Italic
		/// - MS-PMincyo,BoldItalic
		/// - MS-PGothic
		/// - MS-PGothic,Bold
		/// - MS-PGothic,Italic
		/// - MS-PGothic,BoldItalic
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseJPFonts()
		{
			LastError = HPDF_UseJPFonts(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable Korean fonts.
		/// After having invoked this function an application can use the following Japanese fonts:		/// - DotumChe
		/// - DotumChe,Bold
		/// - DotumChe,Italic
		/// - DotumChe,BoldItalic
		/// - Dotum
		/// - Dotum,Bold
		/// - Dotum,Italic
		/// - Dotum,BoldItalic
		/// - BatangChe
		/// - BatangChe,Bold
		/// - BatangChe,Italic
		/// - BatangChe,BoldItalic
		/// - Batang
		/// - Batang,Bold
		/// - Batang,Italic
		/// - Batang,BoldItalic
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseKRFonts()
		{
			LastError = HPDF_UseKRFonts(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable  simplified Chinese fonts.
		/// After having invoked this function an application can use the following Japanese fonts:		/// - DotumChe
		/// - SimSun
		/// - SimSun,Bold
		/// - SimSun,Italic
		/// - SimSun,BoldItalic
		/// - SimHei
		/// - SimHei,Bold
		/// - SimHei,Italic
		/// - SimHei,BoldItalic
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseCNSFonts()
		{
			LastError = HPDF_UseCNSFonts(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable  traditional Chinese fonts.
		/// After having invoked this function an application can use the following Japanese fonts:		
		/// - DotumChe
		/// - MingLiU
		/// - MingLiU,Bold
		/// - MingLiU,Italic
		/// - MingLiU,BoldItalic
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseCNTFonts()
		{
			LastError = HPDF_UseCNTFonts(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Create a new outline object for the document
		/// </summary>
		/// <param name="parent">The handle of an outline object which comes as the parent of the created outline object. If this parameter is NULL, the outline is created as a root outline.</param>
		/// <param name="title">Caption of the outline object</param>
		/// <param name="encoder">Handle of an encoding object applied to the title. If it is null a default encoder is used</param>
		/// <returns>A <see cref="HPDFOutline"/> object or null if an error has occurred</returns>
		public HPDFOutline CreateOutline(HPDFOutline parent, string title, HPDFEncoder encoder = null)
		{
			IntPtr houtline = HPDF_CreateOutline(handle, (parent == null ? IntPtr.Zero : parent.GetHandle()), HPDFTools.StringBuilder(title), (encoder == null ? IntPtr.Zero : encoder.GetHandle()));
			return (houtline == IntPtr.Zero ? null : new HPDFOutline(houtline));
		}
		/// <summary>
		/// Get the handle of a corresponding encoder object by specified encoding name.
		/// </summary>
		/// <param name="encoder_name">A valid encoding name</param>
		/// <returns>A <see cref="HPDFEncoder"/> object or null if an error has occurred</returns>
		public HPDFEncoder GetEncoder(string encoder_name)
		{
			IntPtr hencoder = HPDF_GetEncoder(handle, HPDFTools.StringBuilder(encoder_name));
			return (hencoder == IntPtr.Zero ? null : new HPDFEncoder(hencoder));
		}
		/// <summary>
		/// Get the handle of the current encoder of the document object.
		/// The current encoder is set by invoking <see cref="SetCurrentEncoder(string)"/> and it is used to process a text when an application invokes <see cref="SetInformationType(HPDFInformationTypes, string)"/>.
		/// </summary>
		/// <returns>A <see cref="HPDFEncoder"/> object if a default encoder exists, or null if no default encoder or an error has occurred</returns>
		public HPDFEncoder GetCurrentEncoder()
		{
			IntPtr hencoder = HPDF_GetCurrentEncoder(handle);
			return (hencoder == IntPtr.Zero ? null : new HPDFEncoder(hencoder));
		}
		/// <summary>
		///  Set teh default encoder for the document
		/// </summary>
		/// <param name="encoder_name">A valid encoding name</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetCurrentEncoder(string encoder_name)
		{
			LastError = HPDF_SetCurrentEncoder(handle, HPDFTools.StringBuilder(encoder_name));
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable Japanese encodings.
		/// After being called, an application can use the following Japanese encodings:
		/// - 90ms-RKSJ-H
		/// - 90ms-RKSJ-V
		/// - 90msp-RKSJ-H
		/// - EUC-H
		/// - EUC-V
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseJPEncodings()
		{
			LastError = HPDF_UseJPEncodings(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable Korean encodings.
		/// After being called, an application can use the following Japanese encodings:
		/// - KSC-EUC-H
		/// - KSC-EUC-V
		/// - KSCms-UHC-H
		/// - KSCms-UHC-HW-H
		/// - KSCms-UHC-HW-V
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseKREncodings()
		{
			LastError = HPDF_UseKREncodings(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable simplified Chinese encodings.
		/// After being called, an application can use the following Japanese encodings:
		/// - GB-EUC-H
		/// - GB-EUC-V
		/// - GBK-EUC-H
		/// - GBK-EUC-V
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseCNSEncodings()
		{
			LastError = HPDF_UseCNSEncodings(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Enable traditional Chinese encodings.
		/// After being called, an application can use the following Japanese encodings:
		/// - GB-EUC-H
		/// - GB-EUC-V
		/// - GBK-EUC-H
		/// - GBK-EUC-V
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseCNTEncodings()
		{
			LastError = HPDF_UseCNTEncodings(handle);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Load an external PNG image file
		/// </summary>
		/// <param name="filename">A path to a PNG image file</param>
		/// <returns>A <see cref="HPDFImage"/> object or null if an error has occurred </returns>
		public HPDFImage LoadPNGImageFromFile(string filename)
		{
			IntPtr h = HPDF_LoadPngImageFromFile(handle, HPDFTools.StringBuilder(filename));
			return (h == IntPtr.Zero ? null : new HPDFImage(h));
		}
		/// <summary>
		/// Load an external png image file.
		/// Unlike <see cref="LoadPNGImageFromFile(string)"/> this function does not load whole data immediately but size and color properties only.
		/// The main data is loaded just before the image object is written to the docuemnt, and the loaded data is deleted immediately.
		/// </summary>
		/// <param name="filename">A path to a PNG image file</param>
		/// <returns>A <see cref="HPDFImage"/> object or null if an error has occurred </returns>
		public HPDFImage LoadPNGImageFromFileDelayed(string filename)
		{
			IntPtr h = HPDF_LoadPngImageFromFile2(handle, HPDFTools.StringBuilder(filename));
			return (h == IntPtr.Zero ? null : new HPDFImage(h));
		}
		/// <summary>
		/// Load an external JPEG image file
		/// </summary>
		/// <param name="filename">A path to a JPEG image file</param>
		/// <returns>A <see cref="HPDFImage"/> object or null if an error has occurred </returns>
		public HPDFImage LoadJpegImageFromFile(string filename)
		{
			IntPtr h = HPDF_LoadJpegImageFromFile(handle, HPDFTools.StringBuilder(filename));
			return (h == IntPtr.Zero ? null : new HPDFImage(h));
		}
		/// <summary>
		/// Loads a "raw" image from file.
		/// Raw images are bitmaps gray, RGB or CMYK.
		/// This function loads the data without any conversion, it is therefore usually faster than the other functions.
		/// 1. 8 bit gray scale image.
		///    The gray scale describes one pixel by one byte.
		///    The size of the image data is same as (width * height).
		///    The sequence of the data is as follows:
		///    | 1 | 2 | 3 | 4 |
		///    | 6 | 7 | 8 | 9 |
		///    |11 |12 |13 |14 |
		/// 2. 24bit RGB color image.
		///    The 24bit RGB color image describes one pixel by 3 byte (each one byte describes a value of either red, green or blue).
		///    The size of the image is same as (width * height * 3).
		///    The sequence of the data is as follows:
		///    | 1 | 1 | 1 | 2 | 2 | 2 | 3 | 3 | 3 | 4 | 4 | 4 |
		///    | 6 | 6 | 6 | 7 | 7 | 7 | 8 | 8 | 8 | 9 | 9 | 9 |
		///    |11 |11 |11 |12 |12 |12 |13 |13 |13 |14 |14 |14 |
		/// 3. 36bit CMYK color image.
		///    The 36bit CMYK color image describes one pixel by 4 byte (each one byte describes a value of either Cyan Magenta Yellow Black).
		///    The size of the image is same as (width * height * 4)
		///    The sequence of the data is as follows:
		///    | 1 | 1 | 1 | 1 | 2 | 2 | 2 | 2 | 3 | 3 | 3 | 3 | 4 | 4 | 4 | 4 |
		///    | 6 | 6 | 6 | 6 | 7 | 7 | 7 | 7 | 8 | 8 | 8 | 8 | 9 | 9 | 9 | 9 |
		///    |11 |11 |11 |11 |12 |12 |12 |12 |13 |13 |13 |13 |14 |14 |14 |14 |
		/// </summary>
		/// <param name="filename">A path to a JPEG image file</param>
		/// <param name="color_space">Color space to use among <see cref="HPDFColorSpaces.DeviceGray"/>, <see cref="HPDFColorSpaces.DeviceRGB"/> and <see cref="HPDFColorSpaces.DeviceCMYK"/></param>
		/// <param name="height">The height of an image file</param>
		/// <param name="width">The width of an image file</param>
		/// <returns>A <see cref="HPDFImage"/> object or null if an error has occurred </returns>
		public HPDFImage LoadRawImageFromFile(string filename, uint width, uint height, HPDFColorSpaces color_space)
		{
			IntPtr h = HPDF_LoadRawImageFromFile(handle, HPDFTools.StringBuilder(filename), width, height, color_space);
			return (h == IntPtr.Zero ? null : new HPDFImage(h));
		}
		/// <summary>
		/// Load an image which has "raw" image format from buffer.
		/// This function loads the data without any conversion, it is therefore usually faster than the other functions.
		/// This function loads the same formats from memory than <see cref="LoadRawImageFromFile(string, uint, uint, HPDFColorSpaces)"/>
		/// </summary>
		/// <param name="data">Buffer to the image data</param>
		/// <param name="width">The width of an image file</param>
		/// <param name="height">The height of an image file</param>
		/// <param name="color_space">Color space to use among <see cref="HPDFColorSpaces.DeviceGray"/>, <see cref="HPDFColorSpaces.DeviceRGB"/> and <see cref="HPDFColorSpaces.DeviceCMYK"/></param>
		/// <param name="bits_per_component">The bit size of each color componen which MUST be either 1, 2, 4 or 8</param>
		/// <returns>A <see cref="HPDFImage"/> object or null if an error has occurred </returns>
		public HPDFImage LoadRawImageFromMem(byte[] data, int width, int height, HPDFColorSpaces color_space, uint bits_per_component)
		{
			if (width * height < data.Length)
				return null;
			IntPtr h = HPDF_LoadRawImageFromMem(handle, data, width, height, color_space, bits_per_component);
			return (h == IntPtr.Zero ? null : new HPDFImage(h));
		}
		/// <summary>
		/// Set a text into the information dictionary
		/// It uses the current encoding of the document. If it is null a default encoder is used.
		/// </summary>
		/// <param name="type"><see cref="HPDFInformationTypes"/>, it MUST be a TEXT data</param>
		/// <param name="value">Value to set</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetInformationType(HPDFInformationTypes type, string value)
		{
			if (HPDFInformationTypes._begin_text > type || HPDFInformationTypes._end_text < type)
			{
				LastError = (uint)HPDFErrors.HPDF_INVALID_PARAMETER;
				return false;
			}
			LastError = HPDF_SetInfoAttr(handle, type, HPDFTools.StringBuilder(value));
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set a date into the information dictionary
		/// It uses the current encoding of the document. If it is null a default encoder is used.
		/// </summary>
		/// <param name="type"><see cref="HPDFInformationTypes"/>, it MUST be a DATE data</param>
		/// <param name="value">Value to set</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetInformationAttribute(HPDFInformationTypes type, DateTime value)
		{
			if (HPDFInformationTypes._begin_date <= type && HPDFInformationTypes._end_date >= type)
			{
				HPDFDateStruct dt = new HPDFDateStruct();
				dt.Year = value.Year;
				dt.Month = value.Month;
				dt.Day = value.Day;
				dt.Hour = value.Hour;
				dt.Minutes = value.Minute;
				dt.Seconds = value.Second;
				LastError = HPDF_SetInfoDateAttr(handle, type, dt);
				return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
			}
			LastError = (uint)HPDFErrors.HPDF_INVALID_PARAMETER;
			return false;
		}
		/// <summary>
		/// Get an value from information dictionary.
		/// It uses the current encoding of the document. If it is null a default encoder is used.
		/// </summary>
		/// <param name="type"><see cref="HPDFInformationTypes"/></param>
		/// <returns>The information value if set, null otherwise</returns>
		public string GetInformationAttribute(HPDFInformationTypes type)
		{
			IntPtr s = HPDF_GetInfoAttr(handle, type);
			return Marshal.PtrToStringAnsi(s);
		}
		/// <summary>
		/// Set the pasword for the document. If the password is set, contents in the document are encrypted.
		/// </summary>
		/// <param name="owner_passwd">The password for the owner of the document.
		/// The owner can change the permission of the document.
		/// Null or empty string and the same value as user password are not allowed.</param>
		/// <param name="user_passwd">The password for the user of the document.
		/// The user_password can be set to null or being empty</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetPassword(string owner_passwd, string user_passwd)
		{
			if (string.IsNullOrEmpty(owner_passwd) || 0 == string.Compare(owner_passwd, user_passwd))
			{
				LastError = (uint)HPDFErrors.HPDF_ENCRYPT_INVALID_PASSWORD;
				return false;
			}
			LastError = HPDF_SetPassword(handle, HPDFTools.StringBuilder(owner_passwd), HPDFTools.StringBuilder(user_passwd));
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the flags of the permission for the document
		/// </summary>
		/// <param name="permission">Permissions to apply <see cref="HPDFPermissions"/> which can be combined as flags</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetPermission(HPDFPermissions permission)
		{
			LastError = HPDF_SetPermission(handle, (uint)permission);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the type of encryption.
		/// As a side effect, this function elevates the version of PDF to 1.4 when the mode is set to <see cref="HPDFEncryptModes.R3"/>
		/// </summary>
		/// <param name="mode">Applied encryption <see cref="HPDFEncryptModes"/></param>
		/// <param name="key_len">Specify the byte length of an encryption key. This parameter is valid only when "mode" parameter is set to <see cref="HPDFEncryptModes.R3"/>. Between 5(40bit) and 16(128bit) can be specified for length of the key.</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetEncryptionMode(HPDFEncryptModes mode, uint key_len)
		{
			LastError = HPDF_SetEncryptionMode(handle, mode, key_len);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Set the mode of compression
		/// </summary>
		/// <param name="mode">Compressions to apply <see cref="HPDFCompressionModes"/> which can be combined as flags</param>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool SetCompressionMode(HPDFCompressionModes mode)
		{
			LastError = HPDF_SetCompressionMode(handle, (uint)mode);
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		/// <summary>
		/// Create extended graphics state parameters
		/// </summary>
		/// <returns></returns>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public HPDFExtGState CreateExtGraphicState()
		{
			IntPtr hgstate = HPDF_CreateExtGState(handle);
			return IntPtr.Zero == hgstate ? null : new HPDFExtGState(hgstate);
		}
		/// <summary>
		/// Set using UTF-8 encoder
		/// </summary>
		/// <returns>True if NO ERROR, false otherwise</returns>
		public bool UseUTF8Encoding()
		{
			LastError = HPDF_UseUTFEncodings(handle);
			if ((uint)HPDFErrors.HPDF_NO_ERROR == LastError)
				return SetCurrentEncoder("UTF-8");
			return (uint)HPDFErrors.HPDF_NO_ERROR == LastError;
		}
		#endregion

		#region other methods
		#endregion
	}
}

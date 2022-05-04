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
	#region enums
	/// <summary>
	/// HPDF errors
	/// </summary>
	public enum HPDFErrors : uint
	{
		HPDF_NO_ERROR = 0,
		HPDF_ARRAY_COUNT_ERR = 0x1001,
		HPDF_ARRAY_ITEM_NOT_FOUND = 0x1002,
		HPDF_ARRAY_ITEM_UNEXPECTED_TYPE = 0x1003,
		HPDF_BINARY_LENGTH_ERR = 0x1004,
		HPDF_CANNOT_GET_PALLET = 0x1005,
		HPDF_DICT_COUNT_ERR = 0x1007,
		HPDF_DICT_ITEM_NOT_FOUND = 0x1008,
		HPDF_DICT_ITEM_UNEXPECTED_TYPE = 0x1009,
		HPDF_DICT_STREAM_LENGTH_NOT_FOUND = 0x100A,
		HPDF_DOC_ENCRYPTDICT_NOT_FOUND = 0x100B,
		HPDF_DOC_INVALID_OBJECT = 0x100C,
		/* 0x100D */
		HPDF_DUPLICATE_REGISTRATION = 0x100E,
		HPDF_EXCEED_JWW_CODE_NUM_LIMIT = 0x100F,
		/* 0x1010 */
		HPDF_ENCRYPT_INVALID_PASSWORD = 0x1011,
		/* 0x1012 */
		HPDF_ERR_UNKNOWN_CLASS = 0x1013,
		HPDF_EXCEED_GSTATE_LIMIT = 0x1014,
		HPDF_FAILD_TO_ALLOC_MEM = 0x1015,
		HPDF_FILE_IO_ERROR = 0x1016,
		HPDF_FILE_OPEN_ERROR = 0x1017,
		/* 0x1018 */
		HPDF_FONT_EXISTS = 0x1019,
		HPDF_FONT_INVALID_WIDTHS_TABLE = 0x101A,
		HPDF_INVALID_AFM_HEADER = 0x101B,
		HPDF_INVALID_ANNOTATION = 0x101C,
		/* 0x101D */
		HPDF_INVALID_BIT_PER_COMPONENT = 0x101E,
		HPDF_INVALID_CHAR_MATRICS_DATA = 0x101F,
		HPDF_INVALID_COLOR_SPACE = 0x1020,
		HPDF_INVALID_COMPRESSION_MODE = 0x1021,
		HPDF_INVALID_DATE_TIME = 0x1022,
		HPDF_INVALID_DESTINATION = 0x1023,
		/* 0x1024 */
		HPDF_INVALID_DOCUMENT = 0x1025,
		HPDF_INVALID_DOCUMENT_STATE = 0x1026,
		HPDF_INVALID_ENCODER = 0x1027,
		HPDF_INVALID_ENCODER_TYPE = 0x1028,
		/* 0x1029 */
		/* 0x102A */
		HPDF_INVALID_ENCODING_NAME = 0x102B,
		HPDF_INVALID_ENCRYPT_KEY_LEN = 0x102C,
		HPDF_INVALID_FONTDEF_DATA = 0x102D,
		HPDF_INVALID_FONTDEF_TYPE = 0x102E,
		HPDF_INVALID_FONT_NAME = 0x102F,
		HPDF_INVALID_IMAGE = 0x1030,
		HPDF_INVALID_JPEG_DATA = 0x1031,
		HPDF_INVALID_N_DATA = 0x1032,
		HPDF_INVALID_OBJECT = 0x1033,
		HPDF_INVALID_OBJ_ID = 0x1034,
		HPDF_INVALID_OPERATION = 0x1035,
		HPDF_INVALID_OUTLINE = 0x1036,
		HPDF_INVALID_PAGE = 0x1037,
		HPDF_INVALID_PAGES = 0x1038,
		HPDF_INVALID_PARAMETER = 0x1039,
		/* 0x103A */
		HPDF_INVALID_PNG_IMAGE = 0x103B,
		HPDF_INVALID_STREAM = 0x103C,
		HPDF_MISSING_FILE_NAME_ENTRY = 0x103D,
		/* 0x103E */
		HPDF_INVALID_TTC_FILE = 0x103F,
		HPDF_INVALID_TTC_INDEX = 0x1040,
		HPDF_INVALID_WX_DATA = 0x1041,
		HPDF_ITEM_NOT_FOUND = 0x1042,
		HPDF_LIBPNG_ERROR = 0x1043,
		HPDF_NAME_INVALID_VALUE = 0x1044,
		HPDF_NAME_OUT_OF_RANGE = 0x1045,
		/* 0x1046 */
		/* 0x1047 */
		HPDF_PAGE_INVALID_PARAM_COUNT = 0x1048,
		HPDF_PAGES_MISSING_KIDS_ENTRY = 0x1049,
		HPDF_PAGE_CANNOT_FIND_OBJECT = 0x104A,
		HPDF_PAGE_CANNOT_GET_ROOT_PAGES = 0x104B,
		HPDF_PAGE_CANNOT_RESTORE_GSTATE = 0x104C,
		HPDF_PAGE_CANNOT_SET_PARENT = 0x104D,
		HPDF_PAGE_FONT_NOT_FOUND = 0x104E,
		HPDF_PAGE_INVALID_FONT = 0x104F,
		HPDF_PAGE_INVALID_FONT_SIZE = 0x1050,
		HPDF_PAGE_INVALID_GMODE = 0x1051,
		HPDF_PAGE_INVALID_INDEX = 0x1052,
		HPDF_PAGE_INVALID_ROTATE_VALUE = 0x1053,
		HPDF_PAGE_INVALID_SIZE = 0x1054,
		HPDF_PAGE_INVALID_XOBJECT = 0x1055,
		HPDF_PAGE_OUT_OF_RANGE = 0x1056,
		HPDF_REAL_OUT_OF_RANGE = 0x1057,
		HPDF_STREAM_EOF = 0x1058,
		HPDF_STREAM_READLN_CONTINUE = 0x1059,
		/* 0x105A */
		HPDF_STRING_OUT_OF_RANGE = 0x105B,
		HPDF_THIS_FUNC_WAS_SKIPPED = 0x105C,
		HPDF_TTF_CANNOT_EMBEDDING_FONT = 0x105D,
		HPDF_TTF_INVALID_CMAP = 0x105E,
		HPDF_TTF_INVALID_FOMAT = 0x105F,
		HPDF_TTF_MISSING_TABLE = 0x1060,
		HPDF_UNSUPPORTED_FONT_TYPE = 0x1061,
		HPDF_UNSUPPORTED_FUNC = 0x1062,
		HPDF_UNSUPPORTED_JPEG_FORMAT = 0x1063,
		HPDF_UNSUPPORTED_TYPE1_FONT = 0x1064,
		HPDF_XREF_COUNT_ERR = 0x1065,
		HPDF_ZLIB_ERROR = 0x1066,
		HPDF_INVALID_PAGE_INDEX = 0x1067,
		HPDF_INVALID_URI = 0x1068,
		HPDF_PAGE_LAYOUT_OUT_OF_RANGE = 0x1069,
		HPDF_PAGE_MODE_OUT_OF_RANGE = 0x1070,
		HPDF_PAGE_NUM_STYLE_OUT_OF_RANGE = 0x1071,
		HPDF_ANNOT_INVALID_ICON = 0x1072,
		HPDF_ANNOT_INVALID_BORDER_STYLE = 0x1073,
		HPDF_PAGE_INVALID_DIRECTION = 0x1074,
		HPDF_INVALID_FONT = 0x1075,
		HPDF_PAGE_INSUFFICIENT_SPACE = 0x1076,
		HPDF_PAGE_INVALID_DISPLAY_TIME = 0x1077,
		HPDF_PAGE_INVALID_TRANSITION_TIME = 0x1078,
		HPDF_INVALID_PAGE_SLIDESHOW_TYPE = 0x1079,
		HPDF_EXT_GSTATE_OUT_OF_RANGE = 0x1080,
		HPDF_INVALID_EXT_GSTATE = 0x1081,
		HPDF_EXT_GSTATE_READ_ONLY = 0x1082,
		HPDF_INVALID_U3D_DATA = 0x1083,
		HPDF_NAME_CANNOT_GET_NAMES = 0x1084,
		HPDF_INVALID_ICC_COMPONENT_NUM = 0x1085,
		// library definitions
		HPDF_UNKNOWN_ERROR = 0x9000,
		HPDF_FILE_ALREADY_EXISTS = 0x9001,
	}
	/// <summary>
	/// Compression mode
	/// </summary>
	[Flags]
	public enum HPDFCompressionModes : uint
	{
		/// <summary>
		/// No compression on any element
		/// </summary>
		None = 0,
		/// <summary>
		/// Compress the contents stream of the page
		/// </summary>
		Text = 1,
		/// <summary>
		/// Compress the streams of the image objects
		/// </summary>
		Image = 2,
		/// <summary>
		/// Other stream datas (fonts, cmaps and so on)  are compressed
		/// </summary>
		Metadata = 4,
		/// <summary>
		/// All stream datas are compressed
		/// </summary>
		All = 15,
	}
	/// <summary>
	/// Permissions that can be applied to a PDF docuemnt
	/// </summary>
	[Flags]
	public enum HPDFPermissions : uint
	{
		/// <summary>
		/// User can read the document
		/// </summary>
		Read = 0,
		/// <summary>
		/// User can print the document
		/// </summary>
		Print = 4,
		/// <summary>
		/// User can edit the contents of the document other than annotations and form fields
		/// </summary>
		Edit = 8,
		/// <summary>
		/// User can copy the text and the graphics of the document
		/// </summary>
		Copy = 16,
		/// <summary>
		/// User can add or modify the annotations and form fields of the document
		/// </summary>
		EditAll = 32,
	}
	/// <summary>
	/// Page layout
	/// Describe how pages are displayed
	/// </summary>
	public enum HPDFPageLayouts : uint
	{
		/// <summary>
		/// Only one page is displayed
		/// </summary>
		Single = 0,
		/// <summary>
		/// Display the pages in one column
		/// </summary>
		OneColumn,
		/// <summary>
		/// Display in two columns. Odd page number is displayed left
		/// </summary>
		TwoColumnLeft,
		/// <summary>
		/// Display in two columns. Odd page number is displayed right
		/// </summary>
		TwoColumnRight,
		_eof,
	};
	/// <summary>
	/// Page mode
	/// Describe whether additional panes are displayed along the document or not
	/// </summary>
	public enum HPDFPageModes : uint
	{
		/// <summary>
		/// Display the document with neither outline nor thumbnail
		/// </summary>
		None = 0,
		/// <summary>
		/// Display the document with outline pane
		/// </summary>
		Outline,
		/// <summary>
		/// Display the document with thumbnail pane
		/// </summary>
		Thumbs,
		/// <summary>
		/// Display the document with full screen mode
		/// </summary>
		Fullscreen,
		_eof,
	};
	/// <summary>
	/// Page size
	/// </summary>
	public enum HPDFPageSizes : uint
	{
		/// <summary>
		/// 8½ x 11 (Inches) - 612 x 792 (pixels)
		/// </summary>
		Lettrer = 0,
		/// <summary>
		///  8½ x 14 (Inches) - 612 x 1008 (pixels)
		/// </summary>
		Legal,
		/// <summary>
		/// 297 × 420 (mm) - 841.89 x 1199.551 (pixels)
		/// </summary>
		A3,
		/// <summary>
		/// 210 × 297 (mm) - 595.276 x 841.89 (pixels)
		/// </summary>
		A4,
		/// <summary>
		/// 148 × 210 (mm) - 419.528 x 595.276 (pixels)
		/// </summary>
		A5,
		/// <summary>
		/// 250 × 353 (mm) - 708.661 x 1000.63 (pixels)
		/// </summary>
		B4,
		/// <summary>
		/// 176 × 250 (mm) - 498.898 x 708.661 (pixels)
		/// </summary>
		B5,
		/// <summary>
		/// 7½ x 10½ (Inches) - 522 x 756 (pixels)
		/// </summary>
		Executive,
		/// <summary>
		/// 4 x 6 (Inches) - 288 x 432 (pixels)
		/// </summary>
		US4x6,
		/// <summary>
		/// 4 x 8 (Inches) - 288 x 576 (pixels)
		/// </summary>
		US4x8,
		/// <summary>
		/// 5 x 7 (Inches) - 360 x 504 (pixels)
		/// </summary>
		US5x7,
		/// <summary>
		/// 4.125 x 9.5 (Inches) - 297x 684 (pixels)
		/// </summary>
		Comm10,
		_eof,
	};
	/// <summary>
	/// Page direction
	/// </summary>
	public enum HPDFPageDirections : uint
	{
		/// <summary>
		/// Portrait direction
		/// </summary>
		Portrait = 0,
		/// <summary>
		/// Landscape direction
		/// </summary>
		Landscape,
		_eof,
	};
	/// <summary>
	/// Page numbering style
	/// </summary>
	public enum HPDFPageNumberingStyles : uint
	{
		/// <summary>
		/// 1, 2, 3...
		/// </summary>
		Decimal = 0,
		/// <summary>
		/// I, II, III, IV, V,...
		/// </summary>
		UpperRoman,
		/// <summary>
		/// i, ii, iii, iv, v,...
		/// </summary>
		LowerRoman,
		/// <summary>
		/// A, B, C,...
		/// </summary>
		UpperLetters,
		/// <summary>
		/// a, b, c,...
		/// </summary>
		LowerLetters,
		EOF
	};
	/// <summary>
	/// The writing mode for the encoding object
	/// </summary>
	public enum HPDFWritingModes : uint
	{
		/// <summary>
		/// Horizontal
		/// </summary>
		Horizontal = 0,
		/// <summary>
		/// Vertical
		/// </summary>
		Vertical,
		_eof,
	};
	/// <summary>
	/// Type of encoding of an object
	/// </summary>
	public enum HPDFEncoderTypes : uint
	{
		/// <summary>
		/// Encoder is an encoder for single byte characters
		/// </summary>
		SingleByte = 0,
		/// <summary>
		/// Encoder is an encoder for multi byte characters
		/// </summary>
		DoubleByte,
		/// <summary>
		/// Encoder is uninitialized (may be it is an encoder for multi byte characters)
		/// </summary>
		Uninitialized,
		/// <summary>
		/// Invalid encoder
		/// </summary>
		Unknown,
	};
	/// <summary>
	/// Type of encoding object
	/// </summary>
	public enum HPDFByteTypes : uint
	{
		/// <summary>
		/// Single byte character
		/// </summary>
		Single = 0,
		/// <summary>
		/// Lead byte of a double-byte character
		/// </summary>
		Lead,
		/// <summary>
		/// Trailing byte of a double-byte character
		/// </summary>
		Trail,
		/// <summary>
		/// Invalid encoder or cannot judge the byte type
		/// </summary>
		Unknown,
	};
	/// <summary>
	/// The appearance of when a mouse clicked on a link annotation
	/// </summary>
	public enum HPDFAnnotationHighlightModes : uint
	{
		/// <summary>
		/// No highlighting
		/// </summary>
		NoHightlight = 0,
		/// <summary>
		/// Invert the contents of the area of annotation
		/// </summary>
		InvertBox,
		/// <summary>
		/// Invert the annotation’s border
		/// </summary>
		InvertBorder,
		/// <summary>
		/// Dent the annotation
		/// </summary>
		DownAppearance,
		_eof,
	};
	/// <summary>
	/// The style of the annotation's icon
	/// </summary>
	public enum HPDFAnnotationIcons : uint
	{
		Comment = 0,
		Key,
		Note,
		Help,
		NewParagraph,
		Paragraph,
		Insert,
		_eof,
	};
	/// <summary>
	/// Annotation intent
	/// </summary>
	public enum HPDFAnnotationIntents : uint
	{
		FreeTextCallout = 0,
		FreeTextTypeWriter,
		LineArrow,
		LineDimension,
		PolygonCloud,
		PolylineDimension,
		PolygonDimension,
	}
	/// <summary>
	/// Annotation line ending style
	/// </summary>
	public enum HPDFLineAnnotationEndingStyles : uint
	{
		None = 0,
		Square,
		Circle,
		Diamond,
		OpenArrow,
		ClosedArrow,
		Butt,
		ReopenArrow,
		ReclosedArrow,
		Slash,
	}
	/// <summary>
	/// Annomation capital position
	/// </summary>
	public enum HPDFLineAnnotationCapPositions : uint
	{
		Inline = 0,
		Top
	}
	/// <summary>
	/// Annotation stamp name
	/// </summary>
	public enum HPDFStampAnnotationNames : uint
	{
		Approved = 0,
		Experimental,
		NotApproved,
		AsIs,
		Expired,
		NotForPublicRelease,
		Confidential,
		Final,
		Sold,
		Departmental,
		ForComment,
		TopSecret,
		Draft,
		ForPublicRelease,
	}
	/// <summary>
	/// Color space to use
	/// </summary>
	public enum HPDFColorSpaces : uint
	{
		DeviceGray = 0,
		DeviceRGB,
		DeviceCMYK,
		CALGray,
		CALRGB,
		LAB,
		ICCBased,
		Separation,
		DeviceN,
		Indexed,
		Pattern,
		_eof,
	};
	/// <summary>
	/// Document information
	/// </summary>
	public enum HPDFInformationTypes : int
	{
		_begin_date = 0,
		CreationDate = _begin_date,
		ModificationDate,
		_end_date,
		_begin_text = _end_date,
		Author = _begin_text,
		Creator,
		Producer,
		Title,
		Subject,
		Keywords,
		_end_text,
		_eof = _end_text,
	};
	/// <summary>
	/// Encryption modes
	/// </summary>
	public enum HPDFEncryptModes : uint
	{
		/// <summary>
		/// Use "Revision 2" algorithm.
		/// The length of key is automatically set to 5(40bit).
		/// </summary>
		R2 = 2,
		/// <summary>
		/// Use "Revision 3" algorithm.
		/// Between 5(40bit) and 16(128bit) can be specified for length of the key.
		/// </summary>
		R3 = 3
	};

	public enum HPDFTextRenderingModes : uint
	{
		Fill = 0,
		Stroke,
		FillThenStroke,
		Invisible,
		FillClipping,
		StrokeClipping,
		FillStrokeClipping,
		Clipping,
		_eof,
	};
	[Flags]
	public enum HPDFGraphicModes : uint
	{
		PageDescription = 0x0001,
		PathObject = 0x0002,
		TextObject = 0x0004,
		ClippingPath = 0x0008,
		Shading = 0x0010,
		InlineImage = 0x0020,
		ExternalObject = 0x0040,
	}
	/// <summary>
	/// End of line shapes
	/// </summary>
	public enum HPDFLineEndShapes : uint
	{
		/// <summary>
		/// The line is squared off at the endpoint of the path
		/// </summary>
		Butt = 0,
		/// <summary>
		/// The end of a line becomes a semicircle whose center is the end point of the path
		/// </summary>
		Round,
		/// <summary>
		/// The line continues to the point that exceeds half of the stroke width of the end point
		/// </summary>
		ProjectingSquare,
		_eof,
	};
	/// <summary>
	/// Shape of 2 lines joining at some point
	/// </summary>
	public enum HPDFLineJoins : uint
	{
		/// <summary>
		/// Square angle \/
		/// </summary>
		Miter = 0,
		/// <summary>
		/// Rounded angle
		/// </summary>
		Round,
		/// <summary>
		/// Cut angle \_/
		/// </summary>
		Bevel,
		_eof,
	};
	/// <summary>
	/// Text alignment
	/// </summary>
	public enum HPDFTextAlignments : uint
	{
		Left = 0,
		Right,
		Center,
		Justify,
	};
	/// <summary>
	/// Transition style in case of slideshow
	/// </summary>
	public enum HPDFTransitionStyles : uint
	{
		WipeRight = 0,
		WipeUp,
		WipeLeft,
		WipeDown,
		BarnDoorsHorizontalOut,
		BarnDoorsHorizontalIn,
		BarnDoorsVerticalOut,
		BarnDoorsVerticalIn,
		BoxOut,
		BoxIn,
		BlindsHorizontal,
		BlindsVertical,
		Dissolve,
		GlitterRight,
		GlitterDown,
		GlitterTopLeftToBottomRight,
		Replace,
		_eof,
	};
	/// <summary>
	/// Pixels blend mode for transparency
	/// </summary>
	public enum HPDFBlendModes : uint
	{
		/// <summary>
		/// Normal mode, selects the source color, ignoring the backdrop
		/// Any new drawing will erase existing items that it draws over
		/// </summary>
		HPDF_BM_NORMAL = 0,
		/// <summary>
		/// Multiplies the backdrop and source color values. 
		/// The result color is always at least as dark as either of the two constituent colors.
		/// Multiplying any color with black produces black; multiplying with white leaves the original color unchanged.
		/// Painting successive overlapping objects with a color other than black or white produces progressively darker colors.
		/// </summary>
		HPDF_BM_MULTIPLY,
		/// <summary>
		/// Multiplies the complements of the backdrop and source color values, then complements the result.
		/// The result color is always at least as light as either of the two constituent colors.
		/// Screening any color with white produces white; screening with black leaves the original color unchanged.
		/// The effect is similar to projecting multiple photographic slides simultaneously onto a single screen
		/// </summary>
		HPDF_BM_SCREEN,
		/// <summary>
		/// Multiplies or screens the colors, depending on the backdrop color value.
		/// Source colors overlay the backdrop while preserving its highlights and shadows.
		/// The backdrop color is not replaced but is mixed with the source color to reflect the lightness or darkness of the backdrop
		/// </summary>
		HPDF_BM_OVERLAY,
		/// <summary>
		/// Selects the darker of the backdrop and source colors.
		/// The backdrop is replaced with the source where the source is darker; otherwise, it is left unchanged.
		/// </summary>
		HPDF_BM_DARKEN,
		/// <summary>
		/// Selects the lighter of the backdrop and source colors.
		/// The backdrop is replaced with the source where the source is lighter; otherwise, it is left unchanged.
		/// </summary>
		HPDF_BM_LIGHTEN,
		/// <summary>
		/// Brightens the backdrop color to reflect the source color.
		/// Painting with black produces no changes.
		/// </summary>
		HPDF_BM_COLOR_DODGE,
		/// <summary>
		/// Darkens the backdrop color to reflect the source color.
		/// Painting with white produces no change.
		/// </summary>
		HPDF_BM_COLOR_BUM,
		/// <summary>
		/// Multiplies or screens the colors, depending on the source color value.
		/// The effect is similar to shining a harsh spotlight on the backdrop.
		/// </summary>
		HPDF_BM_HARD_LIGHT,
		/// <summary>
		/// Darkens or lightens the colors, depending on the source color value.
		/// The effect is similar to shining a diffused spotlight on the backdrop
		/// </summary>
		HPDF_BM_SOFT_LIGHT,
		/// <summary>
		/// Subtracts the darker of the two constituent colors from the lighter color.
		/// Painting with white inverts the backdrop color; painting with black produces no change.
		/// </summary>
		HPDF_BM_DIFFERENCE,
		/// <summary>
		/// Produces an effect similar to that of the Difference mode but lower in contrast. Painting with white inverts the backdrop color; painting with black produces no change.
		/// </summary>
		HPDF_BM_EXCLUSHON,
		_eof,
	};
	/// <summary>
	/// Bevel subtypes
	/// </summary>
	public enum HPDFBSSubtypes : uint
	{
		Solid = 0,
		Dashed,
		Beveled,
		Inset,
		Underlined
	}
	/// <summary>
	/// Supported PDF versions
	/// </summary>
	public enum HPDFSupportedVersions : uint
	{
		V12 = 0,
		V13,
		V14,
		V15,
		V16,
		V17,
		_eof,
	}
	/// <summary>
	/// Viewer preferences
	/// </summary>
	[Flags]
	public enum HPDFViewerPreferences : uint
	{
		HideToolbar = 1,
		HideMenubar = 2,
		HideWindowUI = 4,
		FitWindow = 8,
		CenterWindow = 16,
		PrintScalingNone = 32,
	}
	/// <summary>
	/// Single byte encoders (include Latin, Greek, Thai, Russian, Hebrew, Cyrillic, Arabic)
	/// </summary>
	public enum HPDFSingleByteEncoders : uint
	{
		///<summary>
		/// It is the default encoding of PDF
		///</summary>
		StandardEncoding = 1,
		///<summary>
		/// The standard encoding of Mac OS
		///</summary>
		MacRomanEncoding = 2,
		///<summary>
		/// The standerd encoding of Windwos.
		///</summary>
		WinAnsiEncoding = 3,
		///<summary>
		/// Use the built-in encoding of a font.
		///</summary>
		FontSpecific = 4,
		///<summary>
		/// Latin Alphabet No.2
		///</summary>
		ISO8859_2 = 5,
		///<summary>
		/// Latin Alphabet No.3
		///</summary>
		ISO8859_3 = 6,
		///<summary>
		/// Latin Alphabet No.4
		///</summary>
		ISO8859_4 = 7,
		///<summary>
		/// Latin Cyrillic Alphabet
		///</summary>
		ISO8859_5 = 8,
		///<summary>
		/// Latin Arabic Alphabet
		///</summary>
		ISO8859_6 = 9,
		///<summary>
		/// Latin Greek Alphabet
		///</summary>
		ISO8859_7 = 10,
		///<summary>
		/// Latin Hebrew Alphabet
		///</summary>
		ISO8859_8 = 11,
		///<summary>
		/// Latin Alphabet No. 5
		///</summary>
		ISO8859_9 = 12,
		///<summary>
		/// Latin Alphabet No. 6
		///</summary>
		ISO8859_10 = 13,
		///<summary>
		/// Thai, TIS 620-2569 character set
		///</summary>
		ISO8859_11 = 14,
		///<summary>
		/// Latin Alphabet No. 7
		///</summary>
		ISO8859_13 = 15,
		///<summary>
		/// Latin Alphabet No. 8
		///</summary>
		ISO8859_14 = 16,
		///<summary>
		/// Latin Alphabet No. 9
		///</summary>
		ISO8859_15 = 17,
		///<summary>
		/// Latin Alphabet No. 10
		///</summary>
		ISO8859_16 = 18,
		///<summary>
		/// Microsoft Windows Codepage 1250 (EE)
		///</summary>
		CP1250 = 19,
		///<summary>
		/// Microsoft Windows Codepage 1251 (Cyrl)
		///</summary>
		CP1251 = 20,
		///<summary>
		/// Microsoft Windows Codepage 1252 (ANSI)
		///</summary>
		CP1252 = 21,
		///<summary>
		/// Microsoft Windows Codepage 1253 (Greek)
		///</summary>
		CP1253 = 22,
		///<summary>
		/// Microsoft Windows Codepage 1254 (Turk)
		///</summary>
		CP1254 = 23,
		///<summary>
		/// Microsoft Windows Codepage 1255 (Hebr)
		///</summary>
		CP1255 = 24,
		///<summary>
		/// Microsoft Windows Codepage 1256 (Arab
		///</summary>
		CP1256 = 25,
		///<summary>
		/// Microsoft Windows Codepage 1257 (BaltRim)
		///</summary>
		CP1257 = 26,
		///<summary>
		/// Microsoft Windows Codepage 1258 (Viet)
		///</summary>
		CP1258 = 27,
		///<summary>
		/// Russian Net Character Set
		///</summary>
		KOI8_R = 28,
	}
	/// <summary>
	/// Multi bytes encoders (Japanese, simplified Chinese, traditional Chinese, Korean)
	/// </summary>
	public enum HPDFMultiBytesEncoders
	{
		///<summary>
		/// EUC-CN encoding
		///</summary>
		GB_EUC_H = 1,
		///<summary>
		/// Vertical writing virsion of GB-EUC-H
		///</summary>
		GB_EUC_V = 2,
		///<summary>
		/// Microsoft Code Page 936 (lfCharSet 0x86) GBK encoding
		///</summary>
		GBK_EUC_H = 3,
		///<summary>
		/// Vertical writing virsion of GBK-EUC-H
		///</summary>
		GBK_EUC_V = 4,
		///<summary>
		/// Microsoft Code Page 950 (lfCharSet 0x88) Big Five character set with ETen extensions
		///</summary>
		ETen_B5_H = 5,
		///<summary>
		/// Vertical writing virsion of ETen-B5-H
		///</summary>
		ETen_B5_V = 6,
		///<summary>
		/// Microsoft Code Page 932, JIS X 0208 character
		///</summary>
		_90ms_RKSJ_H = 7,
		///<summary>
		/// Vertical writing virsion of 90ms-RKSJ-V
		///</summary>
		_90ms_RKSJ_V = 8,
		///<summary>
		/// Microsoft Code Page 932, JIS X 0208 character (proportional)
		///</summary>
		_90msp_RKSJ_H = 9,
		///<summary>
		/// JIS X 0208 character set, EUC-JP encoding
		///</summary>
		EUC_H = 10,
		///<summary>
		/// Vertical writing virsion of EUC-H
		///</summary>
		EUC_V = 11,
		///<summary>
		/// KS X 1001:1992 character set, EUC-KR encoding
		///</summary>
		KSC_EUC_H = 12,
		///<summary>
		/// Vertical writing virsion of KSC-EUC-V
		///</summary>
		KSC_EUC_V = 13,
		///<summary>
		/// Microsoft Code Page 949 (lfCharSet 0x81), KS X 1001:1992 character set plus 8822 additional hangul, Unified Hangul Code (UHC) encoding (proportional)
		///</summary>
		KSCms_UHC_H = 14,
		///<summary>
		/// Microsoft Code Page 949 (lfCharSet 0x81), KS X 1001:1992 character set plus 8822 additional hangul, Unified Hangul Code (UHC) encoding (fixed width)
		///</summary>
		KSCms_UHC_HW_H = 15,
		///<summary>
		/// Vertical writing virsion of KSCms-UHC-HW-H
		///</summary>
		KSCms_UHC_HW_V = 16,
	}
	/// <summary>
	/// Font types that can be used
	/// </summary>
	public enum HPDFFontTypes : uint
	{
		///<summary>
		/// It is builtin font of PDF. This font can be used for all viewer applications.
		///</summary>
		Base14 = 1,
		///<summary>
		/// One of font formats used by PostScript.
		///</summary>
		Type1 = 2,
		///<summary>
		/// Outline font fotmat widely used.
		///</summary>
		TrueType = 3,
		///<summary>
		/// Font format for multi byte character developed by Adobe.
		///</summary>
		CID = 4,
	}
	#endregion

	#region structs
	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFRectStruct
	{
		public HPDFRectStruct(HPDFRectStruct o) { Left = o.Left; Bottom = o.Bottom; Right = o.Right; Top = o.Top; }
		public float Left;
		public float Bottom;
		public float Right;
		public float Top;
		public bool IsNull { get => 0 == Left && 0 == Bottom && 0 == Right && 0 == Top; }
		public float Width { get => Right - Left; }
		public float Height { get => Top - Bottom; }
		public void Reset() { Left = Bottom = Right = Top = 0F; }
		public override string ToString() { return $"(L:{Left}, T:{Top}, R:{Right}, B:{Bottom})"; }
	}

	//[StructLayout(LayoutKind.Sequential)]
	//public struct HPDFBBoxStruct
	//{
	//	public HPDFBBoxStruct(HPDFBBoxStruct o) { Left = o.Left; Bottom = o.Bottom; Right = o.Right; Top = o.Top; }
	//	public float Left;
	//	public float Bottom;
	//	public float Right;
	//	public float Top;
	//	public bool IsNull { get => 0 == Left && 0 == Bottom && 0 == Right && 0 == Top; }
	//	public float Width { get => Right - Left; }
	//	public float Height { get => Top - Bottom; }
	//	public override string ToString() { return $"(L:{Left}, T:{Top}, R:{Right}, B:{Bottom})"; }
	//}

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFPointStruct
	{
		public HPDFPointStruct(HPDFPointStruct o) { x = o.x; y = o.y; }
		public float x;
		public float y;
		public bool IsNull { get => 0 == x && 0 == y; }
		public void AddX(float v) { x += v; }
		public void DimX(float v) { x -= v; }
		public void AddY(float v) { y += v; }
		public void DimY(float v) { y -= v; }
		public void Reset() { x = y = 0F; }
		public override string ToString() { return $"(X:{x}, Y:{y})"; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFSizeStruct
	{
		public HPDFSizeStruct(HPDFSizeStruct o) { Width = o.Width; Height = o.Height; }
		public float Width;
		public float Height;
		public bool IsNull { get => 0 == Width && 0 == Height; }
		public void AddWidth(float v) { Width += v; }
		public void DimWidth(float v) { Width -= v; }
		public void AddHeight(float v) { Height += v; }
		public void DimHeight(float v) { Height -= v; }
		public void Reset() { Width = Height = 0F; }
		public override string ToString() { return $"(W:{Width}, H:{Height})"; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFDateStruct
	{
		public int Year;
		public int Month;
		public int Day;
		public int Hour;
		public int Minutes;
		public int Seconds;
		public char Indicator;
		public int OffsetHour;
		public int OffsetMinutes;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFTextWidthStruct
	{
		/// <summary>
		/// number of considered characters inside the text
		/// </summary>
		public uint Numchars;

		/// <summary>
		/// number of words inside the text
		/// </summary>
		/* don't use this value (it may be change in the feature), use numspace as alternated. */
		public uint Numwords;

		/// <summary>
		/// width of the text in the current font
		/// </summary>
		public uint Width;
		/// <summary>
		/// Number of spaces inside the text
		/// </summary>
		public uint Numspace;
		public bool IsNull { get => 0 == Numchars && 0 == Numwords && 0 == Width && 0 == Numspace; }
		/// <summary>
		/// Width of text in points
		/// </summary>
		/// <param name="font_size"></param>
		/// <returns></returns>
		public float ActualWidth(float font_size) { return Width * font_size / 1000; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFTransformationMatrixStruct
	{
		public float a;
		public float b;
		public float c;
		public float d;
		public float x;
		public float y;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFDashModeStructInternalStruct
	{
		public ushort ptn0;
		public ushort ptn1;
		public ushort ptn2;
		public ushort ptn3;
		public ushort ptn4;
		public ushort ptn5;
		public ushort ptn6;
		public ushort ptn7;
		public uint num_ptn;
		public uint phase;
	};

	public struct HPDFDashModeStruct
	{
		public ushort[] ptn;
		public uint phase;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFRGBColorStruct
	{
		public float R;
		public float G;
		public float B;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct HPDFCMYKColorStruct
	{
		public float C;
		public float Y;
		public float M;
		public float K;
	};

	public struct HPDFException
	{
		public uint error_no;
		public uint detail_no;
		public IntPtr user_data;
		public string message;
	}
	#endregion

	#region error handler delegate
	/* error handler (call back function) */
	delegate void HPDFErrorHandler(uint error_no, uint detail_no, IntPtr user_data);
	public delegate void HPDFCallback(HPDFException exception);
	#endregion

	public static class StandardFonts
	{
		public const string Courier = "Courier";
		public const string CourierBold = "Courier-Bold";
		public const string CourierOblique = "Courier-Oblique";
		public const string CourierBoldOblique = "Courier-BoldOblique";
		public const string Helvetica = "Helvetica";
		public const string HelveticaBold = "Helvetica-Bold";
		public const string HelveticaOblique = "Helvetica-Oblique";
		public const string HelveticaBoldOblique = "Helvetica-BoldOblique";
		public const string TimesRoman = "Times-Roman";
		public const string TimesBold = "Times-Bold";
		public const string TimesItalic = "Times-Italic";
		public const string TimesBoldItalic = "Times-BoldItalic";
		public const string Symbol = "Symbol";
		public const string ZapfDingbats = "ZapfDingbats";
	}

	static class HPDFTools
	{
		#region implementation
		public static StringBuilder StringBuilder(string s)
		{
			return null == s ? null : new StringBuilder(s);
		}
		#endregion
	}

	public abstract class HPDFClass
	{
		public HPDFClass() { }
		protected const string LIB_NAME = "libhpdf";
		protected const string DLL_NAME = "libhpdf.dll";

		#region imports
		[DllImport(DLL_NAME, SetLastError = true)] //, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		private static extern IntPtr HPDF_GetVersion();
		#endregion

		#region properties
		public static bool IsReady
		{
			get
			{
				try
				{
					string s = Version;
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
		}
		public static string Dll { get => DLL_NAME; }
		public static string Library { get => LIB_NAME; }
		public static string Version
		{
			get
			{
				try
				{
					return $"{System.Runtime.InteropServices.Marshal.PtrToStringAnsi(HPDF_GetVersion())}";
				}
				catch (Exception ex)
				{
				}
				return Resources.LibraryName.Replace(@"%1", Dll);
			}
		}
		/// <summary>
		/// Last error encountered
		/// </summary>
		public uint LastError { get; protected set; }
		#endregion

		#region methods
		/// <summary>
		/// Get the error description
		/// </summary>
		/// <param name="last_error">The error to get the description of; if not specified the description as pointed by <see cref="LastError"/></param>
		public string LastErrorAsString(uint last_error = 0)
		{
			HPDFErrors error = (HPDFErrors)((uint)HPDFErrors.HPDF_NO_ERROR == last_error ? LastError : last_error);
			if (Enum.IsDefined(typeof(HPDFErrors), error))
				return error.ToString();
			return HPDFErrors.HPDF_UNKNOWN_ERROR.ToString();
		}
		#endregion

		#region default values
		public const float HPDF_DEF_LINEWIDTH = 1F;
		public const float HPDF_DEF_MITERLIMIT = 10F;
		public const float HPDF_MIN_MITERLIMIT = 1F;
		public const float HPDF_DEF_FLATNESS = 1F;
		public const float HPDF_DEF_HSCALING = 100F;
		public const float HPDF_MIN_CHARSPACE = -30F;
		public const float HPDF_MAX_CHARSPACE = 300F;
		public const float HPDF_MIN_WORDSPACE = -30F;
		public const float HPDF_MAX_WORDSPACE = 300F;
		public const float HPDF_MIN_HSCALING = 10F;
		public const float HPDF_MAX_HSCALING = 300F;
		public const float HPDF_MAX_FONTSIZE = 600F;

		protected const int HPDF_TRUE = 1;
		protected const int HPDF_FALSE = 0;
		#endregion
	}
}

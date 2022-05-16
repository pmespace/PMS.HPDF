This package implements LIBHPDF and LIBPNG on .NET
All functions available in LIBHPDF can be reused through the use of PMS.LIBHPDF

This library embeds services from
- LIBHARU v2.3.0
- LIBPNG v1.6
- ZLIB v1.2.12

PMS.HPDF is a c# class implementing the PDF services offered inside LIBHARU.
The class itself was created from the hpdf.cs contained in the free distribution of LIBHARU, with some modifications (some function names have changed, a function was added to LIBHPDF -in hpdf_encoder.c, new properties,...) and fixes that made the library unusable (management of returned strings).
The document about LIBHARU can be found at http://libharu.sourceforge.net/documentation.html

Note that the library comes with a DLL to be deployed with (LIBHPDF.DLL) which contains all ZLIB, LIBPNG and LIBHARU services. Installing the nuget package will install the DLL.

I will soon try to briefly describe how to do basic tasks to create a PDF
To be continued...
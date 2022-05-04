using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPDF;

namespace HPDF.DEMO
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FLibhpdf());
		}

		public static bool SaveAndStart(HPDFDocument pdf, string s)
		{
			bool ret = pdf.SaveToFile(s, out bool exists, true);
			if (ret)
			{
				FileInfo fi = new FileInfo(s);
				ProcessStartInfo si = new ProcessStartInfo();
				si.UseShellExecute = true;
				si.FileName = fi.FullName;
				si.Verb = "Open";
				Process.Start(si);
			}
			return ret;
		}
	}
}

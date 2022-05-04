using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPDF;

namespace HPDF.DEMO
{
	public partial class FLibhpdf : Form
	{
		public FLibhpdf()
		{
			InitializeComponent();
		}

		private void RAZ()
		{
			textBox1.Text = null;
		}

		public void ADD(string s)
		{
			textBox1.Text += s + "\r\n";
		}

		private void FLibhpdf_Load(object sender, EventArgs e)
		{
			if (HPDFClass.IsReady)
				Text = $"{Text} - Using {HPDFClass.Library} v{HPDFClass.Version}";
		}

		private void pbClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void pbImage_Click(object sender, EventArgs e)
		{
			RAZ();
			Image.Demo(this);
		}

		private void pbPermission_Click(object sender, EventArgs e)
		{
			RAZ();
			Permission.Demo(this);
		}

		private void pbEncryption_Click(object sender, EventArgs e)
		{
			RAZ();
			Encryption.Demo(this);
		}

		private void pbFont_Click(object sender, EventArgs e)
		{
			RAZ();
			PDFFont.Demo(this);
		}

		private void pbJPFont_Click(object sender, EventArgs e)
		{
			RAZ();
			JPFont.Demo(this);
		}

		private void pbLine_Click(object sender, EventArgs e)
		{
			RAZ();
			Line.Demo(this);
		}

		private void pbOutline_Click(object sender, EventArgs e)
		{
			RAZ();
			Outline.Demo(this);
		}

		private void pbRawImage_Click(object sender, EventArgs e)
		{
			RAZ();
			RawImage.Demo(this);
		}

		private void pbSLideShow_Click(object sender, EventArgs e)
		{
			RAZ();
			SlideShow.Demo(this);
		}
	}
}

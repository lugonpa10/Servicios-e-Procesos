using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       private async Task<string> DownloadFileAsync(string fileName,int delayMs)
        {
            await Task.Delay(delayMs);
            return $"File {fileName} downloaded in {delayMs}";

        }

        private int NumRandom()
        {
            Random rd = new Random();
            return rd.Next(1000, 10000);
        }

        private async Task empezarDescarga()
        {
            string archivo = txtFileName.Text;
            if (string.IsNullOrEmpty(archivo))
            {
                MessageBox.Show("Necesitas introducir un archivo", "Error entrada datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            int delay = NumRandom();

            Task<string> descarga = DownloadFileAsync(archivo, delay);

            string resultado = await descarga;

            txtResults.AppendText(resultado + "\n");

        }

        private async void btnDescarga_Click(object sender, EventArgs e)
        {
            await empezarDescarga();
        }
    }
}

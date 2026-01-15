using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Cliente
{
    public partial class Form1 : Form//   Una funciona para all, time date.  tABORDER. rANGO DE PUERTOS. overflow
    {
        public Form1()
        {
            InitializeComponent();
            btnTime.Text = "time";
            btnAll.Text = "all";
            btnDate.Text = "date";
        }
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int puerto = 31416;

        private async Task<string> comunicacionAsync(string comando)
        {
            try
            {
                using (Socket conexion = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint ep = new IPEndPoint(ip, puerto);
                    await conexion.ConnectAsync(ep);
                    Encoding codificacion = Console.OutputEncoding;
                    using (NetworkStream ns = new NetworkStream(conexion))
                    using (StreamReader sr = new StreamReader(ns, codificacion))
                    using (StreamWriter sw = new StreamWriter(ns, codificacion))
                    {
                        sw.AutoFlush = true;
                        string mensaje = await sr.ReadLineAsync();
                        await sw.WriteLineAsync(comando);
                        mensaje = await sr.ReadLineAsync();
                        return mensaje;
                    }

                }
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                return ex.Message;

            }
            catch (Exception ex)
            {
                return "Error inesperado";
            }
        }

        private async void envioDeComandos(object sender, EventArgs e)//Usar  sender con text o tag (1 linea)
        {
            lblResul.Text = await comunicacionAsync(((Button)sender).Text);
        }
        private async void btnClose_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                lblResul.Text = await comunicacionAsync("close");
            }
            else
            {
                lblResul.Text = await comunicacionAsync($"close {txtPassword.Text}");

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.txtIp.Text = ip.ToString();
            f2.txtPuerto.Text = puerto.ToString();
            DialogResult res;
            bool datosCorrectos = true;
            res = f2.ShowDialog();

            if (res == DialogResult.Cancel)
            {
                MessageBox.Show("No se han guardado los cambios", "Ip + Puerto",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (res == DialogResult.OK)
            {
                int puertoMaximo = IPEndPoint.MaxPort;

                if (!IPAddress.TryParse(f2.txtIp.Text, out IPAddress ipValidar))
                {
                    MessageBox.Show("La IP no es valida", "Ip + Puerto",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    datosCorrectos = false;
                }
                if (!int.TryParse(f2.txtPuerto.Text, out int puertoValidar))
                {
                    MessageBox.Show("El puerto no es valido", "Ip + Puerto",
             MessageBoxButtons.OK, MessageBoxIcon.Error);
                    datosCorrectos = false;

                }
                if (puertoValidar < 0 || puertoValidar > puertoMaximo)
                {
                    MessageBox.Show("Fuera de rango", "Ip + Puerto",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                    datosCorrectos = false;

                }
                if (datosCorrectos)
                {
                    ip = ipValidar;
                    puerto = puertoValidar;

                }
            }
        }
    }
}

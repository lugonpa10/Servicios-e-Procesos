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

namespace Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int puerto = 31416;

        private async Task<string> comunicacionAsync()
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


        private void envioDatos(object sender, EventArgs e)
        {
          

        }
        private async Task btnTime_Click(object sender, EventArgs e)
        {
            string hora = DateTime.Now.ToString("T");
            hora = await comunicacionAsync();
            lblResul.Text = hora;

        }
    }
}

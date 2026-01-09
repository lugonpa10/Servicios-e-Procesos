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

namespace ClienteEco
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int puerto = 31416;

        private async Task<string> EnvioYRecepcionAsync()
        {

            try
            {
                // Se crea el socket para conectarse al servidor
                using (Socket conexion = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint ep = new IPEndPoint(ip, puerto);
                    // Trata de conectarse a la IP y Puerto configurados.
                    await conexion.ConnectAsync(ep);
                    // Si hay exito (si no salta excepción), se procede al protocolo:
                    // - Recepción de mensaje de bienvenida.
                    // - Envío de texto desde txtEnviar
                    // - Recpción de mensaje de servidor
                    Encoding codificacion = Console.OutputEncoding;
                    using (NetworkStream ns = new NetworkStream(conexion))
                    using (StreamReader sr = new StreamReader(ns, codificacion))
                    using (StreamWriter sw = new StreamWriter(ns, codificacion))
                    {
                        sw.AutoFlush = true;

                        // Leemos mensaje de bienvenida y lo deshechamos
                        // (No hacemos nada con él).
                        string msg = await sr.ReadLineAsync();
                        // Enviamos dato
                        await sw.WriteLineAsync(txtEnviar.Text);
                        // Esperamos recepción
                        msg = await sr.ReadLineAsync();
                        return msg;
                    }
                }
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                // Problema de comunicación:
                // no existe servidor, no responde, red caida,...
                return ex.Message;
            }
            catch (Exception ex)
            {
                return $"Error inesperado: {ex.GetType().Name}. Contacte con soporte.";
            }
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            // Desactivamos el botón hasta que se reciba el mensaje (no es imprescindible)
            btnEnviar.Enabled = false;
            string msg = await EnvioYRecepcionAsync();
            // Tras la recepción mostramos en txtRecibir el mensaje recibido.
            txtRecibir.AppendText(msg + Environment.NewLine);
            btnEnviar.Enabled = true;
        }
    }
}


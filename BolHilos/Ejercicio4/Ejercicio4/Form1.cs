using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string BuscaPalabra(string ruta, string cadena)
        {
            string contenido;
            int cont = 0;

            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    contenido = sr.ReadToEnd();
                    for (int i = 0; i <= contenido.Length - cadena.Length; i++)
                    {
                        string comprobacion = contenido.Substring(i, cadena.Length);
                        if (comprobacion == cadena)
                        {
                            cont++;
                        }
                    }
                }


            }
            catch (IOException)
            {

            }
            string nombreArchivo = Path.GetFileName(ruta);

            return $"{nombreArchivo}: la palabra {cadena} aparece {cont} veces";
        }
        List<Task<string>> tareas = new List<Task<string>>();
        private async void BtnBusqueda_Click(object sender, EventArgs e)
        {
            string directorio = txtUrl.Text;
            string[] archivos = Directory.GetFiles(directorio, "*.txt");
            try
            {
                foreach (var archivo in archivos)
                {
                    tareas.Add(Task.Run(() => BuscaPalabra(archivo, txt1.Text.Trim())));
                }

                Task<string> primeraTarea = await Task.WhenAny(tareas);
                string resultado = await primeraTarea;
                listResultados.Items.Add(resultado + Environment.NewLine);


            }
            catch (IOException)
            {

            }

        }


        private async Task<string> BuscaPosicion(string ruta, string cadena)
        {
            int posPalabra = 0;
            string contenido;
            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    contenido = sr.ReadToEnd();
                  
                   
                }
                Random rd = new Random();

                await Task.Delay(rd.Next(1, 50));

                for (int i = 0; i < contenido.Length - cadena.Length; i++)
                {
                    if(contenido.Substring(i,cadena.Length) == cadena)
                    {
                        posPalabra = i;
                    }
                }




               
            }
            catch (IOException)
            {

            }
                string nombreArchivo = Path.GetFileName(ruta);
            if(posPalabra == -1)
            {
                return "NO";
            }
            else
            {
                return $"{nombreArchivo} : {cadena} aparecen en posicion {posPalabra}";
            }
        }

        private void btnPosicion_Click(object sender, EventArgs e)
        {

        }
    }
}

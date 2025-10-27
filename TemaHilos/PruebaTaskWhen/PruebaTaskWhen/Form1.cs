using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaTaskWhen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int SlowFind(int num, byte[] vector)
        {
            Random g = new Random();
            int position;
            // Busqueda mediante índices aleatorios
            // La velocidad depende de la suerte
            do
            {
                position = g.Next(vector.Length);
            } while (num != vector[position]);
            return position;
        }
        public static int SlowFindSleep(int num, byte[] vector)
        {
            Random g = new Random();
            int position = 0;
            // Búsqueda lineal haciendo pausa (liberando la CPU)
            // en cada vuelta del bucle
            while (position < vector.Length && num != vector[position++])
            {
                Task.Delay(0).Wait();
            }
            return position;
        }
        // Genera vector con 100000000 de ceros y un 1 en una posición aleatoria
        // Se realiza de tipo byte y no int para ocupar 100MB en lugar de 400MB
        public byte[] generaVector()
        {
            Task.Delay(1).Wait(); // Evita mismo new Random() en sucesivas llamadas.
            Random g = new Random();
            byte[] v = new byte[100000000];
            v[g.Next(v.Length)] = 1;
            return v;
        }

        // Función que busca el 1 en todos los vectores
        // de forma paralela usando WhenAll para la espera.
        public async void InitAll()
        {
            txtResultados.Text = $"Buscando en todos los vectores";
            btnAll.Enabled = false;
            byte[] v1 = generaVector();
            byte[] v2 = generaVector();
            byte[] v3 = generaVector();
            Task<int> task1 = Task.Run(() => SlowFind(1, v1));
            Task<int> task2 = Task.Run(() => SlowFind(1, v2));
            Task<int> task3 = Task.Run(() => SlowFind(1, v3));
            // En esta línea se libera el bloqueo para hacer otras tareas
            // y solo se continua cuando han acanbado las 3 tareas.
            int[] tiempos = await Task.WhenAll(task1, task2, task3);
            btnAll.Enabled = true;
            txtResultados.Text = $"Resultados WhenAll{Environment.NewLine}";
            for (int i = 0; i < tiempos.Length; i++)
            {
                txtResultados.AppendText($"Posición en v[{i}]: {tiempos[i]}" +
                $"{Environment.NewLine}");
            }
        }


        // Función que busca la posición del 1
        // de un vector mediante el método más rápido
        public async Task<string> InitAny()
        {
            txtResultados.Text = $"Buscando el método maás rápido";
            Random g = new Random();
            byte[] v = generaVector(); ;
            Task<string> task1 = Task.Run(() => $"SlowFind: {SlowFind(1, v)}");
            Task<string> task2 = Task.Run(() => $"SlowFindSleep: {SlowFindSleep(1, v)}");
            // Si a IndexOf le pasas 1 sin casting busca un integer que no existe.
            Task<string> task3 = Task.Run(() => $"IndexOf: {Array.IndexOf(v, (byte)1)}");
            // Hay doble await porque WhenAny es del tipo Task<Task<string>>
            // - Task interior: representa "la tarea que terminó primero"
            // - Task exterior: representa "El dato que contiene esa tarea"
            Task<string> tareaFinal = await Task.WhenAny(task3, task2, task1);
            return await tareaFinal;
        }
        private async void btnAny_Click(object sender, EventArgs e)
        {
            string ganador = await InitAny();
            txtResultados.Text = $"Resultados WhenAny{Environment.NewLine}{ganador}";
        }


        private void btnColor_Click(object sender, EventArgs e)
        {
            Random g = new Random();
            this.BackColor = Color.FromArgb(255, g.Next(256), g.Next(256), g.Next(256));
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            InitAll();
        }
    }
}

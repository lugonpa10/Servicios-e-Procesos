using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    internal class BackUp
    {
        private List<string> log = new List<string>();
        public List<string> Log
        {
            get
            {
                return Log;
            }
        }

        public FileInfo[] buscaArchivos(string directorio, string[] extensiones)
        {
            FileInfo[] archivos = null;
            if (Directory.Exists(directorio))
            {
                DirectoryInfo d = new DirectoryInfo(directorio);
                archivos = Array.FindAll(d.GetFiles(), f => extensiones.Contains(f.Extension));
            }

            return archivos;
        }

        public bool copiaArchivo(FileInfo archivoCopiar, string directorioDestino)
        {
            try
            {
                using (StreamReader sr = new StreamReader(archivoCopiar.FullName))
                using (StreamWriter sw = new StreamWriter($"{directorioDestino}\\{archivoCopiar}"))
                {
                    string linea;
                    int cont = 0;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        sw.Write(linea);
                        cont++;
                    }
                    string entradaLog = $"{archivoCopiar.Name,20} {archivoCopiar.Length,10} {cont,5}";
                    log.Add(entradaLog);
                    Console.WriteLine(entradaLog);
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
        public int buscaYCopia(string directorioOrigen, string directorioDestino, string[] extensiones)
        {
            FileInfo[] archivos = buscaArchivos(directorioOrigen, extensiones);
            int cont = 0;
            Array.ForEach(archivos, arch => cont += copiaArchivo(arch, directorioDestino) ? 1 : 0);
            return cont;
        }

        public async void InitBackup(string directorioOrigen)
        {
            string directorioDestino = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\backup";
            Task<int> tarea1 = Task.Run(() => buscaYCopia(directorioOrigen, directorioDestino, new string[] { ".txt", ".md", ".json" }));

        }
    }

}

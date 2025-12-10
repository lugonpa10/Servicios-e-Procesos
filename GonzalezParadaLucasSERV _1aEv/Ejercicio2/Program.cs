namespace Ejercicio2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            BackUp backUp = new BackUp();
             int cont = await backUp.InitBackup($"{Environment.GetEnvironmentVariable("USERPROFILE")}\\examen");

        }
    }
}

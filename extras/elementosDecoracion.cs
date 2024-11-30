using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.extras
{
    //Clase que se usa para resumir cosas en comun de decoracion para evitar la continuidad en el code 
    public class elementosDecoracion
    {
        //                       Encabezado  
        public static void encabezado()
        {
            string encabezado = @"════════════════════════════════════════════════════════════════════════════════════════════════════";
            Console.WriteLine("\n" + encabezado, Console.ForegroundColor = ConsoleColor.DarkGreen);
        }
        //
        ///                         Orden y Tabulaciones
        //

        /// <summary>
        /// 3 clases Rojo Azul Verde
        /// </summary>
        //Metodo para tabular y decorar camnbia el colo a rojo y lo tabula 
        public static void tabularMenuRojo(string prefix, string text)
        {
            Console.Write("[", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write(prefix, Console.ForegroundColor = ConsoleColor.White);
            Console.Write("]", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write(" " + text, Console.ForegroundColor = ConsoleColor.White);
        }
        //
        //Metodo para tabular y decorar cambia de color a azul y lo tabula
        public static void tabularMenuAzul(string prefix, string text)
        {
            Console.Write("[", Console.ForegroundColor = ConsoleColor.Blue);
            Console.Write(prefix, Console.ForegroundColor = ConsoleColor.White);
            Console.Write("]", Console.ForegroundColor = ConsoleColor.Blue);
            Console.Write(" " + text, Console.ForegroundColor = ConsoleColor.White);
        }
        //
        //Metodo para tabular y decorar cambia de color a verde y lo tabula
        public static void tabularMenuVerde(string prefix, string text)
        {
            Console.Write("[", Console.ForegroundColor = ConsoleColor.Green);
            Console.Write(prefix, Console.ForegroundColor = ConsoleColor.White);
            Console.Write("]", Console.ForegroundColor = ConsoleColor.Green);
            Console.Write(" " + text, Console.ForegroundColor = ConsoleColor.White);
        }
     

        /// <summary>
        /// 4 clases azul darkcyan amarillo rojo
        /// </summary>
        public static void pintarGrafosAzul(string text)
        {
            Console.WriteLine(text, Console.ForegroundColor = ConsoleColor.Blue);
        }
        //
        public static void pintarGrafosCyan(string text)
        {
            Console.WriteLine(text, Console.ForegroundColor = ConsoleColor.DarkCyan);
        }
        ///
        public static void pintarGrafosAmarillo(string text)
        {
            Console.WriteLine(text, Console.ForegroundColor = ConsoleColor.Yellow);
        }
        ///  
        public static void pintarGrafosRojo(string text)
        {
            Console.WriteLine(text, Console.ForegroundColor = ConsoleColor.Red);
        }


        public static void pintarLetrasAzul(string pre, string text)
        {
            Console.Write(pre, Console.ForegroundColor = ConsoleColor.Blue);
            Console.Write(text, Console.ForegroundColor = ConsoleColor.White);
        }

        public static void pintarLetrasAmarillo(string pre, string text)
        {
            Console.Write(pre, Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write(text, Console.ForegroundColor = ConsoleColor.White);
        }
        public static void pintarLetrasCyan(string pre, string text)
        {
            Console.Write(pre, Console.ForegroundColor = ConsoleColor.Cyan);
            Console.Write(text, Console.ForegroundColor = ConsoleColor.White);
        }
        /// 
        //Metodo para imprimir un logo visiblemente estetico del logo principal de la empresa
        public static void logoEmpresa()
        {
            string logo = @"

            ::██::::'██:'██::::'██:'██:::::::'████████:'████::'██████:::::'███::::'██:::::::'██::::'██:'████████:::
           :::███::'███: ██:::: ██: ██:::::::... ██..::. ██::'██... ██:::'██ ██::: ██::::::: ██:::: ██: ██.... ██:::
          ::::████'████: ██:::: ██: ██:::::::::: ██::::: ██:: ██:::..:::'██:. ██:: ██::::::: ██:::: ██: ██:::: ██::::
         :::::██ ███ ██: ██:::: ██: ██:::::::::: ██::::: ██::. ██████::'██:::. ██: ██::::::: ██:::: ██: ██:::: ██:::::
         :::::██. █: ██: ██:::: ██: ██:::::::::: ██::::: ██:::..... ██: █████████: ██::::::: ██:::: ██: ██:::: ██:::::
          ::::██:.:: ██: ██:::: ██: ██:::::::::: ██::::: ██::'██::: ██: ██.... ██: ██::::::: ██:::: ██: ██:::: ██::::
           :::██:::: ██:. ███████:: █████████::: ██::::'████:. ██████:: ██:::: ██: ████████:. ███████:: ████████::::
            ::..:::::..:::.......:::........:::::..:::::....:::......:::..:::::..::........:::.......:::........:::
";
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.DarkBlue);
            Console.WriteLine(logo, Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.DarkBlue);
        }

       
    }
}

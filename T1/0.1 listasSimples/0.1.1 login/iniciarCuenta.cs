using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.extras;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._0_inventarioListaCircular;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._1_reporteListaCircular;
using T1_Gestor_Medico_de_Referencias.T2._1._2_pilas;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._0_ingresoAmbulanciasColaSinPrioridad;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_atencionAlClienteColaConPrioridad;
using T1_Gestor_Medico_de_Referencias.T3._2._1_arbol_Referencia;

namespace T1_Gestor_Medico_de_Referencias.T1._0._1_listasSimples._0._1._1_login
{
    public class iniciarCuenta
    {
        static void mostrarBarraDeCarga(int totalWidth, int delay, char progressChar, char emptyChar)
        {
            Console.WriteLine("\n\n");

            int progressWidth = 0; // Ancho actual de la barra de progreso


            Console.CursorVisible = false; // Ocultar el cursor para una mejor visualización

            for (int i = 0; i <= totalWidth; i++)
            {
                // Calcular el porcentaje de progreso
                progressWidth = i;

                // Crear la barra de carga
                string progressBar = new string(progressChar, progressWidth) + new string(emptyChar, totalWidth - progressWidth);

                // Escribir la barra de carga en la consola
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"\t\t\t[{progressBar}] {i * 100 / totalWidth}%");

                // Esperar antes de la siguiente actualización
                Thread.Sleep(delay);
            }
        }

        public static void IniciarSesion(listaSimplePaciente ListaSimplePaciente
            , listaSimpleCreador ListaSimpleCreador
            , listaDobleTrabajadores ListaDobleTrabajadores
            , listaDobleSedes ListaDobleSedes
            , listaDobleAmbulancias listaDobleAmbulancias
            , listaCircularInventario listaCircularInventario
            , listaCircularReporte listaCircularReporte
            , pilaPacientes PilaPacientes,pilaTrabajadores PilaTrabajadores,pilaAlmacen PilaAlmacen,pilaSedes PilaSedes,pilaAmbulancias PilaAmbulancias
            , colaIngresoDeAmbulancias ColaSinPrioridad
            , colaAlmacen ColaConPrioridad
            , arbolVacunacion ArbolVacunacion)
        {
            while (true)
            {
                Console.Clear();
                elementosDecoracion.logoEmpresa();
                Console.WriteLine();
                Console.Write("".PadLeft(51)); elementosDecoracion.tabularMenuRojo("Usuario", "      ");
                string usuario = Console.ReadLine();
                Console.WriteLine();
                Console.Write("".PadLeft(51)); elementosDecoracion.tabularMenuRojo("Contraseña", "   ");
                string contraseña = LeerContraseña();
                nodoCreador usuarioAutenticado = ListaSimpleCreador.ObtenerUsuario(usuario, contraseña);

                if (usuarioAutenticado != null)
                {
                    Console.WriteLine("\n\n");
                    Console.Write("".PadLeft(48)); Console.WriteLine("╔══════════════════════════╗");
                    Console.Write("".PadLeft(48)); Console.WriteLine("║ Inicio de sesión exitoso ║");
                    Console.Write("".PadLeft(48)); Console.WriteLine("╚══════════════════════════╝");
                    mostrarBarraDeCarga(70, 2, '█', '-');
                    Program.menuPrincipal(ListaSimplePaciente
                                         ,ListaSimpleCreador
                                         ,ListaDobleTrabajadores
                                         ,ListaDobleSedes
                                         ,listaDobleAmbulancias
                                         ,listaCircularInventario
                                         ,listaCircularReporte
                                         ,PilaPacientes, PilaTrabajadores,PilaAlmacen, PilaSedes,PilaAmbulancias
                                         ,ColaSinPrioridad
                                         ,ColaConPrioridad
                                         ,ArbolVacunacion);


                }
                else
                {
                    Console.WriteLine("\n\n");
                    Console.Write("".PadLeft(48)); Console.WriteLine("╔══════════════════════════╗");
                    Console.Write("".PadLeft(48)); Console.WriteLine("║ Inicio de sesión fallido ║");
                    Console.Write("".PadLeft(48)); Console.WriteLine("╚══════════════════════════╝");
                    Console.ReadLine();
                }
            }
        }
        private static string LeerContraseña()
        {
            string contraseña = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Si la tecla no es Enter o Retroceso, agrega el carácter a la contraseña
                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                {
                    contraseña += key.KeyChar;
                    Console.Write("*");
                }
                // Si la tecla es Retroceso, elimina el último carácter de la contraseña
                else if (key.Key == ConsoleKey.Backspace && contraseña.Length > 0)
                {
                    contraseña = contraseña.Substring(0, contraseña.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Salta a la siguiente línea después de que se presiona Enter
            return contraseña;
        }
    }
}

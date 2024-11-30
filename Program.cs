using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.extras;
using T1_Gestor_Medico_de_Referencias.T1._0._1_listasSimples._0._1._1_login;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._0_inventarioListaCircular;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._1_reporteListaCircular;
using T1_Gestor_Medico_de_Referencias.T2._1._2_pilas;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._0_colaConPrioridad._1._3._0._1_ingresoAmbulanciasColaSinPrioridad;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._0_ingresoAmbulanciasColaSinPrioridad;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_atencionAlClienteColaConPrioridad;
using T1_Gestor_Medico_de_Referencias.T3._2._1_arbol_Referencia;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace T1_Gestor_Medico_de_Referencias
{
    class Program
    {
        //Version 4- 08/07/24
        //Se agrego los reportes en pdf y el menu en form

        //metodo para enlazar la dos listas
        static void EnlazarPacienteDoctor(listaSimplePaciente ls, listaDobleTrabajadores lp, pilaPacientes pila)
        {
            Nodo_Paciente p = ls.lista;
            Nodo_Trabajadores t = lp.listaDblTrabajadores;
            while (p != null)
            {
                t = lp.listaDblTrabajadores;
                while (t != null)
                {
                    if (t.Cargo_e == "medico" && t.Asignado == false)
                    {
                        if (p.Doctor_asignado == "" && t.Genero_e == "Masculino")
                        {
                            t.Asignado = true;
                            p.Doctor_asignado = "Dr. " + t.Nombre_e;
                            pila.push("Asignación de Medico", p.Nro_dni_paciente.ToString(), "Se asignó al " + p.Doctor_asignado, "paciente");
                        }
                        else if (p.Doctor_asignado == "" && t.Genero_e == "Femenino")
                        {
                            t.Asignado = true;
                            p.Doctor_asignado = "Dra. " + t.Nombre_e;
                            pila.push("Asignación de Medico", t.Nro_dni_e.ToString(), "Se asignó a la " + p.Doctor_asignado, "paciente");
                        }
                    }

                    t = t.Sgte;
                }
                p = p.Sgte;
            }

        }
        static void EnlazarConductorAmbulancia(listaDobleTrabajadores ls, listaDobleAmbulancias lp, pilaAmbulancias pila)
        {
            Nodo_Trabajadores p = ls.listaDblTrabajadores;
            nodoAmbulancias t = lp.listaDblAmbulancias;
            while (t != null)
            {
                p = ls.listaDblTrabajadores;
                while (p != null)
                {
                    if (p.Cargo_e == "conductor" && p.Asignado == false)
                    {
                        if (t.Conductor == "")
                        {
                            p.Asignado = true;
                            t.Conductor = p.Nombre_e;
                            pila.push("Asignación de Conductor", t.Placa.ToString(), "Se asignó al " + t.Conductor, "Ambulancias");
                        }
                        
                    }

                    p = p.Sgte;
                }
                t = t.Sgte;
            }

        }
        static void EnlazarPacienteSede(listaSimplePaciente ls, listaDobleSedes lp, pilaPacientes pila)
        {
            Nodo_Paciente p = ls.lista;
            nodoSedes t = lp.listaDblSedes;
            bool encontrado=false;
            int contador=0;
            if (p != null && t != null)
            {
                lp.imprimirLD();
                Console.WriteLine("Ingrese el código de la sede en la que registrará los pacientes");
                string codigo=Console.ReadLine();
                lp.BuscarDato(codigo, ref encontrado);
                string nombre = lp.ExtraerNombre(codigo, ref encontrado);
                if (encontrado == true)
                {
                    Console.WriteLine("Ingrese la cantidad de pacientes que asignará a esta sede");
                    int cant=int.Parse(Console.ReadLine());
                    for (int i = 0; i < cant; i++)
                    {
                        while (p.Sede_asignada != "")
                        {
                            p = p.Sgte;
                        }
                        if (p.Sede_asignada=="")
                        {
                            p.Sede_asignada = nombre;
                            pila.push("Se Asignó un Paciente a una Sede", p.Nro_dni_paciente.ToString(), "Sede: " + p.Sede_asignada,"paciente");
                        }
                        p = p.Sgte;
                        contador++;
                        if (p == null)
                        {
                            Console.WriteLine("No habían suficientes pacientes para asignar. Se asignaron "+contador+" pacientes");
                            return;
                        }
                    }
                    Console.WriteLine("Se asignaron " + contador + " pacientes a " + nombre);
                }
                else
                {
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                if (p == null)
                    Console.WriteLine("Ingrese pacientes para completar este proceso");
                else if (t == null)
                    Console.WriteLine("Ingrese sedes medicas para completar este proceso");
            }
        }


        //metodo para visualizar la listas
        static void tablaEnlace(listaSimplePaciente ls)
        {
            Nodo_Paciente l = ls.lista;
            //Mostrar los datos de la lista
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                         Lista de Pacientes                                                                 ║");
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║          Nombre del Paciente         | Edad | Numero de DNI | Seguro Médico |      Malestares        |               Doctor                ║");
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            //Recorrer la lista
            while (l != null)
            {
                Console.WriteLine($"║  {l.Nombre_paciente,-35} | {l.Edad_paciente,-4} | {l.Nro_dni_paciente,-13} | {l.Seguro_med,-13} | {l.Malestares_paciente,-22} | {l.Doctor_asignado,-35} ║ ");
                l = l.Sgte;
            }
            //Pie de página 
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
        //t
        static void tablaEnlaceSede(listaSimplePaciente ls)
        {
            Nodo_Paciente l = ls.lista;
            //Mostrar los datos de la lista
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                         Lista de Pacientes                                                                 ║");
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║          Nombre del Paciente         | Edad | Numero de DNI | Seguro Médico |      Malestares        |               Sede                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            //Recorrer la lista
            while (l != null)
            {
                Console.WriteLine($"║  {l.Nombre_paciente,-35} | {l.Edad_paciente,-4} | {l.Nro_dni_paciente,-13} | {l.Seguro_med,-13} | {l.Malestares_paciente,-22} | {l.Sede_asignada,-35} ║ ");
                l = l.Sgte;
            }
            //Pie de página 
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }


        //metodo de main para inicializar
        static void Main(string[] args)
        {         
            ////    Estructuras de datos actulizadas para la T1 (3 listas)

            //llamando la clase de la lista simple1 de creador para cuentas
            listaSimpleCreador ListaSimpleCreador = new listaSimpleCreador();
            //llamando la clase de la lista simple2 de pacientes
            listaSimplePaciente ListaSimplePaciente = new listaSimplePaciente();
            //llamando la clase de la lista doble1 de trabajadores
            listaDobleTrabajadores ListaDobleTrabajadores = new listaDobleTrabajadores();

            ////    Estructuras de datos actulizadas para la T2 (4 listas [ 2dobles, 2 circulares ] - 4 pila - 1 cola sin prioridad )
            
            // Configurar el tamaño de la ventana de la consola
            int windowWidth = 127;
            int windowHeight = 40; 
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);
            //llamando la clase de la lista doble2 de sedes
            listaDobleSedes ListaDobleSedes = new listaDobleSedes();
            //llamando la clase de la lista doble3 de ambulancias 
            listaDobleAmbulancias ListaDobleAmbulancias = new listaDobleAmbulancias();
            //llamando la clase de la lista circular1 de inventario
            listaCircularInventario ListaCircularInventario = new listaCircularInventario();
           
            //llamando la clase de la pila Pacientes
            pilaPacientes PilaPacientes = new pilaPacientes();
            //llamando la clase de la pila Trabajadores
            pilaTrabajadores PilaTrabajadores = new pilaTrabajadores();
            //llamando la clase de la pila Almacen
            pilaAlmacen PilaAlmacen = new pilaAlmacen();
            //llamando la clase de la pila Sedes
            pilaSedes PilaSedes = new pilaSedes();
            //llamando la clase de la pila ambulancias
            pilaAmbulancias PilaAmbulancias = new pilaAmbulancias();
            //llamando la clase de la cola sin prioridad de almacen
            colaAlmacen ColaAlmacen = new colaAlmacen();//sinPrioridad


            ////    Estructuras de datos actulizadas para la T3 ( 1 lista circular - 1 colaPrioridad - 1 arbol referencia )
            ///
            //llamando la clase de la lista circular2 de reportes
            listaCircularReporte ListaCircularReporte = new listaCircularReporte();
            //llamando la clase de la cola con prioridad de ingreso de ambulancias
            colaIngresoDeAmbulancias ColaConPrioridad = new colaIngresoDeAmbulancias();//conPrioridad
            //llamando la clase del arbol
            arbolVacunacion ArbolVacunacion =new arbolVacunacion();


            //Datos registrados en consola 
            infoDatos.simple1(ListaSimpleCreador); infoDatos.simple2(ListaSimplePaciente); infoDatos.doble1(ListaDobleTrabajadores);

            //Login
            iniciarCuenta.IniciarSesion(ListaSimplePaciente
                                      , ListaSimpleCreador
                                      , ListaDobleTrabajadores
                                      , ListaDobleSedes
                                      , ListaDobleAmbulancias
                                      , ListaCircularInventario
                                      , ListaCircularReporte
                                      , PilaPacientes , PilaTrabajadores,PilaAlmacen,PilaSedes,PilaAmbulancias
                                      , ColaConPrioridad
                                      , ColaAlmacen
                                      , ArbolVacunacion);
        }

        //metodo de menu principal
        public static void menuPrincipal(listaSimplePaciente ListaSimplePaciente
                                       , listaSimpleCreador ListaSimpleCreador
                                       , listaDobleTrabajadores ListaDobleTrabajadores
                                       , listaDobleSedes ListaDobleSedes
                                       , listaDobleAmbulancias listaDobleAmbulancias
                                       , listaCircularInventario listaCircularInventario
                                       , listaCircularReporte listaCircularReporte
                                       , pilaPacientes PilaPacientes, pilaTrabajadores PilaTrabajadores, pilaAlmacen PilaAlmacen,pilaSedes PilaSedes,pilaAmbulancias PilaAmbulancias
                                       , colaIngresoDeAmbulancias ColaConPrioridad 
                                       , colaAlmacen ColaAlmacen
                                       , arbolVacunacion ArbolVacunacion)
        {           

            while (true)
            {
                int windowWidth = 127;
                int windowHeight = 35;
                // Ajustar el tamaño de la ventana de la consola
                Console.SetWindowSize(windowWidth, windowHeight);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(91), Console.ForegroundColor = ConsoleColor.Blue);
                Console.ResetColor();
                Console.WriteLine
(@"
     ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    ::::::::::::::: ███:: ███::: ███████:::: ██████::: ████████::: █████::: █████████::: ██████:::: ███::::::::::::::::::
   :::::::::::::::: ███:: ███:: ███:: ███:: ███:: ██::: ███: ███::: ███:::: ██ ███ ██:: ████████::: ███:::::::::::::::::::   
  ::::::::::::::::: ███:: ███:: ███:: ███:: ██::::::::: ███: ███::: ███::::::: ███::::: ███: ███::: ███::::::::::::::::::::  
  ::::::::::::::::: ███:: ███:: ███:: ███:: ██::::::::: ███: ███::: ███::::::: ███::::: ███: ███::: ███::::::::::::::::::::  
  ::::::::::::::::: █████████:: ███:: ███::: ██████:::: ██████::::: ███::::::: ███::::: ███: ███::: ███::::::::::::::::::::   
  ::::::::::::::::: ███:: ███:: ███:: ███::::::: ███::: ███:::::::: ███::::::: ███::::: ████████::: ███::::::::::::::::::::
  ::::::::::::::::: ███:: ███:: ███:: ███::::::: ███::: ███:::::::: ███::::::: ███::::: ███: ███::: ███::::::::::::::::::::
    ::::::::::::::: ███:: ███:: ███:: ███::: ███████::: ███:::::::: ███::::::: ███::::: ███: ███::: ███:: ██:::::::::::::
     :::::::::::::: ███:: ███::: ███████::::::█████:::: ████:::::: █████::::: █████:::: ██::: ██::: ████████::::::::::::
      :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝".PadLeft(91), Console.ForegroundColor = ConsoleColor.Blue);
                Console.Write("                                               ║");Console.Write("  Estructura de Datos  ",Console.ForegroundColor = ConsoleColor.White);Console.Write("║\n", Console.ForegroundColor = ConsoleColor.Blue);
                Console.WriteLine("                                               ╚═══════════════════════╝".PadLeft(69));
                Console.WriteLine("\n═══════════════════════════", Console.ForegroundColor = ConsoleColor.Blue);
                Console.Write(">|", Console.ForegroundColor = ConsoleColor.Blue); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.Blue);
                Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Blue);
                Console.WriteLine("");
                Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuAzul("1", "Registro Publico"); Console.Write("".PadRight(25)); elementosDecoracion.tabularMenuAzul("3", "Almacen"); Console.Write("".PadRight(27)); elementosDecoracion.tabularMenuAzul("5", "Cuentas\n");//cuenta 
                Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuAzul("2", "Registro Privado"); Console.Write("".PadRight(25)); elementosDecoracion.tabularMenuAzul("4", "Sedes"); Console.Write("".PadRight(29)); elementosDecoracion.tabularMenuAzul("6", "Reportes\n");//txt
                Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuAzul("7", "Registro Ambulancias");
                Console.WriteLine("");
                Console.Write("\n\n\t\t\t\t\t\t   [", Console.ForegroundColor = ConsoleColor.Blue); Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White); Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Blue);
                Console.Write("  [", Console.ForegroundColor = ConsoleColor.Blue); Console.Write("Help", Console.ForegroundColor = ConsoleColor.White); Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Blue);
                Console.ResetColor();
                elementosDecoracion.pintarGrafosAzul("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                elementosDecoracion.pintarLetrasAzul("║", " >| Selecciona un menu -> ");

                string opcion = Console.ReadLine();
                switch (opcion.ToLower())
                {
                    case "1":
                        Program.menuPublico(ListaSimplePaciente
                                            , ListaDobleTrabajadores
                                            , PilaPacientes
                                            ,ListaDobleSedes
                                            , ColaConPrioridad
                                            , listaDobleAmbulancias
                                            , ArbolVacunacion);
                        break;
                    case "2":
                        Program.menuPrivado(ListaSimplePaciente
                                            , ListaDobleTrabajadores
                                            , PilaTrabajadores);
                        break;
                    case "3":
                        Program.menuAlmacen(listaCircularInventario
                                            ,ColaAlmacen
                                            ,PilaAlmacen);
                        break;
                    case "4":
                        Program.menuSedes(ListaDobleSedes
                                         ,PilaSedes);
                        break;
                    case "5":
                        Program.menuCuentas(ListaSimpleCreador);
                        break;
                    case "6":
                        Program.menuReporte( ListaSimplePaciente
                                       ,  ListaSimpleCreador
                                       ,  ListaDobleTrabajadores
                                       ,  ListaDobleSedes
                                       ,  listaDobleAmbulancias
                                       ,  listaCircularInventario
                                       ,  listaCircularReporte
                                       ,  PilaPacientes,  PilaTrabajadores,  PilaAlmacen,  PilaSedes,  PilaAmbulancias
                                       ,  ColaConPrioridad
                                       ,  ColaAlmacen);
                        break;
                    case "7":
                        Program.menuAmbulancia(listaDobleAmbulancias
                                                ,PilaAmbulancias
                                                ,ListaDobleTrabajadores);
                        break;
                    case "help":
                        ayudaMenu.menuAyuda();
                        break;
                    case "salir":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.Write("\n [x] Error / Vuelve a ingresar un valor", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ReadLine();
                        break;
                }
            }
        }
        static nodoArbol NodoArbol;
        
        //metodo de mnu publico
        public static void menuPublico(listaSimplePaciente ListaSimplePaciente
                                     , listaDobleTrabajadores ListaDobleTrabajadores
                                     , pilaPacientes PilaMovimientos
                                     , listaDobleSedes LisDobleSedes
                                     , colaIngresoDeAmbulancias ColaConPrioridad
                                     , listaDobleAmbulancias ListaDobleAmbulancias
                                     , arbolVacunacion arbol){

            int windowWidth = 148;
            int windowHeight = 52;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);

            nodoColaPrioAmbulancias nodoCOLAprio = new nodoColaPrioAmbulancias();
            string gravedad_p;
            string nombre_p;
            int edad_p;
            int nro_dni_p;
            string seguro_med;
            string malestares;
            string genero;
            int Dni_Borrar;
            bool encontrado = false;
            nodoArbol ptr;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            elementosDecoracion.pintarGrafosAmarillo("                  ╔══════════════════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(30));
            Console.WriteLine(@"

                    ███╗░░░███╗ ███████╗ ███╗░░██╗ ██╗░░░██╗   ██████╗░ ███████╗   
                    ████╗░████║ ██╔════╝ ████╗░██║ ██║░░░██║   ██╔══██╗ ██╔════╝ 
                    ██╔████╔██║ █████╗░░ ██╔██╗██║ ██║░░░██║   ██║░░██║ █████╗░░  
                    ██║╚██╔╝██║ ██╔══╝░░ ██║╚████║ ██║░░░██║   ██║░░██║ ██╔══╝░░  
                    ██║░╚═╝░██║ ███████╗ ██║░╚███║ ╚██████╔╝   ██████╔╝ ███████╗   
                    ╚═╝░░░░░╚═╝ ╚══════╝ ╚═╝░░╚══╝ ░╚═════╝░   ╚═════╝░ ╚══════╝   

                                    ██████╗░ ██╗ ███████╗ ███╗░░██╗ ██╗░░░██╗ ███████╗ ███╗░░██╗ ██╗ ██████╗░ ░█████╗░   
                                    ██╔══██╗ ██║ ██╔════╝ ████╗░██║ ██║░░░██║ ██╔════╝ ████╗░██║ ██║ ██╔══██╗ ██╔══██╗   
                                    ██████╦╝ ██║ █████╗░░ ██╔██╗██║ ╚██╗░██╔╝ █████╗░░ ██╔██╗██║ ██║ ██║░░██║ ███████║   
                                    ██╔══██╗ ██║ ██╔══╝░░ ██║╚████║ ░╚████╔╝░ ██╔══╝░░ ██║╚████║ ██║ ██║░░██║ ██╔══██║   
                                    ██████╦╝ ██║ ███████╗ ██║░╚███║ ░░╚██╔╝░░ ███████╗ ██║░╚███║ ██║ ██████╔╝ ██║░░██║   
                                    ╚═════╝░ ╚═╝ ╚══════╝ ╚═╝░░╚══╝ ░░░╚═╝░░░ ╚══════╝ ╚═╝░░╚══╝ ╚═╝ ╚═════╝░ ╚═╝░░╚═╝
            ", Console.ForegroundColor = ConsoleColor.White);
            elementosDecoracion.pintarGrafosAmarillo("                  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════╝\n".PadLeft(30));
            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write(">|", Console.ForegroundColor = ConsoleColor.Yellow); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("═══════════════════════════\n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            ListaSimplePaciente.tamanoLista();Console.Write(" ( ");ListaSimplePaciente.conteoPacienteGrave();Console.Write(" - ");ListaSimplePaciente.conteoPacienteNoGrave();Console.Write(" )\n",Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine("\n");

            Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuRojo("1", "Ingreso de nuevo paciente\t\t\t"); elementosDecoracion.tabularMenuRojo("2", "Modificar pacientes\t\t\t\t"); elementosDecoracion.tabularMenuRojo("3", "Registro de Paciente\n\n");

            Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuRojo("4", "Asignar medico\t\t\t\t"); elementosDecoracion.tabularMenuRojo("6", "Asignar Sede \t\t\t\t"); elementosDecoracion.tabularMenuRojo("8", "Asignar Ambulancia\n");
            Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuRojo("5", "Registro medico\t\t\t\t"); elementosDecoracion.tabularMenuRojo("7", "Registro Sede\t\t\t\t"); elementosDecoracion.tabularMenuRojo("9", "Registro Ambulancia\n\n");


            Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuRojo("10", "Iniciar Campaña Vacunación (Arbol)\t"); elementosDecoracion.tabularMenuRojo("11", "Historial de Movimientos (Pila)\n");
            Console.Write("".PadLeft(10)); elementosDecoracion.tabularMenuRojo("12", "Regresar");

            Console.Write("\n\n\t\t\t\t\t\t\t [", Console.ForegroundColor = ConsoleColor.Yellow);Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write("  [", Console.ForegroundColor = ConsoleColor.Yellow);Console.Write("Help", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();

            elementosDecoracion.pintarGrafosAmarillo("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            elementosDecoracion.pintarLetrasAmarillo("║", " >| Selecciona una opcion -> ");
            Console.ResetColor();

            string opc3 = Console.ReadLine(); int pos = 0;
            switch (opc3.ToLower())
            {
                case "1"://Agregar
                    int opc;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                        elementosDecoracion.tabularMenuRojo("1", "Agregar por posicion\n");
                        elementosDecoracion.tabularMenuRojo("2", "Agregar al inicio\n");
                        elementosDecoracion.tabularMenuRojo("3", "Agregar al final\n");
                        elementosDecoracion.tabularMenuRojo("4", "Generar Pacientes de manera Aleatoria.\n");
                        elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                        Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                        Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc));
                    switch (opc)
                    {
                        case 1:
                            ListaSimplePaciente.imprimir();
                            do {
                                Console.Write("* Elija la posición en la que desea Agregar su nuevo paciente >| ");
                                if (!int.TryParse(Console.ReadLine(), out pos) || pos <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while(pos<=0);
                            ListaSimplePaciente.BuscarDato(pos, ref encontrado);if (pos == 1) encontrado = true;
                            if (encontrado == true)
                            {
                                Console.Write("* Coloque (si - no) si es de gravedad >| ");
                                gravedad_p = Console.ReadLine();
                                Console.Write("* Ingrese el nombre del paciente >| ");
                                nombre_p = Console.ReadLine();
                                //Validar edad paciente
                                do
                                {
                                    Console.Write("* Ingrese la edad >| ");
                                    if (!int.TryParse(Console.ReadLine(), out edad_p) || edad_p <= 0)
                                    {
                                        // Mostrar un mensaje de error si la entrada no es un número entero válido
                                        Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                    }
                                } while (edad_p <= 0);
                                //Validar numero DNI
                                do
                                {
                                    Console.Write("- Ingrese el número de DNI >| ");
                                    if (!int.TryParse(Console.ReadLine(), out nro_dni_p) || nro_dni_p <= 0)
                                    {
                                        // Mostrar un mensaje de error si la entrada no es un número entero válido
                                        Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                    }
                                } while (nro_dni_p <= 0);
                                Console.Write("* Ingrese el nombre del seguro médico >| ");
                                seguro_med = Console.ReadLine();
                                Console.Write("* Indique los malestares presentes >| ");
                                malestares = Console.ReadLine();
                                Console.Write("Ingrese el género del paciente : (Masculino/Femenino) >| ");
                                genero = Console.ReadLine();
                                ListaSimplePaciente.AgregarPosicion(gravedad_p, nombre_p, edad_p, nro_dni_p, seguro_med, malestares, genero, pos);
                                PilaMovimientos.push("Agregar Paciente", nro_dni_p.ToString(), "Se agregó por posicion", "paciente");
                            }

                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias,arbol);
                            return;
                        case 2:
                            Console.Write("* Coloque (si - no) si es de gravedad >| ");
                            gravedad_p = Console.ReadLine();

                            Console.Write("- Ingrese el nombre del paciente >| ");
                            nombre_p = Console.ReadLine();

                            //Validar edad paciente
                            do
                            {
                                Console.Write("* Ingrese la edad >| ");
                                if (!int.TryParse(Console.ReadLine(), out edad_p)|| edad_p <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (edad_p <= 0);
                            //Validar numero DNI
                            do
                            {
                                Console.Write("- Ingrese el número de DNI >| ");
                                if (!int.TryParse(Console.ReadLine(), out nro_dni_p) || nro_dni_p <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (nro_dni_p <= 0);
                            Console.Write("- Ingrese el nombre del seguro médico >| ");
                            seguro_med = Console.ReadLine();
                            Console.Write("- Indique los malestares presentes >| ");
                            malestares = Console.ReadLine();
                            Console.Write("- Ingrese el género del paciente: (Masculino/Femenino) >| ");
                            genero = Console.ReadLine();
                            ListaSimplePaciente.Registrar_Paciente(gravedad_p,nombre_p,
                                edad_p,
                                nro_dni_p,
                                seguro_med,
                                malestares,
                                genero);
                            Console.WriteLine("\nPaciente almacenado correctamnete", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            PilaMovimientos.push("Agregar Paciente", nro_dni_p.ToString(), "Se agregó al inicio", "paciente");
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos,LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                        case 3:
                            Console.Write("* Coloque (si - no) si es de gravedad >| ");
                            gravedad_p = Console.ReadLine();

                            Console.Write("- Ingrese el nombre del paciente >| ");
                            nombre_p = Console.ReadLine();

                            //Validar edad paciente
                            do
                            {
                                Console.Write("* Ingrese la edad >| ");
                                if (!int.TryParse(Console.ReadLine(), out edad_p) || edad_p <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (edad_p <= 0);
                            //Validar numero DNI
                            do
                            {
                                Console.Write("- Ingrese el número de DNI >| ");
                                if (!int.TryParse(Console.ReadLine(), out nro_dni_p) || nro_dni_p <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (nro_dni_p <= 0);
                            Console.Write("- Ingrese el nombre del seguro médico >| ");
                            seguro_med = Console.ReadLine();
                            Console.Write("- Indique los malestares presentes >| ");
                            malestares = Console.ReadLine();
                            Console.Write("- Ingrese el género del paciente: (Masculino/Femenino) >| ");
                            genero = Console.ReadLine();
                            ListaSimplePaciente.RegistrarFinal(gravedad_p,nombre_p,
                                edad_p,
                                nro_dni_p,
                                seguro_med,
                                malestares,
                                genero);
                            Console.WriteLine("\nPaciente almacenado correctamnete", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            PilaMovimientos.push("Agregar Paciente", nro_dni_p.ToString(), "Se agregó al final", "paciente");
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                        case 4://generar aleatoriamente
                            int numeroPacientes;
                            do
                            {
                                Console.Write("Ingrese el número de pacientes que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroPacientes) || numeroPacientes <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroPacientes <= 0);
                            string nombre;
                            int edad;
                            int dni;
                            string gravedad;
                            string seguro;
                            string malestares1;
                            string genero1;
                            Random random = new Random();
                            for (int i = 0; i < numeroPacientes; i++)
                            {
                                if (random.Next(1, 3) == 1) //Generar nombre de hombre
                                {
                                    nombre = ListaSimplePaciente.GenerarNombreHombre();
                                    genero1 = "Masculino";
                                }
                                else //Generar nombre de mujer
                                {
                                    nombre = ListaSimplePaciente.GenerarNombreMujer();
                                    genero1 = "Femenino";
                                }
                                edad = ListaSimplePaciente.GenerarEdad();
                                dni = ListaSimplePaciente.GenerarDNI();
                                seguro = ListaSimplePaciente.GenerarSeguro();
                                gravedad = ListaSimplePaciente.GenerarGravedad();
                                malestares1 = ListaSimplePaciente.GenerarMalestar();
                                ListaSimplePaciente.RegistrarFinal(gravedad,nombre, edad, dni, seguro, malestares1, genero1);
                                PilaMovimientos.push("Agregar Paciente", dni.ToString(), "Se agregó al Final", "paciente");
                            }
                            Console.WriteLine("Se agrego correctamente los " +numeroPacientes+ " pacientes al listado del sistema", Console.ForegroundColor = ConsoleColor.Green);
                            Console.WriteLine("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            
                            return;
                    }
                    break;
                case "2":  //Opciones de modificacion de datos
                    int opc4;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Editar por DNI\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar por DNI\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    }while(!int.TryParse(Console.ReadLine(),out opc4));
                    switch (opc4)
                    {
                        case 1:
                            bool agregarM = false;
                            string cambio1 = "";
                            string DatoPila = "";
                            ListaSimplePaciente.Modificar(ref agregarM, ref cambio1, ref DatoPila);
                            if (agregarM == true)
                            {
                                PilaMovimientos.push("Modificar", DatoPila, cambio1, "paciente");
                            }
                            return;

                        case 2:
                            ListaSimplePaciente.imprimir();
                            do 
                            {
                                Console.WriteLine("Coloca el Número de DNI del Paciente a eliminar >| ");
                            }while(!int.TryParse(Console.ReadLine(), out Dni_Borrar));
                            string dniMovimiento = "";
                            bool encontro = false;
                            ListaSimplePaciente.eliminaElementoValor(Dni_Borrar, ref dniMovimiento, ref encontro);
                            if (encontro == true)
                            {
                                PilaMovimientos.push("Eliminar Paciente", dniMovimiento, "Se eliminó este Paciente", "paciente");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                        case 0:
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "3"://Mostrar tabla
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Tabla General\n");
                    elementosDecoracion.tabularMenuRojo("2", "Tabla de SIS\n");
                    elementosDecoracion.tabularMenuRojo("3", "Tabla de EsSalud\n");
                    elementosDecoracion.tabularMenuRojo("4", "Tabla de Privado\n");
                    elementosDecoracion.tabularMenuRojo("5", "Tabla de pacientes GRAVES\n");
                    elementosDecoracion.tabularMenuRojo("6", "Tabla de pacientes NO GRAVES\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    string opc2 = Console.ReadLine();
                    switch (opc2.ToLower())
                    {
                        case "1":
                            ListaSimplePaciente.imprimir();
                            Console.Write("Actualmente hay "); ListaSimplePaciente.tamanoLista();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            break;
                        case "2":
                            ListaSimplePaciente.imprimirSIS();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            break;
                        case "3":
                            ListaSimplePaciente.imprimirEsSalud();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            break;
                        case "4":
                            ListaSimplePaciente.imprimirPrivado();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            break;
                        case "5":
                            ListaSimplePaciente.imprimirGrave();
                            Console.Write("Actualmente hay "); ListaSimplePaciente.conteoPacienteGrave();
                            Console.WriteLine ("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            break;
                        case "6":
                            ListaSimplePaciente.imprimirNoGrave();
                            Console.Write("Actualmente hay "); ListaSimplePaciente.conteoPacienteNoGrave();
                            Console.WriteLine("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            break;
                        case "0":
                            return;
                    }
                    break;
                case "4":
                    //Asignar paciente a medicos
                    Program.EnlazarPacienteDoctor(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                    Console.Write("Se asigno respectivamente los pacientes :) \n", Console.ForegroundColor = ConsoleColor.Green);
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "5"://mostrar tabla de asignacion de pacientes
                    Console.Clear();
                    Program.tablaEnlace(ListaSimplePaciente);
                    Console.Write("Actualmente hay "); ListaSimplePaciente.tamanoLista();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "6":
                    //Traslado de pacientes a otro centro medico
                    Program.EnlazarPacienteSede(ListaSimplePaciente,LisDobleSedes,PilaMovimientos);
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "7":
                    //Registro de traslado
                    Console.Clear();
                    Program.tablaEnlaceSede(ListaSimplePaciente);
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "8":
                    //Asignar ambulancia
                    ColaConPrioridad.colaConPrio = null;
                    EncolarPacientesGraves(ListaSimplePaciente,ListaDobleAmbulancias, ColaConPrioridad);
                    Console.ReadKey();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "9":
                    //registro
                    ColaConPrioridad.MostrarColaConPrio();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "10":
                    //arbol
                    int opc_4;
                    do
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                            elementosDecoracion.tabularMenuRojo("1", "Generar Arbol para Vacunacion (Generación según edad. Raiz: 65)\n");
                            elementosDecoracion.tabularMenuRojo("2", "Podar Arbol\n");
                            elementosDecoracion.tabularMenuRojo("3", "Talar Arbol\n");
                            elementosDecoracion.tabularMenuRojo("4", "Buscar en Arbol (según edad)\n");
                            elementosDecoracion.tabularMenuRojo("5", "Eliminar un Nodo (según edad)\n");
                            elementosDecoracion.tabularMenuRojo("6", "Mostrar Arbol\n");
                            elementosDecoracion.tabularMenuRojo("7", "Mostrar Arbol inOrder\n");
                            elementosDecoracion.tabularMenuRojo("8", "Mostrar Arbol PreOrder\n");
                            elementosDecoracion.tabularMenuRojo("9", "Mostrar Arbol PostOrder\n");
                            
                            elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                            Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                        } while (!int.TryParse(Console.ReadLine(), out opc_4));
                        switch (opc_4)
                        {
                            case 1:
                                arbol.insertarNodoArbolInicio(ref NodoArbol, 65, " Raiz ");
                                arbol.TalarArbol(NodoArbol);
                                arbol.insertarPacienteArbol(ref NodoArbol, ListaSimplePaciente);
                                Console.WriteLine("Arbol Generado", Console.ForegroundColor = ConsoleColor.Red);
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                PilaMovimientos.push("Generar Arbol", "", "Se genero un Arbol para Campaña de Vacunación", "paciente");
                                Console.ReadKey();
                                break;
                            case 2:
                                if (NodoArbol != null)
                                {
                                    Console.WriteLine("Podar el Arbol\n");
                                    Console.Write("Edad a partir de la que se borrarán los nodos que le siguen : ");
                                    int dato = int.Parse(Console.ReadLine());
                                    //Si el dato buscado es igual al dato del nodo del arbol a podar
                                    if (dato == NodoArbol.edad)
                                    {
                                        //Podar el arbol desde la raiz
                                        arbol.podarArbol(ref NodoArbol); Console.WriteLine("Arbol Podado\n");
                                        PilaMovimientos.push("Podar Arbol", "", "Se podó el Arbol de Campaña de Vacunación", "paciente");
                                    }
                                    else
                                    {
                                        //Buscar el nodo del arbol a podar
                                        //mediante busqueda Recursiva
                                        ptr = arbol.EncontrarNodoRecursivo(NodoArbol, dato);
                                        if (ptr == null)
                                            Console.WriteLine("Elemento no existe en el arbol\n");
                                        else
                                        {
                                            //Podar el arbol desde el nodo encontrado
                                            arbol.podarArbol(ref ptr);
                                            Console.WriteLine("Arbol Podado\n");
                                            PilaMovimientos.push("Podar Arbol", "", "Se podó el Arbol de Campaña de Vacunación", "paciente");
                                        }
                                    }
                                }
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                
                                Console.ReadKey();
                                break;
                            case 3:
                                arbol.TalarArbol(NodoArbol);
                                NodoArbol = null;
                                Console.WriteLine("Arbol Talado", Console.ForegroundColor = ConsoleColor.Red);
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                PilaMovimientos.push("Talar Arbol", "", "Se taló el Arbol de Campaña de Vacunación", "paciente");
                                Console.ReadKey();
                                break;
                            case 4:
                                int Code;
                                bool Encontrado = false;
                                //do while para evitar exception
                                do
                                {
                                    Console.WriteLine("Ingrese la edad que desea buscar en el arbol: ");
                                    if (!int.TryParse(Console.ReadLine(), out Code) || Code <= 0)
                                    {
                                        //Mostrar un mensaje de error si la entrada no es un número entero válido
                                        Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                    }
                                } while (Code <= 0);
                                arbol.BusquedaPorCodigo(NodoArbol, Code, ref Encontrado);
                                if (!Encontrado)
                                {
                                    Console.WriteLine("El paciente no fue encontrado en el registro");
                                }
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ReadKey();
                                break;
                            case 5:
                                int Code1;
                                bool Encontrado1 = false;
                                //do while para evitar exception
                                do
                                {
                                    Console.WriteLine("Ingrese la edad del Paciente a eliminar del arbol: ");
                                    if (!int.TryParse(Console.ReadLine(), out Code1) || Code1 <= 0)
                                    {
                                        //Mostrar un mensaje de error si la entrada no es un número entero válido
                                        Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                    }
                                } while (Code1 <= 0);
                                arbol.BusquedaPorCodigo(NodoArbol, Code1, ref Encontrado1);
                                //Si no fue encontrado muestra mensaje
                                if (!Encontrado1)
                                {
                                    Console.WriteLine("El Paciente no fue encontrado en el registro");
                                }
                                else
                                {
                                    arbol.eliminaNodoABB(ref NodoArbol, Code1);
                                    Console.WriteLine("El nodo del arbol fue eliminado");
                                }
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ReadKey();
                                break;
                            case 6:
                                Console.WriteLine("Raiz: 65 años", Console.ForegroundColor = ConsoleColor.Red);
                                arbol.verArbol(NodoArbol, 0);
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ReadKey();
                                break;
                            case 7:
                                Console.WriteLine("Raiz: 65 años", Console.ForegroundColor = ConsoleColor.Red);
                                arbol.inOrder(NodoArbol);
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ReadKey();
                                break;
                            case 8:
                                Console.WriteLine("Raiz: 65 años", Console.ForegroundColor = ConsoleColor.Red);
                                arbol.preOrden(NodoArbol);
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ReadKey();
                                break;
                            case 9:
                                Console.WriteLine("Raiz: 65 años", Console.ForegroundColor = ConsoleColor.Red);
                                arbol.postOrden(NodoArbol);
                                Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                                Console.ReadKey();
                                break;
                            
                        }
                    } while (opc_4 != 0);
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "11":
                    int opc__1;
                    do { 
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.White;
                    //Historial almacena los movimientos
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Mostrar Historial de Movimientos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Último Movimiento\n");
                    elementosDecoracion.tabularMenuRojo("3", "Eliminar todo el Historial\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc__1));
                    switch (opc__1)
                    {
                        case 1:
                            PilaMovimientos.muestraPila("paciente");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                        case 2:
                            PilaMovimientos.pop();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                        case 3:
                            PilaMovimientos.EliminarPorTipo("paciente");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                            return;
                    }
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPublico(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos, LisDobleSedes, ColaConPrioridad, ListaDobleAmbulancias, arbol);
                    break;
                case "12":
                    return;
                case "help"://HELP
                    ayudaMenu.menuAyuda();
                    break;
                case "salir"://SALIR
                    Environment.Exit(0);
                    break;
            }
        }   
        
        //metodo de menu privado
        public static void menuPrivado(listaSimplePaciente ListaSimplePaciente
                                    , listaDobleTrabajadores ListaDobleTrabajadores
                                    , pilaTrabajadores PilaMovimientos){

            int windowWidth = 147;
            int windowHeight = 50;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);

            string nombre_e;
            int edad_e;
            int nro_dni_e;
            string genero_e;
            string cargo_e;
            int Dni_Borrar;
            bool asignado = false;
            bool encontrado = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            elementosDecoracion.pintarGrafosCyan("      ╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(30));
            Console.WriteLine(@"


         ███╗░░░███╗ ███████╗ ███╗░░██╗ ██╗░░░██╗   ██████╗░ ███████╗   
         ████╗░████║ ██╔════╝ ████╗░██║ ██║░░░██║   ██╔══██╗ ██╔════╝ 
         ██╔██S█╔██║ █████╗░░ ██╔██╗██║ ██║░░░██║   ██║░░██║ █████╗░░  
         ██║╚██╔╝██║ ██╔══╝░░ ██║╚████║ ██║░░░██║   ██║░░██║ ██╔══╝░░  
         ██║░╚═╝░██║ ███████╗ ██║░╚███║ ╚██████╔╝   ██████╔╝ ███████╗   
         ╚═╝░░░░░╚═╝ ╚══════╝ ╚═╝░░╚══╝ ░╚═════╝░   ╚═════╝░ ╚══════╝   

                          ░█████╗░ ██████╗░ ███╗░░░███╗ ██╗ ███╗░░██╗ ██╗ ░██████╗ ████████╗ ██████╗░ ░█████╗░ ██████╗░ ░█████╗░ ██████╗░
                          ██╔══██╗ ██╔══██╗ ████╗░████║ ██║ ████╗░██║ ██║ ██╔════╝ ╚══██╔══╝ ██╔══██╗ ██╔══██╗ ██╔══██╗ ██╔══██╗ ██╔══██╗
                          ███████║ ██║░░██║ ██╔████╔██║ ██║ ██╔██╗██║ ██║ ╚█████╗░ ░░░██║░░░ ██████╔╝ ███████║ ██║░░██║ ██║░░██║ ██████╔╝
                          ██╔══██║ ██║░░██║ ██║╚██╔╝██║ ██║ ██║╚████║ ██║ ░╚═══██╗ ░░░██║░░░ ██╔══██╗ ██╔══██║ ██║░░██║ ██║░░██║ ██╔══██╗
                          ██║░░██║ ██████╔╝ ██║░╚═╝░██║ ██║ ██║░╚███║ ██║ ██████╔╝ ░░░██║░░░ ██║░░██║ ██║░░██║ ██████╔╝ ╚█████╔╝ ██║░░██║
                          ╚═╝░░╚═╝ ╚═════╝░ ╚═╝░░░░░╚═╝ ╚═╝ ╚═╝░░╚══╝ ╚═╝ ╚═════╝░ ░░░╚═╝░░░ ╚═╝░░╚═╝ ╚═╝░░╚═╝ ╚═════╝░ ░╚════╝░ ╚═╝░░╚═╝
            ", Console.ForegroundColor = ConsoleColor.White);
            elementosDecoracion.pintarGrafosCyan("       ╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n".PadLeft(30));
            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.DarkCyan);
            Console.Write(">|", Console.ForegroundColor = ConsoleColor.DarkCyan); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.DarkCyan);
            Console.WriteLine("═══════════════════════════\n", Console.ForegroundColor = ConsoleColor.DarkCyan);
            Console.ResetColor();
            ListaDobleTrabajadores.conteoTrabajador();Console.Write("( ");ListaDobleTrabajadores.conteoSupervisor(); Console.Write(" - "); ListaDobleTrabajadores.conteoMedicos(); Console.Write(" - "); ListaDobleTrabajadores.conteoConductor(); Console.Write(" - "); ListaDobleTrabajadores.conteoLimpieza(); Console.Write(" )\n");
            Console.WriteLine("");
            Console.Write("\t\t  "); elementosDecoracion.tabularMenuVerde("1", "Ingreso de nuevo Trabajador\t\t"); elementosDecoracion.tabularMenuVerde("4", "Traslado de Trabajador\t\t\n");
            Console.Write("\t\t  "); elementosDecoracion.tabularMenuVerde("2", "Ajustes Trabajador\t\t\t"); elementosDecoracion.tabularMenuVerde("5", "Historial de movimientos (Pila)\t\t\n");
            Console.Write("\t\t  "); elementosDecoracion.tabularMenuVerde("3", "Registro de Trabajador\t\t\t"); elementosDecoracion.tabularMenuVerde("6", "Regresar \t\t\n");
            Console.Write("\n\n\t\t\t\t\t[", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White);
            Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Yellow);

            Console.Write("  [", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write("Help", Console.ForegroundColor = ConsoleColor.White);
            Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Yellow);

            elementosDecoracion.pintarGrafosCyan("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            elementosDecoracion.pintarLetrasCyan("║", " >| Selecciona una opcion -> ");
            Console.ResetColor();
            string opc1 = Console.ReadLine(); int pos = 0;
            switch (opc1.ToLower())
            {
                case "1"://Agregar
                    int opc2;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("═════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                        elementosDecoracion.tabularMenuRojo("1", "Agregar Trabajador según Posición(DNI)\n");
                        elementosDecoracion.tabularMenuRojo("2", "Agregar al inicio\n");
                        elementosDecoracion.tabularMenuRojo("3", "Agregar al final\n");
                        elementosDecoracion.tabularMenuRojo("4", "Generar Trabajadores de manera aleatoria\n");
                        elementosDecoracion.tabularMenuRojo("5", "Generar aleatoriamente (MEDICOS)\n");
                        elementosDecoracion.tabularMenuRojo("6", "Generar aleatoriamente (CONDUCTORES)\n");
                        elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                        Console.WriteLine("══════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                        Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc2));
                    switch (opc2)
                    {
                        case 1:
                            ListaDobleTrabajadores.imprimirLD();
                            do
                            {
                                Console.WriteLine("* Ingrese el DNI del Trabajador a partir del que se agregará datos antes");
                            } while (!int.TryParse(Console.ReadLine(), out pos));
                            ListaDobleTrabajadores.BuscarDato(pos, ref encontrado);
                            if (encontrado == true)
                            {
                                Console.WriteLine("* Ingrese el nombre del trabajador : ");
                                nombre_e = Console.ReadLine();
                                //Validar edad paciente
                                do
                                {
                                    Console.WriteLine("* Ingrese la edad : ");
                                    if (!int.TryParse(Console.ReadLine(), out edad_e))
                                    {
                                        Console.WriteLine("Registre información válida del trabajador.");
                                        Console.ReadKey();
                                    }
                                    else if (edad_e < 25 || edad_e > 70)
                                    {
                                        // Mostrar un mensaje de error si el número está fuera del rango
                                        Console.WriteLine("Error: ¡El número debe estar entre 25 y 65!");
                                    }
                                }
                                while (edad_e < 25 || edad_e > 70);

                                //Validar numero DNI
                                do
                                {
                                    Console.WriteLine("* Ingrese el número de DNI : ");
                                    if (!int.TryParse(Console.ReadLine(), out nro_dni_e))
                                    {
                                        Console.WriteLine("Registre información válida del paciente.");
                                        Console.ReadKey();
                                    }
                                } while (nro_dni_e <= 0);

                                Console.WriteLine("* Ingrese el género");
                                genero_e = Console.ReadLine();
                                Console.WriteLine("* Indique el cargo al que pertenece : ");
                                cargo_e = Console.ReadLine();
                                ListaDobleTrabajadores.insertarNodoAntesOtro(nombre_e, edad_e, nro_dni_e, genero_e, cargo_e, asignado, pos);
                                PilaMovimientos.push("Agregar Trabajador", nro_dni_e.ToString(), "Se agrego el trabajador a partir del Dni de otro trabajador", "trabajador");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 2:
                            Console.Write("- Ingrese el nombre del trabajador >| ");
                            nombre_e = Console.ReadLine();

                            //Validar edad paciente
                            do
                            {
                                Console.WriteLine("* Ingrese la edad : ");
                                if (!int.TryParse(Console.ReadLine(), out edad_e))
                                {
                                    Console.WriteLine("Registre información válida del trabajador.");
                                    Console.ReadKey();
                                }
                                else if (edad_e < 25 || edad_e > 70)
                                {
                                    // Mostrar un mensaje de error si el número está fuera del rango
                                    Console.WriteLine("Error: ¡El número debe estar entre 25 y 70!");
                                }
                            }
                            while (edad_e < 25 || edad_e > 70);

                            //Validar numero DNI
                            do
                            {
                                Console.WriteLine("* Ingrese el número de DNI : ");
                                if (!int.TryParse(Console.ReadLine(), out nro_dni_e))
                                {
                                    Console.WriteLine("Registre información válida del paciente.");
                                    Console.ReadKey();
                                }
                            } while (nro_dni_e <= 0);
                            Console.Write("- Ingrese el género >| ");
                            genero_e = Console.ReadLine();
                            Console.Write("- Indique el cargo al que pertenece >| ");
                            cargo_e = Console.ReadLine();
                            ListaDobleTrabajadores.insertaAlInicioLD(nombre_e, edad_e, nro_dni_e, genero_e, cargo_e, asignado);
                            PilaMovimientos.push("Agregar Trabajador", nro_dni_e.ToString(), "Se agrego al inicio", "trabajador");
                            Console.WriteLine("\nTrabajador almacenado correctamnete", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 3:
                            Console.Write("- Ingrese el nombre del trabajador >| ");
                            nombre_e = Console.ReadLine();

                            //Validar edad paciente
                            do
                            {
                                Console.WriteLine("* Ingrese la edad : ");
                                if (!int.TryParse(Console.ReadLine(), out edad_e))
                                {
                                    Console.WriteLine("Registre información válida del trabajador.");
                                    Console.ReadKey();
                                }
                                else if (edad_e < 25 || edad_e > 70)
                                {
                                    // Mostrar un mensaje de error si el número está fuera del rango
                                    Console.WriteLine("Error: ¡El número debe estar entre 25 y 70!");
                                }
                            }
                            while (edad_e < 25 || edad_e > 70);

                            //Validar numero DNI
                            do
                            {
                                Console.WriteLine("* Ingrese el número de DNI : ");
                                if (!int.TryParse(Console.ReadLine(), out nro_dni_e))
                                {
                                    Console.WriteLine("Registre información válida del paciente.");
                                    Console.ReadKey();
                                }
                            } while (nro_dni_e <= 0);

                            Console.Write("- Ingrese el género >| ");
                            genero_e = Console.ReadLine();
                            Console.Write("- Indique el cargo al que pertenece >| ");
                            cargo_e = Console.ReadLine();
                            ListaDobleTrabajadores.insertaAlFinalLD(nombre_e, edad_e, nro_dni_e, genero_e, cargo_e, asignado);
                            PilaMovimientos.push("Agregar Trabajador", nro_dni_e.ToString(), "Se agrego al final", "trabajador");
                            Console.WriteLine("\nTrabajador almacenado correctamente", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 4:
                            Console.Clear();
                            int numeroTrabajadores;
                            do
                            {
                                Console.Write("Ingrese el número de trabajadores que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroTrabajadores) || numeroTrabajadores <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroTrabajadores <= 0);
                            string nombre;
                            int edad;
                            int dni;
                            string genero;
                            string cargo;
                            bool asignado1 = false;
                            Random random = new Random();
                            for (int i = 0; i < numeroTrabajadores; i++)
                            {
                                if (random.Next(1, 3) == 1) //Generar nombre de hombre
                                {
                                    nombre = ListaDobleTrabajadores.GenerarNombreHombre();
                                    genero = "Masculino";
                                }
                                else //Generar nombre de mujer
                                {
                                    nombre = ListaDobleTrabajadores.GenerarNombreMujer();
                                    genero = "Femenino";
                                }
                                edad = ListaDobleTrabajadores.GenerarEdad();
                                dni = ListaDobleTrabajadores.GenerarDNI();
                                cargo = ListaDobleTrabajadores.GenerarCargo();
                                ListaDobleTrabajadores.insertaAlInicioLD(nombre, edad, dni, genero, cargo, asignado1);
                                PilaMovimientos.push("Agregar Trabajador", dni.ToString(), "Se agregó al inicio", "trabajador");
                            }
                            Console.Write(numeroTrabajadores + " generados correctamente\n", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >| ", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 5:
                            Console.Clear();
                            int numeroMedicos;
                            do
                            {
                                Console.Write("Ingrese el número de médicos que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroMedicos) || numeroMedicos <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroMedicos <= 0);
                            string nombreMedico;
                            int edadMedico;
                            int dniMedico;
                            string generoMedico;
                            string cargoMedico;
                            bool asignado1Medico = false;
                            Random random1 = new Random();

                            for (int i = 0; i < numeroMedicos; i++)
                            {
                                if (random1.Next(1, 3) == 1)
                                {
                                    nombreMedico = ListaDobleTrabajadores.GenerarNombreHombre();
                                    generoMedico = "Masculino";
                                }
                                else
                                {
                                    nombreMedico = ListaDobleTrabajadores.GenerarNombreMujer();
                                    generoMedico = "Femenino";
                                }
                                edadMedico = ListaDobleTrabajadores.GenerarEdad();
                                dniMedico = ListaDobleTrabajadores.GenerarDNI();
                                cargoMedico = "medico";
                                ListaDobleTrabajadores.insertaAlInicioLD(nombreMedico, edadMedico, dniMedico, generoMedico, cargoMedico, asignado1Medico);
                                PilaMovimientos.push("Agregar Medico", dniMedico.ToString(), "Se agregó al inicio", "trabajador");
                            }
                            Console.Write("Hay ", Console.ForegroundColor = ConsoleColor.DarkGreen);
                            ListaDobleTrabajadores.conteoTrabajador();
                            Console.Write(" en el sistema en total con ( ", Console.ForegroundColor = ConsoleColor.DarkGreen); ListaDobleTrabajadores.conteoMedicos(); Console.Write(" )\n", Console.ForegroundColor = ConsoleColor.DarkGreen);

                            Console.WriteLine("Se genero " + numeroMedicos + " medicos correctamente\n", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 6:
                            Console.Clear();
                            int numeroConductor;
                            do
                            {
                                Console.Write("Ingrese el número de conductores que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroConductor) || numeroConductor <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroConductor <= 0);
                            string nombreConductor;
                            int edadConductor;
                            int dniConductor;
                            string generoConductor;
                            string cargoConductor;
                            bool asignado1Conductor = false;
                            Random random2 = new Random();

                            for (int i = 0; i < numeroConductor; i++)
                            {
                                if (random2.Next(1, 3) == 1)
                                {
                                    nombreConductor = ListaDobleTrabajadores.GenerarNombreHombre();
                                    generoConductor = "Masculino";
                                }
                                else
                                {
                                    nombreConductor = ListaDobleTrabajadores.GenerarNombreMujer();
                                    generoConductor = "Femenino";
                                }
                                edadConductor = ListaDobleTrabajadores.GenerarEdad();
                                dniConductor = ListaDobleTrabajadores.GenerarDNI();
                                cargoConductor = "conductor";
                                ListaDobleTrabajadores.insertaAlInicioLD(nombreConductor, edadConductor, dniConductor, generoConductor, cargoConductor, asignado1Conductor);
                                PilaMovimientos.push("Agregar Conductor", dniConductor.ToString(), "Se agregó al inicio", "trabajador");
                            }
                            ListaDobleTrabajadores.conteoTrabajador();
                            Console.Write(" conductores generados correctamente\n", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 0:
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                    break;//metodo para agregar trabajadores
                case "2":
                    int opc_1;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Modificar Datos de Trabajador\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Trabajador según DNI\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc_1)) ;
                    switch (opc_1)
                    {

                        case 1:
                            bool agregarM = false;
                            string cambio1 = "";
                            string DatoPila = "";
                            ListaDobleTrabajadores.Modificar(ref agregarM, ref cambio1, ref DatoPila);
                            if (agregarM == true)
                            {
                                PilaMovimientos.push("Modificar", DatoPila, cambio1, "trabajador");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 2:
                            do { 
                            Console.Clear();
                            ListaDobleTrabajadores.imprimirLD();
                            Console.WriteLine("Ingrese el número de DNI del Trabajador a eliminar de la lista");
                            } while (!int.TryParse(Console.ReadLine(), out Dni_Borrar)) ;
                            bool encontro = false;
                            string dni1 = "";
                            ListaDobleTrabajadores.eliminarNodoLD(Dni_Borrar, ListaSimplePaciente, ref encontro, ref dni1);
                            if (encontro == true)
                            {
                                PilaMovimientos.push("Eliminar Trabajador", dni1, "Se eliminó este trabajador", "trabajador");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 0:
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                    break;//metodos para editar cosas de los trabajadores
                case "3":
                    int opc_2;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Tabla General\n");
                    elementosDecoracion.tabularMenuRojo("2", "Tabla de Medicos\n");
                    elementosDecoracion.tabularMenuRojo("3", "Tabla de Administradores\n");
                    elementosDecoracion.tabularMenuRojo("4", "Tabla de personal de Limpieza\n");
                    elementosDecoracion.tabularMenuRojo("5", "Tabla de personal de Conductores\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc_2)) ;
                    switch (opc_2)
                    {
                        case 1:
                            Console.Clear();
                            ListaDobleTrabajadores.imprimirLD();
                            Console.Write("Actualmente hay "); ListaDobleTrabajadores.conteoTrabajador();
                            Console.Write("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            break;
                        case 2:
                            Console.Clear();
                            ListaDobleTrabajadores.imprimirMedicos();
                            Console.Write("Actualmente hay "); ListaDobleTrabajadores.conteoMedicos();
                            Console.Write("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            break;
                        case 3:
                            Console.Clear();
                            ListaDobleTrabajadores.imprimirAdmin();
                            Console.Write("Actualmente hay "); ListaDobleTrabajadores.conteoSupervisor();
                            Console.Write("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            break;
                        case 4:
                            Console.Clear();
                            ListaDobleTrabajadores.imprimirLimpieza();
                            Console.Write("Actualmente hay "); ListaDobleTrabajadores.conteoLimpieza();
                            Console.Write("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            break;
                        case 5:
                            Console.Clear();
                            ListaDobleTrabajadores.imprimirConductor();
                            Console.Write("Actualmente hay "); ListaDobleTrabajadores.conteoConductor();
                            Console.Write("\nPresione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            break;
                        case 0:
                            return;
                    }
                    break;//metodos para llamar listas de trabajadores
                case "4"://TRASLADO DE TRBAJADOR A OTRA SEDE
                    //TRASLADO DE TRBAJADOR A OTRA SEDE

                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                    break;//TRASLADO DE TRBAJADOR A OTRA SEDE
                case "5":
                    int opc__1;
                    do { 
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.White;
                    //Historial almacena los movimientos
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Mostrar Historial de Movimientos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Último Movimiento\n");
                    elementosDecoracion.tabularMenuRojo("3", "Eliminar todo el Historial\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc__1)) ;
                    switch (opc__1)
                    {
                        case 1:
                            PilaMovimientos.muestraPila("trabajador");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 2:
                            PilaMovimientos.pop();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                        case 3:
                            PilaMovimientos.EliminarPorTipo("trabajador");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                            return;
                    }
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuPrivado(ListaSimplePaciente, ListaDobleTrabajadores, PilaMovimientos);
                    break;
                case "6":
                    return;//regresar
                case "help"://HELP
                    ayudaMenu.menuAyuda();
                    break;
                case "salir"://SALIR
                    Environment.Exit(0);
                    break;
            }
        }
        
        //metodo de menu de cuentas del sistema
        public static void menuCuentas(listaSimpleCreador listaSimpleCreador)
        {
            int windowWidth = 120;
            int windowHeight = 40;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            elementosDecoracion.pintarGrafosAmarillo("      ╔════════════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(30));
            Console.WriteLine(@"  

            ░█████╗░ ██╗░░░██╗ ███████╗ ███╗░░██╗ ████████╗ ░█████╗░   ██████╗░ ███████╗  
            ██╔══██╗ ██║░░░██║ ██╔════╝ ████╗░██║ ╚══██╔══╝ ██╔══██╗   ██╔══██╗ ██╔════╝ 
            ██║░░╚═╝ ██║░░░██║ █████╗░░ ██╔██╗██║ ░░░██║░░░ ███████║   ██║░░██║ █████╗░░ 
            ██║░░██╗ ██║░░░██║ ██╔══╝░░ ██║╚████║ ░░░██║░░░ ██╔══██║   ██║░░██║ ██╔══╝░░ 
            ╚█████╔╝ ╚██████╔╝ ███████╗ ██║░╚███║ ░░░██║░░░ ██║░░██║   ██████╔╝ ███████╗  
            ░╚════╝░ ░╚═════╝░ ╚══════╝ ╚═╝░░╚══╝ ░░░╚═╝░░░ ╚═╝░░╚═╝   ╚═════╝░ ╚══════╝  

                            ██╗░░░██╗ ░██████╗ ██╗░░░██╗ ░█████╗░ ██████╗░ ██╗ ░█████╗░
                            ██║░░░██║ ██╔════╝ ██║░░░██║ ██╔══██╗ ██╔══██╗ ██║ ██╔══██╗
                            ██║░░░██║ ╚█████╗░ ██║░░░██║ ███████║ ██████╔╝ ██║ ██║░░██║
                            ██║░░░██║ ░╚═══██╗ ██║░░░██║ ██╔══██║ ██╔══██╗ ██║ ██║░░██║
                            ╚██████╔╝ ██████╔╝ ╚██████╔╝ ██║░░██║ ██║░░██║ ██║ ╚█████╔╝
                            ░╚═════╝░ ╚═════╝░ ░╚═════╝░ ╚═╝░░╚═╝ ╚═╝░░╚═╝ ╚═╝ ░╚════╝░
            ", Console.ForegroundColor = ConsoleColor.White);
            elementosDecoracion.pintarGrafosAmarillo("      ╚═════════════════════════════════════════════════════════════════════════════════════════════════╝\n".PadLeft(30));
            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write(">|", Console.ForegroundColor = ConsoleColor.Yellow); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("═══════════════════════════\n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            listaSimpleCreador.conteoLista();
            Console.WriteLine("");
            elementosDecoracion.tabularMenuRojo("1", "Ingreso de nueva cuenta\t\t"); elementosDecoracion.tabularMenuRojo("4", "Eliminar cuenta\n");
            elementosDecoracion.tabularMenuRojo("2", "Editar cuenta\t\t\t"); elementosDecoracion.tabularMenuRojo("5", "Eliminiar todo\n");
            elementosDecoracion.tabularMenuRojo("3", "Registro de cuenta\t\t\t"); elementosDecoracion.tabularMenuRojo("6", "Atras\n");
            Console.Write("\n\n\t\t\t[", Console.ForegroundColor = ConsoleColor.Yellow);Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Yellow);

            Console.Write("  [", Console.ForegroundColor = ConsoleColor.Yellow);Console.Write("Help", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Yellow);

            elementosDecoracion.pintarGrafosAmarillo("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            elementosDecoracion.pintarLetrasAmarillo("║", " >| Selecciona una opcion -> ");
            Console.ResetColor();
            string opc1 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (opc1.ToLower())
            {
                case "1"://agregar cuenta              
                    Console.Write("\nIngrese el nuevo usuario ");Console.Write(">| ", Console.ForegroundColor = ConsoleColor.Red);
                    string nuevoUsuario = Console.ReadLine();
                    Console.Write("Ingrese la nueva contraseña "); Console.Write(">| ", Console.ForegroundColor = ConsoleColor.Red);
                    string nuevaContraseña = Console.ReadLine();
                    listaSimpleCreador.Agregar(nuevoUsuario, nuevaContraseña);
                    Console.ReadKey();
                    Program.menuCuentas(listaSimpleCreador);
                    break;
                case "2"://editar cuenta
                    Console.Clear();
                    listaSimpleCreador.ImprimirCuentas();
                    Console.Write("\nIngrese el nombre de usuario que desea modificar "); Console.Write(">| ", Console.ForegroundColor = ConsoleColor.Red);
                    string usuario = Console.ReadLine();
                    Console.Write("Ingrese la nueva contraseña >| ");
                    string nuevacontra = Console.ReadLine();
                    listaSimpleCreador.Modificar(usuario, nuevacontra); // Llama al método Modificar
                    Console.ReadKey();
                    Program.menuCuentas(listaSimpleCreador);
                    break;
                case "3"://tabla
                    Console.Clear();
                    listaSimpleCreador.ImprimirCuentas();
                    Console.ReadKey();
                    Program.menuCuentas(listaSimpleCreador);
                    break;
                case "4"://eliminar
                    Console.Clear();
                    listaSimpleCreador.ImprimirCuentas();
                    Console.Write("Ingrese el nombre de usuario que desea eliminar: ");
                    string usuarioEliminar = Console.ReadLine();
                    listaSimpleCreador.Eliminar(usuarioEliminar); // Llama al método Eliminar
                    Console.ReadKey();
                    Program.menuCuentas(listaSimpleCreador);
                    break;
                case "5"://eliminar todo
                    listaSimpleCreador.EliminarTodo();
                    Console.ReadKey();
                    Program.menuCuentas(listaSimpleCreador);
                    break;
                case "6"://atras
                    return;
                case "help"://HELP
                    ayudaMenu.menuAyuda();
                    break;
                case "salir"://SALIR
                    Environment.Exit(0);
                    break;

            }
        }
       
        //metodo de menu de ambulancias
        public static void menuAmbulancia(listaDobleAmbulancias ListaDobleAmbulancias
                                            ,pilaAmbulancias PilaMovimientos
                                            ,listaDobleTrabajadores ListaDobleTrabajadores){

            int windowWidth = 147;
            int windowHeight = 50;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);


            string marca;
            int prioVelocidad;
            string placa;
            string codigo;
            string conductor;
            bool encontrado = false;


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            elementosDecoracion.pintarGrafosAzul("      ╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(30));
            Console.WriteLine(@"    
 
          ███╗░░░███╗ ███████╗ ███╗░░██╗ ██╗░░░██╗   ██████╗░ ███████╗   
          ████╗░████║ ██╔════╝ ████╗░██║ ██║░░░██║   ██╔══██╗ ██╔════╝  
          ██╔████╔██║ █████╗░░ ██╔██╗██║ ██║░░░██║   ██║░░██║ █████╗░░  
          ██║╚██╔╝██║ ██╔══╝░░ ██║╚████║ ██║░░░██║   ██║░░██║ ██╔══╝░░  
          ██║░╚═╝░██║ ███████╗ ██║░╚███║ ╚██████╔╝   ██████╔╝ ███████╗  
          ╚═╝░░░░░╚═╝ ╚══════╝ ╚═╝░░╚══╝ ░╚═════╝░   ╚═════╝░ ╚══════╝   

                                       ░█████╗░ ███╗░░░███╗ ██████╗░ ██╗░░░██╗ ██╗░░░░░ ░█████╗░ ███╗░░██╗ ░█████╗░ ██╗ ░█████╗░ ░██████╗
                                       ██╔══██╗ ████╗░████║ ██╔══██╗ ██║░░░██║ ██║░░░░░ ██╔══██╗ ████╗░██║ ██╔══██╗ ██║ ██╔══██╗ ██╔════╝
                                       ███████║ ██╔████╔██║ ██████╦╝ ██║░░░██║ ██║░░░░░ ███████║ ██╔██╗██║ ██║░░╚═╝ ██║ ███████║ ╚█████╗░
                                       ██╔══██║ ██║╚██╔╝██║ ██╔══██╗ ██║░░░██║ ██║░░░░░ ██╔══██║ ██║╚████║ ██║░░██╗ ██║ ██╔══██║ ░╚═══██╗
                                       ██║░░██║ ██║░╚═╝░██║ ██████╦╝ ╚██████╔╝ ███████╗ ██║░░██║ ██║░╚███║ ╚█████╔╝ ██║ ██║░░██║ ██████╔╝
                                       ╚═╝░░╚═╝ ╚═╝░░░░░╚═╝ ╚═════╝░ ░╚═════╝░ ╚══════╝ ╚═╝░░╚═╝ ╚═╝░░╚══╝ ░╚════╝░ ╚═╝ ╚═╝░░╚═╝ ╚═════╝░
", Console.ForegroundColor = ConsoleColor.White);
            elementosDecoracion.pintarGrafosAzul("       ╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n".PadLeft(30));
            Console.WriteLine("\n═══════════════════════════", Console.ForegroundColor = ConsoleColor.Blue);
            Console.Write(">|", Console.ForegroundColor = ConsoleColor.Blue); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.Blue);
            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Blue);
            Console.ResetColor();
            ListaDobleAmbulancias.conteoAmbulancias();
            Console.WriteLine("");
            elementosDecoracion.tabularMenuAzul("1", "Ingreso de Ambulancias\t\t"); elementosDecoracion.tabularMenuAzul("3", "Registro de Ambulancias\t\t\n");
            elementosDecoracion.tabularMenuAzul("2", "Ajustes de Ambulancias\t\t"); elementosDecoracion.tabularMenuAzul("4", "Historial de Movimientos\t\t\n");
            elementosDecoracion.tabularMenuAzul("5", "Estado\t\t\t\t");               elementosDecoracion.tabularMenuAzul("6", "Asignar Conductores\t\t\n");
            elementosDecoracion.tabularMenuAzul("7", "Regresar\t\t\n");

            Console.Write("\n\n\t\t\t\t[", Console.ForegroundColor = ConsoleColor.Blue);Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Blue);
            Console.Write("  [", Console.ForegroundColor = ConsoleColor.Blue);Console.Write("Help", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Blue);


            elementosDecoracion.pintarGrafosAzul("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            elementosDecoracion.pintarLetrasAzul("║", " >| Selecciona una opcion -> ");
            Console.ResetColor();
            string opc1 = Console.ReadLine(); string pos = "";
            switch (opc1.ToLower())
            {
                case "1"://Agregar
                    int opc;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Agregar por posicion\n");
                    elementosDecoracion.tabularMenuRojo("2", "Agregar al inicio\n");
                    elementosDecoracion.tabularMenuRojo("3", "Agregar al final\n");
                    elementosDecoracion.tabularMenuRojo("4", "Generar Ambulancias de manera Aleatoria.\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    }while(!int.TryParse(Console.ReadLine(), out opc));
                    switch (opc)
                    {
                        case 1:
                            ListaDobleAmbulancias.imprimirAmbu();
                            Console.WriteLine("* Elija el código apartir de la que se agregará su nueva ambulancia >| ");
                            pos = Console.ReadLine();
                            ListaDobleAmbulancias.BuscarDato(pos, ref encontrado);
                            if (encontrado==true)
                            {
                                Console.WriteLine("Ingrese la marca del vehiculo: (ejm: Toyota, Mercedes, etc.)");
                                marca= Console.ReadLine();
                                do
                                {
                                    Console.WriteLine("Ingrese un número para indicar el nivel de gravedad (0 o 1): ");
                                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                    elementosDecoracion.tabularMenuRojo("0", "No tan grave\n");
                                    elementosDecoracion.tabularMenuRojo("1", "Grave\n");
                                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                    if (int.TryParse(Console.ReadLine(), out prioVelocidad) && (prioVelocidad == 0 || prioVelocidad == 1))
                                    {
                                        Console.WriteLine($"Has ingresado el número: {prioVelocidad}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Entrada inválida. Intente nuevamente.");
                                    }
                                } while (prioVelocidad != 0 && prioVelocidad != 1);
                                Console.WriteLine("Ingrese la placa del auto:  (ejm. [ABC-111]");
                                placa = Console.ReadLine();
                                codigo = ListaDobleAmbulancias.GenerarCodigoAmbulancia();
                                Console.WriteLine("Se generó este código automaticamente: "+codigo);
                                conductor = "";
                                ListaDobleAmbulancias.insertarNodoAntesOtroAmbu(marca, prioVelocidad,placa,codigo,conductor,pos);
                                Console.WriteLine("\nAmbulancia almacenada correctamente", Console.ForegroundColor = ConsoleColor.Green);
                                PilaMovimientos.push("Agregar Ambulancia", codigo, "Se agrego según posicion", "Ambulancias");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias,PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 2:
                            Console.WriteLine("Ingrese la marca del vehiculo: (ejm: Toyota, Mercedes, etc.)");
                            marca = Console.ReadLine();
                            do
                            {
                                Console.WriteLine("Ingrese un número (0 o 1): ");
                                Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                elementosDecoracion.tabularMenuRojo("0", "No tan grave\n");
                                elementosDecoracion.tabularMenuRojo("1", "Grave\n");
                                Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                if (int.TryParse(Console.ReadLine(), out prioVelocidad) && (prioVelocidad == 0 || prioVelocidad == 1))
                                {
                                    Console.WriteLine($"Has ingresado el número: {prioVelocidad}");
                                }
                                else
                                {
                                    Console.WriteLine("Entrada inválida. Intente nuevamente.");
                                }
                            } while (prioVelocidad != 0 && prioVelocidad != 1);
                            Console.WriteLine("Ingrese la placa del auto:  (ejm. [ABC-111]");
                            placa = Console.ReadLine();
                            codigo = ListaDobleAmbulancias.GenerarCodigoAmbulancia();
                            Console.WriteLine("Se generó este código automaticamente: " + codigo);
                            conductor = "";
                            ListaDobleAmbulancias.insertarAlInicioAmbu(marca,prioVelocidad,placa,codigo, conductor);
                            PilaMovimientos.push("Agregar Ambulancia", codigo, "Se agrego al inicio", "Ambulancias");
                            Console.WriteLine("\nAmbulancia almacenada correctamente", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 3:
                            Console.WriteLine("Ingrese la marca del vehiculo: (ejm: Toyota, Mercedes, etc.)");
                            marca = Console.ReadLine();
                            do
                            {
                                Console.WriteLine("Ingrese un número (0 o 1): ");
                                Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                elementosDecoracion.tabularMenuRojo("0", "No tan grave\n");
                                elementosDecoracion.tabularMenuRojo("1", "Grave\n");
                                Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                if (int.TryParse(Console.ReadLine(), out prioVelocidad) && (prioVelocidad == 0 || prioVelocidad == 1))
                                {
                                    Console.WriteLine($"Has ingresado el número: {prioVelocidad}");
                                }
                                else
                                {
                                    Console.WriteLine("Entrada inválida. Intente nuevamente.");
                                }
                            } while (prioVelocidad != 0 && prioVelocidad != 1);
                            Console.WriteLine("Ingrese la placa del auto:  (ejm. [ABC-111]");
                            placa = Console.ReadLine();
                            codigo = ListaDobleAmbulancias.GenerarCodigoAmbulancia();
                            Console.WriteLine("Se generó este código automaticamente: " + codigo);
                            conductor = "";
                            ListaDobleAmbulancias.insertarAlInicioAmbu(marca, prioVelocidad, placa, codigo, conductor);
                            PilaMovimientos.push("Agregar Ambulancia", codigo, "Se agrego al final", "Ambulancias");
                            Console.WriteLine("\nAmbulancia almacenada correctamente", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 4:
                            Console.Clear();
                            int numeroAmbulancias;
                            do
                            {
                                Console.Write("Ingrese el número de Ambulancias que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroAmbulancias) || numeroAmbulancias <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroAmbulancias <= 0);
                            for (int i = 0; i < numeroAmbulancias; i++)
                            {
                                marca = ListaDobleAmbulancias.GenerarMarca();
                                prioVelocidad = ListaDobleAmbulancias.GenerarCeroUno();
                                placa = ListaDobleAmbulancias.GenerarPlaca();
                                codigo = ListaDobleAmbulancias.GenerarCodigoAmbulancia();
                                conductor = "";
                                ListaDobleAmbulancias.insertarAlInicioAmbu(marca, prioVelocidad, placa, codigo, conductor);
                                PilaMovimientos.push("Agregar Ambulancia", codigo, "Se agrego al inicio", "Ambulancias");
                            }
                            //ListaDobleTrabajadores.conteoTrabajador(); CONTEO
                            Console.Write("Ambulancias generadas correctamente\n", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);

                            return;
                        case 0:
                            return;
                    }
                    break;
                case "2"://Ajustes
                    int opc_1;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Modificar Datos de Ambulancias\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Ambulancia según Código\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc_1));
                    switch (opc_1)
                    {

                        case 1:
                            bool agregarM = false;
                            string cambio1 = "";
                            string DatoPila = "";
                            ListaDobleAmbulancias.ModificarAmbu(ref agregarM, ref cambio1, ref DatoPila);
                            if (agregarM == true)
                            {
                                PilaMovimientos.push("Modificar", DatoPila, cambio1, "Ambulancias");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias,PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 2:
                            Console.Clear();
                            ListaDobleAmbulancias.imprimirAmbu();
                            Console.WriteLine("Ingrese el código de la Ambulancia a eliminar de la lista");
                            string codigo_=Console.ReadLine();
                            bool encontro = false;
                            string codigo1 = "";
                            ListaDobleAmbulancias.eliminarNodoAmbuLD(codigo_, ref encontro, ref codigo1);
                            if (encontro == true)
                            {
                                PilaMovimientos.push("Eliminar Ambulancias", codigo1, "Se eliminó esta Ambulancia", "Ambulancias");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 0:
                            Program.menuAmbulancia(ListaDobleAmbulancias,PilaMovimientos, ListaDobleTrabajadores);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                    break;
                case "3":
                    Console.Clear();
                    ListaDobleAmbulancias.imprimirAmbu();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuAmbulancia(ListaDobleAmbulancias,PilaMovimientos, ListaDobleTrabajadores);
                    break;
                case "4":
                    int opc__1;
                    do { 
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.White;
                    //Historial almacena los movimientos
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Mostrar Historial de Movimientos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Último Movimiento\n");
                    elementosDecoracion.tabularMenuRojo("3", "Eliminar todo el Historial\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc__1));
                    switch (opc__1)
                    {
                        case 1:
                            PilaMovimientos.muestraPila("Ambulancias");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 2:
                            PilaMovimientos.pop();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                            return;
                        case 3:
                            PilaMovimientos.EliminarPorTipo("Ambulancias");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                            return;
                    }
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                    break;
                case "5":
                    break;
                case "6":
                    Program.EnlazarConductorAmbulancia(ListaDobleTrabajadores, ListaDobleAmbulancias, PilaMovimientos);
                    Console.WriteLine();
                    ListaDobleAmbulancias.imprimirAmbu();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuAmbulancia(ListaDobleAmbulancias, PilaMovimientos, ListaDobleTrabajadores);
                    break;
                case "7":
                    return;
                case "help"://HELP
                    ayudaMenu.menuAyuda();
                    break;
                case "salir"://SALIR
                    Environment.Exit(0);
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        //menu de almacen de productos
        public static void menuAlmacen(listaCircularInventario ListaCircularInventario
                                        , colaAlmacen ColaAlmacen
                                        ,pilaAlmacen PilaMovimientos){

            int windowWidth = 147;
            int windowHeight = 50;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);

            string nombre_prod;
            string codigo;
            int num_prod;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            elementosDecoracion.pintarGrafosRojo("       ╔════════════════════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(30));
            Console.WriteLine(@"    

          ███╗░░░███╗ ███████╗ ███╗░░██╗ ██╗░░░██╗   ██████╗░ ███████╗   
          ████╗░████║ ██╔════╝ ████╗░██║ ██║░░░██║   ██╔══██╗ ██╔════╝   
          ██╔████╔██║ █████╗░░ ██╔██╗██║ ██║░░░██║   ██║░░██║ █████╗░░   
          ██║╚██╔╝██║ ██╔══╝░░ ██║╚████║ ██║░░░██║   ██║░░██║ ██╔══╝░░   
          ██║░╚═╝░██║ ███████╗ ██║░╚███║ ╚██████╔╝   ██████╔╝ ███████╗   
          ╚═╝░░░░░╚═╝ ╚══════╝ ╚═╝░░╚══╝ ░╚═════╝░   ╚═════╝░ ╚══════╝
                         
                                            ░█████╗░ ██╗░░░░░ ███╗░░░███╗ ░█████╗░ ░█████╗░ ███████╗ ███╗░░██╗
                                            ██╔══██╗ ██║░░░░░ ████╗░████║ ██╔══██╗ ██╔══██╗ ██╔════╝ ████╗░██║
                                            ███████║ ██║░░░░░ ██╔████╔██║ ███████║ ██║░░╚═╝ █████╗░░ ██╔██╗██║
                                            ██╔══██║ ██║░░░░░ ██║╚██╔╝██║ ██╔══██║ ██║░░██╗ ██╔══╝░░ ██║╚████║
                                            ██║░░██║ ███████╗ ██║░╚═╝░██║ ██║░░██║ ╚█████╔╝ ███████╗ ██║░╚███║
                                            ╚═╝░░╚═╝ ╚══════╝ ╚═╝░░░░░╚═╝ ╚═╝░░╚═╝ ░╚════╝░ ╚══════╝ ╚═╝░░╚══╝
            ", Console.ForegroundColor = ConsoleColor.White);
            elementosDecoracion.pintarGrafosRojo("       ╚════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n".PadLeft(30));
            Console.WriteLine("\n═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write(">|", Console.ForegroundColor = ConsoleColor.Red); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
            ListaCircularInventario.conteoInventario();
            Console.WriteLine("");
            elementosDecoracion.tabularMenuAzul("1", "Ingreso de Nuevos Productos\t\t\t"); elementosDecoracion.tabularMenuAzul("3", "Registro de Productos\t\t\n");
            elementosDecoracion.tabularMenuAzul("2", "Ajustes de Productos\t\t\t"); elementosDecoracion.tabularMenuAzul("4", "Historial de Movimientos (Pila)\t\t\n");
            elementosDecoracion.tabularMenuAzul("5", "Salida de Productos (Cola)\t\t\t"); elementosDecoracion.tabularMenuAzul("6", "Regresar\t\t\n");

            Console.Write("\n\n\t\t\t\t\t[", Console.ForegroundColor = ConsoleColor.Red);Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write("  [", Console.ForegroundColor = ConsoleColor.Red);Console.Write("Help", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Red);

            elementosDecoracion.pintarGrafosRojo("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            elementosDecoracion.pintarLetrasCyan("║", " >| Selecciona una opcion -> ");
            Console.ResetColor();
            string opc1 = Console.ReadLine();
            switch (opc1.ToLower())
            {
                case "1"://Agregar
                    int opc2;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Agregar Caja de Productos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Generar Cajas de Productos de forma aleatoria\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc2)) ;
                    switch (opc2)
                    {
                        case 1:
                            
                            Console.WriteLine("* Ingrese el nombre del producto: ");
                            nombre_prod = Console.ReadLine();

                            codigo=ListaCircularInventario.GenerarCodigo();
                            Console.WriteLine("Se Generó este código para su producto: "+codigo);
                            Console.Write("Presione enter para continuar >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Console.ForegroundColor= ConsoleColor.White;
                            do
                            {
                                Console.WriteLine("* Ingrese el número de productos en la caja: ");
                                if (!int.TryParse(Console.ReadLine(), out num_prod) || num_prod <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (num_prod <= 0);
                            ListaCircularInventario.Insertar(nombre_prod, codigo, num_prod);
                            PilaMovimientos.push("Agregar Producto", codigo, "Se agregó un nuevo producto en el Almacén", "Almacen");
                            Console.WriteLine("\nProducto almacenado correctamnete", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario,ColaAlmacen, PilaMovimientos);
                            return;
                        case 2:
                            Console.Clear();
                            int numeroCajas;
                            do
                            {
                                Console.Write("Ingrese el número de productos que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroCajas) || numeroCajas <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroCajas <= 0);
                            string nombre;
                            string codigo_;
                            int stock_;
                            Random random = new Random();
                            for (int i = 0; i < numeroCajas; i++)
                            {
                                nombre = ListaCircularInventario.GenerarNombre();
                                codigo_ = ListaCircularInventario.GenerarCodigo();
                                stock_ = random.Next(5, 21);
                                ListaCircularInventario.Insertar(nombre, codigo_,stock_);
                                PilaMovimientos.push("Agregar Producto", codigo_, "Se agregó un nuevo producto en el Almacén", "Almacen");
                            }
                            ListaCircularInventario.conteoInventario();
                            Console.Write("Generados correctamente \n", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);

                            return;
                        case 0:
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                    break;//metodo para agregar trabajadores

                case "2":
                    int opc_1;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Modificar Datos de Productos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Caja de Productos según Código\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc_1)) ;
                    switch (opc_1)
                    {

                        case 1:
                            bool agregarM = false;
                            string cambio1 = "";
                            string DatoPila = "";
                            ListaCircularInventario.Modificar(ref agregarM, ref cambio1, ref DatoPila);
                            if (agregarM == true)
                            {
                                PilaMovimientos.push("Modificar", DatoPila, cambio1, "Almacen");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 2:
                            Console.Clear();
                            ListaCircularInventario.Mostrar();
                            Console.WriteLine("Ingrese el Código del producto a eliminar de la lista");
                            string codigo_borrar = Console.ReadLine();
                            bool encontro = false;
                            string codigo1 = "";
                            ListaCircularInventario.Eliminar(codigo_borrar, ref codigo1, ref encontro);
                            if (encontro == true)
                            {
                                Console.WriteLine("Se elimino correctamente este producto");
                                PilaMovimientos.push("Eliminar Producto", codigo1, "Se eliminó este Producto", "Almacen");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 0:
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                    break;

                case "3":
                    ListaCircularInventario.Mostrar();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);

                    break;
                case "4":
                    int opc__1;
                    do { 
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.White;
                    //Historial almacena los movimientos
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Mostrar Historial de Movimientos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Último Movimiento\n");
                    elementosDecoracion.tabularMenuRojo("3", "Eliminar todo el Historial\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc__1));
                    switch (opc__1)
                    {
                        case 1:
                            PilaMovimientos.muestraPila("Almacen");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 2:
                            PilaMovimientos.pop();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 3:
                            PilaMovimientos.EliminarPorTipo("Almacen");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                    }
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                    break;
                case "5":
                    int opc_2;
                    do {
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.WriteLine("SALIDA DE PRODUCTOS");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Añadir a cola la salida de un producto\n");
                    elementosDecoracion.tabularMenuRojo("2", "Decolar primera posición\n");
                    elementosDecoracion.tabularMenuRojo("3", "Mostrar cola\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc_2)) ;
                    switch (opc_2)
                    {

                        case 1:
                            ListaCircularInventario.Mostrar();
                            Console.WriteLine("Ingresa el Código del producto a extraer: ");
                            codigo=Console.ReadLine();
                            do
                            {
                                Console.WriteLine("Ingresa la cantidad a extraer: ");
                                if (!int.TryParse(Console.ReadLine(), out num_prod) || num_prod <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (num_prod <= 0);
                            bool existe1=false;
                            ColaAlmacen.Encolar(codigo,num_prod,ListaCircularInventario,ref existe1);
                            if(existe1==true)
                            {
                                string info ="Se encoló la salida de "+num_prod+ ".";
                                PilaMovimientos.push("Se encoló la Salida de Stock",codigo,info,"Almacen");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 2:
                            Console.Clear();
                            string info1="";
                            bool si = false;
                            string code__1="";
                            ColaAlmacen.Decolar(ListaCircularInventario,ref info1,ref si,ref code__1);Console.ForegroundColor = ConsoleColor.Red;
                            if (si == true)
                            {
                                PilaMovimientos.push("Se decoló la salida de Stock", code__1,info1,"Almacen");
                            }
                            ListaCircularInventario.Mostrar();Console.ForegroundColor= ConsoleColor.White;
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 3:
                            Console.Clear();
                            ColaAlmacen.Mostrar();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                        case 0:
                            Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuAlmacen(ListaCircularInventario, ColaAlmacen, PilaMovimientos);
                    break;
                case "6":
                    return;//regresar

                case "help"://HELP
                    ayudaMenu.menuAyuda();
                    break;
                case "salir"://SALIR
                    Environment.Exit(0);
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        //menu de sedes hospitalarias
        public static void menuSedes(listaDobleSedes ListaDobleSedes
                                        ,pilaSedes PilaMovimientos){

            int windowWidth = 147;
            int windowHeight = 50;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);

            string nombre_sede ="";
            string ubicacion="";
            int numero=0;
            string codigo = "";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            elementosDecoracion.pintarGrafosCyan("                  ╔══════════════════════════════════════════════════════════════════════════════════════════╗".PadLeft(30));
            Console.WriteLine(@"   

                      ███╗░░░███╗ ███████╗ ███╗░░██╗ ██╗░░░██╗   ██████╗░ ███████╗   
                      ████╗░████║ ██╔════╝ ████╗░██║ ██║░░░██║   ██╔══██╗ ██╔════╝ 
                      ██╔████╔██║ █████╗░░ ██╔██╗██║ ██║░░░██║   ██║░░██║ █████╗░░  
                      ██║╚██╔╝██║ ██╔══╝░░ ██║╚████║ ██║░░░██║   ██║░░██║ ██╔══╝░░  
                      ██║░╚═╝░██║ ███████╗ ██║░╚███║ ╚██████╔╝   ██████╔╝ ███████╗   
                      ╚═╝░░░░░╚═╝ ╚══════╝ ╚═╝░░╚══╝ ░╚═════╝░   ╚═════╝░ ╚══════╝   
          
                                                            ░██████╗ ███████╗ ██████╗░ ███████╗ ░██████╗
                                                            ██╔════╝ ██╔════╝ ██╔══██╗ ██╔════╝ ██╔════╝
                                                            ╚█████╗░ █████╗░░ ██║░░██║ █████╗░░ ╚█████╗░
                                                            ░╚═══██╗ ██╔══╝░░ ██║░░██║ ██╔══╝░░ ░╚═══██╗
                                                            ██████╔╝ ███████╗ ██████╔╝ ███████╗ ██████╔╝
                                                            ╚═════╝░ ╚══════╝ ╚═════╝░ ╚══════╝ ╚═════╝░
            ░", Console.ForegroundColor = ConsoleColor.White);
            elementosDecoracion.pintarGrafosCyan("                  ╚════════════════════════════════════════════════════════════════════════════════════════╝\n".PadLeft(30));
            Console.WriteLine("\n═══════════════════════════", Console.ForegroundColor = ConsoleColor.DarkCyan);
            Console.Write(">|", Console.ForegroundColor = ConsoleColor.DarkCyan); Console.Write(" Selecciona la accion ", Console.ForegroundColor = ConsoleColor.White); Console.Write("|<\n", Console.ForegroundColor = ConsoleColor.Cyan);
            Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.DarkCyan);         
            Console.ResetColor();
            ListaDobleSedes.conteoSedes(); 
            Console.WriteLine("");
            elementosDecoracion.tabularMenuVerde("1", "Ingreso de Nueva Sede\t\t"); elementosDecoracion.tabularMenuVerde("3", "Registro de Sedes\t\t\n");
            elementosDecoracion.tabularMenuVerde("2", "Ajustes de Sedes\t\t\t"); elementosDecoracion.tabularMenuVerde("4", "Historial de Movimientos (Pila)\t\t\n");
            elementosDecoracion.tabularMenuVerde("5", "Regresar\t\t\n");
            Console.Write("\n\n\t\t\t\t[", Console.ForegroundColor = ConsoleColor.Yellow);Console.Write("Salir", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  /", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write("  [", Console.ForegroundColor = ConsoleColor.Yellow);Console.Write("Help", Console.ForegroundColor = ConsoleColor.White);Console.Write("]  \n", Console.ForegroundColor = ConsoleColor.Yellow);

            elementosDecoracion.pintarGrafosCyan("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
            elementosDecoracion.pintarLetrasCyan("║", " >| Selecciona una opcion -> ");
            Console.ResetColor();
            string opc1 = Console.ReadLine();
            switch (opc1.ToLower())
            {
                case "1"://Agregar
                    int opc2;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Agregar Sede según Posición(Código)\n");
                    elementosDecoracion.tabularMenuRojo("2", "Agregar al inicio\n");
                    elementosDecoracion.tabularMenuRojo("3", "Agregar al final\n");
                    elementosDecoracion.tabularMenuRojo("4", "Generar Sedes de manera aleatoria\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc2));
                    switch (opc2)
                    {
                        case 1://Agregar segú código
                            ListaDobleSedes.imprimirLD();
                            Console.WriteLine("* Ingrese el Código de la Sede a partir de la que se agregará datos antes");
                            string pos = Console.ReadLine();
                            bool encontrado = false;
                            ListaDobleSedes.BuscarDato(pos, ref encontrado);
                            if (encontrado == true)
                            {
                                Console.WriteLine("* Ingrese el nombre de la sede: ");
                                nombre_sede = Console.ReadLine();

                                Console.WriteLine("* Ingrese la dirección de la sede: (Ejm. Av. Javier Prado Este 1234)");
                                ubicacion= Console.ReadLine();

                                //Validar num de telefono
                                do
                                {
                                    Console.WriteLine("* Ingrese el número de Teléfono: ");
                                    if (!int.TryParse(Console.ReadLine(), out numero) || numero <= 0)
                                    {
                                        // Mostrar un mensaje de error si la entrada no es un número entero válido
                                        Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                    }
                                } while (numero <= 0);
                                codigo = ListaDobleSedes.GenerarCodigo();
                                Console.WriteLine("* Se generó el siguiente código automaticamente: " + codigo);
                                ListaDobleSedes.insertarNodoAntesOtro(nombre_sede, ubicacion, numero,codigo, pos);
                                PilaMovimientos.push("Agregar Sede", codigo, "Se agrego la Sede a partir del código de otra sede", "sede");
                                Console.WriteLine("\nSede almacenada correctamente", Console.ForegroundColor = ConsoleColor.Green);
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 2://Agregar al inicio
                            Console.WriteLine("* Ingrese el nombre de la sede: ");
                            nombre_sede = Console.ReadLine();

                            Console.WriteLine("* Ingrese la dirección de la sede: (Ejm. Av. Javier Prado Este 1234)");
                            ubicacion = Console.ReadLine();

                            //Validar num de telefono
                            do
                            {
                                Console.WriteLine("* Ingrese el número de Teléfono: ");
                                if (!int.TryParse(Console.ReadLine(), out numero) || numero <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numero <= 0);
                            codigo =ListaDobleSedes.GenerarCodigo();
                            Console.WriteLine("* Se generó el siguiente código automaticamente: "+codigo);
                            ListaDobleSedes.insertaAlInicioLD(nombre_sede, ubicacion, numero, codigo);
                            PilaMovimientos.push("Agregar Sede", codigo, "Se agrego la Sede al inicio", "sede");
                            Console.WriteLine("\nSede almacenada correctamente", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 3://Agregar al final
                            Console.WriteLine("* Ingrese el nombre de la sede: ");
                            nombre_sede = Console.ReadLine();

                            //Validar edad paciente
                            Console.WriteLine("* Ingrese la dirección de la sede: (Ejm. Av. Javier Prado Este 1234)");
                            ubicacion = Console.ReadLine();

                            //Validar numero DNI
                            Console.WriteLine("* Ingrese el número de Teléfono: ");
                            if (!int.TryParse(Console.ReadLine(), out numero))
                            {
                                Console.WriteLine("Registre información válida del paciente.");
                                Console.ReadKey();
                            }
                            codigo = ListaDobleSedes.GenerarCodigo();
                            Console.WriteLine("* Se generó el siguiente código automaticamente: " + codigo);
                            ListaDobleSedes.insertaAlFinalLD(nombre_sede, ubicacion, numero, codigo);
                            PilaMovimientos.push("Agregar Sede", codigo, "Se agrego la Sede al final", "sede");
                            Console.WriteLine("\nSede almacenada correctamente", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 4://Generar aleatoriamente
                            Console.Clear();
                            int numeroSedes;
                            do
                            {
                                Console.Write("Ingrese el número de Sedes que desea generar: ");
                                if (!int.TryParse(Console.ReadLine(), out numeroSedes) || numeroSedes <= 0)
                                {
                                    // Mostrar un mensaje de error si la entrada no es un número entero válido
                                    Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                }
                            } while (numeroSedes <= 0);
                            string nombre="";
                            string ubicacion_="";
                            int numero_=0;
                            string codigo_ = "";
                            Random random = new Random();
                            for (int i = 0; i < numeroSedes; i++)
                            {
                                nombre = ListaDobleSedes.GenerarNombreHospital();
                                ubicacion_= ListaDobleSedes.GenerarDireccion();
                                numero_=ListaDobleSedes.GenerarNumeroTelefono();
                                codigo_ = ListaDobleSedes.GenerarCodigo();
                                ListaDobleSedes.insertaAlInicioLD(nombre, ubicacion_, numero_, codigo_);
                                PilaMovimientos.push("Agregar Sede", codigo_, "Se agregó al inicio", "sede");
                            }
                            ListaDobleSedes.conteoSedes();Console.Write("generadas correctamente\n", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 0:
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                    break;//metodo para agregar trabajadores

                case "2":
                    int opc_1;
                    do { 
                    Console.Clear();
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Modificar Datos de Sede\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Sede según Código\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc_1)) ;
                    switch (opc_1)
                    {

                        case 1:
                            bool agregarM = false;
                            string cambio1 = "";
                            string DatoPila = "";
                            //ListaSimplePaciente.Modificar(ref agregarM, ref cambio1, ref DatoPila);
                            ListaDobleSedes.Modificar(ref agregarM, ref cambio1, ref DatoPila);
                            if (agregarM == true)
                            {
                                PilaMovimientos.push("Modificar", DatoPila, cambio1, "sede");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 2:
                            Console.Clear();
                            ListaDobleSedes.imprimirLD();
                            Console.WriteLine("Ingrese el Código de la Sede a eliminar de la lista");
                            string codigo_borrar=Console.ReadLine();
                            bool encontro = false;
                            string dni1 = "";
                            ListaDobleSedes.eliminarNodoLD(codigo_borrar, ref encontro, ref dni1);
                            if (encontro == true)
                            {
                                Console.WriteLine("Se eliminó correctamente esta sede");
                                PilaMovimientos.push("Eliminar Sede", dni1, "Se eliminó esta sede", "sede");
                            }
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 0:
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                    }
                    Console.ReadKey();
                    Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                    break;

                case "3":
                    Console.Clear();
                    ListaDobleSedes.imprimirLD();
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                    break;
                case "4":
                    int opc__1;
                    do { 
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.White;
                    //Historial almacena los movimientos
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    elementosDecoracion.tabularMenuRojo("1", "Mostrar Historial de Movimientos\n");
                    elementosDecoracion.tabularMenuRojo("2", "Eliminar Último Movimiento\n");
                    elementosDecoracion.tabularMenuRojo("3", "Eliminar todo el Historial\n");
                    elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                    Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
                    } while (!int.TryParse(Console.ReadLine(), out opc__1)) ;
                    switch (opc__1)
                    {
                        case 1:
                            PilaMovimientos.muestraPila("sede");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 2:
                            PilaMovimientos.pop();
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                        case 3:
                            PilaMovimientos.EliminarPorTipo("sede");
                            Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                            Console.ReadKey();
                            Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                            return;
                    }
                    Console.Write("Presione enter para volver >|", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadKey();
                    Program.menuSedes(ListaDobleSedes, PilaMovimientos);
                    break;
                case "5":
                    return;//regresar

                case "help"://HELP
                    ayudaMenu.menuAyuda();
                    break;
                case "salir"://SALIR
                    Environment.Exit(0);
                    break;
            }
        }

        
        
        
        
        
        
        //metodo para generar pdf de reporte de kardex
        public static void GenerarReporteKardex(listaSimplePaciente ListaSimplePaciente
                                      , listaSimpleCreador ListaSimpleCreador
                                      , listaDobleTrabajadores ListaDobleTrabajadores
                                      , listaDobleSedes ListaDobleSedes
                                      , listaDobleAmbulancias listaDobleAmbulancias
                                      , listaCircularInventario listaCircularInventario,
                                       listaCircularReporte listaCircular, string path)
        {
            nodoInventario actual = listaCircularInventario.lista;
            if (actual == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Document document = new Document();

            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();

            // Título del reporte
            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Paragraph title = new Paragraph("Reporte de Inventario", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20f; // Añadir espacio después del título
            document.Add(title);

            // Añadir un espacio en blanco
            // Datos debajo del titulo 
            document.Add(new Paragraph("Hay " + listaCircularInventario.conteoInventario() + " cajas de medicamentos"));
            document.Add(new Paragraph("\n"));

            // Crear una tabla con 3 columnas
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100; // Ancho de la tabla

            // Añadir encabezados a la tabla
            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            table.AddCell(new PdfPCell(new Phrase("Nombre", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Código", headerFont)));
            table.AddCell(new PdfPCell(new Phrase("Stock", headerFont)));

            // Añadir datos del inventario a la tabla
            do
            {
                table.AddCell(new PdfPCell(new Phrase(actual.nombre)));
                table.AddCell(new PdfPCell(new Phrase(actual.codigo.ToString())));
                table.AddCell(new PdfPCell(new Phrase(actual.stock.ToString())));
                actual = actual.Sgte;
            } while (actual != listaCircularInventario.lista);

            // Añadir la tabla al documento
            table.SpacingBefore = 20f; // Añadir espacio antes de la tabla
            table.SpacingAfter = 20f; // Añadir espacio después de la tabla
            document.Add(table);

            // Añadir un espacio en blanco
            document.Add(new Paragraph("\n"));

            // Pie de página
            iTextSharp.text.Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD);
            Paragraph footer = new Paragraph("Estructura de Datos - UPN 20241 ESTD 4708 - GRUPO 5", footerFont);
            footer.Alignment = Element.ALIGN_CENTER;
            footer.SpacingBefore = 20f; // Añadir espacio antes del pie de página
            document.Add(footer);

            document.Close();

            Console.WriteLine("PDF generado exitosamente.");
        }

        //metodo para generar pdf de reporte de pacientes con medicos
        public static void GenerarReportePacientes(listaSimplePaciente ListaSimplePaciente
                                       , listaSimpleCreador ListaSimpleCreador
                                       , listaDobleTrabajadores ListaDobleTrabajadores
                                       , listaDobleSedes ListaDobleSedes
                                       , listaDobleAmbulancias listaDobleAmbulancias
                                       , listaCircularInventario listaCircularInventario,
                                        listaCircularReporte listaCircular, string path)
        {

            Nodo_Paciente l = ListaSimplePaciente.lista;
            Document document = new Document();

            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();

            // Título del reporte         
            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Paragraph title = new Paragraph("Reporte de Pacientes", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Paragraph("\n"));

            // Datos debajo del titulo 
            document.Add(new Paragraph("Hay "+ ListaSimplePaciente.tamanoLista()+" pacientes en el sistema hospitalario"));
            document.Add(new Paragraph("\n")); // Añade una nueva línea


            // Crear una tabla con 6 columnas
            PdfPTable table = new PdfPTable(6);
            table.WidthPercentage = 100; // Ancho de la tabla

            // Añadir encabezados a la tabla
            table.AddCell("Nombre del Paciente");
            table.AddCell("Edad");
            table.AddCell("Número de DNI");
            table.AddCell("Seguro Médico");
            table.AddCell("Malestares");
            table.AddCell("Doctor Asignado");

            // Añadir datos de los pacientes a la tabla
            while (l != null)
            {
                table.AddCell(l.Nombre_paciente);
                table.AddCell(l.Edad_paciente.ToString());
                table.AddCell(l.Nro_dni_paciente.ToString());
                table.AddCell(l.Seguro_med);
                table.AddCell(l.Malestares_paciente);
                table.AddCell(l.Doctor_asignado);
                l = l.Sgte;
            }

            // Añadir la tabla al documento
            document.Add(table);

            // Pie de página
            iTextSharp.text.Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD);
            Paragraph footer = new Paragraph("Estructura de Datos - UPN 20241 ESTD 4708 - GRUPO 5", footerFont);
            footer.Alignment = Element.ALIGN_CENTER;
            document.Add(footer);

            document.Close();
        }

        //metodo para generar pdf de reporte de los datos del sistema un conteo 
        public static void generarReporteDatos(listaSimplePaciente ListaSimplePaciente
                                       , listaSimpleCreador ListaSimpleCreador
                                       , listaDobleTrabajadores ListaDobleTrabajadores
                                       , listaDobleSedes ListaDobleSedes
                                       , listaDobleAmbulancias listaDobleAmbulancias
                                       , listaCircularInventario listaCircularInventario,
                                        listaCircularReporte listaCircular, string path)
        {

            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();
            document.Add(new Paragraph("            Reporte general del sistema hospitalario            "));
            document.Add(new Paragraph("----------------------------------------------------------------"));
            document.Add(new Paragraph("                                         "));
            document.Add(new Paragraph("Datos actualizados del sistema:"));
            document.Add(new Paragraph($"Cuentas: {ListaSimpleCreador.conteoLista()}"));
            document.Add(new Paragraph($"Pacientes: {ListaSimplePaciente.tamanoLista()}"));
            document.Add(new Paragraph($"Trabajadores: {ListaDobleTrabajadores.conteoTrabajador()}"));
            document.Add(new Paragraph($"Medicos: {ListaDobleTrabajadores.conteoMedicos()}"));
            document.Add(new Paragraph($"Supervisores: {ListaDobleTrabajadores.conteoSupervisor()}"));
            document.Add(new Paragraph($"Conductores: {ListaDobleTrabajadores.conteoConductor()}"));
            document.Add(new Paragraph($"Trabajadores de Limpieza: {ListaDobleTrabajadores.conteoLimpieza()}"));
            document.Add(new Paragraph($"Sedes: {ListaDobleSedes.conteoSedes()}"));
            document.Add(new Paragraph($"Almacen: {listaCircularInventario.conteoInventario()}"));
            document.Add(new Paragraph($"Ambulancias: {listaDobleAmbulancias.conteoAmbulancias()}"));
            document.Add(new Paragraph("                                                           "));
            document.Add(new Paragraph("-----------------------------------------------------------------"));
            document.Add(new Paragraph($"GRUPO 5 - docente: Juan Tapia"));
            document.Add(new Paragraph("-----------------------------------------------------------------"));



            document.Close();
        }

        //metodo de menu de reportes en pdf
        public static void menuReporte(listaSimplePaciente ListaSimplePaciente
                                       , listaSimpleCreador ListaSimpleCreador
                                       , listaDobleTrabajadores ListaDobleTrabajadores
                                       , listaDobleSedes ListaDobleSedes
                                       , listaDobleAmbulancias listaDobleAmbulancias
                                       , listaCircularInventario listaCircularInventario
                                       , listaCircularReporte listaCircularReporte
                                       , pilaPacientes PilaPacientes, pilaTrabajadores PilaTrabajadores, pilaAlmacen PilaAlmacen, pilaSedes PilaSedes, pilaAmbulancias PilaAmbulancias
                                       , colaIngresoDeAmbulancias ColaConPrioridad
                                       , colaAlmacen ColaAlmacen) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            int windowWidth = 66;
            int windowHeight = 20;
            // Ajustar el tamaño de la ventana de la consola
            Console.SetWindowSize(windowWidth, windowHeight);
            int opc;
            do
            {
                Console.Clear();
                Console.WriteLine(@"
                        
██████╗░ ███████╗ ██████╗░ ░█████╗░ ██████╗░ ████████╗ ███████╗
██╔══██╗ ██╔════╝ ██╔══██╗ ██╔══██╗ ██╔══██╗ ╚══██╔══╝ ██╔════╝
██████╔╝ █████╗░░ ██████╔╝ ██║░░██║ ██████╔╝ ░░░██║░░░ █████╗░░
██╔══██╗ ██╔══╝░░ ██╔═══╝░ ██║░░██║ ██╔══██╗ ░░░██║░░░ ██╔══╝░░
██║░░██║ ███████╗ ██║░░░░░ ╚█████╔╝ ██║░░██║ ░░░██║░░░ ███████╗
╚═╝░░╚═╝ ╚══════╝ ╚═╝░░░░░ ░╚════╝░ ╚═╝░░╚═╝ ░░░╚═╝░░░ ╚══════╝
                    
                     ");
                Console.WriteLine("═══════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                elementosDecoracion.tabularMenuRojo("1", "Generar Reporte de Datos\n");
                elementosDecoracion.tabularMenuRojo("2", "Generar Reporte de Pacientes \n");
                elementosDecoracion.tabularMenuRojo("3", "Generar Reporte de Kardex inventario \n");
                elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                Console.WriteLine("═══════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                Console.Write("Selecciona >| ", Console.ForegroundColor = ConsoleColor.White);
            } while (!int.TryParse(Console.ReadLine(), out opc));
            switch (opc)
            {

                case 1:
                    Program.generarReporteDatos(ListaSimplePaciente, ListaSimpleCreador, ListaDobleTrabajadores, ListaDobleSedes, listaDobleAmbulancias, listaCircularInventario, listaCircularReporte, "reporte Datos.pdf");
                    Console.Clear();
                    Console.WriteLine("Se genero el pdf como reporte del sistema en\n : Gestor Medico de Referencias\\bin\\Debug\\reporte Datos");
                    Console.ReadKey();
                    return;
                case 2:
                    Program.GenerarReportePacientes(ListaSimplePaciente, ListaSimpleCreador, ListaDobleTrabajadores, ListaDobleSedes, listaDobleAmbulancias, listaCircularInventario, listaCircularReporte, "reporte Pacientes.pdf");
                    Console.Clear();
                    Console.WriteLine("Se genero el pdf como reporte del sistema en\n : Gestor Medico de Referencias\\bin\\Debug\\reporte Pacientes");
                    Console.ReadKey();
                    return;
                case 3:
                    Program.GenerarReporteKardex(ListaSimplePaciente, ListaSimpleCreador, ListaDobleTrabajadores, ListaDobleSedes, listaDobleAmbulancias, listaCircularInventario, listaCircularReporte, "reporte Kardex.pdf");
                    Console.Clear();
                    Console.WriteLine("Se genero el pdf como reporte del sistema en\n : Gestor Medico de Referencias\\bin\\Debug\\reporte Kardex");
                    Console.ReadKey();
                    return;
                case 0:
                    return;
            }

        }
        //metdos para la cola de prioridad
        static void EncolarPacientesGraves(listaSimplePaciente listaPacientes, listaDobleAmbulancias listaAmbulancias, colaIngresoDeAmbulancias cola)
        {
            Nodo_Paciente pacienteActual = listaPacientes.GetFirstPaciente();
            nodoAmbulancias ambulanciaActual = listaAmbulancias.primeraAmbulancia();

            while (pacienteActual != null)
            {
                if (pacienteActual.Gravedad_paciente == "si")
                {
                    bool ambulanciaAsignada = false;

                    while (ambulanciaActual != null && !ambulanciaAsignada)
                    {
                        int prioridad = DeterminarPrioridad(pacienteActual, ambulanciaActual);
                        nodoColaPrioAmbulancias nuevoNodo = new nodoColaPrioAmbulancias
                        {
                            ListaPaciente = pacienteActual,
                            ListaAmbulancias = ambulanciaActual,
                            Prioridad = prioridad
                        };

                        cola.InsertarEnColaConPrioridad(nuevoNodo);
                        ambulanciaAsignada = true;  // Asignar solo una ambulancia por paciente grave
                        ambulanciaActual = ambulanciaActual.Sgte;
                    }
                }
                pacienteActual = pacienteActual.Sgte;
            }

            Console.WriteLine("Pacientes graves han sido encolados con éxito.");
        }
        static int DeterminarPrioridad(Nodo_Paciente paciente, nodoAmbulancias ambulancia)
        {
            if (paciente.Edad_paciente >= 65)
            {
                if (ambulancia.PrioVelocidad == 1)
                {
                    return 1; // Alta prioridad (paciente mayor y ambulancia rápida)
                }
                else
                {
                    return 2; // Media prioridad (paciente mayor pero ambulancia normal)
                }
            }
            else
            {
                if (ambulancia.PrioVelocidad == 1)
                {
                    return 3; // Media prioridad (paciente menor pero ambulancia rápida)
                }
                else
                {
                    return 4; // Baja prioridad (paciente menor y ambulancia normal)
                }
            }
        }
    }
}

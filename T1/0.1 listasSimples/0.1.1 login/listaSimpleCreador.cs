using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T1._0._1_listasSimples._0._1._1_login
{
    public class listaSimpleCreador
    {
        private nodoCreador cabeza;

        public listaSimpleCreador()
        {
            cabeza = null;
        }

        //LOGIN


        //metodo para agregar una cuenta ala lista simple
        public void Agregar(string usuario, string contraseña)
        {
            nodoCreador nuevonodocreador = new nodoCreador(usuario, contraseña);
            if (cabeza == null)
            {
                cabeza = nuevonodocreador;
            }
            else
            {
                nodoCreador actualnodocreador = cabeza;
                while (actualnodocreador.Siguiente != null)
                {
                    actualnodocreador = actualnodocreador.Siguiente;
                }
                actualnodocreador.Siguiente = nuevonodocreador;

            }
        }
        //metodo para editar un nodo de la lista simple de cuentas

        public void Modificar(string usuario, string nuevacontra )
        {
            nodoCreador actualnodocr = cabeza;
            while (actualnodocr != null)
            {
                if (actualnodocr.Usuario == usuario)
                {
                    actualnodocr.Contraseña = nuevacontra;
                    //   pila de editar datos       RegistrarHistorialSimple($"Modificado: {usuario} {nuevacontra} ");
                    Console.WriteLine($"\nHa sido modificado con exito", Console.ForegroundColor = ConsoleColor.Green);
                    Console.ReadLine();
                    return;
                }
                actualnodocr = actualnodocr.Siguiente;

            }
        }

        public void Eliminar(string usuario)
        {
            nodoCreador actualnodocr = cabeza.Siguiente;
            nodoCreador anteriornodocr = cabeza;
            if (cabeza == null)
            {
                Console.WriteLine("\nLa colaAlamacen esta vacia", Console.ForegroundColor = ConsoleColor.Red);
                Console.ReadLine();
                return;
            }
            if (cabeza.Usuario == usuario)
            {
                cabeza = cabeza.Siguiente;
                Console.WriteLine($"\nHa sido eliminado con exito", Console.ForegroundColor = ConsoleColor.Green);
                //RegistrarHistorialSimple($"Eliminado: {usuario}");
                Console.ReadLine();

                return;
            }
            while (actualnodocr != null)
            {
                if (actualnodocr.Usuario == usuario)
                {
                    anteriornodocr.Siguiente = actualnodocr.Siguiente;
                    Console.WriteLine($"\nHa sido eliminado con exito", Console.ForegroundColor = ConsoleColor.Green);
                    //RegistrarHistorialSimple($"Eliminado: {usuario}");
                    Console.ReadLine();

                    return;
                }
                anteriornodocr = actualnodocr;
                actualnodocr = actualnodocr.Siguiente;

            }
            // >|> Todo lo contrario porque desde antes ya se esta haciendo una evaluacion del usuario

            Console.WriteLine($"\nNo se ha encontrado en la colaAlamacen", Console.ForegroundColor = ConsoleColor.Red);
            Console.ReadLine();
        }

        public nodoCreador ObtenerUsuario(string usuario, string contraseña)
        {
            nodoCreador actualnodocr = cabeza;
            while (actualnodocr != null)
            {
                if (actualnodocr.Usuario == usuario && actualnodocr.Contraseña == contraseña)
                {
                    return actualnodocr;
                }
                actualnodocr = actualnodocr.Siguiente;
            }
            return null;
        }

        public void EliminarTodo()
        {
            if (cabeza == null)
            {
                Console.WriteLine("\nLa colaAlamacen esta vacia", Console.ForegroundColor = ConsoleColor.Red);
                Console.ReadLine();
                return;
            }
            nodoCreador actualnodocr = cabeza;
            while (actualnodocr != null)
            {
                nodoCreador siguientenodo = actualnodocr.Siguiente;

                // -<-< Comenzaremos la liberacion de datos
                actualnodocr.Siguiente = null;
                actualnodocr = siguientenodo;
            }
            cabeza = null;
            // >-> Aqui ya se habra vaciado toda la lista
            Console.Write($"Todos los usuarios han sido eliminados con exito", Console.ForegroundColor = ConsoleColor.Green);
            Console.ReadLine();
        }

        public void ImprimirCuentas()
        {
            nodoCreador actual = cabeza;

            // Encabezado del mapa
            Console.WriteLine("╔═════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 Lista de Cuentas                    ║");
            Console.WriteLine("║═════════════════════════════════════════════════════║");
            Console.WriteLine("║ Usuario             | Contraseña                    ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════╝");

            // Recorrer la lista e imprimir cada cuenta
            while (actual != null)
            {
                Console.WriteLine($"║ {actual.Usuario,-19} | {actual.Contraseña,-29} ║");
                actual = actual.Siguiente;
            }

            // Pie de página
            Console.WriteLine("╚═════════════════════════════════════════════════════╝");
        }

        public int conteoLista()
        {
            nodoCreador t = cabeza;
            int contador = 0;
            while (t != null)
            {
                contador++;
                t = t.Siguiente;
            }
            Console.WriteLine(contador + " cuentas", Console.ForegroundColor = ConsoleColor.Green);
            return contador;
        }
    }
}

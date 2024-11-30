using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.extras;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;

namespace T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble
{
    public class listaDobleAmbulancias
    {
        public nodoAmbulancias listaDblAmbulancias;
        public nodoAmbulancias ListaDblAmbulancias { get => listaDblAmbulancias; set => listaDblAmbulancias = value; }

        public listaDobleAmbulancias()
        {
            ListaDblAmbulancias = null;
        }

        public nodoAmbulancias primeraAmbulancia()
        {
            return listaDblAmbulancias;
        }

        //metodos
        public void insertarAlInicioAmbu(string marca, int prioVelocidad, string placa, string codigo, string conductor)
        {
            //Crear el nuevo nodo de la lista doble en q
            nodoAmbulancias q = new nodoAmbulancias(marca, prioVelocidad, placa,codigo, conductor);
            q.Sgte = ListaDblAmbulancias;
            //Condicional de si hay nodos en la lista doble
            if(ListaDblAmbulancias != null)
            {
                //enlace anterior de la lista apunta al nuevo nodo q va ingresar
                listaDblAmbulancias = q;
            }
            //si hay nodods en la lista 
            //hgacer el nuevo noso creado parte de la lista doble de ambulancias
            ListaDblAmbulancias = q;
        }

        public void insertarAlFinalAmbu(string marca, int prioVelocidad, string placa, string codigo, string conductor)
        {
            //definir nodos de ambulancia
            nodoAmbulancias p = ListaDblAmbulancias;
            //crear el nuevo nodo y asignarlo en la parte final de la lista
            nodoAmbulancias q = new nodoAmbulancias(marca, prioVelocidad, placa, codigo, conductor);
            //condicional de si la lista esta vacia
            if(listaDblAmbulancias != null)
            {
                listaDblAmbulancias = q;
            }
            else
            {
                while (p.Sgte != null)
                {
                    p= p.Sgte;
                }
                //Enlace siguiente de p apunta al nuevo nodo creado
                p.Sgte = q;
                //Enlace anterior del nodo creado apunta a p
                q.Ant = p;
            }
        }

        public void insertarNodoAntesOtroAmbu(string marca, int prioVelocidad, string placa, string codigo, string conductor,string datoNodoRef)
        {
            //definir nodos de ambulancia
            nodoAmbulancias p = listaDblAmbulancias;
            nodoAmbulancias Q, T, R;
            Q = p;
            while ((Q.Sgte != null) && (Q.Codigo != datoNodoRef))
            {
                //Ir al siguiente nodo de la lista
                Q = Q.Sgte;
            }
            //El nodo como referencia se encontro
            if (Q.Codigo == datoNodoRef)
            {
                T = new nodoAmbulancias(marca, prioVelocidad, placa, codigo, conductor);
                //Enlace siguiente del nuevo nodo apunta a Q
                T.Sgte = Q;
                R = Q.Ant; //R apunta al anterior de Q
                Q.Ant = T; //Anterior de Q apuna al nuevo nodo creado
                if (p == Q)
                {
                    p = T; //p apunta al nuevo nodo creado
                    T.Ant = null; //Anterior del nuevo nodo creado apunta a null
                }
                else
                {
                    //El siguiente de R apunta al nuevo nodo creado
                    R.Sgte = T;
                    //Anterior del nuevo nodo creado apunta a R
                    T.Ant = R;
                }
            }
        }

        public void ModificarAmbu(ref bool registrarMov, ref string cambio, ref string buscar1)
        {
            nodoAmbulancias p = listaDblAmbulancias;
            bool encontrado = false;
            int opc = 0;
            string buscar;
            imprimirAmbu();
            Console.WriteLine("Ingrese el Código de la Ambulancia: ");
            buscar = Console.ReadLine();
            if (listaDblAmbulancias != null)
            {
                while (p != null && encontrado != true)
                {
                    if (p.Codigo == buscar)
                    {
                        registrarMov = true;
                        Console.Clear();
                        Console.WriteLine("DATO ENCONTRADO");
                        imprimirAmbu();
                        Console.WriteLine("Seleccione el dato que desea modificar:");
                        Console.WriteLine("[1] Marca de Vehiculo");
                        Console.WriteLine("[2] Nivel de Gravedad");
                        Console.WriteLine("[3] Placa");
                        Console.WriteLine("[4] Codigo");
                        Console.WriteLine("[5] Salir");
                        opc = int.Parse(Console.ReadLine());
                        switch (opc)
                        {
                            case 1:
                                Console.WriteLine("Ingrese la marca del vehiculo: (ejm: Toyota, Mercedes, etc.)");
                                p.Marca = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío la marca del auto por: " + p.Marca;
                                break;
                            case 2:
                                int prioVelocidad = -1;
                                do
                                {
                                    Console.WriteLine("Ingrese el nuevo número para indicar el nivel de gravedad (0 o 1): ");
                                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                    elementosDecoracion.tabularMenuRojo("0", "No tan grave\n");
                                    elementosDecoracion.tabularMenuRojo("1", "Grave\n");
                                    Console.WriteLine("═══════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                                    if (int.TryParse(Console.ReadLine(), out prioVelocidad) && (prioVelocidad == 0 || prioVelocidad == 1))
                                    {
                                        Console.WriteLine($"Has ingresado el número: {prioVelocidad}");
                                        p.PrioVelocidad=prioVelocidad;
                                        buscar1 = buscar.ToString();
                                        cambio = "Se cambío el número de gravedad por: " + p.PrioVelocidad;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Entrada inválida. Intente nuevamente.");
                                    }
                                } while (prioVelocidad != 0 && prioVelocidad != 1);
                                break;
                            case 3:// placa de la ambulancia 
                                Console.WriteLine("Ingrese la nueva placa: ");
                                p.Placa=Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío la placa por: " + p.Placa;
                                break;
                            case 4:// codigo de la ambulancia
                                Console.WriteLine("Ingrese el nuevo codigo : ");
                                p.Codigo = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el código de la sede de "+buscar+" a: " + p.Codigo;
                                break;
                        }
                        encontrado = true;
                    }
                    p = p.Sgte;
                }
                if (encontrado != true)
                {
                    Console.WriteLine("LA AMBULANCIA NO FUE ENCONTRADA");
                    return;
                }

            }
            else
            {
                Console.WriteLine("La lista se encuentra vacia");
                return;
            }
        }
        public void BuscarDato(string datoNodoRef, ref bool encontrado){

            nodoAmbulancias p = listaDblAmbulancias;
            nodoAmbulancias Q;
            Q = p;
            if (p != null)
            {
                while ((Q.Sgte !=null) && (Q.Codigo != datoNodoRef)){

                    Q = p.Sgte;
                }
                if (Q.Codigo == datoNodoRef)
                {

                    Console.WriteLine("La ambulancia fue encontrada");
                    encontrado = true;
                }
                else
                    Console.WriteLine("El elemento de referencia no esta en la lista doble...!");
            }
        }
        public void imprimirAmbu()
        {
            //p apunta al inicio de la lista
            nodoAmbulancias p = listaDblAmbulancias;
            //Si la lista doble esta vacia
            if (listaDblAmbulancias == null)
            {
                Console.WriteLine("Lista Doble esta vacia...!");
                return; //Salir
            }
            //Si hay elementos en la lista doble
            //Recorrer la lista desde el nodo inicial hasta el nodo final

            // Cabecera de la tabla
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                             Lista de Vehículos                                                     ║");
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║          Marca               |   Prioridad de Velocidad  |     Placa     |      Código       |             Conductor               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

            while (p != null)
            {
                // Mostrar el dato del nodo
                Console.WriteLine("║ {0,-28} | {1,-25} | {2,-13} | {3,-17} | {4,-35} ║", p.Marca, p.PrioVelocidad, p.Placa, p.Codigo, p.Conductor);
                // Ir al siguiente nodo
                p = p.Sgte;
            }

            // Pie de página
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        public void eliminarNodoAmbuLD(string codigo, ref bool encontro, ref string codigo1)
        {
            //Definir Nodos de Trabajo
            //P y F apunten al inicio de la lista doble
            nodoAmbulancias P = listaDblAmbulancias;
            nodoAmbulancias F = listaDblAmbulancias;
            //Definir apuntadores
            nodoAmbulancias Q, T, R;
            Q = P; //Q apunta al inicio de la lista
            if (Q == null)
            {
                Console.WriteLine("Lista Doble esta Vacia...!");
                return; //Salir
            }
            //Recorrer la lista y hacer que F apunte al final de la lista
            while (F.Sgte != null)
            {
                //Ir al siguiente nodo
                F = F.Sgte;
            }
            //Recorrer la lista y buscar el nodo por su dato
            while ((Q.Sgte != null) && (Q.Codigo != codigo))
            {
                //Ir al siguiente nodo de la lista doble
                Q = Q.Sgte;
            }
            //Si el dato del nodo es igual al dato buscado (Nodo encontrado)
            if (Q.Codigo == codigo)
            {
                codigo1 = codigo;
                encontro = true;
                //Si la lista tiene un solo nodo
                if ((Q == P) && (Q == F))
                {
                    //P y F apuntan a null
                    P = null;
                    F = null;
                }
                else
                {
                    //Si es el primer nodo de la lista
                    if (Q == P)
                    {
                        P = Q.Sgte; //P apunta al siguiente de Q
                        P.Ant = null;
                    }
                    else
                    {
                        //Si es el ultimo nodo de la lista
                        if (Q == F)
                        {
                            F = Q.Ant; //F apunta al anterior de Q
                            F.Sgte = null; //Siguiente de F apunta a null  
                        }
                        //Si es un nodo intermedio a eliminar
                        else
                        {
                            T = Q.Ant; //T apunta al anterior de Q
                            R = Q.Sgte; //R apunta al siguiente de Q
                            T.Sgte = R; //Siguiente de T apunta a R
                            R.Ant = T; //Anterior de R apunta a T
                        }
                    }
                }
                //ListaDBL apunta a P
                listaDblAmbulancias = P;
            }
            else
                Console.WriteLine("El elemento no esta en la colaAlamacen doble...!");
        }


        static Random random = new Random();
        public string GenerarPlaca()
        {
            // Generar dos letras mayúsculas aleatorias
            char letra1 = (char)random.Next('A', 'Z' + 1);
            char letra2 = (char)random.Next('A', 'Z' + 1);
            char letra3 = (char)random.Next('A', 'Z' + 1);

            // Generar tres números aleatorios
            int numero1 = random.Next(10);
            int numero2 = random.Next(10);
            int numero3 = random.Next(10);

            // Formar el código con el formato deseado
            string codigo = $"{letra1}{letra2}{letra3}-{numero1}{numero2}{numero3}";

            return codigo;
        }
        public string GenerarCodigoAmbulancia()
        {
            // Generar dos letras mayúsculas aleatorias
            char letra1 = (char)random.Next('A', 'Z' + 1);
            char letra2 = (char)random.Next('A', 'Z' + 1);

            // Generar tres números aleatorios
            int numero1 = random.Next(10);
            int numero2 = random.Next(10);
            int numero3 = random.Next(10);

            // Formar el código con el formato deseado
            string codigo = $"{letra1}{letra2}-{numero1}{numero2}{numero3}";

            return codigo;
        }
        public string GenerarMarca()
        {
            string[] marcasAmbulancias = {
            "Ford", "Mercedes-Benz", "Chevrolet", "Nissan", "Toyota", "Ram",
            "Fiat", "Volkswagen" };

            return marcasAmbulancias[random.Next(marcasAmbulancias.Length)];
        }
        public int GenerarCeroUno()
        {
            int numero = random.Next(2); // Genera un número entre 0 y 1
            return numero;
        }

        public int conteoAmbulancias()
        {
            nodoAmbulancias t = listaDblAmbulancias;
            int contador = 0;
            while (t != null)
            {
                contador++;
                t = t.Sgte;
            }

            // Opcional: Imprimir el resultado final fuera del bucle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + contador + " ambulancias");
            Console.ResetColor();

            return contador;
        }
    }
}

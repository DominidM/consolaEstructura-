using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;

namespace T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble
{
    public class listaDobleSedes
    {
        //nombrar primer nodo
        public nodoSedes listaDblSedes;

        //getters and setters
        public nodoSedes ListaDblSedes { get => listaDblSedes; set => listaDblSedes = value; }
        public listaDobleSedes()
        {
            ListaDblSedes = null;
        }

        //metodos
        public void insertaAlInicioLD(string nombre, string ubicacion, int numero, string codigo)
        {
            //Crear el nuevo nodo de la lista doble en q
            nodoSedes q = new nodoSedes(nombre, ubicacion, numero, codigo);
            //Enlace siguiente del nuevo nodo apunta a la lista
            q.Sgte = ListaDblSedes;
            //Si hay nodos en la lista doble
            if (ListaDblSedes != null)
            {
                //Enlace anterior de la lista apunta al nueno nodo creado
                ListaDblSedes.Ant = q;
            }
            //Si no hay nodos en lista
            //Hacer el nuevo nodo creado parte de la lista doble
            ListaDblSedes = q;
        }
        public void insertaAlFinalLD(string nombre, string ubicacion, int numero, string codigo)
        {
            //Definir nodos de trabajo
            //p apunta al inicio d ela lista doble
            nodoSedes p = listaDblSedes;
            //Crear el nuevo nodo y asignarle el dato en q
            nodoSedes q = new nodoSedes(nombre, ubicacion, numero, codigo);
            //si la lista esta vacia
            if (listaDblSedes == null)
                //Lista apunta al nuevo nodo creado
                listaDblSedes = q;
            else
            {
                //Recorrer la lista para ir al final
                while (p.Sgte != null)
                {
                    //Ir al siguiente Nodo
                    p = p.Sgte;
                }
                //Enlace siguiente de p apunta al nuevo nodo creado
                p.Sgte = q;
                //Enlace anterior del nodo creado apunta a p
                q.Ant = p;
            }
        }
        public void imprimirLD()
        {
            //p apunta al inicio de la lista
            nodoSedes p = listaDblSedes;
            //Si la lista doble esta vacia
            if (ListaDblSedes == null)
            {
                Console.WriteLine("Lista Doble esta vacia...!");
                return; //Salir
            }
            //Si hay elementos en la lista doble
            //Recorrer la lista desde el nodo inicial hasta el nodo final
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                             Lista de Sedes                                         ║"); 
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║          Nombre de la Sede          |                    Dirección                  |  Teléfono  |     Código      ║");
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            while (p != null)
            {
                //Mostrar el dato del nodo
                Console.WriteLine("║ {0,-35} | {1,-45} | {2,-10} | {3,-15} ║", p.Nombre_sede, p.Ubicacion, p.Numero_telefono, p.Codigo);
                //Ir al siguiente nodo
                p = p.Sgte;
            }
            //Pie de página
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
        public void BuscarDato(string datoNodoRef, ref bool encontrado)
        {
            nodoSedes p = listaDblSedes;
            nodoSedes Q;
            Q = p;
            if (p != null)
            {
                while ((Q.Sgte != null) && (Q.Codigo != datoNodoRef))
                {
                    //Ir al siguiente nodo de la lista
                    Q = Q.Sgte;
                }
                //El nodo como referencia se encontro
                if (Q.Codigo == datoNodoRef)
                {
                    Console.WriteLine("La sede fue encontrada");
                    encontrado = true;
                }
                else
                    Console.WriteLine("El elemento como referencia no esta en la lista doble...!");
            }
        }
        public string ExtraerNombre(string datoNodoRef, ref bool encontrado)
        {
            nodoSedes p = listaDblSedes;
            nodoSedes Q;
            Q = p;
            string nombre = "";
            if (p != null)
            {
                while ((Q.Sgte != null) && (Q.Codigo != datoNodoRef))
                {
                    //Ir al siguiente nodo de la lista
                    Q = Q.Sgte;
                }
                //El nodo como referencia se encontro
                if (Q.Codigo == datoNodoRef)
                {
                    encontrado = true;
                    nombre = Q.Nombre_sede;
                }
            }
            return nombre;
        }
        public void insertarNodoAntesOtro(string nombre, string ubicacion, int numero, string codigo, string datoNodoRef)
        {
            //Definir nodos de trabajo
            //p apunta al inicio de la lista doble
            nodoSedes p = listaDblSedes;
            nodoSedes Q, T, R;
            Q = p; //Hacer que Q apunte a p
            //Recorrer la lista mientras el enlace siguiente de la lista sea diferente de null
            //y el dato del nodo sea diferente al dato del nodo referencial
            while ((Q.Sgte != null) && (Q.Codigo != datoNodoRef))
            {
                //Ir al siguiente nodo de la lista
                Q = Q.Sgte;
            }
            //El nodo como referencia se encontro
            if (Q.Codigo == datoNodoRef)
            {
                //Crear el nuevo nodo de la lista en T y asignarle el dato
                T = new nodoSedes(nombre, ubicacion, numero, codigo);
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
        public void Modificar(ref bool registrarMov, ref string cambio, ref string buscar1)
        {
            nodoSedes p = listaDblSedes;
            bool encontrado = false;
            int opc = 0;
            string buscar;
            imprimirLD();
            Console.WriteLine("Ingrese el Código de la Sede: ");
            buscar=Console.ReadLine();
            if (listaDblSedes != null)
            {
                while (p != null && encontrado != true)
                {
                    if (p.Codigo == buscar)
                    {
                        registrarMov = true;
                        int int_aux = 0;
                        Console.Clear();
                        Console.WriteLine("DATO ENCONTRADO");
                        imprimirLD();
                        Console.WriteLine("Seleccione el dato que desea modificar:");
                        Console.WriteLine("[1] Nombre");
                        Console.WriteLine("[2] Direccion");
                        Console.WriteLine("[3] Número de Teléfono");
                        Console.WriteLine("[4] Cargo");
                        Console.WriteLine("[5] Salir");
                        opc = int.Parse(Console.ReadLine());
                        switch (opc)
                        {
                            case 1://nombre
                                Console.WriteLine("Ingrese el nuevo nombre: ");
                                p.Nombre_sede = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el nombre de la sede por: " + p.Nombre_sede;
                                break;
                            case 2://edad
                                Console.WriteLine("Ingrese la nueva Dirección: ");
                                p.Ubicacion = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío la direccion de la sede por: " + p.Ubicacion;
                                break;
                            case 3://Número de DNI
                                do
                                    Console.WriteLine("Ingrese el nuevo número de telefono: ");
                                while (!int.TryParse(Console.ReadLine(), out int_aux));
                                p.Numero_telefono = int_aux;
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el número de telefono por: " + p.Numero_telefono;
                                break;
                            case 4://Seguro
                                Console.WriteLine("Ingrese el nuevo código: ");
                                p.Codigo = Console.ReadLine();
                                buscar1 = p.Nombre_sede;
                                cambio = "Se cambío el código de la sede a: " + p.Codigo;
                                break;
                        }
                        encontrado = true;
                    }
                    p = p.Sgte;
                }
                if (encontrado != true)
                {
                    Console.WriteLine("LA SEDE NO FUE ENCONTRADA");
                    return;
                }

            }
            else
            {
                Console.WriteLine("La lista se encuentra vacia");
                return;
            }
        }
        public void eliminarNodoLD(string codigo, ref bool encontro, ref string codigo1)
        {
            //Definir Nodos de Trabajo
            //P y F apunten al inicio de la lista doble
            nodoSedes P = listaDblSedes;
            nodoSedes F = listaDblSedes;
            //Definir apuntadores
            nodoSedes Q, T, R;
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
                codigo1 = codigo.ToString();
                encontro = true;
                //Revisar code de listaDblTrabajadores para eliminar tmbn asignacion de sede
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
                listaDblSedes = P;
            }
            else
                Console.WriteLine("El elemento no esta en la lista doble...!");
        }

        static Random random = new Random();
        public string GenerarCodigo()
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
        public string GenerarDireccion()
        {
            string[] nombresAvenidas =
            {
            "Av. Javier Prado",
            "Av. Arequipa",
            "Av. La Marina",
            "Av. Universitaria",
            "Av. El Sol",
            "Av. Larco",
            "Av. Angamos",
            "Av. Benavides",
            "Av. Brasil",
            "Av. San Borja Norte"
            };
            string nombreAvenida = nombresAvenidas[random.Next(nombresAvenidas.Length)];
            int numero = random.Next(100, 10000); 
            return $"{nombreAvenida} {numero}";
        }
        public string GenerarNombreHospital()
        {
            string[] tiposDeServicio =
            {
            "Hospital", "Clínica", "Centro Médico"
            };

            string[] nombresHospitales =
            {
            "San Juan de Dios", "Nuestra Señora de la Paz", "Central", "Santa María", "del Sur",
            "Universitario", "Internacional", "Los Andes", "Regional", "La Luz"
            };

            string tipo = tiposDeServicio[random.Next(tiposDeServicio.Length)];
            string nombreBase = nombresHospitales[random.Next(nombresHospitales.Length)];
            return $"{tipo} {nombreBase}";
        }
        public int GenerarNumeroTelefono()
        {
            char primerDigito = '9';
            string numeroTelefono = primerDigito.ToString();

            for (int i = 1; i < 9; i++)
            {
                numeroTelefono += random.Next(0, 10).ToString();
            }
            
            return int.Parse(numeroTelefono);
        }
        public int conteoSedes()
        {
            nodoSedes t = listaDblSedes;
            int contador = 0;
            while (t != null)
            {
                contador++;
                t = t.Sgte;
            }

            // Opcional: Imprimir el resultado final fuera del bucle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n"+contador + " sedes");
            Console.ResetColor();

            return contador;
        }
    }
}

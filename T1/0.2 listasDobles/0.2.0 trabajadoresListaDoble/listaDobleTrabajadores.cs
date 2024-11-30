using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista
{
    public class listaDobleTrabajadores
    {
        public Nodo_Trabajadores listaDblTrabajadores;
        public Nodo_Trabajadores ListaDblTrabajadores
        {
            get { return listaDblTrabajadores; } //Leer
            set { listaDblTrabajadores = value; } //Escribir
        }
        public listaDobleTrabajadores()
        {
            ListaDblTrabajadores = null;
        }
        public void insertaAlInicioLD(string nombre, int edad, int dni, string genero, string cargo, bool asignado)
        {
            //Crear el nuevo nodo de la lista doble en q
            Nodo_Trabajadores q = new Nodo_Trabajadores(nombre, edad , dni, genero, cargo, asignado);
            //Enlace siguiente del nuevo nodo apunta a la lista
            q.Sgte = ListaDblTrabajadores;
            //Si hay nodos en la lista doble
            if (ListaDblTrabajadores != null)
            {
                //Enlace anterior de la lista apunta al nueno nodo creado
                ListaDblTrabajadores.Ant = q;
            }
            //Si no hay nodos en lista
            //Hacer el nuevo nodo creado parte de la lista doble
            ListaDblTrabajadores = q;
        }
        public void insertaAlFinalLD(string nombre, int edad, int dni, string genero, string cargo, bool asignado)
        {
            //Definir nodos de trabajo
            //p apunta al inicio d ela lista doble
            Nodo_Trabajadores p = listaDblTrabajadores;
            //Crear el nuevo nodo y asignarle el dato en q
            Nodo_Trabajadores q = new Nodo_Trabajadores(nombre, edad, dni, genero, cargo, asignado);
            //si la lista esta vacia
            if (listaDblTrabajadores == null)
                //Lista apunta al nuevo nodo creado
                listaDblTrabajadores = q;
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
        public void imprimirMedicos()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                  Lista de Médicos                             ║");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║           Nombre del Médico         | Edad |   Numero de DNI   |    Género    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════╝");

            Nodo_Trabajadores p = ListaDblTrabajadores;
            while (p != null)
            {
                if (p.Cargo_e == "medico")
                {
                    Console.WriteLine($"║ {p.Nombre_e,-35} | {p.Edad_e,-4} | {p.Nro_dni_e,-17} | {p.Genero_e,-12} ║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════╝");

        }
        public void imprimirAdmin()
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                Lista de Supervisores                            ║");
            Console.WriteLine("║═════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║        Nombre del Supervisor        | Edad |    Numero de DNI    |    Género    ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");

            Nodo_Trabajadores p = ListaDblTrabajadores;
            while (p != null)
            {
                if (p.Cargo_e == "supervisor")
                {
                    Console.WriteLine($"║ {p.Nombre_e,-35} | {p.Edad_e,-4} | {p.Nro_dni_e,-20} | {p.Genero_e,-11} ║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
        }
        public void imprimirLimpieza()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          Lista de personal de limpieza                         ║");
            Console.WriteLine("║════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║   Nombre del personal de limpieza   | Edad |   Numero de DNI   |    Género     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");

            Nodo_Trabajadores p = ListaDblTrabajadores;
            while (p != null)
            {
                if (p.Cargo_e == "limpieza")
                {
                    Console.WriteLine($"║ {p.Nombre_e,-35} | {p.Edad_e,-4} | {p.Nro_dni_e,-17} | {p.Genero_e,-13} ║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
        }
        public void imprimirConductor()
        {

            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          Lista de personal de Conductor                         ║");
            Console.WriteLine("║═════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║  Nombre del personal de seguridad   | Edad |    Numero de DNI    |    Género    ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");

            Nodo_Trabajadores p = ListaDblTrabajadores;
            while (p != null)
            {
                if (p.Cargo_e == "conductor")
                {
                    Console.WriteLine($"║ {p.Nombre_e,-35} | {p.Edad_e,-4} | {p.Nro_dni_e,-19} | {p.Genero_e,-12} ║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
        }
        public void imprimirLD()
        {
            //p apunta al inicio de la lista
            Nodo_Trabajadores p = ListaDblTrabajadores;
            //Si la lista doble esta vacia
            if (ListaDblTrabajadores == null)
            {
                Console.WriteLine("Lista Doble esta vacia...!");
                return; //Salir
            }
            //Si hay elementos en la lista doble
            //Recorrer la lista desde el nodo inicial hasta el nodo final
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                           Lista de Trabajadores                                         ║");
            Console.WriteLine("║═════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║        Nombre del Trabajador        | Edad | Numero de DNI |    Género    |           Cargo             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            while (p != null)
            {
                //Mostrar el dato del nodo
                Console.WriteLine("║ {0,-35} | {1,-4} | {2,-13} | {3,-12} | {4,-27} ║", p.Nombre_e, p.Edad_e, p.Nro_dni_e, p.Genero_e, p.Cargo_e);
                //Ir al siguiente nodo
                p = p.Sgte;
            }
            //Pie de página
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
        public void BuscarDato(int datoNodoRef, ref bool encontrado)
        {
             Nodo_Trabajadores p = ListaDblTrabajadores;
             Nodo_Trabajadores Q;
             Q = p;
             while ((Q.Sgte != null) && (Q.Nro_dni_e != datoNodoRef))
             {
                 //Ir al siguiente nodo de la lista
                 Q = Q.Sgte;
             }
             //El nodo como referencia se encontro
             if (Q.Nro_dni_e == datoNodoRef)
             {
                 Console.WriteLine("El trabajador fue encontrado");
                 encontrado=true;
             }
             else
                 Console.WriteLine("El elemento como referencia no esta en la colaAlamacen doble...!");
        }
        public void insertarNodoAntesOtro(string nombre, int edad, int dni, string genero, string cargo, bool asignado,int datoNodoRef)
        {
            //Definir nodos de trabajo
            //p apunta al inicio de la lista doble
            Nodo_Trabajadores p = ListaDblTrabajadores;
            Nodo_Trabajadores Q, T, R;
            Q = p; //Hacer que Q apunte a p
            //Recorrer la lista mientras el enlace siguiente de la lista sea diferente de null
            //y el dato del nodo sea diferente al dato del nodo referencial
            while ((Q.Sgte != null) && (Q.Nro_dni_e != datoNodoRef))
            {
                //Ir al siguiente nodo de la lista
                Q = Q.Sgte;
            }
            //El nodo como referencia se encontro
            if (Q.Nro_dni_e == datoNodoRef)
            {
                //Crear el nuevo nodo de la lista en T y asignarle el dato
                T = new Nodo_Trabajadores(nombre, edad, dni, genero, cargo, asignado);
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
            Nodo_Trabajadores p = listaDblTrabajadores;
            bool encontrado = false;
            int opc = 0;
            int buscar;
            imprimirLD();
            do
                Console.WriteLine("Ingrese el Nro. de DNI del trabajador a modificar: ");
            while (!int.TryParse(Console.ReadLine(), out buscar));
            if (listaDblTrabajadores != null)
            {
                while (p != null && encontrado != true)
                {
                    if (p.Nro_dni_e == buscar)
                    {
                        registrarMov = true;
                            int int_aux = 0;
                            Console.Clear();
                            Console.WriteLine("DATO ENCONTRADO");
                            imprimirLD();
                            Console.WriteLine("Seleccione el dato que desea modificar:");
                            Console.WriteLine("[1] Nombre");
                            Console.WriteLine("[2] Edad");
                            Console.WriteLine("[3] Número de DNI");
                            Console.WriteLine("[4] Género");
                            Console.WriteLine("[5] Cargo");
                            Console.WriteLine("[6] Salir");
                            opc = int.Parse(Console.ReadLine());
                            switch (opc)
                            {
                                case 1://nombre
                                    Console.WriteLine("Ingrese el nuevo nombre: ");
                                    p.Nombre_e = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el nombre del trabajador por: " + p.Nombre_e;
                                break;
                                case 2://edad
                                    do                                   
                                    Console.WriteLine("Ingrese la nueva edad: ");
                                    while(!int.TryParse(Console.ReadLine(), out int_aux));
                                    p.Edad_e = int_aux;
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío la edad del trabajador por: " + p.Edad_e;
                                break;
                                case 3://Número de DNI
                                    do
                                    Console.WriteLine("Ingrese el nuevo número de DNI: ");
                                    while(!int.TryParse(Console.ReadLine(),out int_aux));
                                    p.Nro_dni_e = int_aux;
                                buscar1 = p.Nombre_e;
                                cambio = "Se cambío el DNI del trabajador por: " + p.Nro_dni_e;
                                break;
                                case 4://Seguro
                                    Console.WriteLine("Ingrese el nuevamente el género: ");
                                    p.Genero_e = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el género del trabajador a: " + p.Genero_e;
                                break;
                                case 5://Malestares
                                    Console.WriteLine("Ingrese nuevamente el cargo del trabajador: ");
                                    p.Cargo_e = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el cargo del paciente a: " + p.Genero_e;
                                break;
                            }
                        encontrado = true;
                    }
                    p = p.Sgte;
                }
                if(encontrado!=true)
                {
                    Console.WriteLine("EL TRABAJADOR NO FUE ENCONTRADO");
                    return;
                }

            }
            else
            {
                Console.WriteLine("La colaAlamacen se encuentra vacia");
                return;
            }
        }
        public void eliminarNodoLD(int dni,listaSimplePaciente listasimple,ref bool encontro, ref string dni1)
        {
            //Definir Nodos de Trabajo
            //P y F apunten al inicio de la lista doble
            Nodo_Paciente p = listasimple.lista;
            Nodo_Trabajadores P = listaDblTrabajadores;
            Nodo_Trabajadores F = listaDblTrabajadores;
            //Definir apuntadores
            Nodo_Trabajadores Q, T, R;
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
            while ((Q.Sgte != null) && (Q.Nro_dni_e != dni))
            {
                //Ir al siguiente nodo de la lista doble
                Q = Q.Sgte;
            }
            //Si el dato del nodo es igual al dato buscado (Nodo encontrado)
            if (Q.Nro_dni_e == dni)
            {
                dni1 = dni.ToString();
                encontro=true;
                while (p != null)
                {
                    if (p.Doctor_asignado=="Dr. "+Q.Nombre_e||p.Doctor_asignado=="Dra. "+Q.Nombre_e)
                    {
                        p.Doctor_asignado = "";
                    }
                    p = p.Sgte;
                }
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
                listaDblTrabajadores = P;
            }
            else
                Console.WriteLine("El elemento no esta en la colaAlamacen doble...!");
        }
        //Metodo para generar nombre de hombres
        static Random random = new Random();
        public string GenerarNombreHombre()
        {
            string[] nombresHombres = {
    "Albert", "Ricardo", "Santiago", "Hugo", "Emilio",
    "Joaquín", "Federico", "Ignacio", "Óscar", "Nicolás",
    "Álvaro", "Fabián", "Eduardo", "Enrique", "Roberto",
    "Omar", "Marcos", "Alonso", "Dante", "Esteban"
};
            string[] apellidos = {
    "Castro", "Fernández", "Vidal", "Giménez", "Benítez",
    "Suárez", "Rivas", "Ortega", "Aguilar", "Santos",
    "Blanco", "Castillo", "Molina", "Navarro", "Silva",
    "Ramos", "Cruz", "Gallego", "Iglesias", "León"
};

            string nombre = nombresHombres[random.Next(nombresHombres.Length)];
            string apellido1 = apellidos[random.Next(apellidos.Length)];
            string apellido2;

            do
            {
                apellido2 = apellidos[random.Next(apellidos.Length)];
            } while (apellido2 == apellido1);

            return $"{nombre} {apellido1} {apellido2}";
        }
        //Metodo para generar nombre de mujeres
        public string GenerarNombreMujer()
        {
            string[] nombresMujeres = {"Laura", "Valentina", "Camila", "Alejandra", "Lorena",
    "Sandra", "Natalia", "Vanessa", "Isabella", "Carla",
    "Verónica", "Melissa", "Adriana", "Mónica", "Gabriela",
    "Cecilia", "Marina", "Diana", "Karina", "Patricia"};
            string[] apellidos = {"Pérez", "Fernández", "Vidal", "Giménez", "Benítez",
    "Suárez", "Rivas", "Ortega", "Aguilar", "Santos",
    "Blanco", "Castillo", "Molina", "Navarro", "Silva",
    "Ramos", "Cruz", "Gallego", "Iglesias", "León" };

            string nombre = nombresMujeres[random.Next(nombresMujeres.Length)];
            string apellido1 = apellidos[random.Next(apellidos.Length)];
            string apellido2;

            do
            {
                apellido2 = apellidos[random.Next(apellidos.Length)];
            } while (apellido2 == apellido1);

            return $"{nombre} {apellido1} {apellido2}";
        }
        //Metodo para generar un dato en la variable "Malestar del paciente"
        public string GenerarCargo()
        {
            string[] cargo = {"supervisor","medico","conductor","limpieza"};

            return cargo[random.Next(cargo.Length)];
        }
        //Generador de Dni
        public int GenerarDNI()
        {
            //Generar el primer dígito del DNI en el rango de 1 a 9
            int primerDigito = random.Next(1, 10);

            int dni = primerDigito;

            //Generar los siguientes 7 dígitos aleatoriamente
            for (int i = 0; i < 7; i++)
            {
                dni = dni * 10 + random.Next(10); //Números del 0 al 9
            }

            return dni;
        }
        //generar edad
        public int GenerarEdad()
        {
            return random.Next(25, 71); //Genera un número aleatorio entre 1 y 100 (incluyendo ambos extremos)
        }

        public int conteoTrabajador()
        {
            Nodo_Trabajadores t = listaDblTrabajadores;
            int contador = 0;
            while (t != null)
            {
                contador++;
                t = t.Sgte;
            }

            // Opcional: Imprimir el resultado final fuera del bucle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(contador + " trabajadores");
            Console.ResetColor();

            return contador;
        }

        public int conteoMedicos()
        {
            Nodo_Trabajadores t = listaDblTrabajadores;
            int contador = 0;
            while (t != null)
            {
                if (t.Cargo_e == "medico")
                {
                    contador++;
                }
                t = t.Sgte;

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(contador + " medicos");
            Console.ResetColor();

            return contador;
        }

        public int conteoConductor()
        {
            Nodo_Trabajadores t = listaDblTrabajadores;
            int contador = 0;
            while (t != null)
            {
                if (t.Cargo_e == "conductor")
                {
                    contador++;
                }
                t = t.Sgte;

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(contador + " conductor");
            Console.ResetColor();

            return contador;
        }

        public int conteoSupervisor()
        {
            Nodo_Trabajadores t = listaDblTrabajadores;
            int contador = 0;
            while (t != null)
            {
                if (t.Cargo_e == "supervisor")
                {
                    contador++;
                }
                t = t.Sgte;

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(contador + " supervisor");
            Console.ResetColor();

            return contador;
        }

        public int conteoLimpieza()
        {
            Nodo_Trabajadores t = listaDblTrabajadores;
            int contador = 0;
            while (t != null)
            {
                if (t.Cargo_e == "limpieza")
                {
                    contador++;
                }
                t = t.Sgte;

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(contador + " limpieza");
            Console.ResetColor();

            return contador;
        }
    }
}


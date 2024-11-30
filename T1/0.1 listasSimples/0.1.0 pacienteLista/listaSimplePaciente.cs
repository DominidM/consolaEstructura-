using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T2._1._2_pilas;

namespace T1_Gestor_Medico_de_Referencias
{
    public class listaSimplePaciente
    {
        public Nodo_Paciente lista;
        public listaSimplePaciente()
        {
            lista = null;
        }
        public Nodo_Paciente GetFirstPaciente()
        {
            return lista;

        }

        //Metodo para ingresar pacientes
        public void Registrar_Paciente(string Gravedad_paciente ,string Nombre_Paciente, int Edad_Paciente, int Nro_Dni_Paciente, string Seguro_Med, string Malestares_Paciente, string Genero_Paciente)
        {
                //Declaracion del nodo
                Nodo_Paciente q = new Nodo_Paciente();
            q.Nombre_paciente = Nombre_Paciente;
            q.Edad_paciente = Edad_Paciente;
            q.Nro_dni_paciente = Nro_Dni_Paciente;
            q.Seguro_med = Seguro_Med;
            q.Malestares_paciente = Malestares_Paciente;
            q.Genero_paciente = Genero_Paciente;
            q.Gravedad_paciente = Gravedad_paciente;
            //Si la lista esta vacia registra los datos como primer registro
            if (lista == null)
            {
                lista = q;
            }
            //Si no, registra los datos al inicio
            else
            {
                q.Sgte = lista;
                lista = q;
            }
        }
       
        //Metodo para mostrar la tabla de pacientes
        public void imprimir()
        {
            Console.Clear();
            Nodo_Paciente l = lista;
            //Mostrar los datos de la lista
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                     Lista de Pacientes                                               ║");
            Console.WriteLine("║══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║ Grave |      Nombre del Paciente      | Edad | Numero de DNI | Seguro Médico |      Malestares        |    Genero    ║");
            Console.WriteLine("║══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            //Recorrer la lista
            while (l != null)
            {
                Console.WriteLine($"║  {l.Gravedad_paciente,-4 } | {l.Nombre_paciente,-29} | {l.Edad_paciente,-4} | {l.Nro_dni_paciente,-13} | {l.Seguro_med,-13} | {l.Malestares_paciente,-22} | {l.Genero_paciente,-12} ║ ");
                l = l.Sgte;
            }
            //Pie de página
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
        
        public void imprimirSIS()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                      Lista de SIS                                                 ║");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║    Nombre del paciente     | Edad |    Numero de DNI    |   Género   |       Malestares       |   Doc. Asignado   ║ ");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Nodo_Paciente p = lista;
            while (p != null)
            {
                if (p.Seguro_med == "SIS")
                {
                    Console.WriteLine($"║ {p.Nombre_paciente,-25} | {p.Edad_paciente,-4} | {p.Nro_dni_paciente,-19} | {p.Genero_paciente,-9}  | {p.Malestares_paciente,-20}   | {p.Doctor_asignado,-18}║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }

        public void imprimirEsSalud()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                   Lista de EsSalud                                                ║");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║    Nombre del paciente     | Edad |    Numero de DNI    |   Género   |       Malestares       |   Doc. Asignado   ║ ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Nodo_Paciente p = lista;
            while (p != null)
            {
                if (p.Seguro_med == "EsSalud")
                {
                    Console.WriteLine($"║ {p.Nombre_paciente,-25} | {p.Edad_paciente,-4} | {p.Nro_dni_paciente,-19} | {p.Genero_paciente,-9}  | {p.Malestares_paciente,-20}   | {p.Doctor_asignado,-22}║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }

        public void imprimirPrivado()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                   Lista de Privado                                                ║");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║    Nombre del paciente     | Edad |    Numero de DNI    |   Género   |       Malestares       |   Doc. Asignado   ║ ");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Nodo_Paciente p = lista;
            while (p != null)
            {
                if (p.Seguro_med == "Privado")
                {
                    Console.WriteLine($"║ {p.Nombre_paciente,-25}  |  {p.Edad_paciente,-2}  |  {p.Nro_dni_paciente,-16}  |  {p.Genero_paciente,-9}   |  {p.Malestares_paciente,-18  }   | {p.Doctor_asignado,-18}║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }

        public void imprimirGrave()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                             Lista de pacientes graves                                             ║");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║    Nombre del paciente     | Edad |    Numero de DNI    |   Género   |       Malestares       |   Doc. Asignado   ║ ");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Nodo_Paciente p = lista;
            while (p != null)
            {
                if (p.Gravedad_paciente == "si")
                {
                    Console.WriteLine($"║ {p.Nombre_paciente,-25}  |  {p.Edad_paciente,-2}  |  {p.Nro_dni_paciente,-16}  |  {p.Genero_paciente,-9}   |  {p.Malestares_paciente,-18}   | {p.Doctor_asignado,-18}║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }


        public void imprimirNoGrave()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                          Lista de pacientes no graves                                             ║");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Console.WriteLine("║    Nombre del paciente     | Edad |    Numero de DNI    |   Género   |       Malestares       |   Doc. Asignado   ║ ");
            Console.WriteLine("║═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════║");
            Nodo_Paciente p = lista;
            while (p != null)
            {
                if (p.Gravedad_paciente == "no")
                {
                    Console.WriteLine($"║ {p.Nombre_paciente,-25}  |  {p.Edad_paciente,-2}  |  {p.Nro_dni_paciente,-16}  |  {p.Genero_paciente,-9}   |  {p.Malestares_paciente,-18}   | {p.Doctor_asignado,-18}║");
                }
                p = p.Sgte;
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }


        //Buscar dato en la lista
        public void RegistrarFinal(string Gravedad_Paciente,string Nombre_Paciente, int Edad_Paciente, int Nro_Dni_Paciente, string Seguro_Med, string Malestares_Paciente, string Genero_Paciente)
        {
            Nodo_Paciente q = new Nodo_Paciente();
            q.Gravedad_paciente = Gravedad_Paciente;
            q.Nombre_paciente = Nombre_Paciente;
            q.Edad_paciente = Edad_Paciente;
            q.Nro_dni_paciente = Nro_Dni_Paciente;
            q.Seguro_med = Seguro_Med;
            q.Malestares_paciente = Malestares_Paciente;
            q.Genero_paciente = Genero_Paciente;
            Nodo_Paciente t = lista;
            if (lista == null)
            {
                lista = q;
            }
            else
            {
                while (t.Sgte != null)
                {
                    t = t.Sgte;
                }
                t.Sgte = q;
            }
            
        }
        public void BuscarDato(int pos, ref bool encontrado)
        {
            Nodo_Paciente t = lista; 
            for (int i = 0; t != null; i++)
            {
                if (i == pos-1)
                {
                    //posicion encontrada
                    encontrado = true;
                    return;//Salir
                }
                //Ir al siguiente nodo de la lista
                t = t.Sgte;
            }
            
        }
        //Metodo para Agregar segun Posicion Seleccionada al listado
        public void AgregarPosicion(string gravedad ,string nombre, int edad, int dni, string seguro, string malestares, string Genero_Paciente, int pos)
        {
            Nodo_Paciente q, t;
            q=new Nodo_Paciente();
            q.Gravedad_paciente = gravedad;
            q.Nombre_paciente = nombre;
            q.Edad_paciente= edad;
            q.Nro_dni_paciente=dni;
            q.Seguro_med=seguro;
            q.Malestares_paciente= malestares;
            q.Genero_paciente = Genero_Paciente;
            t = new Nodo_Paciente();
            if (pos == 1)
            {
                q.Sgte = lista;//Enlace siguiente del nuevo nodo apunta segun lista
                lista = q;//Hacer q el nuevo inicio de la lista
            }
            else
            {
                //Posicion no es al inicio de la lista
                t = lista;//t apunta segun lista
                //Recorrer la lista para encontrar la ubicacion
                for (int i = 0; t != null; i++)
                {
                    if (i == pos-1)
                    {
                        //posicion encontrada
                        //Hacemos que el enlace del nuevo nodo creado a insertar
                        //apunte segun el enlace siguente de t usado para recorrer 
                        q.Sgte = t.Sgte;
                        t.Sgte = q;//Enlace Sgte de t apunta al nodo creado
                        return;//Salir
                    }
                    //Ir al siguiente nodo de la lista
                    t = t.Sgte;
                }
            }
            //Error en la posicion
            if (t == null)
            {
                Console.WriteLine("ERROR   Posición Erronea");
            }
        
        }

        //Metodo para contar la cantidad de nodos en la lista
        public int tamanoLista()
        {
            Nodo_Paciente t = lista;
            int contador = 0;
            while (t != null)
            {
                contador++;
                t = t.Sgte;
            }
            Console.WriteLine(contador+" pacientes",Console.ForegroundColor=ConsoleColor.Green);
            return contador;
        }

         public int conteoPacienteGrave()
        {
            Nodo_Paciente t = lista;
            int contador = 0;
            while (t != null)
            {
                if (t.Gravedad_paciente == "si")
            {
             contador++;
            }
            t = t.Sgte;
            }

            Console.Write(contador + " grave", Console.ForegroundColor = ConsoleColor.Green);
            Console.ResetColor();

        return contador;
        }


        public int conteoPacienteNoGrave()
        {
            Nodo_Paciente t = lista;
            int contador = 0;
            while (t != null)
            {
                if (t.Gravedad_paciente == "no")
                {
                    contador++;
                }
                t = t.Sgte;
            }
        
            Console.Write(contador + " no grave", Console.ForegroundColor = ConsoleColor.Green);
            Console.ResetColor();
        
            return contador;
        }
               
        public void Modificar(ref bool registrarMov, ref string cambio, ref string buscar1)
        {
            Nodo_Paciente p = lista;
            bool encontrado= false;
            int opc = 0;
            int buscar;
            imprimir();
            do
                Console.WriteLine("Ingrese el Nro. de DNI del Paciente a modificar: ");
            while (!int.TryParse(Console.ReadLine(), out buscar));
            if (lista != null)
            {   
                while (p != null && encontrado != true)
                {
                    if (p.Nro_dni_paciente==buscar) 
                    {
                        registrarMov = true;
                        
                        
                            int int_aux = 0;
                            Console.Clear();
                            Console.WriteLine("DATO ENCONTRADO");
                            imprimir();
                            Console.WriteLine("Seleccione el dato que desea modificar:");
                            Console.WriteLine("[1] Nombre");
                            Console.WriteLine("[2] Edad");
                            Console.WriteLine("[3] Número de DNI");
                            Console.WriteLine("[4] Seguro Médico");
                            Console.WriteLine("[5] Malestares");
                            Console.WriteLine("[6] Género");
                            Console.WriteLine("[7] Gravedad");
                            Console.WriteLine("[8] Salir");
                            opc= int.Parse(Console.ReadLine());
                            switch (opc)
                            {
                                case 1://nombre
                                    Console.WriteLine("Ingrese el nuevo nombre: ");
                                    p.Nombre_paciente=Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el nombre del paciente por: "+p.Nombre_paciente;
                                    break;
                                case 2://edad
                                    do
                                    {
                                        Console.Write("Ingrese la nueva edad: ");
                                        if (!int.TryParse(Console.ReadLine(), out int_aux) || int_aux <= 0)
                                        {
                                            // Mostrar un mensaje de error si la entrada no es un número entero válido
                                            Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                        }
                                    
                                    } while (int_aux <= 0);
                                    p.Edad_paciente = int_aux;
                                    buscar1 = buscar.ToString();
                                    cambio = "Se cambío la edad del paciente por: "+p.Edad_paciente;
                                break;
                                case 3://Número de DNI
                                    do 
                                    { 
                                       Console.WriteLine("Ingrese el nuevo número de DNI: ");
                                       if (!int.TryParse(Console.ReadLine(), out int_aux) || int_aux <= 0)
                                       {
                                           // Mostrar un mensaje de error si la entrada no es un número entero válido
                                           Console.WriteLine("Error: ¡Debes ingresar un número valido!");
                                       }
                                    }
                                    while (int_aux <= 0) ;
                                    p.Nro_dni_paciente = int_aux;
                                buscar1 = p.Nombre_paciente;
                                cambio = "Se cambío el número de DNI del paciente por: "+p.Nro_dni_paciente;
                                break;
                                case 4://Seguro
                                    Console.WriteLine("Ingrese el nuevo seguro médico: ");
                                    p.Seguro_med=Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el seguro médico del paciente por: "+p.Seguro_med;
                                break;
                                case 5://Malestares
                                    Console.WriteLine("Ingrese nuevamente los Malestares del paciente: ");
                                    p.Malestares_paciente = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambíaron los malestares del paciente por: "+p.Malestares_paciente;
                                break;
                                case 6://Genero
                                    Console.WriteLine("Ingrese nuevamente el género del paciente: ");
                                p.Genero_paciente=Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el género del paciente a: " +p.Genero_paciente;
                                break;
                                case 7: //gravedad
                                Console.WriteLine("Ingrese nuevamente la gravedad del paciente: ");
                                p.Gravedad_paciente = Console.ReadLine();
                                buscar1 = buscar.ToString();
                                cambio = "Se cambío el género del paciente a: " + p.Gravedad_paciente;
                                break;
                            }
                        encontrado = true;
                    }
                    p = p.Sgte;
                }
                if(!encontrado)
                {
                    Console.WriteLine("EL DATO NO FUE ENCONTRADO");
                }
            }
            else
            {
                Console.WriteLine("La lista se encuentra vacia");
            }
        }
        //Metodo para Eliminar un Nodo_cliente de la lista 
        public void eliminaElementoValor(int buscado,ref string dni,ref bool encontro)
        {
          
            //Si la lista esta vacia
            Nodo_Paciente t = lista;
            Nodo_Paciente aux = null;
            bool flag = false;
            if (lista != null)
            {
                while (t != null)
                {
                    if (t.Nro_dni_paciente == buscado)
                    {
                        encontro = true;
                        dni=buscado.ToString();
                        flag = true;
                        if (t == lista)
                        {
                            lista = t.Sgte;
                        }
                        else
                        {
                            aux.Sgte = t.Sgte;
                        }
                    }
                    aux = t;
                    t = t.Sgte;
                }
                if (flag == false)
                {
                    Console.WriteLine("No se encontró este paciente");
                }
            }
            else
            {
                Console.WriteLine("La lista esta vacia, no se puede eliminar ningun dato");
            }
        }
        //Metodo para generar nombre de hombres
        static Random random= new Random();
        public string GenerarNombreHombre()
        {
            string[] nombresHombres = {
    "Juan", "José", "Luis", "Carlos", "Andrés",
    "Pedro", "Javier", "Daniel", "Miguel", "Antonio",
    "Diego", "Alejandro", "Rafael", "Manuel", "David",
    "Fernando", "Francisco", "Jorge", "Alberto", "Sergio"
};
            string[] apellidos = {"García", "Rodríguez", "Martínez", "López", "González", "Pérez", "Sánchez",
                "Ramírez", "Torres", "Flores", "Vásquez", "Gómez", "Díaz", "Hernández", "Jiménez", "Álvarez",
                "Moreno", "Muñoz", "Romero", "Ruiz" };

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
            string[] nombresMujeres = {"María", "Ana", "Laura", "Carmen", "Rosa", "Isabel", "Patricia", "Carolina",
                "Gabriela", "Lucía", "Elena", "Paola", "Adriana", "Claudia", "Sofía","Marta", "Paula", "Beatriz", "Natalia", "Julia" };
            string[] apellidos = {"García", "Rodríguez", "Martínez", "López", "González", "Pérez", "Sánchez",
                "Ramírez", "Torres", "Flores", "Vásquez", "Gómez", "Díaz", "Hernández", "Jiménez", "Álvarez",
                "Moreno", "Muñoz", "Romero", "Ruiz" };

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
        public string GenerarMalestar()
        {
            string[] malestares = {
            "Migraña", "Fiebre", "Náuseas", "Vómitos", "Mareos", "Tos", "Congestión nasal",
            "Dolor de garganta", "Dolor de espalda", "Dolor abdominal", "Diarrea", "Estreñimiento",
            "Dolor muscular", "Fatiga", "Insomnio", "Ansiedad", "Depresión",
            "Palpitaciones", "Dolor en el pecho"};

            return malestares[random.Next(malestares.Length)];
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
        //Generador de nombre de Seguro
        public string GenerarSeguro()
        {
            string[] seguros = { "SIS", "EsSalud", "Privado" };
            return seguros[random.Next(seguros.Length)];
        }
        //generar edad
        public int GenerarEdad()
        {
            return random.Next(1, 101); //Genera un número aleatorio entre 1 y 100 (incluyendo ambos extremos)
        }
        public string GenerarGravedad()
        {
            string[] gravedad = { "si", "no" };
            return gravedad[random.Next(gravedad.Length)];
        }
       
    }
}

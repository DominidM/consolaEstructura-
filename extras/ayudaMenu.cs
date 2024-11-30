using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.extras
{
    public class ayudaMenu
    {
        //Metodo de case 1 para indicar los controles del sistema
        public static void manejoControles()
        {
            Console.Clear();
            Console.WriteLine("-- Informacion --");
            Console.WriteLine(@"
            El uso de estos es de manera totalmente simple mediante el uso de numeros o letras para poder dirigirse
            hacia donde uno indique , teniendo la funcion de regresar de cada uno de ellos sin temor a la perdida
            de datos.

            Manteniendo los cambios que se hagan durante el proceso.
            
            ©Copyright - Grupo 5 - Estructura de datos - 2024 

            ©Juan Dominid Mu~noz Eslava
            ©Josue Antony Navarro Escudero
            ©Mario Antonio Mallqui Vega 
");
            Console.ReadLine();
        }

        //Metodo de case 2 para indicar que sistemas de estructuras van a ser usadas en este sistema
        public static void estructurasUsadas()
        {
            Console.Clear();
            Console.WriteLine("-- Estructuras Usadas --", Console.ForegroundColor = ConsoleColor.Green);
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine(@"
    Cada semana se estuvo manejando la implementacion de cada una de estas y en base a la rubrica tenemos
    lo siguiente para este proyecto del curso de Estructura de Datos.

    Temas: Gestor medico

        Listas Simplemente Enlazadas ( T1 ){
    
            CRUD de un paciente
            CRUD de cuentas para ingresar al sistema
            return;
        }
        Listas Doblemente Enlazadas ( T1 - T2 ){
    
            CRUD de trabajadores
            CRUD de sedes
            CRUD de ambulancias 
            return;
        }
        Lista Circulamente Enlazadas ( T2 ){
            
            CRUD de reportes
            CRUD de productos (Almacen)
            return;
        }
        

        Pilas ( T2 ){
            
            Historial
            return;
        }


        Colas ( T2 ){
            
            Retiro de mercaderia de almacen 
            return;
        }
        Colas de Prioridad ( T2 ){
            
            Asignacion de pacientes graves a ambulancias
            return;
        }


        Arbol de busqueda ABB ( T2 ){
            
                
            return;
        }
        Busqueda de Arboles de terminos y referencias ( T3 ){
            
            
            
            return;
        }

        Funciones Combinadas ( T1 - T2 - T3 - fnl){

            
            return;
        }

        Grafos ( T3 ) {
            
            
            return;
        }

    ©Copyright - Grupo 5 - Estructura de datos - 2024 

            ©Juan Dominid Mu~noz Eslava
            ©Josue Antony Navarro Escudero
            ©Mario Antonio Mallqui Vega 
");
            Console.ReadLine();
        }

        //Metodo de case 3 para indicar los creditos de los q desarrollaron el sistema
        public static void creditosSistema()
        {
            Console.Clear();
            Console.WriteLine("-- Creditos --");
            Console.WriteLine(@"

            Este proyecto fue creado en el trascurso del curso de estructura de datos
                                           de UPN 20241 Estructura de Datos-4708, con 
                    el docente JUAN RICARDO TAPIA CARBAJAL y DANTE JESUS GARCIA PAICO
            -------------------------------------------------------------------------


                        Créditos del Sistema de Gestión Hospitalaria

        Desarrollado por: [Grupo 5]

            Introducción
        El Sistema de Gestión Hospitalaria (SGH) ha sido concebido y 
        desarrollado por un equipo de ingenieros y profesionales de la
        salud dedicados a mejorar la eficiencia y la calidad del cuidado médico.
        Nuestro objetivo principal es ofrecer una solución integral que optimice
        todos los procesos administrativos y clínicos de las instituciones
    hospitalarias, asegurando un entorno de trabajo más fluido y una 
    atención al paciente de primer nivel.


Beneficios del Sistema                                              


*Eficiencia Administrativa:                                           *Mejora en la Atención al Paciente:

  Automatización de Procesos.-                                          Historia Clínica Electrónica (HCE).-
    Nuestro sistema automatiza tareas rutinarias                          Acceso rápido y seguro a los historiales médicos
    como la gestión de citas, la                                          de los pacientes, permitiendo
    facturación y la administración de inventarios,                       a los profesionales de la salud tomar decisiones
    reduciendo significativamente el tiempo y el esfuerzo                 informadas de manera oportuna.
    requerido para estas actividades.

  Reducción de Errores.-                                                Portal del Paciente.-
   La automatización y la digitalización                                 Los pacientes pueden acceder a sus propios registros,
   de los registros médicos minimizan los errores humanos,               programar citas, y comunicarse con su equipo médico
   mejorando la precisión y la confiabilidad de la información.          desde cualquier lugar, fomentando una mayor
                                                                         participación y satisfacción.


*Optimización de Recursos:                                            *Seguridad y Cumplimiento:

  Gestión de Inventarios.-                                              Protección de Datos.-
   Seguimiento preciso de medicamentos y suministros                     Implementamos las más avanzadas medidas de seguridad
   médicos, asegurando que siempre haya disponibilidad                   para proteger la información sensible de los
   y reduciendo el desperdicio.                                          pacientes, cumpliendo con las normativas nacionales 
                                                                         e internacionales.

  Asignación de Personal.-                                              Auditorías y Reportes.-
   Herramientas avanzadas para la planificación                          eneración automática de informes y registros de auditoría,
   y distribución eficiente del personal, mejorando                      G facilitando el cumplimiento de las regulaciones y 
   la cobertura y reduciendo la carga de trabajo.                        normativas vigentes.


*Interoperabilidad:

  Integración con Otros Sistemas.-                                      Escalabilidad.- 
    Nuestro SGH se integra fácilmente con otros sistemas de salud,      Diseñado para crecer junto con la institución, nuestro sistema
    laboratorios y farmacias, permitiendo un flujo de información       puede adaptarse a las necesidades cambiantes  del hospital,
    continuo y sin interrupciones.                                      desde pequeñas clínicas hasta grandes redes hospitalarias.

  
Agradecimientos

Queremos expresar nuestro profundo agradecimiento a todos los profesionales
de la salud que han colaborado en la creación y perfeccionamiento de este sistema.
Su experiencia y conocimientos han sido fundamentales para el desarrollo de una 
herramienta que verdaderamente responde a las necesidades del sector sanitario.

Compromiso Futuro

En Grupo 5, estamos comprometidos con la innovación continua
y la mejora constante de nuestro Sistema de Gestión Hospitalaria.
Nos esforzamos por mantenernos a la vanguardia de la tecnología 
y las mejores prácticas en el cuidado de la salud, garantizando que
nuestros usuarios siempre cuenten con las herramientas más avanzadas y efectivas.



Contacto
Para más información sobre nuestro sistema, asistencia técnica o consultas generales, no dude en contactarnos:

Correo Electrónico: [dominidzero@gmail.com]
Teléfono: [+51 975 852 932]
Gracias por confiar en nosotros para mejorar la gestión y la atención en su institución de salud

           
  ©Copyright - Grupo 5 - Estructura de datos - 2024 

            ©Juan Dominid Mu~noz Eslava
            ©Josue Antony Navarro Escudero
            ©Mario Antonio Mallqui Vega ");
            Console.ReadKey();
        }

        //Metodo para ayuda al cliente 
        public static void menuAyuda()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                Console.WriteLine(">|   SELECCIONA EL TIPO DE AYUDA QUE REQUIERAS   |<", Console.ForegroundColor = ConsoleColor.White);
                Console.WriteLine("═══════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                elementosDecoracion.tabularMenuRojo("1", "Manejo de controles\n");
                elementosDecoracion.tabularMenuRojo("2", "Estructuras usadas\n");
                elementosDecoracion.tabularMenuRojo("3", "Creditos\n"); 
                elementosDecoracion.tabularMenuRojo("0", "Volver\n");
                Console.WriteLine("══════════════════════════════════════════════════", Console.ForegroundColor = ConsoleColor.Red);
                Console.Write(">| ", Console.ForegroundColor = ConsoleColor.White);
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        manejoControles();
                        break;
                    case "2":
                        estructurasUsadas();
                        break;
                    case "3":
                        creditosSistema();
                        break;

                    case "0":
                        return;
                    default:
                        Console.Write("\n [x] ERROR1 / Vuelve a ingresar un valor", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}

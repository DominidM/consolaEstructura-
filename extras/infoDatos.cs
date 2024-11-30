using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._1_listasSimples._0._1._1_login;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._0_inventarioListaCircular;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._1_reporteListaCircular;

namespace T1_Gestor_Medico_de_Referencias.extras
{
    //Clase para generar datos de las estructurad de datos del sistema 
    public class infoDatos
    {
        /// <summary>
        /// Ingresar datos por consola 
        /// </summary>

        //listas 

        //Metodo para ingresar datos por consola de la lista simple

        //cuenta
        public static void simple1(listaSimpleCreador listaSimpleCreador)
        {
            listaSimpleCreador.Agregar("cuenta", "hola");
            listaSimpleCreador.Agregar("VACIO", "adios");
        }
        //pacientes
        public static void simple2(listaSimplePaciente listaSimplePaciente) //(SIS / EsSalud / Privado)
        {
            //listaSimplePaciente.Registrar_Paciente("Carlos",23,82794561,"SIS","Fatiga","Masculino");
            //listaSimplePaciente.Registrar_Paciente("Maria",19,69502147,"EsSalud","Disnea","Femenino");
            //listaSimplePaciente.Registrar_Paciente("Dominid",23,77230584,"Privado","Dolor abdominal","Masculino");
            //listaSimplePaciente.Registrar_Paciente("Luisa",54,75333155,"SIS","Mareos","Femenino");
            //listaSimplePaciente.Registrar_Paciente("Ximena", 29, 74933021,"EsSalud","Fiebre elevada","Femenino");
            //listaSimplePaciente.Registrar_Paciente("Allison", 17, 73315856, "EsSalud", "Gases", "Feminino");
            //listaSimplePaciente.Registrar_Paciente("Anthony", 24, 73417120, "Privado", "Sangrado", "Masculino");
            //listaSimplePaciente.Registrar_Paciente("Mario", 22, 78328130, "Privado", "Mareos", "Masculino");
        }

        //Metodo para ingresar datos por consola de la lista doble

        //trabajadores
        public static void doble1(listaDobleTrabajadores listaDobleTrabajadores)   //(supervisor/medico/conductor/limpieza)
        {
            //listaDobleTrabajadores.insertaAlInicioLD("Jorge",32,75229498,"Masculino","supervisor",false);
            //listaDobleTrabajadores.insertaAlInicioLD("Roxana", 24, 73452386,"Femenino","medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Pedro", 28, 69874122, "Masculino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Laura", 35, 62587429, "Femenino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Diego", 42, 73217489, "Masculino", "conductor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Ana", 29, 73258784, "Femenino", "limpieza", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Luis", 31, 78751346, "Masculino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Mario", 21, 78333122, "Masculino", "conductor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Fabiana", 19, 72413868, "Femenino", "limpieza", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Sara", 28, 77852361, "Femenino", "supervisor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Alberto", 30, 76326457, "Masculino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Eva", 35, 51478233, "Femenino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Francisco", 40, 89741253, "Masculino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Anabelle", 25, 73698742, "Femenino", "conductor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Diego", 29, 71478753, "Masculino", "conductor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Lorena", 33, 75896732, "Femenino", "limpieza", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Marco", 27, 76325874, "Masculino", "limpieza", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Luisa", 31, 74125876, "Femenino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Pedro", 34, 79856321, "Masculino", "supervisor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Carmen", 26, 74567891, "Femenino", "medico", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Antonio", 39, 73214567, "Masculino", "supervisor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Isabel", 32, 79874563, "Femenino", "conductor", false);
            //listaDobleTrabajadores.insertaAlInicioLD("Javier", 37, 77412589, "Masculino", "limpieza", false);
        }
        //Sedes
        public static void doble2(listaDobleSedes listaDobleSedes)
        {


        }
        //Ambulancias
        public static void doble3(listaDobleAmbulancias listaDobleAmbulancias)
        {


        }
    
        //Metodo para ingresar datos por consola de la lista circular

        //Inventario
        public static void circular1(listaCircularInventario listaCircularInventario) 
        {

        }
        //Reporte
        public static void circular2(listaCircularReporte listaCircularReporte)
        {

        }

    }
}

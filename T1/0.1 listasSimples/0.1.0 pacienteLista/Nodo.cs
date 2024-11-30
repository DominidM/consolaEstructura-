using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias
{
    public class Nodo_Paciente
    {
        //Definimos variables
        private string nombre_paciente;
        private int edad_paciente; //edad pasa de 65 prio+1
        private int nro_dni_paciente;
        private string seguro_med;
        private string malestares_paciente;
        private string genero_paciente;
        private string gravedad_paciente;


        private string doctor_asignado="";  //doctor ya asignado
        private bool ambulancia_asignada = false;    //asignar ambulancia si variable gravedad es == si
        private string sede_asignada = "";  //asignar la sede
        private Nodo_Paciente sgte;


        public string Nombre_paciente {get => nombre_paciente;  set =>  nombre_paciente = value; }
        public int Edad_paciente {get => edad_paciente;  set => edad_paciente = value;}
        public int Nro_dni_paciente { get => nro_dni_paciente; set => nro_dni_paciente = value;}
        public string Seguro_med { get => seguro_med; set => seguro_med = value;}
        public string Malestares_paciente { get => malestares_paciente; set => malestares_paciente = value;}
        public string Genero_paciente { get => genero_paciente; set => genero_paciente = value;}
        public string Doctor_asignado { get => doctor_asignado; set => doctor_asignado = value; }
        public bool Ambulancia_asignada { get => ambulancia_asignada; set => ambulancia_asignada = value; }
        public string Sede_asignada { get => sede_asignada; set => sede_asignada = value; }
        public string Gravedad_paciente { get => gravedad_paciente; set => gravedad_paciente = value; }

        internal Nodo_Paciente Sgte
        {
            get { return sgte; }
            set { sgte = value; }
        }
        //Declaramos el nodo para el registro de los datos del paciente

    }
}

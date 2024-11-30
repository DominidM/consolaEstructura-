using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista
{
    public class Nodo_Trabajadores
    {
        //VARIABLES 
        private string nombre_e;
        private int edad_e;
        private int nro_dni_e;
        private string genero_e;
        private string cargo_e;
        private bool asignado=false;
        private Nodo_Trabajadores sgte;
        private Nodo_Trabajadores ant;

        //GETS Y SETS
        public string Nombre_e { get => nombre_e; set => nombre_e = value; }
        public int Edad_e { get => edad_e; set => edad_e = value; }
        public int Nro_dni_e { get => nro_dni_e; set => nro_dni_e = value; }
        public string Genero_e { get => genero_e; set => genero_e = value; }
        public string Cargo_e { get => cargo_e; set => cargo_e = value; }
        public bool Asignado { get => asignado; set => asignado = value; }

        public Nodo_Trabajadores Sgte
        {
            get { return sgte; }
            set { sgte = value; }
        }
        public Nodo_Trabajadores Ant
        {
            get { return ant; }
            set { ant = value; }
        }
        //CONSTRUCTOR
        public Nodo_Trabajadores(string nombre, int edad, int dni, string genero, string cargo, bool asignado)
        {
            Nombre_e = nombre;
            Edad_e = edad;
            Nro_dni_e = dni;
            Genero_e = genero;
            Cargo_e = cargo;
            Ant = null;
            Sgte = null;
            this.Asignado = asignado;
        }
    }
}

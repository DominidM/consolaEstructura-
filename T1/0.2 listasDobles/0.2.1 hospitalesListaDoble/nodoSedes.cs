using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;

namespace T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble
{
    public class nodoSedes
    {
        //VARIABLES 
        private string nombre_sede;
        private string ubicacion;
        private int numero_telefono;
        private string codigo;
        private nodoSedes sgte;
        private nodoSedes ant;

        //GETS Y SETS
        public string Nombre_sede { get => nombre_sede; set => nombre_sede = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public int Numero_telefono { get => numero_telefono; set => numero_telefono = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public nodoSedes Sgte
        {
            get { return sgte; }
            set { sgte = value; }
        }
        public nodoSedes Ant
        {
            get { return ant; }
            set { ant = value; }
        }

        //CONSTRUCTOR
        public nodoSedes(string nombre, string ubicacion, int numero,string codigo)
        {
            Nombre_sede = nombre;
            Ubicacion = ubicacion;
            Numero_telefono = numero;
            Codigo = codigo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T3._2._1_arbol_Referencia
{
    public class nodoArbol
    {
        public int edad;
        public string nombre;
        public nodoArbol izq;
        public nodoArbol der;

        public nodoArbol(int edad, string nombre)
        {
            this.edad = edad;
            this.nombre = nombre;
            izq = null;
            der = null;
        }
    }
}

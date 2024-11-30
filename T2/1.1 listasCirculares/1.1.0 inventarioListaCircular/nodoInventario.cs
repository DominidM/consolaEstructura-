using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._0_inventarioListaCircular
{
    public class nodoInventario
    {
        //VARIABLES
        public string nombre { get; set; }
        public string codigo { get; set; }
        public int stock { get; set; }
        public nodoInventario Sgte { get; set; }
        //CONSTRUCTOR
        public nodoInventario(string nombre, string codigo, int stock)
        {
            this.nombre = nombre;
            this.codigo = codigo;
            this.stock = stock;
            this.Sgte = null;
        }
    }
}

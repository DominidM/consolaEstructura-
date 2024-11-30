using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_Almacen
{
    public class nodoAlmacen
    {
        //VARIABLES PRIVATE
        private string CodigoProducto;
        private int Cantidad;
        private nodoAlmacen Sgte1;

        //GETTERS AND SETTERS
        public string codigoProducto { get => CodigoProducto; set => CodigoProducto = value; }
        public int cantidad { get => Cantidad; set => Cantidad = value; }
        public nodoAlmacen Sgte { get => Sgte1; set => Sgte1 = value; }

        //CONSTRUCTOR
        public nodoAlmacen(string codigoProducto, int cantidad)
        {
            this.CodigoProducto = codigoProducto;
            this.cantidad = cantidad;
            this.Sgte = null;
        }

    }
}

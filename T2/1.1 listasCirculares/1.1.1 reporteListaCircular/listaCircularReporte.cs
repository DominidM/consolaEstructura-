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

namespace T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._1_reporteListaCircular
{
    public class listaCircularReporte
    {
        public nodoReporte cabeza;
        public nodoReporte ultimo;

        public listaCircularReporte()
        {
            cabeza = null;
            ultimo = null;
        }

        public void Mostrar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            nodoReporte actual = cabeza;
            do
            {
                Console.WriteLine($"Fecha Reporte: {actual.FechaReporte}");
                actual = actual.Sgte;
            } while (actual != cabeza);
            Console.ReadKey();
        }

        // Propiedad para obtener el head de la lista
        public nodoReporte Head
        {
            get { return cabeza; }
        }
    }
}

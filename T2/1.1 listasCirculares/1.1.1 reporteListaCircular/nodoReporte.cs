using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._1_listasSimples._0._1._1_login;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_Almacen;

namespace T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._1_reporteListaCircular
{
    public class nodoReporte
    {
        private DateTime fechaReporte;

        private nodoReporte sgte;

        //agregados para la clase nodoreporte
        public DateTime FechaReporte { get => fechaReporte; set => fechaReporte = value; }
        public nodoReporte Sgte { get => sgte; set => sgte = value; }
    }
}

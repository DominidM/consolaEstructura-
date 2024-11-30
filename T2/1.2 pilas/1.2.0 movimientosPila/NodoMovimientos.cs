using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T2._1._2_pilas
{
    public class NodoMovimientos
    {
        private string tipo_movimiento;
        private string info_extra;
        private DateTime hora_registro;
        private string cambio;
        private string tipo_dato;
        private NodoMovimientos sgte;

        public string Tipo_movimiento { get => tipo_movimiento; set => tipo_movimiento = value; }
        public string Info_extra { get => info_extra; set => info_extra = value; }
        public DateTime Hora_registro { get => hora_registro; set => hora_registro = value; }
        public string Cambio { get => cambio; set => cambio = value; }
        public string Tipo_dato { get => tipo_dato; set => tipo_dato = value; }
        public NodoMovimientos Sgte { get => sgte; set => sgte = value; }
    }
}

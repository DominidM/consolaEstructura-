using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble;

namespace T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._0_colaConPrioridad._1._3._0._1_ingresoAmbulanciasColaSinPrioridad
{
    public class nodoColaPrioAmbulancias
    {
        //variables privadas
        private Nodo_Paciente listaPaciente;
        private nodoAmbulancias listaAmbulancias; //  compuesta por la clase de ambulancias
        private int prioridad;
        private nodoColaPrioAmbulancias sgte;


        //getters and setters

        //usando una dependencia 
        public Nodo_Paciente ListaPaciente { get => listaPaciente; set => listaPaciente = value; }
        public nodoAmbulancias ListaAmbulancias { get => listaAmbulancias; set => listaAmbulancias = value; }
        //puntero
        public nodoColaPrioAmbulancias Sgte { get => sgte; set => sgte = value; }
        //prioridad de la cola
        public int Prioridad { get => prioridad; set => prioridad = value; }
    }
}

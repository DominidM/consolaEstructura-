using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;

namespace T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble
{
    public class nodoAmbulancias
    {
        //VARIABLES 
        private string marca;
        private int prioVelocidad;  
        private string placa;
        private string codigo;
        private string conductor;
        private nodoAmbulancias sgte;
        private nodoAmbulancias ant;

        //getters and setters
        public int PrioVelocidad { get => prioVelocidad; set => prioVelocidad = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Placa { get => placa; set => placa = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Conductor { get => conductor; set => conductor = value; }
        public nodoAmbulancias Sgte { get => sgte; set => sgte = value; }
        public nodoAmbulancias Ant { get => ant; set => ant = value; }

        //constructor
        public nodoAmbulancias(string marca, int prioVelocidad, string placa, string codigo, string conductor)
        {
            this.marca = marca; //marca del carro
            this.prioVelocidad = prioVelocidad; // prioridad 
            this.placa = placa;
            this.codigo = codigo;
            this.conductor = conductor;
            this.sgte = null;
            this.ant = null;
        }
    }
}

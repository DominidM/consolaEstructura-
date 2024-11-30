using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._0_trabajadoresLista;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._2_ambulanciasListaDoble;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._0_colaConPrioridad._1._3._0._1_ingresoAmbulanciasColaSinPrioridad;

namespace T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._0_ingresoAmbulanciasColaSinPrioridad
{
    public class colaIngresoDeAmbulancias
    {
        public nodoColaPrioAmbulancias colaConPrio;

        public void InsertarEnColaConPrioridad(nodoColaPrioAmbulancias nuevoNodo)
        {
            if (colaConPrio == null || nuevoNodo.Prioridad < colaConPrio.Prioridad)
            {
                nuevoNodo.Sgte = colaConPrio;
                colaConPrio = nuevoNodo;
            }
            else
            {
                nodoColaPrioAmbulancias actual = colaConPrio;
                while (actual.Sgte != null && actual.Sgte.Prioridad <= nuevoNodo.Prioridad)
                {
                    actual = actual.Sgte;
                }
                nuevoNodo.Sgte = actual.Sgte;
                actual.Sgte = nuevoNodo;
            }
        }
    
        public void MostrarColaConPrio()
        {
            nodoColaPrioAmbulancias actual = colaConPrio;
            while (actual != null)
            {
                Console.WriteLine($"Paciente: {actual.ListaPaciente.Nombre_paciente}, Edad: {actual.ListaPaciente.Edad_paciente}, Ambulancia: {actual.ListaAmbulancias.Marca}, Prioridad: {actual.Prioridad}");
                actual = actual.Sgte;
            }
        }
        
    }
}

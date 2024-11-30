using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T2._1._2_pilas
{
    public class pilaPacientes
    {
        public NodoMovimientos listap;

        public pilaPacientes()
        {
            listap = null;
        }
        public void push(string movimiento, string info,string cambio,string tipo_dato)
        {
            NodoMovimientos q = new NodoMovimientos();
            q.Tipo_movimiento = movimiento;
            q.Info_extra = info;
            q.Hora_registro = DateTime.Now;
            q.Cambio=cambio;
            q.Tipo_dato = tipo_dato;
            q.Sgte = listap;
            listap = q;
        }

        public void muestraPila(string tipoDato)
        {
            NodoMovimientos t = listap;
            while (t != null)
            {
                if (t.Tipo_dato==tipoDato)
                {
                    Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"\t\tTipo de movimiento: {t.Tipo_movimiento,-25} ");
                    Console.WriteLine($"\t\tDato del Usuario: {t.Info_extra,-25} ");
                    Console.WriteLine($"\t\tHora: {t.Hora_registro,-25} ");
                    Console.WriteLine($"\t\tInformación extra: {t.Cambio,-25} ");
                 
                }
                t = t.Sgte;
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════╝");

        }

        public void pop()
        {
            if (listap != null)
            {
                listap = listap.Sgte;
            }
            else
            {
                Console.WriteLine("Pila de movimientos vacia, no se pueden retirar mas valores .. ");
            }
        }
        public void EliminarPorTipo(string tipo)
        {
            //Verificar si la pila está vacía
            if (listap == null)
            {
                Console.WriteLine("La pila está vacía.");
                return;
            }

            //Recorrer la pila y eliminar los nodos que coincidan con el tipo especificado
            NodoMovimientos actual = listap;
            NodoMovimientos anterior = null;

            while (actual != null)
            {
                //Verificar si el tipo de dato del nodo actual coincide con el tipo especificado
                if (actual.Tipo_dato == tipo)
                {
                    //Si es el primer nodo de la pila, actualizar el tope
                    if (actual == listap)
                    {
                        listap = actual.Sgte;
                    }
                    else
                    {
                        //Enlazar el nodo anterior con el siguiente nodo
                        anterior.Sgte = actual.Sgte;
                    }

                    //Avanzar al siguiente nodo
                    actual = actual.Sgte;
                }
                else
                {
                    //Avanzar al siguiente nodo
                    anterior = actual;
                    actual = actual.Sgte;
                }
            }

            Console.WriteLine($"Se han eliminado todos los nodos de tipo {tipo}.");
        }
    }
}


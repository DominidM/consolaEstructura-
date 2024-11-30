using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T3._2._1_arbol_Referencia
{
    public class arbolVacunacion
    {
        public nodoArbol ArbolVacunacion;

        public arbolVacunacion()
        {
            ArbolVacunacion = null;
        }
        public void insertarNodoArbolInicio(ref nodoArbol arbol, int edad, string nombre)
        {
            //Si el arbol es nulo, no tiene nodos
            if (arbol == null)
            {
                //Crear el nodo del arbol
                arbol = new nodoArbol(edad, nombre);
                //Asignar el dato al nodo del arbol

            }
            else
            {
                //Arbol tiene nodos o elementos
                //Si el dato ingresado es menor al valor del nodo del arbol
                //se inserta el dato en el arbol por la izquierda
                if (edad < arbol.edad)
                    insertarNodoArbolInicio(ref arbol.izq, edad, nombre);
                else
                    //Si el dato ingresado es mayor o igual al valor del nodo
                    //del arbol, se inserta el dato en el arbol por la derecha
                    if (edad >= arbol.edad)
                    insertarNodoArbolInicio(ref arbol.der, edad, nombre);
            }
        }
        public void insertarPacienteArbol(ref nodoArbol arbol, listaSimplePaciente listaPaciente)
        {
            Nodo_Paciente p = listaPaciente.lista;
            if (p != null)
            {
                while (p != null)
                {
                    insertarNodoArbolInicio(ref arbol, p.Edad_paciente, p.Nombre_paciente);
                    p = p.Sgte;
                }
            }
        }
        public void verArbol(nodoArbol arbol, int n)
        {
            int i;
            //Si el arbol es nulo, salir
            if (arbol == null)
                return; //Salir
            //Mostrar el arbol por la derecha
            verArbol(arbol.der, n + 1); //Nivel 1
            //Imprimir espacios en blanco
            for (i = 0; i < n; i++)
                Console.Write("    ");
            //Mostrar el dato del nodo del arbol
            Console.WriteLine("{0}", "|" + arbol.edad + "|" + arbol.nombre); //Nivel 0
            //Mostrar el arbol por la izquierda
            verArbol(arbol.izq, n + 1); //Nivel 1
        }
        public void TalarArbol(nodoArbol arbol)
        {
            if (arbol == null)
            {
                return;
            }

            // Recorrer y eliminar el subárbol izquierdo
            TalarArbol(arbol.izq);
            // Recorrer y eliminar el subárbol derecho
            TalarArbol(arbol.der);
            arbol.izq = null;
            arbol.der = null;
            arbol = null;

        }
        public void inOrder(nodoArbol arbol)
        {
            //Si el arbol no es nulo
            if (arbol != null)
            {
                //Mostrar el arbol izquierdo
                inOrder(arbol.izq);
                //Mostrar la raiz
                Console.WriteLine("{0}", "|" + arbol.edad + "|" + arbol.nombre);
                //Mostrar el arbol derecho
                inOrder(arbol.der);
            }
        }
        public void postOrden(nodoArbol arbol)
        {
            //Si el arbol no es nulo
            if (arbol != null)
            {
                //Mostrar el arbol izquierdo
                postOrden(arbol.izq);
                //Mostrar el arbol derecho
                postOrden(arbol.der);
                //Mostrar la raiz
                Console.WriteLine("{0}", "|" + arbol.edad + "|" + arbol.nombre);
            }
        }
        public void preOrden(nodoArbol arbol)
        {
            //Si el arbol no es nulo
            if (arbol != null)
            {
                //Mostrar la raiz
                Console.WriteLine("{0}", "|" + arbol.edad + "|" + arbol.nombre);
                //Mostrar el arbol izquierdo
                preOrden(arbol.izq);
                //Mostrar el arbol derecho
                preOrden(arbol.der);
            }
        }
        public void BusquedaPorCodigo(nodoArbol arbol, int codigo, ref bool encontrado)
        {
            //Comprobamos si el arbol está vacio
            if (arbol != null)
            {
                BusquedaPorCodigo(arbol.izq, codigo, ref encontrado);
                if (arbol.edad == codigo)
                {
                    encontrado = true;
                    Console.WriteLine("{0}", "|" + arbol.edad + "|" + arbol.nombre);
                }
                BusquedaPorCodigo(arbol.der, codigo, ref encontrado);
            }
        }
        public void podarArbol(ref nodoArbol arbol)
        {
            //Si el arbol tiene elementos o nodos
            if (arbol != null)
            {
                //Podar el arbol por la izquierda
                podarArbol(ref arbol.izq);
                //Podar el arbol por la derecha
                podarArbol(ref arbol.der);
                arbol = null; //Eliminar el nodo del arbol
            }
        }
        public nodoArbol EncontrarNodoRecursivo(nodoArbol nodo, int valor)
        {
            if (nodo == null || nodo.edad == valor)
            {
                return nodo;
            }
            //Buscamos el nodo en los lados izquierdo y derecho del arbol
            if (valor < nodo.edad)
            {
                return EncontrarNodoRecursivo(nodo.izq, valor);
            }
            else
            {
                return EncontrarNodoRecursivo(nodo.der, valor);
            }
        }
        public void eliminaNodoABB(ref nodoArbol arbol, int dato)
        {
            nodoArbol p1, p2; //p1 y p2 hacen referencia al nodo del arbol
            //Si el arbol no tiene elementos, salir
            if (arbol == null) return;
            //Si el dato es menor al dato del nodo del arbol
            //Eliminar el nodo del arbol por la izquierda
            if (dato < arbol.edad) eliminaNodoABB(ref arbol.izq, dato);
            //Si el dato es mayor al dato del nodo del arbol
            //Eliminar el nodo del arbol por la derecha
            else if (dato > arbol.edad) eliminaNodoABB(ref arbol.der, dato);
            //Si el arbol no tiene hijos
            else if (arbol.izq == arbol.der)
            {
                arbol = null; //arbol apunta a null
            }
            //Si el arbol no tiene hijo por la izquierda
            else if (arbol.izq == null)
            {
                p1 = arbol; //p1 apunta al nodo del arbol (guardar el puntero del elemento a eliminar)  
                arbol = arbol.der; //Apuntar al hijo derecho del arbol
                p1 = null; //p1 apunta a null
            }
            //Si el arbol no tiene hijo por la derecha
            else if (arbol.der == null)
            {
                p1 = arbol; //p1 apunta al nodo del arbol (guardar el puntero del elemento a eliminar)  
                arbol = arbol.izq; //Apuntar al hijo izquierdo del arbol
                p1 = null; //p1 apunta a null
            }
            else
            {
                //Si el arbol tiene dos hijos
                p1 = arbol.der; //p1 apunta al hijo derecho del arbol (guardar el puntero)
                p2 = arbol.der; //p2 apunta al hijo derecho del arbol (para avanzar)
                //Buscar el menor de los mayores
                while (p2.izq != null)
                    p2 = p2.izq; //Avanzar al siguiente hijo por la izquierda
                //Hijo izquierdo de menor de mayor = Hijo izquierdo de eliminado
                p2.izq = arbol.izq;
                arbol = null;//Eliminar el nodo del arbol
                arbol = p1; //El padre apunta al derecho de eliminado
            }
        }
    }
}

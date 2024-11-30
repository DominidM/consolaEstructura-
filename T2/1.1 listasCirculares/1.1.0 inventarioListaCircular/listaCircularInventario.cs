using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T1._0._2_listasDobles._0._2._1_hospitalesListaDoble;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_Almacen;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_atencionAlClienteColaConPrioridad;

namespace T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._0_inventarioListaCircular
{
    public class listaCircularInventario
    {
        public nodoInventario lista;

        public listaCircularInventario()
        {
            lista = null;
        }

        public void Insertar(string nombre, string codigo, int stock)
        {
            nodoInventario p = new nodoInventario(nombre, codigo, stock);

            if (lista == null)
            {
                lista = p;
                lista.Sgte = lista; // El único nodo apunta a sí mismo
            }
            else
            {
                nodoInventario actual = lista;
                while (actual.Sgte != lista)
                {
                    actual = actual.Sgte;
                }
                actual.Sgte = p;
                p.Sgte = lista;
            }
        }
        public void Mostrar()
        {
            if (lista == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            nodoInventario actual = lista;
            Console.WriteLine("REGISTRO:");
            Console.WriteLine("----------------------------");
            do
            {
                Console.WriteLine($"Nombre: {actual.nombre}\n Código: {actual.codigo}\n Stock: {actual.stock}");
                Console.WriteLine("----------------------------");
                actual = actual.Sgte;
            } while (actual != lista);
        }
        public void Modificar(ref bool registrarMov, ref string cambio, ref string buscar1)
        {
            colaAlmacen cola=new colaAlmacen();
            nodoAlmacen A=cola.colaAlamacen;
            int int_aux=0;
            if (lista == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }
            Mostrar();
            Console.WriteLine("Ingrese el código del Producto a modificar: ");
            string codigo=Console.ReadLine();
            nodoInventario p = lista;
            do
            {
                if (p.codigo == codigo)
                {
                    registrarMov = true;
                    Console.Clear();
                    Console.WriteLine($"Nodo encontrado: Nombre: {p.nombre}, Código: {p.codigo}, Stock: {p.stock}");
                    Console.WriteLine("Seleccione el dato que desea modificar:");
                    Console.WriteLine("[1] Nombre del producto");
                    Console.WriteLine("[2] Código");
                    Console.WriteLine("[3] Stock");
                    int opc=int.Parse(Console.ReadLine());
                    switch (opc)
                    {
                        case 1:
                            Console.WriteLine("Ingrese el nuevo nombre: ");
                            p.nombre = Console.ReadLine();
                            buscar1 = codigo;
                            cambio = "Se cambío el nombre del producto a :" +p.nombre;
                            break;
                        case 2:
                            Console.WriteLine("Ingrese el nuevo código: (Ejm. PD-001)");
                            string codigo1= Console.ReadLine();
                            p.codigo = codigo1;
                            buscar1 = codigo1;
                            if (A != null)
                            {
                                while (A != null)
                                {
                                    if (A.codigoProducto == codigo)
                                    {
                                        A.codigoProducto = codigo1;
                                    }
                                    A = A.Sgte;
                                }
                            }
                            cambio = "Se cambío el código del producto de "+codigo+" a "+codigo1;
                            break;
                        case 3:
                            do
                            {
                                Console.WriteLine("Ingrese el nuevo stock: ");
                            }
                            while (!int.TryParse(Console.ReadLine(), out int_aux));
                            p.stock = int_aux;
                            buscar1=codigo;
                            cambio = "Se cambío el stock a: "+p.stock;
                            break;
                    }
                    Console.WriteLine("Dato modificado exitosamente.");
                    return;
                }
                    p = p.Sgte;
            } while (p != lista);

            Console.WriteLine("No se encontró un nodo con el código especificado.");
        }
        public bool Buscar(string codigo)
        {
            if (lista == null)
            {
                Console.WriteLine("La lista está vacía.");
                return false;
            }
            nodoInventario p = lista.Sgte;
            do
            {
                if (p.codigo == codigo)
                {
                    return true;
                }
                p = p.Sgte;
            } while (p != lista.Sgte);

            Console.WriteLine("No se encontró un nodo con el código especificado.");
            return false;
        }

        public void Eliminar(string codigo,ref string codigo1,ref bool encontro)
        {
            if (lista == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }
            nodoInventario p = lista.Sgte;
            nodoInventario ant = lista;
            do
            {
                if (p.codigo == codigo)
                {
                    encontro= true;
                    codigo1 = codigo;
                    if (p == lista && p.Sgte == lista)
                    {
                        lista = null;
                    }
                    else
                    {
                        ant.Sgte = p.Sgte;
                        if (p == lista)
                        {
                            lista = ant;
                        }
                    }
                    Console.WriteLine("Nodo eliminado exitosamente.");
                    return;
                }
                ant = p;
                p = p.Sgte;
            } while (p != lista.Sgte);

            Console.WriteLine("No se encontró un nodo con el código especificado.");
        }
        Random random = new Random();
        public string GenerarCodigo()
        {
            // Generar dos letras mayúsculas aleatorias
            char letra1 = (char)random.Next('A', 'Z' + 1);
            char letra2 = (char)random.Next('A', 'Z' + 1);

            // Generar tres números aleatorios
            int numero1 = random.Next(10);
            int numero2 = random.Next(10);
            int numero3 = random.Next(10);

            // Formar el código con el formato deseado
            string codigo = $"{letra1}{letra2}-{numero1}{numero2}{numero3}";

            return codigo;
        }
        public string GenerarNombre()
        {
            string[] nombres = {
            "Paracetamol", "Ibuprofeno", "Aspirina", "Omeprazol", "Amoxicilina",
            "Loratadina", "Ciprofloxacino", "Diazepam", "Atorvastatina", "Metformina",
            "Ranitidina", "Dipirona", "Clonazepam", "Pantoprazol", "Sildenafil",
            "Fluoxetina", "Cetirizina", "Losartan", "Levofloxacino", "Simvastatina",
            "Venlafaxina", "Tramadol", "Hidroclorotiazida", "Enalapril", "Escitalopram",
            "Metronidazol", "Tadalafil", "Alprazolam", "Furosemida", "Escopolamina"
            };
            return nombres[random.Next(nombres.Length)];
        }
        public int GenerarNumero()
        {
            Random rnd = new Random();
            return rnd.Next(5, 21); 
     
        }

        public int conteoInventario()
        {
            // Verificar si la lista está vacía
            if (lista == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n0 cajas de productos en el inventario");
                Console.ResetColor();
                return 0;
            }

            nodoInventario t = lista;
            int contador = 0;

            // Recorrer la lista circularmente enlazada
            do
            {
                contador++;
                t = t.Sgte;
            } while (t != lista);

            // Imprimir el resultado final fuera del bucle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + contador + " cajas de productos en el invetarnio");
            Console.ResetColor();

            return contador;
        }

    }
}

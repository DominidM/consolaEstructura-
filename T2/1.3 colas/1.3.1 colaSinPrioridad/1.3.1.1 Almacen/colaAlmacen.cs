using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1_Gestor_Medico_de_Referencias.T2._1._1_listasCirculares._1._1._0_inventarioListaCircular;
using T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_Almacen;

namespace T1_Gestor_Medico_de_Referencias.T2._1._3_colas._1._3._1_atencionAlClienteColaConPrioridad
{
    public class colaAlmacen
    {
        public nodoAlmacen colaAlamacen;

        public colaAlmacen()
        {
            colaAlamacen = null;
        }

        public void Encolar(string codigoProducto, int cantidad,listaCircularInventario listaC,ref bool existe)
        {
            nodoInventario nodoinv = listaC.lista;
            existe=false;
            if(nodoinv!=null) { 
            do
            {
                if (nodoinv.codigo == codigoProducto)
                {
                    existe = true;
                }
                nodoinv = nodoinv.Sgte;
            } while (nodoinv != listaC.lista);
            if (!existe)
            {
                    Console.WriteLine($"El producto con código " + codigoProducto + " no existe en el inventario. Intente de nuevo.");
                return;
            }

            nodoAlmacen nuevoPedido = new nodoAlmacen(codigoProducto, cantidad);
            if (colaAlamacen == null)
            {
                colaAlamacen = nuevoPedido;
            }
            else
            {
                nodoAlmacen p = colaAlamacen;
                while (p.Sgte != null)
                {
                    p = p.Sgte;
                }
                p.Sgte = nuevoPedido;
            }
            }
        }

        public void Decolar(listaCircularInventario inventario,ref string info,ref bool si,ref string code)
        {
            if (colaAlamacen == null)
            {
                Console.WriteLine("La cola está vacía.");
                return;
            }
            nodoInventario producto = inventario.lista;
            nodoAlmacen pedido = colaAlamacen;
            code = pedido.codigoProducto;
            si = false;
            while (pedido != null&&si!=true)
            {
                do
                {
                    if (!inventario.Buscar(pedido.codigoProducto))
                    {
                        Console.WriteLine($"El producto con código {pedido.codigoProducto} no existe en el inventario.");
                    }
                    else
                    {
                     
                        if(pedido.codigoProducto==producto.codigo)
                        {
                            if (producto.stock >= pedido.cantidad)
                            {
                                producto.stock -= pedido.cantidad;
                                Console.WriteLine($"Se ha despachado {pedido.cantidad} unidades del producto con código {pedido.codigoProducto}.");
                                info = $"Se ha despachado {pedido.cantidad} unidades del producto con código {pedido.codigoProducto}.";
                            }
                            else
                            {
                                Console.WriteLine($"No hay suficiente stock del producto con código {pedido.codigoProducto}. " +
                                    $"Se despachan {producto.stock} unidades.");
                                info = $"No hay suficiente stock del producto con código {pedido.codigoProducto}. " +
                                    $"Se despachan {producto.stock} unidades.";
                                pedido.cantidad -= producto.stock;
                                producto.stock = 0;
                            }
                            si = true;
                        }
                       
                    }
                    producto = producto.Sgte;
                } while (producto != inventario.lista && si != true);
                pedido = pedido.Sgte;
            }
            colaAlamacen=colaAlamacen.Sgte;
        }


        public void Mostrar()
        {
            if (colaAlamacen == null)
            {
                Console.WriteLine("La cola de pedidos está vacía.");
                return;
            }

            nodoAlmacen p = colaAlamacen;
            Console.WriteLine("Cola de pedidos:");
            while (p != null)
            {
                Console.WriteLine($"Producto: {p.codigoProducto}, Cantidad Solicitada: {p.cantidad}");
                Console.WriteLine("-----------------------------------------------------------------");
                p = p.Sgte;
            }
        }
    }
}

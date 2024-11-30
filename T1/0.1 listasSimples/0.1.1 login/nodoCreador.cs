using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1_Gestor_Medico_de_Referencias.T1._0._1_listasSimples._0._1._1_login
{
    // segunda lista simple para el inicio de sesion 
    public class nodoCreador
    {
        public string Usuario { get; set; } //VARIABLE DE NOMBRE DE USUARIO
        public string Contraseña { get; set; }  //VARIABLE DE LA CONTRASENA DE USUARIO
        public nodoCreador Siguiente { get; set; } //puntero
        public string Token { get; set; }

        public nodoCreador(string usuario, string contraseña) //CONSTRUCTOR PARA EL NODOCREADOR
        {
            Usuario = usuario;
            Contraseña = contraseña;
            Siguiente = null;
            Token = null;
        }
    }
}

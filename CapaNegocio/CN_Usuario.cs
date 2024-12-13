using CapaDatos;
using CapaEntitades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objcd_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objcd_usuario.Listar();
        }

        public int Registrar(Usuario usuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (usuario.Documento == string.Empty)
                Mensaje += "Es necesario el documento del usuario\n";

            if (usuario.NombreCompleto == string.Empty)
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (usuario.Clave == string.Empty)
                Mensaje += "Es necesaria la clave del usuario\n";


            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_usuario.Registrar(usuario, out Mensaje);

            }
        }

        public bool Editar(Usuario usuario, out string Mensaje) 
        {
            Mensaje = string.Empty;

            if (usuario.Documento == string.Empty)
                Mensaje += "Es necesario el documento del usuario\n";

            if (usuario.NombreCompleto == string.Empty)
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (usuario.Clave == string.Empty)
                Mensaje += "Es necesaria la clave del usuario\n";


            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_usuario.Editar(usuario, out Mensaje);
            }
        }

        public bool Eliminar(Usuario usuario,out string Mensaje)
        {
            return objcd_usuario.Eliminar(usuario, out Mensaje);
        }
    }
}

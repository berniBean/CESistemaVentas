using CapaDatos;
using CapaEntitades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_Permiso objcd_usuario = new CD_Permiso();

        public List<Permiso> Listar(int idUsuario)
        {
            return objcd_usuario.Listar(idUsuario);
        }
    }
}

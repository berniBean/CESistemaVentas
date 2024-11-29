using CapaDatos;
using CapaEntitades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Rol
    {
        private CD_Rol objcd_Rol = new CD_Rol();

        public List<Rol> Listar()
        {
            return objcd_Rol.Listar();
        }
    }
}

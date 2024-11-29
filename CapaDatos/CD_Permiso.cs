using CapaEntitades;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Text;

namespace CapaDatos
{
    public class CD_Permiso
    {
        public List<Permiso> Listar(int idUsuario)
        {
            List<Permiso> usuarios = new List<Permiso>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Idrol,p.NombreMenu from PERMISO p ");
                    query.AppendLine(" inner join ROL r on r.IdRol = p.IdRol");
                    query.AppendLine(" inner join USUARIO u on u.IdRol = r.IdRol");
                    query.AppendLine(" where u.IdUsuario = @idUsuario");


                    SqlCommand cmd = new SqlCommand(query.ToString(), sqlConnection);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.CommandType = CommandType.Text;
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Permiso
                            {
                                oRol = new Rol() { IdRol = Convert.ToInt32(reader["IdRol"]) },
                                NombreMenu = reader["NombreMenu"].ToString()

                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                    usuarios = new List<Permiso>();
                }
            }
            return usuarios;
        }
    }
}

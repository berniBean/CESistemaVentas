using CapaEntitades;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System;

namespace CapaDatos
{
    public class CD_Rol
    {
        public List<Rol> Listar()
        {
            List<Rol> usuarios = new List<Rol>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRol,Descripcion from Rol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Rol
                            {
                                IdRol = Convert.ToInt32(reader["IdRol"]),
                                Descripcion = reader["Descripcion"].ToString()

                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                    usuarios = new List<Rol>();
                }
            }
            return usuarios;
        }
    }
}

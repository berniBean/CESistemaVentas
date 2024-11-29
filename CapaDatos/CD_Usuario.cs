using CapaEntitades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IdRol,r.Descripcion from usuario u");
                    query.AppendLine(" inner join ROL r on r.IdRol = u.IdRol");

                    //string query = "select IdUsuario,Documento,NombreCompleto,Correo,Clave,Estado from usuario";

                    SqlCommand cmd = new SqlCommand(query.ToString(), sqlConnection);
                    cmd.CommandType = CommandType.Text;
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while (reader.Read()) 
                        {
                            usuarios.Add(new Usuario 
                            { 
                                IdUsuario = Convert.ToInt32( reader["IdUsuario"]),
                                Documento = Convert.ToString(reader["Documento"]),
                                NombreCompleto = Convert.ToString(reader["NombreCompleto"]),
                                Correo= Convert.ToString(reader["Correo"]),
                                Clave = Convert.ToString(reader["Clave"]),
                                Estado= Convert.ToBoolean(reader["Estado"]),
                                oRol = new Rol() { 
                                    IdRol =(Int32)reader["IdRol"], 
                                    Descripcion = reader["Descripcion"].ToString() 
                                }

                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                    usuarios = new List<Usuario>();
                }
            }
            return usuarios;
        }
    }
}

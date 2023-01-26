using System.Data;
using MEDISMART.Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace MEDISMART.Data
{
    public class DbContext
    {
        public DbContext(string valor) => Valor = valor;
        public string Valor { get; }
    }

    /*
    //OBTENER LA LISTA DE NUESTROS CONTACTOS
    public List<UsuarioModel> Listar()
    {
        var oLista = new List<UsuarioModel>();

        //REFERENCIA A LAS LISTA
        var cn = new Conexion();
        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            //LEER EL RESULTADO DEL PROCEDIMIENTO ALMACENADO
            using (var dr = cmd.ExecuteReader())
            {
                //LEEMOS CADA UNO DE LOS REGISTROS DE SP_LISTAR
                while (dr.Read())
                {
                    oLista.Add(new UsuarioModel()
                    {
                        Nombre = dr["Nombre"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                    }) ; 
                }
            }
        }
        return oLista;
    }
    */
}

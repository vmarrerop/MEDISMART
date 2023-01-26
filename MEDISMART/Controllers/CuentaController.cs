using MEDISMART.Data;
using MEDISMART.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace MVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly DbContext _contexto;

        public CuentaController(DbContext contexto)
        {
            _contexto = contexto;
        }

        public ActionResult Registrar()
        {
            return View("Registrar");
        }

        [HttpPost]
        public ActionResult Registrar(UsuarioModel u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new(_contexto.Valor))
                    {
                        using (SqlCommand cmd = new("sp_registrar", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = u.Nombre;
                            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = u.Direccion;
                            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = u.Edad;
                            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = u.Usuario;
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = u.Clave;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return View("Registrar");
            }
            ViewData["error"] = "Error de credenciales";
            return View("Registrar");
        }


        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new(_contexto.Valor))
                    {
                        using (SqlCommand cmd = new("sp_login", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = login.Usuario;
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = login.Clave;
                            con.Open();

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                Response.Cookies.Append("user", "Bienvenido " + login.Usuario);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewData["error"] = "Error de credenciales";
                            }

                            con.Close();
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                return View("Login");
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }

        /*
        //COMPRABACIÓN PARA USUARIOS DUPLICADOS
        private void addNewAppName(string appName)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_registrar", cnx);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = appName;
                try
                {
                    cnx.Open();
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Application name already exists!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Record has been succed!!!!", "Insertion done!!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch
                {
                    MessageBox.Show("Has been getting error, try it again later.", "Connection error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cnx.State == ConnectionState.Open) { cnx.Close(); }
                }
            }
        }
        */
    }
}

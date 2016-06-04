using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTokaPixka.Controllers
{
    //Clase para recuperar informacion de la base de datos
    public class Lectura {
        public int Id { get; set; }
        public string Valor { get; set; }
        public string EstatusText { get; set; }
        public string EstatusMod { get; set; }
        public string TimestampLectura { get; set; }
        public string TimestampLocalServer { get; set; }
        public string Especie { get; set; }
        public string Ruta { get; set; }
        public Exception Ex { get; set; }
    }

    public class MiCosechaController : Controller
    {




        // GET: MiCosecha
        public ActionResult Index()
        {
            ViewBag.ListLect = obtenerLectura();
            return View();
        }

        //Obtener lecturas diarias
        private List<Lectura> obtenerLectura() {
            List<Lectura> listLec = new List<Lectura>();
            try
            {
                using (SqlConnection cnx = new SqlConnection("Server=sqlex-instance1.cfictah90jaj.us-west-2.rds.amazonaws.com,1433; Database=toka; Trusted_Connection=false; User id=admin; Password=qwerty123;"))
                {
                    cnx.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_Obtener_Lecturas", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listLec.Add(new Lectura
                                {
                                    Id = int.Parse(reader["Id"].ToString()),
                                    Valor = reader["Valor"].ToString(),
                                    EstatusText = reader["EstatusText"].ToString(),
                                    TimestampLectura = reader["TimestampLectura"].ToString(),
                                    EstatusMod = reader["EstatusMod"].ToString(),
                                    Especie = reader["Especie"].ToString(),
                                    Ruta = reader["Ruta"].ToString()
                                });
                            }
                        }
                        //lstConceptos.Add(new Concepto {
                        //    Descripcion = Convert.ToString(cmd.ExecuteNonQuery())
                        //});
                    }
                    cnx.Close();
                }
                return listLec;
            }
            catch (Exception ex)
            {
                listLec.Add(new Lectura { Ex = ex });
                return listLec;
            } 
        }


        ////recuperar prueba
         
        ////             using (SqlConnection conn = new SqlConnection())
        ////    {
        ////        // Create the connectionString
        ////        // Trusted_Connection is used to denote the connection uses Windows Authentication
        ////        //conn.ConnectionString = "Server=[server_name];Database=[database_name];Trusted_Connection=true";

        ////        conn.ConnectionString = "Server=sqlex-instance1.cfictah90jaj.us-west-2.rds.amazonaws.com,1433; Database=toka; Trusted_Connection=false; User id=admin; Password=qwerty123;";

        ////        conn.Open();
        ////        // Create the command
        ////        //SqlCommand command = new SqlCommand("SELECT * FROM dbo.LecturasDiarias WHERE Valor = @0", conn);
        ////        SqlCommand command = new SqlCommand("SELECT * FROM dbo.LecturasDiarias", conn);
        ////        // Add the parameters.
        ////        //command.Parameters.Add(new SqlParameter("0", 1));

        ////        /* Get the rows and display on the screen! 
        ////         * This section of the code has the basic code
        ////         * that will display the content from the Database Table
        ////         * on the screen using an SqlDataReader. */
        ////        //recuperamos informacion desde la base
        ////        using (SqlDataReader reader = command.ExecuteReader())
        ////        {
        ////            Console.WriteLine("Id\tValor\t\tEstatusText\t\tTimestampLectura\t\tTimestampLocalServer\t");
        ////            while (reader.Read())
        ////            {
        ////                Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t {4}",
        ////                    reader[0], reader[1], reader[2], reader[3], reader[4]));
        ////            }
        ////        }
        ////        //Console.WriteLine("Data displayed! Now press enter to move to the next section!");
        ////        Console.ReadLine();
        ////    }



    }
}
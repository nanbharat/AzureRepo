using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;


namespace StorageCrudApp.Controllers
{
    public class AzureSqlController : Controller
    {


        // GET: AzureSql
        private  readonly string conn;


        public AzureSqlController()
        {
           conn = ConfigurationManager.AppSettings.Get("AzureSqlConn");
        }

        public ActionResult Index()
        {
            List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
            using(SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    lstEmployee.Add(
                        new EmployeeModel()
                        {
                            Id = Convert.ToInt32(dr[0]),
                            Name = dr[1].ToString()
                        });
                }
                
            }

            return View(lstEmployee);
        }


        public ActionResult Create(int Id)
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                if (Id == 0)
                {
                    return View(model);
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_GetEmployeeById", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            model = new EmployeeModel()
                            {
                                Id = Convert.ToInt32(dr[0]),
                                Name = dr[1].ToString()
                            };
                        }

                    }
                    return View(model);
                }

            }
            catch( Exception ex )
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult Create( EmployeeModel model )
        {
            try
            {
                if (model != null && model.Id == 0)
                {
                    GenricUpdateInsert(model,"SP_InsertEmployee");
                }
                else
                {
                    GenricUpdateInsert(model, "SP_UpdateEmployee");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int Id)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                 


                    cmd.ExecuteNonQuery();
                }



                return RedirectToAction("Index");
            }
            catch( Exception ex)
            {
                throw ex;
            }

        }


        private void GenricUpdateInsert(EmployeeModel model,string procName)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(procName, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Name", model.Name);


                cmd.ExecuteNonQuery();
            }

        }





    }
}
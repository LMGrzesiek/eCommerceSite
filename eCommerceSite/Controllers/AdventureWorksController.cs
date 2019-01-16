using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;

namespace CodingTemple.DotNet.ShovelStore.Controllers
{
    public class AdventureWorksController : Controller
    {
        private SqlConnection _sqlConnection;

        public AdventureWorksController(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public IActionResult Index(int? id)
        {
            Dictionary<int, string> states = new Dictionary<int, string>();
            Models.AdventureWorks.TopSaleReport report = new Models.AdventureWorks.TopSaleReport();
            try
            {

                _sqlConnection.Open();
                using (SqlCommand command = _sqlConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM vw_states";

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        int namePosition = dataReader.GetOrdinal("Name");
                        int idPosition = dataReader.GetOrdinal("StateProvinceID");

                        while (dataReader.Read())
                        {
                            int stateProvinceId = dataReader.GetInt32(idPosition);
                            string name = dataReader.GetString(namePosition);
                            states.Add(stateProvinceId, name);
                        }
                        dataReader.Close();
                    }
                }
                if (id != null)
                {
                    using (SqlCommand reportCommand = _sqlConnection.CreateCommand())
                    {
                        reportCommand.CommandText = "sp_TopProductForState";
                        reportCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        reportCommand.Parameters.AddWithValue("@StateProvince", id.Value);

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(reportCommand))
                        {

                            DataSet ds = new System.Data.DataSet();
                            dataAdapter.Fill(ds);

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                report.TopSaleByQuantities.Add(new Models.AdventureWorks.TopSaleByQuantity
                                {
                                    OrderQuantity = int.Parse(row[0].ToString()),
                                    Name = row[1].ToString(),
                                    Description = row[2].ToString()

                                });
                            }

                            foreach (DataRow row in ds.Tables[1].Rows)
                            {

                                report.TopSaleByDollars.Add(new Models.AdventureWorks.TopSaleByDollar
                                {
                                    TotalMoney = decimal.Parse(row[0].ToString()),
                                    Name = row[1].ToString(),
                                    Description = row[2].ToString()

                                });
                            }
                        }
                    }
                }
                _sqlConnection.Close();

            }
            catch (Exception ex)
            {
                //TODO: Log the exception somewhere and notify the system administrator so they can figure out why this broke.
            }

            ViewBag.states = states;

            return View(report);
        }
    }
}
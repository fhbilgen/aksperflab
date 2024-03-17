using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;

namespace webAPP.Pages
{
    public class get_citiesModel : PageModel
    {
        public IList<City>? Cities { get; set; }
        public void OnGet()
        {
            Cities = GetData();
        }

        public List<City> GetData()
        {
            List<City> cities = new List<City>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                
                builder.DataSource = Environment.GetEnvironmentVariable("SQLSERVERNAME");
                builder.InitialCatalog = Environment.GetEnvironmentVariable("SQLSERVERDBNAME");
                builder.UserID = Environment.GetEnvironmentVariable("SQLUSERNAME");
                builder.Password = Environment.GetEnvironmentVariable("SQLPASSWORD");                

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to SQL Server");
                    string sql = "SELECT name, callcode, trafficcode FROM dbo.cities";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Reading data from table");
                            Console.WriteLine($"{reader.RecordsAffected.ToString()} cities are retrieved");
                            while (reader.Read())
                            {
                                var cname = reader["name"].ToString();
                                int ccode, tcode;
                                int.TryParse(reader["callcode"].ToString(), out ccode);
                                int.TryParse(reader["trafficcode"].ToString(), out tcode);
                                cities.Add(new City(cname, ccode, tcode));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

            return cities;
        }

    }
}

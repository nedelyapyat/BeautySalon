using BeautySalon.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Repositories
{
    namespace BeautySalon.Repositories
    {
        public class ServiceRepository : DbRepository
        {
            public ServiceRepository(string connectionString) : base(connectionString) { }

            public List<Service> GetAllServices()
            {
                var services = new List<Service>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Услуги", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                services.Add(new Service
                                {
                                    ServiceId = Convert.ToInt32(reader["Код_услуги"]),
                                    Name = reader["Наименование"].ToString(),
                                    Description = reader["Описание"].ToString(),
                                    Price = Convert.ToDecimal(reader["Стоимость"])
                                });
                            }
                        }
                    }
                }
                return services;
            }
        }
    }
}
using BeautySalon.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Repositories
{
    public class MasterRepository : DbRepository
    {
        public MasterRepository(string connectionString) : base(connectionString) { }

        public List<Master> GetAllMasters()
        {
            var masters = new List<Master>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Мастер", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            masters.Add(new Master
                            {
                                MasterId = Convert.ToInt32(reader["Код_мастера"]),
                                FullName = reader["ФИО_мастера"].ToString(),
                                Phone = reader["Телефон_мастера"].ToString(),
                                Address = reader["Адрес_мастера"].ToString()
                            });
                        }
                    }
                }
            }
            return masters;
        }
    }
}
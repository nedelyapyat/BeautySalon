using BeautySalon.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Repositories
{
    public class ClientRepository : DbRepository
    {
        public ClientRepository(string connectionString) : base(connectionString) { }

        public List<Client> GetAllClients()
        {
            var clients = new List<Client>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Клиент", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                ClientId = Convert.ToInt32(reader["Код_клиента"]),
                                FullName = reader["ФИО_клиента"].ToString(),
                                Address = reader["Адрес"].ToString(),
                                Phone = reader["Телефон"].ToString()
                            });
                        }
                    }
                }
            }
            return clients;
        }
    }
}

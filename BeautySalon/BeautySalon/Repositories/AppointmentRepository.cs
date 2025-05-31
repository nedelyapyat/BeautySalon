using BeautySalon.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Repositories
{
    public class AppointmentRepository : DbRepository
    {
        public AppointmentRepository(string connectionString) : base(connectionString) { }

        public List<Appointment> GetAllAppointments()
        {
            var appointments = new List<Appointment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Записи", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                AppointmentDate = Convert.ToDateTime(reader["Дата_записи"]),
                                ClientId = Convert.ToInt32(reader["Код_клиента"]),
                                MasterId = Convert.ToInt32(reader["Код_мастера"]),
                                ServiceId = Convert.ToInt32(reader["Код_услуги"]),
                                Status = reader["Статус"].ToString(),
                                Comment = reader["Комментарий"].ToString()
                            });
                        }
                    }
                }
            }
            return appointments;
        }

        public void AddAppointment(Appointment appointment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(
                    "INSERT INTO Записи (Дата_записи, Код_клиента, Код_мастера, Код_услуги, Статус, Комментарий) " +
                    "VALUES (@Date, @ClientId, @MasterId, @ServiceId, @Status, @Comment)", connection))
                {
                    command.Parameters.AddWithValue("@Date", appointment.AppointmentDate);
                    command.Parameters.AddWithValue("@ClientId", appointment.ClientId);
                    command.Parameters.AddWithValue("@MasterId", appointment.MasterId);
                    command.Parameters.AddWithValue("@ServiceId", appointment.ServiceId);
                    command.Parameters.AddWithValue("@Status", appointment.Status);
                    command.Parameters.AddWithValue("@Comment", appointment.Comment ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
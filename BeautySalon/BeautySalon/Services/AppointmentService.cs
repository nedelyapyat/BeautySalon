using BeautySalon.Models;
using BeautySalon.Repositories;
using BeautySalon.Repositories.BeautySalon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepo;
        private readonly MasterRepository _masterRepo;
        private readonly ClientRepository _clientRepo;
        private readonly ServiceRepository _serviceRepo;

        public AppointmentService(
            AppointmentRepository appointmentRepo,
            MasterRepository masterRepo,
            ClientRepository clientRepo,
            ServiceRepository serviceRepo)
        {
            _appointmentRepo = appointmentRepo;
            _masterRepo = masterRepo;
            _clientRepo = clientRepo;
            _serviceRepo = serviceRepo;
        }

        public bool IsMasterAvailable(int masterId, DateTime date)
        {
            var appointments = _appointmentRepo.GetAllAppointments()
                .Where(a => a.MasterId == masterId &&
                           a.AppointmentDate.Date == date.Date &&
                           a.Status != "Отменено")
                .ToList();

            return !appointments.Any(a => a.AppointmentDate.TimeOfDay == date.TimeOfDay);
        }

        public void CreateAppointment(Appointment appointment)
        {
            if (!IsMasterAvailable(appointment.MasterId, appointment.AppointmentDate))
            {
                throw new Exception("Мастер уже занят в это время!");
            }

            _appointmentRepo.AddAppointment(appointment);
        }

        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return _appointmentRepo.GetAllAppointments()
                .Where(a => a.AppointmentDate.Date == date.Date)
                .ToList();
        }
    }
}

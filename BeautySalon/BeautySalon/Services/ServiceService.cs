using BeautySalon.Models;
using BeautySalon.Repositories.BeautySalon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Services
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;

        public ServiceService(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public List<Service> GetAllServices()
        {
            return _serviceRepository.GetAllServices();
        }
    }
}

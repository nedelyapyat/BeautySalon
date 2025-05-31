using BeautySalon.Models;
using BeautySalon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Services
{
    public class MasterService
    {
        private readonly MasterRepository _masterRepository;

        public MasterService(MasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public List<Master> GetAllMasters()
        {
            return _masterRepository.GetAllMasters();
        }
    }
}


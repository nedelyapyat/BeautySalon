using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Repositories
{
    public abstract class DbRepository
    {
        protected readonly string _connectionString;

        public DbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

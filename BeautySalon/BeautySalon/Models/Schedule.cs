using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan Time { get; set; }
        public int MasterId { get; set; }
    }
}

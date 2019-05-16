using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeOff.Data
{
 
    public class Test
    {
        public TimeSheet d { get; set; }
    }

    public class TimeSheet
    {
        public List<TimeSheetDetails> results { get; set; }
    }
    public class TimeSheetDetails
    {
        public string startDate { get; set; }
        public string approvalStatus { get; set; }

        public string endDate { get; set; }

        public string timeType { get; set; }
    }
}

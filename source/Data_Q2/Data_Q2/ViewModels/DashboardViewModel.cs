using Data_Q2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_Q2.ViewModels
{
    public class DashboardViewModel
    {
        public string FileName { get; set; }
        public string TicketSum { get; set; }
        public string AvgRspsTime { get; set; }
        public IEnumerable<DashboardTicketData> TicketDataList { get; set; }
    }
    public class DashboardTicketData : TicketData
    {
       
    }
}

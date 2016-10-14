using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketSystem.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public String CustomerResponse { get; set; }
        public String EmployeeResponse { get; set; }
        public string ScheduledDay { get; set; }
        public string ScheduledTime { get; set; }
    }
}
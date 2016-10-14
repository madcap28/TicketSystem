namespace TicketSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ticket
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public string CustomerResponse { get; set; }

        public string EmployeeResponse { get; set; }

        public string ScheduledTime { get; set; }

        public string ScheduledDay { get; set; }
    }
}

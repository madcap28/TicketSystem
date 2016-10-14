using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketSystem.Models;

namespace TicketSystem.Controllers
{
    public class TicketController : Controller
    {
        private DateTime _returnDate = DateTime.Now;
        private TicketContext1 _dbContext;
        public TicketController()
        {
            _dbContext = new TicketContext1();
        }
        // GET: Ticket
        public ActionResult Index()
        {
            if (User.IsInRole("Customer"))
            {
                var tickets = _dbContext.Tickets.ToList();
                List<Ticket> myTickets = new List<Ticket>();
                foreach (Ticket ticket in tickets)
                {
                    if (ticket.Email == User.Identity.Name)
                    {
                        myTickets.Add(ticket);
                    }
                }
                return View(myTickets);
            }
            if (User.IsInRole("Employee") || User.IsInRole("Admin"))
            {
                var tickets = _dbContext.Tickets.ToList();
                List<Ticket> myTickets = new List<Ticket>();
                foreach (Ticket ticket in tickets)
                {
                    if (ticket.Status.Equals("Open"))
                    {
                        myTickets.Add(ticket);
                    }
                }
                return View(myTickets);
            }
            return View();
        }
        public ActionResult Search(string id)
        {
            string SearchString = id;
                if (User.IsInRole("Customer"))
                {
                    var tickets = _dbContext.Tickets.ToList();
                    List<Ticket> myTickets = new List<Ticket>();
                    foreach (Ticket ticket in tickets)
                    {
                        if (ticket.Email == User.Identity.Name && ticket.Description.Contains(SearchString))
                        {
                            myTickets.Add(ticket);
                        }
                    }
                    return View(myTickets);
                }
                if (User.IsInRole("Employee") || User.IsInRole("Admin"))
                {
                    var tickets = _dbContext.Tickets.ToList();
                    List<Ticket> myTickets = new List<Ticket>();
                    foreach (Ticket ticket in tickets)
                    {
                        if (ticket.Status.Equals("Open") && ticket.Description.Contains(SearchString))
                        {
                            myTickets.Add(ticket);
                        }
                    }
                    return View(myTickets);
                }
                return View();
            
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Add(Ticket ticket)
        {
            if (User.Identity.Name != "")
            {
                ticket.Date = _returnDate;
                ticket.Email = User.Identity.Name;
                ticket.Status = "Open";
                _dbContext.Tickets.Add(ticket);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.ID == id);
            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }
        [HttpPost]
        public ActionResult Update(Ticket ticket)
        {
            var ticketInDB = _dbContext.Tickets.SingleOrDefault(v => v.ID == ticket.ID);

            if (ticketInDB == null)
                return HttpNotFound();

            ticketInDB.Title = ticket.Title;
            ticketInDB.Description = ticket.Description;
            if (ticket.CustomerResponse != null)
            {
                ticketInDB.CustomerResponse = ticket.CustomerResponse;
            }
            if (ticket.EmployeeResponse != null)
            {
                ticketInDB.EmployeeResponse = ticket.EmployeeResponse;
            }
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.ID == id);

            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }
        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.ID == id);

            if (ticket == null)
                return HttpNotFound();

            _dbContext.Tickets.Remove(ticket);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult TicketDetails(int id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.ID == id);

            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }
        public ActionResult CloseTicket(int id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.ID == id);

            if (ticket == null)
                return HttpNotFound();

            ticket.Status = "Closed";
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Schedule(int id)
        {
            var ticket = _dbContext.Tickets.SingleOrDefault(v => v.ID == id);

            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }
        
        public ActionResult SetSchedule(Ticket ticket)
        {
            var ticketInDB = _dbContext.Tickets.SingleOrDefault(v => v.ID == ticket.ID);

            if (ticketInDB == null)
                return HttpNotFound();

            ticketInDB.ScheduledDay = ticket.ScheduledDay;
            ticketInDB.ScheduledTime = ticket.ScheduledTime;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
       
       
    }
}
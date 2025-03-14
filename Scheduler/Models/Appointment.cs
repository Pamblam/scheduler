using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models {
    internal class Appointment {

        public int appointmentId { get; set; }

        public Customer customer { get; set; }

        public Scheduler.Models.User user { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string location { get; set; }

        public string contact { get; set; }

        public string type { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public DateTime createDate { get; set; }

        public string createdBy { get; set; }

        public DateTime lastUpdate { get; set; }

        public string lastUpdateBy { get; set; }

        public Appointment(int appointmentId, Customer customer, Scheduler.Models.User user, string title, string description, string location, string contact, string type, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy) { 
            this.appointmentId = appointmentId;
            this.customer = customer;
            this.user = user;
            this.title = title;
            this.description = description;
            this.location = location;
            this.contact = contact;
            this.type = type;
            this.start = start;
            this.end = end;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }

    }
}

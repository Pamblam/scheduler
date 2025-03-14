using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models {
    internal class Customer {
        public int customerId { get; set; }

        public string customerName { get; set; }

        public Address address { get; set; }

        public bool active { get; set; }

        public DateTime createDate { get; set; }

        public string createdBy { get; set; }

        public DateTime lastUpdate { get; set; }

        public string lastUpdateBy { get; set; }

        public Customer(int customerId, string customerName, Address address, bool active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy) {
            this.customerId = customerId;
            this.customerName = customerName;
            this.address = address;
            this.active = active;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }
    }
}

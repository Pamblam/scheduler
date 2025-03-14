using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models {
    internal class Address {
        public int addressId { get; set; }

        public string address { get; set; }

        public string address2 { get; set; }

        public City city { get; set; }

        public string postalCode { get; set; }

        public string phone { get; set; }

        public DateTime createDate { get; set; }

        public string createdBy { get; set; }

        public DateTime lastUpdate { get; set; }

        public string lastUpdateBy { get; set; }

        public Address(int addressId, string address, string address2, City city, string postalCode, string phone, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy) {
            this.addressId = addressId;
            this.address = address;
            this.address2 = address2;
            this.city = city;
            this.postalCode = postalCode;
            this.phone = phone;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }
    }
}

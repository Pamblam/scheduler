using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models {
    internal class Country {

        public int countryId { get; set; }

        public string country { get; set; }

        public DateTime createDate { get; set; }

        public string createdBy { get; set; }

        public DateTime lastUpdate { get; set; }

        public string lastUpdateBy { get; set; }

        public Country(int countryId, string country, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy) {
            this.countryId = countryId;
            this.country = country;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }

    }
}

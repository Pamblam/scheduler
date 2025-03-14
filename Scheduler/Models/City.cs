using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models {
    internal class City {
        public int cityId { get; set; }

        public string city { get; set; }

        public Country country { get; set; }

        public DateTime createDate { get; set; }

        public string createdBy { get; set; }

        public DateTime lastUpdate { get; set; }

        public string lastUpdateBy { get; set; }

        public City(int cityId, string city, Country country, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy) {
            this.cityId = cityId;
            this.city = city;
            this.country = country;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }
    }
}

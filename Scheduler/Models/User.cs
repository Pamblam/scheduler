using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models {
    internal class User {

        public int userId { get; set; }

        public string userName { get; set; }

        public bool active { get; set; }

        public DateTime createDate { get; set; }

        public string createdBy { get; set; }

        public DateTime lastUpdate { get; set; }

        public string lastUpdateBy { get; set; }

        public User(int userId, string userName, bool active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy) { 
            this.userId = userId;
            this.userName = userName;
            this.active = active;
            this.createDate = createDate;
            this.createdBy = createdBy;
            this.lastUpdate = lastUpdate;
            this.lastUpdateBy = lastUpdateBy;
        }
    }
}

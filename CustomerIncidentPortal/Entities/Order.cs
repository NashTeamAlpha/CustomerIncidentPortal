using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIncidentPortal.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string DateCompleted { get; set; }
        public int CustomerId { get; set; }
    }
}

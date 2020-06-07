using System;
using System.Collections.Generic;

namespace KursDb.Model.Tables
{
    public class Order
    {
        public int Id { get; set; }
        public string Sum { get; set; }
        public DateTime Date { get; set; }
        public int HoursComplet { get; set; }

        public virtual ICollection<ModelInOrder> ModelInOrders { get; set; }
    }
}

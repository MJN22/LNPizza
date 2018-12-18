using System;
using System.Collections.Generic;

namespace LNPizza.DataAccess
{
    public partial class OrderedPizzas
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int OrderId1 { get; set; }

        public virtual Pizza IdNavigation { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}

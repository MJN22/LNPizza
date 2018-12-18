using System;
using System.Collections.Generic;

namespace LNPizza.DataAccess
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrderedPizzasIdNavigation = new HashSet<OrderedPizzas>();
            OrderedPizzasPizza = new HashSet<OrderedPizzas>();
            PizzaIngredients = new HashSet<PizzaIngredients>();
        }

        public int Id { get; set; }
        public string PizzaName { get; set; }
        public decimal Costs { get; set; }

        public virtual ICollection<OrderedPizzas> OrderedPizzasIdNavigation { get; set; }
        public virtual ICollection<OrderedPizzas> OrderedPizzasPizza { get; set; }
        public virtual ICollection<PizzaIngredients> PizzaIngredients { get; set; }
    }
}

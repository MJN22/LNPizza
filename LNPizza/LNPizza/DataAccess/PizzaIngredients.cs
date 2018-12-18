using System;
using System.Collections.Generic;

namespace LNPizza.DataAccess
{
    public partial class PizzaIngredients
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string IngredientName { get; set; }
        public int IngredientCost { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}

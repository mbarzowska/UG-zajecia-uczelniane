using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodParty.Models 
{
    public class Pizzeria 
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhotoURL { get; set; }

        public ICollection<PizzaSize> Sizes { get; set; }

        public override string ToString() {
            return $"({Id}) {Name}";
        }
    }
}

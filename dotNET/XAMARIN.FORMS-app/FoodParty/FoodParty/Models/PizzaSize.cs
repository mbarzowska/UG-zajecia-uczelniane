using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FoodParty.Models 
{
    public class PizzaSize 
    {
        public int Id { get; set; }

        public int Delimeter { get; set; }

        public int PizzeriaId { get; set; }

        [ForeignKey(nameof(PizzeriaId))]
        public Pizzeria Pizzeria { get; set; }

        public override string ToString() 
        {
            return $"{Delimeter}";
        }
    }
}

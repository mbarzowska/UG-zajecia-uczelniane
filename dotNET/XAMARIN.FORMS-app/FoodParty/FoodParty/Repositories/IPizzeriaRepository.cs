using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FoodParty.Models;

namespace FoodParty.Repositories 
{
    public interface IPizzeriaRepository 
    {
        Task<IEnumerable<Pizzeria>> GetPizzeriasAsync();

        Task<Pizzeria> GetPizzeriaByIdAsync(int id);

        Task<bool> AddPizzeriaAsync(Pizzeria pizzeria);

        Task<bool> UpdatePizzeriaAsync(Pizzeria pizzeria);

        Task<bool> RemovePizzeriaAsync(int id);

        Task<IEnumerable<Pizzeria>> QueryPizzeriasAsync(Func<Pizzeria, bool> predicate);
    }
}

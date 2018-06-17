using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FoodParty.Models;

namespace FoodParty.Repositories 
{
    public interface IPizzaSizeRepository 
    {
        Task<IEnumerable<PizzaSize>> GetSizesAsync();

        Task<PizzaSize> GetSizeByIdAsync(int id);

        Task<bool> AddSizeAsync(PizzaSize size);

        Task<bool> UpdateSizeAsync(PizzaSize size);

        Task<bool> RemoveSizeAsync(int id);

        Task<IEnumerable<PizzaSize>> QuerySizesAsync(Func<PizzaSize, bool> predicate);
    }
}

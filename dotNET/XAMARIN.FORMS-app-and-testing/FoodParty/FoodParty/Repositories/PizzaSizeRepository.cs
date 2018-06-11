using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodParty.Data;
using FoodParty.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms.Xaml;

namespace FoodParty.Repositories 
{
    public class PizzaSizeRepository : IPizzaSizeRepository 
    {
        private readonly FPContext _databaseContext;

        public PizzaSizeRepository(FPContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<PizzaSize>> GetSizesAsync() 
        {
            try 
            {
                var sizes = await _databaseContext.Sizes.Include(p => p.Pizzeria).ToListAsync();
                return sizes;
            } 
            catch (Exception) 
            {
                return null;
            }
        }

        public async Task<PizzaSize> GetSizeByIdAsync(int id) 
        {
            try 
            {
                var size = await _databaseContext.Sizes.Include(p => p.Pizzeria).SingleOrDefaultAsync(x => x.Id == id);
                return size;
            } 
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddSizeAsync(PizzaSize size) 
        {
            try 
            {
                var tracking = await _databaseContext.Sizes.AddAsync(size);
                var isAdded = tracking.State == EntityState.Added;

                await _databaseContext.SaveChangesAsync();
                return isAdded;
            } 
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> UpdateSizeAsync(PizzaSize size) 
        {
            try 
            {
                var tracking = _databaseContext.Sizes.Update(size);
                var isUpdated = tracking.State == EntityState.Modified;

                await _databaseContext.SaveChangesAsync();
                return isUpdated;
            } 
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> RemoveSizeAsync(int id) 
        {
            try 
            {
                var size = await _databaseContext.Sizes.FindAsync(id);

                var tracking = _databaseContext.Sizes.Remove(size);
                var isDeleted = tracking.State == EntityState.Deleted;

                await _databaseContext.SaveChangesAsync();
                return isDeleted;
            } 
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<IEnumerable<PizzaSize>> QuerySizesAsync(Func<PizzaSize, bool> predicate) 
        {
            try {
                return await Task.Run(() => _databaseContext.Sizes.Where(predicate).OrderBy(x => x.Delimeter));
            } 
            catch (Exception) 
            {
                return null;
            }
        }
    }
}

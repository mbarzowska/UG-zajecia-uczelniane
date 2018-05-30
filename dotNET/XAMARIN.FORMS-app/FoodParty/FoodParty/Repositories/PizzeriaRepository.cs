using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodParty.Data;
using FoodParty.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodParty.Repositories 
{
    public class PizzeriaRepository : IPizzeriaRepository 
    {
        private readonly FPContext _databaseContext;

        public PizzeriaRepository(FPContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Pizzeria>> GetPizzeriasAsync() 
        {
            try 
            {
                var pizzerias = await _databaseContext.Pizzerias.Include(p => p.Sizes).ToListAsync();
                return pizzerias;
            } 
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Pizzeria> GetPizzeriaByIdAsync(int id) 
        {
            try 
            {
                var pizzeria = await _databaseContext.Pizzerias.Include(p => p.Sizes).SingleOrDefaultAsync(x => x.Id == id);
                return pizzeria;
            } 
            catch (Exception) 
            {
                return null;
            }
        }

        public async Task<bool> AddPizzeriaAsync(Pizzeria pizzeria) 
        {
            try 
            {
                var tracking = await _databaseContext.Pizzerias.AddAsync(pizzeria);
                var isAdded = tracking.State == EntityState.Added;
                await _databaseContext.SaveChangesAsync();
                return isAdded;
            } 
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> UpdatePizzeriaAsync(Pizzeria pizzeria) 
        {
            try 
            {
                var tracking = _databaseContext.Pizzerias.Update(pizzeria);
                var isUpdated = tracking.State == EntityState.Modified;
                await _databaseContext.SaveChangesAsync();
               
                return isUpdated;
            }
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> RemovePizzeriaAsync(int id) 
        {
            try 
            {
                var pizzeria = await _databaseContext.Pizzerias.FindAsync(id);

                var tracking = _databaseContext.Pizzerias.Remove(pizzeria);
                var isDeleted = tracking.State == EntityState.Deleted;
                await _databaseContext.SaveChangesAsync();
               
                return isDeleted;
            } 
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<IEnumerable<Pizzeria>> QueryPizzeriasAsync(Func<Pizzeria, bool> predicate) 
        {
            try 
            {
                var pizzerias = _databaseContext.Pizzerias.Where(predicate);
                return pizzerias.ToList();
            } 
            catch (Exception) 
            {
                return null;
            }
        }
    }
}

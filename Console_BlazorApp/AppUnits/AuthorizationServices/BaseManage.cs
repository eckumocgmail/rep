using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BaseManager<TItem>  where TItem: BaseEntity
{
 

    private DbSet<TItem> _dbSet;
    private DbContext _context;

    public BaseManager(DbSet<TItem> dbSet, DbContext dbContext)
    {
        this._dbSet = dbSet;
        this._context = dbContext;
    }


    private bool ItemExists(int id)
    {
        return this._dbSet.Any(e => e.Id == id);
    }


    [HttpDelete("{id}")]
    public virtual async Task<int> DeleteItem(int id)
    {
        var Item = await this._dbSet.FindAsync(id);
        if (Item == null)
        {
            throw new System.Exception($"Не найдена запись с идентификатором id=" + id);
        }
        else
        {
            this._dbSet.Remove(Item);
            await _context.SaveChangesAsync();
        }        
        return await _context.SaveChangesAsync();
    }

    [HttpGet("{id}")]
    public virtual async Task<TItem> GetItem(int id)
    {
        return await this._dbSet.FindAsync(id);
    }


    [HttpGet()]
    public virtual async Task<IEnumerable<TItem>> GetItems()
    {
        return await this._dbSet.ToListAsync();
    }

    [HttpPost()]
    public virtual async Task<TItem> PostItem(TItem Item)
    {
        this._dbSet.Add(Item);
        await _context.SaveChangesAsync();
        return Item;
    }

    [HttpPut("{id}")]
    public virtual async Task<int> PutItem(int id, TItem Item)
    {
        Item.Id = id;
        _context.Entry(Item).State = EntityState.Modified;
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ItemExists(id))
            {
                throw new System.ArgumentException(
                    $"Не найдена запись с идентификатором id=" + id, "id");
            }
            else
            {
                throw;
            }
        }
    }

}
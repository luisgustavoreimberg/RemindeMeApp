using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RemindeMeApp.Backend.Data;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Shared.Services;

namespace RemindeMeApp.Backend.Services;

public class TagService : ITagService
{
    private readonly ApplicationDbContext _context;

    public TagService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        return await _context.Tags.FindAsync(id);
    }

    public async Task<Tag> CreateAsync(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Tag> GetOrCreateByNameAsync(string name)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Nome == name);
        if (tag == null)
        {
            tag = new Tag { Nome = name, CorHexadecimal = "#808080" };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }
        return tag;
    }
}

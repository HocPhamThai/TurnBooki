using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TurnBooki.Business.Services.IServices;
using TurnBooki.DataAccess.Data;
using TurnBooki.Models;

namespace TurnBooki.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await this.context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await this.context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            this.context.Categories.Add(category);
            await this.context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await this.context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }
            this.context.Categories.Remove(category);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            this.context.Categories.Update(category);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryID = null)
        {
            if (categoryID.HasValue)
            {
                return !await this.context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != categoryID.Value);
            }
            else
            {
                return !await this.context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            }
        }
    }
}

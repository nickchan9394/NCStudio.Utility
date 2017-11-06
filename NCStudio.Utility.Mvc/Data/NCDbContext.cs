using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Mvc.Data
{
    public class NCDbContext : DbContext, INCDbContext
    {
        public NCDbContext() { }
        public NCDbContext(DbContextOptions options) : base(options) { }

        public void LoadNavigationProperty(EntityEntry entry, params string[] props)
        {
            foreach (var prop in props)
            {
                entry.Navigation(prop).Load();
            }
        }

        public void MarkAsDeleted<T>(T entity) where T : class
        {
            this.Entry<T>(entity).State = EntityState.Deleted;
        }
    }
}

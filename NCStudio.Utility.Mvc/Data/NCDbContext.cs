using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NCStudio.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCStudio.Utility.Mvc.Data
{
    public class NCDbContext : DbContext, INCDbContext,IUnitOfWork
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

        public async Task RunTransactionAsync(Func<Task> transactionFunc)
        {
            using (var transactionBeginTask = Database.BeginTransactionAsync())
            using (var transaction = await transactionBeginTask)
            {
                await transactionFunc();
                transaction.Commit();
            }
        }

        public async Task<TResult> RunTransactionAsync<TResult>(Func<Task<TResult>> transactionFunc)
        {
            using (var transactionBeginTask = Database.BeginTransactionAsync())
            using (var transaction = await transactionBeginTask)
            {
                var result = await transactionFunc();
                transaction.Commit();
                return result;
            }
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }
    }
}

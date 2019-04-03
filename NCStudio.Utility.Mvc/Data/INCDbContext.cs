using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NCStudio.Utility.Mvc.Data
{
    public interface INCDbContext
    {
        EntityEntry Add(object entity);
        int SaveChanges();

        void LoadNavigationProperty(EntityEntry entry, params string[] props);
        void MarkAsDeleted<T>(T entity) where T : class;
    }
}
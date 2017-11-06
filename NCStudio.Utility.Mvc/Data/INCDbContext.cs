using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NCStudio.Utility.Mvc.Data
{
    public interface INCDbContext
    {
        void LoadNavigationProperty(EntityEntry entry, params string[] props);
        void MarkAsDeleted<T>(T entity) where T : class;
    }
}
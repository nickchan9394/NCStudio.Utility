using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace NCStudio.Utility.Testing
{
    public static class Mocking
    {
        public static DbSet<T> GetMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.As<IAsyncEnumerable<T>>().Setup(d => d.GetEnumerator()).Returns(new AsyncEnumerator<T>(queryable.GetEnumerator()));
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            dbSet.Setup(d => d.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>((s) => sourceList.AddRange(s));
            dbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>((s) => sourceList.Remove(s));

            return dbSet.Object;
        }

       

        protected class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public AsyncEnumerable(Expression expression)
                : base(expression) { }

            public IAsyncEnumerator<T> GetEnumerator() =>
                new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        protected class AsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> enumerator;

            public AsyncEnumerator(IEnumerator<T> enumerator) =>
                this.enumerator = enumerator ?? throw new ArgumentNullException();

            public T Current => enumerator.Current;

            public void Dispose() { }

            public Task<bool> MoveNext(CancellationToken cancellationToken) =>
                Task.FromResult(enumerator.MoveNext());
        }
    }
}

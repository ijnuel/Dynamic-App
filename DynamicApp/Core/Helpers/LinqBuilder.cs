using System.Linq.Expressions;

namespace Core.Helpers
{
    public static class LinqBuilder
    {
        public static Expression<Func<TEntity, bool>> Or<TEntity>(this Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, bool>> newQuery)
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.OrElse(GetRewrittenQuery(query, newQuery), newQuery.Body), newQuery.Parameters);
        }
        public static Expression<Func<TEntity, bool>> And<TEntity>(this Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, bool>> newQuery)
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(GetRewrittenQuery(query, newQuery), newQuery.Body), newQuery.Parameters);
        }
        public static Expression GetRewrittenQuery<TEntity>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, bool>> newQuery)
        {
            return new ReplaceVisitor(query.Parameters[0], newQuery.Parameters[0]).Visit(query.Body);
        }
    }
    class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public ReplaceVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}

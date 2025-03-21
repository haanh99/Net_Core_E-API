using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{ 
    protected BaseSpecification() :this(null) { }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy{get; private set;}

    public Expression<Func<T, object>>? OrderByDescending {get; private set;}

    public bool IsDistinct {get; private set;}

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression){
        OrderByDescending = orderByDescExpression;
    }

    // protected void ApplyDistinct() => IsDistinct = true;

    protected void ApplyDistinct(){
        IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
 : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() :this(null) { }
    public Expression<Func<T, TResult>>? Selector {get; private set;}

    protected void AddSelector(Expression<Func<T, TResult>> selectorExpression) => Selector = selectorExpression;
}

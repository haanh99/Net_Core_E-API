using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        AddSelector(x => x.Brand);
        ApplyDistinct();
    }
}

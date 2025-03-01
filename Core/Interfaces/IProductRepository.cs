using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? type, string? brand, string? sort);
    Task<Product?> GetProductByIdAsync(int id);

    Task<IReadOnlyList<string>> GetProductTypesAsync();

    Task<IReadOnlyList<string>> GetProductBrandsAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}

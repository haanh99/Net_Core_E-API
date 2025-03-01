using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct(string? brand, string? type, string? sort)
    {
        return Ok(await repository.GetProductsAsync(brand, type, sort)); //await repository.GetProductsAsync();
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetProductByIdAsync(id);

        if (product == null) return NotFound();      
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct (Product product)
    {
        repository.AddProduct(product);

        if (await repository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }

        return BadRequest("Cannot create product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (id != product.Id || !ProductExists(id)) return BadRequest("Cannot find product");

        repository.UpdateProduct(product);

       if (await repository.SaveChangesAsync()) 
       {
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
       }

        return BadRequest("Cannot update product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        // var product = await _context.Products.FindAsync(id);
        var product = await repository.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        repository.DeleteProduct(product);

        if (await repository.SaveChangesAsync()) return NoContent();

        return BadRequest("Cannot delete product"); 
    }

    private bool ProductExists(int id)
    {
        return repository.ProductExists(id);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands(){
        return Ok(await repository.GetProductBrandsAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes(){
        return Ok(await repository.GetProductTypesAsync());
    }
}

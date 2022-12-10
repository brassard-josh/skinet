using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try
            {
                IReadOnlyList<Product> products = null;
                await Task.Run(async () => {
                    products = await _repo.GetProductsAsync();
                });

                if (products != null)
                    return Ok(products);
                else 
                    return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error retrieving list of products");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            try
            {
                Product product = null;
                await Task.Run(async () => {
                    product = await _repo.GetProductByIdAsync(id);
                });

                if (product != null)
                    return Ok(product);
                else
                    return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Error retrieving product with Id = {id}");
            }
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            try
            {
                IReadOnlyList<ProductBrand> brands = null;
                await Task.Run(async () => {
                    brands = await _repo.GetProductBrandsAsync();
                });

                if (brands != null)
                    return Ok(brands);
                else 
                    return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error retrieving list of product brands");
            }
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            try
            {
                IReadOnlyList<ProductType> types = null;
                await Task.Run(async () => {
                    types = await _repo.GetProductTypesAsync();
                });

                if (types != null)
                    return Ok(types);
                else 
                    return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error retrieving list of product types");
            }
        }
    }
}
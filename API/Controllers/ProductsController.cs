using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo, 
            IGenericRepository<ProductBrand> productBrandsRepo, 
            IGenericRepository<ProductType> productTypesRepo,
            IMapper mapper
        )
        {
            _productsRepo = productsRepo;
            _productBrandsRepo = productBrandsRepo;
            _productTypesRepo = productTypesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParams prodParams)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(prodParams);
                var countSpec = new ProductWithFiltersForCountSpecification(prodParams);
                var totalItems = await _productsRepo.CountAsync(countSpec);

                var products = await _productsRepo.ListAsync(spec);

                var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

                return Ok(new Pagination<ProductToReturnDTO>(prodParams.PageIndex, prodParams.PageSize, totalItems, data));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new ApiException(500, $"Something went wrong: {ex.Message}", ex.StackTrace.ToString()));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id) 
        {
            try
            {
                Product product = null;
                var spec = new ProductsWithTypesAndBrandsSpecification(id);

                await Task.Run(async () => {
                    product = await _productsRepo.GetEntityWithSpec(spec);
                });

                if (product != null)
                    return Ok(_mapper.Map<Product, ProductToReturnDTO>(product));
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
                    brands = await _productBrandsRepo.ListAllAsync();
                });

                if (brands != null)
                    return Ok(brands);
                else 
                    return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Error retrieving list of product brands!");
            }
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            try
            {
                IReadOnlyList<ProductType> types = null;
                await Task.Run(async () => {
                    types = await _productTypesRepo.ListAllAsync();
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
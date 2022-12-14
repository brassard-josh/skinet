using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : SpecificationBase<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams prodParams) 
            : base(x => 
                (string.IsNullOrEmpty(prodParams.Search) || x.Name.ToLower().Contains(prodParams.Search)) &&
                (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) && 
                (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            
            if (!string.IsNullOrEmpty(prodParams.Sort)) 
            {
                switch(prodParams.Sort) 
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }

            ApplyPaging(prodParams.PageSize * (prodParams.PageIndex - 1), prodParams.PageSize);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos
{
    public class ProductToReturnDTO
    {
        // public ProductToReturnDTO(Product theProduct) 
        // {
        //     if (theProduct != null) 
        //     {
        //         Id = theProduct.Id;
        //         Name = theProduct.Name;
        //         Description = theProduct.Description;
        //         Price = theProduct.Price;
        //         PictureUrl = theProduct.PictureUrl;
        //         ProductBrand = theProduct.ProductBrand.Name;
        //         ProductType = theProduct.ProductType.Name;
        //     }
        // }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}
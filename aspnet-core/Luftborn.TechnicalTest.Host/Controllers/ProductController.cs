using Luftborn.TechnicalTest.Controllers;
using Luftborn.TechnicalTest.Domain.Repositories;
using Luftborn.TechnicalTest.Products;
using Luftborn.TechnicalTest.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luftborn.TechnicalTest.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductController : TechnicalTestControllerBase
    {
        private readonly IRepository<Product, int> _productRepository;

        public ProductController(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAll()
        {
            return await _productRepository.GetAll().Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                AvailableQuantities = product.AvailableQuantities,
                IsActive = product.IsActive
            }).ToListAsync();
        }

        [HttpGet]
        public async Task<ProductDto> GetProductForEdit(int productId)
        {
            var product = await _productRepository.FirstOrDefaultAsync(productId);
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                AvailableQuantities = product.AvailableQuantities,
                IsActive = product.IsActive
            };
        }

        [HttpPost]
        public async Task CreateOrUpdate([FromBody]ProductDto product)
        {
            if (!product.Id.HasValue)
            {
                await Create(product);
            }
            else
            {
                await Update(product);
            }
        }

        protected virtual async Task Create(ProductDto product)
        {
            await _productRepository.InsertAsync(new Product 
            {
                Name = product.Name,
                Description = product.Description,
                AvailableQuantities = product.AvailableQuantities,
                IsActive = product.IsActive
            });
        }
        protected virtual async Task Update(ProductDto product)
        {
            var currentProduct = await _productRepository.FirstOrDefaultAsync(product.Id.Value);

            currentProduct.Name = product.Name;
            currentProduct.Description = product.Description;
            currentProduct.AvailableQuantities = product.AvailableQuantities;
            currentProduct.IsActive = product.IsActive;

            await _productRepository.UpdateAsync(currentProduct);
        }

        [HttpDelete]
        public async Task Delete(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }

    }
}

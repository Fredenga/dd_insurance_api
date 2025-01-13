using InsuranceAPI.Data;
using InsuranceAPI.DTOs;
using InsuranceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Repository
{
    public class InsuranceProductRepository(InsuranceDbContext context)
    {
        public async Task<InsuranceProduct> CreateInsuranceProductAsync(InsuranceProductDTO productDTO)
        {
            
            var check = await context.InsuranceProducts.FirstOrDefaultAsync(p => p.ProductName.ToLower().Equals(productDTO.ProductName));
            if(check is not null)
            {
                throw new Exception("Insurance Product already exists!");
            }

            var product = context.InsuranceProducts.Add(new InsuranceProduct
            {
                ProductName = productDTO.ProductName!,
                Category = productDTO.Category!,
                BasePremium = productDTO.BasePremium,
                Description = productDTO.Description,
            }).Entity;

            await context.SaveChangesAsync();
            return product;
        }
        public async Task<IEnumerable<InsuranceProduct>> GetAllInsuranceProductsAsync()
        {
            var products = await context.InsuranceProducts.ToListAsync();
            return products is not null ? products : null!;
        }

        public async Task<InsuranceProduct> GetInsuranceProductAsyncById(int id)
        {
            var product = await context.InsuranceProducts.FirstOrDefaultAsync(p => p.ProductId == id);
            return product is not null ? product : null!;
        }

        public async Task<InsuranceProduct> UpdateInsuranceProductAsync(int id, InsuranceProductDTO productDTO)
        {
            var product = await context.InsuranceProducts.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product is null)
            {
                throw new Exception("Product not found");
            }

            product.ProductName = productDTO.ProductName!;
            product.Category = productDTO.Category!;
            product.BasePremium = productDTO.BasePremium;
            product.Description = productDTO.Description;

            
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<string> DeleteInsuranceProductAsync(int id)
        {
            var product = await context.InsuranceProducts.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product is null)
            {
                throw new Exception("Product not found");
            }
            context.Entry(product).State = EntityState.Detached;
            context.InsuranceProducts.Remove(product);
            await context.SaveChangesAsync();

            return "Insurance product deleted successfully";
        }
    }
}

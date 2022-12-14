using Ecommerce.Utilities.EFCore;
using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Products.Infrastructure.Repositories
{
    public class ProductRepository : EFCoreRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<bool> AnyAsync(Guid id)
        {
            return AnyAsync(_ => _.Id == id);
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            return await GetAsync(_ => _.Id == id);
        }

        public IQueryable<Product> GetDeactiveProducts()
        {
            return GetQuery(_ => !_.IsDeleted && !_.IsActive);
        }

        public IQueryable<Product> GetProducts()
        {
            return GetQuery(_ => !_.IsDeleted && _.IsActive);
        }
    }
}
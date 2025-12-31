using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopLite.StoreFx.Data;
using eShopLite.StoreFx.Models;
using Microsoft.EntityFrameworkCore;

namespace eShopLite.StoreFx.Services
{
    /// <summary>
    /// Interface for store-related business operations.
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// Retrieves all products asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a collection of products.</returns>
        Task<IEnumerable<Product>> GetProductsAsync();

        /// <summary>
        /// Retrieves all stores asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a collection of store information.</returns>
        Task<IEnumerable<StoreInfo>> GetStoresAsync();
    }

    /// <summary>
    /// Provides business logic for store operations.
    /// </summary>
    public class StoreService : IStoreService
    {
        private readonly StoreDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when context is null.</exception>
        public StoreService(StoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<StoreInfo>> GetStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }
    }
}
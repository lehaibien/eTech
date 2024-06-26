﻿using eTech.Entities;

namespace eTech.Services.Interfaces {
    public interface IProductService {
        public Task<List<Product>> GetAll();
        public Task<List<Product>> GetLatestProduct(int limit);
        public Task<Product> GetById(int id);
        public Task<Product> GetByBrandId(int brandId);
        public Task<Product> GetByCategoryId(int categoryId);
        public Task<List<Product>> Search(string query);
        public Task<Product> Add(Product product);
        public Task<Product> Update(Product product);
        public Task Delete(int id);
    }
}

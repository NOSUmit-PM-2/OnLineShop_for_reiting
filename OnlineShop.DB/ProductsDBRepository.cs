using OnlineShop.DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.DB
{
	public class ProductsDBRepository : IProductDBsRepository
	{
		private readonly DatabaseContext _databaseContext;

		public ProductsDBRepository(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public List<Product> GetAllProducts()
		{
			return _databaseContext.Products.ToList();
		}

		public Product GetProductById(int id)
		{
			return _databaseContext.Products.FirstOrDefault(p => p.Id == id);
		}

		public void AddProduct(Product product)
		{
			_databaseContext.Products.Add(product);
			_databaseContext.SaveChanges();
		}

		public void UpdateProduct(Product product)
		{
			var existingProduct = GetProductById(product.Id);
			if (existingProduct != null)
			{
				existingProduct.Name = product.Name;
				existingProduct.Description = product.Description;
				existingProduct.Price = product.Price;
				existingProduct.Category = product.Category;
				_databaseContext.SaveChanges();
			}
		}

		public void DeleteProduct(int id)
		{
			var product = GetProductById(id);
			if (product != null)
			{
				_databaseContext.Products.Remove(product);
				_databaseContext.SaveChanges();
			}
		}
	}
}
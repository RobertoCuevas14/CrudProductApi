using Microsoft.EntityFrameworkCore;
using CrudP.Data;
using CrudP.Entities;
namespace CrudP.Repositories
{
 
        public class ProductRepository : IProductRepository
        {
            private readonly AppDbContext _context;

            public ProductRepository(AppDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Product> GetAllProducts()
            {
                return _context.Products.Include(p => p.Cargo).ToList();
            }

            public Product GetProductById(int id)
            {
                return _context.Products.Include(p => p.Cargo).FirstOrDefault(p => p.Id == id);
            }

            public void AddProduct(Product product)
            {
                _context.Products.Add(product);
                SaveChanges();
            }

            public void UpdateProduct(Product product)
            {
                _context.Products.Update(product);
                SaveChanges();
            }

            public void DeleteProduct(int id)
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    SaveChanges();
                }
            }

            public void SaveChanges()
            {
                _context.SaveChanges();
            }
        }
    }


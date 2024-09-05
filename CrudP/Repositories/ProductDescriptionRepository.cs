using CrudP.Data;
using CrudP.Entities;

namespace CrudP.Repositories
{
    public class ProductDescriptionRepository : IProductDescriptionRepository
    {
        private readonly AppDbContext _context;

        public ProductDescriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddDescription(ProductDescription productDescription)
        {
            _context.ProductDescriptions.Add(productDescription);
            SaveChanges();
        }

        public IEnumerable<ProductDescription> GetDescriptionsByProductId(int productId)
        {
            return _context.ProductDescriptions.Where(d => d.ProductId == productId).ToList();
        }

        public void DeleteDescription(int id)
        {
            var productDescription = _context.ProductDescriptions.Find(id);
            if (productDescription != null)
            {
                _context.ProductDescriptions.Remove(productDescription);
                SaveChanges();
            }
        }

        public void UpdateDescription(ProductDescription productDescription)
        {
            _context.ProductDescriptions.Update(productDescription);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
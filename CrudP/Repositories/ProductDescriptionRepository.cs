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

        public void AddDescription(ProductDescription description)
        {
            _context.ProductDescriptions.Add(description);
            SaveChanges();
        }

        public IEnumerable<ProductDescription> GetDescriptionsByProductId(int productId)
        {
            return _context.ProductDescriptions.Where(d => d.ProductId == productId).ToList();
        }

        public void DeleteDescription(int id)
        {
            var description = _context.ProductDescriptions.Find(id);
            if (description != null)
            {
                _context.ProductDescriptions.Remove(description);
                SaveChanges();
            }
        }

        public void UpdateDescription(ProductDescription description)
        {
            _context.ProductDescriptions.Update(description);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
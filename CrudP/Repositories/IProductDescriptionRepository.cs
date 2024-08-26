using CrudP.Entities;
public interface IProductDescriptionRepository
{
    void AddDescription(ProductDescription description);
    IEnumerable<ProductDescription> GetDescriptionsByProductId(int productId);
    void DeleteDescription(int id);
    void UpdateDescription(ProductDescription description);
    void SaveChanges();
}

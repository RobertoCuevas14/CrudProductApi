using CrudP.Entities;
public interface IProductDescriptionRepository
{
    void AddDescription(ProductDescription productDescription);
    IEnumerable<ProductDescription> GetDescriptionsByProductId(int productId);
    void DeleteDescription(int id);
    void UpdateDescription(ProductDescription productDescription);
    void SaveChanges();
}

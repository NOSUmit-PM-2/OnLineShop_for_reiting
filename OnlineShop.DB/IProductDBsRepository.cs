namespace OnlineShop.DB
{
    public interface IProductDBsRepository
    {
        List<ProductDB> GetAll();
        ProductDB TryGetById(Guid id);
        void Add(ProductDB product);
        void Update(ProductDB product); 

        
        void DecreaseQuantity(Guid productId, int amount = 1);
        void IncreaseQuantity(Guid productId, int amount = 1);
        int GetAvailableQuantity(Guid productId);

        public interface IProductDBsRepository
        {
            void AddRange(List<ProductDB> products);
        }
    }
}
using ComputerStore.Database.Entities;
using System.Text.Json.Serialization;

namespace ComputerStore.Models
{
    public class ProductPurchaseModel
    {
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Guid PurchaseId { get; set; }

        public int Quantity { get; set; }

        public static implicit operator ProductPurchaseModel(ProductPurchase productPurchase)
        {
            return new ProductPurchaseModel()
            {
                ProductId = productPurchase.ProductId,
                PurchaseId = productPurchase.PurchaseId,
                Quantity = productPurchase.Quantity
            };
        }

        public static implicit operator ProductPurchase(ProductPurchaseModel productPurchaseModel)
        {
            return new ProductPurchase()
            {
                ProductId = productPurchaseModel.ProductId,
                PurchaseId = productPurchaseModel.PurchaseId,
                Quantity = productPurchaseModel.Quantity
            };
        }
    }
}

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

        public ProductModel? Product { get; set; }
        public PurchaseModel? Purchase { get; set; }

        public static implicit operator ProductPurchaseModel(ProductPurchase productPurchase)
        {
            ProductPurchaseModel productPurchaseModel = new ProductPurchaseModel()
            {
                ProductId = productPurchase.ProductId,
                PurchaseId = productPurchase.PurchaseId,
                Quantity = productPurchase.Quantity
            };

            if (productPurchase.Product != null)
            {
                productPurchaseModel.Product = productPurchase.Product;
            }
            if (productPurchase.Purchase != null)
            {
                productPurchaseModel.Purchase = productPurchase.Purchase;
            }

            return productPurchaseModel;
        }

        public static implicit operator ProductPurchase(ProductPurchaseModel productPurchaseModel)
        {
            ProductPurchase productPurchase = new ProductPurchase()
            {
                ProductId = productPurchaseModel.ProductId,
                PurchaseId = productPurchaseModel.PurchaseId,
                Quantity = productPurchaseModel.Quantity
            };

            if (productPurchaseModel.Product != null)
            {
                productPurchase.Product = productPurchaseModel.Product;
            }
            if (productPurchaseModel.Purchase != null)
            {
                productPurchase.Purchase = productPurchaseModel.Purchase;
            }

            return productPurchase;
        }
    }
}

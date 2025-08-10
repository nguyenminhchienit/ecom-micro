using Shared.Enums.Inventory;

namespace Shared.DTOs.Inventory
{
    public class PurchaseProductDto
    {
        public EDocumentType DocumentType => EDocumentType.Purchase;

        private string ItemNo;

        public string GetItemNo() => ItemNo;

        public void SetItemNo(string itemNo) => ItemNo = itemNo;

        public int Quantity { get; set; }

        public string DocumentNo { get; set; } = string.Empty;

        public string ExternalDocumentNo { get; set; } = string.Empty;
    }
}

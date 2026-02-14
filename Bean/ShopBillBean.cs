using System;

namespace Com.Wipro.Shop.Bean
{
    public class ShopBillBean
    {
        public int BillID { get; set; }
        public string ItemID { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
    }
}


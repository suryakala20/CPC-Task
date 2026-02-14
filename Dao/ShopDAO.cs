using System;
using Microsoft.Data.SqlClient;
using Com.Wipro.Shop.Util;

namespace Com.Wipro.Shop.Dao
{
    public class ShopDAO
    {
        //  Validate Item
        public bool validateItem(string itemID)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            string q = "SELECT COUNT(*) FROM SHOP_ITEM_TBL WHERE Item_ID=@id";

            using SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", itemID);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        // View Item
        public void viewItem(string itemID)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            string q = "SELECT * FROM SHOP_ITEM_TBL WHERE Item_ID=@id";

            using SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", itemID);

            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Console.WriteLine("\n--- ITEM DETAILS ---");
                Console.WriteLine("Item ID   : " + dr["Item_ID"]);
                Console.WriteLine("Name      : " + dr["Item_Name"]);
                Console.WriteLine("Price     : " + dr["Price"]);
                Console.WriteLine("Quantity  : " + dr["Quantity"]);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        // Generate Bill
        public void generateBill(string itemID, int qty)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            // Check available quantity
            string checkQty = "SELECT Quantity, Price FROM SHOP_ITEM_TBL WHERE Item_ID=@id";
            using SqlCommand checkCmd = new SqlCommand(checkQty, con);
            checkCmd.Parameters.AddWithValue("@id", itemID);

            using SqlDataReader dr = checkCmd.ExecuteReader();

            if (!dr.Read())
            {
                Console.WriteLine("Invalid Item ID");
                return;
            }

            int availableQty = Convert.ToInt32(dr["Quantity"]);
            double price = Convert.ToDouble(dr["Price"]);
            dr.Close();

            if (qty > availableQty)
            {
                Console.WriteLine("Not enough stock available!");
                return;
            }

            double total = price * qty;

            // Insert Bill
            string insertBill = @"INSERT INTO SHOP_BILL_TBL
                                  (Bill_ID, Item_ID, Quantity, Total_Amount, Bill_Date)
                                  VALUES (@bid, @id, @qty, @total, GETDATE())";

            using SqlCommand billCmd = new SqlCommand(insertBill, con);
            billCmd.Parameters.AddWithValue("@bid", new Random().Next(1000, 9999));
            billCmd.Parameters.AddWithValue("@id", itemID);
            billCmd.Parameters.AddWithValue("@qty", qty);
            billCmd.Parameters.AddWithValue("@total", total);

            billCmd.ExecuteNonQuery();

            // Update Quantity
            string updateQty = "UPDATE SHOP_ITEM_TBL SET Quantity = Quantity - @q WHERE Item_ID=@id";

            using SqlCommand updateCmd = new SqlCommand(updateQty, con);
            updateCmd.Parameters.AddWithValue("@q", qty);
            updateCmd.Parameters.AddWithValue("@id", itemID);

            updateCmd.ExecuteNonQuery();

            Console.WriteLine("\nBill Generated Successfully!");
            Console.WriteLine("Total Amount = " + total);
        }

        //  Update Quantity
        public void updateQuantity(string itemID, int qty)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            string q = "UPDATE SHOP_ITEM_TBL SET Quantity=@q WHERE Item_ID=@id";

            using SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@q", qty);
            cmd.Parameters.AddWithValue("@id", itemID);

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Quantity Updated Successfully!");
            else
                Console.WriteLine("Item not found!");
        }

        // ADD NEW ITEM (NEWLY ADDED METHOD)
        public void addItem(string itemID, string name, double price, int qty)
        {
            using SqlConnection con = DBUtil.GetDBConnection();
            con.Open();

            // Check if item already exists
            if (validateItem(itemID))
            {
                Console.WriteLine("Item already exists!");
                return;
            }

            string q = @"INSERT INTO SHOP_ITEM_TBL 
                        (Item_ID, Item_Name, Price, Quantity)
                        VALUES (@id, @name, @price, @qty)";

            using SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@id", itemID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@qty", qty);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Item Added Successfully!");
        }
    }
}

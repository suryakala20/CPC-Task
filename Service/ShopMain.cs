using System;
using Com.Wipro.Shop.Dao;

namespace Com.Wipro.Shop.Service
{
    public class ShopMain
    {
        public static void Main()
        {
            ShopDAO dao = new ShopDAO();

            while (true)
            {
                Console.WriteLine("\n===== SHOP BILLING SYSTEM =====");
                Console.WriteLine("1. View Item");
                Console.WriteLine("2. Generate Bill");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Add New Item");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");

                int ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        Console.Write("Enter Item ID: ");
                        string id = Console.ReadLine();
                        if (dao.validateItem(id))
                            dao.viewItem(id);
                        else
                            Console.WriteLine("INVALID ITEM ID");
                        break;

                    case 2:
                        Console.Write("Enter Item ID: ");
                        string iid = Console.ReadLine();
                        Console.Write("Enter Quantity: ");
                        int q = int.Parse(Console.ReadLine());
                        dao.generateBill(iid, q);
                        break;

                    case 3:
                        Console.Write("Enter Item ID: ");
                        string uid = Console.ReadLine();
                        Console.Write("Enter New Quantity: ");
                        int nq = int.Parse(Console.ReadLine());
                        dao.updateQuantity(uid, nq);
                        break;

                    case 4:
                        Console.Write("Enter Item ID: ");
                        string newId = Console.ReadLine();
                        Console.Write("Enter Item Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        double price = double.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int qty = int.Parse(Console.ReadLine());

                        dao.addItem(newId, name, price, qty);
                        break;

                    case 5:
                        return;
                }
            }
        }
    }
}

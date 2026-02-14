using Microsoft.Data.SqlClient;

namespace Com.Wipro.Shop.Util
{
    public class DBUtil
    {
        public static SqlConnection GetDBConnection()
        {
            string connectionString =
                "Server=localhost\\SQLEXPRESS;" +
                "Database=BillingForShopDB;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;";

            return new SqlConnection(connectionString);
        }
    }
}

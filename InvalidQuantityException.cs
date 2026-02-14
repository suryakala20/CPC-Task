using System;

namespace Com.Wipro.Shop.Util
{
    public class InvalidQuantityException : Exception
    {
        public override string ToString()
        {
            return "INVALID QUANTITY";
        }
    }
}

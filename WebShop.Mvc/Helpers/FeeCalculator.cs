namespace WebShop.Mvc.Helpers
{
    // Move it to Common assembly and share between wpf and mvc clients
    public static class FeeCalculator
    {
        private static decimal vatPercentage = 0.2M;

        public static decimal GetVatByPriceExclVat(decimal priceExclVat)
        {
            return (1 + vatPercentage) * priceExclVat;
        }

        public static decimal GetPriceInclVatByPriceExclVat(decimal priceExclVat)
        {
            return (1 + vatPercentage) * priceExclVat;
        }
    }
}
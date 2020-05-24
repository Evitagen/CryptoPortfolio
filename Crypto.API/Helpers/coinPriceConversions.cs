namespace Crypto.API.Helpers
{
    public class coinPriceConversions
    {
        public static decimal priceinBTC(decimal btcPrice, decimal shitcoinPrice) 
        {
            return btcPrice / shitcoinPrice;
        }

        public static decimal priceinEth(decimal ethPrice, decimal shitcoinPrice)
        {
            return ethPrice / shitcoinPrice;
        }
    }
}
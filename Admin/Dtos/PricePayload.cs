using Admin.Models;

namespace Admin.Dtos
{
    public class PricePayload
    {
        public PricePayload(Price price) 
        {
            Price = price;
        }

        public Price Price { get; }
    }
}

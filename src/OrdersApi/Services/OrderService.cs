namespace OrdersApi.Services;

public record Customer(string Name, string LoyaltyTier);

public record Order(decimal Total, int Quantity, Customer? Customer);

public class OrderService
{
    /// <summary>
    /// Calculates a loyalty discount based on the customer's tier.
    /// BUG: throws NullReferenceException when Customer is null
    /// (guest checkout, API call without auth, data migration).
    /// </summary>
    public decimal CalculateDiscount(Order order)
    {
        var rate = order.Customer.LoyaltyTier switch
        {
            "gold" => 0.15m,
            "silver" => 0.10m,
            _ => 0.05m
        };
        return order.Total * rate;
    }
}

namespace OrdersApi.Services;

public record Customer(string Name, string LoyaltyTier);

public record Order(decimal Total, int Quantity, Customer? Customer);

public class OrderService
{
    /// <summary>
    /// Calculates a loyalty discount based on the customer's tier.
    /// Returns 0 for guest checkouts (null Customer).
    /// </summary>
    public decimal CalculateDiscount(Order order)
    {
        if (order.Customer is null)
            return 0m;

        var rate = order.Customer.LoyaltyTier switch
        {
            "gold" => 0.15m,
            "silver" => 0.10m,
            _ => 0.05m
        };
        return order.Total * rate;
    }
}

using OrdersApi.Services;
using Xunit;

namespace OrdersApi.Tests;

public class OrderServiceTests
{
    private readonly OrderService _sut = new();

    [Fact]
    public void CalculateDiscount_GoldCustomer_Returns15Percent()
    {
        var order = new Order(200m, 2, new Customer("Alice", "gold"));
        var result = _sut.CalculateDiscount(order);
        Assert.Equal(30.0m, result); // 200 * 0.15
    }

    [Fact]
    public void CalculateDiscount_GuestCheckout_ReturnsZero()
    {
        // This test will FAIL with NullReferenceException.
        // Guest checkouts have no Customer object.
        var order = new Order(200m, 1, Customer: null);
        var result = _sut.CalculateDiscount(order);
        Assert.Equal(0m, result);
    }

    [Fact]
    public void CalculateDiscount_SilverCustomer_Returns10Percent()
    {
        var order = new Order(100m, 1, new Customer("Bob", "silver"));
        var result = _sut.CalculateDiscount(order);
        Assert.Equal(10.0m, result); // 100 * 0.10
    }
}

using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for Sale and SaleItem entities using the Bogus library.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker<DeveloperEvaluation.Domain.Entities.Sale> SaleFaker = new Faker<DeveloperEvaluation.Domain.Entities.Sale>()
        .CustomInstantiator(f => new DeveloperEvaluation.Domain.Entities.Sale(
            f.Random.Guid(),
            f.Random.String2(5, 10),
            f.Date.Past(),
            f.Random.Guid(),
            f.Name.FullName(),
            f.Random.Guid(),
            f.Company.CompanyName()
        ));

    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .CustomInstantiator(f => new SaleItem(
            f.Random.Guid(),
            f.Random.Guid(),
            f.Commerce.ProductName(),
            f.Random.Int(1, 20),
            f.Random.Decimal(1, 100),
            f.Random.Decimal(0, 0.2m)
        ));

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// </summary>
    public static DeveloperEvaluation.Domain.Entities.Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// </summary>
    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    /// <summary>
    /// Generates a Sale with a list of valid SaleItems.
    /// </summary>
    public static DeveloperEvaluation.Domain.Entities.Sale GenerateSaleWithItems(int itemCount = 3)
    {
        var sale = GenerateValidSale();
        var itemsField = typeof(DeveloperEvaluation.Domain.Entities.Sale).GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var itemsList = (List<SaleItem>)itemsField.GetValue(sale);
        for (int i = 0; i < itemCount; i++)
        {
            itemsList.Add(GenerateValidSaleItem());
        }
        return sale;
    }
}

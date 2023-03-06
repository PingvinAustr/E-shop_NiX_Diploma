namespace MVC.ViewModels;

public record CatalogItem
{
    public int CarId { get; set; }

    public string CarName { get; set; } = null!;

    public string CarPromo { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageFileName { get; set; } = null!;

    public CatalogType CarType { get; set; } = null!;

    public CatalogBrand Manufacturer { get; set; } = null!;

    public bool IsAvailable { get; set; }
}
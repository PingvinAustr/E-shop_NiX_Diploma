namespace MVC.ViewModels;

public record CatalogItem
{
    public int Id { get; set; }

    public string CarName { get; set; } = null!;

    public string CarPromo { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageFileName { get; set; } = null!;

    public CatalogType CatalogType { get; set; } = null!;

    public CatalogBrand CatalogBrand { get; set; } = null!;

    public bool isAvaliable { get; set; }
}
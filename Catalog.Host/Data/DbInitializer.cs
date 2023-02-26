using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Manufacturers.Any())
            {
                await context.Manufacturers.AddRangeAsync(GetPreconfiguredManufacturers());

                await context.SaveChangesAsync();
            }

            if (!context.Types.Any())
            {
                await context.Types.AddRangeAsync(GetPreconfiguredTypes());

                await context.SaveChangesAsync();
            }

            if (!context.Cars.Any())
            {
                await context.Cars.AddRangeAsync(GetPreconfiguredCars());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Manufacturer> GetPreconfiguredManufacturers()
        {
            return new List<Manufacturer>()
        {
            new Manufacturer() { ManufacturerId = 1, ManufacturerName = "Mercedes-Benz", ManufacturerCountry = "Germany" },
            new Manufacturer() { ManufacturerId = 2, ManufacturerName = "BWM", ManufacturerCountry = "Germany" },
            new Manufacturer() { ManufacturerId = 3, ManufacturerName = "Ford", ManufacturerCountry = "USA" },
            new Manufacturer() { ManufacturerId = 4, ManufacturerName = "Lexus", ManufacturerCountry = "Japan" },
            new Manufacturer() { ManufacturerId = 5, ManufacturerName = "Avtozaz", ManufacturerCountry = "Ukraine" }
        };
        }

        private static IEnumerable<Entities.Type> GetPreconfiguredTypes()
        {
            return new List<Entities.Type>()
        {
            new Entities.Type() { TypeName = "Business class car", TypeDescription = "Business" },
            new Entities.Type() { TypeName = "Racing car", TypeDescription = "Racing" },
            new Entities.Type() { TypeName = "Historical car", TypeDescription = "History" },
            new Entities.Type() { TypeName = "Medium class car", TypeDescription = "Medium" },
        };
        }

        private static IEnumerable<Car> GetPreconfiguredCars()
        {
            return new List<Car>()
        {
            new Car() { CarName = "Mercedes-AMG GmbH GT", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 1, TypeId = 1, Price = 102000 },
            new Car() { CarName = "Mercedes S-class", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 1, TypeId = 1, Price = 100000 },
            new Car() { CarName = "Mercedes A-class", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 1, TypeId = 4, Price = 100000 },
            new Car() { CarName = "Mercedes C-class", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 1, TypeId = 4, Price = 100000 },
            new Car() { CarName = "BMW X1 sDrive18i", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 2, TypeId = 4, Price = 35000 },
            new Car() { CarName = "BMW X7 M50d", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 2, TypeId = 1, Price = 125000 },
            new Car() { CarName = "BMW X7 xDrive30d", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 2, TypeId = 1, Price = 75000 },
            new Car() { CarName = "Ford GT Mk II ", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 3, TypeId = 2, Price = 300000 },
            new Car() { CarName = "Ford Mustang 1967", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 3, TypeId = 3, Price = 50000 },
            new Car() { CarName = "Zaporozhets", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 5, TypeId = 3, Price = 102000 },
            new Car() { CarName = "LEXUS ES300H", CarPromo = "Promo", ImageFileName = "Image", IsAvailable = true, ManufacturerId = 4, TypeId = 4, Price = 55000 },
        };
        }
    }
}

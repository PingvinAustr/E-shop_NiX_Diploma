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
            new Entities.Type() { TypeName = "Business class car", TypeDescription = "Business class cars are designed with executive needs in mind. They offer high-end features, luxury interiors, and cutting-edge technology, ensuring a comfortable and safe ride. Typically, these cars are sleek, stylish, and come with a powerful engine. They are ideal for corporate travel, business meetings, and VIP transportation." },
            new Entities.Type() { TypeName = "Racing car", TypeDescription = "Racing cars are purpose-built machines designed for high-speed performance on the track. These cars are lightweight, aerodynamic, and feature advanced suspensions, brakes, and engines. They often have stripped-down interiors and safety features to reduce weight. Racing cars come in various categories, from open-wheel single-seaters to closed cockpit sports cars, and are built for speed and handling above all else." },
            new Entities.Type() { TypeName = "Historical car", TypeDescription = "Historical cars are classic vehicles that evoke a sense of nostalgia and beauty from a bygone era. These cars are often considered works of art. They feature unique styling, luxury interiors, and mechanical engineering that reflects the time period they were built in. Historical cars are a symbol of automotive history and have become highly sought-after by collectors and enthusiasts." },
            new Entities.Type() { TypeName = "Medium class car", TypeDescription = "Medium class cars, also known as midsize cars, are practical and affordable vehicles that offer a balance between size, performance, and fuel efficiency. These cars are spacious enough to accommodate five passengers and have ample trunk space. They offer a comfortable ride, good handling, and modern features such as touchscreen infotainment systems, rearview cameras, and advanced safety features. Medium class cars are a popular choice for families, commuters, and budget-conscious buyers." },
        };
        }

        private static IEnumerable<Car> GetPreconfiguredCars()
        {
            return new List<Car>()
        {
            new Car() { CarName = "Mercedes-AMG GmbH GT", CarPromo = "The Mercedes-AMG GT is a high-performance sports car manufactured by Mercedes-AMG GmbH, the high-performance subsidiary of Mercedes-Benz. The GT is powered by a hand-built twin-turbocharged 4.0-liter V8 engine that produces up to 577 horsepower and 516 lb-ft of torque. The car is available in coupe and roadster versions and features a lightweight aluminum body, advanced aerodynamics, and a luxurious interior with state-of-the-art technology. The AMG GT is a true driver's car that offers exceptional handling, acceleration, and performance, making it a popular choice among car enthusiasts and professionals alike.", ImageFileName = "1", IsAvailable = true, ManufacturerId = 1, TypeId = 1, Price = 102000 },
            new Car() { CarName = "Mercedes S-class", CarPromo = "The Mercedes S Class is a full-size luxury sedan that embodies the highest levels of sophistication and refinement. The S Class is equipped with a range of powerful engines, including hybrid and plug-in hybrid options, and features advanced technology, such as adaptive cruise control, a head-up display, and a 12.3-inch infotainment system. The interior is meticulously crafted with premium materials, including leather, wood, and metal accents, and offers an unparalleled level of comfort and convenience. The S Class is a flagship model of the Mercedes-Benz brand and is designed to provide the ultimate driving experience.", ImageFileName = "2", IsAvailable = true, ManufacturerId = 1, TypeId = 1, Price = 100000 },
            new Car() { CarName = "Mercedes A-class", CarPromo = "The Mercedes A Class is a compact executive car that offers a combination of performance, luxury, and practicality. The A Class is equipped with a range of engines, including petrol, diesel, and plug-in hybrid options, and features a sleek design and aerodynamic styling. The interior is modern and sophisticated, with a focus on technology, such as the MBUX infotainment system, and convenience, with features like the panoramic sunroof and keyless entry. The A Class is a versatile car that is perfect for city driving, long trips, and everything in between.", ImageFileName = "3", IsAvailable = true, ManufacturerId = 1, TypeId = 4, Price = 100000 },
            new Car() { CarName = "Mercedes C-class", CarPromo = "The Mercedes C Class is a mid-size luxury car that combines performance, comfort, and practicality. The C Class is available in sedan, coupe, and cabriolet versions and offers a range of engine options, including petrol, diesel, and hybrid options. The interior is spacious and refined, with high-quality materials and advanced technology, such as the 10.25-inch infotainment system and the driver assistance package. The C Class is a stylish and elegant car that offers an exceptional driving experience, making it a popular choice for business and leisure purposes.", ImageFileName = "4", IsAvailable = true, ManufacturerId = 1, TypeId = 4, Price = 100000 },
            new Car() { CarName = "BMW X1 sDrive18i", CarPromo = "The BMW X1 sDrive18i is a compact luxury crossover SUV that offers an impressive combination of performance, comfort, and versatility. The X1 is powered by a turbocharged 1.5-liter three-cylinder engine that produces up to 140 horsepower and 162 lb-ft of torque, while also providing exceptional fuel economy. The car features a spacious and luxurious interior, with premium materials and advanced technology, such as the 8.8-inch infotainment system and the panoramic sunroof. The X1 is a practical car that offers excellent handling and maneuverability, making it perfect for city driving and long trips.", ImageFileName = "5", IsAvailable = true, ManufacturerId = 2, TypeId = 4, Price = 35000 },
            new Car() { CarName = "BMW X7 M50d", CarPromo = "The BMW X7 M50d is a full-size luxury SUV that offers a combination of power, luxury, and versatility. The X7 M50d is powered by a quad-turbocharged 3.0-liter six-cylinder diesel engine that produces up to 400 horsepower and 560 lb-ft of torque, while also providing exceptional fuel economy. The car features a spacious and luxurious interior, with advanced technology, such as the 12.3-inch infotainment system and the panoramic sunroof. The X7 M50d is a practical car that offers excellent handling and maneuverability, making it perfect for families and business purposes. It also offers a high level of safety and security, with features like lane departure warning, automatic emergency braking, and a surround-view camera system.", ImageFileName = "6", IsAvailable = true, ManufacturerId = 2, TypeId = 1, Price = 125000 },
            new Car() { CarName = "BMW X7 xDrive30d", CarPromo = "The BMW X7 xDrive30d is a full-size luxury SUV that offers a balance of power, comfort, and versatility. The X7 xDrive30d is powered by a turbocharged 3.0-liter six-cylinder diesel engine that produces up to 286 horsepower and 479 lb-ft of torque, while also providing good fuel economy. The car features a spacious and luxurious interior, with advanced technology, such as the 12.3-inch infotainment system and the panoramic sunroof. The X7 xDrive30d is a practical car that offers excellent handling and maneuverability, making it perfect for families and business purposes. It also offers a high level of safety and security, with features like adaptive cruise control, automatic emergency braking, and lane departure warning.", ImageFileName = "7", IsAvailable = true, ManufacturerId = 2, TypeId = 1, Price = 75000 },
            new Car() { CarName = "Ford GT Mk II", CarPromo = "The Ford GT Mk II is a high-performance track-only supercar that offers exceptional speed, agility, and handling. The GT Mk II is powered by a 3.5-liter twin-turbocharged V6 engine that produces up to 700 horsepower, allowing the car to go from 0 to 60 mph in just 3.3 seconds. The car features advanced aerodynamics, a lightweight carbon fiber body, and a racing-inspired interior with state-of-the-art technology. The GT Mk II is a limited edition car that is designed for serious racing enthusiasts who demand the best in performance and exclusivity.", ImageFileName = "8", IsAvailable = true, ManufacturerId = 3, TypeId = 2, Price = 300000 },
            new Car() { CarName = "Ford Mustang 1967", CarPromo = "The 1967 Ford Mustang is an iconic American muscle car that embodies the spirit of the 1960s. The Mustang features a powerful V8 engine, with up to 390 horsepower, and a sleek design with distinctive lines and styling cues. The interior is spacious and comfortable, with a range of options, such as leather seats and a wood-rimmed steering wheel. The Mustang is a classic car that is perfect for collectors, enthusiasts, and anyone who appreciates the timeless design and performance of this legendary vehicle.", ImageFileName = "9", IsAvailable = true, ManufacturerId = 3, TypeId = 3, Price = 50000 },
            new Car() { CarName = "Zaporozhets", CarPromo = "The Zaporozhets is a classic Soviet-era car that was produced in Ukraine from the 1960s to the 1990s. The car features a rear-engine, rear-wheel-drive layout, with a distinctive boxy design and simple mechanicals. The Zaporozhets was a popular car in the Soviet Union, due to its affordability, durability, and ease of maintenance. The car was also known for its practicality, with a spacious interior and good fuel economy. Today, the Zaporozhets is a rare and unique vehicle that represents a piece of automotive history from the Soviet era.", ImageFileName = "10", IsAvailable = false, ManufacturerId = 5, TypeId = 3, Price = 102000 },
            new Car() { CarName = "LEXUS ES300H", CarPromo = "The Lexus ES300h is a mid-size luxury sedan that combines the latest technology with a refined driving experience. The ES300h is powered by a hybrid powertrain that consists of a 2.5-liter four-cylinder engine and an electric motor, providing a total output of 215 horsepower. The car features a spacious and luxurious interior, with premium materials and advanced technology, such as the 12.3-inch infotainment system and the Mark Levinson sound system. The ES300h is also equipped with a range of safety features, such as automatic emergency braking, lane departure warning, and adaptive cruise control, making it a top pick for safety-conscious buyers.", ImageFileName = "11", IsAvailable = true, ManufacturerId = 4, TypeId = 4, Price = 55000 },
        };
        }
    }
}

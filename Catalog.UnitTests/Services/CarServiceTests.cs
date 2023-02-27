using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CarServiceTests
{
    private readonly ICarService _carService;

    private readonly Mock<ICarRepository> _carRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly Car _testCarEntity = new Car()
    {
        CarName = "CarEntityName",
        CarPromo = "CarEntityDescription",
        Price = 1000,
        IsAvailable = false,
        ImageFileName = "EntityImageFileName",
        ManufacturerId = 1,
        TypeId = 1
    };
    private readonly CarDto _testCarDto = new CarDto()
    {
        CarName = "CarDtoName",
        CarPromo = "CarDtoDescription",
        Price = 1000,
        IsAvailable = false,
        ImageFileName = "DtoImageFileName"
    };

    public CarServiceTests()
    {
        _carRepository = new Mock<ICarRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _carService = new CarService(_dbContextWrapper.Object, _logger.Object, _carRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task Add_Success()
    {
        // arrange
        var testResult = 1;

        _carRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _carService.Add(_testCarEntity.CarName, _testCarEntity.CarPromo, _testCarEntity.Price, _testCarEntity.IsAvailable, _testCarEntity.TypeId, _testCarEntity.ManufacturerId, _testCarEntity.ImageFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Add_Failed()
    {
        // arrange
        int? testResult = null;

        _carRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _carService.Add(_testCarEntity.CarName, _testCarEntity.CarPromo, _testCarEntity.Price, _testCarEntity.IsAvailable, _testCarEntity.ManufacturerId, _testCarEntity.TypeId, _testCarEntity.ImageFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public void Delete_Success()
    {
        // arrange
        int testItemId = 1;
        bool testResult = true;

        _carRepository.Setup(s => s.Delete(testItemId)).Returns(testResult);

        // act
        var result = _carService.Delete(testItemId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public void Delete_Failed()
    {
        // arrange
        int testItemId = 2;
        bool testResult = false;

        _carRepository.Setup(s => s.Delete(testItemId)).Returns(testResult);

        // act
        var result = _carService.Delete(testItemId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Put_Success()
    {
        // arrange
        var itemId = 1;
        var expected = 1;

        _mapper.Setup(m => m.Map<Car>(_testCarDto)).Returns(new Car());

        _carRepository.Setup(s => s.Put(
            It.IsAny<Car>(),
            itemId)).ReturnsAsync(expected);

        // act
        var result = await _carService.Put(_testCarDto, itemId);

        // assert
        result.Should().Be(expected);
    }

    [Fact]
    public async Task Put_Failed()
    {
        // arrange
        var itemId = 1;
        int? expected = null;

        _mapper.Setup(m => m.Map<Car>(_testCarDto)).Returns(new Car());

        _carRepository.Setup(s => s.Put(
            It.IsAny<Car>(),
            itemId)).ReturnsAsync(expected);

        // act
        var result = await _carService.Put(_testCarDto, itemId);

        // assert
        result.Should().Be(expected);
    }

}
using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTests
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICarRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<Infrastructure.Services.Interfaces.IDbContextWrapper<AppDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTests()
    {
        _catalogItemRepository = new Mock<ICarRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<Infrastructure.Services.Interfaces.IDbContextWrapper<AppDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<Car>()
        {
            Data = new List<Car>()
            {
                new Car()
                {
                    CarName = "TestCarName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new Car()
        {
            CarName = "TestCarName"
        };

        var catalogItemDtoSuccess = new CarDto()
        {
            CarName = "TestCarName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CarDto>(
            It.Is<Car>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).Returns((Func<PaginatedItemsResponse<CarDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetManufacturers_Success()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 0;
        var totalCount = 2;

        var paginatedItemsSuccess = new PaginatedItems<Manufacturer>
        {
            Data = new List<Manufacturer>
                {
                    new Manufacturer { ManufacturerId = 1, ManufacturerName = "Brand1", ManufacturerCountry = "A" },
                    new Manufacturer { ManufacturerId = 2, ManufacturerName = "Brand2", ManufacturerCountry = "B" }
                },
            TotalCount = totalCount
        };

        _catalogItemRepository.Setup(s => s.GetManufacturers()).ReturnsAsync(paginatedItemsSuccess);

        // Act
        var result = await _catalogService.GetManufacturers();

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(2);
        result.Count.Should().Be(totalCount);
        result.PageIndex.Should().Be(pageIndex);
        result.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public async Task GetManufacturers_Fail()
    {
        // Arrange
        var pageIndex = 4;
        var pageSize = 2;
        var totalCount = 2;

        var paginatedItemsSuccess = new PaginatedItems<Manufacturer>
        {
            Data = new List<Manufacturer>
                {
                    new Manufacturer { ManufacturerId = 1, ManufacturerName = "Brand1", ManufacturerCountry = "A" },
                    new Manufacturer { ManufacturerId = 2, ManufacturerName = "Brand2", ManufacturerCountry = "B" }
                },
            TotalCount = totalCount
        };

        _catalogItemRepository.Setup(s => s.GetManufacturers()).ReturnsAsync(paginatedItemsSuccess);

        // Act
        var result = await _catalogService.GetManufacturers();

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(2);
        result.Count.Should().Be(totalCount);
        result.PageIndex.Should().NotBe(pageIndex);
        result.PageSize.Should().NotBe(pageSize);
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        var testCarId = 1;
        var testCar = new Car()
        {
            CarId = testCarId,
            CarName = "TestCarName",
        };
        var testPaginatedItems = new PaginatedItems<Car>()
        {
            Data = new List<Car>() { testCar },
            TotalCount = 1,
        };
        _catalogItemRepository.Setup(s => s.GetById(It.Is<int>(i => i == testCarId))).ReturnsAsync(testPaginatedItems);

        // act
        var result = await _catalogService.GetById(testCarId);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(1);
    }

    [Fact]
    public async Task GetByIdAsync_Fail ()
    {
        // arrange
        var testCarId = 1;
        _catalogItemRepository.Setup(s => s.GetById(It.Is<int>(i => i == testCarId))).ReturnsAsync((PaginatedItems<Car>)null!);

        // act
        var result = await _catalogService.GetById(testCarId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByBrand_Succes()
    {
        // Arrange
        int brandId = 1;
        int pageIndex = 0;
        int pageSize = 10;

        var expected = new PaginatedItems<Car>
        {
            Data = new[]
            {
                    new Car { CarId = 1, CarName = "Car1" },
                    new Car { CarId = 2, CarName = "Car2" }
                },
            TotalCount = 2
        };

        _catalogItemRepository.Setup(r => r.GetByBrand(brandId))
                          .ReturnsAsync(expected);

        // Act
        var result = await _catalogItemRepository.Object.GetByBrand(brandId);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetByBrand_Fail()
    {
        // Arrange
        int brandId = -1;
        int pageIndex = 0;
        int pageSize = 10;

        var expected = new PaginatedItems<Car>();

        _catalogItemRepository.Setup(r => r.GetByBrand(brandId))
                          .ReturnsAsync(expected);

        // Act
        var result = await _catalogItemRepository.Object.GetByBrand(brandId);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetByTypeAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 0;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<Car>()
        {
            Data = new List<Car>()
            {
                new Car()
                {
                    CarName = "TestCarName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new Car()
        {
            CarName = "TestCarName"
        };

        var catalogItemDtoSuccess = new CarDto()
        {
            CarName = "TestCarName"
        };

        _catalogItemRepository.Setup(s => s.GetByType(
            It.IsAny<int>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CarDto>(
            It.Is<Car>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetByType(testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetByTypeAsync_Fail()
    {
        int testTypeIndex = 0;
        // arrange
        _catalogItemRepository.Setup(s => s.GetByType(
            It.IsAny<int>())).ReturnsAsync((PaginatedItems<Car>)null!);

        // act
        var result = await _catalogService.GetByType(testTypeIndex);

        // assert
        result.Should().BeNull();
    }

}
using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICarRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICarRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>>();
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
                    CarName = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new Car()
        {
            CarName = "TestName"
        };

        var catalogItemDtoSuccess = new CarDto()
        {
            CarName = "TestName"
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
}
using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICarService _catalogService;

    private readonly Mock<ICarRepository> _catalogItemRepository;
    private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly Car _testItem = new Car()
    {
        CarName = "Name",
        CarPromo = "Description",
        Price = 1000,
        IsAvailable = false,
        ImageFileName = "a",
        ManufacturerId = 1,
        TypeId = 1
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICarRepository>();
        _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CarService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.CarName, _testItem.CarPromo, _testItem.Price, _testItem.IsAvailable, _testItem.TypeId, _testItem.ManufacturerId, _testItem.ImageFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.CarName, _testItem.CarPromo, _testItem.Price, _testItem.IsAvailable, _testItem.ManufacturerId, _testItem.TypeId, _testItem.ImageFileName);

        // assert
        result.Should().Be(testResult);
    }
}
using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class ManufacturerServiceTests
    {

        private readonly ManufacturerService _manufacturerService;

        private readonly Mock<IManufacturerRepository> _manufacturerRepository;
        private readonly Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<ManufacturerService>> _logger;
        private readonly Mock<IMapper> _mapper;

        public ManufacturerServiceTests()
        {
            _manufacturerRepository = new Mock<IManufacturerRepository>();
            _dbContextWrapper = new Mock<Host.Services.Interfaces.IDbContextWrapper<AppDbContext>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<ManufacturerService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _manufacturerService = new ManufacturerService(_dbContextWrapper.Object, _logger.Object, _manufacturerRepository.Object, _mapper.Object);
        }

        private readonly Manufacturer _testManufacturerEntity = new Manufacturer()
        {
            ManufacturerId = 1,
            ManufacturerName = "ManufacturerEntityName",
            ManufacturerCountry = "ManufacturerEntityCountry"
        };

        private readonly ManufacturerDto _testManufacturerDto = new ManufacturerDto()
        {
            ManufacturerId = 1,
            ManufacturerName = "ManufacturerDtoName",
            ManufacturerCountry = "ManufactuerDtoCountry"
        };

        [Fact]
        public async Task Add_Success()
        {
            // arrange
            int testResult = 1;

            _manufacturerRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _manufacturerService.Add(_testManufacturerEntity.ManufacturerName, _testManufacturerEntity.ManufacturerCountry);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Add_Fail()
        {
            // arrange
            int? testResult = null;

            _manufacturerRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _manufacturerService.Add(_testManufacturerEntity.ManufacturerName, _testManufacturerEntity.ManufacturerCountry);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Success()
        {
            // arrange
            int testItemId = 1;
            bool testResult = true;

            _manufacturerRepository.Setup(s => s.Delete(testItemId)).Returns(testResult);

            // act
            var result = _manufacturerService.Delete(testItemId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Fail()
        {
            // arrange
            int testItemId = 1;
            bool testResult = false;

            _manufacturerRepository.Setup(s => s.Delete(testItemId)).Returns(testResult);

            // act
            var result = _manufacturerService.Delete(testItemId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Put_Success()
        {
            // arrange
            var itemId = 1;
            var expected = 1;

            _mapper.Setup(m => m.Map<Manufacturer>(_testManufacturerDto)).Returns(new Manufacturer());

            _manufacturerRepository.Setup(s => s.Put(
                It.IsAny<Manufacturer>(),
                itemId)).ReturnsAsync(expected);

            // act
            var result = await _manufacturerService.Put(_testManufacturerDto, itemId);

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public async Task Put_Failed()
        {
            // arrange
            var itemId = 1;
            int? expected = null;

            _mapper.Setup(m => m.Map<Manufacturer>(_testManufacturerDto)).Returns(new Manufacturer());

            _manufacturerRepository.Setup(s => s.Put(
                It.IsAny<Manufacturer>(),
                itemId)).ReturnsAsync(expected);

            // act
            var result = await _manufacturerService.Put(_testManufacturerDto, itemId);

            // assert
            result.Should().Be(expected);
        }
    }
}

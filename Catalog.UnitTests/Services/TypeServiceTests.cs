using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class TypeServiceTests
    {

        private readonly TypeService _typeService;

        private readonly Mock<ITypeRepository> _typeRepository;
        private readonly Mock<Infrastructure.Services.Interfaces.IDbContextWrapper<AppDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<TypeService>> _logger;
        private readonly Mock<IMapper> _mapper;

        public TypeServiceTests()
        {
            _typeRepository = new Mock<ITypeRepository>();
            _dbContextWrapper = new Mock<Infrastructure.Services.Interfaces.IDbContextWrapper<AppDbContext>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<TypeService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _typeService = new TypeService(_dbContextWrapper.Object, _logger.Object, _typeRepository.Object, _mapper.Object);
        }

        private readonly Host.Data.Entities.Type _testTypeEntity = new Host.Data.Entities.Type()
        {
            TypeId = 1,
            TypeName = "TestEntityTypeName",
            TypeDescription = "TestEntityTypeDescription"
        };

        private readonly TypeDto _testTypeDto = new TypeDto()
        {
            TypeId = 1,
            TypeName = "TestDtpTypeName",
            TypeDescription = "TestDtoTypeDescription"
        };

        [Fact]
        public async Task Add_Success()
        {
            // arrange
            int testResult = 1;

            _typeRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _typeService.Add(_testTypeEntity.TypeName, _testTypeEntity.TypeDescription);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Add_Fail()
        {
            // arrange
            int? testResult = null;

            _typeRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            // _testTypeEntity
            var result = await _typeService.Add(_testTypeEntity.TypeName, _testTypeEntity.TypeDescription);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Success()
        {
            // arrange
            int testItemId = 1;
            bool testResult = true;

            _typeRepository.Setup(s => s.Delete(testItemId)).Returns(testResult);

            // act
            var result = _typeService.Delete(testItemId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Delete_Fail()
        {
            // arrange
            int testItemId = 1;
            bool testResult = false;

            _typeRepository.Setup(s => s.Delete(testItemId)).Returns(testResult);

            // act
            var result = _typeService.Delete(testItemId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Put_Success()
        {
            // arrange
            var itemId = 1;
            var expected = 1;

            _mapper.Setup(m => m.Map<Host.Data.Entities.Type>(_testTypeDto)).Returns(new Host.Data.Entities.Type());

            _typeRepository.Setup(s => s.Put(
                It.IsAny<Host.Data.Entities.Type>(),
                itemId)).ReturnsAsync(expected);

            // act
            var result = await _typeService.Put(_testTypeDto, itemId);

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public async Task Put_Failed()
        {
            // arrange
            var itemId = 1;
            int? expected = null;

            _mapper.Setup(m => m.Map<Host.Data.Entities.Type>(_testTypeDto)).Returns(new Host.Data.Entities.Type());

            _typeRepository.Setup(s => s.Put(
                It.IsAny<Host.Data.Entities.Type>(),
                itemId)).ReturnsAsync(expected);

            // act
            var result = await _typeService.Put(_testTypeDto, itemId);

            // assert
            result.Should().Be(expected);
        }
    }
}

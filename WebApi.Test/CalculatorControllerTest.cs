using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Services;

namespace WebApi.Test
{
    [TestFixture]
    public class CalculatorControllerTest
    {
        private CalculatorController _calculatorController;
        private Mock<ICalculatorService> _calculatorService;

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public async Task AddNumberAsync_OK()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                this._calculatorService = mock.Mock<ICalculatorService>();
                this._calculatorController = new CalculatorController(this._calculatorService.Object)
                {
                    Request = new HttpRequestMessage(),
                    Configuration = new HttpConfiguration()
                };
                this._calculatorService.Setup(e => e.AddNumberAsync(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(5);

                // Act
                var responseMessage = await this._calculatorController.Get(1, 9);

                // Assert
                this._calculatorService.Verify(e => e.AddNumberAsync(It.IsAny<int>(), It.IsAny<int>()), Times.AtMostOnce);
                Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.IsTrue(responseMessage.IsSuccessStatusCode);
                Assert.IsTrue(responseMessage.TryGetContentValue(out int value));
                Assert.AreEqual(5, value);
            }
        }

        [Test]
        public async Task AddNumberAsync_BadRequest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                this._calculatorService = mock.Mock<ICalculatorService>();
                this._calculatorController = new CalculatorController(this._calculatorService.Object)
                {
                    Request = new HttpRequestMessage(),
                    Configuration = new HttpConfiguration()
                };
                Expression<Func<ICalculatorService, Task<int>>> addNumberAsyncExpression =
                    e => e.AddNumberAsync(It.IsAny<int>(), It.IsAny<int>());
                this._calculatorService.Setup(addNumberAsyncExpression).ReturnsAsync(5);

                // Act
                var responseMessage = await this._calculatorController.Get(1, 0);

                // Assert
                this._calculatorService.Verify(e => e.AddNumberAsync(It.IsAny<int>(), It.IsAny<int>()), Times.AtMostOnce);
                Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
                Assert.IsTrue(!responseMessage.IsSuccessStatusCode);
            }
        }
    }
}

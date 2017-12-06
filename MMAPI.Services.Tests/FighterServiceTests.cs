using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Repository.Interfaces;
using MMAPI.Models;
using Moq;
using System.Threading.Tasks;

namespace MMAPI.Services.Tests
{
    [TestClass]
    public class FighterServiceTests
    {
        [TestMethod]
        [TestCategory("Services > FighterService > CreateAsync")]
        public async Task CreateAsync_Valid_ShouldReturnGuid()
        {
            // Arrange
            var expectedGuidString = "67deb339-8180-457f-9568-577ab5d51c5a";
            var fighter = new Fighter
            {
                FirstName = "Test",
                LastName = "Fighter",
                Reach = 10,
                Height = 10,
                DateOfBirth = new DateTimeOffset(1999, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            var fighterRepoMock = new Mock<IRepository<Fighter>>();       
            fighterRepoMock.Setup(r => r.CreateAsync(fighter)).ReturnsAsync(expectedGuidString);

            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act
            var actualGuidString = await fighterService.CreateAsync(fighter);

            // Assert
            Assert.AreEqual(expectedGuidString, actualGuidString);
        }

        [TestMethod]
        [TestCategory("Services > FighterService > CreateAsync")]
        public async Task ValidateAndCreateAsync_Invalid_ShouldThrow()
        {
            // Arrange
            var fighter = new Fighter();
            var fighterRepoMock = new Mock<IRepository<Fighter>>();
            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await fighterService.ValidateAndCreateAsync(fighter);
            });
        }
    }
}

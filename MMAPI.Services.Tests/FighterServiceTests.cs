using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Repository.Interfaces;
using MMAPI.Models;
using Moq;
using System.Threading.Tasks;
using MMAPI.Common.Exceptions;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MMAPI.Services.Tests
{
    [TestClass]
    public class FighterServiceTests
    {
        [TestMethod]
        [TestCategory("Services > FighterService > ValidateAndCreateAsync")]
        public async Task ValidateAndCreateAsync_Valid_ShouldReturnGuid()
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
            var actualGuidString = await fighterService.ValidateAndCreateAsync(fighter);

            // Assert
            Assert.AreEqual(new Guid(expectedGuidString), actualGuidString);
            fighterRepoMock.Verify(r => r.CreateAsync(fighter), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > FighterService > ValidateAndCreateAsync")]
        public async Task ValidateAndCreateAsync_Invalid_ShouldThrow()
        {
            // Arrange
            var fighter = new Fighter();
            var fighterRepoMock = new Mock<IRepository<Fighter>>();
            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<ValidationFailedException>(async () =>
            {
                await fighterService.ValidateAndCreateAsync(fighter);
            });
            fighterRepoMock.Verify(r => r.CreateAsync(fighter), Times.Never());
        }

        [TestMethod]
        [TestCategory("Services > FighterService > ValidateAndCreateAsync")]
        public async Task ValidateAndCreateAsync_Null_ShouldThrow()
        {
            // Arrange
            Fighter fighter = null;
            var fighterRepoMock = new Mock<IRepository<Fighter>>();
            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<ValidationFailedException>(async () =>
            {
                await fighterService.ValidateAndCreateAsync(fighter);
            });
            fighterRepoMock.Verify(r => r.CreateAsync(fighter), Times.Never());
        }

        [TestMethod]
        [TestCategory("Services > FighterService > ExistsAsync")]
        public async Task ExistsAsync_New_ShouldReturnFalse()
        {
            // Arrange
            var fighter = new Fighter
            {
                FirstName = "Test",
                LastName = "Fighter",
                Reach = 10,
                Height = 10,
                DateOfBirth = new DateTimeOffset(1999, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            var fighterRepoMock = new Mock<IRepository<Fighter>>();
            fighterRepoMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Fighter, bool>>>()))
                           .ReturnsAsync(new List<Fighter>());

            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act
            var actualExists = await fighterService.ExistsAsync(fighter);

            // Assert
            Assert.AreEqual(false, actualExists);
            fighterRepoMock.Verify(r => r.FindAsync(It.IsAny<Expression<Func<Fighter, bool>>>()), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > FighterService > ExistsAsync")]
        public async Task ExistsAsync_Exists_ShouldReturnFalse()
        {
            // Arrange
            var fighter = new Fighter
            {
                FirstName = "Test",
                LastName = "Fighter",
                Reach = 10,
                Height = 10,
                DateOfBirth = new DateTimeOffset(1999, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            var fighterRepoMock = new Mock<IRepository<Fighter>>();
            fighterRepoMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Fighter, bool>>>()))
                           .ReturnsAsync(new List<Fighter> { fighter });

            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act
            var actualExists = await fighterService.ExistsAsync(fighter);

            // Assert
            Assert.AreEqual(true, actualExists);
            fighterRepoMock.Verify(r => r.FindAsync(It.IsAny<Expression<Func<Fighter, bool>>>()), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > FighterService > ExistsAsync")]
        public async Task ExistsAsync_Null_ShouldThrowException()
        {
            // Arrange
            Fighter fighter = null;
            var fighterRepoMock = new Mock<IRepository<Fighter>>();
            var fighterService = new FighterService(fighterRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await fighterService.ExistsAsync(fighter);
            });
            fighterRepoMock.Verify(r => r.FindAsync(It.IsAny<Expression<Func<Fighter, bool>>>()), Times.Never());
        }
    }
}

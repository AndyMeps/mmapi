using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Common.Attributes;
using MMAPI.Repository.Interfaces;
using System;
using Moq;
using System.Threading.Tasks;
using MMAPI.Repository.Exceptions;
using MMAPI.Services.Exceptions;

namespace MMAPI.Services.Tests
{
    #region Class Stubs
    public class NameTestClass : IEntity
    {
        public Guid? Id { get; set; }
    }

    public class SimpleObject : IEntity
    {
        public Guid? Id { get; set; }
    }

    [CollectionName("test-collection-name")]
    public class SimpleAttributeObject : IEntity
    {
        public Guid? Id { get; set; }
    }
    #endregion

    [TestClass]
    public class DocumentServiceTests
    {
        [TestMethod]
        [TestCategory("Services > DocumentService > CollectionName")]
        public void CollectionName_DefaultForNameTestClass_ShouldMatch()
        {
            // Arrange
            var service = new DocumentService<NameTestClass>(new Mock<IRepository<NameTestClass>>().Object);

            // Act
            var collectionName = service.CollectionName;

            // Assert
            Assert.AreEqual("nametestclasses", collectionName);
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > CollectionName")]
        public void CollectionName_DefaultForSimpleObject_ShouldMatch()
        {
            // Arrange
            var service = new DocumentService<SimpleObject>(new Mock<IRepository<SimpleObject>>().Object);

            // Act
            var collectionName = service.CollectionName;

            // Assert
            Assert.AreEqual("simpleobjects", collectionName);
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > CollectionName")]
        public void CollectionName_CollectionNameAttribute_ShouldMatch()
        {
            // Arrange
            var fighterRepoMock = new Mock<IRepository<SimpleAttributeObject>>();
            var service = new DocumentService<SimpleAttributeObject>(new Mock<IRepository<SimpleAttributeObject>>().Object);

            // Act
            var collectionName = service.CollectionName;

            // Assert
            Assert.AreEqual("test-collection-name", collectionName);
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > CreateAsync")]
        public async Task CreateAsync_Null_ShouldThrowException()
        {
            // Arrange
            SimpleObject simpleObject = null;
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await service.CreateAsync(simpleObject);
            });

            documentRepoMock.Verify(m => m.CreateAsync(simpleObject), Times.Never());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > CreateAsync")]
        public async Task CreateAsync_Valid_ShouldReturnGuid()
        {
            // Arrange
            var expectedGuid = "26966ECE-4ED1-40D7-936B-5E7E6F2B79C3";
            var simpleObject = new SimpleObject();
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.CreateAsync(simpleObject)).ReturnsAsync(expectedGuid);
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.CreateAsync(simpleObject);

            // Assert
            Assert.AreEqual(expectedGuid, result);

            documentRepoMock.Verify(m => m.CreateAsync(simpleObject), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > CreateAsync")]
        public async Task CreateAsync_RepositoryException_ShouldThrowException()
        {
            // Arrange
            var simpleObject = new SimpleObject();
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.CreateAsync(simpleObject)).ThrowsAsync(new RepositoryException(RepositoryExceptionType.ServerError));
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<CreateFailedException>(async () =>
            {
                await service.CreateAsync(simpleObject);
            });

            documentRepoMock.Verify(m => m.CreateAsync(simpleObject), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > DeleteAsync")]
        public async Task DeleteAsync_ValidId_ShouldReturn()
        {
            var id = "314AE5B3-D209-4F3B-AD25-AF427EF4BD3A";
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.FromResult(default(object)));
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            await service.DeleteAsync(id);

            // Assert
            documentRepoMock.Verify(m => m.DeleteAsync(id), Times.Once());
        }
    }
}

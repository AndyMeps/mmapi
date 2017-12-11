using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Common.Attributes;
using MMAPI.Repository.Interfaces;
using System;
using Moq;
using System.Threading.Tasks;
using MMAPI.Repository.Exceptions;
using MMAPI.Services.Exceptions;
using System.Linq.Expressions;
using System.Collections.Generic;

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
        #region CollectionName
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
        #endregion

        #region CreateAsync
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
        #endregion

        #region DeleteAsync
        [TestMethod]
        [TestCategory("Services > DocumentService > DeleteAsync")]
        public async Task DeleteAsync_ValidId_ShouldReturn()
        {
            // Arrange
            var id = "314AE5B3-D209-4F3B-AD25-AF427EF4BD3A";
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.FromResult(default(object)));
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            await service.DeleteAsync(id);

            // Assert
            documentRepoMock.Verify(m => m.DeleteAsync(id), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > DeleteAsync")]
        public async Task DeleteAsync_Null_ShouldThrow()
        {
            // Arrange
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            await Assert.ThrowsExceptionAsync<InvalidResourceIdException>(async () =>
            {
                await service.DeleteAsync(null);
            });
            

            // Assert
            documentRepoMock.Verify(m => m.DeleteAsync(null), Times.Never());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > DeleteAsync")]
        public async Task DeleteAsync_NotAGuid_ShouldThrow()
        {
            // Arrange
            var id = "Abcefag32!2++0%&3t213t";
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            await Assert.ThrowsExceptionAsync<InvalidResourceIdException>(async () =>
            {
                await service.DeleteAsync(id);
            });


            // Assert
            documentRepoMock.Verify(m => m.DeleteAsync(id), Times.Never());
        }
        #endregion

        #region ExistsAsync
        [TestMethod]
        [TestCategory("Services > DocumentService > ExistsAsync")]
        public async Task ExistsAsync_ValidExpressionSingle_ShouldReturnTrue()
        {
            // Arrange
            Expression<Func<SimpleObject, bool>> expression = d => d.Id.ToString() == "314AE5B3-D209-4F3B-AD25-AF427EF4BD3A";
            var testObject = new SimpleObject { Id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A") };
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.FindAsync(expression))
                            .ReturnsAsync(new List<SimpleObject> { testObject });
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.ExistsAsync(expression);

            // Assert
            Assert.AreEqual(true, result);
            documentRepoMock.Verify(r => r.FindAsync(expression), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > ExistsAsync")]
        public async Task ExistsAsync_ValidExpressionMulti_ShouldReturnTrue()
        {
            // Arrange
            Expression<Func<SimpleObject, bool>> expression = d => true;
            var testObject = new SimpleObject { Id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A") };
            var testObject2 = new SimpleObject { Id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A") };
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.FindAsync(expression))
                            .ReturnsAsync(new List<SimpleObject> { testObject, testObject2 });
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.ExistsAsync(expression);

            // Assert
            Assert.AreEqual(true, result);
            documentRepoMock.Verify(r => r.FindAsync(expression), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > ExistsAsync")]
        public async Task ExistsAsync_InvalidExpressionEmpty_ShouldReturnFalse()
        {
            // Arrange
            Expression<Func<SimpleObject, bool>> expression = d => d.Id.ToString() == "314AE5B3-D209-4F3B-AD25-AF427EF4BD3B";
            var testObject = new SimpleObject { Id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A") };
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.FindAsync(expression))
                            .ReturnsAsync(new List<SimpleObject> {});
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.ExistsAsync(expression);

            // Assert
            Assert.AreEqual(false, result);
            documentRepoMock.Verify(r => r.FindAsync(expression), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > ExistsAsync")]
        public async Task ExistsAsync_InvalidExpressionNull_ShouldReturnFalse()
        {
            // Arrange
            Expression<Func<SimpleObject, bool>> expression = d => d.Id.ToString() == "314AE5B3-D209-4F3B-AD25-AF427EF4BD3B";
            var testObject = new SimpleObject { Id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A") };
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            List<SimpleObject> resultMock = null;
            documentRepoMock.Setup(r => r.FindAsync(expression))
                            .ReturnsAsync(resultMock);
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.ExistsAsync(expression);

            // Assert
            Assert.AreEqual(false, result);
            documentRepoMock.Verify(r => r.FindAsync(expression), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > ExistsAsync")]
        public async Task ExistsAsync_NullExpression_ShouldThrow()
        {
            // Arrange
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act, Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await service.ExistsAsync(null);
            });            
            documentRepoMock.Verify(r => r.FindAsync(null), Times.Never());
        }
        #endregion

        #region FindByIdAsync
        [TestMethod]
        [TestCategory("Services > DocumentService > FindByIdAsync")]
        public async Task FindByIdAsync_ValidId_ShouldReturn()
        {
            // Arrange
            var id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A");
            var simpleObjectResult = new SimpleObject { Id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A") };
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.FindByIdAsync(id)).ReturnsAsync(simpleObjectResult);
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.FindByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id.ToString(), result.Id.ToString());
            documentRepoMock.Verify(r => r.FindByIdAsync(id), Times.Once());
        }

        [TestMethod]
        [TestCategory("Services > DocumentService > FindByIdAsync")]
        public async Task FindByIdAsync_InvalidId_ShouldReturnNull()
        {
            // Arrange
            var id = new Guid("314AE5B3-D209-4F3B-AD25-AF427EF4BD3A");
            SimpleObject simpleObjectResult = null;
            var documentRepoMock = new Mock<IRepository<SimpleObject>>();
            documentRepoMock.Setup(r => r.FindByIdAsync(id)).ReturnsAsync(simpleObjectResult);
            var service = new DocumentService<SimpleObject>(documentRepoMock.Object);

            // Act
            var result = await service.FindByIdAsync(id);

            Assert.IsNull(result);
            documentRepoMock.Verify(r => r.FindByIdAsync(id), Times.Once());
        }
        #endregion
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Common.Attributes;
using MMAPI.Repository.Interfaces;
using System;

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
    public class DocumentCollectionServiceTests
    {
        [TestMethod]
        [TestCategory("Services > DocumentCollectionService > CollectionName")]
        public void CollectionName_DefaultForNameTestClass_ShouldMatch()
        {
            var service = new DocumentService<NameTestClass>("https://documents.azure.com:443", "1234", "dbName");

            Assert.AreEqual("nametestclasses", service.CollectionName);
        }

        [TestMethod]
        [TestCategory("Services > DocumentCollectionService > CollectionName")]
        public void CollectionName_DefaultForSimpleObject_ShouldMatch()
        {
            var service = new DocumentService<SimpleObject>("https://documents.azure.com:443", "1234", "dbName");

            Assert.AreEqual("simpleobjects", service.CollectionName);
        }

        [TestMethod]
        [TestCategory("Services > DocumentCollectionService > CollectionName")]
        public void CollectionName_CollectionNameAttribute_ShouldMatch()
        {
            var service = new DocumentService<SimpleAttributeObject>("https://documents.azure.com:443", "1234", "dbName");

            Assert.AreEqual("test-collection-name", service.CollectionName);
        }
    }
}

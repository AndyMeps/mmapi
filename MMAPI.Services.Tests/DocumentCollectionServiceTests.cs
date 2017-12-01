using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Services.Attributes;
using MMAPI.Services.Interfaces;
using System;

namespace MMAPI.Services.Tests
{
    #region Class Stubs
    public class NameTestClass : IDocumentEntity
    {
        public Guid? Id { get; set; }
    }

    public class SimpleObject : IDocumentEntity
    {
        public Guid? Id { get; set; }
    }

    [CollectionName("test-collection-name")]
    public class SimpleAttributeObject : IDocumentEntity
    {
        public Guid? Id { get; set; }
    }
    #endregion

    [TestClass]
    public class DocumentCollectionServiceTests
    {
        [TestMethod]
        public void CollectionName_DefaultForNameTestClass_ShouldMatch()
        {
            var service = new DocumentCollectionService<NameTestClass>("uri", "authKey", "dbName");

            Assert.AreEqual("nametestclasses", service.CollectionName);
        }

        [TestMethod]
        public void CollectionName_DefaultForSimpleObject_ShouldMatch()
        {
            var service = new DocumentCollectionService<SimpleObject>("uri", "authKey", "dbName");

            Assert.AreEqual("simpleobjects", service.CollectionName);
        }

        [TestMethod]
        public void CollectionName_CollectionNameAttribute_ShouldMatch()
        {
            var service = new DocumentCollectionService<SimpleAttributeObject>("uri", "authKey", "dbName");

            Assert.AreEqual("test-collection-name", service.CollectionName);
        }
    }
}

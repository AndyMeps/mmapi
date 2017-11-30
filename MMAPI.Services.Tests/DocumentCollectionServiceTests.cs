using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMAPI.Services.Interfaces;
using System;

namespace MMAPI.Services.Tests
{
    public class NameTestClass : IDocumentEntity
    {
        public Guid? Id { get; set; }
    }

    public class SimpleObject : IDocumentEntity
    {
        public Guid? Id { get; set; }
    }

    [TestClass]
    public class DocumentCollectionServiceTests
    {
        [TestMethod]
        public void NameTestClassTest()
        {
            var service = new DocumentCollectionService<NameTestClass>("uri", "authKey", "dbName");

            Assert.AreEqual("nametestclasses", service.CollectionName);
        }

        [TestMethod]
        public void SimpleObjectTest()
        {
            var service = new DocumentCollectionService<SimpleObject>("uri", "authKey", "dbName");

            Assert.AreEqual("simpleobjects", service.CollectionName);
        }
    }
}

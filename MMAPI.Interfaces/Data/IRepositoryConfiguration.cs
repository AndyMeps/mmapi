using System;

namespace MMAPI.Interfaces.Data
{
    public interface IRepositoryConfiguration
    {
        Uri Endpoint { get; set; }
        string AuthKey { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
        Uri GetDocumentCollectionUri();
    }
}

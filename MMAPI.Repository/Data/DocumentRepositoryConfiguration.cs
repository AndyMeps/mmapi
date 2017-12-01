using Microsoft.Azure.Documents.Client;
using MMAPI.Interfaces.Data;
using System;

namespace MMAPI.Repository.Data
{
    public class DocumentRepositoryConfiguration : IRepositoryConfiguration
    {
        public Uri Endpoint { get; set; }
        public string AuthKey { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }

        public DocumentRepositoryConfiguration(string endpointUri, string authKey, string database, string collection)
        {
            if (string.IsNullOrWhiteSpace(endpointUri)) throw new ArgumentException("endpoint uri required", "endpointUri");
            if (string.IsNullOrWhiteSpace(authKey)) throw new ArgumentException("auth key required", "authKey");
            if (string.IsNullOrWhiteSpace(database)) throw new ArgumentException("database required", "database");
            if (string.IsNullOrWhiteSpace(collection)) throw new ArgumentException("collection required", "collection");

            this.Endpoint = new Uri(endpointUri);
            this.AuthKey = authKey;
            this.Database = database;
            this.Collection = collection;
        }

        public Uri GetDocumentCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(Database, Collection);
        }
    }
}

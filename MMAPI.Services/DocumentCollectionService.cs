using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using MMAPI.Services.Factories;
using MMAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MMAPI.Services
{
    public class DocumentCollectionService<TDocumentEntity> : IDocumentCollectionService<TDocumentEntity> where TDocumentEntity : IDocumentEntity
    {
        private string _uri;
        private string _authKey;
        private string _databaseName;

        public DocumentCollectionService(string uri, string authKey, string databaseName)
        {
            _authKey = authKey;
            _uri = uri;
            _databaseName = databaseName;
        }

        public async Task<string> CreateAsync(TDocumentEntity entity)
        {
            using (var client = GetClient)
            {
                var uri = UriFactory.CreateDocumentCollectionUri(_databaseName, CollectionName);
                Document result = await client.CreateDocumentAsync(uri, entity);
                return result.Id;
            }
        }

        public string CollectionName
        {
            get
            {
                return CollectionPropertyFactory.GetCollectionName<TDocumentEntity>();
            }
        }

        private DocumentClient GetClient
        {
            get
            {
                return new DocumentClient(new Uri(_uri), _authKey, new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct
                });
            }
        }
    }
}

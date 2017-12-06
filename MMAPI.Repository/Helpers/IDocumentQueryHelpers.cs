using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMAPI.Repository.Helpers
{
    public static class IDocumentQueryHelpers
    {
        public async static Task<IEnumerable<T>> GetAllResultsAsync<T>(this IDocumentQuery<T> queryAll)
        {
            var list = new List<T>();

            while (queryAll.HasMoreResults)
            {
                var docs = await queryAll.ExecuteNextAsync<T>();

                foreach (var d in docs)
                {
                    list.Add(d);
                }
            }

            return list;
        }

        public async static Task<T> FirstOrDefault<T>(this IDocumentQuery<T> query)
        {
            while (query.HasMoreResults)
            {
                var docs = await query.ExecuteNextAsync<T>();

                foreach (var d in docs)
                {
                    return d;
                }
            }

            return default(T);
        }
    }
}

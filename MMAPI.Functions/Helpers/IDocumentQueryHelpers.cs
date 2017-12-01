using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMAPI.Functions.Helpers
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
    }
}

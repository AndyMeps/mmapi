using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Helpers
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

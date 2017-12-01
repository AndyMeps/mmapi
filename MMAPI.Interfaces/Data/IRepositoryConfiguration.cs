using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

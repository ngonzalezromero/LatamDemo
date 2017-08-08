using System.Collections.Generic;
using System.Threading.Tasks;
using Latam.Domain;

namespace Latam.Infraestructure
{
    public interface IRepositoryFactory
    {
        IRepository GetProvider(Provider provider);
    }
}

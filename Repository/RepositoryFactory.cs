using System.IO;
using Latam.Domain;
using Latam.Infraestructure;

namespace Latam.Repository
{
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        private readonly EFRepository _eFRepository;
        private readonly MemoryRepository _memoryRepository;

        public RepositoryFactory(EFRepository eFRepository, MemoryRepository memoryRepository)
        {
            _eFRepository = eFRepository;
            _memoryRepository = memoryRepository;
        }

        public IRepository GetProvider(Provider provider)
        {
            switch (provider)
            {
                case Provider.DataBase:
                    return _eFRepository;
                case Provider.Memory:
                    return _memoryRepository;
                default:
                    throw new InvalidDataException();
            }
        }

    }
}
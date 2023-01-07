using Dmail.Data.Context;
using Dmail.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Domain.Factories
{
    public static class RepositoryFactory
    {
        public static TRepo Create<TRepo>() where TRepo : BaseRepository
        {
            var dbContext = DBContextFactory.CreateDBContext();
            var repositoryInstance = Activator.CreateInstance(typeof(TRepo), dbContext) as TRepo;

            return repositoryInstance!;
        }
    }
}
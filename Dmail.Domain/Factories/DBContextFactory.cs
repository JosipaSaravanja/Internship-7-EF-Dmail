using Dmail.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Dmail.Domain.Factories
{
    public static class DBContextFactory
    {
        public static DmailDBContext CreateDBContext()
        {
            var options = new DbContextOptionsBuilder()
            .UseNpgsql(ConfigurationManager.ConnectionStrings["DmailApp"].ConnectionString)
            .Options;

            return new DmailDBContext(options);
        }
    }
}
    

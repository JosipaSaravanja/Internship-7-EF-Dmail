using Dmail.Data.Entitets;
using Dmail.Data.Context;
using Dmail.Domain.Enums;

namespace Dmail.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DmailDBContext context;

        protected BaseRepository(DmailDBContext context)
        {
            this.context = context;
        }

        protected ResponseResultType SaveChanges()
        {
            if (context.SaveChanges() > 0)
                return ResponseResultType.Succeeded;
            return ResponseResultType.NoChanges;
        }
    }
}
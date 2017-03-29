using System.Runtime.Remoting.Messaging;

namespace Putidms.Domain.DAL
{
    public class ContextFactory
    {
        public static EFDbContext CurrentContext()
        {
            EFDbContext _nContext = CallContext.GetData("PutidmsContext") as EFDbContext;
            if (_nContext == null)
            {
                _nContext = new EFDbContext();
                CallContext.SetData("PutidmsContext", _nContext);
            }
            return _nContext;
        }
    }
}

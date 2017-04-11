using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FortuneCommon
{
    [ServiceContract]
    public interface ICookieService
    {
        [OperationContract]
        string GetCookie();

    }
}

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
        [OperationContract(Name = "GetCookie2")] //wcf doesn't like when methods end with "Async"
        Task<string> GetCookieAsync();

    }
}

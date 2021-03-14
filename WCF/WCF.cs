using System.Collections.Generic;
using System.ServiceModel;
using DLL;

namespace WCF
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Dictionary<string,int> GetTXT(string value);
    }

}

// реализация интерфейса

using System.Collections.Generic;
using DLL;

namespace WCF
{
    public class Service1 : IService1
    {
        public Dictionary<string,int> GetTXT(string value)
        {
            Class1 dll = new Class1();
            return dll.Catcher2(value);
        }
    }
}

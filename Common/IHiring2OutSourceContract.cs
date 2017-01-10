using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Entities;
namespace Common
{
    [ServiceContract(CallbackContract = typeof(IHiring2OutSourceContract_CallBack))]
    public interface IHiring2OutSourceContract
    {
        [OperationContract ]
        bool Introduce(Company company);
        [OperationContract]
        bool AnswerToRequest(Company company);
		[OperationContract]
		bool CloseCompany(Company company);
	}

    [ServiceContract]
    public interface IHiring2OutSourceContract_CallBack
    {
        [OperationContract]
        bool SendRequest(string adress,Company company);
    }
}

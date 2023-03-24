using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;


/// <summary>
/// Summary description for SecurityService_Interface
/// </summary>
/// 
 [ServiceContract]
interface INavigationSystemService
{
     [OperationContract]
    string GetHeaderMenu(bool CheckAuthentication);
   
}
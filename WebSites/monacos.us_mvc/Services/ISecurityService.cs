using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;


/// <summary>
/// Summary description for SecuritySystemService_Interface
/// </summary>
/// 
 [ServiceContract]
interface ISecurityService
{

    [OperationContract]
    bool Login(String strUserName, String strPassword);

    [OperationContract]
    bool Logout();

    [OperationContract]
    string GetSessionID();

    [OperationContract]
    bool GetLogInStatus();

    [OperationContract]
    bool GetLogInStatusRole(string RoleName);




}
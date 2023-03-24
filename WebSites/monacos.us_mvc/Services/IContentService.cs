using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;
using monacos.us.dtos;


/// <summary>
/// Summary description for IContentService
/// </summary>
/// 

    
[ServiceContract]
public interface IContentService
{

    [OperationContract]
    List<ContentDTO> GetActiveContent(Int32 ContentArea_ID, bool IncludeOnlyCurrentlyPublished );

    [OperationContract]
    ContentDTO SelectContent(Int32 Content_ID);

    [OperationContract]
    void AddContent(ContentDTO objContentDTO );


    [OperationContract]
    void UpdateContent(ContentDTO objContentDTO);


    /*
    [OperationContract]
    string UpdateActiveContent(Int32 ContentArea_ID);
    */


   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

/// <summary>
/// Summary description for RestException
/// </summary>
/// 
[DataContract]
public class RestException
{
	public RestException(string errorInfo, string errorDetails)
    {
        ErrorInfo = errorInfo;
        ErrorDetails = errorDetails;
	}

    [DataMember]
    public string ErrorInfo { get; private set; }

    [DataMember]
    public string ErrorDetails { get; private set; }


}


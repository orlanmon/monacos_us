using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Http;
using System.ServiceModel.Web;


public partial class PostProxy : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string proxyURL = string.Empty;

        try
        {
            proxyURL = HttpUtility.UrlDecode(Request.QueryString["url"].ToString());



            if (proxyURL != string.Empty)
            {

              
                string strRequestData = null;
                byte[] bytes = null;
                StreamReader objStreamReader = null;

                objStreamReader = new StreamReader(Request.InputStream);

                strRequestData = objStreamReader.ReadToEnd();

                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

                bytes = encoding.GetBytes(strRequestData);


                HttpWebRequest objHttprequest = (HttpWebRequest)WebRequest.Create(proxyURL);
                objHttprequest.Method = Request.HttpMethod;

                if (Request.HttpMethod == "POST" || Request.HttpMethod == "PUT")
                {

                    objHttprequest.ContentLength = bytes.Length;
                    objHttprequest.ContentType = Request.ContentType;


                    Stream objOutputStream = objHttprequest.GetRequestStream();

                    objOutputStream.Write(bytes, 0, bytes.Length);

                    objOutputStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)objHttprequest.GetResponse();

                string contentType = response.ContentType;
                Stream content = response.GetResponseStream();
                StreamReader contentReader = new StreamReader(content);

                Response.Clear();

                Response.ContentType = contentType;
                Response.Write(contentReader.ReadToEnd());

              


            }
        }
        catch(System.Exception objException )
        {

            throw new WebException(objException.Message, WebExceptionStatus.SendFailure);

        }
    }

    protected override void OnInit(EventArgs e)
    {
        Response.ClearHeaders();
        Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
        Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
        Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1 
        Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1 
        Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1 
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1 
        Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1 
        Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.1

        base.OnInit(e);
    }
}

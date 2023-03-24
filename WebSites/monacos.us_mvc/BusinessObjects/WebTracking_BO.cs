using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Data;

namespace monacos.us_mvc.BusinessObjects
{


    /// <summary>
    /// Summary description for WebTracking_BO
    /// </summary>
    public class WebTracking_BO
    {
        public WebTracking_BO()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private string _DatabaseConnection = null;


        public string DatabaseConnection
        {

            get
            {
                return _DatabaseConnection;
            }

            set
            {
                _DatabaseConnection = value;
            }

        }


        public Int32 WebSessionCount()
        {
            Int32 WebSessionCount = 0;


            System.Data.SqlClient.SqlConnection objSqlConnection = null;
            System.Data.SqlClient.SqlCommand objSqlCommand = null;

            try
            {


                objSqlConnection = new System.Data.SqlClient.SqlConnection();
                objSqlConnection.ConnectionString = _DatabaseConnection;


                objSqlCommand = objSqlConnection.CreateCommand();
                objSqlCommand.CommandText = "SELECT Count(*) FROM  WebSessions";

                objSqlCommand.Connection.Open();


                WebSessionCount = Convert.ToInt32(objSqlCommand.ExecuteScalar());



                objSqlCommand.Connection.Close();

            }
            catch (Exception e)
            {
                // Do Nothing
            }
            finally
            {

                if (objSqlCommand != null && objSqlCommand.Connection != null && objSqlCommand.Connection.State == ConnectionState.Open)
                {

                    objSqlCommand.Connection.Close();

                }
            }

            return WebSessionCount;

        }


        public void RecordSessionInformation(HttpRequest objHttpRequest)
        {

            string SessionID = "";
            string Header_Value = "";
            string DatabaseConnectionString = "";
            Int32 WebSessionID = 0;



            System.Data.SqlClient.SqlConnection objSqlConnection;
            System.Data.SqlClient.SqlCommand objSqlCommand;
            System.Data.SqlClient.SqlParameter objSqlParameter;


            try
            {

                HttpSessionState objSessionState = HttpContext.Current.Session;

                SessionID = objSessionState.SessionID;


                DatabaseConnectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnectionString"];

                objSqlConnection = new System.Data.SqlClient.SqlConnection();
                objSqlConnection.ConnectionString = DatabaseConnectionString;


                objSqlCommand = objSqlConnection.CreateCommand();
                objSqlCommand.CommandText = "INSERT INTO  WebSessions ( SessionID ) VALUES ( @SessionID ) Set @WebSessionID = SCOPE_IDENTITY()";
                objSqlCommand.Parameters.AddWithValue("@SessionID", SessionID);


                objSqlParameter = new System.Data.SqlClient.SqlParameter("@WebSessionID", SqlDbType.Int);
                objSqlParameter.Direction = ParameterDirection.Output;
                objSqlCommand.Parameters.Add(objSqlParameter);



                objSqlCommand.Connection.Open();

                objSqlCommand.ExecuteNonQuery();

                objSqlCommand.Connection.Close();


                WebSessionID = (Int32)objSqlCommand.Parameters["@WebSessionID"].Value;


                objSqlCommand.Dispose();

                objSqlCommand = new SqlCommand();
                objSqlCommand.CommandText = "INSERT INTO  WebSessionInformation ( WebSessionID, WebSessionServerVariableName, WebSessionServerVariableValue ) VALUES ( @WebSessionID, @WebSessionServerVariableName, @WebSessionServerVariableValue ) ";
                objSqlCommand.Connection = objSqlConnection;

                objSqlCommand.Parameters.Add("@WebSessionID", SqlDbType.Int);
                objSqlCommand.Parameters.Add("@WebSessionServerVariableName", SqlDbType.VarChar, 100);
                objSqlCommand.Parameters.Add("@WebSessionServerVariableValue", SqlDbType.VarChar, 100);


                objSqlCommand.Connection.Open();


                Header_Value = objHttpRequest.ServerVariables["SERVER_SOFTWARE"];


                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "SERVER_SOFTWARE";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;

                objSqlCommand.ExecuteNonQuery();


                Header_Value = objHttpRequest.ServerVariables["SERVER_PORT"];


                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "SERVER_PORT";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;

                objSqlCommand.ExecuteNonQuery();


                Header_Value = objHttpRequest.ServerVariables["SERVER_NAME"];

                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "SERVER_NAME";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;

                objSqlCommand.ExecuteNonQuery();



                Header_Value = objHttpRequest.ServerVariables["HTTP_USER_AGENT"];

                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "HTTP_USER_AGENT";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;

                objSqlCommand.ExecuteNonQuery();



                Header_Value = objHttpRequest.ServerVariables["REQUEST_METHOD"];

                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "REQUEST_METHOD";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;

                objSqlCommand.ExecuteNonQuery();


                Header_Value = objHttpRequest.ServerVariables["REMOTE_ADDR"];

                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "REMOTE_ADDR";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;


                objSqlCommand.ExecuteNonQuery();



                Header_Value = objHttpRequest.ServerVariables["REMOTE_HOST"];

                objSqlCommand.Parameters["@WebSessionID"].Value = WebSessionID;
                objSqlCommand.Parameters["@WebSessionServerVariableName"].Value = "REMOTE_HOST";
                objSqlCommand.Parameters["@WebSessionServerVariableValue"].Value = Header_Value;

                objSqlCommand.ExecuteNonQuery();




                objSqlCommand.Connection.Close();



            }
            catch
            {
                // Do Nothing
            }


        }


    }
}

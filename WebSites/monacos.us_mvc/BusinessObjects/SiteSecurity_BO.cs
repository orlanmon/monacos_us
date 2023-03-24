using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace monacos.us_mvc.BusinessObjects
{

    public class UserRoles
    {
        private Dictionary<string, Role> _objUserRoles = new Dictionary<string, Role>();

        public Role this[int Index]
        {
            get
            {

                string[] KeyArray = _objUserRoles.Keys.ToArray();

                if ((Index >= 0) && (Index < KeyArray.Count()))
                {
                    if (_objUserRoles[KeyArray[Index]] != null)
                    {
                        return _objUserRoles[KeyArray[Index]];
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }


            }
        }

        public Role this[string RoleName]
        {
            get
            {
                if (this._objUserRoles[RoleName] != null)
                {
                    return this._objUserRoles[RoleName];
                }
                else
                {
                    return null;
                }

            }

        }

        public int Count()
        {
            return _objUserRoles.Count;
        }

        public void Add(Role objUserRole)
        {
            _objUserRoles.Add(objUserRole.Role_Name, objUserRole);

        }

    }


    public class Role
    {

        public Int32 Resource_ID = 0;
        public Int32 Resource_Role_ID = 0;
        public string Role_Name = "";
        public string Role_Description = "";

    }


    public class User
    {
        public Int32 User_ID;
        public string User_Login;
        public string User_Password;

    }




    /// <summary>
    /// Summary description for SiteSecurity_BO
    /// </summary>
    public class SiteSecurity_BO
    {
        public SiteSecurity_BO()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        string _DatabaseConnection;

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

        public Authentication GetAuthentication(string Username, string Password, string ResourceName)
        {
            Authentication objAuthentication = new Authentication();

            DataTable objTable_Roles = null;
            StringBuilder objSQL = null;

            SqlCommand objSqlCommand = null;
            SqlDataAdapter objSqlDataAdapter = null;
            SqlConnection objSqlConnection = new SqlConnection();


            objSqlConnection.ConnectionString = this._DatabaseConnection;


            // Query User

            objSqlCommand = new SqlCommand();
            objSqlCommand.Connection = objSqlConnection;
            objSqlCommand.CommandText = "Select * FROM Users WHERE User_Login = @User_Login AND  User_Password = @User_Password";
            objSqlCommand.CommandType = CommandType.Text;
            objSqlCommand.Parameters.AddWithValue("@User_Login", Username);
            objSqlCommand.Parameters.AddWithValue("@User_Password", Password);

            objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);


            objTable_Roles = new DataTable();


            objSqlDataAdapter.Fill(objTable_Roles);

            objSqlCommand = null;
            objSqlDataAdapter = null;

            if (objTable_Roles.Rows.Count == 0)
            {
                objAuthentication = null;
            }
            else
            {

                // Populate User Information

                objAuthentication.LoginUser.User_ID = Convert.ToInt32(objTable_Roles.Rows[0]["User_ID"]);
                objAuthentication.LoginUser.User_Login = objTable_Roles.Rows[0]["User_Login"].ToString();
                objAuthentication.LoginUser.User_Password = objTable_Roles.Rows[0]["User_Password"].ToString();

                // Query User Roles for Resource

                objSqlCommand = new SqlCommand();
                objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);


                objSQL = new StringBuilder();


                objSQL.Append("SELECT  Resource_Roles.Resource_ID,  Resource_Roles.Role_Name,  Resource_Roles.Role_Description,  Resource_Roles.Resource_Role_ID ");
                objSQL.Append("FROM  Resource INNER JOIN ");
                objSQL.Append(" Resource_Roles ON  Resource.Resource_ID =  Resource_Roles.Resource_ID INNER JOIN ");
                objSQL.Append(" User_Role_Xref ON  Resource_Roles.Resource_Role_ID =  User_Role_Xref.Resource_Role_ID INNER JOIN ");
                objSQL.Append(" Users ON  User_Role_Xref.User_ID =  Users.User_ID ");
                objSQL.Append("WHERE ( Users.User_ID = @User_ID)");
                objSQL.Append(" AND ( Resource.Resource_Name = @Resource_Name)");

                objSqlCommand.CommandText = objSQL.ToString();
                objSqlCommand.Connection = objSqlConnection;
                objSqlCommand.CommandType = CommandType.Text;
                objSqlCommand.Parameters.AddWithValue("@User_ID", objAuthentication.LoginUser.User_ID);
                objSqlCommand.Parameters.AddWithValue("@Resource_Name", ResourceName);

                objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);

                objTable_Roles = new DataTable();


                objSqlDataAdapter.Fill(objTable_Roles);

                objSqlCommand = null;
                objSqlDataAdapter = null;
                objSQL = null;

                Role objRole = null;


                foreach (DataRow objRoleDataRow in objTable_Roles.Rows)
                {
                    objRole = new Role();

                    objRole.Resource_ID = Convert.ToInt32(objRoleDataRow["Resource_ID"]);
                    objRole.Role_Description = objRoleDataRow["Role_Description"].ToString();
                    objRole.Role_Name = objRoleDataRow["Role_Name"].ToString();
                    objRole.Resource_Role_ID = Convert.ToInt32(objRoleDataRow["Resource_Role_ID"]);

                    // Add Role
                    objAuthentication.UserRoles.Add(objRole);

                    objRole = null;

                }

                objSqlCommand = null;
                objSqlDataAdapter = null;
                objSQL = null;


            }



            return objAuthentication;
        }

    }

    public class Authentication
    {

        private UserRoles _objUserRoles = new UserRoles();

        public UserRoles UserRoles
        {
            get
            {
                return _objUserRoles;
            }
        }

        private User _objUser = new User();

        public User LoginUser
        {
            get
            {
                return this._objUser;
            }


        }

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using System.Text;
//using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace monacos.us_mvc.BusinessObjects
{


    [DataContract]
    [Serializable]
    public class MenuItem
    {
        [DataMember(Name = "text", Order = 1)]

        public string text = null;

        [DataMember(Name = "imageUrl", Order = 2)]
        public string imageUrl = null;

        [DataMember(Name = "url", Order = 3)]
        public string url = null;

        [DataMember(Name = "target", Order = 3)]
        public string target = null;

        [DataMember(Name = "items", Order = 4)]
        public IList<MenuItem> items = null;

        // Used Internally Only
        public string NavigationItemID = null;

    }



    /// <summary>
    /// Summary description for MenuSystem_BO
    /// </summary>
    public class NavigationSystem_BO
    {


        public NavigationSystem_BO()
        {
            //
            // TODO: Add constructor logic here
            //
        }


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


        private String _DatabaseConnection = "";


        public string BuildNavigationMenu(int NavigationTypeID, int Resource_RoleID)
        {

            String KendoMenuJSON = null;



            try
            {

                KendoMenuJSON = BuildNavigationMenu_RootNodes(NavigationTypeID, Resource_RoleID);

            }

            catch (System.Exception objException)
            {
                throw new Exception("MenuSystem_BO::BuildNavigationMenu Error: " + objException.Message);
            }

            finally
            {

            }

            return KendoMenuJSON;

        }


        public string BuildNavigationMenu_RootNodes(int Navigation_TypeID, int Resource_RoleID)
        {

            System.Data.SqlClient.SqlDataAdapter objSqlDataAdapter = null;
            System.Data.SqlClient.SqlCommand objSqCommand = null;
            System.Data.SqlClient.SqlConnection objSqlConnection = null;
            string strSQL = "";

            System.Data.DataTable objDataTable_RootNodes = null;
            System.Data.DataTable objDataTable_ChildNodes = null;
            MenuItem objMenuItem = null;
            //MenuItem RootLevelMenu = new MenuItem();
            List<MenuItem> RootLevelMenuList = new List<MenuItem>();
            String KendoMenuJSON = null;



            try
            {

                // This Query Also Has Some Additional Logic To Remove Navigation Items Better Rendered with one Root Item in a Tree View
                // But Rendered with out that Root Item in a Menu View
                strSQL = "SELECT Navigation_Items.Navigation_Item_Caption, Navigation_Items.Navigation_Item_URI, Navigation_Items.Navigation_Item_Level_Sort_Order, " +
                         " Navigation_Items.Parent_Navigation_Item_ID, NavigationItem_ResourceRole_Xref.Resource_Role_ID,  " +
                         " Navigation_Items.Navigation_Item_ID, Navigation_Types.Navigation_Type_ID, Navigation_Items.Navigation_Item_URI_Target,  Navigation_Items.Navigation_Item_Image " +
                         " FROM Navigation_Types INNER JOIN " +
                         " Navigation_Items ON Navigation_Types.Navigation_Type_ID = Navigation_Items.Navigation_Type_ID INNER JOIN " +
                         " NavigationItem_ResourceRole_Xref ON Navigation_Items.Navigation_Item_ID = NavigationItem_ResourceRole_Xref.Navigation_Item_ID " +
                         " WHERE  (NavigationItem_ResourceRole_Xref.Resource_Role_ID = @Resource_Role_ID) AND " +
                         " (Navigation_Types.Navigation_Type_ID = @Navigation_Type_ID) AND " +
                         "  ( Navigation_Items.Navigation_Item_Menu_Visible = 1 ) AND " +
                         " (  " +
                         " (Navigation_Items.Parent_Navigation_Item_ID IS NULL AND Navigation_Items.Navigation_Item_Menu_Visible = 1 ) " +
                         " OR ( " +
                             "  Navigation_Items.Parent_Navigation_Item_ID IN ( " +
                             "  SELECT Navigation_Items .Navigation_Item_ID " +
                             "  FROM Navigation_Types INNER JOIN " +
                             "  Navigation_Items ON Navigation_Types.Navigation_Type_ID = Navigation_Items.Navigation_Type_ID INNER JOIN " +
                             "  NavigationItem_ResourceRole_Xref ON Navigation_Items.Navigation_Item_ID = NavigationItem_ResourceRole_Xref.Navigation_Item_ID " +
                             "  WHERE  NavigationItem_ResourceRole_Xref.Resource_Role_ID = @Resource_Role_ID  AND Navigation_Items.Parent_Navigation_Item_ID IS NULL AND Navigation_Items.Navigation_Item_Menu_Visible = 0  AND  Navigation_Types.Navigation_Type_ID = @Navigation_Type_ID " +
                            " ) " +
                         " )" +
                        ") ORDER BY Navigation_Items.Navigation_Item_Level_Sort_Order ";

                try
                {

                    objSqlConnection = new SqlConnection(_DatabaseConnection);
                }

                catch (System.Exception objException)
                {
                    throw new Exception("Failure to connect to database. Error: " + objException.Message);
                }


                objSqCommand = new SqlCommand(strSQL, objSqlConnection);
                objSqCommand.Parameters.AddWithValue("@Navigation_Type_ID", Navigation_TypeID);
                objSqCommand.Parameters.AddWithValue("@Resource_Role_ID", Resource_RoleID);

                objSqlDataAdapter = new SqlDataAdapter(objSqCommand);

                objDataTable_RootNodes = new DataTable();

                objSqlDataAdapter.Fill(objDataTable_RootNodes);

                // Now Get All Child Nodes (Nested N Levels Deep) in One Data Table
                // For Given Navigation Type ID and Role

                strSQL = "SELECT Navigation_Items.Navigation_Item_Caption, Navigation_Items.Navigation_Item_URI, Navigation_Items.Navigation_Item_Level_Sort_Order, " +
                         " Navigation_Items.Parent_Navigation_Item_ID, NavigationItem_ResourceRole_Xref.Resource_Role_ID,  " +
                         " Navigation_Items.Navigation_Item_ID, Navigation_Types.Navigation_Type_ID, Navigation_Items.Navigation_Item_URI_Target, Navigation_Items.Navigation_Item_Image " +
                         " FROM Navigation_Types INNER JOIN " +
                         " Navigation_Items ON Navigation_Types.Navigation_Type_ID = Navigation_Items.Navigation_Type_ID INNER JOIN " +
                         " NavigationItem_ResourceRole_Xref ON Navigation_Items.Navigation_Item_ID = NavigationItem_ResourceRole_Xref.Navigation_Item_ID " +
                         " WHERE  (NavigationItem_ResourceRole_Xref.Resource_Role_ID = @Resource_Role_ID) AND (Navigation_Items.Parent_Navigation_Item_ID IS NOT NULL) AND " +
                         " (Navigation_Types.Navigation_Type_ID = @Navigation_Type_ID) AND " +
                         " (Navigation_Items.Navigation_Item_Menu_Visible = 1 )";


                objSqCommand = new SqlCommand(strSQL, objSqlConnection);
                objSqCommand.Parameters.AddWithValue("@Navigation_Type_ID", Navigation_TypeID);
                objSqCommand.Parameters.AddWithValue("@Resource_Role_ID", Resource_RoleID);

                objSqlDataAdapter = new SqlDataAdapter(objSqCommand);

                objDataTable_ChildNodes = new DataTable();

                objSqlDataAdapter.Fill(objDataTable_ChildNodes);




                // Start With Root Nodes 
                foreach (System.Data.DataRow objDataRow_Node in objDataTable_RootNodes.Rows)
                {

                    // Render Node

                    objMenuItem = new MenuItem();
                    objMenuItem.items = new List<MenuItem>();





                    if (objDataRow_Node["Navigation_Item_Caption"] != System.DBNull.Value)
                    {
                        objMenuItem.text = objDataRow_Node["Navigation_Item_Caption"].ToString().Trim();


                    }


                    if (objDataRow_Node["Navigation_Item_URI"] != System.DBNull.Value)
                    {

                        if (objDataRow_Node["Navigation_Item_URI"].ToString().IndexOf("http", 0) != -1)
                        {

                            // External URL Leaves As It Is
                            objMenuItem.url = objDataRow_Node["Navigation_Item_URI"].ToString().Trim();

                        }
                        else
                        {


                            // Web App MVC Route URI 
                            objMenuItem.url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + objDataRow_Node["Navigation_Item_URI"].ToString().Trim();





                        }


                    }


                    if (objDataRow_Node["Navigation_Item_URI_Target"] != System.DBNull.Value)
                    {
                        // Determine By www in url and not by this attribute
                        objMenuItem.target = objDataRow_Node["Navigation_Item_URI_Target"].ToString().Trim();
                    }



                    if (objDataRow_Node["Navigation_Item_Image"] != System.DBNull.Value)
                    {
                        objMenuItem.imageUrl = objDataRow_Node["Navigation_Item_Image"].ToString().Trim();

                    }


                    // Navigation Item ID
                    objMenuItem.NavigationItemID = objDataRow_Node["Navigation_Item_ID"].ToString().Trim();

                    //RootLevelMenu.items.Add(objMenuItem);
                    RootLevelMenuList.Add(objMenuItem);


                    // Render Children If Neccessary

                    BuildNavigationMenu_ChildNodes(ref objMenuItem, ref objDataTable_ChildNodes);

                }


                var settings = new JsonSerializerSettings();

                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.DefaultValueHandling = DefaultValueHandling.Ignore;


                KendoMenuJSON = Newtonsoft.Json.JsonConvert.SerializeObject(RootLevelMenuList, settings);


            }

            catch (System.Exception objException)
            {
                throw new Exception("MenuSystem_BO::BuildNavigationMenu_RootNodes Error: " + objException.Message);
            }

            finally
            {
                objSqlDataAdapter = null;
                objDataTable_RootNodes = null;
                objSqCommand = null;

            }

            return KendoMenuJSON;



        }


        public void BuildNavigationMenu_ChildNodes(ref MenuItem objParentMenuItem, ref System.Data.DataTable objChildNodeDataTable)
        {

            System.Data.DataView objDataView = null;
            MenuItem objMenuItem = null;



            try
            {


                objDataView = new DataView(objChildNodeDataTable);


                // Filter All Children Of Parent ID Passed In Via
                // Tree Node Value
                objDataView.RowFilter = "Parent_Navigation_Item_ID = " + Convert.ToInt32(objParentMenuItem.NavigationItemID);
                objDataView.Sort = "Navigation_Item_Level_Sort_Order ASC";


                foreach (System.Data.DataRowView objDataViewRow_Node in objDataView)
                {


                    // Render Node

                    objMenuItem = new MenuItem();
                    objMenuItem.items = new List<MenuItem>();

                    if (objDataViewRow_Node["Navigation_Item_Caption"] != System.DBNull.Value)
                    {
                        objMenuItem.text = objDataViewRow_Node["Navigation_Item_Caption"].ToString().Trim();

                    }


                    if (objDataViewRow_Node["Navigation_Item_URI"].ToString().IndexOf("http", 0) != -1)
                    {

                        // External URL Leaves As It Is
                        objMenuItem.url = objDataViewRow_Node["Navigation_Item_URI"].ToString().Trim();

                    }
                    else
                    {


                        // Web App MVC Route URI 
                        objMenuItem.url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + objDataViewRow_Node["Navigation_Item_URI"].ToString().Trim();


                    }



                    if (objDataViewRow_Node["Navigation_Item_URI_Target"] != System.DBNull.Value)
                    {


                        objMenuItem.target = objDataViewRow_Node["Navigation_Item_URI_Target"].ToString().Trim();

                    }


                    if (objDataViewRow_Node["Navigation_Item_Image"] != System.DBNull.Value)
                    {
                        objMenuItem.imageUrl = objDataViewRow_Node["Navigation_Item_Image"].ToString().Trim();

                    }

                    // Navigation Item ID
                    objMenuItem.NavigationItemID = objDataViewRow_Node["Navigation_Item_ID"].ToString().Trim();


                    objParentMenuItem.items.Add(objMenuItem);


                    // Render Children If Neccessary

                    BuildNavigationMenu_ChildNodes(ref objMenuItem, ref objChildNodeDataTable);


                }

            }

            catch (System.Exception objException)
            {
                throw new Exception("MenuSystem:_BO:BuildNavigationMenu_ChildNodes Error: " + objException.Message);
            }

            finally
            {
                objDataView = null;
            }


        }


    }
}

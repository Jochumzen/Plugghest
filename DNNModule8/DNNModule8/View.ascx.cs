/*
' Copyright (c) 2014  Plugghest.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using Plugghest.Modules.DNNModule8.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Data.SqlClient;
using System.Data;



using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Reflection;


using DotNetNuke;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Entities.Tabs;

using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common.Utilities;

namespace Plugghest.Modules.DNNModule8
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DNNModule8ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DNNModule8ModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if ((!IsPostBack))
                {
                    //PermGrid.TabID = -1;
                    //ParentsDropDownList.DataSource = TabController.GetPortalTabs(PortalId, TabId, true, false);
                    //ParentsDropDownList.DataBind();
                }

            }
            catch (Exception ex)
            {
                //failure
                Exceptions.ProcessModuleLoadException(this, ex);
            }


            try
            {
                var tc = new ItemController();
                rptItemList.DataSource = tc.GetItems(ModuleId);
                rptItemList.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void rptItemListOnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var lnkEdit = e.Item.FindControl("lnkEdit") as HyperLink;
                var lnkDelete = e.Item.FindControl("lnkDelete") as LinkButton;

                var pnlAdminControls = e.Item.FindControl("pnlAdmin") as Panel;

                var t = (Item)e.Item.DataItem;

                if (IsEditable && lnkDelete != null && lnkEdit != null && pnlAdminControls != null)
                {
                    pnlAdminControls.Visible = true;
                    lnkDelete.CommandArgument = t.ItemId.ToString();
                    lnkDelete.Enabled = lnkDelete.Visible = lnkEdit.Enabled = lnkEdit.Visible = true;

                    lnkEdit.NavigateUrl = EditUrl(string.Empty, string.Empty, "Edit", "tid=" + t.ItemId);

                    ClientAPI.AddButtonConfirm(lnkDelete, Localization.GetString("ConfirmDelete", LocalResourceFile));
                }
                else
                {
                    pnlAdminControls.Visible = false;
                }
            }
        }


        public void rptItemListOnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect(EditUrl(string.Empty, string.Empty, "Edit", "tid=" + e.CommandArgument));
            }

            if (e.CommandName == "Delete")
            {
                var tc = new ItemController();
                tc.DeleteItem(Convert.ToInt32(e.CommandArgument), ModuleId);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        protected void btntest_Click(object sender, EventArgs e)
        {
            int fn = Convert.ToInt32(txtfirstnumber.Text);
            int sn = Convert.ToInt32(txtsecondnumber.Text);
            int sum = fn + sn;
            txtresult.Text = sum.ToString();

            //SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=EmployeeDb;Integrated Security=True");
            //SqlCommand cmd = new SqlCommand("insert into Emp values('a','a',324234,'dsf','dfsdf')", con);
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            Insert();
        }

        protected void Insert()
        {
            var emp = new Employee();
            var empc = new EmpController();
            emp.UserName = "r";
            emp.Password = "a";
            emp.PhoneNo = 32132;
            emp.Address = "asdfsd";
            emp.EmailId = "r@sdfds.sd";
            empc.CreateEmp(emp);
        }



        //==--- new Page code ---==


        protected void btnAddPage_Click(object sender, EventArgs e)
        {
            TabPermissionCollection tab = new TabPermissionCollection();
            CreatePage(txtTabName.Text, txtTabTitle.Text, txtTabDesc.Text, txtTabKeyWords.Text,tab);
        }





        private void CreatePage(string PageName, string PageTitle, string Description, string Keywords, TabPermissionCollection Permissions, bool LoadDefaultModules = true)
        {
            TabController controller = new TabController();
            TabInfo newTab = new DotNetNuke.Entities.Tabs.TabInfo();
            TabPermissionCollection newPermissions = newTab.TabPermissions;
            PermissionProvider permissionProvider = new PermissionProvider();
            TabPermissionInfo infPermission = default(TabPermissionInfo);

            // set new page properties
            newTab.PortalID = PortalId;
            newTab.TabName = PageName;
            newTab.Title = PageTitle;
            newTab.Description = Description;
            newTab.KeyWords = Keywords;
            newTab.IsDeleted = false;
            newTab.IsSuperTab = false;
            newTab.IsVisible = true;
            newTab.DisableLink = false;
            newTab.IconFile = "";
            newTab.Url = "";
            newTab.ParentId = Convert.ToInt32(ParentsDropDownList.SelectedValue);

            // create new page
            controller.AddTab(newTab, LoadDefaultModules);

            // copy permissions selected in Permissions collection



            for (int index = 0; index <= (Permissions.Count - 1); index++)
            {
                infPermission = new TabPermissionInfo();

                infPermission.AllowAccess = Permissions[index].AllowAccess;
                infPermission.RoleID = Permissions[index].RoleID;
                infPermission.RoleName = Permissions[index].RoleName;
                infPermission.TabID = Permissions[index].TabID;
                infPermission.PermissionID = Permissions[index].PermissionID;

                //save permission info
                newPermissions.Add(infPermission, true);
                permissionProvider.SaveTabPermissions(newTab);
            }

            //create module on page
            //CreateModule(newTab.TabID, "MyHTMLModule", "ContentPane", "Text/HTML")
            //CreateModule(newTab.TabID, "MyModule", "ContentPane", "Custom_DNN_Module");

            // clear the cache
            //DataCache.ClearModuleCache(newTab.TabID);




            
        }


    }
}



    

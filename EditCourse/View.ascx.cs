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
using Plugghest.Base;
using Plugghest.Modules.EditCourse.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Plugghest.Modules.EditCourse
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from EditCourseModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : EditCourseModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["cid"] != null && Request.QueryString["cid"].Trim() != "")
                    {
                        string CID = this.Request.QueryString["cid"].ToString();
                        int courseid;
                        if (int.TryParse(CID, out courseid)) //check is number...
                        {
                            BindTree(courseid);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "StartupScriptforTree",
                            "alert('Please Select Course');", true);
                    }
                }
                catch (Exception exc) //Module failed to load
                {
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }
        }

        protected void btnAddHeading_Click(object sender, EventArgs e)
        {
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string json = hdnGetJosnResult.Value;
            //var person = js.Deserialize<Subject_Tree[]>(json).ToList();

            //SubjectController objcontroller = new SubjectController();
            //var abc = objcontroller.GetSubject_Item();

            //var tree = BuildTree(person);
        }

        public void BindTree(int courseId)
        {
            BaseHandler bh = new BaseHandler();
            var tree = bh.GetCourseItemsAsTree(courseId);

            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);

        }

        protected void btnAddplugg_Click(object sender, EventArgs e)
        {
            bool ischeck = CheckPlugg();
            if (ischeck)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Callfunction", "AddTempPlugg();",
                    true);
            }
        }

        protected Boolean CheckPlugg()
        {
            bool ischecked = true;

            lblPlugg.Text = "";
            BaseHandler ph = new BaseHandler();

            if (!string.IsNullOrEmpty(txtAddPlugg.Text))
            {
                int num;
                bool isNumeric = int.TryParse(txtAddPlugg.Text, out num); //check number.....
                if (isNumeric)
                {
                    Plugg p = ph.GetPlugg(num);

                    if (p != null)
                    {
                        lblPlugg.Text += p.Title;
                    }
                    else
                    {
                        lblPlugg.Text += num + ": Error – Plugg " + num + " does not exist" + "<br />";
                        ischecked = false;
                    }
                }
                else
                {
                    lblPlugg.Text = "Pluggs in wrong format.";
                    ischecked = false;
                }
            }

            return ischecked;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BaseHandler bh = new BaseHandler();
            var jss = new JavaScriptSerializer();
            int courseId = Convert.ToInt32(Page.Request.QueryString["cid"]);
            bh.SaveCourseItems(jss.Deserialize<List<CourseItem>>(hdnGetJosnResult.Value), courseId);
            BindTree(courseId);
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
    }
}
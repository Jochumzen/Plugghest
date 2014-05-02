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
                    if (this.Request.QueryString["cid"] != null && this.Request.QueryString["cid"].ToString().Trim() != "")
                    {
                        string CID = this.Request.QueryString["cid"].ToString();
                        int courseid;
                        if (int.TryParse(CID, out courseid))//check is number...
                        {
                            BindTree(courseid);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "StartupScriptforTree", "alert('Please Select Course');", true);
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


        public void BindTree(int courseid)
        {
            BaseHandler objcoursehdler = new BaseHandler();
            var Courselist = objcoursehdler.GetCourseItemsForTree(courseid);

            var tree = BuildTree(Courselist);

            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            hdnTreeData.Value = TheSerializer.Serialize(tree);



        }


        #region Create Tree

        //Recursive function for create tree....
        public IList<CourseItem> BuildTree(IEnumerable<CourseItem> source)
        {
            var groups = source.GroupBy(i => i.Mother);


            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        //To Add Child
        private void AddChildren(CourseItem node, IDictionary<int, List<CourseItem>> source)
        {
            if (source.ContainsKey(node.CourseItemID))
            {
                node.children = source[node.CourseItemID];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<CourseItem>();
            }
        }

        #endregion

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

        protected void btnAddplugg_Click(object sender, EventArgs e)
        {
            //bool ischeck = CheckPlugg();
            //if (ischeck)
            //{
            //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "Callfunction", "AddTempPlugg();", true);
            //}
        }

        protected Boolean CheckPlugg()
        {
            bool ischecked = true;

            lblPlugg.Text = "";
            BaseHandler ph = new BaseHandler();

            if (!string.IsNullOrEmpty(txtAddPlugg.Text))
            {
                int num;
                bool isNumeric = int.TryParse(txtAddPlugg.Text, out num);//check number.....
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

        public string SaveCourse(List<CourseItem> data)
        {
            System.Text.StringBuilder intReturnId = new System.Text.StringBuilder();
            if (data != null)
            {
                if (data.Count > 0)
                {
                    foreach (CourseItem item in data)
                    {
                        // save code 
                        Plugghest.Base.BaseHandler objCourseHandler = new BaseHandler();
                        CourseItem objcourseitem = new CourseItem();
                        objcourseitem.CourseItemID = item.CourseItemID;
                        objcourseitem.ItemID = item.ItemID;
                        objcourseitem.ItemType = item.ItemType;
                        if (item.Mother != null)
                        {
                            objcourseitem.Mother = Convert.ToInt32(item.Mother);
                        }
                        objcourseitem.CIOrder = item.CIOrder;
                        objcourseitem.CourseID = Convert.ToInt32(this.Request.QueryString["cid"]);
                        objcourseitem.CourseID = 25;
                        if (item.ItemType == 1 && objcourseitem.CourseItemID == 0)
                        {
                            CourseHeadings objcourseHeading = new CourseHeadings();
                            objcourseHeading.Title = item.Title;
                            objcourseHeading = objCourseHandler.CreateHeading(objcourseHeading);

                            objcourseitem.ItemID = objcourseHeading.HeadingID;
                        }

                        //txtAddPlugg.Text = (item.CourseItemID + item.CourseID + item.ItemType).ToString();

                        if (objcourseitem.CourseItemID == 0)
                        {


                            objCourseHandler.CreateCourseItem(objcourseitem);
                        }
                        else
                        {
                            objCourseHandler.UpdateCourseItem(objcourseitem);
                        }
                        intReturnId.Append(objcourseitem.CourseItemID).Append(",");
                        //List<CourseTree> data1 = data.Where(x => x.Mother == item.CourseItemID).ToList();
                        intReturnId.Append(SaveCourse(item.children));
                    }
                }
            }
            return intReturnId.ToString();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            Plugghest.Base.BaseHandler objCourseHandler = new BaseHandler();
            string jsonTreeRecord = hdnGetJosnResult.Value;
            var jss = new JavaScriptSerializer();
            var data = jss.Deserialize<List<CourseItem>>(jsonTreeRecord);

            List<string> strIDs = SaveCourse(data).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<CourseItem> objAllCourseItem = (List<CourseItem>)objCourseHandler.GetCourseItemsForCourse(Convert.ToInt32(this.Request.QueryString["cid"])).ToList();
            List<CourseItem> objCourseToDelete = objAllCourseItem.Where(x => !strIDs.Contains(x.CourseItemID.ToString())).ToList();
            foreach (CourseItem item in objCourseToDelete)
            {
                objCourseHandler.DeleteCourseItem(item);
            }
            BindTree(Convert.ToInt32(this.Request.QueryString["cid"].ToString()));
        }
    }
}
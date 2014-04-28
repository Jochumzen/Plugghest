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
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using Plugghest.Base;
using Plugghest.DNN;
using DotNetNuke.Common;


namespace Plugghest.Modules.CreateCourse
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CreateCourseModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CreateCourseModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCultureDropDownList();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void LoadCultureDropDownList()
        {
            try
            {
                Localization myLoc = new Localization();
                Localization.LoadCultureDropDownList(DDLanguage, CultureDropDownTypes.NativeName, myLoc.CurrentCulture);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            bool ischeck = CheckPlugg();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Boolean ischeck = CheckPlugg();

            if (!ischeck) //check validation
                return;

            Course c;
            c = SaveCourse();
            if (c.CourseId != 0) //0 means Error in Update/Save
                Response.Redirect(Globals.NavigateURL(c.TabId));


            //DNNHelper h = new DNNHelper();
            //h.AddCoursePage("C" + c.CourseId.ToString() + ": " + c.Title, "C" + c.CourseId);

            //Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" + "C" + c.CourseId + ".aspx");

        }

        protected Course SaveCourse()
        {
            BaseHandler bh = new BaseHandler();
            Course c = new Course();
            List<CourseItem> cis = new List<CourseItem>();
            ReadFromControls(c, cis);

            try
            {
                bh.CreateCourse(c, cis);  //Create CoursePage, Course and CourseItems (only Pluggs)
            }
            catch (Exception ex)
            {
                lblError.Text = "Failed to create a Course: " + ex.Message;
                Exceptions.LogException(ex);
                c.CourseId = 0;
            }

            return c;
        }

        protected void ReadFromControls(Course c, List<CourseItem>  cis)
        {
            c.Title = txtTitle.Text;
            c.CreatedInCultureCode = DDLanguage.SelectedValue;

            if (rdEditPlug.Text == "Any registered user")
                c.WhoCanEdit = EWhoCanEdit.Anyone;
            else
                c.WhoCanEdit = EWhoCanEdit.OnlyMe;
            
            c.CreatedOnDate = DateTime.Now;
            c.CreatedByUserId = this.UserId;
            c.ModifiedOnDate = DateTime.Now; ;
            c.ModifiedByUserId = this.UserId;
            c.Description = txtHtmlText.Text;

            // string is in form "44,45,48,52" holding PluggIDs
            string pluggtext = txtPluggs.Text.Trim();

            // Todo: Check that pluggtext is in the correct format before creating the Course

            CourseItem ci;
            if (!string.IsNullOrEmpty(pluggtext))
            {
                string[] itempluggs = pluggtext.Split(',');

                for (int i = 0; i < itempluggs.Length; i++)
                {
                    ci = new CourseItem();
                    ci.ItemID = Convert.ToInt32(itempluggs[i]);
                    ci.CIOrder = i + 1;
                    ci.ItemType = 0;
                    ci.Mother = 0;
                    cis.Add(ci);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected Boolean CheckPlugg()
        {
            bool ischecked = true;

            CIInfo.Text = "";
            BaseHandler ph = new BaseHandler();

            //string pluggtext = "12,56,34,45k,56";
            string pluggtext = txtPluggs.Text.Trim();
            if (!string.IsNullOrEmpty(pluggtext))
            {
                string[] itempluggs = pluggtext.Split(',');

                for (int i = 0; i < itempluggs.Length; i++)
                {
                    int num;
                    bool isNumeric = int.TryParse(itempluggs[i], out num);//check number.....
                    if (isNumeric)
                    {
                        Plugg p = ph.GetPlugg(num);

                        if (p != null)
                        {
                            CIInfo.Text += num + ": " + p.Title + "<br />";
                        }
                        else
                        {
                            CIInfo.Text += num + ": Error – Plugg " + num + " does not exist" + "<br />";
                            ischecked = false;
                        }
                    }
                    else
                    {
                        CIInfo.Text = "Pluggs in wrong format.";
                        ischecked = false;
                    }
                }
            }

            return ischecked;
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
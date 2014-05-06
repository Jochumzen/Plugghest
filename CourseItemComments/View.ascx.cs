/*
' Copyright (c) 2014  Pluggest.com
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
using Pluggest.Modules.CourseItemComments.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base;
using System.Collections.Generic;

namespace Pluggest.Modules.CourseItemComments
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CourseItemCommentsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CourseItemCommentsModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["cid"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["cid"])
                        && Request.QueryString["iid"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["iid"]))
                    {
                        string CID = this.Request.QueryString["cid"].ToString();
                        string IID = this.Request.QueryString["iid"].ToString();
                        int courseid;
                        int Itemid;
                        if (int.TryParse(CID, out courseid) && int.TryParse(IID, out Itemid)) //check is number...
                        {
                            BaseHandler bh = new BaseHandler();
                            List<CourseItemComment> objCourseItemComment = (List<CourseItemComment>)bh.GetCourseItemComment(courseid, Itemid);
                            if (objCourseItemComment.Count > 0)
                            {
                                labHtmlText.Text = objCourseItemComment[0].HtmlText; 
                            }
                        }

                       
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "StartupScriptforTree",
                            "alert('Please Select Course');", true);
                    }


                    string editQS = Request.QueryString["edit"];
                    if (editQS != null && !string.IsNullOrWhiteSpace(editQS) && editQS.ToLower() == "true" )
                    {
                        btnEditHTML.Style.Add("display", "block");
                    }
                    else
                    {
                        btnEditHTML.Style.Add("display", "none");
                    }


                }
                catch (Exception exc) //Module failed to load
                {
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }
        }

        protected void btnSaveHTML_Click(object sender, EventArgs e)
        {
            string CID = this.Request.QueryString["cid"].ToString();
            string IID = this.Request.QueryString["iid"].ToString();
            int courseid;
            int Itemid;
            if (int.TryParse(CID, out courseid) && int.TryParse(IID, out Itemid)) //check is number...
            {
                BaseHandler bh = new BaseHandler();
                List<CourseItemComment> objCourseItemComment = (List<CourseItemComment>)bh.GetCourseItemComment(courseid, Itemid);
                if (objCourseItemComment.Count > 0)
                {
                    objCourseItemComment[0].HtmlText = hdHTML.Value;
                    bh.UpdateCourseItemComment(objCourseItemComment[0]);
                   labHtmlText.Text= objCourseItemComment[0].HtmlText  ;
                }
            }
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
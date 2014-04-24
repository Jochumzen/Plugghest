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
using DotNetNuke.Entities.Tabs;
using Plugghest.Modules.PlugghestPanel.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Courses;
using Plugghest.DNN;

namespace Plugghest.Modules.PlugghestPanel
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from PlugghestPanelModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : PlugghestPanelModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
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

        protected void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            //CourseHandler ch = new CourseHandler();
            //Course c = new Course();
            //c.CourseId = Convert.ToInt32(tbDeleteCourseID);
            
        }

        protected void btnDeleteTab_Click(object sender, EventArgs e)
        {
            DNNHelper h =  new DNNHelper();
            TabInfo t;
            string s = tbDeleteTabID.Text;


            if (s.IndexOf(',') > -1)
            {
                string[] tabIDs = s.Split(',');

                for (int i = 0; i < tabIDs.Length; i++)
                {
                    t = new TabInfo();
                    t.TabID = Convert.ToInt32(tabIDs[i]);
                    h.DeleteTab(t);
                }                
            }
            else
            {
                int posOfDash = s.IndexOf('-');
                if (posOfDash > -1)
                {
                    string starts = s.Substring(0, posOfDash);
                    string ends = s.Substring(posOfDash+1,s.Length-posOfDash-1);
                    int startint = Convert.ToInt32(starts);
                    int endint = Convert.ToInt32(ends);
                    for (int tID = startint; tID <= endint; tID++)
                    {
                        t = new TabInfo();
                        t.TabID = tID;
                        h.DeleteTab(t);
                    }
                }
                else
                {
                    t = new TabInfo();
                    t.TabID = Convert.ToInt32(s);
                    h.DeleteTab(t);
                }
            }
            tbDeleteTabID.Text = "";
        }
    }
}
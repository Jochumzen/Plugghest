/*
' Copyright (c) 2014  Christoc.com
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
using Christoc.Modules.DisplayPluggeTitle.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base;

namespace Christoc.Modules.DisplayPluggeTitle
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DisplayPluggeTitleModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DisplayPluggeTitleModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
                    PluggContainer p = GetPluggContainer(curlan);
                    if (p.ThePlugg.CreatedInCultureCode == curlan)
                    {
                        divTranslatedPluggeTitle.Style.Add("display", "none");
                        return;
                    }
                    SetValues(p);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private PluggContainer GetPluggContainer(string curlan)
        {
            BaseHandler plugghandler = new BaseHandler();
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);


            PluggContainer p = new PluggContainer();
            p.ThePlugg = plugghandler.GetPlugg(pluggid);
            p.CultureCode = curlan;
            p.LoadTitle();
            return p;
        }

        private void SetValues(PluggContainer p)
        {
            lblTranslatedPluggeTitle.Text = p.TheTitle.Text;
            txtTranslatedPluggeTitle.Text = p.TheTitle.Text;
            btnCancelTranslatedPluggeTitle.Style.Add("display", "none");
            if (p.TheTitle.CcStatus == ECCStatus.HumanTranslated)
            {
                SetValueHumanTranslated();
            }
            else if (p.TheTitle.CcStatus == ECCStatus.GoogleTranslated)
            {
                SetValuesGoogleTranslated();
            }
        }

        private void SetValuesGoogleTranslated()
        {
            btnEditTranslatedPluggeTitle.Style.Add("value", "Improve Google Translated Text");
        }

        private void SetValueHumanTranslated()
        {
            btnEditTranslatedPluggeTitle.Attributes.Add("value", "Improve Translation");
            btnGoogleTextOk.Style.Add("display", "none");
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

        protected void btnSaveTranslatedPluggeTitle_Click(object sender, EventArgs e)
        {
            PluggContainer p = GetPluggContainer((Page as DotNetNuke.Framework.PageBase).PageCulture.Name);
            p.TheTitle.Text = txtTranslatedPluggeTitle.Text;
            p.TheTitle.CcStatus = ECCStatus.HumanTranslated;
            new BaseHandler().SavePhText(p.TheTitle);
            SetValueHumanTranslated();
        }
    }
}
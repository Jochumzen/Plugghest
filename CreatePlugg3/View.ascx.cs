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
using Plugghest.Modules.CreatePlugg3.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base2;
using System.Collections.Generic;

namespace Plugghest.Modules.CreatePlugg3
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CreatePlugg3ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CreatePlugg3ModuleBase, IActionable
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SetLocalizationText();
                if (!IsPostBack)
                {
                    rdbtnWhoCanEdit.DataSource = Enum.GetNames(typeof(EWhoCanEdit));
                    rdbtnWhoCanEdit.DataBind();
                    rdbtnWhoCanEdit.Items[1].Selected = true;                 
                   
                }
                
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        private void SetLocalizationText()
        {
           string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
           lblTitle.Text = Localization.GetString("Title", this.LocalResourceFile + ".ascx." + curlan + ".resx");
           lblTitle.HelpKey = "lblTitle";
           lblTitle.HelpText = Localization.GetString("TitleDes", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            lblDescrip.Text = Localization.GetString("Description", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            lblDescrip.HelpKey = "lblDescrip";
            lblDescrip.HelpText = Localization.GetString("DescriptionDes", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            lblWhoCanEdit.Text = Localization.GetString("WhoCanEdit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            lblWhoCanEdit.HelpKey = "lblWhoCanEdit";
            lblWhoCanEdit.HelpText = Localization.GetString("WhoCanEditDes", this.LocalResourceFile + ".ascx." + curlan + ".resx");


            lblCreatePlugg.Text = Localization.GetString("CreatePlugg", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btnOk.Text = Localization.GetString("Ok", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            lblCreatePlugg1.Text = Localization.GetString("CreatePlugg", this.LocalResourceFile + ".ascx." + curlan + ".resx");

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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text.Trim() == "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Err", "alert('Please enter title !')", true);
                    return;
                }
                BaseHandler bh = new BaseHandler();
                List<object> cmpData = new List<object>();
                PluggContainer pc = new PluggContainer(new Localization().CurrentCulture);
                pc.ThePlugg.CreatedByUserId = this.UserId;
                pc.ThePlugg.ModifiedByUserId = this.UserId;
                pc.ThePlugg.PluggId = 0;
                pc.SetTitle(txtTitle.Text);
                string subjectStr = Page.Request.QueryString["s"];
                if (subjectStr != null)
                {
                    int subid = Convert.ToInt32(subjectStr);
                    pc.ThePlugg.SubjectId = subid;      
                }
                    else
                pc.ThePlugg.SubjectId = 0;

                pc.SetDescription(txtDescription.Text);
                pc.ThePlugg.WhoCanEdit = (EWhoCanEdit)Enum.Parse(typeof(EWhoCanEdit), rdbtnWhoCanEdit.SelectedValue);

                bh.CreateBasicPlugg(pc);
                txtTitle.Text = "";
                txtDescription.Text = "";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Success", "alert('New Plugg is created successfully')", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", "alert('Error : " + ex.Message + "')", true);
            }
        }       
     
    }
}
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

namespace Christoc.Modules.CreatePlugg3
{
    public partial class CreatePlugg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                //pc.ThePlugg.CreatedByUserId = this.UserId;
                //pc.ThePlugg.ModifiedByUserId = this.UserId;
                pc.ThePlugg.PluggId = 0;
                pc.SetTitle(txtTitle.Text);

                //pc.SetDescription(txtDescription.Text);

                PHText RichRichText = new PHText();
                RichRichText.ItemType = ETextItemType.PluggComponentRichRichText;
                cmpData.Add(RichRichText);

                Plugghest.Base2.YouTube objYouTube = new Plugghest.Base2.YouTube();
                cmpData.Add(objYouTube);

                bh.SavePlugg(pc, cmpData);

                txtTitle.Text = "";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Success", "alert('New Plugg is created successfully')", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", "alert('Error : " + ex.Message + "')", true);
            }
        }
    }
}
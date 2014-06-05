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
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base2;
using System.Collections.Generic;
using DotNetNuke.Entities.Tabs;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Plugghest.Modules.DisplayCourse
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DisplayCourseModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DisplayCourseModuleBase, IActionable
    {
        int Courseid;
        bool IsAuthorized = false, IsCase3, IsCase2, chkComTxt = false; string curlan, EditStr;
        string LabNoComtxt, LabSubjectTxt, btnEditCourseTxt, BtnEditTxt, BtncanceleditTet, BtncanceltransTxt, BtnlocalTxt, BtntransCourseTxt, BtnCancelTxt, BtnGoogleTransTxtOkTxt, BtnImpgoogleTransTxt, BtnImproveHumTransTxt, BtnLabelTxt, BtnLatexTxt, BtnRemoveTxt, BtnRichRichTxttxt, BtnRichTextTxt, BtnSaveTxt, BtnYouTubeTxt;

        CourseContainer cc;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string CourseTitle = ((DotNetNuke.Framework.CDefault)this.Page).Title;//get Course from page title
                CourseTitle = CourseTitle.Replace("C", "");

                if (!string.IsNullOrEmpty(CourseTitle))
                {
                    Courseid = Convert.ToInt32(CourseTitle);
                }
                curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;

                cc = new CourseContainer(curlan, Courseid);

                CallLocalization();
                EditStr = Page.Request.QueryString["edit"];
                IsAuthorized = (cc.TheCourse.WhoCanEdit == EWhoCanEdit.Anyone || cc.TheCourse.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));
                if (this.UserId == -1)
                {
                    IsAuthorized = false;
                }
                IsCase3 = (EditStr == "1" && IsAuthorized);
                IsCase2 = (EditStr == "2" && IsAuthorized);

                BaseHandler bh = new BaseHandler();

                if (cc.TheDescription == null)
                    cc.LoadDescription();

                if (cc.TheTitle == null)
                    cc.LoadTitle();

                LoadRichRichText();

                lblDescription.Text = Server.HtmlDecode(cc.TheDescription.Text);
                lblTitle.Text = Server.HtmlDecode(cc.TheTitle.Text);
                lblRichRichTxt.Text = Server.HtmlDecode(cc.TheRichRichText.Text);

                BindTree(cc.TheCourse.SubjectId);


                PageLoadFun();
            }

            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void LoadRichRichText()
        {
            if (cc.TheRichRichText == null)
            {
                cc.LoadRichRichText();

                if (cc.TheRichRichText != null)
                {
                    cc.TheRichRichText.Text = !string.IsNullOrEmpty(cc.TheRichRichText.Text) ? cc.TheRichRichText.Text : "(No Text)";
                }
                else
                {
                    PHText objPHtext = new PHText();
                    cc.TheRichRichText = objPHtext;
                    cc.TheRichRichText.Text = "(No Text)";
                }
            }
        }
        private void PageLoadFun()
        {

            if (cc.CultureCode == cc.TheCourse.CreatedInCultureCode)
            {
                if (IsCase2)
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=0", "language=" + cc.TheCourse.CreatedInCultureCode }));


                btnlocal.Visible = false;
                btntransCourse.Visible = false;
                btnEditCourse.Visible = true;
            }
            else
            {
                if (IsCase3)
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + cc.TheCourse.CreatedInCultureCode }));


                btnEditCourse.Visible = false;
                btncanceledit.Visible = false;
            }
            //CheckEditCase();


            if (!IsAuthorized)
            {
                btnEditCourse.Visible = false;
            }
            if (IsCase3)
            {
                string TreeHTMLstring = "<input type='button' id='btnTreeEdit' class='btnTreeEdit'  value=" + BtnEditTxt + " />";
                divTree.Controls.Add(new LiteralControl(TreeHTMLstring));

                btneditT.Visible = true;
                btneditD.Visible = true;
                btneditR.Visible = true;

                btnEditCourse.Visible = false;
                btncanceledit.Visible = true;
            }
            if (IsCase2)
            {
                cc.LoadTitle();
                cc.LoadDescription();
                LoadRichRichText();

                if (cc.TheTitle.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                {
                    btnGoogleTransTxtOkT.Visible = true;
                    btnImpGTransT.Visible = true;
                }
                if (cc.TheDescription.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                {
                    btnGoogleTransTxtOkD.Visible = true;
                    btnImpGTransD.Visible = true;
                }
                if (cc.TheRichRichText.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                {
                    btnGoogleTransTxtOkR.Visible = true;
                    btnImpGTransR.Visible = true;
                }

                if (cc.TheTitle.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                {
                    btnImpHumTnsTxtT.Visible = true;
                }
                if (cc.TheDescription.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                {
                    btnImpHumTnsTxtD.Visible = true;
                }
                if (cc.TheRichRichText.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                {
                    btnImpHumTnsTxtR.Visible = true;
                }
                btncanceltrans.Visible = true;
                btntransCourse.Visible = false;

            }

        }
        private void CallLocalization()
        {
            SetLocalizationText();

            SetStaticBtnText();
        }

        private void SetLocalizationText()
        {
            btnEditCourseTxt = Localization.GetString("btnEditCourse", this.LocalResourceFile + ".ascx." + curlan + ".resx");


            LabSubjectTxt = Localization.GetString("Subject", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            LabNoComtxt = Localization.GetString("lblNoComponent", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtncanceleditTet = Localization.GetString("btncanceledit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtncanceltransTxt = Localization.GetString("btncanceltrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnCancelTxt = Localization.GetString("Cancel", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnGoogleTransTxtOkTxt = Localization.GetString("GoogleTransTxtOk", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnImpgoogleTransTxt = Localization.GetString("ImpgoogleTrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnImproveHumTransTxt = Localization.GetString("ImproveHumTransTxt", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnRichRichTxttxt = Localization.GetString("RichRichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnSaveTxt = Localization.GetString("Save", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnlocalTxt = Localization.GetString("btnlocal", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnlocalTxt = BtnlocalTxt + " (" + cc.TheCourse.CreatedInCultureCode + ")";
            BtntransCourseTxt = Localization.GetString("btntransCourse", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnEditTxt = Localization.GetString("Edit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        }
        protected void btnSelSub_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(hdnNodeSubjectId.Value))
            {
                int id = Convert.ToInt32(hdnNodeSubjectId.Value);

                BaseHandler plugghandler = new BaseHandler();
                cc.TheCourse.SubjectId = id;
                cc.LoadTitle();
                List<object> blankList = new List<object>();
                BaseHandler bh = new BaseHandler();
                try
                {
                    bh.CreateCourse(cc);
                }
                catch (Exception)
                {
                }
            }

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }
        private void SetStaticBtnText()
        {
            btnlocal.Text = BtnlocalTxt;
            btnEditCourse.Text = btnEditCourseTxt;
            btncanceledit.Text = BtncanceleditTet;
            btncanceltrans.Text = BtncanceltransTxt;
            btntransCourse.Text = BtntransCourseTxt;

            btnSaveRRt.Text = BtnSaveTxt;
            btnTitleSave.Text = BtnSaveTxt;
            btnDescriptionSave.Text = BtnSaveTxt;
            btnSelSub.Text = BtnSaveTxt;

            btnCanRRt.Text = BtnCancelTxt;
            btnDescCancel.Text = BtnCancelTxt;
            btnTreecancel.Text = BtnCancelTxt;

            Cancel.Text = BtnCancelTxt;
            btneditT.Text = BtnEditTxt;
            btneditD.Text = BtnEditTxt;
            btneditR.Text = BtnEditTxt;
            btnGoogleTransTxtOkT.Text = BtnGoogleTransTxtOkTxt;
            btnGoogleTransTxtOkD.Text = BtnGoogleTransTxtOkTxt;
            btnGoogleTransTxtOkR.Text = BtnGoogleTransTxtOkTxt;
            btnImpGTransT.Text = BtnImpgoogleTransTxt;
            btnImpGTransD.Text = BtnImpgoogleTransTxt;
            btnImpGTransR.Text = BtnImpgoogleTransTxt;
            btnImpHumTnsTxtT.Text = BtnImproveHumTransTxt;
            btnImpHumTnsTxtD.Text = BtnImproveHumTransTxt;
            btnImpHumTnsTxtR.Text = BtnImproveHumTransTxt;
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

        protected void btnlocal_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "test=2", "language=" + cc.TheCourse.CreatedInCultureCode }));
        }

        protected void btncanceltrans_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "test=2", "language=" + curlan }));
        }



        protected void btncanceledit_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "test=2", "language=" + curlan }));
        }

        protected void btntransCourse_Click(object sender, EventArgs e)
        {
            btntransCourse.Visible = false;
            btncanceltrans.Visible = true;
            if (!chkComTxt)
                ShowNoComMsg();
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=2", "language=" + curlan }));

        }

        private void ShowNoComMsg()
        {
            lblnoCom.Visible = true;
            //lblnoCom.Text = LabNoComtxt;
        }

        protected void btnTitleSave_Click(object sender, EventArgs e)
        {
            ETextItemType ItemType = ETextItemType.CourseTitle;
            string txt = txttitle.Text;
            pnlCourseCom.Visible = true;
            if (EditStr == "2")
            {
                UpdatePHtext(ItemType, txt);
            }
            else
            {
                BaseHandler bh = new BaseHandler();
                cc.LoadTitle();
                cc.TheTitle.Text = txt;
                bh.CreateCourse(cc);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            pnlCourseCom.Visible = true;
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));
        }

        protected void btnDescriptionSave_Click(object sender, EventArgs e)
        {
            ETextItemType ItemType = ETextItemType.CourseDescription;
            string txt = txtDescription.Text;
            pnlCourseCom.Visible = true;
            cc.LoadTitle();
            if (EditStr == "2")
            {
                UpdatePHtext(ItemType, txt);
            }
            else
            {
                BaseHandler bh = new BaseHandler();
                cc.LoadDescription();
                cc.TheDescription.Text = txt;
                bh.CreateCourse(cc);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));


        }

        private void UpdatePHtext(ETextItemType ItemType, string txt)
        {
            BaseHandler bh = new BaseHandler();

            PHText phText = bh.GetCurrentVersionText(curlan, cc.TheCourse.CourseId, ItemType);

            phText.Text = txt;
            phText.CreatedByUserId = this.UserId;
            phText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(phText);
        }

        protected void btnSaveRRt_Click(object sender, EventArgs e)
        {
            ETextItemType ItemType = ETextItemType.CourseRichRichText;
            string txt = richrichtext.Text;
            pnlCourseCom.Visible = true;
            if (EditStr == "2")
            {
                UpdatePHtext(ItemType, txt);
            }
            else
            {
                BaseHandler bh = new BaseHandler();
                //cc.LoadRichRichText();
                cc.LoadTitle();
                LoadRichRichText();
                cc.TheRichRichText.Text = txt;
                bh.CreateCourse(cc);
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }

        protected void btneditT_Click(object sender, EventArgs e)
        {
            pnlRRT.Visible = false;
            pnlTitle.Visible = true;
            pnlDescription.Visible = false;

            txttitle.Text = lblTitle.Text;
            pnlCourseCom.Visible = false;
        }

        protected void btneditD_Click(object sender, EventArgs e)
        {
            pnlRRT.Visible = false;
            pnlTitle.Visible = false;
            pnlDescription.Visible = true;
            txtDescription.Text = lblDescription.Text;
            pnlCourseCom.Visible = false;
        }
        public void BindTree(int subid)
        {
            BaseHandler objBaseHandler = new BaseHandler();
            List<Subject> SubList = (List<Subject>)objBaseHandler.GetSubjectsAsFlatList(curlan);
            string childName = SubList.Find(x => x.SubjectId == subid).label;
            int id = Convert.ToInt32(SubList.Find(x => x.SubjectId == subid).MotherId);
            while (id != 0)
            {
                Subject newSub = SubList.Find(x => x.SubjectId == id);
                childName = newSub.label + "->" + childName;
                id = Convert.ToInt32(newSub.MotherId);
            }
            lbltree.Text = LabSubjectTxt + ": " + childName;

            var tree = objBaseHandler.GetSubjectsAsTree(curlan);
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);

        }
        protected void btneditR_Click(object sender, EventArgs e)
        {
            pnlRRT.Visible = true;
            pnlTitle.Visible = false;
            pnlDescription.Visible = false;
            richrichtext.Text = lblRichRichTxt.Text;
            pnlCourseCom.Visible = false;
        }

        protected void btnEditcourse_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=1", "language=" + curlan }));
        }

        protected void btnImpGTransT_Click(object sender, EventArgs e)
        {
            BaseHandler bh = new BaseHandler();
            ETextItemType ItemType = ETextItemType.CourseTitle;
            PHText phText = bh.GetCurrentVersionText(curlan, cc.TheCourse.CourseId, ItemType);
            phText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(phText);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }

        protected void btnImpGTransD_Click(object sender, EventArgs e)
        {
            BaseHandler bh = new BaseHandler();
            ETextItemType ItemType = ETextItemType.CourseDescription;
            PHText phText = bh.GetCurrentVersionText(curlan, cc.TheCourse.CourseId, ItemType);
            phText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(phText);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }

        protected void btnImpGTransR_Click(object sender, EventArgs e)
        {
            BaseHandler bh = new BaseHandler();
            ETextItemType ItemType = ETextItemType.CourseRichRichText;
            PHText phText = bh.GetCurrentVersionText(curlan, cc.TheCourse.CourseId, ItemType);
            phText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(phText);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }
    }
}
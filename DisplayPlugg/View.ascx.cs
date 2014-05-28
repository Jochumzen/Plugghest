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
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Helpers;
using Plugghest.Base2;
using System.Collections.Generic;
using Plugghest.DNN;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;

namespace Plugghest.Modules.DisplayPlugg
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DisplayPluggModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DisplayPluggModuleBase, IActionable
    {
        string EditStr = ""; string curlan = ""; int pluggid;
        bool IsAuthorized = false, IsCase3, IsCase2;
        string BtnAddtxt, LabAddNewcomTxt, btnEditPlugTxt, BtnYoutubeTxt, BtnEditTxt, BtncanceleditTet, BtncanceltransTxt, BtnlocalTxt, BtntransplugTxt, BtnCancelTxt, BtnGoogleTransTxtOkTxt, BtnImpgoogleTransTxt, BtnImproveHumTransTxt, BtnLabelTxt, BtnLatexTxt, BtnRemoveTxt, BtnRichRichTxttxt, BtnRichTextTxt, BtnSaveTxt, BtnYouTubeTxt;
        PluggContainer p;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;

                pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
                p = new PluggContainer(curlan, pluggid);
                CallLocalization();
                EditStr = Page.Request.QueryString["edit"];
                IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));
                IsCase3 = (EditStr == "1" && IsAuthorized);
                IsCase2 = (EditStr == "2" && IsAuthorized);
               
                    PageLoadFun();
             

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void CallLocalization()
        {
            SetLocalizationText();

            SetStaticBtnText();
        }

        private void SetStaticBtnText()
        {
            btnlocal.Text = BtnlocalTxt;
            btnEditPlug.Text = btnEditPlugTxt;
            btncanceledit.Text = BtncanceleditTet;
            btncanceltrans.Text = BtncanceltransTxt;
            btntransplug.Text = BtntransplugTxt;


            btnSaveRRt.Text = BtnSaveTxt;
            btnSaveRt.Text = BtnSaveTxt;
            btnLabelSave.Text = BtnSaveTxt;
            btnYtSave.Text = BtnSaveTxt;
            btnLatexSave.Text = BtnSaveTxt;
            btnSelSub.Text = BtnSaveTxt;

            btnCanRRt.Text = BtnCancelTxt;
            btnCanRt.Text = BtnCancelTxt;
            btnYtCaNCEL.Text = BtnCancelTxt;
            btnLatexCancel.Text = BtnCancelTxt;
            btnTreecancel.Text = BtnCancelTxt;
            Cancel.Text = BtnCancelTxt;
        }

        private void SetLocalizationText()
        {
            btnEditPlugTxt = Localization.GetString("btnEditPlug", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            BtncanceleditTet = Localization.GetString("btncanceledit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnAddtxt = Localization.GetString("Add", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtncanceltransTxt = Localization.GetString("btncanceltrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnCancelTxt = Localization.GetString("Cancel", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnGoogleTransTxtOkTxt = Localization.GetString("GoogleTransTxtOk", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnImpgoogleTransTxt = Localization.GetString("ImpgoogleTrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnImproveHumTransTxt = Localization.GetString("ImproveHumTransTxt", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnLabelTxt = Localization.GetString("Label", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnLatexTxt = Localization.GetString("Latex", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnYoutubeTxt = Localization.GetString("YouTube", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnRemoveTxt = Localization.GetString("Remove", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnRichRichTxttxt = Localization.GetString("RichRichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnRichTextTxt = Localization.GetString("RichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnSaveTxt = Localization.GetString("Save", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnYouTubeTxt = Localization.GetString("YouTube", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnlocalTxt = Localization.GetString("btnlocal", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnlocalTxt = BtnlocalTxt + "( " + p.ThePlugg.CreatedInCultureCode + " )";
            BtntransplugTxt = Localization.GetString("btntransplug", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            LabAddNewcomTxt = Localization.GetString("AddNewCom", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            BtnEditTxt = Localization.GetString("Edit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        }

        private void PageLoadFun()
        {

            if (p.CultureCode == p.ThePlugg.CreatedInCultureCode)
            {
                if (IsCase2)
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=0", "language=" + p.ThePlugg.CreatedInCultureCode }));


                btnlocal.Visible = false;
                btntransplug.Visible = false;
                btnEditPlug.Visible = true;
            }
            else
            {
                if (IsCase3)
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + p.ThePlugg.CreatedInCultureCode }));


                btnEditPlug.Visible = false;
                btncanceledit.Visible = false;
            }
            //CheckEditCase();


            if (!IsAuthorized)
            {
                btnEditPlug.Visible = false;
            }
            if (IsCase3)
            {

                btnEditPlug.Visible = false;
                btncanceledit.Visible = true;
            }
            if (IsCase2)
            {
                btncanceltrans.Visible = true;
                btntransplug.Visible = false;
            }     
            DisPlayPluggComp();
        }



        private void DisPlayPluggComp()
        {
            List<PluggComponent> comps = p.GetComponentList();
            BaseHandler bh = new BaseHandler();
            string ddl = ""; string str = "</select></div>"; int i = 0;


            ddl = CreateDropDown(ddl);

            Label dynamicLabel = new Label();


            int? subid = p.ThePlugg.SubjectId;
            CreateSubject(i, subid);

            foreach (PluggComponent comp in comps)
            {
                switch (comp.ComponentType)
                {
                    case EComponentType.Label:

                        PHText lbl = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentLabel);
                        string LabHTMLstring = CreateDiv(lbl, "Label" + i, BtnLabelTxt);
                        //This condition is used for editing plugg
                        if (IsCase3)
                        {
                            int orderid = comp.ComponentOrder;
                            CreateBtnDel(orderid, "btncsdel", "btnlbDel" + i + "");
                            CreateBtnEdit(comp, lbl, "btncsdel", "btnlbEdit" + i + "");

                            LabHTMLstring = "</div>" + LabAddNewcomTxt + "<select class='ddlclass' id='ddl" + i + "'>";
                            LabHTMLstring = LabHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));
                            CreateBtnAdd(orderid, "btncs", "btnlbAdd" + i + "");
                            divTitle.Controls.Add(new LiteralControl(str));
                        }
                        //This condition is used for Translation The Plugg Text(same for all cases)
                        else if (IsCase2)
                        {
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                CreateBtnImproveHumGoogleTrans(comp, lbl, "googletrans", "btnrtIGT" + i + "");
                                CreateBtnGoogleT(lbl, "googleTrasok", "btnGTText" + i + "");
                            }
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                CreateBtnImproveHumGoogleTrans(comp, lbl, "btnhumantrans", "btnlbl" + i + "");
                            }
                            divTitle.Controls.Add(new LiteralControl(str));
                        }
                        break;

                    case EComponentType.RichText:
                        PHText rt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichText);
                        string RtHTMLstring = CreateDiv(rt, "RichText" + i, BtnRichTextTxt);
                        if (IsCase3)
                        {
                            int RTorderid = comp.ComponentOrder;

                            CreateBtnDel(RTorderid, "btncsdel", "btnrtDel" + i + "");
                            CreateBtnEdit(comp, rt, "btncsdel", "btnrtEdit" + i + "");
                            RtHTMLstring = "</div>" + LabAddNewcomTxt + "<select class='ddlclass' id='Rtddl" + i + "'>";
                            RtHTMLstring = RtHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));
                            CreateBtnAdd(RTorderid, "btncs", "btnrtAdd" + i + "");
                            divTitle.Controls.Add(new LiteralControl(str));
                        }
                        else if (IsCase2)
                        {
                            if (rt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                CreateBtnImproveHumGoogleTrans(comp, rt, "googletrans", "btnrtIGT" + i + "");
                                CreateBtnGoogleT(rt, "googleTrasok", "btnrtGTText" + i + "");

                            }
                            if (rt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                CreateBtnImproveHumGoogleTrans(comp, rt, "btnhumantrans", "btnrtIHT" + i + "");
                            }
                            divTitle.Controls.Add(new LiteralControl(str));
                        }
                        break;

                    case EComponentType.RichRichText:
                        PHText rrt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichRichText);
                        string RRTHTMLstring = CreateDiv(rrt, "RichRichText" + i, BtnRichRichTxttxt);
                        if (IsCase3)
                        {
                            int RRTorderid = comp.ComponentOrder;

                            CreateBtnDel(RRTorderid, "btncsdel", "btnrrtDel" + i + "");
                            CreateBtnEdit(comp, rrt, "btncsdel", "btnrrtEdit" + i + "");
                            RRTHTMLstring = "</div>" + LabAddNewcomTxt + "<select class='ddlclass' id='Rtddl" + i + "'>";
                            RRTHTMLstring = RRTHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            CreateBtnAdd(RRTorderid, "btncs", "btnrrtAdd" + i + "");


                            divTitle.Controls.Add(new LiteralControl(str));
                        }
                        else if (IsCase2)
                        {
                            if (rrt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                CreateBtnImproveHumGoogleTrans(comp, rrt, "googletrans", "btnrrtIGT" + i + "");
                                CreateBtnGoogleT(rrt, "googleTrasok", "btnrrtGTText" + i + "");
                            }
                            if (rrt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                CreateBtnImproveHumGoogleTrans(comp, rrt, "btnhumantrans", "btnrrtIHT" + i + "");

                            }

                            divTitle.Controls.Add(new LiteralControl(str));
                        }

                        break;

                    case EComponentType.Latex:
                        PHLatex lat = bh.GetCurrentVersionLatexText(curlan, comp.PluggComponentId, ELatexItemType.PluggComponentLatex);
                        string LatHTMLstring = CreateDivLat(lat, "Latex" + i);
                        if (IsCase3)
                        {
                            int ltorderid = comp.ComponentOrder;



                            CreateBtnDel(ltorderid, "btncsdel", "btnltDel" + i + "");

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnltEdit" + i;
                            editbtn.Text = BtnEditTxt;
                            editbtn.Click += (s, e) => { CallLatFun(ltorderid, comp, lat, "1"); };
                            divTitle.Controls.Add(editbtn);

                            LatHTMLstring = "</div>" + LabAddNewcomTxt + "<select class='ddlclass' id='ltddl" + i + "'>";
                            LatHTMLstring = LatHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));


                            CreateBtnAdd(ltorderid, "btncs", "btnlatexAdd" + i + "");
                            divTitle.Controls.Add(new LiteralControl(str));
                        }

                        break;


                    case EComponentType.YouTube:
                        YouTube yt = bh.GetYouTubeByComponentId(comp.PluggComponentId);
                        string strYoutubeIframe = "";
                        string ytYouTubecode = "";
                        try
                        {
                            strYoutubeIframe = yt.GetIframeString(p.CultureCode);
                        }
                        catch
                        {
                            strYoutubeIframe = "";
                        }
                        if (yt == null)
                        {
                            ytYouTubecode = "(No text)";
                        }
                        else
                        {
                            ytYouTubecode = yt.YouTubeCode;
                        }
                        var ytdivid = "Youtube" + i;
                        var ytddlid = "ytddl" + i;
                        var ytorderid = comp.ComponentOrder;
                        string ytHTMLstring = "";
                        if (IsCase3)
                        {
                            ytHTMLstring = "<div><div id=" + ytdivid + " class='Main'>" + BtnYoutubeTxt + ":" + ytYouTubecode + "";

                            divTitle.Controls.Add(new LiteralControl(ytHTMLstring));

                            CreateBtnDel(ytorderid, "btncsdel", "btnytDel" + i + "");

                            string IdYt = "btnrrtEdit" + i;

                            CreateBtnYTEdit(comp, yt, ytorderid, "btncsdel", "IdYt" + i + "");

                            ytHTMLstring = "</div>" + strYoutubeIframe + "</br>" + LabAddNewcomTxt + "<select class='ddlclass' id=" + ytddlid + ">";
                            ytHTMLstring = ytHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(ytHTMLstring));

                            CreateBtnAdd(ytorderid, "btncs", "btnytAdd" + i + "");

                            divTitle.Controls.Add(new LiteralControl(str));
                        }
                        else
                        {
                            ytHTMLstring = "<div><div id=" + ytdivid + " class='Main'>  " + BtnYoutubeTxt + ":" + ytYouTubecode + "</div>" + strYoutubeIframe + "</div>";
                            divTitle.Controls.Add(new LiteralControl(ytHTMLstring));
                        }
                        break;
                }
                i++;
            }
        }

        private void CreateBtnImproveHumGoogleTrans(PluggComponent comp, PHText lbl, string css, string id)
        {
            Button btnImpHumTras = new Button();
            btnImpHumTras.CssClass = css;
            btnImpHumTras.ID = id;
            btnImpHumTras.Text = BtnImproveHumTransTxt;
            btnImpHumTras.Click += (s, e) => { ImpGoogleTrans(comp, lbl); };
            divTitle.Controls.Add(btnImpHumTras);
        }

        private void CreateBtnAdd(int orderid, string CssClass, string ID)
        {
            Button Addbutton = new Button();
            Addbutton.CssClass = CssClass;
            Addbutton.ID = ID;
            Addbutton.Text = BtnAddtxt;
            Addbutton.Click += (s, e) => { callingAddPlugg(orderid); };
            divTitle.Controls.Add(Addbutton);
        }
        private void CreateBtnYTEdit(PluggComponent comp, YouTube yt, int ytorderid, string CssClass, string ID)
        {
            Button editbtn = new Button();
            editbtn.CssClass = CssClass;
            editbtn.ID = ID;
            editbtn.Text = BtnEditTxt;
            editbtn.Click += (s, e) => { YouTubeEdit(ytorderid, comp, yt); };
            divTitle.Controls.Add(editbtn);
        }

        private string CreateDivLat(PHLatex lat, string ltdivid)
        {
            string LatHTMLstring = "";
            if (lat == null)
                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'>" + BtnLatexTxt + ":";
            else
                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'> " + BtnLatexTxt + ":" + lat.Text + "";
            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));
            return LatHTMLstring;
        }

        private void CreateSubject(int i, int? subid)
        {
            if (subid != null)
            {
                BindTree(Convert.ToInt32(subid));             

                if (IsCase3)
                {
                    string TreeHTMLstring = "<input type='button' id='btnTreeEdit" + i + "' class='btnTreeEdit'  value=" + BtnEditTxt + " />";
                    divTree.Controls.Add(new LiteralControl(TreeHTMLstring));
                }
            }

        }

        private string CreateDiv(PHText Phtxt, string divid, string obj)
        {
            string HTMLstring = "";
            if (Phtxt == null)
                HTMLstring = "<div><div id=" + divid + " class='Main'" + obj + ": ";
            else
                HTMLstring = "<div><div id=" + divid + " class='Main'>" + obj + ":" + Phtxt.Text + "";
            divTitle.Controls.Add(new LiteralControl(HTMLstring));
            return HTMLstring;
        }

        private void CreateBtnGoogleT(PHText rrt, string CssClassGT, string IDGT)
        {
            Button btnGT = new Button();
            btnGT.CssClass = CssClassGT;
            btnGT.ID = IDGT;
            btnGT.Text = BtnGoogleTransTxtOkTxt;
            btnGT.Click += (s, e) => { GoogleTranText(rrt); };
            divTitle.Controls.Add(btnGT);
        }

        private void CreateBtnEdit(PluggComponent comp, PHText lbl, string CssClass, string ID)
        {
            Button editbtn = new Button();
            editbtn.CssClass = CssClass;
            editbtn.ID = ID;
            editbtn.Text = BtnEditTxt;
            editbtn.Click += (s, e) => { ImpGoogleTrans(comp, lbl); };
            divTitle.Controls.Add(editbtn);
        }

        private void CreateBtnDel(int orderid, string CssClass, string ID)
        {
            Button delbtn = new Button();
            delbtn.CssClass = CssClass;
            delbtn.ID = ID;
            delbtn.Text = BtnRemoveTxt;
            delbtn.Click += (s, e) => { callingDelPlugg(orderid); };
            divTitle.Controls.Add(delbtn);
        }

        private static string CreateDropDown(string ddl)
        {
            foreach (string name in Enum.GetNames(typeof(EComponentType)))
            {
                if (name != "NotSet")
                {
                    string dl = "<option  value=" + name + " >" + name + "</option>";
                    ddl = ddl + dl;
                }
            }
            return ddl;
        }

        private void CallLatFun(int ltorderid, PluggComponent comp, PHLatex lat, string p)
        {
            hdnlabel.Value = Convert.ToString(comp.PluggComponentId);

            if (comp.ComponentType == EComponentType.Latex)
            {
                pnlRRT.Visible = true;
                pnllabel.Visible = false;
                pnlletex.Visible = false;
                richtextbox.Visible = false;
                pnlLatex.Visible = false;
                pnlYoutube.Visible = false;
                richrichtext.Text = lat.Text;
            }
        }

        private void YouTubeEdit(int ytorderid, PluggComponent comp, YouTube yt)
        {
            string ytcode = "";
            if (yt == null)
                ytcode = "";
            else
                ytcode = yt.YouTubeCode;
            hdnlabel.Value = Convert.ToString(comp.PluggComponentId);

            if (comp.ComponentType == EComponentType.YouTube)
            {
                pnlLatex.Visible = false;
                pnlYoutube.Visible = true;
                pnlRRT.Visible = false;
                pnllabel.Visible = false;
                pnlletex.Visible = false;
                richtextbox.Visible = false;
                txtYouTube.Text = ytcode;
            }
        }

        private void GoogleTranText(PHText txt)
        {
            BaseHandler bh = new BaseHandler();
            txt.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(txt);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));
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
                childName = newSub.label + ">" + childName;
                id = Convert.ToInt32(newSub.MotherId);
            }
            lbltree.Text = "Subject:" + childName;

            var tree = objBaseHandler.GetSubjectsAsTree(curlan);
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);

        }

        private void ImpGoogleTrans(PluggComponent comp, PHText CulTxt)
        {
            hdnlabel.Value = Convert.ToString(comp.PluggComponentId);

            string text = CulTxt.Text;
            switch (comp.ComponentType)
            {
                case EComponentType.Label:
                    CompHide();
                    txtlabel.Text = text;
                    pnllabel.Visible = true;
                    break;

                case EComponentType.RichText:
                    CompHide();
                    richtextbox.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", " $(document).ready(function () {$('#editor').html('" + text.Replace("\r\n", "<br />") + "')});", true);
                    break;

                case EComponentType.RichRichText:
                    CompHide();
                    pnlRRT.Visible = true;
                    richrichtext.Text = text;
                    break;
            }
        }

        private void CompHide()
        {
            pnlRRT.Visible = false;
            pnllabel.Visible = false;
            pnlletex.Visible = false;
            richtextbox.Visible = false;
            pnlLatex.Visible = false;
            pnlYoutube.Visible = false;
        }


        private void callingDelPlugg(int orderid)
        {
            BaseHandler plugghandler = new BaseHandler();
            plugghandler.DeleteComponent(p, orderid);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=1", "language=" + curlan }));

        }

        private void callingAddPlugg(int orderid)
        {
            var id = hdn.Value;

            BaseHandler plugghandler = new BaseHandler();
            PluggComponent pc = new PluggComponent();
            pc.ComponentOrder = orderid + 1;
            pc.ComponentType = (EComponentType)Enum.Parse(typeof(EComponentType), id);
            plugghandler.AddComponent(p, pc);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=1", "language=" + curlan }));


        }

        protected void btnEditPlugg_Click(object sender, EventArgs e)
        {
            btncanceledit.Visible = true;
            btnEditPlug.Visible = false;

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=1", "language=" + curlan }));
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

        protected void btncanceledit_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "test=2", "language=" + curlan }));
        }

        protected void btnLabelSave_Click(object sender, EventArgs e)
        {
            ETextItemType ItemType = ETextItemType.PluggComponentLabel;
            string txt = txtlabel.Text;
            UpdatePHtext(ItemType, txt);

        }

        protected void btnSaveRt_Click(object sender, EventArgs e)
        {
            ETextItemType ItemType = ETextItemType.PluggComponentRichText;
            string txt = hdnrichtext.Value;
            UpdatePHtext(ItemType, txt);
        }


        private void UpdatePHtext(ETextItemType ItemType, string txt)
        {
            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            List<PluggComponent> comps = p.GetComponentList();
            PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
            BaseHandler bh = new BaseHandler();

            var comtype = cToAdd.ComponentType;

            PHText phText = bh.GetCurrentVersionText(curlan, itemid, ItemType);

            phText.Text = txt;

            if (EditStr == "2")
                phText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;

            bh.SavePhText(phText);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));
        }

        protected void btntransplug_Click(object sender, EventArgs e)
        {
            btntransplug.Visible = false;
            btncanceltrans.Visible = true;
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=2", "language=" + curlan }));

        }



        protected void btnlocal_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "test=2", "language=" + p.ThePlugg.CreatedInCultureCode }));
        }

        protected void btncanceltrans_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "test=2", "language=" + curlan }));
        }

        protected void btnSaveRRt_Click(object sender, EventArgs e)
        {
            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            List<PluggComponent> comps = p.GetComponentList();
            PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
            BaseHandler bh = new BaseHandler();

            var comtype = cToAdd.ComponentType;

            switch (cToAdd.ComponentType)
            {
                case EComponentType.RichRichText:
                    PHText RichRichText = bh.GetCurrentVersionText(curlan, itemid, ETextItemType.PluggComponentRichRichText);
                    RichRichText.Text = richrichtext.Text;
                    if (EditStr == "2")
                        RichRichText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;

                    bh.SavePhText(RichRichText);
                    break;

                case EComponentType.Latex:

                    PHLatex latex = bh.GetCurrentVersionLatexText(curlan, Convert.ToInt32(id), ELatexItemType.PluggComponentLatex);

                    latex.Text = richrichtext.Text;
                    bh.SaveLatexText(latex);
                    break;
            }

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));


        }

        protected void btnYtSave_Click(object sender, EventArgs e)
        {
            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            List<PluggComponent> comps = p.GetComponentList();

            BaseHandler bh = new BaseHandler();


            List<object> objToadd = new List<object>();

            YouTube yt = bh.GetYouTubeByComponentId(Convert.ToInt32(id));
            if (yt == null)
                yt = new YouTube();
            try
            {
                yt.YouTubeTitle = yttitle.Value;
                yt.YouTubeDuration = Convert.ToInt32(ytduration.Value);
                yt.YouTubeCode = ytYouTubeCode.Value;
                yt.YouTubeAuthor = ytAuthor.Value;
                yt.YouTubeCreatedOn = Convert.ToDateTime(ytYouTubeCreatedOn.Value);
                yt.YouTubeComment = ytYouTubeComment.Value;
                yt.PluggComponentId = itemid;
            }
            catch
            {

            }

            bh.SaveYouTube(yt);

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=1", "language=" + curlan }));
        }

        protected void btnLatexSave_Click(object sender, EventArgs e)
        {

            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            List<PluggComponent> comps = p.GetComponentList();

            BaseHandler bh = new BaseHandler();


            List<object> objToadd = new List<object>();

            PHLatex lt = bh.GetCurrentVersionLatexText(curlan, Convert.ToInt32(id), ELatexItemType.PluggComponentLatex);
            lt.HtmlText = richrichtext.Text;
            objToadd.Add(lt);

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=1", "language=" + curlan }));

        }

        protected void btnSelSub_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(hdnNodeSubjectId.Value))
            {
                int id = Convert.ToInt32(hdnNodeSubjectId.Value);

                BaseHandler plugghandler = new BaseHandler();
                p.ThePlugg.SubjectId = id;
                p.LoadTitle();
                List<object> blankList = new List<object>();
                BaseHandler bh = new BaseHandler();
                try
                {
                    bh.SavePlugg(p, blankList);
                }
                catch (Exception)
                {
                }
            }

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", new string[] { "edit=" + EditStr, "language=" + curlan }));

        }
    }
}
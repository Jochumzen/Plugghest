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
        string EditStr = ""; string curlan = "";
        bool IsAuthorized = false;
        private System.Resources.ResourceManager rm;
        //  string curlan, Add1,btnSaveSubjects1, btncanceledit1, btncanceltrans1, btnlocal1, btntransplug1, Cancel1, GoogleTransTxtOk1, ImpgoogleTrans1, ImproveHumTransTxt1, Label1, Latex1, Remove1, RichRichText1, RichText1, Save1, YouTube1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //CallLocalization();
                curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
                                
                var btncanceledit1 = Localization.GetString("btncanceledit", this.LocalResourceFile + ".ascx." + curlan + ".resx");


                EditStr = Page.Request.QueryString["edit"];

                if (!IsPostBack)
                {
                    PageLoadFun();

                    pnlRRT.Visible = false;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;

                }
                else
                {
                    if (ViewState["falg"] != null)
                    {
                        PageLoadFun();

                    }
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        //private void CallLocalization()
        //{       
        //    string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name; 

        //    btncanceledit1 = Localization.GetString("btntransplug", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //   Add1 = Localization.GetString("Add", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  btncanceltrans1 = Localization.GetString("btncanceltrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  Cancel1 = Localization.GetString("Cancel", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  GoogleTransTxtOk1 = Localization.GetString("GoogleTransTxtOk", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  ImpgoogleTrans1 = Localization.GetString("ImpgoogleTrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  ImproveHumTransTxt1 = Localization.GetString("ImproveHumTransTxt", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  Label1 = Localization.GetString("Label", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  Latex1 = Localization.GetString("Latex", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  Remove1 = Localization.GetString("Remove", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  RichRichText1 = Localization.GetString("RichRichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  RichText1 = Localization.GetString("RichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  Save1 = Localization.GetString("Save", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  YouTube1 = Localization.GetString("YouTube", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //     btnlocal1 = Localization.GetString("btnlocal", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  btntransplug1 = Localization.GetString("btntransplug", this.LocalResourceFile + ".ascx." + curlan + ".resx");
        //  btnlocal.Text = btnlocal1;
        //  btncanceltrans.Text = btncanceltrans1;
        //  btntransplug.Text = btntransplug1;
        //  btncanceledit.Text = btncanceledit1;
        //  btnSaveSubjects.Text = btnSaveSubjects1;
        //  Button5.Text = Save1;
        //  Button6.Text = Cancel1;
        //  Save.Text = Save1;
        //  Button1.Text = Save1;
        //  Cancel.Text = Cancel1;
        //  Button2.Text = Cancel1;
        //  btnSaveSubjects.Text = btnSaveSubjects1;
        //}



        private void PageLoadFun()
        {
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);
            IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));

            // if (p.CultureCode == p.ThePlugg.CreatedInCultureCode )
            SetPageText(curlan, p);
            ViewState.Add("falg", true);
        }
        //private void AddPlug_Click(object sender, EventArgs e)
        //{ 

        //}
        void myFunction(object sender, CommandEventArgs e)
        {
            int RecordId = Int32.Parse(e.CommandArgument.ToString());
            Response.Write(RecordId.ToString());
        }
        private void SetPageText(string curlan, PluggContainer p)
        {

            if (IsAuthorized == true)
            {
                btnSaveSubjects.Visible = true;
                btncanceledit.Visible = false;
            }
            else
            {
                btnSaveSubjects.Visible = false;
                btncanceledit.Visible = false;
            }
            if (EditStr == "1" && IsAuthorized == true)
            {
                btncanceledit.Visible = true;
                btnSaveSubjects.Visible = false;
            }
            else
            {

            }

            //if (p.ThePlugg.YouTubeCode == null)
            //{
            //    lblYoutube.Text = "[No Video]";
            //}
            //else
            //{
            //    p.TheVideo = new Youtube(p.ThePlugg.YouTubeCode);
            //    if (p.TheVideo.IsValid)
            //        lblYoutube.Text = p.TheVideo.GetIframeString(curlan.Substring(3, 2));
            //}
            //p.LoadAllText();
            //if (p.TheLatex != null)
            //{
            //    hdLatextText.Value = p.TheLatex.Text;
            //    lblLatexTextInHtml.Text = Server.HtmlDecode(p.TheLatex.HtmlText);
            //}
            //if (p.TheHtmlText != null)
            //    lblHtmlText.Text = Server.HtmlDecode(p.TheLatex.HtmlText); ;


            List<PluggComponent> comps = p.GetComponentList();
            BaseHandler bh = new BaseHandler();

            string ddl = "";
            foreach (string name in Enum.GetNames(typeof(EComponentType)))
            {
                if (name != "NotSet")
                {
                    string dl = "<option onSelect='disable();' value=" + name + " >" + name + "</option>";
                    ddl = ddl + dl;

                }
            }

            Label dynamicLabel = new Label();
            int i = 0;
            foreach (PluggComponent comp in comps)
            {




                switch (comp.ComponentType)
                {




                    case EComponentType.Label:

                        PHText lbl = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentLabel);


                        var divid = "Label" + i;
                        var ddlid = "ddl" + i;
                        var orderid = comp.ComponentOrder;
                        string yourHTMLstring0 = "";


                        if (EditStr == "1" && IsAuthorized == true)
                        {

                            if (lbl == null)
                            {
                                yourHTMLstring0 = "<div><div id=" + divid + " class='Main'> <label  id='lbllabel" + i + "' runat='server' >Label:</label>";
                                lbl.Text = "";
                            }
                            else
                                yourHTMLstring0 = "<div><div id=" + divid + " class='Main'><label  id='lbllabel" + i + "' runat='server'  >Label:" + lbl.Text + "</label>";

                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring0));
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnIGT" + i;
                                btn.Text = "Improve google Translation";
                                // btn.Text = ImpgoogleTrans1;
                                //btn.Text = "<%$ Resources:WebResources, btnIGT" + i+" %>";
                                //btn.meta:resourcekey="fd";
                                btn.Click += (s, e) => { ImpGoogleTrans(orderid, comp, lbl, "1"); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnGTText" + i;
                                btn1.Text = "Google Translation Text Ok ";
                                // btn1.Text = GoogleTransTxtOk1;
                                btn1.Click += (s, e) => { GoogleTranText(orderid, lbl); };
                                divTitle.Controls.Add(btn1);



                                // lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                            }
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnIHT" + i;
                                btn.Text = "Improve Human Translation Text";
                                // btn.Text = ImproveHumTransTxt1;
                                btn.Click += (s, e) => { ImpGoogleTrans(orderid, comp, lbl, "2"); };
                                divTitle.Controls.Add(btn);

                            }

                            Button delbtn = new Button();
                            delbtn.CssClass = "btncsdel";
                            delbtn.ID = "btnlbDel" + i;
                            delbtn.Text = "Remove";
                            // delbtn.Text = Remove1;

                            delbtn.Click += (s, e) => { callingDel(orderid); };
                            divTitle.Controls.Add(delbtn);

                            yourHTMLstring0 = "</div><select class='ddlclass' id=" + ddlid + ">";
                            yourHTMLstring0 = yourHTMLstring0 + ddl;
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring0));

                            Button Addbutton = new Button();
                            Addbutton.CssClass = "btncs";
                            Addbutton.ID = "btnlbAdd" + i;
                            Addbutton.Text = "Add";
                            // Addbutton.Text = Add1;
                            Addbutton.Click += (s, e) => { calling(orderid); };
                            divTitle.Controls.Add(Addbutton);

                            string n1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(n1));
                        }
                        else
                        {
                            if (lbl == null)
                                yourHTMLstring0 = "<div><div id=" + divid + " class='Main'>Label: </div></div>";
                            else
                                yourHTMLstring0 = "<div><div id=" + divid + " class='Main'> Label:" + lbl.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring0));

                        }
                        break;

                    case EComponentType.RichText:
                        PHText rt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichText);
                        //Handle rich text

                        string yourHTMLstring = "";

                        divTitle.Controls.Add(new LiteralControl(yourHTMLstring));
                        var RTdivid = "RichText" + i;
                        var RTddlid = "Rtddl" + i;
                        var RTorderid = comp.ComponentOrder;

                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            if (rt == null)
                            {
                                yourHTMLstring = "<div><div id=" + RTdivid + " class='Main'>RichText: ";
                                rt.Text = "";
                            }
                            else
                                yourHTMLstring = "<div><div id=" + RTdivid + " class='Main'> RichText:" + rt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring));

                            if (rt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnrtIGT" + i;
                                btn.Text = "Improve google Translation";
                                btn.Click += (s, e) => { ImpGoogleTrans(RTorderid, comp, rt, "1"); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnrtGTText" + i;
                                btn1.Text = "Google Translation Text Ok ";
                                btn1.Click += (s, e) => { GoogleTranText(RTorderid, rt); };
                                divTitle.Controls.Add(btn1);


                                // lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                            }
                            if (rt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnrtIHT" + i;
                                btn.Text = "Improve Human Translation Text";
                                btn.Click += (s, e) => { ImpGoogleTrans(RTorderid, comp, rt, "2"); };
                                divTitle.Controls.Add(btn);

                            }
                            Button delrtbtn = new Button();
                            delrtbtn.CssClass = "btncsdel";
                            delrtbtn.ID = "btnrtDel" + i;
                            delrtbtn.Text = "Remove";
                            delrtbtn.Click += (s, e) => { callingDel(RTorderid); };
                            divTitle.Controls.Add(delrtbtn);

                            yourHTMLstring = "</div><select class='ddlclass' id=" + RTddlid + ">";
                            yourHTMLstring = yourHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring));

                            Button Addrtbutton = new Button();
                            Addrtbutton.CssClass = "btncs";
                            Addrtbutton.ID = "btnrtAdd" + i;
                            Addrtbutton.Text = "Add";
                            Addrtbutton.Click += (s, e) => { calling(RTorderid); };
                            divTitle.Controls.Add(Addrtbutton);

                            string rtn1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(rtn1));
                        }
                        else
                        {
                            if (rt == null)
                                yourHTMLstring = "<div><div id=" + RTdivid + " class='Main'>RichText: </div></div>";
                            else
                                yourHTMLstring = "<div><div id=" + RTdivid + " class='Main'> RichText:" + rt.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring));

                        }
                        break;

                    case EComponentType.RichRichText:
                        PHText rrt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichRichText);
                        var RRTdivid = "RichRichText" + i;
                        var RRTddlid = "Rtddl" + i;
                        var RRTorderid = comp.ComponentOrder;
                        string yourHTMLstring1 = "";
                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            if (rrt == null)
                            {
                                yourHTMLstring1 = "<div><div id=" + RRTdivid + " class='Main'>RichRichText: ";
                                rrt.Text = "";
                            }
                            else
                                yourHTMLstring1 = "<div><div id=" + RRTdivid + " class='Main'> RichRichText:" + rrt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring1));

                            if (rrt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnrrtIGT" + i;
                                btn.Text = "Improve google Translation";
                                btn.Click += (s, e) => { ImpGoogleTrans(RRTorderid, comp, rrt, "1"); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnrrtGTText" + i;
                                btn1.Text = "Google Translation Text Ok ";
                                btn1.Click += (s, e) => { GoogleTranText(RRTorderid, rrt); };
                                divTitle.Controls.Add(btn1);


                                // lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                            }
                            if (rrt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnrrtIHT" + i;
                                btn.Text = "Improve Human Translation Text";
                                btn.Click += (s, e) => { ImpGoogleTrans(RRTorderid, comp, rrt, "2"); };
                                divTitle.Controls.Add(btn);

                            }
                            Button delrrtbtn = new Button();
                            delrrtbtn.CssClass = "btncsdel";
                            delrrtbtn.ID = "btnrrtDel" + i;
                            delrrtbtn.Text = "Remove";
                            delrrtbtn.Click += (s, e) => { callingDel(RRTorderid); };
                            divTitle.Controls.Add(delrrtbtn);

                            yourHTMLstring1 = "</div><select class='ddlclass' id=" + RRTddlid + ">";
                            yourHTMLstring1 = yourHTMLstring1 + ddl;
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring1));

                            Button Addrrtbutton = new Button();
                            Addrrtbutton.CssClass = "btncs";
                            Addrrtbutton.ID = "btnrrtAdd" + i;
                            Addrrtbutton.Text = "Add";
                            Addrrtbutton.Click += (s, e) => { calling(RRTorderid); };
                            divTitle.Controls.Add(Addrrtbutton);

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else
                        {
                            if (rrt == null)
                                yourHTMLstring1 = "<div><div id=" + RRTdivid + " class='Main'>RichRichText: </div></div>";
                            else
                                yourHTMLstring1 = "<div><div id=" + RRTdivid + " class='Main'> RichRichText:" + rrt.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring1));

                        }
                        break;

                    case EComponentType.Latex:
                        PHLatex lt = bh.GetCurrentVersionLatexText(curlan, comp.PluggComponentId, ELatexItemType.PluggComponentLatex);
                        var ltdivid = "Latex" + i;
                        var ltddlid = "ltddl" + i;
                        var ltorderid = comp.ComponentOrder;
                        string yourHTMLstring2 = "";
                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            if (lt == null)
                            {
                                yourHTMLstring = "<div><div id=" + ltdivid + " class='Main'>Latex:</div>";
                                lt.Text = "";
                            }
                            else
                                yourHTMLstring = "<div><div id=" + ltdivid + " class='Main'> Latex:" + lt.Text + "</div>";

                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring2));

                            Button delltbtn = new Button();
                            delltbtn.CssClass = "btncsdel";
                            delltbtn.ID = "btnltDel" + i;
                            delltbtn.Text = "Remove";
                            delltbtn.Click += (s, e) => { callingDel(ltorderid); };
                            divTitle.Controls.Add(delltbtn);

                            yourHTMLstring2 = "<select class='ddlclass' id=" + ltddlid + ">";
                            yourHTMLstring2 = yourHTMLstring2 + ddl;
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring2));

                            Button Addltbutton = new Button();
                            Addltbutton.CssClass = "btncs";
                            Addltbutton.ID = "btnltAdd" + i;
                            Addltbutton.Text = "Add";
                            Addltbutton.Click += (s, e) => { calling(ltorderid); };
                            divTitle.Controls.Add(Addltbutton);

                            string nlt = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nlt));
                        }
                        else
                        {
                            if (lt == null)
                                yourHTMLstring2 = "<div><div id=" + ltdivid + " class='Main'>Latex: </div></div>";
                            else
                                yourHTMLstring2 = "<div><div id=" + ltdivid + " class='Main'> Latex:" + lt.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring2));

                        }
                        break;


                    case EComponentType.YouTube:
                        YouTube yt = bh.GetYouTubeByComponentId(comp.PluggComponentId);
                        if (EditStr == "1" && IsAuthorized == true)
                        {

                        }
                        else
                        {

                        }
                        //Handle YouTube. The iframe is in:
                        //public string GetIframeString(string p.CultureCode) 
                        break;

                }
                i++;
            }


            //bool IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));
            //string editQS = Request.QueryString["edit"];
            //if (editQS != null && !string.IsNullOrWhiteSpace(editQS) && editQS.ToLower() == "true" && IsAuthorized)
            //{
            //    divTitle.Style.Add("display", "block");
            //    lblLatexSepretor.Style.Add("display", "block");
            //    btnEditLatextText.Style.Add("display", "block");
            //    btnEditHtmlText.Style.Add("display", "block");
            //    btnExitEditMode.Visible = true;
            //    lblTitle.Text = p.TheTitle.Text;
            //}
            //else
            //{
            //    if (IsAuthorized)
            //    {
            //        btnEditPlugg.Visible = true;
            //    }
            //    divTitle.Style.Add("display", "none");
            //    lblLatexSepretor.Style.Add("display", "none");
            //    btnEditLatextText.Style.Add("display", "none");
            //    btnExitEditMode.Visible = false;
            //    btnEditHtmlText.Style.Add("display", "none");
            //}
            //btnCancelLatext.Style.Add("display", "none");
            //btnCancelTitle.Style.Add("display", "none");
            //btnCancelHtmlText.Style.Add("display", "none");

            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "inithide", "$('.dnnForm.dnnTextEditor.dnnClear').hide();", true);
        }

        private void GoogleTranText(int orderid, PHText txt)
        {
            txt.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
        }

        //protected override void InitializeCulture()
        //{
        //    base.InitializeCulture();
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Session["Culture"].ToString());
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Session["Culture"].ToString());

        //}








        private void ImpGoogleTrans(int orderid, PluggComponent comp, PHText CulTxt, string Translatetype)
        {

            hdnlabel.Value = Convert.ToString(comp.PluggComponentId);

            string text = CulTxt.Text;
            switch (comp.ComponentType)
            {


                case EComponentType.Label:

                    pnlRRT.Visible = false;
                    pnllabel.Visible = true;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;

                    txtlabel.Text = text;

                    break;

                case EComponentType.RichText:
                    pnlRRT.Visible = false;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = true;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "vvvvvvvvvvvvvvvvvvvvv", " $(document).ready(function () {$('#editor').html('" + text.Replace("\r\n", "<br />") + "')});", true);

                    break;


                case EComponentType.RichRichText:

                    pnlRRT.Visible = true;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;
                    richrichtext.Text = text;

                    break;

                case EComponentType.Latex:
                    pnlRRT.Visible = false;
                    pnllabel.Visible = false;
                    pnlletex.Visible = true;

                    richtextbox.Visible = false;
                    txtletex.Text = text;

                    break;


            }


        }


        private void callingDel(int orderid)
        {
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);

            plugghandler.DeleteComponent(p, orderid);
            PageLoadFun();
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));

        }

        private void calling(int orderid)
        {

            var id = hdn.Value;
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);
            PluggComponent pc = new PluggComponent();
            pc.ComponentOrder = orderid + 1;
            //pc.ComponentType = EComponentType.RichRichText;
            pc.ComponentType = (EComponentType)Enum.Parse(typeof(EComponentType), id);
            plugghandler.AddComponent(p, pc);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));


        }

        protected void btnSaveTitle_Click(object sender, EventArgs e)
        {

            //BaseHandler plugghandler = new BaseHandler();
            //int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //PluggContainer p = new PluggContainer();
            //p.ThePlugg = plugghandler.GetPlugg(pluggid);
            //string curlan = (Page as PageBase).PageCulture.Name;
            //p.CultureCode = curlan;
            //p.LoadTitle();
            //if (p.TheTitle.Text != txtSaveTitle.Text)
            //{
            //    p.TheTitle.Text = txtSaveTitle.Text;
            //    plugghandler.SavePhText(p.TheTitle);
            //}
            //SetPageText(curlan, p);

            //btnSaveTitle.Style.Add("display", "none");
            //txtSaveTitle.Style.Add("display", "none");
        }

        protected void btnSaveLatext_Click(object sender, EventArgs e)
        {
            //BaseHandler plugghandler = new BaseHandler();
            //int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //PluggContainer p = new PluggContainer();
            //p.ThePlugg = plugghandler.GetPlugg(pluggid);
            //string curlan = (Page as PageBase).PageCulture.Name;
            //p.CultureCode = curlan;
            //p.LoadLatexText();
            //if (p.TheLatex.Text != txtLatextText.Text)
            //{
            //    p.TheTitle.Text = txtLatextText.Text;
            //    plugghandler.SaveLatexText(p.TheLatex);
            //}

            //SetPageText(curlan, p);
            //btnSaveLatext.Style.Add("display", "none");
            //txtLatextText.Style.Add("display", "none");
        }

        protected void btnSaveHtmltext_Click(object sender, EventArgs e)
        {
            //BaseHandler plugghandler = new BaseHandler();
            //int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //PluggContainer p = new PluggContainer();
            //p.ThePlugg = plugghandler.GetPlugg(pluggid);
            //string curlan = (Page as PageBase).PageCulture.Name;
            //p.CultureCode = curlan;
            //p.LoadHtmlText();
            //if (p.TheHtmlText.Text != txtHtmlText.Text)
            //{
            //    p.TheHtmlText.Text = txtHtmlText.Text;
            //    plugghandler.SavePhText(p.TheHtmlText);
            //}

            //SetPageText(curlan, p);
            //btnSaveHtmltext.Style.Add("display", "none");
        }

        protected void btnEditPlugg_Click(object sender, EventArgs e)
        {
            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=true"));
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));

        }

        protected void btnExitEditMode_Click(object sender, EventArgs e)
        {
            // Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, ""));
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
            btncanceledit.Visible = true;
            btnSaveSubjects.Visible = false;
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
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void Save_Click(object sender, EventArgs e)
        {

            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);


            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            PluggContainer p = new PluggContainer(curlan, pluggid);
            List<PluggComponent> comps = p.GetComponentList();
            PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
            BaseHandler bh = new BaseHandler();

            var comtype = cToAdd.ComponentType;

            PHText lbl = bh.GetCurrentVersionText(curlan, itemid, ETextItemType.PluggComponentLabel);

            lbl.Text = txtlabel.Text;
            lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(lbl);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));



        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }

        protected void btntransplug_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            PluggContainer p = new PluggContainer(curlan, pluggid);
            List<PluggComponent> comps = p.GetComponentList();
            PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
            BaseHandler bh = new BaseHandler();

            var comtype = cToAdd.ComponentType;


            PHText RichRichText = bh.GetCurrentVersionText(curlan, itemid, ETextItemType.PluggComponentRichRichText);
            RichRichText.Text = richrichtext.Text;
            RichRichText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(RichRichText);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            PluggContainer p = new PluggContainer(curlan, pluggid);
            List<PluggComponent> comps = p.GetComponentList();
            PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
            BaseHandler bh = new BaseHandler();

            var comtype = cToAdd.ComponentType;

            PHText RichText = bh.GetCurrentVersionText(curlan, itemid, ETextItemType.PluggComponentRichText);

            RichText.Text = hdnrichtext.Value;
            RichText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
            bh.SavePhText(RichText);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));


        }
    }
}
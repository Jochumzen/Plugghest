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
        string Add1, btnSaveSubjects1, btnEdit1, btncanceledit1, btncanceltrans1, btnlocal1, btntransplug1, Cancel1, GoogleTransTxtOk1, ImpgoogleTrans1, ImproveHumTransTxt1, Label1, Latex1, Remove1, RichRichText1, RichText1, Save1, YouTube1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
                CallLocalization(curlan);


                EditStr = Page.Request.QueryString["edit"];

                if (!IsPostBack)
                {
                    PageLoadFun();
                    pnlRRT.Visible = false;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;
                    pnlLatex.Visible = false;
                    pnlYoutube.Visible = false;
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

        private void CallLocalization(string curlan)
        {

            btnSaveSubjects1 = Localization.GetString("btnSaveSubjects", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            btncanceledit1 = Localization.GetString("btncanceledit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            Add1 = Localization.GetString("Add", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btncanceltrans1 = Localization.GetString("btncanceltrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            Cancel1 = Localization.GetString("Cancel", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            GoogleTransTxtOk1 = Localization.GetString("GoogleTransTxtOk", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            ImpgoogleTrans1 = Localization.GetString("ImpgoogleTrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            ImproveHumTransTxt1 = Localization.GetString("ImproveHumTransTxt", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            Label1 = Localization.GetString("Label", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            Latex1 = Localization.GetString("Latex", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            Remove1 = Localization.GetString("Remove", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            RichRichText1 = Localization.GetString("RichRichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            RichText1 = Localization.GetString("RichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            Save1 = Localization.GetString("Save", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            YouTube1 = Localization.GetString("YouTube", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btnlocal1 = Localization.GetString("btnlocal", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btntransplug1 = Localization.GetString("btntransplug", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            btnEdit1 = Localization.GetString("Edit", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            btnlocal.Text = btnlocal1;
            btnSaveSubjects.Text = btnSaveSubjects1;
            btncanceledit.Text = btncanceledit1;
            btncanceltrans.Text = btncanceltrans1;
            btntransplug.Text = btntransplug1;
            btncanceledit.Text = btncanceledit1;
            btnSaveRt.Text = Save1;
            btnCanRt.Text = Cancel1;
            btnLabelSave.Text = Save1;
            Cancel.Text = Cancel1;
        }

        private void PageLoadFun()
        {
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);
            IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));

            if (p.CultureCode == p.ThePlugg.CreatedInCultureCode)
                btnlocal.Visible = false;
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
                btncanceltrans.Visible = false;

            }
            else
            {
                btnSaveSubjects.Visible = false;
                btncanceledit.Visible = false;
                btncanceltrans.Visible = false;

            }
            if (EditStr == "1" && IsAuthorized == true)
            {
                btncanceledit.Visible = true;
                btnSaveSubjects.Visible = false;
                btncanceltrans.Visible = false;
            }
            else
            {

            }
            if (EditStr == "2" && IsAuthorized == true)
            {
                btncanceltrans.Visible = true;
                btntransplug.Visible = false;
            }
            else
            {

            }



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
                        string LabHTMLstring = "";


                        if (EditStr == "1" && IsAuthorized == true)
                        {

                            if (lbl == null)
                            {
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'> <label  id='lbllabel" + i + "' runat='server' >" + Label1 + ":</label>";
                                lbl.Text = "";
                            }
                            else
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'><label  id='lbllabel" + i + "' runat='server'  >" + Label1 + ":" + lbl.Text + "</label>";

                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));


                            Button delbtn = new Button();
                            delbtn.CssClass = "btncsdel";
                            delbtn.ID = "btnlbDel" + i;
                            delbtn.Text = Remove1;
                            delbtn.Click += (s, e) => { callingDel(orderid); };
                            divTitle.Controls.Add(delbtn);

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnlbEdit" + i;
                            editbtn.Text = btnEdit1;
                            editbtn.Click += (s, e) => { ImpGoogleTrans(orderid, comp, lbl, "1"); };
                            divTitle.Controls.Add(editbtn);



                            LabHTMLstring = "</div><select class='ddlclass' id=" + ddlid + ">";
                            LabHTMLstring = LabHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));

                            Button Addbutton = new Button();
                            Addbutton.CssClass = "btncs";
                            Addbutton.ID = "btnlbAdd" + i;
                            Addbutton.Text = Add1;
                            Addbutton.Click += (s, e) => { calling(orderid); };
                            divTitle.Controls.Add(Addbutton);

                            string n1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(n1));
                        }
                        else if (EditStr == "2" && IsAuthorized == true)
                        {
                            if (lbl == null)
                            {
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'> <label  id='lbllabel" + i + "' runat='server' >" + Label1 + ":</label>";
                                lbl.Text = "";
                            }
                            else
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'><label  id='lbllabel" + i + "' runat='server'  >" + Label1 + ":" + lbl.Text + "</label>";

                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnIGT" + i;
                                btn.Text = ImpgoogleTrans1;
                                btn.Click += (s, e) => { ImpGoogleTrans(orderid, comp, lbl, "1"); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnGTText" + i;
                                btn1.Text = GoogleTransTxtOk1;
                                btn1.Click += (s, e) => { GoogleTranText(orderid, lbl); };
                                divTitle.Controls.Add(btn1);

                            }
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnIHT" + i;
                                btn.Text = ImproveHumTransTxt1;
                                btn.Click += (s, e) => { ImpGoogleTrans(orderid, comp, lbl, "2"); };
                                divTitle.Controls.Add(btn);

                            }


                            string n1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(n1));
                        }

                        else
                        {
                            if (lbl == null)
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'" + Label1 + ": </div></div>";
                            else
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'>" + Label1 + ":" + lbl.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));

                        }
                        break;

                    case EComponentType.RichText:
                        PHText rt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichText);
                        //Handle rich text

                        string RtHTMLstring = "";

                        divTitle.Controls.Add(new LiteralControl(RtHTMLstring));
                        var RTdivid = "RichText" + i;
                        var RTddlid = "Rtddl" + i;
                        var RTorderid = comp.ComponentOrder;

                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            if (rt == null)
                            {
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'>" + RichText1 + ": ";
                                rt.Text = "";
                            }
                            else
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'> " + RichText1 + ":" + rt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));


                            Button delrtbtn = new Button();
                            delrtbtn.CssClass = "btncsdel";
                            delrtbtn.ID = "btnrtDel" + i;
                            //delrtbtn.Text = "Remove";
                            delrtbtn.Text = Remove1;
                            delrtbtn.Click += (s, e) => { callingDel(RTorderid); };
                            divTitle.Controls.Add(delrtbtn);

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnrtEdit" + i;
                            editbtn.Text = btnEdit1;
                            editbtn.Click += (s, e) => { ImpGoogleTrans(RTorderid, comp, rt, "1"); };
                            divTitle.Controls.Add(editbtn);

                            RtHTMLstring = "</div><select class='ddlclass' id=" + RTddlid + ">";
                            RtHTMLstring = RtHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));

                            Button Addrtbutton = new Button();
                            Addrtbutton.CssClass = "btncs";
                            Addrtbutton.ID = "btnrtAdd" + i;
                            //Addrtbutton.Text = "Add";
                            Addrtbutton.Text = Add1;
                            Addrtbutton.Click += (s, e) => { calling(RTorderid); };
                            divTitle.Controls.Add(Addrtbutton);

                            string rtn1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(rtn1));
                        }
                        else if (EditStr == "2" && IsAuthorized == true)
                        {
                            if (rt == null)
                            {
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'>" + RichText1 + ": ";
                                rt.Text = "";
                            }
                            else
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'> " + RichText1 + ":" + rt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));

                            if (rt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnrtIGT" + i;
                                // btn.Text = "Improve google Translation";
                                btn.Text = ImpgoogleTrans1;
                                btn.Click += (s, e) => { ImpGoogleTrans(RTorderid, comp, rt, "1"); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnrtGTText" + i;
                                // btn1.Text = "Google Translation Text Ok ";
                                btn1.Text = GoogleTransTxtOk1;
                                btn1.Click += (s, e) => { GoogleTranText(RTorderid, rt); };
                                divTitle.Controls.Add(btn1);


                                // lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                            }
                            if (rt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnrtIHT" + i;
                                //btn.Text = "Improve Human Translation Text";
                                btn.Text = ImproveHumTransTxt1;
                                btn.Click += (s, e) => { ImpGoogleTrans(RTorderid, comp, rt, "2"); };
                                divTitle.Controls.Add(btn);

                            }

                            string rtn1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(rtn1));
                        }
                        else
                        {
                            if (rt == null)
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'>" + RichText1 + ": </div></div>";
                            else
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'> " + RichText1 + ":" + rt.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));

                        }
                        break;

                    case EComponentType.RichRichText:
                        PHText rrt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichRichText);
                        var RRTdivid = "RichRichText" + i;
                        var RRTddlid = "Rtddl" + i;
                        var RRTorderid = comp.ComponentOrder;
                        string RRTHTMLstring = "";
                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            if (rrt == null)
                            {
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'>" + RichRichText1 + ": ";
                                rrt.Text = "";
                            }
                            else
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'> " + RichRichText1 + ":" + rrt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button delrrtbtn = new Button();
                            delrrtbtn.CssClass = "btncsdel";
                            delrrtbtn.ID = "btnrrtDel" + i;
                            //delrrtbtn.Text = "Remove";
                            delrrtbtn.Text = Remove1;
                            delrrtbtn.Click += (s, e) => { callingDel(RRTorderid); };
                            divTitle.Controls.Add(delrrtbtn);


                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnrrtEdit" + i;
                            editbtn.Text = btnEdit1;
                            editbtn.Click += (s, e) => { ImpGoogleTrans(RRTorderid, comp, rrt, "1"); };
                            divTitle.Controls.Add(editbtn);

                            RRTHTMLstring = "</div><select class='ddlclass' id=" + RRTddlid + ">";
                            RRTHTMLstring = RRTHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button Addrrtbutton = new Button();
                            Addrrtbutton.CssClass = "btncs";
                            Addrrtbutton.ID = "btnrrtAdd" + i;
                            // Addrrtbutton.Text = "Add";
                            Addrrtbutton.Text = Add1;
                            Addrrtbutton.Click += (s, e) => { calling(RRTorderid); };
                            divTitle.Controls.Add(Addrrtbutton);

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else if (EditStr == "2" && IsAuthorized == true)
                        {
                            if (rrt == null)
                            {
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'>" + RichRichText1 + ": ";
                                rrt.Text = "";
                            }
                            else
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'> " + RichRichText1 + ":" + rrt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            if (rrt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnrrtIGT" + i;
                                btn.Text = ImpgoogleTrans1;
                                btn.Click += (s, e) => { ImpGoogleTrans(RRTorderid, comp, rrt, "1"); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnrrtGTText" + i;
                                btn1.Text = GoogleTransTxtOk1;
                                btn1.Click += (s, e) => { GoogleTranText(RRTorderid, rrt); };
                                divTitle.Controls.Add(btn1);


                            }
                            if (rrt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnrrtIHT" + i;
                                btn.Text = ImproveHumTransTxt1;
                                btn.Click += (s, e) => { ImpGoogleTrans(RRTorderid, comp, rrt, "2"); };
                                divTitle.Controls.Add(btn);

                            }

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else
                        {
                            if (rrt == null)
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'>" + RichRichText1 + ": </div></div>";
                            else
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'> " + RichRichText1 + ":" + rrt.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                        }
                        break;

                    case EComponentType.Latex:
                        PHLatex lat = bh.GetCurrentVersionLatexText(curlan, comp.PluggComponentId, ELatexItemType.PluggComponentLatex);

                        //PHLatex lt = bh.GetCurrentVersionLatexText(curlan, comp.PluggComponentId, ELatexItemType.PluggComponentLatex);
                        var ltdivid = "Latex" + i;
                        var ltddlid = "ltddl" + i;
                        var ltorderid = comp.ComponentOrder;
                        string LatHTMLstring = "";
                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            if (lat == null)
                            {
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'>" + Latex1 + ":</div>";
                                lat.Text = "";
                            }
                            else
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'> " + Latex1 + ":" + lat.Text + "</div>";

                            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));

                            Button delltbtn = new Button();
                            delltbtn.CssClass = "btncsdel";
                            delltbtn.ID = "btnltDel" + i;
                            // delltbtn.Text = "Remove";
                            delltbtn.Text = Remove1;
                            delltbtn.Click += (s, e) => { callingDel(ltorderid); };
                            divTitle.Controls.Add(delltbtn);

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnltEdit" + i;
                            editbtn.Text = btnEdit1;
                            editbtn.Click += (s, e) => { CallLatFun(ltorderid, comp, lat, "1"); };
                            divTitle.Controls.Add(editbtn);


                            LatHTMLstring = "<select class='ddlclass' id=" + ltddlid + ">";
                            LatHTMLstring = LatHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));

                            Button Addltbutton = new Button();
                            Addltbutton.CssClass = "btncs";
                            Addltbutton.ID = "btnltAdd" + i;
                            //Addltbutton.Text = "Add";
                            Addltbutton.Text = Add1;
                            Addltbutton.Click += (s, e) => { calling(ltorderid); };
                            divTitle.Controls.Add(Addltbutton);

                            string nlt = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nlt));
                        }
                        else
                        {
                            if (lat == null)
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'>" + Latex1 + ": </div></div>";
                            else
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'> " + Latex1 + ":" + lat.Text + "</div></div>";
                            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));

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
                        string yourHTMLstring3 = "";
                        if (EditStr == "1" && IsAuthorized == true)
                        {
                            RRTHTMLstring = "<div><div id=" + ytdivid + " class='Main'>YouTube::" + ytYouTubecode + "";

                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button delrrtbtn = new Button();
                            delrrtbtn.CssClass = "btncsdel";
                            delrrtbtn.ID = "btnytDel" + i;
                            //delrrtbtn.Text = "Remove";
                            delrrtbtn.Text = Remove1;
                            delrrtbtn.Click += (s, e) => { callingDel(ytorderid); };
                            divTitle.Controls.Add(delrrtbtn);


                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnrrtEdit" + i;
                            editbtn.Text = btnEdit1;
                            editbtn.Click += (s, e) => { YouTubeEdit(ytorderid, comp, yt, "1"); };
                            divTitle.Controls.Add(editbtn);

                            RRTHTMLstring = "</div>" + strYoutubeIframe + "</br><select class='ddlclass' id=" + ytddlid + ">";
                            RRTHTMLstring = RRTHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button Addrrtbutton = new Button();
                            Addrrtbutton.CssClass = "btncs";
                            Addrrtbutton.ID = "btnytAdd" + i;
                            // Addrrtbutton.Text = "Add";
                            Addrrtbutton.Text = Add1;
                            Addrrtbutton.Click += (s, e) => { calling(ytorderid); };
                            divTitle.Controls.Add(Addrrtbutton);

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else
                        {
                            yourHTMLstring3 = "<div><div id=" + ytdivid + " class='Main'>  YouTube:" + ytYouTubecode + "</div>" + strYoutubeIframe + "</div>";
                            divTitle.Controls.Add(new LiteralControl(yourHTMLstring3));
                        }

                        break;

                }
                i++;
            }
        }

        private void CallLatFun(int ltorderid, PluggComponent comp, PHLatex lat, string p)
        {

            hdnlabel.Value = Convert.ToString(comp.PluggComponentId);

            switch (comp.ComponentType)
            {
                case EComponentType.Latex:
                    pnlRRT.Visible = true;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;
                    pnlLatex.Visible = false;
                    pnlYoutube.Visible = false;
                    richrichtext.Text = lat.Text;
                    break;


            }
        }

        private void YouTubeEdit(int ytorderid, PluggComponent comp, YouTube yt, string p)
        {
            string ytcode = "";
            if (yt == null)
                ytcode = "";
            else
                ytcode = yt.YouTubeCode;
            hdnlabel.Value = Convert.ToString(comp.PluggComponentId);

            switch (comp.ComponentType)
            {


                case EComponentType.YouTube:

                    pnlLatex.Visible = false;
                    pnlYoutube.Visible = true;
                    pnlRRT.Visible = false;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;
                    txtYouTube.Text = ytcode;

                    break;

            }
        }

        private void callingEdit(int orderid)
        {
            throw new NotImplementedException();
        }

        private void GoogleTranText(int orderid, PHText txt)
        {
            txt.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
        }








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
                    pnlLatex.Visible = false;
                    pnlYoutube.Visible = false;
                    txtlabel.Text = text;

                    break;

                case EComponentType.RichText:
                    pnlRRT.Visible = false;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = true;
                    pnlLatex.Visible = false;
                    pnlYoutube.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "xx", " $(document).ready(function () {$('#editor').html('" + text.Replace("\r\n", "<br />") + "')});", true);



                    break;


                case EComponentType.RichRichText:

                    pnlRRT.Visible = true;
                    pnllabel.Visible = false;
                    pnlletex.Visible = false;
                    richtextbox.Visible = false;
                    pnlLatex.Visible = false;
                    pnlYoutube.Visible = false;
                    richrichtext.Text = text;

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

        protected void btnLabelSave_Click(object sender, EventArgs e)
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
            bh.SavePhText(lbl);
            if (EditStr == "2")
            {
                lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));
            }
            if (EditStr == "1")
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));

        }



        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=" + EditStr + ""));
        }



        protected void btntransplug_Click(object sender, EventArgs e)
        {
            btntransplug.Visible = false;
            btncanceltrans.Visible = true;
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));

            //Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));

        }


        protected void btnSaveRt_Click(object sender, EventArgs e)
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

            bh.SavePhText(RichText);
            if (EditStr == "2")
            {
                RichText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));
            }
            if (EditStr == "1")
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));


        }

        protected void btnlocal_Click(object sender, EventArgs e)
        {

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);
            IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));

            p.CultureCode = p.ThePlugg.CreatedInCultureCode;

            PluggContainer p1 = new PluggContainer(p.CultureCode, pluggid);

            curlan = p.CultureCode;
            CallLocalization(curlan);

            SetPageText(p.CultureCode, p1);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());


        }

        protected void btncanceltrans_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnSaveRRt_Click(object sender, EventArgs e)
        {
            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            PluggContainer p = new PluggContainer(curlan, pluggid);
            List<PluggComponent> comps = p.GetComponentList();
            PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
            BaseHandler bh = new BaseHandler();

            var comtype = cToAdd.ComponentType;
            PHText RichRichText = null;
            switch (cToAdd.ComponentType)
            {


                case EComponentType.RichRichText:
                    RichRichText = bh.GetCurrentVersionText(curlan, itemid, ETextItemType.PluggComponentRichRichText);
                    RichRichText.Text = richrichtext.Text;
                    bh.SavePhText(RichRichText);
                    break;

                case EComponentType.Latex:

                    PHLatex latex = bh.GetCurrentVersionLatexText(curlan, Convert.ToInt32(id), ELatexItemType.PluggComponentLatex);

                    latex.Text = richrichtext.Text;
                    bh.SaveLatexText(latex);
                    break;
            }

            if (EditStr == "2")
            {
                RichRichText.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));
            }
            if (EditStr == "1")
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));


        }

        protected void btnYtSave_Click(object sender, EventArgs e)
        {
            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            PluggContainer p = new PluggContainer(curlan, pluggid);
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

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }

        protected void btnLatexSave_Click(object sender, EventArgs e)
        {

            var id = hdnlabel.Value;
            var itemid = Convert.ToInt32(id);

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            PluggContainer p = new PluggContainer(curlan, pluggid);
            List<PluggComponent> comps = p.GetComponentList();

            BaseHandler bh = new BaseHandler();


            List<object> objToadd = new List<object>();

            PHLatex lt = bh.GetCurrentVersionLatexText(curlan, Convert.ToInt32(id), ELatexItemType.PluggComponentLatex);
            lt.HtmlText = richrichtext.Text;
            objToadd.Add(lt);

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));

        }
    }
}
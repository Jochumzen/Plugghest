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
        string EditStr = ""; string curlan = "";
        bool IsAuthorized = false;
        private System.Resources.ResourceManager rm;
        string AddObj, btnSaveSubjectsObj,YoutubeObj, btnEditObj, btncanceleditObj, btncanceltransObj, btnlocalObj, btntransplugObj, CancelObj, GoogleTransTxtOkObj, ImpgoogleTransObj, ImproveHumTransTxtObj, LabelObj, LatexObj, RemoveObj, RichRichTextObj, RichTextObj, SaveObj, YouTubeObj;
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

            btnSaveSubjectsObj = Localization.GetString("btnSaveSubjects", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            btncanceleditObj = Localization.GetString("btncanceledit", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            AddObj = Localization.GetString("Add", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btncanceltransObj = Localization.GetString("btncanceltrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            CancelObj = Localization.GetString("Cancel", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            GoogleTransTxtOkObj = Localization.GetString("GoogleTransTxtOk", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            ImpgoogleTransObj = Localization.GetString("ImpgoogleTrans", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            ImproveHumTransTxtObj = Localization.GetString("ImproveHumTransTxt", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            LabelObj = Localization.GetString("Label", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            LatexObj = Localization.GetString("Latex", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            YoutubeObj = Localization.GetString("YouTube", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            RemoveObj = Localization.GetString("Remove", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            RichRichTextObj = Localization.GetString("RichRichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            RichTextObj = Localization.GetString("RichText", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            SaveObj = Localization.GetString("Save", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            YouTubeObj = Localization.GetString("YouTube", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btnlocalObj = Localization.GetString("btnlocal", this.LocalResourceFile + ".ascx." + curlan + ".resx");
            btntransplugObj = Localization.GetString("btntransplug", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            btnEditObj = Localization.GetString("Edit", this.LocalResourceFile + ".ascx." + curlan + ".resx");

            btnlocal.Text = btnlocalObj;
            btnSaveSubjects.Text = btnSaveSubjectsObj;
            btncanceledit.Text = btncanceleditObj;
            btncanceltrans.Text = btncanceltransObj;
            btntransplug.Text = btntransplugObj;
            btncanceledit.Text = btncanceleditObj;
            btnSaveRt.Text = SaveObj;
            btnCanRt.Text = CancelObj;
            btnLabelSave.Text = SaveObj;
            Cancel.Text = CancelObj;
        }

        private void PageLoadFun()
        {
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);    
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);
            IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));

            if (p.CultureCode == p.ThePlugg.CreatedInCultureCode)
                btnlocal.Visible = false;
            SetPageText(curlan, p);
            ViewState.Add("falg", true);
        }
     
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

//creating  dropdownlist 
            string ddl = "";
            foreach (string name in Enum.GetNames(typeof(EComponentType)))
            {
                if (name != "NotSet")
                {
                    string dl = "<option  value=" + name + " >" + name + "</option>";
                    ddl = ddl + dl;

                }
            }

            Label dynamicLabel = new Label();
            int i = 0;

            var subid = p.ThePlugg.SubjectId;
            if (subid != null)
            {
                string TreeHTMLstring = "";

                int ID = Convert.ToInt32(subid);
                BindTree(ID);           
                if (EditStr == "1" && IsAuthorized == true)
                {
                    TreeHTMLstring = "<input type='button' id='btnTreeEdit" + i + "' class='btnTreeEdit' value='" + btnEditObj + "' />";
                    divTree.Controls.Add(new LiteralControl(TreeHTMLstring));
                }
            }
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

                        //This condition is used for editing plugg
                        if (EditStr == "1" && IsAuthorized == true)
                        {

                            if (lbl == null)
                            {
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'> <label  id='lbllabel" + i + "' runat='server' >" + LabelObj + ":</label>";
                                lbl.Text = "";
                            }
                            else
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'><label  id='lbllabel" + i + "' runat='server'  >" + LabelObj + ":" + lbl.Text + "</label>";

                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));


                            Button delbtn = new Button();
                            delbtn.CssClass = "btncsdel";
                            delbtn.ID = "btnlbDel" + i;
                            delbtn.Text = RemoveObj;
                            delbtn.Click += (s, e) => { callingDel(orderid); };
                            divTitle.Controls.Add(delbtn);

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnlbEdit" + i;
                            editbtn.Text = btnEditObj;
                            editbtn.Click += (s, e) => { ImpGoogleTrans( comp, lbl); };
                            divTitle.Controls.Add(editbtn);



                            LabHTMLstring = "</div><select class='ddlclass' id=" + ddlid + ">";
                            LabHTMLstring = LabHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));

                            Button Addbutton = new Button();
                            Addbutton.CssClass = "btncs";
                            Addbutton.ID = "btnlbAdd" + i;
                            Addbutton.Text = AddObj;
                            Addbutton.Click += (s, e) => { callingAdd(orderid); };
                            divTitle.Controls.Add(Addbutton);

                            string n1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(n1));
                        }
                        //This condition is used for Translation The Plugg Text(same for all cases)
                        else if (EditStr == "2" && IsAuthorized == true)
                        {
                            if (lbl == null)
                            {
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'> <label  id='lbllabel" + i + "' runat='server' >" + LabelObj + ":</label>";
                                lbl.Text = "";
                            }
                            else
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'><label  id='lbllabel" + i + "' runat='server'  >" + LabelObj + ":" + lbl.Text + "</label>";

                            divTitle.Controls.Add(new LiteralControl(LabHTMLstring));
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnIGT" + i;
                                btn.Text = ImpgoogleTransObj;
                                btn.Click += (s, e) => { ImpGoogleTrans(comp, lbl); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnGTText" + i;
                                btn1.Text = GoogleTransTxtOkObj;
                                btn1.Click += (s, e) => { GoogleTranText(lbl); };
                                divTitle.Controls.Add(btn1);

                            }
                            if (lbl.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnIHT" + i;
                                btn.Text = ImproveHumTransTxtObj;
                                btn.Click += (s, e) => { ImpGoogleTrans(comp, lbl); };
                                divTitle.Controls.Add(btn);

                            }


                            string n1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(n1));
                        }

                        else
                        {
                            if (lbl == null)
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'" + LabelObj + ": </div></div>";
                            else
                                LabHTMLstring = "<div><div id=" + divid + " class='Main'>" + LabelObj + ":" + lbl.Text + "</div></div>";
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
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'>" + RichTextObj + ": ";
                                rt.Text = "";
                            }
                            else
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'> " + RichTextObj + ":" + rt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));


                            Button delrtbtn = new Button();
                            delrtbtn.CssClass = "btncsdel";
                            delrtbtn.ID = "btnrtDel" + i;
                            //delrtbtn.Text = "Remove";
                            delrtbtn.Text = RemoveObj;
                            delrtbtn.Click += (s, e) => { callingDel(RTorderid); };
                            divTitle.Controls.Add(delrtbtn);

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnrtEdit" + i;
                            editbtn.Text = btnEditObj;
                            editbtn.Click += (s, e) => { ImpGoogleTrans(comp, rt); };
                            divTitle.Controls.Add(editbtn);

                            RtHTMLstring = "</div><select class='ddlclass' id=" + RTddlid + ">";
                            RtHTMLstring = RtHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));

                            Button Addrtbutton = new Button();
                            Addrtbutton.CssClass = "btncs";
                            Addrtbutton.ID = "btnrtAdd" + i;
                            //Addrtbutton.Text = "Add";
                            Addrtbutton.Text = AddObj;
                            Addrtbutton.Click += (s, e) => { callingAdd(RTorderid); };
                            divTitle.Controls.Add(Addrtbutton);

                            string rtn1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(rtn1));
                        }
                        else if (EditStr == "2" && IsAuthorized == true)
                        {
                            if (rt == null)
                            {
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'>" + RichTextObj + ": ";
                                rt.Text = "";
                            }
                            else
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'> " + RichTextObj + ":" + rt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RtHTMLstring));

                            if (rt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnrtIGT" + i;
                                // btn.Text = "Improve google Translation";
                                btn.Text = ImpgoogleTransObj;
                                btn.Click += (s, e) => { ImpGoogleTrans(comp, rt); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnrtGTText" + i;
                                // btn1.Text = "Google Translation Text Ok ";
                                btn1.Text = GoogleTransTxtOkObj;
                                btn1.Click += (s, e) => { GoogleTranText( rt); };
                                divTitle.Controls.Add(btn1);


                                // lbl.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
                            }
                            if (rt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnrtIHT" + i;
                                //btn.Text = "Improve Human Translation Text";
                                btn.Text = ImproveHumTransTxtObj;
                                btn.Click += (s, e) => { ImpGoogleTrans(comp, rt); };
                                divTitle.Controls.Add(btn);

                            }

                            string rtn1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(rtn1));
                        }
                        else
                        {
                            if (rt == null)
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'>" + RichTextObj + ": </div></div>";
                            else
                                RtHTMLstring = "<div><div id=" + RTdivid + " class='Main'> " + RichTextObj + ":" + rt.Text + "</div></div>";
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
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'>" + RichRichTextObj + ": ";
                                rrt.Text = "";
                            }
                            else
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'> " + RichRichTextObj + ":" + rrt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button delrrtbtn = new Button();
                            delrrtbtn.CssClass = "btncsdel";
                            delrrtbtn.ID = "btnrrtDel" + i;
                            //delrrtbtn.Text = "Remove";
                            delrrtbtn.Text = RemoveObj;
                            delrrtbtn.Click += (s, e) => { callingDel(RRTorderid); };
                            divTitle.Controls.Add(delrrtbtn);


                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnrrtEdit" + i;
                            editbtn.Text = btnEditObj;
                            editbtn.Click += (s, e) => { ImpGoogleTrans(comp, rrt); };
                            divTitle.Controls.Add(editbtn);

                            RRTHTMLstring = "</div><select class='ddlclass' id=" + RRTddlid + ">";
                            RRTHTMLstring = RRTHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button Addrrtbutton = new Button();
                            Addrrtbutton.CssClass = "btncs";
                            Addrrtbutton.ID = "btnrrtAdd" + i;
                            // Addrrtbutton.Text = "Add";
                            Addrrtbutton.Text = AddObj;
                            Addrrtbutton.Click += (s, e) => { callingAdd(RRTorderid); };
                            divTitle.Controls.Add(Addrrtbutton);

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else if (EditStr == "2" && IsAuthorized == true)
                        {
                            if (rrt == null)
                            {
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'>" + RichRichTextObj + ": ";
                                rrt.Text = "";
                            }
                            else
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'> " + RichRichTextObj + ":" + rrt.Text + "";

                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            if (rrt.CultureCodeStatus == ECultureCodeStatus.GoogleTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "googletrans";
                                btn.ID = "btnrrtIGT" + i;
                                btn.Text = ImpgoogleTransObj;
                                btn.Click += (s, e) => { ImpGoogleTrans(comp, rrt); };
                                divTitle.Controls.Add(btn);

                                Button btn1 = new Button();
                                btn1.CssClass = "googleTrasok";
                                btn1.ID = "btnrrtGTText" + i;
                                btn1.Text = GoogleTransTxtOkObj;
                                btn1.Click += (s, e) => { GoogleTranText( rrt); };
                                divTitle.Controls.Add(btn1);


                            }
                            if (rrt.CultureCodeStatus == ECultureCodeStatus.HumanTranslated)
                            {
                                Button btn = new Button();
                                btn.CssClass = "btnhumantrans";
                                btn.ID = "btnrrtIHT" + i;
                                btn.Text = ImproveHumTransTxtObj;
                                btn.Click += (s, e) => { ImpGoogleTrans(comp, rrt); };
                                divTitle.Controls.Add(btn);

                            }

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else
                        {
                            if (rrt == null)
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'>" + RichRichTextObj + ": </div></div>";
                            else
                                RRTHTMLstring = "<div><div id=" + RRTdivid + " class='Main'> " + RichRichTextObj + ":" + rrt.Text + "</div></div>";
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
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'>" + LatexObj + ":";
                                lat.Text = "";
                            }
                            else
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'> " + LatexObj + ":" + lat.Text +"";

                            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));

                            Button delltbtn = new Button();
                            delltbtn.CssClass = "btncsdel";
                            delltbtn.ID = "btnltDel" + i;
                            // delltbtn.Text = "Remove";
                            delltbtn.Text = RemoveObj;
                            delltbtn.Click += (s, e) => { callingDel(ltorderid); };
                            divTitle.Controls.Add(delltbtn);

                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnltEdit" + i;
                            editbtn.Text = btnEditObj;
                            editbtn.Click += (s, e) => { CallLatFun(ltorderid, comp, lat, "1"); };
                            divTitle.Controls.Add(editbtn);
                           
                            LatHTMLstring = "</div><select class='ddlclass' id=" + ltddlid + ">";
                            LatHTMLstring = LatHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(LatHTMLstring));

                            Button Addltbutton = new Button();
                            Addltbutton.CssClass = "btncs";
                            Addltbutton.ID = "btnltAdd" + i;
                            //Addltbutton.Text = "Add";
                            Addltbutton.Text = AddObj;
                            Addltbutton.Click += (s, e) => { callingAdd(ltorderid); };
                            divTitle.Controls.Add(Addltbutton);

                            string nlt = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nlt));
                        }
                        else
                        {
                            if (lat == null)
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'>" + LatexObj + ": </div></div>";
                            else
                                LatHTMLstring = "<div><div id=" + ltdivid + " class='Main'> " + LatexObj + ":" + lat.Text + "</div></div>";
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
                            RRTHTMLstring = "<div><div id=" + ytdivid + " class='Main'>" + YoutubeObj + ":" + ytYouTubecode + "";

                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button delrrtbtn = new Button();
                            delrrtbtn.CssClass = "btncsdel";
                            delrrtbtn.ID = "btnytDel" + i;
                            //delrrtbtn.Text = "Remove";
                            delrrtbtn.Text = RemoveObj;
                            delrrtbtn.Click += (s, e) => { callingDel(ytorderid); };
                            divTitle.Controls.Add(delrrtbtn);


                            Button editbtn = new Button();
                            editbtn.CssClass = "btncsdel";
                            editbtn.ID = "btnrrtEdit" + i;
                            editbtn.Text = btnEditObj;
                            editbtn.Click += (s, e) => { YouTubeEdit(ytorderid, comp, yt, "1"); };
                            divTitle.Controls.Add(editbtn);

                            RRTHTMLstring = "</div>" + strYoutubeIframe + "</br><select class='ddlclass' id=" + ytddlid + ">";
                            RRTHTMLstring = RRTHTMLstring + ddl;
                            divTitle.Controls.Add(new LiteralControl(RRTHTMLstring));

                            Button Addrrtbutton = new Button();
                            Addrrtbutton.CssClass = "btncs";
                            Addrrtbutton.ID = "btnytAdd" + i;
                            // Addrrtbutton.Text = "Add";
                            Addrrtbutton.Text = AddObj;
                            Addrrtbutton.Click += (s, e) => { callingAdd(ytorderid); };
                            divTitle.Controls.Add(Addrrtbutton);

                            string nrrt1 = "</select></div>";
                            divTitle.Controls.Add(new LiteralControl(nrrt1));
                        }
                        else
                        {
                            yourHTMLstring3 = "<div><div id=" + ytdivid + " class='Main'>  " + YoutubeObj + ":" + ytYouTubecode + "</div>" + strYoutubeIframe + "</div>";
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

        //private void callingEdit(int orderid)
        //{
        //    throw new NotImplementedException();
        //}

        private void GoogleTranText( PHText txt)
        {
            txt.CultureCodeStatus = ECultureCodeStatus.HumanTranslated;
        }


        public void BindTree(int subid)
        {
            BaseHandler objBaseHandler = new BaseHandler();
            //Subject  objSub = objsubhandler.GetSubject(subid);
          
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
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan, pluggid);
            plugghandler.DeleteComponent(p, orderid);
            PageLoadFun();
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));

        }

        private void callingAdd(int orderid)
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

        protected void btnSelSub_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(hdnNodeSubjectId.Value))
            {
                int id = Convert.ToInt32(hdnNodeSubjectId.Value);
                int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
                string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
                BaseHandler plugghandler = new BaseHandler();
                PluggContainer p = new PluggContainer(curlan, pluggid);
                p.ThePlugg.SubjectId = id;
                p.LoadTitle();
                List<object> blankList = new List<object>();
                // PluggComponent cToAdd = comps.Find(x => x.PluggComponentId == Convert.ToInt32(id));
                BaseHandler bh = new BaseHandler();

                ///after updating record(subject id), it shwoing exception "Cannot create Page. Page with this PageName already exists" in AddPluggPage function.
                try
                {
                    bh.SavePlugg(p, blankList);
                }
                catch (Exception)
                {
                }
            }
            else
            {

            }
            if (EditStr == "2")
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=2"));
            if (EditStr == "1")
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=1"));
        }
    }
}
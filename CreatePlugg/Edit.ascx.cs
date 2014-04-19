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
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using Plugghest.Pluggs;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Entities.Modules.Definitions;
using System.IO;
using Latex2MathML;
using Ionic.Zip;
using System.Collections.Generic;
using Plugghest.DNN;

namespace Plugghest.Modules.CreatePlugg
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from CreatePluggModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : CreatePluggModuleBase
    {
        //initialize ...
        //public int PluggId;
        //public string Title;
        //public string CreatedInCultureCode;
        //public int WhoCanEdit;
        //public DateTime CreatedOnDate;
        //public int CreatedByUserId;
        //public DateTime ModifiedOnDate;
        //public int ModifiedByUserId;
        //public int? Subject;
        //public string CultureCode;
        //public string YouTubeString;
        //public string HtmlText;
        //public string LatexText;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadCultureDropDownList();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        private void LoadCultureDropDownList()
        {
            try
            {
                Localization myLoc = new Localization();
                Localization.LoadCultureDropDownList(DDLanguage, CultureDropDownTypes.NativeName, myLoc.CurrentCulture);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnRemovePluggs_Click(object sender, EventArgs e)
        {
            PluggHandler ph = new PluggHandler();
            IEnumerable<Plugg> pluggrecord = ph.GetAllPluggs();

            foreach (Plugg p in pluggrecord)
            {
                string tabName = p.PluggId.ToString();

                TabController tabController = new TabController();
                TabInfo oldTab = tabController.GetTabByName(tabName, PortalId);
                if (oldTab != null)
                {
                    if (oldTab.Modules != null)
                    {
                        foreach (DotNetNuke.Entities.Modules.ModuleInfo mod in oldTab.Modules)
                        {
                            ModuleController moduleC = new ModuleController();
                            moduleC.DeleteModule(mod.ModuleID);
                            moduleC.DeleteModuleSettings(mod.ModuleID);
                        }
                    }

                    tabController.DeleteTab(oldTab.TabID, PortalId);
                    tabController.DeleteTabSettings(oldTab.TabID);
                    //DataCache.RemoveCache(oldTab.TabID);
                }
            }

            //Remove record from plugg and pluggscontent....
            ph.DeleteAllPluggContent();//Remove all records PluggsContent

            ph.DeleteAllPluggs();//Remove all records Pluggs

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }


        protected void CreatePages_Click(object sender, EventArgs e)
        {
            try
            {

                PluggHandler ph = new PluggHandler();
                IEnumerable<Plugg> pluggrecord = ph.GetAllPluggs();

                foreach (Plugg p in pluggrecord)
                {

                    string tabName = p.PluggId.ToString();

                    //Remove tab(page) if allready exist................
                    TabController tabController = new TabController();
                    TabInfo oldTab = tabController.GetTabByName(tabName, PortalId);
                    if (oldTab != null)
                    {
                        if (oldTab.Modules != null)
                        {
                            foreach (DotNetNuke.Entities.Modules.ModuleInfo mod in oldTab.Modules)
                            {
                                ModuleController moduleC = new ModuleController();
                                moduleC.DeleteModule(mod.ModuleID);
                                moduleC.DeleteModuleSettings(mod.ModuleID);
                            }
                        }

                        tabController.DeleteTab(oldTab.TabID, PortalId);
                        tabController.DeleteTabSettings(oldTab.TabID);
                        //DataCache.RemoveCache(oldTab.TabID);
                    }

                    //Add NEW PAGE(TAB).....
                    DNNHelper d = new DNNHelper();
                    d.AddPage(PortalId, p.PluggId.ToString());
                }

                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
            }

            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        #region Create Plugg Using TextFile and Zip.....

        protected void btnReadTextFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Upload_Textfile.HasFile)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(Upload_Textfile.FileContent);
                    //if pluggid exist on db then update otherwise create
                    PluggsUsingFile(file);
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please upload a text file.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void PluggsUsingFile(StreamReader file)
        {
            string line;
            Plugg p = new Plugg();
            PluggContent pc = new PluggContent();

            //string htmlText = "";
            string latexText = "";
            //string youTube = "";
            //string latexTextInHtml = "";

            while ((line = file.ReadLine()) != null)
            {
                latexText += line + System.Environment.NewLine;

                line = line.Replace("{", "");
                line = line.Replace("}", "");

                //Plugg
                if (line.Contains("pluggid"))
                {
                    string pluggIdStr;
                    string tobesearched = "pluggid";
                    pluggIdStr = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    if (!string.IsNullOrEmpty(pluggIdStr.Trim()))
                        p.PluggId = Convert.ToInt32(pluggIdStr);
                } 
                
                if (line.Contains("pluggtitle"))
                {
                    string tobesearched = "pluggtitle";
                    p.Title = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                }


                if (line.Contains("language"))
                {
                    string tobesearched = "language";
                    p.CreatedInCultureCode = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                }

                if (line.Contains("edit"))
                {
                    string tobesearched = "edit";
                    string WhoCanEdit_ = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    if (WhoCanEdit_ == "Me")
                        p.WhoCanEdit = 2;
                    else
                        p.WhoCanEdit = 1;
                }

                //Pluggcontent
                if (line.Contains("youtube"))
                {
                    string tobesearched = "youtube";
                    pc.YouTubeString = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                }

                if (line.Contains("html"))
                {
                    string tobesearched = "html";
                    pc.HtmlText = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                }

            }

            p.ModifiedOnDate = DateTime.Now; ;
            p.ModifiedByUserId = UserId;

            pc.LatexText = latexText;
            if (!string.IsNullOrEmpty(latexText.Trim()))
            {
                LatexToMathMLConverter myConverter = new LatexToMathMLConverter(latexText);
                myConverter.Convert();
                pc.LatexTextInHtml = myConverter.HTMLOutput;
            }

            if (p.PluggId > 0)
                UpdatePlugg(p,pc);
            else
                CreatePlugg(p,pc);
        }

        public void CreatePlugg(Plugg p, PluggContent pc)
        {
            p.CreatedOnDate = DateTime.Now;
            p.CreatedByUserId = UserId;

            PluggHandler ph = new PluggHandler();

            ph.CreatePlugg(p);
            pc.PluggId = p.PluggId;

            for (int i = 0; i < DDLanguage.Items.Count; i++)
            {
                pc.CultureCode = DDLanguage.Items[i].Value;
                CreatePluggContent(pc);
            }

            DNNHelper h = new DNNHelper();
            h.AddPage(PortalId,p.PluggId.ToString());

            lblError.Visible = true;
            lblError.Text = "Plugg has been Successfully created.";
        }

        public void UpdatePlugg(Plugg p, PluggContent pc)
        {
            PluggHandler ph = new PluggHandler();
            ph.UpdatePlugg(p);

            ph.UpdatePlugg(p);
            pc.PluggId = p.PluggId;

            for (int i = 0; i < DDLanguage.Items.Count; i++)
            {
                pc.CultureCode = DDLanguage.Items[i].Value;
                UpdatePluggContent(pc);
            }
            lblError.Visible = true;
            lblError.Text = "Plugg has been Successfully Updated.";
        }

        public void CreatePluggContent(PluggContent pc) 
        {
            //manage culture code
            string cul_code = pc.CultureCode;
            int index = cul_code.IndexOf("-");
            if (index > 0)
                cul_code = cul_code.Substring(0, index);

            string link = pc.YouTubeString;
            if (!string.IsNullOrEmpty(link))
            {

                if (link.Length == 11)
                {
                    link = "http://www.youtube.com/embed/" + link;
                }
                else
                {
                    //replace watch?v to embed
                    link = link.Replace("watch?v=", "embed/");
                }

                link = link + "?cc_load_policy=1&amp;cc_lang_pref=" + cul_code;
                //Add Iframe....
                //link = "<iframe width='640' height='390' src='" + link + "' frameborder='0'></iframe>";
                link = "<iframe width=" + 640 + " height=" + 390 + " src=" + link + " frameborder=" + 0 + "></iframe>";
            }

            pc.YouTubeString = link;

            PluggHandler ph = new PluggHandler();
            ph.CreatePluggContent(pc);
        }

        public void UpdatePluggContent(PluggContent pc) 
        {
            //manage culture code
            string cul_code = pc.CultureCode;
            int index = cul_code.IndexOf("-");
            if (index > 0)
                cul_code = cul_code.Substring(0, index);

            string link = pc.YouTubeString;
            if (!string.IsNullOrEmpty(link))
            {

                if (link.Length == 11)
                {
                    link = "http://www.youtube.com/embed/" + link;
                }
                else
                {
                    //replace watch?v to embed
                    link = link.Replace("watch?v=", "embed/");
                }

                link = link + "?cc_load_policy=1&amp;cc_lang_pref=" + cul_code;
                //Add Iframe....
                //link = "<iframe width='640' height='390' src='" + link + "' frameborder='0'></iframe>";
                link = "<iframe width=" + 640 + " height=" + 390 + " src=" + link + " frameborder=" + 0 + "></iframe>";
            }

            //pc.YouTubeString = link;

            //PluggHandler ph = new PluggHandler();
            //Boolean IsExist = plugc.CheckIsPlugExist(Convert.ToInt32(pluggcontent.PluggId));
            //if (IsExist)
            //{
            //    Update CreatePluggin...
            //    plugc.UpdatePluggContent(pluggcontent);
            //    lblError.Text = "";
            //}
            //else
            //{
            //    lblError.Text = "No such Plugg";
            //    return;
            //}

            //pluggcontent.YouTubeString = YouTubeString;//add again file youtube string to PluggController To remove iframe...
        }


        protected void btnReadZipFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Upload_Textfile.HasFile)
                {
                    Stream fs = Upload_Textfile.FileContent;

                    //For Read zip code : DotNetZip http://dotnetzip.codeplex.com/
                    using (ZipFile zip = ZipFile.Read(fs))
                    {
                        foreach (ZipEntry zipentry in zip)
                        {

                            using (var ms = new MemoryStream())
                            {
                                zipentry.Extract(ms);
                                // The StreamReader will read from the current position of the MemoryStream which is currently 
                                // set at the end of the string we just wrote to it. We need to set the position to 0 in order to read 
                                ms.Position = 0;// from the beginning.
                                StreamReader mystream = new StreamReader(ms);

                                //Create/Update Pluggs
                                PluggsUsingFile(mystream);
                            }
                        }

                        lblError.Visible = true;
                        lblError.Text = "Pluggs has been Successfully created.";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please upload a Zip file.";
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

    }
}
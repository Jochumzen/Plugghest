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
        public int PluggId;
        public string Title;
        public string CreatedInCultureCode;
        public int WhoCanEdit;
        public DateTime CreatedOnDate;
        public int CreatedByUserId;
        public DateTime ModifiedOnDate;
        public int ModifiedByUserId;
        public int? Subject;
        public string CultureCode;
        public string YouTubeString;
        public string HtmlText;
        public string LatexText;
        public string LatexTextInHtml;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadLanguage();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        private void LoadLanguage()
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
            //tabController.DeleteTab(136, PortalId);

            PluggController pc = new PluggController();
            var pluggrecord = pc.GetAllPluggs();

            if (pluggrecord != null)
            {
                foreach (var item in pluggrecord)
                {
                    string tabName = item.PluggId.ToString();

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
                pc.DeleteAllPluggsContent();//Remove all records PluggsContent

                pc.DeleteAllPluggRecord();//Remove all records Pluggs

            }

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }


        protected void CreatePages_Click(object sender, EventArgs e)
        {
            try
            {

                PluggController pc = new PluggController();
                var pluggrecord = pc.GetAllPluggs();

                if (pluggrecord != null)
                {
                    foreach (var item in pluggrecord)
                    {
                        string tabName = item.PluggId.ToString();

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
                        CreatePage(item.PluggId.ToString(), item.PluggId.ToString(), null, null, null);
                    }
                }

                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
            }

            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        #region create page and add module.....



        private void CreatePage(string PageName, string PageTitle, string Description, string Keywords, TabPermissionCollection Permissions, bool LoadDefaultModules = true)
        {
            TabController controller = new TabController();
            TabInfo newTab = new DotNetNuke.Entities.Tabs.TabInfo();
            TabPermissionCollection newPermissions = newTab.TabPermissions;
            PermissionProvider permissionProvider = new PermissionProvider();


            // set new page properties
            newTab.PortalID = this.PortalId;
            newTab.TabName = PageName;

            newTab.Title = PageTitle;

            newTab.Description = Description;
            newTab.KeyWords = Keywords;
            newTab.IsDeleted = false;
            newTab.IsSuperTab = false;
            newTab.IsVisible = false;//for menu...
            newTab.DisableLink = false;
            newTab.IconFile = "";
            newTab.Url = "";

            //newTab.ParentId = Convert.ToInt32(1);

            // create new page
            int tabId = controller.AddTab(newTab, LoadDefaultModules);

            //Set Tab Permission
            TabPermissionInfo tpi = new TabPermissionInfo();
            tpi.TabID = tabId;
            tpi.PermissionID = 3;//for view
            tpi.PermissionKey = "VIEW";
            tpi.PermissionName = "View Tab";
            tpi.AllowAccess = true;
            tpi.RoleID = 0; //Role ID of administrator         
            newPermissions.Add(tpi, true);
            permissionProvider.SaveTabPermissions(newTab);

            TabPermissionInfo tpi1 = new TabPermissionInfo();
            tpi1.TabID = tabId;
            tpi1.PermissionID = 4;//for edit
            tpi1.PermissionKey = "EDIT";
            tpi1.PermissionName = "Edit Tab";
            tpi1.AllowAccess = true;
            tpi1.RoleID = 0; //Role ID of administrator       
            newPermissions.Add(tpi, true);
            permissionProvider.SaveTabPermissions(newTab);


            //create module on page
            CreateModule(tabId);


            //Clear Cache
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(TabId);
            DotNetNuke.Common.Utilities.DataCache.ClearTabsCache(PortalId);
            DotNetNuke.Common.Utilities.DataCache.ClearPortalCache(PortalId, false);
            //////..................................
        }


        public void CreateModule(int tabId)
        {

            ModuleDefinitionInfo moduleDefinitionInfo = new ModuleDefinitionInfo();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.PortalID = this.PortalId;
            moduleInfo.TabID = tabId;
            moduleInfo.ModuleOrder = 1;
            moduleInfo.ModuleTitle = "";
            moduleInfo.PaneName = "";


            //Get ModuleDefinationId.............
            //PluggController pc = new PluggController();
            //int MDId = pc.GetModuleDefId("DisplayPlugg");
            //moduleInfo.ModuleDefID = MDId;

            DesktopModuleInfo desktopModuleInfo = null;
            foreach (KeyValuePair<int, DesktopModuleInfo> kvp in DesktopModuleController.GetDesktopModules(this.PortalId))
            {
                DesktopModuleInfo mod = kvp.Value;
                if (mod != null)
                    if (mod.FriendlyName == "DisplayPlugg")
                    {
                        desktopModuleInfo = mod;
                        var mc = new ModuleDefinitionController();
                        var mInfo = new ModuleDefinitionInfo();
                        mInfo = mc.GetModuleDefinitionByName(desktopModuleInfo.DesktopModuleID, desktopModuleInfo.FriendlyName);
                        //int moduleDefId = mInfo.ModuleDefID;
                        moduleInfo.ModuleDefID = mInfo.ModuleDefID;
                    }
            }
            ///////////////////////..............

            moduleInfo.CacheTime = moduleDefinitionInfo.DefaultCacheTime;//Default Cache Time is 0
            moduleInfo.InheritViewPermissions = true;//Inherit View Permissions from Tab
            moduleInfo.AllTabs = false;
            moduleInfo.Alignment = "Top";

            ModuleController moduleController = new ModuleController();
            int moduleId = moduleController.AddModule(moduleInfo);
        }

        #endregion


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
            //System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
            //System.IO.StreamReader file = new System.IO.StreamReader(Upload_Textfile.FileContent);

            string Language = "en-US";//Default
            int WhoCanEdit = 1;
            string youTube = "";
            string HtmlText = "";
            string Latextext = "";
            string LatexTextToHtml = "";
            string Plugg_Id = "";

            while ((line = file.ReadLine()) != null)
            {
                Latextext += line + System.Environment.NewLine;

                line = line.Replace("{", "");
                line = line.Replace("}", "");

                if (line.Contains("pluggid"))
                {
                    string tobesearched = "pluggid";
                    Plugg_Id = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    if (!string.IsNullOrEmpty(Plugg_Id.Trim()))
                        PluggId = Convert.ToInt32(Plugg_Id);
                }

                if (line.Contains("language"))
                {
                    string tobesearched = "language";
                    Language = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    CreatedInCultureCode = Language;
                    CultureCode = Language;
                }

                if (line.Contains("edit"))
                {
                    string tobesearched = "edit";
                    string WhoCanEdit_ = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    if (WhoCanEdit_ == "Me")
                        WhoCanEdit = 2;

                    WhoCanEdit = Convert.ToInt32(WhoCanEdit);
                }

                if (line.Contains("youtube"))
                {
                    string tobesearched = "youtube";
                    youTube = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    YouTubeString = youTube;
                }

                if (line.Contains("html"))
                {
                    string tobesearched = "html";
                    HtmlText = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                    HtmlText = HtmlText;
                }

                if (line.Contains("pluggtitle"))
                {
                    string tobesearched = "pluggtitle";
                    Title = line.Substring(line.IndexOf(tobesearched) + tobesearched.Length);
                }
            }
            if (!string.IsNullOrEmpty(Latextext.Trim()))
            {
                LatexToMathMLConverter myConverter = new LatexToMathMLConverter(Latextext);
                myConverter.Convert();
                LatexTextToHtml = myConverter.HTMLOutput;
            }

            LatexText = Latextext;
            LatexTextInHtml = LatexTextToHtml;

            Plugghest.Pluggs.Plugg plugg = new Plugg(PluggId, Title, CreatedInCultureCode, WhoCanEdit, CreatedOnDate, CreatedByUserId, ModifiedOnDate, ModifiedByUserId, Subject);
            PluggContent pluggcontent = new PluggContent(PluggId, CultureCode, YouTubeString, HtmlText, LatexText, LatexTextInHtml);
            CreateUpdatePluggs(plugg, pluggcontent);
        }



        public void CreateUpdatePluggs(Plugg plug, PluggContent pluggcontent)
        {
            plug.CreatedOnDate = DateTime.Now;
            plug.CreatedByUserId = 1;
            plug.ModifiedOnDate = DateTime.Now; ;
            plug.ModifiedByUserId = 1;
            PluggController PlugCtl = new PluggController();
            PluggHandler plughandler = new PluggHandler();

            if (plug.PluggId > 0)
            {
                PlugCtl.UpdatePlugg(plug);

                pluggcontent.PluggId = plug.PluggId;
                //To get all Language
                for (int i = 0; i < DDLanguage.Items.Count; i++)
                {
                    //Insert on PluggContent
                    pluggcontent.CultureCode = DDLanguage.Items[i].Value;
                    CreatePlugginContent(pluggcontent, true);
                }
                lblError.Visible = true;
                lblError.Text = "Plugg has been Successfully Updated.";

            }
            else
            {
                plughandler.AddNewPlugg(plug);

                pluggcontent.PluggId = plug.PluggId;
                //To get all Language
                for (int i = 0; i < DDLanguage.Items.Count; i++)
                {
                    //Insert on PluggContent
                    pluggcontent.CultureCode = DDLanguage.Items[i].Value;
                    CreatePlugginContent(pluggcontent);
                }

                //Add NEW PAGE(TAB).....
                CreatePage(plug.PluggId.ToString(), plug.PluggId.ToString(), null, null, null);

                lblError.Visible = true;
                lblError.Text = "Plugg has been Successfully created.";

                //Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" + plug.PluggId + ".aspx");
            }


        }


        public void CreatePlugginContent(PluggContent pluggcontent, Boolean isUpdate = false) //optional parameter to check for update
        {
            //manage culture code
            string cul_code = pluggcontent.CultureCode;
            int index = cul_code.IndexOf("-");
            if (index > 0)
                cul_code = cul_code.Substring(0, index);

            string YouTubeString = pluggcontent.YouTubeString;//youtubestring

            string link = pluggcontent.YouTubeString;
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

            pluggcontent.YouTubeString = link;

            PluggController plugc = new PluggController();
            PluggHandler plughandler = new PluggHandler();
            if (isUpdate)
            {
                Boolean IsExist = plugc.CheckIsPlugExist(Convert.ToInt32(pluggcontent.PluggId));
                if (IsExist)
                {
                    //Update CreatePluggin...
                    plugc.UpdatePluggContent(pluggcontent);
                    lblError.Text = "";
                }
                else
                {
                    lblError.Text = "No such Plugg";
                    HideControl();
                    return;
                }
            }
            else //Insert.........
            {
                plughandler.AddNewPluggContent(pluggcontent);//create puggin content
            }

            pluggcontent.YouTubeString = YouTubeString;//add again file youtube string to PluggController To remove iframe...
        }


        public void HideControl()
        {
            CreatePages.Visible = false;
            btnRemovePluggs.Visible = false;
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
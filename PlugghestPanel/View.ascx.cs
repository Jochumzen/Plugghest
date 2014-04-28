﻿/*
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
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;
using Plugghest.Modules.PlugghestPanel.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base;
using Plugghest.DNN;
using System.IO;
using Ionic.Zip;

namespace Plugghest.Modules.PlugghestPanel
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from PlugghestPanelModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : PlugghestPanelModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
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

        protected void btnDeleteCourse_Click(object sender, EventArgs e)
        {

            
        }

        protected void btnDeleteTab_Click(object sender, EventArgs e)
        {
            DNNHelper h =  new DNNHelper();
            TabInfo t;
            string s = tbDeleteTabID.Text;


            if (s.IndexOf(',') > -1)
            {
                string[] tabIDs = s.Split(',');

                for (int i = 0; i < tabIDs.Length; i++)
                {
                    t = new TabInfo();
                    t.TabID = Convert.ToInt32(tabIDs[i]);
                    h.DeleteTab(t);
                }                
            }
            else
            {
                int posOfDash = s.IndexOf('-');
                if (posOfDash > -1)
                {
                    string starts = s.Substring(0, posOfDash);
                    string ends = s.Substring(posOfDash+1,s.Length-posOfDash-1);
                    int startint = Convert.ToInt32(starts);
                    int endint = Convert.ToInt32(ends);
                    for (int tID = startint; tID <= endint; tID++)
                    {
                        t = new TabInfo();
                        t.TabID = tID;
                        h.DeleteTab(t);
                    }
                }
                else
                {
                    t = new TabInfo();
                    t.TabID = Convert.ToInt32(s);
                    h.DeleteTab(t);
                }
            }
            tbDeleteTabID.Text = "";
        }

        protected void btnDeleteAllPluggs_Click(object sender, EventArgs e)
        {
            BaseHandler bh = new BaseHandler();
            bh.DeleteAllPluggs();
        }

        protected void lbReadLatexFile_Click(object sender, EventArgs e)
        {
            if (fuLatexFile.HasFile)
            {
                StreamReader file = new StreamReader(fuLatexFile.FileContent);
                ReadSingleFile(file);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Please upload a text file.";
            }
        }

        protected void lbReadZip_Click(object sender, EventArgs e)
        {
            if (fuLatexFile.HasFile)
            {
                Stream fs = fuLatexFile.FileContent;

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
                            ReadSingleFile(mystream);
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

        protected void ReadSingleFile(StreamReader file)
        {
            string latex = file.ReadToEnd();
            string pluggIdStr = GetCommand(latex, "pluggid");

            BaseHandler bh = new BaseHandler();
            if (pluggIdStr != "")
            {
                int pluggId;
                bool isNum = int.TryParse(pluggIdStr, out pluggId);
                if (!isNum)
                {
                    lblError.Text = "PluggId is not an integer";
                    return;
                }
                Plugg p = bh.GetPlugg(pluggId);
                if (p == null)
                {
                    lblError.Text = "File has a PluggId which does not exist";
                    return;
                }
                //Todo: Handle Update of Latex file. 
            }
            else
            {
                Plugg p = new Plugg();
                PluggContent pc = new PluggContent();

                string title = GetCommand(latex, "pluggtitle");
                string section = GetCommand(latex, "section");
                if (title != "")
                    p.Title = title;
                else if (section != "")
                    p.Title = section;
                else
                    p.Title = "Untitled";

                string CultureCode = GetCommand(latex, "culture");
                if (CultureCode != "")
                    p.CreatedInCultureCode = CultureCode;
                else
                    p.CreatedInCultureCode = "en-US";

                string whoCanEdit = GetCommand(latex, "edit");
                if (whoCanEdit == "me")
                    p.WhoCanEdit = EWhoCanEdit.OnlyMe;
                else if (whoCanEdit == "anyone")
                    p.WhoCanEdit = EWhoCanEdit.Anyone;
                else
                    p.WhoCanEdit = EWhoCanEdit.Anyone;

                string youTubeCode = GetCommand(latex, "youtube");
                if (youTubeCode != "")
                    p.YouTubeCode = youTubeCode;

                string html = GetCommand(latex, "html");
                if (html != "")
                    pc.HtmlText = html;

                pc.LatexText = latex;

                p.CreatedByUserId = UserId;
                p.CreatedOnDate = DateTime.Now;
                p.ModifiedByUserId = UserId;
                p.ModifiedOnDate = DateTime.Now;

                p.SubjectId = 0;

                bh.CreatePlugg(p, pc);
            }            
        }

        protected string GetCommand(string latex, string cmd)
        {
            int cmdPos = latex.IndexOf("\\" + cmd);
            if (cmdPos < 0)
                return "";
            int pos1 = latex.IndexOf('{', cmdPos);
            int pos2 = latex.IndexOf('}', cmdPos);
            string s = latex.Substring(pos1 + 1, pos2 - pos1 - 1);
            return s;
        }
    }
}
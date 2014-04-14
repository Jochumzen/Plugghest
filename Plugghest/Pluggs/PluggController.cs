/*
' Copyright (c) 2014 Plugghest.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using System.Collections.Generic;
using DotNetNuke.Data;
using System.Data;
using System;

namespace Plugghest.Pluggs
{
    public class PluggController
    {
        public Plugg CreatePlug(Plugg t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Plugg>();
                rep.Insert(t);
            }
            return t;
        }

        public Plugg GetPlugg(int? plugid)
        {
            Plugg p;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Plugg>();
                p = rep.GetById(plugid);
            }
            return p;
        }

        public void CreatePlugginContent(PluggContent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                rep.Insert(t);
            }
        }

        public int GetModuleDefId(string FriendlyName)
        {
            List<ModuleDef> plug = new List<ModuleDef>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<ModuleDef>(CommandType.TableDirect, "select ModuleDefId from ModuleDefinitions where FriendlyName='" + FriendlyName + "'");
                foreach (var item in rec)
                {
                    plug.Add(new ModuleDef { ModuleDefID = item.ModuleDefID });
                }
            }
            return plug[0].ModuleDefID;
        }

        public List<Plugg> GetAllPlugg_PageName()
        {
            List<Plugg> plug = new List<Plugg>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Plugg>(CommandType.TableDirect, "select PluggId from Pluggs");
                foreach (var item in rec)
                {
                    plug.Add(new Plugg { PluggId = item.PluggId });
                }
            }
            return plug;
        }


        public void DeleteAllPluggsContent()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.ExecuteQuery<Plugg>(CommandType.TableDirect, "truncate table PluggsContent");
            }
        }

        public void DeleteAllPluggRecord()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.ExecuteQuery<Plugg>(CommandType.TableDirect, "DELETE FROM Pluggs DBCC CHECKIDENT ('Pluggs',RESEED, 0)");
                //use DBCC CHECKIDENT  for start with 0 ............
            }
        }


        public Boolean CheckIsPlugExist(int PID)
        {
            IsExist objisexist = new IsExist();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<IsExist>(CommandType.TableDirect, "select count(PluggId) as isexist from Pluggs where pluggid=" + PID);
                foreach (var item in rec)
                {
                    objisexist.isexist = item.isexist;
                }
            }
            return objisexist.isexist;
        }

        public class IsExist
        {
            public Boolean isexist { get; set; }
        }


        public Plugg GetPlug(int PluggId)
        {
            Plugg plugg = new Plugg();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Plugg>(CommandType.TableDirect, "select * from Pluggs where pluggid=" + PluggId);
                foreach (var item in rec)
                {
                    plugg.Title = item.Title; plugg.CreatedByUserId = item.CreatedByUserId; plugg.CreatedInCultureCode = item.CreatedInCultureCode; plugg.CreatedOnDate = item.CreatedOnDate; plugg.ModifiedOnDate = item.ModifiedOnDate; plugg.WhoCanEdit=item.WhoCanEdit ;
                }
            }
            return plugg;
        }

        public PluggContent GetPlugContent(int PluggId, string CultureCode)
        {
            PluggContent pluggcontent = new PluggContent();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, "select * from PluggsContent where pluggid=" + PluggId+" and culturecode='"+CultureCode+"' ");
                foreach (var item in rec)
                {
                    pluggcontent.CultureCode = item.CultureCode; pluggcontent.HtmlText = item.HtmlText; pluggcontent.LatexText = item.LatexText; pluggcontent.YouTubeString = item.YouTubeString;
                }
            }
            return pluggcontent;
        }


        public void UpdatePlugg(Plugg plug)
        {
            using (IDataContext db = DataContext.Instance())
            {
                var rep = db.GetRepository<Plugg>();
                rep.Update(plug);
            }
        }

        public void UpdatePluggContent(PluggContent plugContent)
        {
            using (IDataContext db = DataContext.Instance())
            {
                db.Execute(CommandType.Text, "update pluggscontent set YoutubeString='" + plugContent.YouTubeString + "', Htmltext='" + plugContent.HtmlText + "',LatexText='" + plugContent.LatexText + "',LatexTextInHtml='" + plugContent.LatexTextInHtml + "' where pluggid="+plugContent.PluggId +" and Culturecode='"+plugContent.CultureCode+"' ");
            }
        }

        public List<PluggInView> GetPluggRecords()
        {
            List<PluggInView> plug = new List<PluggInView>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggInView>(CommandType.TableDirect, @"select PluggId,Title as 'PluggName',pluggs.CreatedByUserId,Username from pluggs join Users on users.UserID=Pluggs.CreatedByUserId ");

                foreach (var item in rec)
                {
                    plug.Add(new PluggInView { PluggName = item.PluggName, PluggId = item.PluggId, CreatedByUserId = item.CreatedByUserId, UserName = item.UserName });
                }
            }

            return plug;
        }

        public List<PluggContentInDisplayPlugg> GetPluggincontents(int PluggId)
        {
            List<PluggContentInDisplayPlugg> plug = new List<PluggContentInDisplayPlugg>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggContentInDisplayPlugg>(CommandType.TableDirect, @"select Title,CultureCode,YouTubeString,HtmlText,LatexText,LatexTextInHtml from Pluggs inner join PluggsContent
                                                                                      on pluggs.PluggId=PluggsContent.PluggId  where Pluggs.PluggId=" + PluggId);

                foreach (var item in rec)
                {
                    plug.Add(new PluggContentInDisplayPlugg { Title = item.Title, CultureCode = item.CultureCode, YouTubeString = item.YouTubeString, HtmlText = item.HtmlText, LatexText = item.LatexText, LatexTextInHtml = item.LatexTextInHtml });
                }
            }

            return plug;
        }

    }
}
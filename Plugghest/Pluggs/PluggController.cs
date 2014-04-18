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
using Plugghest.Courses;

namespace Plugghest.Pluggs
{
    public class PluggController
    {

        //Plugg

        public void CreatePlugg(Plugg t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Plugg>();
                rep.Insert(t);
            }
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

        public void UpdatePlugg(Plugg plug)
        {
            using (IDataContext db = DataContext.Instance())
            {
                var rep = db.GetRepository<Plugg>();
                rep.Update(plug);
            }
        }

        public IEnumerable<Plugg> GetAllPluggs()
        {
            IEnumerable<Plugg> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Plugg>();
                t = rep.Get();
            }
            return t;
        }

        public IEnumerable<Plugg> GetPluggsInCourse(int courseId)
        {
            IEnumerable<Plugg> t = null;
            //Todo: 
            return t;
        }

        public void DeleteAllPluggs()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.ExecuteQuery<Plugg>(CommandType.TableDirect, "DELETE FROM Pluggs DBCC CHECKIDENT ('Pluggs',RESEED, 0)");
                //use DBCC CHECKIDENT  for start with 0 ............
            }
        }

        //PluggContent

        public void CreatePluggContent(PluggContent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                rep.Insert(t);
            }
        }

        public PluggContent GetPluggContent(int pluggId, string cultureCode)
        {
            PluggContent pluggcontent = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, "select * from PluggsContent where pluggid=" + pluggId + " and culturecode='" + cultureCode + "' ");
                foreach (var item in rec)
                {
                    pluggcontent = new PluggContent(item.PluggId, item.CultureCode, item.YouTubeString, item.HtmlText, item.LatexText, item.LatexTextInHtml);
                }
            }
            return pluggcontent;
        }

        public List<PluggContent> GetPluggContent(int pluggId)
        {
            List<PluggContent> pc = new List<PluggContent>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, @"select Title,CultureCode,YouTubeString,HtmlText,LatexText,LatexTextInHtml from Pluggs inner join PluggsContent
                                                                                      on pluggs.PluggId=PluggsContent.PluggId  where Pluggs.PluggId=" + pluggId);
                foreach (var item in rec)
                {
                    pc.Add(new PluggContent(item.PluggId, item.CultureCode, item.YouTubeString, item.HtmlText, item.LatexText, item.LatexTextInHtml));
                }
                //remove .... Title = item.Title,
            }
            return pc;
        }

        public void UpdatePluggContent(PluggContent plugContent)
        {
            using (IDataContext db = DataContext.Instance())
            {
                db.Execute(CommandType.Text, "update pluggscontent set YoutubeString='" + plugContent.YouTubeString + "', Htmltext='" + plugContent.HtmlText + "',LatexText='" + plugContent.LatexText + "',LatexTextInHtml='" + plugContent.LatexTextInHtml + "' where pluggid=" + plugContent.PluggId + " and Culturecode='" + plugContent.CultureCode + "' ");
            }
        }

        public void DeleteAllPluggContent()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, "truncate table PluggsContent");
            }
        }

        //PluggForDNN

        public List<PluggInfoForDNNGrid> GetPluggRecords()
        {
            List<PluggInfoForDNNGrid> plug = new List<PluggInfoForDNNGrid>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggInfoForDNNGrid>(CommandType.TableDirect, @"select PluggId, Title as PluggName, Username from pluggs join Users on users.UserID=Pluggs.CreatedByUserId ");

                foreach (var item in rec)
                {
                    plug.Add(new PluggInfoForDNNGrid{PluggId= item.PluggId,PluggName= item.PluggName,UserName= item.UserName});
                }
            }

            return plug;
        }

    }
}
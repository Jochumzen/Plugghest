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

        public Boolean CreatePluggContent(PluggContent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                rep.Insert(t);
                return true;
            }
        }

        public string GetPlugTitle(int PluggId)
        {
            string plugtitle = "";
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select Title from Pluggs where PluggId=" + PluggId);
                foreach (var item in rec)
                {
                    plug.Add(new Course { Title = item.Title });
                }
                if (plug.Count > 0)
                {
                    plugtitle = plug[0].Title; ;
                }
            }
            return plugtitle;
        }


        public List<Course> GetPluggsByCourseID(int CourseID)
        {
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,pluggs.Title as 'PluggName',Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new Course { CourseId = item.CourseId, PluggId = item.PluggId });
                }
            }
            return plug;
        }


        //P.J. GetAllPlugg_PageName is a very strange name for what method does
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
            Boolean isexist;
            using (IDataContext ctx = DataContext.Instance())
            {
                isexist = ctx.ExecuteScalar<Boolean>(CommandType.Text, "select count(PluggId) as isexist from Pluggs where pluggid=" + PID);
            }
            return isexist;
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


        public List<Plugg> GetPluggRecords()
        {
            List<Plugg> plug = new List<Plugg>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Plugg>(CommandType.TableDirect, @"select PluggId, Title ,pluggs.CreatedByUserId,Username from pluggs join Users on users.UserID=Pluggs.CreatedByUserId ");

                foreach (var item in rec)
                {
                    plug.Add(new Plugg { Title = item.Title, PluggId = item.PluggId, CreatedByUserId = item.CreatedByUserId});
                }
            }

            return plug;
        }


        public List<Course> GetPluggsByCourseIDForMenu(int CourseID)
        {
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,pluggs.Title as 'PluggName',Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new Course { CourseId = item.CourseId, PluggId = item.PluggId, PluggName = item.PluggName, Orders = item.Orders });
                }
            }
            return plug;
        }


        public List<PluggContent> GetPluggincontents(int PluggId)
        {
            List<PluggContent> plug = new List<PluggContent>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, @"select Title,CultureCode,YouTubeString,HtmlText,LatexText,LatexTextInHtml from Pluggs inner join PluggsContent
                                                                                      on pluggs.PluggId=PluggsContent.PluggId  where Pluggs.PluggId=" + PluggId);
                foreach (var item in rec)
                {
                    plug.Add(new PluggContent {CultureCode = item.CultureCode, YouTubeString = item.YouTubeString, HtmlText = item.HtmlText, LatexText = item.LatexText, LatexTextInHtml = item.LatexTextInHtml });
                }
                //remove .... Title = item.Title,
            }
            return plug;
        }

    }
}
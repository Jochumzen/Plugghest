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
        //public Plugg CreatePlug(Plugg t)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<Plugg>();
        //        rep.Insert(t);
        //    }
        //    return t;
        //}

        public int CreatePlug(Plugg p)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                int PID = ctx.ExecuteScalar<int>(CommandType.Text, "insert into pluggs values('" + p.Title + "','" + p.CreatedInCultureCode + "','" + p.WhoCanEdit + "','" + p.CreatedOnDate + "','" + p.CreatedByUserId + "','" + p.ModifiedOnDate + "','" + p.ModifiedByUserId + "','" + p.Subject + "') SELECT SCOPE_IDENTITY() as PID");
                return PID;
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

        public Boolean CreatePluggContent(PluggContent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                rep.Insert(t);
                return true;
            }
        }

   
        public List<CoursePlugg> CoursePluggs(int CourseID)
        {
            List<CoursePlugg> plug = new List<CoursePlugg>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<CoursePlugg>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new CoursePlugg { CourseId = item.CourseId, PluggId = item.PluggId,Orders=item.Orders });
                }
            }
            return plug;
        }



        //This method will get all the Pluggs
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
            PluggContent pluggcontent=null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, "select * from PluggsContent where pluggid=" + PluggId+" and culturecode='"+CultureCode+"' ");
                foreach (var item in rec)
                {
                    pluggcontent = new PluggContent(item.PluggId, item.CultureCode, item.YouTubeString, item.HtmlText, item.LatexText, item.LatexTextInHtml);
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



        public List<PluggInfoForDNNGrid> GetPluggRecords()
        {
            List<PluggInfoForDNNGrid> plug = new List<PluggInfoForDNNGrid>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggInfoForDNNGrid>(CommandType.TableDirect, @"select PluggId, Title as PluggName, username from pluggs join Users on users.UserID=Pluggs.CreatedByUserId ");

                foreach (var item in rec)
                {
                    plug.Add(new PluggInfoForDNNGrid{PluggId= item.PluggId,PluggName= item.PluggName,UserName= item.UserName});
                }
            }

            return plug;
        }


        public List<CoursePlugg> GetCoursePlugg(int CourseID)
        {
            List<CoursePlugg> plug = new List<CoursePlugg>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<CoursePlugg>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,pluggs.Title,Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new CoursePlugg { CourseId = item.CourseId, PluggId = item.PluggId, Orders = item.Orders });
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
                    plug.Add(new PluggContent (item.PluggId,item.CultureCode, item.YouTubeString, item.HtmlText, item.LatexText,item.LatexTextInHtml));
                }
                //remove .... Title = item.Title,
            }
            return plug;
        }

    }
}
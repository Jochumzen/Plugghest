using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plugghest.Base
{
    class BaseRepository
    {
        #region Plugg

        public void CreatePlugg(Plugg t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Plugg>();
                rep.Insert(t);
            }
        }

        public Plugg GetPlugg(int pluggId)
        {
            Plugg p;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Plugg>();
                p = rep.GetById(pluggId);
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

        public void DeletePlugg(Plugg plug)
        {
            using (IDataContext db = DataContext.Instance())
            {
                var rep = db.GetRepository<Plugg>();
                rep.Delete(plug);
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
            //Todo: Complete GetPluggsInCourse
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

        #endregion

        #region PluggContent

        public void CreatePluggContent(PluggContent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                rep.Insert(t);
            }
        }

        public PluggContent GetPluggContent(int pluggContentId)
        {
            PluggContent pc;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                pc = rep.GetById(pluggContentId);
            }
            return pc;
        }

        public PluggContent GetPluggContent(int pluggId, string cultureCode)
        {
            IEnumerable<PluggContent> pcs;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<PluggContent>();
                pcs = rep.Find("Where PluggId = @0 AND CultureCode = @1", pluggId, cultureCode);
            }
            return pcs.First();  //There can be only one. PetaPoco does not handle composite key
        }

        public void UpdatePluggContent(PluggContent pc)
        {
            using (IDataContext db = DataContext.Instance())
            {
                var rep = db.GetRepository<PluggContent>();
                rep.Update(pc);
            }
        }

        public void DeletePluggContent(PluggContent pc)
        {
            using (IDataContext db = DataContext.Instance())
            {
                var rep = db.GetRepository<PluggContent>();
                rep.Delete(pc);
            }
        }

        public IEnumerable<PluggContent> GetAllContentInPlugg(int pluggId)
        {
            IEnumerable<PluggContent> pc = new List<PluggContent>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var repository = ctx.GetRepository<PluggContent>();
                pc = repository.Find("WHERE PluggId = @0", pluggId);
            }
            return pc;
        }

        public void DeleteAllPluggContent()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.ExecuteQuery<PluggContent>(CommandType.TableDirect, "truncate table PluggsContent");
            }
        }

        #endregion

        #region Course

        public void CreateCourse(Course t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                rep.Insert(t);
            }
        }

        public void DeleteCourse(Course t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                rep.Delete(t);
            }
        }

        public void UpdateCourse(Course t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                rep.Update(t);
            }
        }

        public Course GetCourse(int? courseId)
        {
            Course p;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                p = rep.GetById(courseId);
            }
            return p;
        }

        #endregion

        #region CourseItem

        public void CreateCourseItem(CourseItem t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseItem>();
                rep.Insert(t);
            }
        }

        public void UpdateCourseItem(CourseItem t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseItem>();
                rep.Update(t);
            }
        }
        public void DeleteCourseItem(CourseItem t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseItem>();
                rep.Delete(t);
            }
        }

        public IEnumerable<CourseItem> GetCourseItemsForCourse(int CourseID)
        {
            IEnumerable<CourseItem> cps;
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<CourseItem>();
                cps = repository.Find("WHERE CourseID = @0 ORDER BY CIOrder", CourseID);
            }
            return cps;
        }

        //The same item may go into a course several times so collection is correct
        public IEnumerable<CourseItem> GetCourseItems(int courseId, int itemId)
        {
            IEnumerable<CourseItem> cis;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseItem>();
                cis = rep.Find("WHERE CourseId = @0 AND ItemId = @1", courseId, itemId);
            }
            return cis;
        }



        #endregion

        #region Other

        public List<PluggInfoForDNNGrid> GetPluggRecords()
        {
            List<PluggInfoForDNNGrid> plug = new List<PluggInfoForDNNGrid>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<PluggInfoForDNNGrid>(CommandType.TableDirect, @"select PluggId, Title as PluggName, Username from pluggs join Users on users.UserID=Pluggs.CreatedByUserId ");

                foreach (var item in rec)
                {
                    plug.Add(new PluggInfoForDNNGrid { PluggId = item.PluggId, PluggName = item.PluggName, UserName = item.UserName });
                }
            }

            return plug;
        }

        //CourseForDNN

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            List<CourseInfoForDNNGrid> cs = new List<CourseInfoForDNNGrid>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<CourseInfoForDNNGrid>(CommandType.TableDirect, @"select CourseId, Title as CourseName, Username from courses join Users on Users.UserID=Courses.CreatedByUserId ");

                foreach (var item in rec)
                {
                    cs.Add(new CourseInfoForDNNGrid() { CourseId = item.CourseId, CourseName = item.CourseName, UserName = item.UserName });
                }
            }

            return cs;
        }

        public List<CourseTree> GetCourseItems(int CourseID)
        {
            List<CourseTree> objsubjectitem = new List<CourseTree>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<CourseTree>(CommandType.TableDirect, @"select Pluggs.Title,Mother,ItemType,CourseItemID, CIOrder as [Order] ,ItemID  from CourseItems join Pluggs on Pluggs.PluggId=CourseItems.Itemid  where CourseID=" + CourseID + " and ItemType = 0 union select CourseHeadings.Title,Mother,ItemType,CourseItemID,CIOrder as [Order],ItemID  from CourseItems join CourseHeadings on CourseHeadings.HeadingID = CourseItems.ItemID where CourseID=" + CourseID + " and ItemType = 1 order by CIOrder");

                foreach (var val in rec)
                {
                    objsubjectitem.Add(new CourseTree { label = val.Title, Title = val.Title, Mother = val.Mother, Order = val.Order, CourseItemID = val.CourseItemID, ItemType = val.ItemType, ItemID = val.ItemID });
                }
            }
            return objsubjectitem;
        }

        #endregion




        #region CourseHeading
        public CourseHeadings CreateHeading(CourseHeadings t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseHeadings>();
                rep.Insert(t);
            }
            return t;
        }


        public void UpdateHeading(CourseHeadings t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseHeadings>();
                rep.Update(t);
            }
        }
        #endregion
    }
}

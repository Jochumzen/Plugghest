using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.Localization;
using Latex2MathML;
using Plugghest.DNN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Base
{
    public class BaseHandler
    {
        private int PortalID = 0;  //Global. As for now - only one Portal: 0

        BaseRepository rep = new BaseRepository();

        #region Plugg

        public Plugg GetPlugg(int pluggId)
        {
            return rep.GetPlugg(pluggId);
        }

        public IEnumerable<Plugg> GetAllPluggs()
        {
            return rep.GetAllPluggs();
        }

        //Create a Plugg, identical PluggContent in all languages and PluggPage
        public void CreatePlugg(Plugg p, PluggContent pc)
        {
            rep.CreatePlugg(p);

            //Create identical PluggContent in all locales from the single pc (translation later)
            try
            {
                pc.PluggId = p.PluggId;
                if (pc.LatexText != null)
                {
                    LatexToMathMLConverter myConverter = new LatexToMathMLConverter(pc.LatexText);
                    myConverter.Convert();
                    pc.LatexTextInHtml = myConverter.HTMLOutput;
                }

                LocaleController lc = new LocaleController();
                var locales = lc.GetLocales(PortalID);
                foreach (var locale in locales)
                {
                    pc.CultureCode = locale.Key;
                    rep.CreatePluggContent(pc);
                }
            }
            catch (Exception)
            {
                DeletePlugg(p);
                throw;
            }

            //Create PluggPage
            DNNHelper d = new DNNHelper();
            string pageUrl = p.PluggId.ToString();
            string pageName = pageUrl + ": " + p.Title;
            try
            {
                TabInfo newTab = d.AddPluggPage(pageName, pageUrl);
                p.TabId = newTab.TabID;
                rep.UpdatePlugg(p);
            }
            catch (Exception)
            {
                DeletePlugg(p);
                throw;
            }
        }

        public void DeletePlugg(Plugg p)
        {
            // Todo: Don't delete Plugg if: It has comments or ratings, Its included in a course.
            // Todo: Soft delete of Plugg
            if (p == null)
            {
                throw new Exception("Cannot delete: Plugg not initialized");
                return;
            }

            TabController tabController = new TabController();
            TabInfo getTab = tabController.GetTab(p.TabId);

            if (getTab != null)
            {
                DNNHelper h = new DNNHelper();
                h.DeleteTab(getTab);
            }

            var pcs = GetAllContentInPlugg(p.PluggId);
            foreach (PluggContent pcDelete in pcs)
            {
                rep.DeletePluggContent(pcDelete);
            }

            rep.DeletePlugg(p);
        }

        public void UpdatePlugg(Plugg p, PluggContent pc)
        {
            //For restore if something goes wrong
            Plugg oldP = GetPlugg(p.PluggId);
            IEnumerable<PluggContent> oldPCs = GetAllContentInPlugg(p.PluggId);

            rep.UpdatePlugg(p); //No repair necessary if this fails

            //For now, remove all PluggContent and recreate in all languages from pc. Fix this when we can deal with translations
            try
            {
                foreach (PluggContent pcDelete in oldPCs)
                {
                    rep.DeletePluggContent(pcDelete);
                }

                pc.PluggId = p.PluggId;
                if (pc.LatexText != null)
                {
                    LatexToMathMLConverter myConverter = new LatexToMathMLConverter(pc.LatexText);
                    myConverter.Convert();
                    pc.LatexTextInHtml = myConverter.HTMLOutput;
                }

                LocaleController lc = new LocaleController();
                var locales = lc.GetLocales(PortalID);
                foreach (var locale in locales)
                {
                    pc.CultureCode = locale.Key;
                    rep.CreatePluggContent(pc);
                }
            }
            catch (Exception)
            {
                //recreate old Plugg/PluggContent before rethrow
                var pcs = GetAllContentInPlugg(p.PluggId);
                foreach (PluggContent pcDelete in pcs)
                {
                    rep.DeletePluggContent(pcDelete);
                }
                rep.DeletePlugg(p);

                rep.CreatePlugg(oldP);
                foreach (PluggContent oldPC in oldPCs)
                    rep.CreatePluggContent(oldPC);
                throw;
            }

        }

        public void DeleteAllPluggs()
        {
            //Todo: Business logic for DeleteAllPluggs
            var pluggs = rep.GetAllPluggs();
            foreach (Plugg p in pluggs)
                DeletePlugg(p);
        }

        public IEnumerable<Plugg> GetPluggsInCourse(int courseId)
        {
            return rep.GetPluggsInCourse(courseId);
        }

        #endregion

        #region PluggContent

        public void CreatePluggContent(PluggContent plugcontent)
        {
            rep.CreatePluggContent(plugcontent);
        }

        public PluggContent GetPluggContent(int pluggId, string cultureCode)
        {
            return rep.GetPluggContent(pluggId, cultureCode);
        }

        public IEnumerable<PluggContent> GetAllContentInPlugg(int pluggId)
        {
            return rep.GetAllContentInPlugg(pluggId);
        }

        public void UpdatePluggContent(PluggContent pc)
        {
            rep.UpdatePluggContent(pc);
        }

        public void DeleteAllPluggContent()
        {
            rep.DeleteAllPluggContent();
        }

        #endregion

        #region Course

        public void CreateCourse(Course c, List<CourseItem> cis)
        {
            rep.CreateCourse(c);

            try
            {
                foreach (CourseItem ci in cis)
                {
                    ci.CourseID = c.CourseId;
                    rep.CreateCourseItem(ci);
                }
            }
            catch (Exception)
            {
                DeleteCourse(c);
                throw;
            }

            //Create CoursePage
            DNNHelper d = new DNNHelper();
            string pageUrl = "C" + c.CourseId.ToString();
            string pageName = pageUrl + ": " + c.Title;
            try
            {
                TabInfo newTab = d.AddCoursePage(pageName, pageUrl);
                c.TabId = newTab.TabID;
                rep.UpdateCourse(c);
            }
            catch (Exception)
            {
                DeleteCourse(c);
                throw;
            }
        }

        public Course GetCourse(int CourseID)
        {
            return rep.GetCourse(CourseID);
        }

        public void DeleteCourse(Course c)
        {
            // Todo: Don't delete course if: It has comments or ratings
            // Todo: Soft delete of Course
            if (c == null)
            {
                throw new Exception("Cannot delete: Course not initialized");
                return;
            }

            TabController tabController = new TabController();
            TabInfo getTab = tabController.GetTab(c.TabId);

            if (getTab != null)
            {
                DNNHelper h = new DNNHelper();
                h.DeleteTab(getTab);
            }

            var cis = rep.GetCourseItemsForCourse(c.CourseId);
            foreach (CourseItem ciDelete in cis)
            {
                rep.DeleteCourseItem(ciDelete);
            }

            rep.DeleteCourse(c);
        }

        #endregion

        #region CourseItem

        public void CreateCoursePlugg(CourseItem cp)
        {
            rep.CreateCourseItem(cp);
        }

        public IEnumerable<CourseItem> GetCourseItems(int CourseID, int ItemID)
        {
            return rep.GetCourseItems(CourseID, ItemID);
        }

        public IEnumerable<CourseItem> GetCourseItemsForCourse(int CourseID)
        {
            return rep.GetCourseItemsForCourse(CourseID);
        }

        public void CreateCourseItem(CourseItem ci)
        {
            rep.CreateCourseItem(ci);
        }

        public void UpdateCourseItem(CourseItem ci)
        {
            rep.UpdateCourseItem(ci);
        }

        public void DeleteCourseItem(CourseItem ci)
        {
            rep.DeleteCourseItem(ci);
        }
        #endregion

        #region Other

        public List<PluggInfoForDNNGrid> GetPluggListForGrid()
        {
            return rep.GetPluggRecords();
        }

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            return rep.GetCoursesForDNN();
        }

        public List<CourseItem> GetCourseItemsForTree(int CourseID)
        {
            return rep.GetCourseItemsForTree(CourseID);
        }

        #endregion
        #region CourseHeading
        public CourseHeadings CreateHeading(CourseHeadings h)
        {
            return rep.CreateHeading(h);
        }

        public void UpdateHeading(CourseHeadings h)
        {
            rep.UpdateHeading(h);
        }

        #endregion
    }
}

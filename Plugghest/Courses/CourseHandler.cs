using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Courses
{
    //Work as a Business Layer.......................
    public class CourseHandler
    {
        CourseController cc = new CourseController();

        public void CreateCourse(Course c)
        {
            cc.CreateCourse(c);
        }

        public Course GetCourse(int CourseID)
        {
            return cc.GetCourse(CourseID);
        }

        public void DeleteCourse(Course c)
        {
            cc.DeleteCourse(c);
        }

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            return cc.GetCoursesForDNN();
        }

        //CoursePluggs

        public void CreateCoursePlugg(CourseItem cp)
        {
            cc.CreateCourseItem(cp);
        }

        public IEnumerable<CourseItem> GetCoursePlugg(int CourseID, int ItemID)
        {
            return cc.GetCoursePlugg(CourseID, ItemID);
        }

        public IEnumerable<CourseItem> GetCoursePluggsForCourse(int CourseID)
        {
            return cc.GetCoursePluggsForCourse(CourseID);
        }

        public List<Course_Tree> GetCourseItems(int CourseID)
        {
            return cc.GetCourseItems(CourseID);
        }
    }
}

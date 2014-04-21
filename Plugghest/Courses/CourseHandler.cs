using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Courses
{
    //Work as a Business Layer.......................
    public class CourseHandler
    {
        CourseController coursecnt = new CourseController();

        public void CreateCourse(Course c)
        {
            coursecnt.CreateCourse(c);
        }

        public Course GetCourse(int CourseID)
        {
            return coursecnt.GetCourse(CourseID);
        }

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            return coursecnt.GetCoursesForDNN();
        }

        //CoursePluggs

        public void CreateCoursePlugg(CourseItems cp)
        {
            coursecnt.CreateCoursePlugg(cp);
        }

        public IEnumerable<CourseItems> GetCoursePlugg(int CourseID, int ItemID)
        {
            return coursecnt.GetCoursePlugg(CourseID, ItemID);
        }

        public IEnumerable<CourseItems> GetCoursePluggsForCourse(int CourseID)
        {
            return coursecnt.GetCoursePluggsForCourse(CourseID);
        }

        public List<Course_Tree> GetCourseItems(int CourseID)
        {
            return coursecnt.GetCourseItems(CourseID);
        }
    }
}

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

        public Course GetCourse(int courseId)
        {
            return coursecnt.GetCourse(courseId);
        }

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            return coursecnt.GetCoursesForDNN();
        }

        //CoursePluggs

        public void CreateCoursePlugg(CoursePlugg cp)
        {
            coursecnt.CreateCoursePlugg(cp);
        }

        public IEnumerable<CoursePlugg> GetCoursePluggsForCourse(int courseId)
        {
            return coursecnt.GetCoursePluggsForCourse(courseId);
        }
    }
}

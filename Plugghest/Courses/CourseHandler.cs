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

        public void CreateCourses(Course c)
        {
            coursecnt.CreateCourse(c);
        }

        public Course GetCourse(int courseId)
        {
            return coursecnt.GetCourse(courseId);
        }

        public Boolean CreateCoursePlugg(CoursePlugg cp)
        {
            return coursecnt.CreateCoursePlugg(cp);
        }

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            return coursecnt.GetCoursesForDNN();
        }
    }
}

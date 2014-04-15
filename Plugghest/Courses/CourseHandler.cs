using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Courses
{
    //Work as a Business Layer.......................
    class CourseHandler
    {
        CourseController coursecnt = null;

        public Course AddNewCourses(Course Course)
        {
            return coursecnt.CreateCourse(Course);
        }

        public Boolean AddNewCoursePlugg(CoursePlugg Course)
        {
            return coursecnt.CreateCoursePlugg(Course);
        }
    }
}

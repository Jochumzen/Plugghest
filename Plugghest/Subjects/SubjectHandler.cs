using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Subjects
{
    public class SubjectHandler
    {
        SubjectController subjectcntr = new SubjectController();

        public List<SubjectTree> GetSubject_Item()
        {
            return subjectcntr.GetSubject_Item();
        }

        public void CreateSubject(Subject t)
        {
            subjectcntr.CreateSubject(t);
        }

        public Subject GetSubject(int SubjectId)
        {
            return subjectcntr.GetSubject(SubjectId);
        }

        public List<Subject> GetSubjectFromMother(int? MotherName, int order)
        {
            return subjectcntr.GetSubjectFromMother(MotherName, order);
        }

        public void UpdateSubjectOrder(int SubjectId, int Order)
        {
            subjectcntr.UpdateSubjectOrder(SubjectId, Order);
        }

    }
}

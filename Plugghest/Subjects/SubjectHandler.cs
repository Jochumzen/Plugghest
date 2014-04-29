using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Subjects
{
    public class SubjectHandler
    {
        SubjectController subjectcntr = new SubjectController();

        public IEnumerable<Subject> GetAllSubjects()
        {
            return subjectcntr.GetAllSubjects();
        }

        public void CreateSubject(Subject t)
        {
            subjectcntr.CreateSubject(t);
        }

        public void UpdateSubject(Subject t)
        {
            subjectcntr.UpdateItem(t);
        }

        public Subject GetSubject(int subjectId)
        {
            return subjectcntr.GetSubject(subjectId);
        }

        public IEnumerable<Subject> GetSubjectsFromMotherWhereOrderGreaterThan(int? mother, int order)
        {
            return subjectcntr.GetSubjectsFromMotherWhereOrderGreaterThan(mother, order);
        }

        //Recursive function for create tree....
        public IList<Subject> GetSubjectsAsTree()
        {
            IEnumerable<Subject> source = GetAllSubjects();

            var groups = source.GroupBy(i => i.Mother);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        //To Add Child
        private void AddChildren(Subject node, IDictionary<int, List<Subject>> source)
        {
            if (source.ContainsKey(node.SubjectID))
            {
                node.children = source[node.SubjectID];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<Subject>();
            }
        }

    }
}

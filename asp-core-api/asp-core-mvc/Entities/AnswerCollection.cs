using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public class AnswerCollection : IEnumerable<Answer>
    {
        private List<Answer> Answers { get; set; } = new List<Answer>();

        public void Add(Answer a) => Answers.Add(a);

        public void Remove(Answer a) => Answers.Remove(a);

        public Answer this[string qid]
        {
            get => Answers.Where(o => o.QuestionID == qid).First();
            set { Answers[Answers.FindIndex(o => o.QuestionID == qid)] = value; }
        }

        public IEnumerator<Answer> GetEnumerator()
        {
            return ((IEnumerable<Answer>)Answers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Answer>)Answers).GetEnumerator();
        }
    }
}

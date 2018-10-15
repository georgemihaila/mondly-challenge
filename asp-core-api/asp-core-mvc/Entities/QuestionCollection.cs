using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public class QuestionCollection : IEnumerable<MultipleAnswerQuestion>
    {
        public QuestionCollection(List<MultipleAnswerQuestion> questions)
        {
            this.MultipleAnswerQuestions = questions;
        }

        public MultipleAnswerQuestion this[int index]
        {
            get => MultipleAnswerQuestions[index];
            set { MultipleAnswerQuestions[index] = value; }
        }

        public MultipleAnswerQuestion this[string id]
        {
            get => MultipleAnswerQuestions.Where(o => o.ID == id).First();
            set { MultipleAnswerQuestions[MultipleAnswerQuestions.FindIndex(o => o.ID == id)] = value; }
        }

        public List<MultipleAnswerQuestion> MultipleAnswerQuestions { get; set; }

        public IEnumerator<MultipleAnswerQuestion> GetEnumerator()
        {
            return ((IEnumerable<MultipleAnswerQuestion>)MultipleAnswerQuestions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<MultipleAnswerQuestion>)MultipleAnswerQuestions).GetEnumerator();
        }
    }
}

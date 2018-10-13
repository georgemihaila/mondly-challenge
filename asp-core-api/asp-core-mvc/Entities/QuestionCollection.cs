using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public class QuestionCollection
    {
        public QuestionCollection(List<IQuestion> questions)
        {
            this.MultipleAnswerQuestions = questions.OfType<MultipleAnswerQuestion>().ToList();
        }

        public List<MultipleAnswerQuestion> MultipleAnswerQuestions { get; set; }
    }
}

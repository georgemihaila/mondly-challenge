using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public sealed class MultipleAnswerQuestion : IQuestion
    {
        public string Title { get; set; }

        public string ID { get; set; }

        public List<string> Answers { get; set; }

        public string CorrectAnswer { get; set; }

        public string Category { get; set; }

        public static explicit operator MultipleAnswerRestrictedQuestion(MultipleAnswerQuestion e)
        {
            return new MultipleAnswerRestrictedQuestion()
            {
                Answers = e.Answers,
                ID = e.ID,
                Title = e.Title
            };
        }
    }
}

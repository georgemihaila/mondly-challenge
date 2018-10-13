using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public static class Globals
    {
        public static QuestionCollection Questions => new QuestionCollection(new List<IQuestion>()
            {
                new MultipleAnswerQuestion()
                {
                    ID = "10",
                    Category = "General knowledge",
                    Title = "Berlin is the capital of which country?",
                    Answers = new List<string>()
                    {
                        "Belgium",
                        "Netherlands",
                        "Germany",
                        "France"
                    },
                    CorrectAnswer = "Germany"
                },
                new MultipleAnswerQuestion()
                {
                    ID = "11",
                    Category = "General knowledge",
                    Title = "Does the moon have gravity?",
                    Answers = new List<string>()
                    {
                        "Yes",
                        "No"
                    },
                    CorrectAnswer = "Yes"
                },
                new MultipleAnswerQuestion()
                {
                    ID = "12",
                    Category = "General knowledge",
                    Title = "You want to make green paint. Which colors do you mix together?",
                    Answers = new List<string>()
                    {
                        "Red and yellow",
                        "Blue and yellow",
                        "Orange and purple"
                    },
                    CorrectAnswer =  "Blue and yellow"
                },
                new MultipleAnswerQuestion()
                {
                    ID = "12",
                    Category = "General knowledge",
                    Title = "Which is the longest river in the world?",
                    Answers = new List<string>()
                    {
                       "Amazon",
                       "Nile",
                       "Mississippi"
                    },
                    CorrectAnswer = "Amazon"
                },
                new MultipleAnswerQuestion()
                {
                    ID = "13",
                    Category = "General knowledge",
                    Title = "What language do most people in Austria speak?",
                    Answers = new List<string>()
                    {
                       "German",
                       "Austrian",
                       "Hungarian"
                    },
                    CorrectAnswer = "German"
                },
            });

        public static List<Session> Sessions { get; set; } = new List<Session>();
    }
}

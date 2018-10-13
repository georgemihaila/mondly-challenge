using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_core_mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static asp_core_mvc.Entities.Globals;

namespace asp_core_mvc.Controllers
{
    public class QuestionsController : Controller
    {

        public string GetNextQuestion(string sid, string token)
        {
            if (!Globals.SessionCollection[sid].IsValid(token))
                return Failure;
            return JsonConvert.SerializeObject(Globals.SessionCollection[sid].Questions[Globals.SessionCollection[sid].Players.FindByToken(token).QuestionIndex]);
        }

        public string Answer(string answer, string qid, string sid, string token)
        {
            if (!Globals.SessionCollection[sid].IsValid(token))
                return Failure;
            Globals.SessionCollection[sid].Players[Globals.SessionCollection[sid].Players.FindByToken(token)].Answers.Add(new Entities.Answer()
            {
                QuestionID = Globals.SessionCollection[sid].Players.FindByToken(token).Answers[qid].QuestionID,
                Value = answer
            });
            Globals.SessionCollection[sid].Players[Globals.SessionCollection[sid].Players.FindByToken(token)].QuestionIndex++;
            if (Same(Globals.SessionCollection[sid].Players.Select(q=>q.Answers.Count()).ToList()))
            {

            }
            return Success;
        }

        

        class ResultCollection
        {
            public ResultCollection()
            {
                this.Results = new List<Results>();
            }

            public List<Results> Results { get; set; }
        }

        class Results
        {
            public string Who { get; set; }

            public string What { get; set; }
        }

        private bool Same(List<int> x)
        {
            for (int i = 0; i < x.Count; i++)
            {
                for (int j = i + 1; j < x.Count; j++)
                    if (x[i] != x[j]) return false;
            }
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_core_mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace asp_core_mvc.Controllers
{
    public class QuestionsController : Controller
    {
        public string MultipleAnswer() => JsonConvert.SerializeObject(Globals.Questions.MultipleAnswerQuestions.Select(o => (MultipleAnswerRestrictedQuestion)o));
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public class MultipleAnswerRestrictedQuestion
    {
        public string Title { get; set; }

        public string ID { get; set; }

        public List<string> Answers { get; set; }
    }
}

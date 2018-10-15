using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public interface IQuestion
    {
        string ID { get; set; }
        
        string Title { get; set; }

        string CorrectAnswer { get; set; }

        string Category { get; set; }
    }
}

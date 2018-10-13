using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public sealed class Session
    {
        public string ID { get; set; }

        public List<Player> Players { get; set; }

        public List<IQuestion> Questions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public sealed class Player
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public int Lives { get; set; } = 3;
    }
}

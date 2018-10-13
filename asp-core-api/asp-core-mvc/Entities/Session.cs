using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public sealed class Session
    {
        public string ID { get; set; }

        public PlayerCollection Players { get; set; }

        public QuestionCollection Questions { get; set; }

        public Player this[Player index]
        {
            get => Players.Where(o => o == index).First();
            set { Players[Players.FindIndex(index)] = value; }
        }

        public bool IsValid(string token)
        {
            return (Players.Select(o => o.Token).Contains(token));
        }

        public Session()
        {
            this.Players = new PlayerCollection(new List<Player>());
            this.Questions = new QuestionCollection(new List<MultipleAnswerQuestion>());
        }
    }
}

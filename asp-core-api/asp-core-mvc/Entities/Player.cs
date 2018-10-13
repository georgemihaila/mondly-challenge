using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public sealed class Player
    {
        public string Name { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public AnswerCollection Answers { get; set; } = new AnswerCollection();

        public int QuestionIndex { get; set; } = 0;

        public string SessionID { get; set; } = string.Empty;

        public int Lives { get; set; } = 3;

        public override bool Equals(object obj)
        {
            var player = obj as Player;
            return player != null &&
                   Name == player.Name &&
                   Token == player.Token &&
                   EqualityComparer<AnswerCollection>.Default.Equals(Answers, player.Answers) &&
                   QuestionIndex == player.QuestionIndex &&
                   SessionID == player.SessionID &&
                   Lives == player.Lives;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Token, Answers, QuestionIndex, SessionID, Lives);
        }
    }
}

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
    public class PlayersController : Controller
    {
        //[HttpPost]
        [Produces("application/json")]
        public string Create(string name)
        {
            Globals.SessionCollection = Globals.SessionCollection ?? new SessionCollection(new List<Session>());
            string sid = GetRandomString(16);
            Player player = new Player()
            {
                Name = name,
                SessionID = sid,
                Token = GetRandomString(8)
            };
            if (Globals.SessionCollection.Count() == 0)
            {
                Globals.SessionCollection.Add(new Session()
                {
                    ID = sid,
                    Players = new PlayerCollection(new List<Player>() { player }),
                    Questions = new QuestionCollection(Globals.GetRandomQuestions(5))
                });
            }
            else
            {
                if (Globals.SessionCollection.ContainsPlayer(name))
                    return Failure;
                var collection = Globals.SessionCollection.Where(q => q.Players.Count() < 3).ToList();
                if (collection.Count() >= 1)
                {
                    var x = collection[0];
                    player.SessionID = x.ID;
                    Globals.SessionCollection[Globals.SessionCollection[x.ID].ID].Players.Add(player);
                }
                else
                {
                    Globals.SessionCollection.Add(new Session()
                    {
                        ID = sid,
                        Players = new PlayerCollection(new List<Player>() { player }),
                        Questions = new QuestionCollection(Globals.GetRandomQuestions(5))
                    });
                }
            }
            return JsonConvert.SerializeObject(player);
        }

        public string List(string sid, string token)
        {
            if (!Globals.SessionCollection[sid].IsValid(token))
                return Failure;
            //return JsonConvert.SerializeObject(Globals.SessionCollection[sid].Questions[Globals.SessionCollection[sid].Players.FindByToken(token).QuestionIndex]);
            return JsonConvert.SerializeObject(Globals.SessionCollection[sid].Players);
        }

        Random random = new Random();
        private string GetRandomString(int length)
        {
            string seed = "abcdef0123456789";
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s += seed[random.Next(seed.Length)];
            return s;
        }
    }
}
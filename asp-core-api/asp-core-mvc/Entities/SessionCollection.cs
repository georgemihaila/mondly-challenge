using asp_core_mvc.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public class SessionCollection : IEnumerable<Session>
    {
        private List<Session> Sessions { get; set; } = new List<Session>();

        public SessionCollection(List<Session> sessions)
        {

        }

        public bool ContainsPlayer(string name) => Sessions.Any(o => o.Players.ContainsPlayer(name));

        public void Add(Session item) => this.Sessions.Add(item);

        public void Remove(Session item) => this.Sessions.Remove(item);

        public Session this[int index]
        {
            get => this.Sessions[index];
            set { Sessions[index] = value; }
        }

        public Session this[string sessionID]
        {
            get { return Sessions.Where(o => o.ID == sessionID).First(); }
            set { Sessions[Sessions.FindIndex(o => o.ID == sessionID)] = value; }
        }

        public IEnumerator<Session> GetEnumerator()
        {
            return ((IEnumerable<Session>)Sessions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Session>)Sessions).GetEnumerator();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_core_mvc.Entities
{
    public class PlayerCollection : IEnumerable<Player>
    {
        private List<Player> Items { get; set; }

        public bool ContainsPlayer(string name) => Items.Where(o => o.Name == name).Count() == 1;

        public void Add(Player player) => Items.Add(player);

        public void Remove(Player player) => Items.Remove(player);

        public int FindIndex(Player p) => Items.FindIndex(o => o == p);

        public Player this[int index]
        {
            get => Items[index];
            set { Items[index] = value; }
        }

        public Player this[string name]
        {
            get => Items.Where(o => o.Name == name).First();
            set { Items[Items.FindIndex(o => o.Name == name)] = value; }
        }

        public Player this[Player player]
        {
            get => Items.Where(o => player == o).First();
            set { Items[Items.FindIndex(o => o.Token == player.Token)] = value; }
        }

        public Player FindByToken(string token) => Items.Where(o => o.Token == token).First();

        public PlayerCollection(List<Player> players)
        {
            this.Items = players;
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return ((IEnumerable<Player>)Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Player>)Items).GetEnumerator();
        }
    }
}

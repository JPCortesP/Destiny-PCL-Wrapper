using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public class ItemBase
    {
        public object Manifest { get; set; }
    }
    
    public class Character
    {

    }
    public class Player
    {
        protected Player() { }
        public Player(object Manifest, object Origen)
        {
            _items = new Lazy<List<ItemBase>>(() => ( initItems (manifest, origen)), true);
            _characters = new Lazy<List<Character>>(() => initChars(manifest, origen), true);
        }
        private object manifest;
        private object origen;
        private Lazy<List<Character>> _characters;
        private Lazy<List<ItemBase>> _items;
        public List<Character> Characters { get { return _characters.Value; } }
        public List<ItemBase> Items { get { return _items.Value; } }

        static List<Character> initChars(object manifest, object origen)
        {
            Thread.Sleep(1000); 
            return new List<Character>();
        }
        static List<ItemBase> initItems( object manifest, object origen)
        {
            Thread.Sleep(1000);
            return new List<ItemBase>();
        }
    }
}

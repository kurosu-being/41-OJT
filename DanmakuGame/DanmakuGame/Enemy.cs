using System.Windows.Forms;

namespace DanmakuGame
{
    public class Enemy
    {
        public int ID { get; internal set; }
        public int Left { get; internal set; }
        public int Bottom { get; internal set; }
        public int Right { get; internal set; }
        public Control Parent { get; internal set; }
    }
}
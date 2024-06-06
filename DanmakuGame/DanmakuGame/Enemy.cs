using System.Windows.Forms;

namespace DanmakuGame
{
    public class Enemy
    {
        public static int Life { get; internal set; }
        public static bool IsDead { get; internal set; }
        public int ID { get; internal set; }
        public int Left { get; internal set; }
        public int Bottom { get; internal set; }
        public int Right { get; internal set; }
        public Control Parent { get; internal set; }
    }
}
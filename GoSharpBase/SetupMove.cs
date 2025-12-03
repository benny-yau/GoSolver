using System;

namespace Go
{
    [Serializable]
    public class SetupMove
    {
        public Point Move { get; set; }
        public Content Content { get; set; }

        public SetupMove(Point move, Content content)
        {
            this.Move = move;
            this.Content = content;
        }

        public override string ToString()
        {
            return "Move: " + Move.ToString() + " Content: " + Content;
        }
    }
}

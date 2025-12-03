using System;

namespace Go
{
    public class Link<T>
    {
        public T Move { get; set; }
        public object CheckMove { get; set; }

        public Link(T move, object checkMove)
        {
            this.Move = move;
            this.CheckMove = checkMove;
        }

        public Boolean EqualLink(Link<T> linkedPoint)
        {
            if (linkedPoint.Move.Equals((T)CheckMove) && ((T)linkedPoint.CheckMove).Equals(Move))
                return true;
            return false;
        }

        public override string ToString()
        {
            return "Move: " + Move.ToString() + " CheckMove: " + CheckMove;
        }
    }
}

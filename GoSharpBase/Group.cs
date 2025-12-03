using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    [Serializable]
    public class Group
    {
        private HashSet<Point> points = new HashSet<Point>();
        private HashSet<Point> liberties = new HashSet<Point>();
        private HashSet<Point> neighbours = new HashSet<Point>();

        public Content Content { get; private set; }
        public Boolean? IsNonKillable { get; set; }

        public HashSet<Point> Liberties
        {
            get
            {
                return liberties;
            }
        }

        public HashSet<Point> Neighbours
        {
            get
            {
                return neighbours;
            }
        }

        public HashSet<Point> Points
        {
            get
            {
                return points;
            }
        }

        public Group(Content c)
        {
            Content = c;
        }

        public void AddNeighbour(Point p, Boolean liberty = false)
        {
            if (liberty)
                liberties.Add(p);

            neighbours.Add(p);
        }

        public override string ToString()
        {
            return Content.ToString() + ":{" + points.GetConcatenatedString() + "}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Represents a group of stones (black, white, or empty spaces) on a board object. Neighbours represent immediate connected points that are of different content.
    /// </summary>
    public class Group
    {
        private HashSet<Point> points = new HashSet<Point>();
        private HashSet<Point> liberties = new HashSet<Point>();
        private HashSet<Point> neighbours = new HashSet<Point>();

        public Content Content { get; private set; }
        public Boolean? IsNonKillable { get; set; }
        public Boolean IsCoveredEye { get; set; }

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

        public void AddPoint(int x, int y)
        {
            points.Add(new Point(x, y));
        }

        public bool ContainsPoint(int x, int y)
        {
            return points.Contains(new Point(x, y));
        }

        public void AddNeighbour(int x, int y, Boolean liberty = false)
        {
            Point p = new Point(x, y);
            if (liberty)
                liberties.Add(p);

            neighbours.Add(p);
        }

        public override string ToString()
        {
            if (points.Count == 0) return Content.ToString() + ":{}";
            string rc = Content.ToString() + ":{";
            foreach (Point p in points) rc += p.ToString() + ",";
            rc = rc.Substring(0, rc.Length - 1) + "}";
            return rc;
        }
    }
}

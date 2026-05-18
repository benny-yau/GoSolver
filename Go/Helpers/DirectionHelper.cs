using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    public static class DirectionExtensions
    {
        [DebuggerStepThroughAttribute()]
        public static Direction Opposite(this Direction direction)
        {
            if (direction == Direction.None) throw new Exception();
            if (direction == Direction.Left)
                return Direction.Right;
            else if (direction == Direction.Right)
                return Direction.Left;
            else if (direction == Direction.Up)
                return Direction.Down;
            else if (direction == Direction.Down)
                return Direction.Up;
            else
                return Direction.None;
        }
    }

    public class DirectionHelper
    {
        static List<Link<Direction>> directionalLinkedList = null;
        /// <summary>
        /// Create linked list of all four directions, in clockwise rotation with direction pointing to center.
        /// </summary>
        public static List<Link<Direction>> DirectionLinkedList
        {
            get
            {
                if (directionalLinkedList == null)
                {
                    Link<Direction> directionLeft = new Link<Direction>(Direction.Left, null);
                    Link<Direction> directionDown = new Link<Direction>(Direction.Down, directionLeft);
                    Link<Direction> directionRight = new Link<Direction>(Direction.Right, directionDown);
                    Link<Direction> directionUp = new Link<Direction>(Direction.Up, directionRight);
                    directionLeft.CheckMove = directionUp;
                    directionalLinkedList = new List<Link<Direction>>() { directionUp, directionRight, directionDown, directionLeft };
                }
                return directionalLinkedList;
            }
        }

        /// <summary>
        /// Get new direction based on number of times direction rotated.
        /// </summary>
        public static Direction GetNewDirection(Direction direction, int count = 0)
        {
            Link<Direction> directionPoint = DirectionLinkedList.Where(m => m.Move == direction).First();
            for (int i = 0; i <= count - 1; i++)
            {
                directionPoint = ((Link<Direction>)directionPoint.CheckMove);
            }
            return directionPoint.Move;
        }

        /// <summary>
        /// Check if point is at edge based on direction specified. 
        /// </summary>
        public static Boolean IsEdgeInDirection(Board board, Point p, Direction direction)
        {
            if (direction == Direction.Left)
                return (p.x == 0);
            if (direction == Direction.Right)
                return (p.x == board.SizeX - 1);
            if (direction == Direction.Up)
                return (p.y == 0);
            if (direction == Direction.Down)
                return (p.y == board.SizeY - 1);
            return false;
        }

        /// <summary>
        /// Move point in direction specified.
        /// </summary>
        public static Point GetPointInDirection(Board board, Point p, Direction direction, Boolean checkWithinBoard = true)
        {
            Point q = Game.PassMove;

            if (direction == Direction.Left)
                q = p.LeftPoint();
            else if (direction == Direction.Right)
                q = p.RightPoint();
            else if (direction == Direction.Up)
                q = p.UpPoint();
            else if (direction == Direction.Down)
                q = p.DownPoint();

            if (checkWithinBoard && !board.PointWithinBoard(q))
                return Game.PassMove;
            return q;
        }

        /// <summary>
        /// Repeat number of times in same direction to move point.
        /// </summary>
        public static Point GetPointInDirection(int repeat, Board board, Point p, Direction direction)
        {
            Point q = p;
            for (int i = 0; i <= repeat - 1; i++)
            {
                q = GetPointInDirection(board, q, direction, false);
            }
            if (!board.PointWithinBoard(q))
                return Game.PassMove;
            return q;
        }

        /// <summary>
        /// Get direction where p is moving away from q.
        /// </summary>
        public static (Direction, int, int) GetDirectionFromTwoPoints(Point p, Point q)
        {
            int x_dist = p.x - q.x;
            int y_dist = p.y - q.y;
            Boolean x_only = Math.Abs(x_dist) >= Math.Abs(y_dist);

            if (x_only)
            {
                if (x_dist > 0)
                    return (Direction.Right, x_dist, y_dist);
                else if (x_dist < 0)
                    return (Direction.Left, x_dist, y_dist);
            }
            else
            {
                if (y_dist > 0)
                    return (Direction.Down, x_dist, y_dist);
                else if (y_dist < 0)
                    return (Direction.Up, x_dist, y_dist);
            }
            return (Direction.None, x_dist, y_dist);
        }

        /// <summary>
        /// Get the count to rotate to get direction from linked list.
        /// </summary>
        public static int GetRotationIndex(Direction wallDirection)
        {
            return DirectionLinkedList.FindIndex(m => m.Move.Equals(wallDirection));
        }


        public static List<Direction> GetDirections(Board board, Point p)
        {
            int n = 3;
            List<Direction> directions = new List<Direction>();
            if (board.PointWithinBoard(p.x - n, p.y))
                directions.Add(Direction.Left);
            if (board.PointWithinBoard(p.x + n, p.y))
                directions.Add(Direction.Right);
            if (board.PointWithinBoard(p.x, p.y - n))
                directions.Add(Direction.Up);
            if (board.PointWithinBoard(p.x, p.y + n))
                directions.Add(Direction.Down);
            return directions;
        }


        public static Boolean CheckPointInDirection(Direction direction, Point p, Point q)
        {
            if (direction == Direction.Left)
            {
                if (q.x < p.x && Math.Abs(q.y - p.y) <= 1)
                    return true;
            }
            else if (direction == Direction.Right)
            {
                if (q.x > p.x && Math.Abs(q.y - p.y) <= 1)
                    return true;
            }
            else if (direction == Direction.Up)
            {
                if (q.y < p.y && Math.Abs(q.x - p.x) <= 1)
                    return true;
            }
            else if (direction == Direction.Down)
            {
                if (q.y > p.y && Math.Abs(q.x - p.x) <= 1)
                    return true;
            }
            return false;
        }

    }
}

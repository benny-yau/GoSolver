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
        static List<LinkedPoint<Direction>> directionalLinkedList = null;
        /// <summary>
        /// Create linked list of all four directions, in clockwise rotation with direction pointing to center.
        /// </summary>
        public static List<LinkedPoint<Direction>> DirectionLinkedList
        {
            get
            {
                if (directionalLinkedList == null)
                {
                    LinkedPoint<Direction> directionLeft = new LinkedPoint<Direction>(Direction.Left, null);
                    LinkedPoint<Direction> directionDown = new LinkedPoint<Direction>(Direction.Down, directionLeft);
                    LinkedPoint<Direction> directionRight = new LinkedPoint<Direction>(Direction.Right, directionDown);
                    LinkedPoint<Direction> directionUp = new LinkedPoint<Direction>(Direction.Up, directionRight);
                    directionLeft.CheckMove = directionUp;
                    directionalLinkedList = new List<LinkedPoint<Direction>>() { directionUp, directionRight, directionDown, directionLeft };
                }
                return directionalLinkedList;
            }
        }

        /// <summary>
        /// Get new direction based on number of times direction rotated.
        /// </summary>
        public static Direction GetNewDirection(Direction direction, int count = 0)
        {
            LinkedPoint<Direction> directionPoint = DirectionLinkedList.Where(m => m.Move == direction).First();
            for (int i = 0; i <= count - 1; i++)
            {
                directionPoint = ((LinkedPoint<Direction>)directionPoint.CheckMove);
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
            Boolean x_only =  Math.Abs(x_dist) >= Math.Abs(y_dist);

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

    }
}

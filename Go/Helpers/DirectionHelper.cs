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
        /// Direction linked list.
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
        /// Get new direction.
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
        /// Is edge in direction. 
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
        /// Get point in direction.
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
        /// Get point in direction.
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
        /// Get directions for leap move.
        /// </summary>
        public static List<KeyValuePair<Point, Direction>> GetDirectionsForLeapMove(Board board)
        {
            List<KeyValuePair<Point, Direction>> directions = new List<KeyValuePair<Point, Direction>>();
            int x = 3;
            Point p = board.Move.Value;
            //check up direction
            if (board.PointWithinBoard(p.x, p.y - x))
                directions.Add(new KeyValuePair<Point, Direction>(p, Direction.Up));
            //check down direction
            if (board.PointWithinBoard(p.x, p.y + x))
                directions.Add(new KeyValuePair<Point, Direction>(p, Direction.Down));
            //check left direction
            if (board.PointWithinBoard(p.x - x, p.y))
                directions.Add(new KeyValuePair<Point, Direction>(p, Direction.Left));
            //check right direction
            if (board.PointWithinBoard(p.x + x, p.y))
                directions.Add(new KeyValuePair<Point, Direction>(p, Direction.Right));
            return directions;
        }

        /// <summary>
        /// Verify opponent in all direction.
        /// </summary>
        public static Boolean VerifyOppponentInAllDirection(Board board, List<Point> opponentPoints)
        {
            foreach (KeyValuePair<Point, Direction> direction in DirectionHelper.GetDirectionsForLeapMove(board))
            {
                if (direction.Value == Direction.Up && !opponentPoints.Where(n => n.y < direction.Key.y).Select(n => board.GetGroupAt(n)).Any(n => CheckOpponentGroup(board, n)))
                    return false;
                if (direction.Value == Direction.Down && !opponentPoints.Where(n => n.y > direction.Key.y).Select(n => board.GetGroupAt(n)).Any(n => CheckOpponentGroup(board, n)))
                    return false;
                if (direction.Value == Direction.Left && !opponentPoints.Where(n => n.x < direction.Key.x).Select(n => board.GetGroupAt(n)).Any(n => CheckOpponentGroup(board, n)))
                    return false;
                if (direction.Value == Direction.Right && !opponentPoints.Where(n => n.x > direction.Key.x).Select(n => board.GetGroupAt(n)).Any(n => CheckOpponentGroup(board, n)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check opponent group.
        /// </summary>
        private static Boolean CheckOpponentGroup(Board board, Group group)
        {
            return group.Points.Count >= 2 && group.Liberties.Count > 1 && group.Liberties.Count <= group.Neighbours.Count * 0.5 && !board.GetNeighbourGroups(group).Any(n => !n.Equals(board.MoveGroup) && ImmovableHelper.IsSuicidalWithoutKo(board, n));
        }

    }
}

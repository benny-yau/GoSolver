using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Represents a board position containing current move and associated properties, without any game context.
    /// </summary>
    [Serializable]
    public class Board
    {
        public int SizeY { get { return content.GetLength(0); } }
        public int SizeX { get { return content.GetLength(1); } }
        private Content[,] content { get; set; }
        private List<Group> GroupCache = new List<Group>();
        public Group[,] GroupCacheFromPoint { get; set; }

        public Point? Move { get; set; }
        public Group MoveGroup { get; set; }
        public Point? singlePointCapture;
        public List<Group> CapturedList = new List<Group>();
        public List<Point> LastMoves = new List<Point>();

        private List<Group> atariTargets;
        public List<Group> AtariTargets
        {
            get
            {
                if (atariTargets == null)
                {
                    atariTargets = new List<Group>();
                    FindAtari(this);
                }
                return atariTargets;
            }
            set
            {
                atariTargets = value;
            }
        }

        public Dictionary<Content, List<Group>> CorneredKillerGroup { get; set; }
        public GameInfo GameInfo { get; set; }
        public static readonly Point PassMove = new Point(-1, -1);

        public Board(int sx, int sy)
        {
            content = new Content[sx, sy];
        }

        public Board(GameInfo gameInfo) : this(gameInfo.BoardSizeX, gameInfo.BoardSizeY)
        {
            this.GameInfo = gameInfo;
        }

        /// <summary>
        /// Creates new board based from last board.
        /// </summary>
        public Board(Board fromBoard)
        {
            content = new Content[fromBoard.SizeX, fromBoard.SizeY];
            Array.Copy(fromBoard.content, content, content.Length);
            this.LastMoves.AddRange(fromBoard.LastMoves);
            this.singlePointCapture = fromBoard.singlePointCapture;
            this.GameInfo = fromBoard.GameInfo;

            this.GroupCache.AddRange(fromBoard.GroupCache);
            if (fromBoard.GroupCacheFromPoint != null)
            {
                GroupCacheFromPoint = new Group[fromBoard.SizeX, fromBoard.SizeY];
                Array.Copy(fromBoard.GroupCacheFromPoint, GroupCacheFromPoint, GroupCacheFromPoint.Length);
            }
        }

        /// <summary>
        /// Get and set content of the point on the board.
        /// </summary>
        public Content this[int x, int y]
        {
            get
            {
                return GetContentAt(x, y);
            }
            set
            {
                SetContentAt(x, y, value);
            }
        }

        public Content this[Point n]
        {
            get
            {
                return GetContentAt(n.x, n.y);
            }
            set
            {
                SetContentAt(n.x, n.y, value);
            }
        }

        private Content GetContentAt(int x, int y)
        {
            return content[x, y];
        }

        /// <summary>
        /// Check for point within board coordinates. Set content on board and clear group cache.
        /// </summary>
        private void SetContentAt(int x, int y, Content c)
        {
            if (!(PointWithinBoard(x, y)))
                throw new Exception("Invalid coordinate.");

            content[x, y] = c;
            ClearGroupCache();
        }

        /// <summary>
        /// Get last move on board if available.
        /// </summary>
        public Point? LastMove
        {
            get
            {
                if (this.LastMoves.Count == 0) return null;
                return this.LastMoves[this.LastMoves.Count - 1];
            }
        }

        /// <summary>
        /// Get all captured points from captured list.
        /// </summary>
        public IEnumerable<Point> CapturedPoints
        {
            get
            {
                foreach (Group group in CapturedList.OrderByDescending(group => group.Points.Count))
                {
                    foreach (Point p in group.Points)
                        yield return p;
                }
            }
        }

        /// <summary>
        /// Create group and obtain group points by recursively adding direct connected points.
        /// Store group in cache and retrieve from cache on the next call.
        /// </summary>
        public Group GetGroupAt(int x, int y)
        {
            if (GroupCacheFromPoint == null) GroupCacheFromPoint = new Group[SizeX, SizeY];
            Group group = GroupCacheFromPoint[x, y];
            if (group == null)
            {
                group = new Group(content[x, y]);
                if (group.Content == Content.Empty)
                    throw new Exception("Group content cannot be empty.");
                RecursiveAddPoint(group, x, y);
                GroupCache.Add(group);
            }
            return group;
        }

        public Group GetGroupAt(Point n)
        {
            return GetGroupAt(n.x, n.y);
        }

        /// <summary>
        /// Add direct connected point recursively to form the group.
        /// If point is not of same content then add to neighbour.
        /// </summary>
        public void RecursiveAddPoint(Group group, int x, int y)
        {
            if (this[x, y] == group.Content)
            {
                if (group.ContainsPoint(x, y)) return;
                group.AddPoint(x, y);
                GroupCacheFromPoint[x, y] = group;
                List<Point> stoneNeighbours = GetStoneNeighbours(x, y);
                stoneNeighbours.ForEach(n => RecursiveAddPoint(group, n.x, n.y));
            }
            else
            {
                group.AddNeighbour(x, y, this[x, y] == Content.Empty);
            }
        }

        /// <summary>
        /// Get total count of liberties of move group.
        /// </summary>
        public int MoveGroupLiberties
        {
            get
            {
                if (this.MoveGroup == null) return 0;
                return this.MoveGroup.Liberties.Count;
            }
        }

        /// <summary>
        /// Get total count of liberties of group.
        /// </summary>
        public int GetGroupLiberties(Group group)
        {
            int libs = 0;
            foreach (Point n in group.Neighbours)
            {
                if (this[n] == Content.Empty) libs++;
            }
            return libs;
        }

        public int GetGroupLiberties(Point p)
        {
            Group group = this.GetGroupAt(p);
            return group.Liberties.Count;
        }

        /// <summary>
        /// Get liberty points of group.
        /// </summary>
        public List<Point> GetGroupLibertyPoints(Group group)
        {
            List<Point> libertyPoints = new List<Point>();
            foreach (Point n in group.Neighbours)
            {
                if (this[n] == Content.Empty)
                    libertyPoints.Add(n);
            }
            return libertyPoints;
        }

        public HashSet<Point> GetLibertiesOfGroups(List<Group> targetGroups)
        {
            HashSet<Point> libertyPoints = new HashSet<Point>();
            targetGroups.ForEach(group => GetGroupLibertyPoints(group).ForEach(p => libertyPoints.Add(p)));
            return libertyPoints;
        }

        /// <summary>
        /// Get all groups that are captured by current move.
        /// </summary>
        public HashSet<Group> GetCapturedGroups(int x, int y)
        {
            HashSet<Group> captures = new HashSet<Group>();
            List<Point> stoneNeighbours = GetStoneNeighbours(x, y);
            Content c = this[x, y];
            foreach (Point n in stoneNeighbours)
            {
                if (this[n] == c.Opposite())
                {
                    Group ngroup = GetGroupAt(n);
                    if (GetGroupLiberties(ngroup) == 0)
                        captures.Add(ngroup);
                }
            }
            return captures;
        }

        /// <summary>
        /// Get directly connected points on all four directions.
        /// </summary>
        public List<Point> GetStoneNeighbours(int x, int y)
        {
            List<Point> rc = new List<Point>();
            if (x > 0) rc.Add(new Point(x - 1, y));
            if (x < SizeX - 1) rc.Add(new Point(x + 1, y));
            if (y > 0) rc.Add(new Point(x, y - 1));
            if (y < SizeY - 1) rc.Add(new Point(x, y + 1));
            return rc;
        }

        public List<Point> GetStoneNeighbours(Point? p = null)
        {
            if (p == null) p = this.Move.Value;
            return GetStoneNeighbours(p.Value.x, p.Value.y);
        }

        /// <summary>
        /// Get diagonal points on all four directions.
        /// </summary>
        public List<Point> GetDiagonalNeighbours(int x, int y)
        {
            List<Point> rc = new List<Point>();
            if (x > 0 && y > 0) rc.Add(new Point(x - 1, y - 1));
            if (x < SizeX - 1 && y < SizeY - 1) rc.Add(new Point(x + 1, y + 1));
            if (y > 0 && x < SizeX - 1) rc.Add(new Point(x + 1, y - 1));
            if (x > 0 && y < SizeY - 1) rc.Add(new Point(x - 1, y + 1));
            return rc;
        }

        public List<Point> GetDiagonalNeighbours(Point? p = null)
        {
            if (p == null) p = this.Move.Value;
            return GetDiagonalNeighbours(p.Value.x, p.Value.y);
        }

        /// <summary>
        /// Get all surrounding points, both stone and diagonal neighbours.
        /// </summary>
        public List<Point> GetStoneAndDiagonalNeighbours(int x, int y)
        {
            return GetStoneNeighbours(x, y).Union(GetDiagonalNeighbours(x, y)).ToList();
        }
        public List<Point> GetStoneAndDiagonalNeighbours(Point? p = null)
        {
            if (p == null) p = this.Move.Value;
            return GetStoneAndDiagonalNeighbours(p.Value.x, p.Value.y);
        }

        /// <summary>
        /// Get closest neighbour points to specific point by going in circles with increasing distance.
        /// </summary>
        public List<Point> GetClosestNeighbour(Point p, int maxDistance = 2, Content c = Content.Unknown, Boolean excludeCorner = true)
        {
            int x = p.x;
            int y = p.y;
            if (c == Content.Unknown) c = this[x, y];
            List<Point> result = new List<Point>();
            for (int i = 1; i <= maxDistance; i++)
            {
                for (int j = -i; j <= i - 1; j++)
                {
                    if (PointWithinBoard(x - i, y - j) && this[x - i, y - j] == c)
                        result.Add(new Point(x - i, y - j)); //left
                    if (PointWithinBoard(x + j, y - i) && this[x + j, y - i] == c)
                        result.Add(new Point(x + j, y - i)); //top
                    if (PointWithinBoard(x + i, y + j) && this[x + i, y + j] == c)
                        result.Add(new Point(x + i, y + j)); //right
                    if (PointWithinBoard(x - j, y + i) && this[x - j, y + i] == c)
                        result.Add(new Point(x - j, y + i)); //bottom
                }
            }
            if (excludeCorner)
                result.RemoveAll(r => Math.Abs(r.x - x) >= Math.Max(2, maxDistance) && Math.Abs(r.x - x) == Math.Abs(r.y - y));
            return result;
        }

        /// <summary>
        /// Get distinct neighbour groups that are of opposite content to the current group.
        /// </summary>
        public HashSet<Group> GetDistinctNeighbourGroups(Group group)
        {
            Content content = group.Content.Opposite();
            HashSet<Group> neighbourGroups = new HashSet<Group>();
            foreach (Point p in group.Neighbours)
            {
                if (this[p] == content)
                {
                    Group pGroup = this.GetGroupAt(p);
                    neighbourGroups.Add(pGroup);
                }
            }
            return neighbourGroups;
        }

        public List<Group> GetNeighbourGroups(Group group = null)
        {
            if (group == null) group = this.MoveGroup;
            return GetDistinctNeighbourGroups(group).ToList();
        }

        public List<Group> GetNeighbourGroups(Point p)
        {
            Group group = this.GetGroupAt(p);
            return GetNeighbourGroups(group);
        }

        public HashSet<Group> GetGroupsFromStoneNeighbours(Point p, Content c)
        {
            List<Point> stoneNeighbours = this.GetStoneNeighbours(p).Where(q => this[q] == c.Opposite()).ToList();
            return this.GetGroupsFromPoints(stoneNeighbours);
        }

        public List<Group> GetGroupsFromStoneNeighbours(Point p)
        {
            return GetGroupsFromStoneNeighbours(p, this[p]).ToList();
        }

        /// <summary>
        /// Get distinct groups from list of points.
        /// </summary>
        public HashSet<Group> GetGroupsFromPoints(List<Point> points)
        {
            HashSet<Group> groups = new HashSet<Group>();
            points.ForEach(p => groups.Add(this.GetGroupAt(p)));
            return groups;
        }

        public bool IsAtariMove
        {
            get
            {
                return AtariTargets.Count > 0;
            }
        }

        /// <summary>
        /// Check if point is single point from direct connected points.
        /// </summary>
        public Boolean IsSinglePoint(Point? p = null)
        {
            if (p == null) p = this.Move;
            Content c = this[p.Value];
            return this.GetStoneNeighbours(p).All(q => this[q] != c);
        }

        /// <summary>
        /// Set points to empty for captured groups.
        /// </summary>
        private List<Group> Capture(IEnumerable<Group> captures)
        {
            foreach (Group g in captures)
            {
                List<Point> captured = g.Points.ToList();
                captured.ForEach(p => this[p] = Content.Empty);
            }
            return captures.ToList();
        }

        public void ClearGroupCache()
        {
            GroupCache.Clear();
            GroupCacheFromPoint = null;
            CorneredKillerGroup = null;
            AtariTargets = null;
            CapturedList.Clear();
        }

        /// <summary>
        /// Create new board and make move on board.
        /// </summary>
        public Board MakeMoveOnNewBoard(Point p, Content c, Boolean excludeKo = false)
        {
            Board board = new Board(this);
            if (board.InternalMakeMove(p, c, excludeKo) == MakeMoveResult.Legal)
                return board;
            return null;
        }

        /// <summary>
        /// Makes move on board internally. Returns result as MakeMoveResult.
        /// </summary>
        public MakeMoveResult InternalMakeMove(int x, int y, Content content, Boolean excludeKo = false)
        {
            Move = new Point(x, y);
            this.CapturedList.Clear();
            Point? previousPtCapture = singlePointCapture;
            singlePointCapture = null;

            if (Move.Equals(PassMove))
            {
                LastMoves.Add(Move.Value);
                return MakeMoveResult.Pass;
            }
            else if (this[x, y] != Content.Empty) // move occupied
                return MakeMoveResult.NotEmpty;

            this[x, y] = content; // make new move

            HashSet<Group> capturedGroups = GetCapturedGroups(x, y);
            if (capturedGroups.Count > 0)
            {
                if (capturedGroups.Count == 1)
                {
                    // check for ko move
                    IEnumerable<Point> points = capturedGroups.First().Points;
                    if (points.Count() == 1)
                    {
                        if (previousPtCapture != null && !excludeKo && Move.Equals(previousPtCapture))  //is ko
                        {
                            this[x, y] = Content.Empty;
                            singlePointCapture = previousPtCapture;
                            return MakeMoveResult.KoBlocked;
                        }
                        singlePointCapture = points.First();
                    }
                }
            }
            this.CapturedList = Capture(capturedGroups);
            MoveGroup = GetGroupAt(x, y);

            if (capturedGroups.Count == 0 && MoveGroupLiberties == 0) // suicide move
            {
                this[x, y] = Content.Empty;
                return MakeMoveResult.Suicide;
            }
            LastMoves.Add(Move.Value);
            return MakeMoveResult.Legal;
        }


        public MakeMoveResult InternalMakeMove(Point p, Content content, Boolean excludeKo = false)
        {
            return InternalMakeMove(p.x, p.y, content, excludeKo);
        }

        /// <summary>
        /// Check if point within board coordinates.
        /// </summary>
        public Boolean PointWithinBoard(int x, int y)
        {
            return (x >= 0 && x <= SizeX - 1 && y >= 0 && y <= SizeY - 1);
        }

        public Boolean PointWithinBoard(Point p)
        {
            return PointWithinBoard(p.x, p.y);
        }

        /// <summary>
        /// Check if point within middle area of board.
        /// </summary>
        public Boolean PointWithinMiddleArea(int x, int y)
        {
            return (x > 0 && x < SizeX - 1 && y > 0 && y < SizeY - 1);
        }

        public Boolean PointWithinMiddleArea(Point p)
        {
            return PointWithinMiddleArea(p.x, p.y);
        }

        public Boolean CornerPoint(Point p)
        {
            return (p.x == 0 || p.x == SizeX - 1) && (p.y == 0 || p.y == SizeY - 1);
        }

        /// <summary>
        /// Find atari for last move on board and set all groups to AtariTargets.
        /// </summary>
        public static List<Group> FindAtari(Board board)
        {
            Point? lastMove = board.LastMove;
            if (lastMove == null) return null;
            if (lastMove.Equals(PassMove)) return null;
            Content c = board[lastMove.Value];

            //set atari targets in board 
            board.AtariTargets = board.GetGroupsFromStoneNeighbours(lastMove.Value, c).Where(group => group.Liberties.Count == 1).ToList();
            return board.AtariTargets;
        }

        /// <summary>
        /// Atari can be resolved by capturing stones or escaping atari group.
        /// Resolve atari by capturing stones <see cref="UnitTestProject.FindAndResolveAtariTest.FindAndResolveAtariTest_Scenario_TianLongTu_Q15618" />
        /// </summary>
        public static Boolean ResolveAtari(Board currentBoard, Board tryBoard)
        {
            //capture stones to resolve atari
            if (tryBoard.CapturedList.Any(group => currentBoard.GetNeighbourGroups(group).Any(g => g.Liberties.Count == 1)))
                return true;

            //check neighbour points with group liberty increased from one.
            if (tryBoard.MoveGroupLiberties > 1)
            {
                Point move = tryBoard.Move.Value;
                Content c = tryBoard[move];
                HashSet<Group> groups = currentBoard.GetGroupsFromStoneNeighbours(move, c.Opposite());
                if (groups.Any(group => group.Liberties.Count == 1))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Draws board.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rc = "\n" + new String(' ', 4);
            for (int j = 0; j < SizeX; j++)
            {
                rc += j.ToString().PadRight(2, ' ');
            }
            for (int i = 0; i < SizeY; i++)
            {
                rc += "\n" + i.ToString().PadLeft(3, ' ') + " ";
                for (int j = 0; j < SizeX; j++)
                {

                    if (content[j, i] == Content.Empty) rc += ".";
                    else if (content[j, i] == Content.Black) rc += "X";
                    else rc += "O";
                    rc += " ";
                }
            }
            return rc;
        }

        public String PrintSetupScript()
        {
            string rc = "";
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Content c = this[i, j];
                    if (c != Content.Empty)
                        rc += "g.SetupMove(" + i + ", " + j + ", Content." + c.ToString() + ");\n";
                }
            }
            return rc;
        }

        /// <summary>
        /// Compares between two boards to check for recursions or superkos. 
        /// </summary>
        public override bool Equals(object value)
        {
            Board compareBoard = (Board)value;
            for (int y = 8; y <= 18; y++)
            {
                for (int x = 0; x <= 12; x++)
                {
                    if (content[x, y] != compareBoard[x, y])
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get last moves in string.
        /// </summary>
        public String GetLastMoves()
        {
            String msg = "";
            List<Point> lastMoves = this.LastMoves;
            for (int i = 0; i <= lastMoves.Count - 1; i++)
            {
                Point p = lastMoves[i];
                msg += p;
                if (i < lastMoves.Count - 1)
                    msg += ",";
            }
            return msg;
        }
    }
}

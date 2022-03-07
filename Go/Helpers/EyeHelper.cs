using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public enum EyeType
    {
        CoveredEye,
        UnCoveredEye,
        SemiSolidEye,
        RealSolidEye
    }

    public class EyeHelper
    {

        /// <summary>
        /// An eye is a point where all direct connected points are black or white as specified.
        /// </summary>
        public static Boolean FindEye(Board board, int x, int y, Content c = Content.Unknown)
        {
            return FindEye(x, y, board, c).Item1;
        }

        public static (Boolean, Content) FindEye(int x, int y, Board board, Content c = Content.Unknown)
        {
            if (!board.PointWithinBoard(x, y))
                return (false, c);
            if (board[x, y] != Content.Empty) return (false, c);

            List<Point> stoneNeighbours = board.GetStoneNeighbours(x, y);
            if (c == Content.Unknown) c = board[stoneNeighbours.First()];
            if (c == Content.Empty) return (false, c);
            if (stoneNeighbours.All(q => board[q] == c))
                return (true, c);
            return (false, c);
        }

        public static Boolean FindEye(Board board, Point p, Content c = Content.Unknown)
        {
            return FindEye(board, p.x, p.y, c);
        }

        /// <summary>
        /// An uncovered eye is a point where none or only one diagonal point covered by opposite content if point is in the middle area, and no diagonal point covered by opposite content if point is at the side or at the corner.
        /// </summary>
        public static Boolean FindUncoveredEye(Board currentBoard, int x, int y, Content c)
        {
            if (FindEye(currentBoard, x, y, c))
            {
                return FindUncoveredPoint(currentBoard, x, y, c);
            }
            return false;
        }

        public static Boolean FindUncoveredPoint(Board currentBoard, int x, int y, Content c)
        {
            List<Point> diagonalNeighbours = currentBoard.GetDiagonalNeighbours(x, y);
            List<Point> oppositeContent = diagonalNeighbours.Where(q => currentBoard[q] == c.Opposite()).ToList();

            if (diagonalNeighbours.Count == 4) // middle area
                return (oppositeContent.Count <= 1);
            else //side or corner
                return (oppositeContent.Count == 0);
        }

        /// <summary>
        /// Find false eye which has one or more diagonal points covered by opposite content.
        /// </summary>
        public static Boolean FindCoveredEye(Board tryBoard, Point p, Content c)
        {
            if (FindNonSemiSolidEye(tryBoard, p, c))
            {
                List<Point> diagonalNeighbours = tryBoard.GetDiagonalNeighbours(p.x, p.y);
                List<Point> oppositeContent = diagonalNeighbours.Where(q => tryBoard[q] == c.Opposite()).ToList();

                //ensure covered point has more than one liberty
                if (diagonalNeighbours.Count == 4)
                {
                    if (oppositeContent.Count > 1 && oppositeContent.Count(q => tryBoard.GetGroupLiberties(q) > 1) > 1)
                        return true;
                }
                else
                {
                    if (oppositeContent.Count > 0 && oppositeContent.Any(q => tryBoard.GetGroupLiberties(q) > 1))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find eye that is not semi solid eye.
        /// </summary>
        public static Boolean FindNonSemiSolidEye(Board board, Point p, Content c)
        {
            return EyeHelper.FindEye(board, p.x, p.y, c) && !EyeHelper.FindSemiSolidEyes(p, board, c).Item1;
        }

        /// <summary>
        /// Suicide at covered eye.
        /// Make move at the other empty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B57" />
        /// Check for killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_2" />
        /// </summary>
        public static Boolean SuicideAtCoveredEye(Board capturedBoard, Board tryBoard)
        {
            if (capturedBoard == null || tryBoard == null) return false;
            Point move = tryBoard.Move.Value;
            Content c = capturedBoard[capturedBoard.Move.Value];
            if (tryBoard.MoveGroup.Points.Count != 2) return false;
            Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(tryBoard, move, c);
            foreach (Group group in capturedBoard.CapturedList)
            {
                if (group.Points.Count != 2) continue;
                //make move again at last move
                Board board = capturedBoard.MakeMoveOnNewBoard(move, c.Opposite());
                if (board == null) continue;
                //capture move and find covered eye
                Board b = ImmovableHelper.CaptureSuicideGroup(board);
                if (b != null && FindCoveredEye(b, move, c))
                    return true;
                //check for killer group
                if (killerGroup == null) continue;
                if (!tryBoard.GetStoneNeighbours().Any(p => tryBoard[p] == Content.Empty)) continue;
                //make move at the other empty point
                Point move2 = group.Points.First(p => !p.Equals(move));
                Board board2 = capturedBoard.MakeMoveOnNewBoard(move2, c.Opposite());
                if (board2 == null) continue;
                Board b2 = ImmovableHelper.CaptureSuicideGroup(board2);
                if (b2 != null && FindCoveredEye(b2, move2, c))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Semi solid eyes are real eyes that can have diagonals with immovable points.
        /// </summary>
        public static (Boolean, List<Point>, List<LinkedPoint<Point>>) FindSemiSolidEyes(Point p, Board board, Content c = Content.Unknown)
        {
            GameInfo gameInfo = board.GameInfo;
            (Boolean isEye, Content content) = FindEye(p.x, p.y, board, c);
            if (!isEye)
                return (false, null, null);
            if (c == Content.Unknown) c = content;

            //ensure all groups have more than one liberty
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(p, c.Opposite());
            if (neighbourGroups.Count > 1 && neighbourGroups.Any(group => group.Liberties.Count == 1))
                return (false, null, null);

            //get suicide point or tiger's mouth at all diagonals
            (List<Point> immovablePoints, List<LinkedPoint<Point>> tigerMouthPoints) = GetImmovablePoints(p, board, c);
            Boolean found = false;
            List<Point> diagonals = board.GetDiagonalNeighbours(p.x, p.y);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            //for eye point in middle, 3 of the diagonals should be immovable
            if (board.PointWithinMiddleArea(p))
                found = (stoneCount + immovablePoints.Count >= diagonalCount - 1);
            else //for eye point at side or corner, all diagonals should be immovable
                found = (stoneCount + immovablePoints.Count == diagonalCount);
            return (found, immovablePoints, tigerMouthPoints);
        }

        /// <summary>
        /// Get all immovable points at eye point diagonals.
        /// </summary>
        private static (List<Point>, List<LinkedPoint<Point>>) GetImmovablePoints(Point eyePoint, Board m, Content c)
        {
            List<Point> immovablePoints = new List<Point>();
            List<LinkedPoint<Point>> tigerMouthPoints = new List<LinkedPoint<Point>>();

            foreach (Point p in m.GetDiagonalNeighbours(eyePoint.x, eyePoint.y))
            {
                if (m[p] == c) continue;
                (Boolean isImmovable, Point? isTigerMouth) = ImmovableHelper.IsImmovablePoint(p, c, m);
                if (isImmovable)
                {
                    immovablePoints.Add(p);
                    if (isTigerMouth != null)
                        tigerMouthPoints.Add(new LinkedPoint<Point>(p, eyePoint));
                }
            }
            return (immovablePoints, tigerMouthPoints);
        }


        /// <summary>
        /// Find real solid eyes, filled with same content at the diagonals, not immovable points.
        /// </summary>
        public static Boolean FindRealSolidEyes(Point p, Content c, Board board)
        {
            if (!FindUncoveredEye(board, p.x, p.y, c))
                return false;

            List<Point> diagonals = board.GetDiagonalNeighbours(p.x, p.y);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            if (board.PointWithinMiddleArea(p)) //middle area
                return (stoneCount >= diagonalCount - 1);
            else //side or corner
                return (stoneCount == diagonalCount);
        }

        /// <summary>
        /// Find if any of the two empty points is a real eye and return only the first one found.
        /// </summary>
        public static Board FindRealEyesWithinTwoEmptyPoints(Board board, Group eyeGroup, EyeType eyeType = EyeType.SemiSolidEye)
        {
            int eyeGroupCount = eyeGroup.Points.Count;
            if (eyeGroupCount != 2) return null;
            if (eyeGroup.Points.Any(p => board[p] != Content.Empty)) return null;
            Point eye = eyeGroup.Points.First();
            Point otherEye = eyeGroup.Points.First(p => !p.Equals(eye));
            Content c = eyeGroup.Content.Opposite();
            Board b = board.MakeMoveOnNewBoard(eye, c);
            if (b != null)
            {
                if (eyeType == EyeType.SemiSolidEye && EyeHelper.FindSemiSolidEyes(otherEye, b, c).Item1)
                    return b;
                if (eyeType == EyeType.RealSolidEye && EyeHelper.FindRealSolidEyes(otherEye, c, b))
                    return b;
            }
            Board b2 = board.MakeMoveOnNewBoard(otherEye, c);
            if (b2 != null)
            {
                if (eyeType == EyeType.SemiSolidEye && EyeHelper.FindSemiSolidEyes(eye, b2, c).Item1)
                    return b2;
                if (eyeType == EyeType.RealSolidEye && EyeHelper.FindRealSolidEyes(eye, c, b2))
                    return b2;
            }
            return null;
        }

        /// <summary>
        /// Find if killer group of four points or less can produce real eye. 
        /// Check connect and die <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_Weiqi101_2282" /> 
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150" /> 
        /// Check snapback <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Scenario_XuanXuanGo_B31" /> 
        /// Check two point filled killer group <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_A38" /> 
        /// Ensure all groups have more than one liberty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31469" /> 
        /// </summary>
        public static Boolean FindRealEyeWithinEmptySpace(Board board, Group killerGroup, EyeType eyeType = EyeType.SemiSolidEye)
        {
            if (killerGroup.Points.Count > 3)
                return false;

            Board b = new Board(board);
            b.LastMoves.Clear();

            //ensure all groups have more than one liberty
            List<Group> neighbourGroups = board.GetNeighbourGroups(killerGroup);
            if (neighbourGroups.Count > 1 && neighbourGroups.Any(group => group.Liberties.Count == 1))
                return false;

            //find real eye
            if (MakeMoveWithinEmptySpace(b, killerGroup, eyeType))
            {
                if (neighbourGroups.Count == 1)
                    return true;

                //check connect and die
                if (neighbourGroups.Any(group => ImmovableHelper.CheckConnectAndDie(board, group)))
                    return false;

                //check snapback
                if (neighbourGroups.Any(group => ImmovableHelper.CheckSnapback(board, group)))
                    return false;

                //check two point filled killer group
                if (eyeType == EyeType.SemiSolidEye && CheckTwoPointFilledKillerGroup(board, killerGroup))
                    return false;

                //ensure survival can make move at empty spaces
                return ImmovableHelper.ClearEmptySpace(board, killerGroup);
            }
            return false;
        }

        public static Boolean FindRealEyeWithinEmptySpace(Board board, Point p, Content c, EyeType eyeType = EyeType.SemiSolidEye)
        {
            Group eyeGroup = BothAliveHelper.GetKillerGroupFromCache(board, p, c);
            if (eyeGroup == null) return false;
            return FindRealEyeWithinEmptySpace(board, eyeGroup, eyeType);
        }

        /// <summary>
        /// Rare scenario to check for semi-solid eye within empty space.
        /// </summary>
        private static Boolean CheckTwoPointFilledKillerGroup(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content.Opposite();
            if (!KoHelper.KoContentEnabled(c.Opposite(), board.GameInfo)) return false;
            if (killerGroup.Points.Count != 3) return false;
            List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == killerGroup.Content).ToList();
            if (contentPoints.Count != 2) return false;

            List<LinkedPoint<Point>> diagonalPoints = LinkHelper.GetGroupDiagonals(board, board.GetGroupAt(contentPoints.First()));
            List<Point> eyeDiagonals = diagonalPoints.Select(p => p.Move).Where(p => board[p] == Content.Empty).ToList();

            if (eyeDiagonals.Count != 1) return false;
            Board b = board;
            //tiger mouth at diagonal
            Point? libertyPoint = ImmovableHelper.FindTigerMouth(board, eyeDiagonals.First(), c);
            if (libertyPoint != null && board[libertyPoint.Value] == Content.Empty)
            {
                (_, b) = ImmovableHelper.IsSuicidalMove(libertyPoint.Value, c, b);
                if (b == null) return false;
            }
            //eye at diagonal
            Point eye = eyeDiagonals.First();
            if (EyeHelper.FindEye(b, eye) && b.GetGroupsFromStoneNeighbours(eye, c).All(group => group.Liberties.Count > 1))
            {
                Group diagonalGroup = b.GetGroupsFromStoneNeighbours(eyeDiagonals.First(), killerGroup.Content).Except(b.GetNeighbourGroups(killerGroup)).FirstOrDefault();
                if (diagonalGroup != null && ImmovableHelper.CheckConnectAndDie(b, diagonalGroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Allow opponent to make move within eye space to ensure the space can produce the required eye type. 
        /// </summary>
        private static Boolean MakeMoveWithinEmptySpace(Board board, Group killerGroup, EyeType eyeType = EyeType.SemiSolidEye)
        {
            Content c = killerGroup.Content;
            List<Point> availablePoints = killerGroup.Points.Where(p => board[p] == Content.Empty && !FindEye(board, p.x, p.y, c.Opposite())).ToList();
            if (availablePoints.Count == 0)
            {
                if (eyeType == EyeType.SemiSolidEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindSemiSolidEyes(k, board, c.Opposite()).Item1);
                else if (eyeType == EyeType.UnCoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindUncoveredEye(board, k.x, k.y, c.Opposite()));
                else if (eyeType == EyeType.CoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindCoveredEye(board, k, c.Opposite()));
                else if (eyeType == EyeType.RealSolidEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindRealSolidEyes(k, c.Opposite(), board));
            }
            //alternate the player content
            Content content = (board.LastMoves.Count % 2 == 0) ? c : c.Opposite();
            Boolean result = false;
            for (int j = 0; j <= availablePoints.Count - 1; j++)
            {
                Board b = new Board(board);
                Point q = availablePoints[j];
                //make move
                if (b.InternalMakeMove(q, content, true) != MakeMoveResult.Legal)
                    b.LastMoves.Add(Game.PassMove);
                //killer move
                if (content == c)
                {
                    //capture of points outside of killer group
                    if (b.CapturedList.Count > 0 && !b.CapturedPoints.All(x => killerGroup.Points.Contains(x)))
                        return false;
                    //atari move that opponent cannot escape
                    if (b.MoveGroupLiberties > 1 && b.AtariTargets.Any(t => ImmovableHelper.UnescapableGroup(b, t).Item1))
                        return false;
                }
                //make opponent move
                result = MakeMoveWithinEmptySpace(b, killerGroup, eyeType);
                if (eyeType == EyeType.CoveredEye)
                {
                    if (result) return true;
                }
                else
                {
                    //opponent to try all possible moves
                    if (content == c && result == false)
                        return false;
                    if (content == c.Opposite() && result == true)
                        return true;
                }
            }
            return result;
        }
    }
}

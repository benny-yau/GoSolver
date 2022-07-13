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
        public static Boolean FindUncoveredEye(Board currentBoard, Point n, Content c)
        {
            if (FindEye(currentBoard, n, c))
            {
                return FindUncoveredPoint(currentBoard, n, c);
            }
            return false;
        }

        public static Boolean FindUncoveredPoint(Board currentBoard, Point eyePoint, Content c)
        {
            return !IsCovered(currentBoard, eyePoint, c);
        }

        public static Boolean CoveredMove(Board board, Point eyePoint, Content c)
        {
            List<Point> diagonalPoints = board.GetDiagonalNeighbours(eyePoint);
            List<Point> stonePoints = board.GetStoneNeighbours();
            diagonalPoints = diagonalPoints.Intersect(stonePoints).ToList();
            if (!diagonalPoints.All(p => board[p] == c.Opposite())) return false;
            if (!IsCovered(board, eyePoint, c)) return false;
            return true;
        }

        public static Boolean IsCovered(Board board, Point p, Content c)
        {
            List<Point> diagonalNeighbours = board.GetDiagonalNeighbours(p);
            List<Point> oppositeContent = diagonalNeighbours.Where(q => board[q] == c.Opposite()).ToList();
            if (diagonalNeighbours.Count == 4) // middle area
                return (oppositeContent.Count >= 2);
            else //side or corner
                return (oppositeContent.Count >= 1);
        }

        /// <summary>
        /// Find false eye which has one or more diagonal points covered by opposite content.
        /// </summary>
        public static Boolean FindCoveredEye(Board tryBoard, Point p, Content c)
        {
            if (FindNonSemiSolidEye(tryBoard, p, c))
            {
                if (IsCovered(tryBoard, p, c))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find covered eye within empty space after capture.
        /// </summary>
        public static Boolean FindCoveredEyeByCapture(Board capturedBoard, Group capturedGroup)
        {
            int capturedCount = capturedGroup.Points.Count;
            return (capturedCount == 1 || capturedCount == 2) && EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, capturedGroup, EyeType.CoveredEye);
        }

        public static Boolean FindCoveredEyeByCapture(Board board)
        {
            Board b = ImmovableHelper.CaptureSuicideGroup(board);
            if (b != null && FindCoveredEyeByCapture(b, board.MoveGroup))
                return true;
            return false;
        }

        /// <summary>
        /// Check for covered eye with one or more liberties for suicide group.
        /// </summary>
        public static Boolean CheckCoveredEyeAtSuicideGroup(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            Point move = tryBoard.Move.Value;
            Content c = group.Content;

            if (FindCoveredEyeByCapture(tryBoard))
                return true;

            if (group.Points.Count != 2) return false;
            if (group.Points.Any(p => tryBoard.GetDiagonalNeighbours(p).Count(q => tryBoard[q] == c && LinkHelper.PointsBetweenDiagonals(p, q).All(r => tryBoard[r] == c.Opposite())) == (tryBoard.PointWithinMiddleArea(p) ? 2 : 1)))
                return true;
            return false;
        }

        /// <summary>
        /// Find eye that is not semi solid eye.
        /// </summary>
        public static Boolean FindNonSemiSolidEye(Board board, Point p, Content c)
        {
            return EyeHelper.FindEye(board, p, c) && !EyeHelper.FindSemiSolidEyes(p, board, c).Item1;
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
            Content c = capturedBoard.MoveGroup.Content;
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
        public static (Boolean, List<Point>) FindSemiSolidEyes(Point p, Board board, Content c = Content.Unknown)
        {
            GameInfo gameInfo = board.GameInfo;
            (Boolean isEye, Content content) = FindEye(p.x, p.y, board, c);
            if (!isEye)
                return (false, null);
            if (c == Content.Unknown) c = content;

            //ensure all groups have more than one liberty
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(p, c.Opposite());
            if (neighbourGroups.Count > 1 && neighbourGroups.Any(group => group.Liberties.Count == 1))
                return (false, null);

            //get suicide point or tiger's mouth at all diagonals
            List<Point> immovablePoints = GetImmovablePoints(p, board, c);
            Boolean found = false;
            List<Point> diagonals = board.GetDiagonalNeighbours(p);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            //for eye point in middle, 3 of the diagonals should be immovable
            if (board.PointWithinMiddleArea(p))
                found = (stoneCount + immovablePoints.Count >= diagonalCount - 1);
            else //for eye point at side or corner, all diagonals should be immovable
                found = (stoneCount + immovablePoints.Count == diagonalCount);
            return (found, immovablePoints);
        }

        /// <summary>
        /// Get all immovable points at eye point diagonals.
        /// </summary>
        private static List<Point> GetImmovablePoints(Point eyePoint, Board board, Content c)
        {
            List<Point> immovablePoints = new List<Point>();
            foreach (Point p in board.GetDiagonalNeighbours(eyePoint))
            {
                if (board[p] == c) continue;
                if (ImmovableHelper.IsImmovablePoint(p, c, board).Item1)
                    immovablePoints.Add(p);
            }
            return immovablePoints;
        }


        /// <summary>
        /// Find real solid eyes, filled with same content at the diagonals, not immovable points.
        /// </summary>
        public static Boolean FindRealSolidEyes(Point p, Content c, Board board)
        {
            if (!FindUncoveredEye(board, p, c))
                return false;

            List<Point> diagonals = board.GetDiagonalNeighbours(p);
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
        /// Find if killer group of three points or less can produce real eye. 
        /// Check snapback <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Scenario_XuanXuanGo_B31" /> 
        /// Ensure all groups have more than one liberty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31469" /> 
        /// Ensure survival can make move at empty spaces <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" /> 
        /// </summary>
        public static Boolean FindRealEyeWithinEmptySpace(Board board, Group killerGroup, EyeType eyeType = EyeType.SemiSolidEye)
        {
            if (killerGroup.Points.Count > 3)
                return false;

            //ensure all groups have more than one liberty
            List<Group> eyeGroups = board.GetNeighbourGroups(killerGroup);
            if (eyeGroups.Count > 1 && eyeGroups.Any(group => group.Liberties.Count == 1))
                return false;

            Board b = new Board(board);
            b.LastMoves.Clear();

            //find real eye
            if (MakeMoveWithinEmptySpace(b, killerGroup, eyeType))
            {
                if (eyeGroups.Count == 1 || eyeType == EyeType.CoveredEye)
                    return true;

                //check snapback
                if (eyeGroups.Any(group => ImmovableHelper.CheckSnapback(board, group)))
                    return false;

                //check unique corner connect and die
                if (CheckUniqueCornerConnectAndDie(board, killerGroup))
                    return false;

                return true;
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
        /// Unique corner connect and die.
        /// One-point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_Corner_B33" /> 
        /// Two-point capture <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanQiJing_A38" /> 
        /// </summary>
        private static Boolean CheckUniqueCornerConnectAndDie(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            if (!KoHelper.KoContentEnabled(c, board.GameInfo)) return false;

            if (killerGroup.Points.Count == 1)
            {
                //ensure corner point
                Point k = killerGroup.Points.First();
                if (!board.GetStoneNeighbours(k).Any(p => board[p] == c.Opposite() && board.CornerPoint(p) && board.GetGroupAt(p).Points.Count == 1)) return false;

            }
            else
            {
                //two-point capture
                if (killerGroup.Points.Count != 3) return false;
                List<Point> contentPoints = killerGroup.Points.Where(t => board[t] == c).ToList();
                if (contentPoints.Count != 2) return false;

                //ensure corner point
                if (!killerGroup.Points.Any(p => board.CornerPoint(p))) return false;
            }

            List<LinkedPoint<Point>> diagonalPoints = LinkHelper.GetGroupDiagonals(board, killerGroup);
            List<Point> eyeDiagonals = diagonalPoints.Select(p => p.Move).Where(p => board[p] == Content.Empty).ToList();
            if (eyeDiagonals.Count != 1) return false;

            Board b = board;
            if (killerGroup.Points.Count > 1)
            {
                //tiger mouth at diagonal
                Point? libertyPoint = ImmovableHelper.FindTigerMouth(board, eyeDiagonals.First(), c.Opposite());
                if (libertyPoint != null && board[libertyPoint.Value] == Content.Empty)
                {
                    (_, b) = ImmovableHelper.IsSuicidalMove(libertyPoint.Value, c.Opposite(), b);
                    if (b == null) return false;
                }
            }

            //eye at diagonal
            Point eye = eyeDiagonals.First();
            if (!EyeHelper.FindEye(b, eye) || board.PointWithinMiddleArea(eye)) return false;

            List<Group> eyeGroups = b.GetGroupsFromStoneNeighbours(eye, c).ToList();
            if (!eyeGroups.All(group => group.Liberties.Count > 1)) return false;

            //check connect and die at target group
            Group targetGroup = eyeGroups.Except(b.GetNeighbourGroups(killerGroup)).FirstOrDefault();
            if (targetGroup != null && ImmovableHelper.CheckConnectAndDie(b, targetGroup))
                return true;
            return false;
        }

        /// <summary>
        /// Allow opponent to make move within eye space to ensure the space can produce the required eye type. 
        /// </summary>
        private static Boolean MakeMoveWithinEmptySpace(Board board, Group killerGroup, EyeType eyeType = EyeType.SemiSolidEye)
        {
            Content c = killerGroup.Content;
            List<Point> availablePoints = killerGroup.Points.Where(p => board[p] == Content.Empty && !FindEye(board, p, c.Opposite())).ToList();
            if (availablePoints.Count == 0)
            {
                if (eyeType == EyeType.SemiSolidEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindSemiSolidEyes(k, board, c.Opposite()).Item1);
                else if (eyeType == EyeType.UnCoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindUncoveredEye(board, k, c.Opposite()));
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
                if (b.InternalMakeMove(q, content) != MakeMoveResult.Legal)
                    b.LastMoves.Add(Game.PassMove);
                //killer move
                if (content == c)
                {
                    if (b.CapturedList.Count > 0)
                    {
                        //capture of real eye group
                        if (b.CapturedList.Count == 1 && b.GetNeighbourGroups().Count == 0)
                            return true;
                        return false;
                    }
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

        /// <summary>
        /// Real eye of diagonally connected groups.
        /// Check for covered eye killer group <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16738" /> 
        /// </summary>
        public static Boolean RealEyeOfDiagonallyConnectedGroups(Board board, Group killerGroup, Boolean checkConnected = false)
        {
            Content c = killerGroup.Content;
            if (killerGroup.Points.Count <= 3 || killerGroup.Points.Any(p => board[p] != Content.Empty)) return false;
            //check for covered eye killer group
            if (killerGroup.Points.Any(p => EyeHelper.IsCovered(board, p, c.Opposite()))) return false;

            if (checkConnected)
            {
                (Boolean isKillerGroup, List<Group> groups) = BothAliveHelper.CheckNeighbourGroupsOfKillerGroup(board, killerGroup);
                if (!isKillerGroup) return false;

                HashSet<Group> connectedGroups = LinkHelper.GetAllDiagonalConnectedGroups(board, groups.First());
                if (groups.Except(connectedGroups).Any())
                    return false;
            }
            return true;
        }
    }
}

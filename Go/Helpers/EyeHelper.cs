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

        public static Boolean FindEye(Board board, Point eye, Content c = Content.Unknown)
        {
            return FindEye(board, eye.x, eye.y, c);
        }

        /// <summary>
        /// An uncovered eye is a point where none or only one diagonal point covered by opposite content if point is in the middle area, and no diagonal point covered by opposite content if point is at the side or at the corner.
        /// </summary>
        public static Boolean FindUncoveredEye(Board currentBoard, Point eye, Content c)
        {
            if (FindEye(currentBoard, eye, c))
            {
                return FindUncoveredPoint(currentBoard, eye, c);
            }
            return false;
        }

        public static Boolean FindUncoveredPoint(Board currentBoard, Point eye, Content c)
        {
            return !IsCovered(currentBoard, eye, c);
        }

        public static Boolean CoveredMove(Board board, Point eye, Content c)
        {
            List<Point> diagonalPoints = board.GetDiagonalNeighbours(eye);
            List<Point> stonePoints = board.GetStoneNeighbours();
            diagonalPoints = diagonalPoints.Intersect(stonePoints).ToList();
            if (!diagonalPoints.All(p => board[p] == c.Opposite())) return false;
            if (!IsCovered(board, eye, c)) return false;
            return true;
        }

        public static Boolean IsCovered(Board board, Point eye, Content c)
        {
            List<Point> diagonalNeighbours = board.GetDiagonalNeighbours(eye).Where(q => board[q] == c.Opposite()).ToList();
            List<Point> stonePoints = board.GetStoneNeighbours(eye).Where(n => board[n] == c).ToList();
            if (board.PointWithinMiddleArea(eye))
                return (stonePoints.Count >= 3 && diagonalNeighbours.Count >= 2);
            else //side or corner
                return (stonePoints.Count >= 2 && diagonalNeighbours.Count >= 1);
        }

        /// <summary>
        /// Find false eye which has one or more diagonal points covered by opposite content.
        /// </summary>
        public static Boolean FindCoveredEye(Board tryBoard, Point eye, Content c)
        {
            if (FindNonSemiSolidEye(tryBoard, eye, c) && IsCovered(tryBoard, eye, c))
            {
                if (!tryBoard.GetGroupsFromStoneNeighbours(eye, c.Opposite()).All(gr => gr.Liberties.Count == 1))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find covered eye within empty space after capture.
        /// </summary>
        public static Boolean FindCoveredEyeAfterCapture(Board capturedBoard, Group capturedGroup)
        {
            int capturedCount = capturedGroup.Points.Count;
            if (capturedCount != 1 && capturedCount != 2) return false;
            return EyeHelper.FindRealEyeWithinEmptySpace(capturedBoard, capturedGroup, EyeType.CoveredEye);
        }

        public static Boolean FindCoveredEyeByCapture(Board board)
        {
            Board b = ImmovableHelper.CaptureSuicideGroup(board);
            if (b != null && FindCoveredEyeAfterCapture(b, board.MoveGroup))
                return true;
            return false;
        }

        /// <summary>
        /// Check for two-point covered eye for suicide group.
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
        public static Boolean FindNonSemiSolidEye(Board board, Point eye, Content c)
        {
            return EyeHelper.FindEye(board, eye, c) && !EyeHelper.FindSemiSolidEye(eye, board, c).Item1;
        }

        /// <summary>
        /// Two-point suicide at covered eye. 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.CoveredEyeMoveTest_Scenario_WuQingYuan_Q31469" />
        /// Make move at the other empty point <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B57" />
        /// Check for killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16424_2" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WuQingYuan_Q31499_2" />
        /// </summary>
        public static Boolean TwoPointSuicideAtCoveredEye(Board capturedBoard, Board tryBoard)
        {
            if (capturedBoard == null || tryBoard == null) return false;
            Point move = tryBoard.Move.Value;
            Content c = capturedBoard.MoveGroup.Content;
            if (tryBoard.MoveGroup.Points.Count != 2) return false;
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(tryBoard, move, c);
            foreach (Group group in capturedBoard.CapturedList)
            {
                if (group.Points.Count != 2) continue;
                //make move again at last move
                Board b = capturedBoard.MakeMoveOnNewBoard(move, c.Opposite());
                if (b == null) continue;
                //capture move and find covered eye
                if (EyeHelper.FindCoveredEyeByCapture(b))
                    return true;
                //check for killer group
                if (killerGroup == null) continue;
                if (!tryBoard.GetStoneNeighbours().Any(p => tryBoard[p] == Content.Empty)) continue;
                //make move at the other empty point
                Point move2 = group.Points.First(p => !p.Equals(move));
                Board b2 = capturedBoard.MakeMoveOnNewBoard(move2, c.Opposite());
                if (b2 == null) continue;
                if (EyeHelper.FindCoveredEyeByCapture(b2))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Semi solid eyes are real eyes that can have diagonals with immovable points.
        /// </summary>
        public static (Boolean, List<Point>) FindSemiSolidEye(Point eye, Board board, Content c = Content.Unknown)
        {
            GameInfo gameInfo = board.GameInfo;
            (Boolean isEye, Content content) = FindEye(eye.x, eye.y, board, c);
            if (!isEye)
                return (false, null);
            if (c == Content.Unknown) c = content;

            //ensure all groups have more than one liberty
            HashSet<Group> neighbourGroups = board.GetGroupsFromStoneNeighbours(eye, c.Opposite());
            if (neighbourGroups.Count > 1 && neighbourGroups.Any(group => group.Liberties.Count == 1))
                return (false, null);

            //get suicide point or tiger's mouth at all diagonals
            List<Point> immovablePoints = GetImmovablePoints(eye, board, c);
            Boolean found = false;
            List<Point> diagonals = board.GetDiagonalNeighbours(eye);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            //for eye point in middle, 3 of the diagonals should be immovable
            if (board.PointWithinMiddleArea(eye))
                found = (stoneCount + immovablePoints.Count >= diagonalCount - 1);
            else //for eye point at side or corner, all diagonals should be immovable
                found = (stoneCount + immovablePoints.Count == diagonalCount);

            //check double atari
            if (found && stoneCount < 2 && DoubleAtariOnSemiSolidEye(board, eye, c))
                return (false, null);

            return (found, immovablePoints);
        }

        /// <summary>
        /// Double atari on semi solid eye. <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_DoubleAtariOnSemiSolidEye" />
        /// </summary>
        private static Boolean DoubleAtariOnSemiSolidEye(Board board, Point eye, Content c)
        {
            List<Point> stoneNeighbours = board.GetStoneNeighbours(eye);
            foreach (Point p in stoneNeighbours)
            {
                Group eyeGroup = board.GetGroupAt(p);
                if (eyeGroup.Points.Count != 1) continue;
                List<Point> diagonalPoints = LinkHelper.GetGroupLinkedDiagonals(board, eyeGroup).Select(d => d.Move).Except(stoneNeighbours).ToList();
                if (diagonalPoints.Count < 2 || board.GetGroupsFromPoints(diagonalPoints).Count(n => n.Liberties.Count <= 2) < 2) continue;
                foreach (Point q in board.GetStoneNeighbours(p).Where(n => !n.Equals(eye) && board[n] == Content.Empty))
                {
                    (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(q, c.Opposite(), board, false);
                    if (suicidal) continue;
                    if (b.CapturedList.Count > 0 || b.AtariTargets.Count > 1)
                        return true;
                }
            }
            return false;
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
        public static Boolean FindRealSolidEye(Point eye, Content c, Board board)
        {
            if (!FindUncoveredEye(board, eye, c))
                return false;

            List<Point> diagonals = board.GetDiagonalNeighbours(eye);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            if (board.PointWithinMiddleArea(eye)) //middle area
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
                if (eyeType == EyeType.SemiSolidEye && EyeHelper.FindSemiSolidEye(otherEye, b, c).Item1)
                    return b;
                if (eyeType == EyeType.RealSolidEye && EyeHelper.FindRealSolidEye(otherEye, c, b))
                    return b;
            }
            Board b2 = board.MakeMoveOnNewBoard(otherEye, c);
            if (b2 != null)
            {
                if (eyeType == EyeType.SemiSolidEye && EyeHelper.FindSemiSolidEye(eye, b2, c).Item1)
                    return b2;
                if (eyeType == EyeType.RealSolidEye && EyeHelper.FindRealSolidEye(eye, c, b2))
                    return b2;
            }
            return null;
        }

        /// <summary>
        /// Find if killer group of three points or less can produce real eye. 
        /// Check snapback <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Scenario_XuanXuanGo_B31" /> 
        /// Ensure all groups have more than one liberty <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31469" /> 
        /// Ensure survival can make move at empty spaces <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_GuanZiPu_Q18796_2" /> 
        /// Check group for two opponent stones <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A139_3" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982" />
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
                if (eyeGroups.Count == 1 || eyeType != EyeType.SemiSolidEye)
                    return true;

                //check group for two opponent stones
                if (killerGroup.Points.Count == 3 && killerGroup.Points.Count(p => board[p] == killerGroup.Content) == 2)
                    return false;

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
            Group eyeGroup = GroupHelper.GetKillerGroupFromCache(board, p, c);
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
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindSemiSolidEye(k, board, c.Opposite()).Item1);
                else if (eyeType == EyeType.UnCoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindUncoveredEye(board, k, c.Opposite()));
                else if (eyeType == EyeType.CoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindCoveredEye(board, k, c.Opposite()));
                else if (eyeType == EyeType.RealSolidEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindRealSolidEye(k, c.Opposite(), board));
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
                if (content == c && b.CapturedList.Count > 0)
                {
                    //capture of real eye group
                    if (b.CapturedList.Count == 1 && b.GetNeighbourGroups().Count == 0)
                        return true;
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

        /// <summary>
        /// Real eye of diagonally connected groups.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_GuanZiPu_B3_2" /> 
        /// Check for covered eye killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_4" /> 
        /// Ensure all groups are connected <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_ScenarioHighLevel28" /> 
        /// </summary>
        public static Boolean RealEyeOfDiagonallyConnectedGroups(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            if (killerGroup.Points.Count <= 3) return false;
            //check for covered eye killer group
            if (killerGroup.Points.Any(p => EyeHelper.IsCovered(board, p, c.Opposite()))) return false;

            (Boolean isKillerGroup, List<Group> connectedGroups) = GroupHelper.CheckNeighbourGroupsOfKillerGroup(board, killerGroup, false);
            if (!isKillerGroup)
                return false;

            foreach (Group group in connectedGroups)
            {
                //ensure all groups are connected
                List<Group> diagonalGroups = LinkHelper.GetAllDiagonalGroups(board, group, null, false).Intersect(connectedGroups).ToList();
                if (diagonalGroups.Any(d => d != group && !LinkHelper.IsImmediateDiagonallyConnected(board, d, group)))
                    return false;

                //check connect and die
                if (ImmovableHelper.CheckConnectAndDie(board, group))
                    return false;
            }

            if (killerGroup.Points.Any(p => board[p] != Content.Empty))
            {
                //ensure all liberties cannot create eye for opponent
                if (killerGroup.Points.Where(p => board[p] == Content.Empty).All(lib => NoEyeForOpponentWithinKillerGroup(board, lib, c, connectedGroups)))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// No eye for opponent within killer group.
        /// </summary>
        public static Boolean NoEyeForOpponentWithinKillerGroup(Board board, Point liberty, Content c, List<Group> connectedGroups)
        {
            if (board.GetStoneNeighbours(liberty).Any(n => board[n] == c.Opposite() && connectedGroups.Contains(board.GetGroupAt(n))))
                return true;

            Boolean eyeInMiddleArea = board.PointWithinMiddleArea(liberty);
            int diagonalWallCount = 0;
            foreach (Point q in board.GetDiagonalNeighbours(liberty))
            {
                if (board[q] == c.Opposite() && connectedGroups.Contains(board.GetGroupAt(q)))
                    diagonalWallCount += 1;
                if (eyeInMiddleArea && diagonalWallCount > 1 || !eyeInMiddleArea && diagonalWallCount > 0)
                    return true;
            }
            return false;
        }
    }
}

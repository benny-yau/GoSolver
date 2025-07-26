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
        /// Find eye.
        /// </summary>
        public static Boolean FindEye(Board board, Point eye, Content c = Content.Unknown)
        {
            if (board[eye] != Content.Empty) return false;
            List<Point> nstones = board.GetStoneNeighbours(eye);
            if (c == Content.Unknown) c = board[nstones.First()];
            if (c == Content.Empty) return false;
            if (nstones.All(q => board[q] == c))
                return true;
            return false;
        }

        /// <summary>
        /// Find uncovered eye.
        /// </summary>
        public static Boolean FindUncoveredEye(Board board, Point eye, Content c)
        {
            if (FindEye(board, eye, c))
            {
                return FindUncoveredPoint(board, eye, c);
            }
            return false;
        }

        public static Boolean FindUncoveredPoint(Board currentBoard, Point eye, Content c)
        {
            return !IsCovered(currentBoard, eye, c);
        }

        /// <summary>
        /// Covered move.
        /// </summary>
        public static Boolean CoveredMove(Board board, Point eye, Content c)
        {
            List<Point> diagonals = board.GetDiagonalNeighbours(eye);
            List<Point> nstones = board.GetStoneNeighbours();
            diagonals = diagonals.Intersect(nstones).ToList();
            if (!diagonals.All(p => board[p] == c.Opposite())) return false;
            if (!IsCovered(board, eye, c)) return false;
            return true;
        }

        /// <summary>
        /// Is covered.
        /// </summary>
        public static Boolean IsCovered(Board board, Point eye, Content c)
        {
            List<Point> npoints = LinkHelper.GetDiagonalsAtStoneNeighbours(board, eye, c);
            List<Point> diagonals = board.GetDiagonalNeighbours(eye).Where(n => board[n] == c.Opposite() && board.GetStoneNeighbours(n).Intersect(npoints).Count() >= 2).ToList();
            if (diagonals.All(n => ImmovableHelper.CheckConnectAndDie(board, board.GetGroupAt(n), false))) return false;
            if (board.PointWithinMiddleArea(eye))
                return (diagonals.Count >= 2);
            else
                return (diagonals.Count >= 1);
        }

        /// <summary>
        /// Find covered eye.
        /// </summary>
        public static Boolean FindCoveredEye(Board board, Point eye, Content c)
        {
            if (FindEye(board, eye, c) && IsCovered(board, eye, c))
            {
                if (!board.GetGroupsFromStoneNeighbours(eye, c.Opposite()).All(gr => gr.Liberties.Count == 1))
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

        /// <summary>
        /// Find non semi solid eye.
        /// </summary>
        public static Boolean FindNonSemiSolidEye(Board board, Point eye, Content c)
        {
            return EyeHelper.FindUncoveredEye(board, eye, c) && !EyeHelper.FindSemiSolidEye(eye, board, c).Item1;
        }

        /// <summary>
        /// Find semi solid eye.
        /// </summary>
        public static (Boolean, List<Point>) FindSemiSolidEye(Point eye, Board board, Content c)
        {
            GameInfo gameInfo = board.GameInfo;
            if (!FindEye(board, eye, c)) return (false, null);

            //ensure all groups have more than one liberty
            HashSet<Group> ngroups = board.GetGroupsFromStoneNeighbours(eye, c.Opposite());
            if (ngroups.Count > 1 && ngroups.Any(n => n.Liberties.Count == 1))
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
            return (found, immovablePoints);
        }

        /// <summary>
        /// Get all immovable points.
        /// </summary>
        private static List<Point> GetImmovablePoints(Point eyePoint, Board board, Content c)
        {
            List<Point> immovablePoints = new List<Point>();
            foreach (Point p in board.GetDiagonalNeighbours(eyePoint))
            {
                if (board[p] == c) continue;
                if (ImmovableHelper.IsImmovablePoint(board, p, c))
                    immovablePoints.Add(p);
            }
            return immovablePoints;
        }


        /// <summary>
        /// Find real solid eye.
        /// </summary>
        public static Boolean FindRealSolidEye(Point eye, Content c, Board board)
        {
            if (!FindUncoveredEye(board, eye, c))
                return false;

            List<Point> diagonals = board.GetDiagonalNeighbours(eye);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            if (board.PointWithinMiddleArea(eye))
                return (stoneCount >= diagonalCount - 1);
            else
                return (stoneCount == diagonalCount);
        }

        /// <summary>
        /// Find real eye within two empty point, and return only the first one found.
        /// Check killer formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q2413_2" /> 
        /// </summary>
        public static Board FindRealEyeWithinTwoEmptyPoints(Board board, Group eyeGroup, EyeType eyeType = EyeType.RealSolidEye)
        {
            if (eyeGroup == null || eyeGroup.Points.Count != 2 || eyeGroup.Points.Any(p => board[p] != Content.Empty)) return null;
            Point eye = eyeGroup.Points.First();
            Point otherEye = eyeGroup.Points.First(p => !p.Equals(eye));
            Content c = eyeGroup.Content.Opposite();
            Board b = board.MakeMoveOnNewBoard(eye, c);
            Board b2 = board.MakeMoveOnNewBoard(otherEye, c);
            if (b != null && EyeHelper.FindRealEyeWithinEmptySpace(b, otherEye, c, eyeType))
            {
                //check killer formation
                if (b2 != null && !KillerFormationHelper.IsKillerFormationFromFunc(b2))
                    return b;
            }
            if (b2 != null && EyeHelper.FindRealEyeWithinEmptySpace(b2, eye, c, eyeType))
            {
                if (b != null && !KillerFormationHelper.IsKillerFormationFromFunc(b))
                    return b2;
            }
            return null;
        }

        /// <summary>
        /// Find real eye within empty space, not more than three points. 
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
            if (eyeGroups.Count > 1 && eyeGroups.Any(n => n.Liberties.Count == 1))
                return false;

            Board b = new Board(board);
            b.LastMoves.Clear();

            //find real eye
            if (MakeMoveWithinEmptySpace(b, killerGroup, eyeType))
            {
                if (eyeGroups.Count == 1 || eyeType != EyeType.SemiSolidEye)
                    return true;

                //check two opponent stones
                if (CheckTwoOpponentStonesInRealEye(board, killerGroup))
                    return false;

                //check snapback
                if (killerGroup.Points.Any(p => board[p] != Content.Empty && ImmovableHelper.CheckSnapbackFromMove(board, p)))
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
        /// Check two opponent stones in real eye.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A139_3" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982" />
        /// </summary>
        private static Boolean CheckTwoOpponentStonesInRealEye(Board board, Group killerGroup)
        {
            if (killerGroup.Points.Count != 3) return false;
            List<Point> nstones = killerGroup.Points.Where(p => board[p] == killerGroup.Content).ToList();
            if (nstones.Count != 2) return false;
            HashSet<Group> ngroups = board.GetGroupsFromPoints(nstones);
            if (ngroups.Any(n => LinkHelper.GetDiagonalGroups(board, n).Any(s => !ngroups.Contains(s))))
                return true;
            return false;
        }

        /// <summary>
        /// Make move within empty space, to check if the required eye type can be produced. 
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
                    //whole group dying
                    if (b.GetNeighbourGroups().Count == 0)
                        return true;
                    return false;
                }
                //make opponent move
                result = MakeMoveWithinEmptySpace(b, killerGroup, eyeType);
                //return result
                if (!result && eyeType != EyeType.CoveredEye)
                    return false;
                if (result && eyeType == EyeType.CoveredEye)
                    return true;
            }
            return result;
        }

        /// <summary>
        /// Real eye of diagonally connected groups.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_GuanZiPu_B3_2" /> 
        /// Check connect and die <see cref="UnitTestProject.BothAliveTest.BothAliveTest_Scenario_WuQingYuan_Q15126" /> 
        /// Check for covered eye killer group <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_4" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30315" /> 
        /// Possible corner six formation <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" /> 
        /// Ensure all groups are connected <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_ScenarioHighLevel28" /> 
        /// </summary>
        public static Boolean RealEyeOfDiagonallyConnectedGroups(Board board, Group killerGroup)
        {
            Content c = killerGroup.Content;
            if (killerGroup.Points.Count <= 3) return false;

            //ensure killer group is surrounded by diagonal groups
            (Boolean isKillerGroup, List<Group> diagonalGroups) = GroupHelper.CheckNeighbourGroupsOfKillerGroup(board, killerGroup);
            if (!isKillerGroup)
                return false;

            if (!WallHelper.StrongGroups(board, diagonalGroups))
                return false;

            List<LinkedPoint<Point>> checkedDiagonals = new List<LinkedPoint<Point>>();
            foreach (Group diagonalGroup in diagonalGroups)
            {
                //ensure all groups are connected
                foreach (LinkedPoint<Point> diagonal in LinkHelper.GetGroupLinkedDiagonals(board, diagonalGroup))
                {
                    Group group = board.GetGroupAt(diagonal.Move);
                    if (!diagonalGroups.Contains(group)) continue;
                    if (checkedDiagonals.Any(n => n.EqualLink(diagonal))) continue;
                    if (!LinkHelper.IsImmediateDiagonallyConnected(board, diagonalGroup, group))
                        return false;
                    checkedDiagonals.Add(diagonal);
                }
            }

            //check for covered eye killer group
            if (killerGroup.Points.Any(p => EyeHelper.IsCovered(board, p, c.Opposite()))) return false;

            //possible corner six formation
            if (KillerFormationHelper.OneByThreeFormation(board, killerGroup) && killerGroup.Points.Any(n => board.CornerPoint(n)))
                return false;

            //check opponent stones within killer group
            List<Point> opponentStones = killerGroup.Points.Where(p => board[p] == c).ToList();
            if (!opponentStones.Any()) return true;

            HashSet<Group> opponentGroups = board.GetGroupsFromPoints(opponentStones);
            if (opponentGroups.Count == 1 && ImmovableHelper.CheckConnectAndDie(board, opponentGroups.First(), false))
                return true;

            //ensure all liberties cannot create eye for opponent
            if (killerGroup.Points.Where(p => board[p] == Content.Empty).All(n => NoEyeForOpponentWithinKillerGroup(board, n, c)))
                return true;

            if (WallHelper.TargetWithAnyNonKillableGroup(board, killerGroup))
                return true;

            return false;
        }

        /// <summary>
        /// Find real eye for any killer group. Within three points at FindRealEyeWithinEmptySpace and more than three points at RealEyeOfDiagonallyConnectedGroups.
        /// </summary>
        public static Boolean FindRealEyeOfAnyKillerGroup(Board board, Group killerGroup)
        {
            if (EyeHelper.FindRealEyeWithinEmptySpace(board, killerGroup) || EyeHelper.RealEyeOfDiagonallyConnectedGroups(board, killerGroup))
                return true;
            return false;
        }

        /// <summary>
        /// No eye for opponent within killer group.
        /// </summary>
        public static Boolean NoEyeForOpponentWithinKillerGroup(Board board, Point liberty, Content c)
        {
            if (board.GetStoneNeighbours(liberty).Any(n => board[n] == c.Opposite()))
                return true;

            Boolean eyeInMiddleArea = board.PointWithinMiddleArea(liberty);
            int diagonalWallCount = 0;
            foreach (Point q in board.GetDiagonalNeighbours(liberty))
            {
                if (board[q] == c.Opposite())
                    diagonalWallCount += 1;
                if (eyeInMiddleArea && diagonalWallCount > 1 || !eyeInMiddleArea && diagonalWallCount > 0)
                    return true;
            }
            return false;
        }
    }
}

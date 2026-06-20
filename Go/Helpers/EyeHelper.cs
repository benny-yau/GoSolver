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
            if (!FindEye(board, eye, c)) return false;
            return !IsCovered(board, eye, c);
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
        /// Covered point within two point group.
        /// </summary>
        public static Boolean CoveredPointWithinTwoPointGroup(Board board, Point move, Content c)
        {
            if (IsCovered(board, move, c) && GroupHelper.CheckKillerGroupPoints(board, move, c) != null)
                return true;
            return false;
        }

        /// <summary>
        /// Find covered eye.
        /// </summary>
        public static Boolean FindCoveredEye(Board board, Point eye, Content c)
        {
            if (!FindEye(board, eye, c)) return false;
            if (!IsCovered(board, eye, c)) return false;
            if (board.GetGroupsFromStoneNeighbours(eye, c.Opposite()).All(gr => gr.Liberties.Count == 1)) return false;
            return true;
        }

        /// <summary>
        /// Find covered eye at stone neighbour.
        /// </summary>
        public static List<Point> FindCoveredEyeAtStoneNeighbour(Board board)
        {
            Content c = board.MoveGroup.Content;
            return board.GetStoneNeighbours().Where(n => EyeHelper.FindCoveredEye(board, n, c)).ToList();
        }

        /// <summary>
        /// Find non semi solid eye.
        /// </summary>
        public static Boolean FindNonSemiSolidEye(Board board, Point eye, Content c)
        {
            return EyeHelper.FindUncoveredEye(board, eye, c) && !EyeHelper.FindSemiSolidEye(board, eye, c);
        }

        /// <summary>
        /// Find semi solid eye.
        /// </summary>
        public static Boolean FindSemiSolidEye(Board board, Point eye, Content c)
        {
            if (!FindEye(board, eye, c)) return false;

            //ensure all groups have more than one liberty
            List<Group> ngroups = board.GetGroupsFromStoneNeighbours(eye, c.Opposite());
            if (ngroups.Count > 1 && ngroups.Any(n => n.Liberties.Count == 1))
                return false;

            //get immovable points at all diagonals
            List<Point> immovablePoints = board.GetDiagonalNeighbours(eye).Where(n => ImmovableHelper.IsImmovablePoint(board, n, c)).ToList();
            Boolean found = false;
            List<Point> diagonals = board.GetDiagonalNeighbours(eye);
            int stoneCount = diagonals.Count(d => board[d] == c);
            int diagonalCount = diagonals.Count;
            //for eye point in middle, 3 of the diagonals should be immovable
            if (board.PointWithinMiddleArea(eye))
                found = (stoneCount + immovablePoints.Count >= diagonalCount - 1);
            else //for eye point at side or corner, all diagonals should be immovable
                found = (stoneCount + immovablePoints.Count == diagonalCount);
            return found;
        }

        /// <summary>
        /// Find real solid eye.
        /// </summary>
        public static Boolean FindRealSolidEye(Board board, Point eye, Content c)
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

            //find real eye
            if (CheckRealEyeWithinEmptySpace(board, killerGroup, eyeType))
            {
                if (eyeType != EyeType.SemiSolidEye)
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
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindSemiSolidEye(board, k, c.Opposite()));
                else if (eyeType == EyeType.UnCoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindUncoveredEye(board, k, c.Opposite()));
                else if (eyeType == EyeType.CoveredEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindCoveredEye(board, k, c.Opposite()));
                else if (eyeType == EyeType.RealSolidEye)
                    return killerGroup.Points.Any(k => board[k] == Content.Empty && FindRealSolidEye(board, k, c.Opposite()));
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
                if (eyeType == EyeType.CoveredEye)
                {
                    if (result) return true;
                }
                else
                {
                    if (content == c && result == false)
                        return false;
                    if (content == c.Opposite() && result == true)
                        return true;
                }
            }
            return result;
        }

        /// <summary>
        /// Check real eye within empty space. 
        /// </summary>
        public static Boolean CheckRealEyeWithinEmptySpace(Board board, Group killerGroup, EyeType eyeType = EyeType.SemiSolidEye)
        {
            //ensure all groups have more than one liberty
            List<Group> eyeGroups = board.GetNeighbourGroups(killerGroup);
            if (eyeGroups.Count == 1)
                return true;
            if (eyeGroups.Any(n => n.Liberties.Count == 1))
                return false;

            Board b = new Board(board);
            b.LastMoves.Clear();

            return MakeMoveWithinEmptySpace(b, killerGroup, eyeType);
        }

        /// <summary>
        /// Real eye of diagonally connected groups.
        /// Ensure all groups are connected <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_ScenarioHighLevel28" /> 
        /// Check four point group <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_B28_2" />
        /// Check opponent stones within killer group <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_A67_101Weiqi" /> 
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38_3" /> 
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

            //ensure all groups are connected
            List<Link<Point>> checkedDiagonals = new List<Link<Point>>();
            foreach (Group diagonalGroup in diagonalGroups)
            {
                foreach (Link<Point> diagonal in LinkHelper.GetGroupLinkedDiagonals(board, diagonalGroup))
                {
                    Group group = board.GetGroupAt(diagonal.Move);
                    if (!diagonalGroups.Contains(group)) continue;
                    if (checkedDiagonals.Any(n => n.EqualLink(diagonal))) continue;
                    if (!LinkHelper.IsImmediateDiagonallyConnected(board, diagonalGroup, group))
                        return false;
                    checkedDiagonals.Add(diagonal);
                }
            }

            //check four point group
            if (killerGroup.Points.Count == 4 && !CheckRealEyeWithinEmptySpace(board, killerGroup))
                return false;

            //check opponent stones within killer group
            List<Point> opponentStones = killerGroup.Points.Where(p => board[p] == c).ToList();
            if (!opponentStones.Any())
                return true;

            foreach (Group group in board.GetGroupsFromPoints(opponentStones))
            {
                if (!ImmovableHelper.CheckConnectAndDie(board, group, false))
                    return false;
                foreach (Link<Point> p in LinkHelper.GetGroupLinkedDiagonals(board, group))
                {
                    if (LinkHelper.PointsBetweenDiagonals(p).All(n => board[n] == c.Opposite() && diagonalGroups.Contains(board.GetGroupAt(n))))
                        return false;
                }
            }
            return true;
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

        public static Boolean FindRealEyeOfAnyKillerGroup(Board board, Point p, Content c)
        {
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(board, p, c);
            if (killerGroup == null) return false;
            if (EyeHelper.FindRealEyeWithinEmptySpace(board, killerGroup) || EyeHelper.RealEyeOfDiagonallyConnectedGroups(board, killerGroup))
                return true;
            return false;
        }

        #region check real eye at diagonal or neighbour group
        /// <summary>
        /// Check real eye in neighbour groups.
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_TianLongTu_Q16738_3" />
        /// <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanGo_Q18472" />
        /// Check for corner six <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_A38" />
        /// Find real eye with strong groups <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_XuanXuanQiJing_B25" />
        /// </summary>
        public static Boolean CheckRealEyeInNeighbourGroups(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;

            //real eye at move killer group
            Group killerGroup = GroupHelper.GetKillerGroupFromCache(captureBoard, move, c.Opposite());
            if (killerGroup != null && killerGroup.Points.Count <= 2 && !EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, killerGroup))
                return false;

            //check for corner six
            if (KillerFormationHelper.CornerSixFormation(tryBoard))
                return false;

            //bent three
            if (KillerFormationHelper.BentThreeSuicideAtCoveredEye(tryBoard, captureBoard))
                return false;

            //find real eye in neighbour killer groups
            if (killerGroup == null) killerGroup = tryBoard.MoveGroup;
            foreach ((Group kgroup, List<Group> cgroups) in GroupHelper.CheckIfNeighbourKillerGroup(captureBoard, killerGroup))
            {
                if (cgroups.Count == 1) return true;
                if (!WallHelper.StrongGroups(captureBoard, cgroups)) continue;
                if (EyeHelper.FindRealEyeOfAnyKillerGroup(captureBoard, kgroup))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check diagonal for killer group on capture.
        /// </summary>
        public static IEnumerable<Group> CheckDiagonalForKillerGroupOnCapture(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            foreach (Link<Point> d in LinkHelper.GetGroupDiagonals(tryBoard).Where(n => tryBoard[n.Move] != c.Opposite()))
            {
                (Boolean rc, Group groupP) = GroupHelper.CheckIfDifferentKillerGroup(captureBoard, d.Move, move, c.Opposite());
                if (rc) yield return groupP;
            }
        }

        /// <summary>
        /// Check diagonal for real eye on capture.
        /// </summary>
        public static IEnumerable<Group> CheckDiagonalForRealEyeOnCapture(Board tryBoard, Board captureBoard)
        {
            return CheckDiagonalForKillerGroupOnCapture(tryBoard, captureBoard).Where(n => n != null && EyeHelper.FindRealEyeOfAnyKillerGroup(captureBoard, n));
        }

        /// <summary>
        /// Check capture move liberty.
        /// </summary>
        public static Boolean CheckCaptureMoveLiberty(Board tryBoard, Board captureBoard)
        {
            Point move = tryBoard.Move.Value;
            Content c = tryBoard.MoveGroup.Content;
            foreach (Point p in captureBoard.GetMoveLiberties().Where(n => !n.Equals(move)))
            {
                (Boolean rc, Group groupP) = GroupHelper.CheckIfDifferentKillerGroup(captureBoard, p, move, c.Opposite());
                if (!rc) continue;
                if (WallHelper.NoEyeForSurvival(captureBoard, p, c.Opposite())) continue;
                if (groupP != null && EyeHelper.FindRealEyeWithinEmptySpace(captureBoard, groupP)) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Find real eye at diagonal.
        /// </summary>
        public static IEnumerable<Point> FindRealEyeAtDiagonal(Board board, Point p, Content c)
        {
            foreach (Point d in board.GetDiagonalNeighbours(p))
            {
                if (board[d] == c) continue;
                if (EyeHelper.FindRealEyeWithinEmptySpace(board, d, c))
                    yield return d;
            }
        }

        /// <summary>
        /// Find real eye at diagonal.
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_XuanXuanGo_A151_101Weiqi_2" /> 
        /// Three point real eye <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WindAndTime_Q30188" /> 
        /// <see cref="UnitTestProject.RedundantKoMoveTest.RedundantKoMoveTest_Scenario_WuQingYuan_Q30982" /> 
        /// </summary>
        public static Boolean FindRealEyeAtDiagonal(List<Point> diagonals, Board b, Content c)
        {
            List<Group> killerGroups = GroupHelper.GetKillerGroupsFromPoints(diagonals, b, c);
            return killerGroups.All(n => EyeHelper.FindRealEyeOfAnyKillerGroup(b, n));
        }
        #endregion
    }
}

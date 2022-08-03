using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class LifeCheck
    {
        /// <summary>
        /// Confirm alive.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_ScenarioTestConfirmAlive1" />
        /// Partial alive <see cref="UnitTestProject.PartiallyAliveTest.PartiallyAliveTest_Scenario_WindAndTime_Q30215" />
        /// </summary>
        public static ConfirmAliveResult ConfirmAlive(Board board, List<Point> target = null)
        {
            target = LifeCheck.GetTargets(board, target);
            if (target == null || target.Count == 0) return ConfirmAliveResult.Unknown;

            if (target.Count > 1) //partial alive
            {
                if (target.Any(p => ConfirmAlive(board, p) == ConfirmAliveResult.Alive))
                    return ConfirmAliveResult.Alive;
            }
            else if (target.Count == 1)
            {
                return ConfirmAlive(board, target.First());
            }
            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16860" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A28" />
        /// </summary>
        public static ConfirmAliveResult ConfirmAlive(Board board, Point target)
        {
            List<Group> eyes = new List<Group>();
            List<LinkedPoint<Point>> tigerMouthList = new List<LinkedPoint<Point>>();

            Content c = board[target];
            //ensure at least two liberties
            Group targetGroup = board.GetGroupAt(target);
            if (targetGroup.Liberties.Count == 1) return ConfirmAliveResult.Unknown;

            //get at least two possible eyes
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board, c, true).OrderBy(gr => gr.Points.Count).ToList();
            if (killerGroups.Count < 2) return ConfirmAliveResult.Unknown;
            //get extended groups from target group
            HashSet<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(board, targetGroup);
            //ensure group is connected to target
            killerGroups.RemoveAll(e => !board.GetNeighbourGroups(e).All(n => groups.Contains(n)));
            if (killerGroups.Count < 2) return ConfirmAliveResult.Unknown;

            //check for semi solid eyes
            for (int i = 0; i <= killerGroups.Count - 1; i++)
            {
                Group group = killerGroups[i];
                if (group.Points.Count <= 3)
                {
                    if (EyeHelper.FindRealEyeWithinEmptySpace(board, group, EyeType.SemiSolidEye))
                        eyes.Add(group);
                }
                else
                {
                    if (EyeHelper.RealEyeOfDiagonallyConnectedGroups(board, group))
                        eyes.Add(group);
                }
                //get tiger mouths of eye groups
                GetTigerMouthsOfEyeGroups(board, group, tigerMouthList);
                if (eyes.Count + killerGroups.Count - 1 - i < 2)
                    break;
            }
            if (eyes.Count < 2) return ConfirmAliveResult.Unknown;
            //check for tiger mouth exception
            if (CheckTigerMouthExceptions(board, tigerMouthList.Select(t => t.Move), c, true)) return ConfirmAliveResult.Unknown;

            if (CheckOpponentDoubleAtari(board, eyes, tigerMouthList)) return ConfirmAliveResult.Unknown;

            //at least two semi solid eyes to predetermine target group is alive
            if (eyes.Count >= 2)
                return ConfirmAliveResult.Alive;
            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Check tiger mouth exceptions.
        /// Possible corner three formation <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A139_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31503" />
        /// Suicidal at tiger mouth  <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31177" />
        /// Tiger mouth escape with atari <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150" />
        /// Double tiger mouth <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_B3" />
        /// </summary>
        public static Boolean CheckTigerMouthExceptions(Board board, IEnumerable<Point> tigerMouthList, Content c, Boolean lifeCheck = false)
        {
            foreach (Point tigerMouth in tigerMouthList)
            {
                Point? libertyPoint = ImmovableHelper.FindTigerMouth(board, tigerMouth, c);
                if (libertyPoint == null) continue;
                if (board[libertyPoint.Value] != Content.Empty)
                    continue;

                if (lifeCheck)
                {
                    //possible corner three formation
                    if (KillerFormationHelper.PossibleCornerThreeFormation(board, tigerMouth, c))
                        return true;
                    //suicidal at tiger mouth
                    if (board.GetDiagonalNeighbours(tigerMouth).Any(n => EyeHelper.FindUncoveredEye(board, n, c) && board.GetGroupsFromStoneNeighbours(n, c.Opposite()).All(e => e.Liberties.Count <= 2)) && ImmovableHelper.IsSuicidalMove(board, tigerMouth, c))
                        return true;
                }

                if (CommonTigerMouthExceptions(board, c, tigerMouth, libertyPoint.Value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Common tiger mouth exceptions.
        /// Check for atari at tiger mouth <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_4" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_9" />
        /// Check for tiger mouth threat group  
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_7" />
        /// Check for two threat groups <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_6" />
        /// Check for another tiger mouth at move <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_10" />
        /// Check for link breakage <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_Nie60_2" />
        /// Link breakage for pre atari groups <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_3" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_4" />
        /// </summary>
        public static Boolean CommonTigerMouthExceptions(Board board, Content c, Point tigerMouth, Point libertyPoint)
        {
            //check for tiger mouth threat group
            Group threatGroup = TigerMouthThreatGroup(board, tigerMouth, c);
            if (threatGroup != null && AtariHelper.AtariByGroup(board, threatGroup))
                return true;

            //make move at liberty
            Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite());
            if (b != null && !ImmovableHelper.CheckConnectAndDie(b))
            {
                //check for atari at tiger mouth
                HashSet<Group> tmGroups = b.GetGroupsFromStoneNeighbours(tigerMouth, c.Opposite());
                Boolean isNegligible = !(b.MoveGroupLiberties > 2 && b.AtariTargets.Any(t => !tmGroups.Contains(t) && !ImmovableHelper.UnescapableGroup(b, t).Item1)) && b.CapturedList.Count == 0 && !Board.ResolveAtari(board, b);
                if (!isNegligible)
                    return true;

                if (threatGroup != null)
                {
                    //check for two threat groups
                    if (LinkHelper.GetPreviousMoveGroup(board, b).Any(t => t.Liberties.Count == 2 && !t.Equals(threatGroup) && t.Liberties.Any(l => ImmovableHelper.FindTigerMouth(board, c, l))))
                        return true;
                    //check for another tiger mouth at move
                    if (b.GetStoneNeighbours().Any(n => b[n] == Content.Empty && ImmovableHelper.FindTigerMouth(board, c, n)))
                        return true;
                }

                //check for link breakage
                if (b.MoveGroup.Points.Count > 1)
                {
                    List<Point> stoneNeighbours = LinkHelper.GetNeighboursDiagonallyLinked(b);
                    if (b.GetDiagonalNeighbours(b.Move).Any(n => b[n] != c && b.GetStoneNeighbours(n).Intersect(stoneNeighbours).Count() >= 2 && !ImmovableHelper.IsImmovablePoint(n, c, b).Item1))
                        return true;
                }
            }

            //check if another tiger mouth present at diagonal neighbours
            return DoubleTigerMouthLink(board, c, tigerMouth, libertyPoint);
        }

        private static Group TigerMouthThreatGroup(Board board, Point tigerMouth, Content c)
        {
            List<Point> lib = board.GetStoneNeighbours(tigerMouth).Where(n => board[n] != c).ToList();
            if (lib.Count == 1 && board[lib.First()] == c.Opposite())
            {
                Group threatGroup = board.GetGroupAt(lib.First());
                if (threatGroup.Liberties.Count == 2)
                    return threatGroup;
            }
            return null;
        }


        /// <summary>
        /// Double tiger mouth link.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150" />
        /// </summary>
        public static Boolean DoubleTigerMouthLink(Board board, Content c, Point tigerMouth, Point libertyPoint)
        {
            if (board[tigerMouth] != Content.Empty || !board.GetStoneNeighbours(tigerMouth).Contains(libertyPoint)) return false;
            //make killer move at liberty
            Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite());
            if (b == null || b.MoveGroupLiberties <= 2) return false;
            //get all stone neigbours of liberty
            List<Point> diagonals = board.GetStoneNeighbours(libertyPoint);
            diagonals.Remove(tigerMouth);
            diagonals = diagonals.Where(d => board[d] == Content.Empty && ImmovableHelper.FindTigerMouth(board, c, d)).ToList();
            foreach (Point diagonal in diagonals)
            {
                //ensure link for groups
                Board b2 = b.MakeMoveOnNewBoard(diagonal, c);
                if (b2 != null && LinkHelper.IsAbsoluteLinkForGroups(b, b2) && !ImmovableHelper.IsSuicidalMove(b2, tigerMouth, c.Opposite()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get tiger mouth of eye groups.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_3" />
        /// </summary>
        private static void GetTigerMouthsOfEyeGroups(Board board, Group eye, List<LinkedPoint<Point>> tigerMouthList)
        {
            Content c = eye.Content;
            List<LinkedPoint<Point>> diagonalPoints = LinkHelper.GetGroupDiagonals(board, eye);
            foreach (LinkedPoint<Point> p in diagonalPoints)
            {
                if (board[p.Move] == Content.Empty && ImmovableHelper.FindTigerMouth(board, c.Opposite(), p.Move))
                {
                    if (board.GetGroupsFromStoneNeighbours(p.Move, c).Count() == 1) continue;
                    //ensure tiger mouth is immovable
                    if (ImmovableHelper.IsImmovablePoint(p.Move, c.Opposite(), board).Item1)
                        tigerMouthList.Add(p);
                }
            }
        }


        /// <summary>
        /// Check opponent double atari moves.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_7" />
        /// </summary>
        private static Boolean CheckOpponentDoubleAtari(Board board, List<Group> eyes, List<LinkedPoint<Point>> tigerMouthList)
        {
            //get eye groups
            List<Group> targetGroups = new List<Group>();
            eyes.ForEach(eye => targetGroups.AddRange(board.GetNeighbourGroups(eye)));

            //get tiger mouth groups
            Content c = eyes.First().Content;
            tigerMouthList.ForEach(tigerMouth => targetGroups.AddRange(board.GetGroupsFromStoneNeighbours(tigerMouth.Move, c)));
            targetGroups = targetGroups.Where(t => t.Liberties.Count == 2).Distinct().ToList();
            //get distinct liberties of target groups
            List<Point> liberties = board.GetLibertiesOfGroups(targetGroups).Distinct().ToList();

            foreach (Point liberty in liberties)
            {
                //make atari move
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c, board);
                if (suicidal) continue;
                //double atari with any target group
                if (b.AtariTargets.Count >= 2 && b.AtariTargets.Any(a => targetGroups.Any(t => t.Equals(board.GetGroupAt(a.Points.First())))))
                {
                    if (DoubleAtariEscape(b)) continue;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Double atari escape.
        /// </summary>
        private static Boolean DoubleAtariEscape(Board board)
        {
            if (board.AtariTargets.Count <= 1) return false;
            foreach (Group targetGroup in board.AtariTargets)
            {
                //make escape move for target group
                (Boolean unEscapable, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(board, targetGroup);
                if (unEscapable) continue;
                //check if any atari targets left
                if (!board.AtariTargets.Any(t => escapeBoard.GetGroupLiberties(t.Points.First()) == 1))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get targets of survival group. Can specify other targets than specified in game info.
        /// Specify another target <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_GuanZiPu_B3" />
        /// </summary>
        public static List<Point> GetTargets(Board board, List<Point> target = null)
        {
            Content content = Content.Unknown;
            if (target == null)
            {
                //get target from game info
                target = new List<Point>(board.GameInfo.targetPoints);
                content = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
                return target.Where(t => board[t] == content).ToList(); //get the target that is still alive
            }
            else //can specify another target
            {
                target = new List<Point>(target);
                Point? p = target.Where(t => board[t] != Content.Empty).FirstOrDefault();
                if (!Convert.ToBoolean(p.Value.NotEmpty)) return new List<Point>();
                else return new List<Point>() { p.Value };
            }
        }

        /// <summary>
        /// Get target group and all diagonally connected groups.
        /// </summary>
        public static HashSet<Group> GetTargetConnectedGroups(Board board, List<Point> target = null)
        {
            HashSet<Group> groups = new HashSet<Group>();
            if (target == null)
            {
                target = LifeCheck.GetTargets(board);
                if (target.Count == 0) return groups;
            }
            foreach (Group targetGroup in board.GetGroupsFromPoints(target))
                LinkHelper.GetAllDiagonalConnectedGroups(board, targetGroup, groups);

            return groups;
        }

        /// <summary>
        /// Check if target points are killed when converted to empty points or kill content points.
        /// </summary>
        public static ConfirmAliveResult CheckIfTargetGroupKilled(Board board, List<Point> targetGroup = null, Content content = Content.Unknown)
        {
            if (targetGroup == null)
            {
                GameInfo gameInfo = board.GameInfo;
                targetGroup = gameInfo.targetPoints;
                content = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill);
            }
            List<Point> killedPoints = targetGroup.Where(q => board[q] == Content.Empty || board[q] == content).ToList();
            if (killedPoints.Count > 0 && killedPoints.Count == targetGroup.Count)
                return ConfirmAliveResult.Dead;
            return ConfirmAliveResult.Alive;
        }

        /// <summary>
        /// Includes target survived and target killed flags to prompt user.
        /// </summary>
        public static ConfirmAliveResult CheckIfTargetSurvivedOrKilled(ConfirmAliveResult result, SurviveOrKill surviveOrKill, Game g)
        {
            ConfirmAliveResult confirmAlive = CheckIfDeadOrAlive(surviveOrKill, g);
            if (confirmAlive == ConfirmAliveResult.Alive)
                result |= ConfirmAliveResult.TargetSurvived;
            else if (confirmAlive == ConfirmAliveResult.Dead)
                result |= ConfirmAliveResult.TargetKilled;
            return result;
        }


        /// <summary>
        /// Check if target group is dead or alive, including survival points check. Ignore ko check to return only dead or alive regardless of ko.
        /// </summary>
        public static ConfirmAliveResult CheckIfDeadOrAlive(SurviveOrKill surviveOrKill, Game g, Boolean ignoreKoCheck = false)
        {
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            //check for survival points
            Boolean survivalPointsKilled = (g.Board.CapturedPoints.Intersect(g.GameInfo.survivalPoints).Any());
            if (survivalPointsKilled)
                return (surviveOrKill == SurviveOrKill.Survive) ? ConfirmAliveResult.Alive : ConfirmAliveResult.Dead;

            if (surviveOrKill == SurviveOrKill.Survive)
            {
                //check confirm alive
                confirmAlive = LifeCheck.ConfirmAlive(g.Board);
                if (confirmAlive == ConfirmAliveResult.Alive)
                {
                    //check for ko alive
                    if (ignoreKoCheck || g.KoGameCheck == KoCheck.None || g.KoGameCheck == KoCheck.Kill)
                        return confirmAlive;
                    else
                        return ConfirmAliveResult.KoAlive;
                }
            }
            else
            {
                //check if target group killed
                confirmAlive = LifeCheck.CheckIfTargetGroupKilled(g.Board);
                if (confirmAlive == ConfirmAliveResult.Dead)
                {
                    //check for ko alive
                    if (ignoreKoCheck || g.KoGameCheck == KoCheck.None || g.KoGameCheck == KoCheck.Survive)
                        return confirmAlive;
                    else
                        return ConfirmAliveResult.KoAlive;
                }
            }
            return ConfirmAliveResult.Unknown;
        }
    }
}

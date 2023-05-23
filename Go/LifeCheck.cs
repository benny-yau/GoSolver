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
        public static ConfirmAliveResult ConfirmAlive(Board board, List<Point> targetPoints = null)
        {
            List<Group> targets = LifeCheck.GetTargets(board, targetPoints);
            if (targets.Any(p => ConfirmAlive(board, p) == ConfirmAliveResult.Alive))
                return ConfirmAliveResult.Alive;
            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Confirm alive.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16860" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A28" />
        /// </summary>
        public static ConfirmAliveResult ConfirmAlive(Board board, Group targetGroup)
        {
            List<Group> eyes = new List<Group>();
            List<LinkedPoint<Point>> tigerMouthList = new List<LinkedPoint<Point>>();

            Content c = targetGroup.Content;
            //ensure at least two liberties
            if (targetGroup.Liberties.Count == 1) return ConfirmAliveResult.Unknown;

            //get at least two possible eyes
            List<Group> killerGroups = GetTwoPossibleEyes(board, targetGroup);
            if (killerGroups == null) return ConfirmAliveResult.Unknown;

            //check for real eyes
            for (int i = 0; i <= killerGroups.Count - 1; i++)
            {
                Group group = killerGroups[i];
                if (EyeHelper.FindRealEyeOfAnyKillerGroup(board, group))
                    eyes.Add(group);
                //get tiger mouths of eye groups
                GetTigerMouthsOfEyeGroups(board, group, tigerMouthList);
                if (eyes.Count + killerGroups.Count - 1 - i < 2)
                    break;
            }
            if (eyes.Count < 2) return ConfirmAliveResult.Unknown;
            //check for tiger mouth exception
            if (CheckTigerMouthExceptions(board, tigerMouthList.Select(t => t.Move), c, true)) return ConfirmAliveResult.Unknown;

            //two real eyes to confirm alive
            if (eyes.Count >= 2)
                return ConfirmAliveResult.Alive;
            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Get two possible eyes.
        /// </summary>
        public static List<Group> GetTwoPossibleEyes(Board board, Group targetGroup)
        {
            Content c = targetGroup.Content;
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board, c).OrderBy(gr => gr.Points.Count).ToList();
            if (killerGroups.Count < 2) return null;
            //get extended groups from target group
            HashSet<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(board, targetGroup);
            //ensure group is connected to target
            killerGroups.RemoveAll(e => !board.GetNeighbourGroups(e).All(n => groups.Contains(n)));
            if (killerGroups.Count < 2) return null;
            return killerGroups;
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
                //possible corner three formation
                if (lifeCheck && KillerFormationHelper.PossibleCornerThreeFormation(board, tigerMouth, c))
                    return true;

                if (CommonTigerMouthExceptions(board, c, tigerMouth, libertyPoint.Value, lifeCheck))
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
        /// Check for link breakage <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_2" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_Nie60_2" /> 
        /// Check for another tiger mouth at move <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150_10" />
        /// Check double tiger mouth at move <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571" />
        /// <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_WindAndTime_Q30150" />
        /// Double ko break <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_y" />
        /// </summary>
        public static Boolean CommonTigerMouthExceptions(Board board, Content c, Point tigerMouth, Point libertyPoint, Boolean lifeCheck = false)
        {
            //double ko break
            if (LinkHelper.DoubleKoBreak(board, tigerMouth, c))
                return true;

            //make move at liberty
            Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite(), true);
            if (b == null || b.MoveGroupLiberties == 1) return false;

            //check negligible for links
            HashSet<Group> tmGroups = b.GetGroupsFromStoneNeighbours(tigerMouth, c.Opposite());
            if (LinkHelper.CheckNegligibleForLinks(b, board, t => !tmGroups.Contains(t)))
                return true;

            //check for tiger mouth threat group
            Group threatGroup = LinkHelper.TigerMouthThreatGroup(board, tigerMouth, c);
            if (threatGroup != null)
            {
                if (AtariHelper.AtariByGroup(board, threatGroup)) return true;
                if (LinkHelper.LinkWithThreatGroup(b, board, s => s == threatGroup))
                    return true;
            }

            //check for ko break
            if (LinkHelper.CheckForKoBreak(b))
                return true;

            //check for link breakage
            if (LinkHelper.LinkBreakage(b, c))
            {
                if (b.MoveGroupLiberties > 2 || CheckThreatGroupEscape(b, tigerMouth, new List<Point>() { b.Move.Value }))
                    return true;
            }

            //check for another tiger mouth at move
            List<Point> tigerMouths = b.GetStoneNeighbours().Where(n => !n.Equals(tigerMouth) && LinkHelper.IsTigerMouthForLink(board, n, c, !lifeCheck)).ToList();
            if (tigerMouths.Any())
            {
                if (b.MoveGroupLiberties > 3 || CheckThreatGroupEscape(b, tigerMouth, tigerMouths))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check threat group escape.
        /// </summary>
        public static Boolean CheckThreatGroupEscape(Board b, Point tigerMouth, List<Point> targetPoints = null)
        {
            Content c = b.MoveGroup.Content;

            //fill tiger mouth
            Board b2 = b.MakeMoveOnNewBoard(tigerMouth, c.Opposite(), true);
            if (b2 == null || ImmovableHelper.CheckConnectAndDie(b2)) return true;

            //check non killable
            if (targetPoints != null && targetPoints.All(n => b2.GetGroupsFromStoneNeighbours(n, c).All(s => WallHelper.IsNonKillableGroup(b2, s))))
                return false;

            //make second move
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(b2, b2.GetGroupLiberties(b.MoveGroup), c);
            if (moveBoards.Any(n => n.MoveGroupLiberties > 1 || !GameTryMove.IsNegligibleForBoard(n, b2)))
                return true;

            //check double atari
            List<Group> ngroups = b2.GetNeighbourGroups(b.MoveGroup);
            if (ngroups.Any(n => n.Liberties.Count == 1) || LinkHelper.DoubleAtariOnTargetGroups(b2, ngroups))
                return true;

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
                if (LinkHelper.IsTigerMouthForLink(board, p.Move, c.Opposite(), false))
                    tigerMouthList.Add(p);
            }
        }

        /// <summary>
        /// Get targets of survival group. Can specify other targets than specified in game info.
        /// </summary>
        public static List<Group> GetTargets(Board board, List<Point> target = null)
        {
            Content content = Content.Unknown;
            if (target == null)
            {
                //get target from game info
                target = board.GameInfo.targetPoints;
                content = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
                return target.Where(t => board[t] == content).Select(t => board.GetGroupAt(t)).Distinct().ToList(); //get the target that is still alive
            }
            else //can specify another target
            {
                return target.Where(t => board[t] != Content.Empty).Select(t => board.GetGroupAt(t)).Distinct().ToList();
            }
        }

        /// <summary>
        /// Check if all target points are killed.
        /// </summary>
        public static ConfirmAliveResult CheckIfTargetGroupKilled(Board board)
        {
            GameInfo gameInfo = board.GameInfo;
            List<Point> targetGroup = gameInfo.targetPoints;
            Content content = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Survive);
            List<Point> killedPoints = targetGroup.Where(q => board[q] != content).ToList();
            if (killedPoints.Count > 0 && killedPoints.Count == targetGroup.Count)
                return ConfirmAliveResult.Dead;
            return ConfirmAliveResult.Unknown;
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
        /// Check if target group is dead or alive, including survival points check.
        /// </summary>
        public static ConfirmAliveResult CheckIfDeadOrAlive(SurviveOrKill surviveOrKill, Game g)
        {
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            //check for survival points
            Boolean survivalPointsKilled = g.Board.CapturedPoints.Intersect(g.GameInfo.survivalPoints).Any();
            if (survivalPointsKilled)
                return (surviveOrKill == SurviveOrKill.Survive) ? ConfirmAliveResult.Alive : ConfirmAliveResult.Dead;

            //check target dead or alive
            if (surviveOrKill == SurviveOrKill.Survive)
                confirmAlive = LifeCheck.ConfirmAlive(g.Board);
            else
                confirmAlive = LifeCheck.CheckIfTargetGroupKilled(g.Board);
            return confirmAlive;
        }
    }
}

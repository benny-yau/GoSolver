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
            Content c = targetGroup.Content;
            List<Group> eyes = new List<Group>();
            List<LinkedPoint<Point>> tigerMouthList = new List<LinkedPoint<Point>>();

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
            if (eyes.Count < 2)
                return ConfirmAliveResult.Unknown;
            //check for tiger mouth exception
            if (CheckTigerMouthExceptions(board, tigerMouthList.Select(t => t.Move), c))
                return ConfirmAliveResult.Unknown;

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
            List<Group> killerGroups = GroupHelper.GetKillerGroups(board, c).ToList();
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
        /// </summary>
        public static Boolean CheckTigerMouthExceptions(Board board, IEnumerable<Point> tigerMouthList, Content c)
        {
            foreach (Point tigerMouth in tigerMouthList)
            {
                Point? libertyPoint = ImmovableHelper.FindTigerMouth(board, tigerMouth, c);
                if (libertyPoint == null || board[libertyPoint.Value] != Content.Empty) continue;
                if (CommonTigerMouthExceptions(board, c, tigerMouth, libertyPoint.Value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Common tiger mouth exceptions.
        /// </summary>
        public static Boolean CommonTigerMouthExceptions(Board board, Content c, Point tigerMouth, Point libertyPoint)
        {
            //make move at liberty point
            Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite(), true);
            if (ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false))
            {
                if (!KillerFormationHelper.PossibleCornerThreeFormation(board, tigerMouth, c))
                    return false;
            }

            //check negligible for links
            HashSet<Group> tmGroups = b.GetGroupsFromStoneNeighbours(tigerMouth, c.Opposite());
            if (LinkHelper.CheckNegligibleForLinks(b, board, t => !tmGroups.Contains(t)))
                return true;

            //check for tiger mouth threat group
            Group threatGroup = LinkHelper.TigerMouthThreatGroup(board, tigerMouth, c);
            if (threatGroup != null && LinkHelper.LinkWithImmovableGroup(b, board, s => s == threatGroup))
                return true;

            //check for link breakage
            if (LinkHelper.LinkBreakage(b))
                return true;

            //check for another tiger mouth at move
            List<Point> tigerMouths = LinkHelper.MoveAtTigerMouth(b, board).Where(n => !n.Equals(tigerMouth)).ToList();
            if (tigerMouths.Any() && CheckThreatGroupEscape(b, tigerMouth, tigerMouths))
                return true;

            //check for ko break
            if (LinkHelper.CheckForKoBreak(b))
                return true;

            //double ko break
            if (LinkHelper.DoubleKoBreak(board, tigerMouth, c))
                return true;

            return false;
        }

        /// <summary>
        /// Check threat group escape.
        /// </summary>
        public static Boolean CheckThreatGroupEscape(Board board, Point tigerMouth, List<Point> targetPoints)
        {
            Point move = board.Move.Value;
            Content c = board.MoveGroup.Content;
            if (board.MoveGroupLiberties > 3) return true;
            //fill tiger mouth
            Board b = board.MakeMoveOnNewBoard(tigerMouth, c.Opposite(), true);

            //check non killable
            if (targetPoints.All(n => b.GetGroupsFromStoneNeighbours(n, c).All(s => WallHelper.IsNonKillableGroup(b, s))))
                return false;

            Group moveGroup = b.GetCurrentGroup(board.MoveGroup);
            if (moveGroup.Liberties.Count == 2 && moveGroup.Liberties.All(n => ImmovableHelper.IsSuicidalMove(b, n, c)))
                return false;
            return true;
        }

        /// <summary>
        /// Get tiger mouth of eye groups.
        /// </summary>
        private static void GetTigerMouthsOfEyeGroups(Board board, Group eye, List<LinkedPoint<Point>> tigerMouthList)
        {
            Content c = eye.Content;
            List<LinkedPoint<Point>> diagonalPoints = LinkHelper.GetGroupDiagonals(board, eye);
            foreach (LinkedPoint<Point> p in diagonalPoints)
            {
                if (ImmovableHelper.FindTigerMouthForLink(board, p.Move, c.Opposite()))
                    tigerMouthList.Add(p);
            }
        }

        /// <summary>
        /// Get targets of survival group. Can specify other targets than specified in game info.
        /// </summary>
        public static List<Group> GetTargets(Board board, List<Point> target = null)
        {
            Content c = Content.Unknown;
            if (target == null)
            {
                //get target from game info
                target = board.GameInfo.targetPoints;
                c = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
                return target.Where(t => board[t] == c).Select(t => board.GetGroupAt(t)).Distinct().ToList(); //get the target that is still alive
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
            GameInfo gi = board.GameInfo;
            List<Point> targetGroup = gi.targetPoints;
            Content c = GameHelper.GetContentForSurviveOrKill(gi, SurviveOrKill.Survive);
            List<Point> killedPoints = targetGroup.Where(q => board[q] != c).ToList();
            if (killedPoints.Count > 0 && killedPoints.Count == targetGroup.Count)
                return ConfirmAliveResult.Dead;
            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Includes target survived and target killed flags to prompt user.
        /// </summary>
        public static ConfirmAliveResult CheckIfTargetSurvivedOrKilled(ConfirmAliveResult result, SurviveOrKill surviveOrKill, Board board)
        {
            ConfirmAliveResult confirmAlive = CheckIfDeadOrAlive(surviveOrKill, board);
            if (confirmAlive == ConfirmAliveResult.Alive)
                result |= ConfirmAliveResult.TargetSurvived;
            else if (confirmAlive == ConfirmAliveResult.Dead)
                result |= ConfirmAliveResult.TargetKilled;
            return result;
        }


        /// <summary>
        /// Check if target group is dead or alive, including survival points check.
        /// </summary>
        public static ConfirmAliveResult CheckIfDeadOrAlive(SurviveOrKill surviveOrKill, Board board)
        {
            ConfirmAliveResult confirmAlive = ConfirmAliveResult.Unknown;
            //check for survival points
            if (board.CapturedPoints.Intersect(board.GameInfo.survivalPoints).Any())
                return (surviveOrKill == SurviveOrKill.Survive) ? ConfirmAliveResult.Alive : ConfirmAliveResult.Dead;

            //check target dead or alive
            if (surviveOrKill == SurviveOrKill.Survive)
                confirmAlive = LifeCheck.ConfirmAlive(board);
            else
                confirmAlive = LifeCheck.CheckIfTargetGroupKilled(board);
            return confirmAlive;
        }
    }
}

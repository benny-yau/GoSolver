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
        public static ConfirmAliveResult ConfirmAlive(Board board)
        {
            List<Group> targets = LifeCheck.GetTargets(board);
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
            List<Link<Point>> tigerMouthList = new List<Link<Point>>();

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
                if (eyes.Count + killerGroups.Count - i - 1 < 2)
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
            (Boolean connectAndDie, Board b) = ImmovableHelper.ConnectAndDieMove(board, libertyPoint, c.Opposite(), false, false);
            if (b == null) return false;
            if (connectAndDie)
            {
                if (!KillerFormationHelper.PossibleCornerThreeFormation(board, tigerMouth, c))
                    return false;
            }

            //check negligible for links
            List<Group> tmGroups = b.GetGroupsFromStoneNeighbours(tigerMouth, c.Opposite());
            if (LinkHelper.CheckNegligibleForLinks(b, board, t => !tmGroups.Contains(t)))
                return true;

            //check for tiger mouth threat group
            Group threatGroup = LinkHelper.TigerMouthThreatGroup(board, tigerMouth, c);
            if (threatGroup != null && LinkHelper.LinkWithImmovableGroup(b, board, s => s == threatGroup))
                return true;

            //check for link breakage
            if (LinkHelper.LinkBreakage(b, board))
                return true;

            //check for another tiger mouth at move
            List<Point> tigerMouths = LinkHelper.MoveAtTigerMouth(b, board).Where(n => !n.Equals(tigerMouth)).ToList();
            if (tigerMouths.Any() && b.MoveGroupLiberties > 3)
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
        /// Get tiger mouth of eye groups.
        /// </summary>
        private static void GetTigerMouthsOfEyeGroups(Board board, Group eye, List<Link<Point>> tigerMouthList)
        {
            Content c = eye.Content;
            foreach (Link<Point> p in LinkHelper.GetGroupDiagonals(board, eye))
            {
                if (ImmovableHelper.FindTigerMouthForLink(board, p.Move, c.Opposite()))
                    tigerMouthList.Add(p);
            }
        }

        /// <summary>
        /// Get targets.
        /// </summary>
        public static List<Group> GetTargets(Board board)
        {
            Content c = Content.Unknown;
            List<Point> target = board.GameInfo.targetPoints;
            c = GameHelper.GetContentForSurviveOrKill(board.GameInfo, SurviveOrKill.Survive);
            return target.Where(t => board[t] == c).Select(t => board.GetGroupAt(t)).Distinct().ToList();
        }

        /// <summary>
        /// Check if target group killed.
        /// </summary>
        public static ConfirmAliveResult CheckIfTargetGroupKilled(Board board)
        {
            GameInfo gi = board.GameInfo;
            Content c = GameHelper.GetContentForSurviveOrKill(gi, SurviveOrKill.Survive);
            //check all targets killed
            List<Point> killedPoints = gi.targetPoints.Where(q => board[q] != c).ToList();
            if (killedPoints.Count > 0 && killedPoints.Count == gi.targetPoints.Count)
                return ConfirmAliveResult.Dead;
            //check for unescapable group
            if (LifeCheck.GetTargets(board).All(n => ImmovableHelper.UnescapableGroup(board, n).Item1))
                return ConfirmAliveResult.Dead;
            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Check if target survived or killed.
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
        /// Check if dead or alive.
        /// </summary>
        public static ConfirmAliveResult CheckIfDeadOrAlive(SurviveOrKill surviveOrKill, Board board, Boolean? checkSurvival = null)
        {
            //check for survival points
            if (CheckForSurvivalPoints(surviveOrKill, board))
                return (surviveOrKill == SurviveOrKill.Survive) ? ConfirmAliveResult.Alive : ConfirmAliveResult.Dead;

            //check for kill
            if (surviveOrKill == SurviveOrKill.Kill)
                return LifeCheck.CheckIfTargetGroupKilled(board);

            //check for survival
            if (surviveOrKill != SurviveOrKill.Survive) return ConfirmAliveResult.Unknown;

            //check last move valid
            if (!CheckLastMoveValid(board, checkSurvival)) return ConfirmAliveResult.Unknown;

            //check target alive
            if (LifeCheck.ConfirmAlive(board) == ConfirmAliveResult.Alive)
                return ConfirmAliveResult.Alive;

            //check external link
            if (board.GameInfo.survivalLinkPoints.Any(n => LinkHelper.IsExternalLinkToTargetGroup(board, n)))
                return ConfirmAliveResult.Alive;

            if (UniquePatternsHelper.CheckForTenThousandYearKo(board))
                return ConfirmAliveResult.Alive;

            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Check for survival points.
        /// </summary>
        public static Boolean CheckForSurvivalPoints(SurviveOrKill surviveOrKill, Board board)
        {
            Content c = GameHelper.GetContentForSurviveOrKill(board.GameInfo, surviveOrKill);
            List<Point> survivalPoints = board.GameInfo.survivalPoints;
            //check if captured
            if (board.CapturedPoints.Intersect(survivalPoints).Any())
                return true;
            //check for unescapable group
            HashSet<Group> groups = board.GetGroupsFromPoints(survivalPoints.Where(n => board[n] == c.Opposite()).ToList());
            if (groups.Any(n => ImmovableHelper.UnescapableGroup(board, n).Item1))
                return true;
            return false;
        }

        /// <summary>
        /// Check last move valid.
        /// </summary>
        public static Boolean CheckLastMoveValid(Board board, Boolean? checkSurvival = null)
        {
            if (checkSurvival == null)
            {
                if (!board.LastMoves.Any()) return true;
                return !ImmovableHelper.CheckConnectAndDie(board, board.MoveGroup, false);
            }
            return checkSurvival.Value;
        }
    }
}

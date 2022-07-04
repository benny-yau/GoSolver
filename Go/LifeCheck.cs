using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class LifeCheck
    {
        /// <summary>
        /// Confirm if result is alive by surrounding target group and trying to kill by all possible means.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_ScenarioTestConfirmAlive1" />
        /// Partial alive <see cref="UnitTestProject.PartiallyAliveTest.PartiallyAliveTest_Scenario_WindAndTime_Q30215" />
        /// </summary>
        public static ConfirmAliveResult ConfirmAlive(Board board, List<Point> target = null)
        {
            target = LifeCheck.GetTargets(board, target);
            if (target == null || target.Count == 0) return ConfirmAliveResult.Unknown;

            if (target.Count > 1) //more than one target point (allow partial alive)
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
        /// Extended life check predetermines target group is alive if two semi-solid eyes found, so the target group can be confirmed alive earlier than if the preliminary life check is used only.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16860" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A28" />
        /// </summary>
        public static ConfirmAliveResult ConfirmAlive(Board board, Point target)
        {
            List<Group> eyeGroups = new List<Group>();
            List<LinkedPoint<Point>> tigerMouthList = new List<LinkedPoint<Point>>();

            Content c = board[target];
            //ensure at least two liberties
            Group targetGroup = board.GetGroupAt(target);
            if (targetGroup.Liberties.Count == 1) return ConfirmAliveResult.Unknown;

            //get at least two possible eyes
            List<Group> killerGroups = BothAliveHelper.GetCorneredKillerGroup(board, c, false);
            if (killerGroups.Count < 2) return ConfirmAliveResult.Unknown;
            //get extended groups from target group
            HashSet<Group> groups = LinkHelper.GetAllDiagonalConnectedGroups(board, targetGroup);
            //get eye groups not more than three points
            List<Group> possibleEyes = killerGroups.Where(s => s.Points.Count <= 3).ToList();
            //ensure group is connected to target
            possibleEyes.RemoveAll(e => !board.GetNeighbourGroups(e).All(n => groups.Contains(n)));
            if (possibleEyes.Count < 2) return ConfirmAliveResult.Unknown;

            for (int i = 0; i <= possibleEyes.Count - 1; i++)
            {
                Group group = possibleEyes[i];
                if (group.Points.Count > 1)
                {
                    //check for semi solid eye within group
                    if (EyeHelper.FindRealEyeWithinEmptySpace(board, group, EyeType.SemiSolidEye))
                    {
                        //remove preatari groups
                        if (CheckForPreAtariGroups(board, group)) continue;

                        eyeGroups.Add(group);

                        //get tiger mouths for two=point group to check for exception
                        GetTigerMouthsInTwoPointGroup(board, group, tigerMouthList);
                    }
                }
                else
                {
                    //check if semi solid eyes for single point eye
                    Point p = group.Points.First();
                    (Boolean found, _, List<LinkedPoint<Point>> tigerMouths) = EyeHelper.FindSemiSolidEyes(p, board, c);
                    if (found)
                    {
                        eyeGroups.Add(group);
                        //get all tiger mouths to check for exception
                        if (!EyeHelper.FindRealSolidEyes(p, c, board))
                            tigerMouthList.AddRange(tigerMouths);
                    }
                }
                if (eyeGroups.Count + possibleEyes.Count - 1 - i < 2)
                    break;
            }
            //check for exception case scenario
            CheckTigerMouthForException(board, tigerMouthList, eyeGroups, c);
            CheckOpponentAtariMoves(board, eyeGroups, tigerMouthList);

            //at least two semi solid eyes to predetermine target group is alive
            if (eyeGroups.Count >= 2)
                return ConfirmAliveResult.Alive;

            return ConfirmAliveResult.Unknown;
        }

        /// <summary>
        /// Pre atari groups.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_3" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60_4" />
        /// </summary>
        private static Boolean CheckForPreAtariGroups(Board board, Group group)
        {
            //group of three empty points with three or more neighbour groups and bent three formation
            if (group.Points.Count == 3 && group.Points.All(p => board[p] == Content.Empty) && board.GetNeighbourGroups(group).Count >= 3 && KillerFormationHelper.MaxLengthOfGrid(group.Points) == 1)
            {
                Content c = group.Content;
                Board coveredBoard = new Board(board);
                List<Group> neighbourGroups = board.GetNeighbourGroups(group);
                //cover external liberties
                neighbourGroups.ForEach(gr => gr.Liberties.Where(p => BothAliveHelper.GetKillerGroupFromCache(board, p, c.Opposite()) == null).ToList().ForEach(p => coveredBoard[p] = c));
                //get middle point of group
                Point q = group.Points.First(s => board.GetStoneNeighbours(s).Intersect(group.Points).Count() == 2);
                //make move and check for unescapable group
                Board b = coveredBoard.MakeMoveOnNewBoard(q, c);
                if (b != null && b.AtariTargets.Count > 0 && b.AtariTargets.Any(t => ImmovableHelper.UnescapableGroup(b, t).Item1))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check exceptions to confirm alive at tiger mouths for semi solid eye.
        /// Tiger mouth escape with atari <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150" />
        /// Double tiger mouth <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_XuanXuanGo_B3" />
        /// </summary>
        private static void CheckTigerMouthForException(Board board, List<LinkedPoint<Point>> tigerMouthList, List<Group> eyeGroups, Content c)
        {
            if (eyeGroups.Count < 2) return;

            HashSet<Point> libertyPoints = new HashSet<Point>();
            foreach (LinkedPoint<Point> tigerMouth in tigerMouthList)
            {
                if (board[tigerMouth.Move] != Content.Empty) continue;
                Point? libertyPoint = ImmovableHelper.FindTigerMouth(board, tigerMouth.Move, c);
                if (libertyPoint == null) continue;

                //ensure tiger mouth is external
                Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(board, libertyPoint.Value, c);
                if (killerGroup != null) continue;

                //check for double tiger mouth
                if (libertyPoints.Contains(libertyPoint.Value))
                {
                    eyeGroups.RemoveAll(group => group.Points.Contains((Point)tigerMouth.CheckMove));
                    if (eyeGroups.Count < 2) return;
                }
                else libertyPoints.Add(libertyPoint.Value);

                //check for other exceptions
                if (TigerMouthExceptions(board, c, tigerMouth, libertyPoint.Value))
                {
                    eyeGroups.RemoveAll(group => group.Points.Contains((Point)tigerMouth.CheckMove));
                    if (eyeGroups.Count < 2) return;
                }
            }
        }

        /// <summary>
        /// Atari, resolve atari, and captured at tiger mouth liberty move.
        /// Possible corner three formation
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Corner_A139_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31503" />
        /// Suicidal at tiger mouth  <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WuQingYuan_Q31177" />
        /// Check for atari that is not tiger mouth  <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_WindAndTime_Q30150_2" />
        /// </summary>
        public static Boolean TigerMouthExceptions(Board board, Content c, LinkedPoint<Point> tigerMouth, Point libertyPoint)
        {
            if (board[tigerMouth.Move] != Content.Empty || board[libertyPoint] != Content.Empty) return false;
            //possible corner three formation
            if (KillerFormationHelper.PossibleCornerThreeFormation(board, tigerMouth.Move, c))
                return true;
            //suicidal at tiger mouth
            if (board.GetGroupsFromStoneNeighbours(tigerMouth.Move, c.Opposite()).All(n => n.Liberties.Count <= 2) && ImmovableHelper.IsSuicidalMove(board, tigerMouth.Move, c))
                return true;

            (Boolean suicidal, Board b1) = ImmovableHelper.IsSuicidalMove(libertyPoint, c.Opposite(), board);
            if (suicidal) return false;
            //check for atari that is not tiger mouth  
            if (b1.AtariTargets.Count() > 0 || b1.CapturedList.Count > 0)
                return true;
            Board.ResolveAtari(board, b1);
            if (b1.AtariResolved) return true;

            //check liberty point
            if (CheckLibertyPointOfTigerMouths(board, c, tigerMouth.Move, libertyPoint))
                return true;

            //check if another tiger mouth present at diagonal neighbours
            return DoubleTigerMouthLink(board, c, tigerMouth.Move, libertyPoint);
        }

        /// <summary>
        /// Check liberty point of tiger mouth not link for groups
        /// to ensure not liberty point of another tiger mouth.
        /// </summary>
        private static Boolean CheckLibertyPointOfTigerMouths(Board board, Content c, Point tigerMouth, Point libertyPoint)
        {
            if (!board.GetStoneNeighbours(tigerMouth).Any(n => board[n] == c.Opposite())) return false;
            if (BothAliveHelper.GetKillerGroupFromCache(board, libertyPoint, c) != null) return false;
            //escape at liberty point
            Board b = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite(), true);
            if (b == null) return false;
            //join with another group with two liberties
            List<Group> groups = LinkHelper.GetPreviousMoveGroup(board, b);
            if (groups.Count == 1) return false;
            return (groups.Count(group => group.Liberties.Count == 2) >= 2);
        }

        /// <summary>
        /// Double tiger mouth one of which is link for groups.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571" />
        /// Check for three groups <see cref="UnitTestProject.LinkHelperTest.LinkHelperTest_Scenario_TianLongTu_Q16571" />
        /// </summary>
        public static Boolean DoubleTigerMouthLink(Board board, Content c, Point tigerMouth, Point libertyPoint, List<Group> threeGroups = null)
        {
            if (board[tigerMouth] != Content.Empty || board[libertyPoint] != Content.Empty) return false;
            //make killer move at liberty
            Board b1 = board.MakeMoveOnNewBoard(libertyPoint, c.Opposite(), true);
            if (b1 == null || b1.MoveGroupLiberties <= 3) return false;
            //get all stone neigbours of liberty
            List<Point> diagonals = board.GetStoneNeighbours(libertyPoint);
            diagonals.Remove(tigerMouth);
            diagonals = diagonals.Where(d => board[d] == Content.Empty && ImmovableHelper.FindTigerMouth(board, c, d)).ToList();
            foreach (Point diagonal in diagonals)
            {
                //ensure tiger mouth is external
                Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(board, diagonal, c);
                if (killerGroup != null) continue;

                //ensure link for groups
                Board b2 = b1.MakeMoveOnNewBoard(diagonal, c);
                if (b2 != null && LinkHelper.IsAbsoluteLinkForGroups(b1, b2))
                {
                    //check for three groups
                    if (threeGroups != null && board.GetGroupsFromStoneNeighbours(diagonal, c.Opposite()).Intersect(threeGroups).Count() != 2) 
                        continue;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get tiger mouth in two-point groups.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_2" />
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_TianLongTu_Q16571_3" />
        /// </summary>
        private static void GetTigerMouthsInTwoPointGroup(Board board, Group group, List<LinkedPoint<Point>> tigerMouthList)
        {
            Content c = group.Content;
            if (group.Points.Count != 2) return;
            foreach (Point p in group.Points)
            {
                Board b = board.MakeMoveOnNewBoard(p, c.Opposite());
                if (b == null) continue;
                Point q = group.Points.First(point => !point.Equals(p));
                (Boolean found, _, List<LinkedPoint<Point>> tigerMouths) = EyeHelper.FindSemiSolidEyes(q, b, c.Opposite());
                if (found && !EyeHelper.FindRealSolidEyes(q, c.Opposite(), b))
                    tigerMouthList.AddRange(tigerMouths);
            }
        }

        
        /// <summary>
        /// Check opponent atari moves.
        /// </summary>
        private static void CheckOpponentAtariMoves(Board board, List<Group> eyeGroups, List<LinkedPoint<Point>> tigerMouthList)
        {
            if (eyeGroups.Count < 2) return;
            //get neighbours of eye groups
            List<Group> targetGroups = new List<Group>();
            eyeGroups.ForEach(eyeGroup => targetGroups.AddRange(board.GetNeighbourGroups(eyeGroup)));

            //get neighbours of tiger mouths
            Content c = eyeGroups.First().Content;
            tigerMouthList.ForEach(tigerMouth => targetGroups.AddRange(board.GetGroupsFromStoneNeighbours(tigerMouth.Move, c)));

            //get distinct liberties of target groups
            List<Point> liberties = board.GetLibertiesOfGroups(targetGroups).Distinct().ToList();

            foreach (Point liberty in liberties)
            {
                //get external liberty
                Group killerGroup = BothAliveHelper.GetKillerGroupFromCache(board, liberty, c.Opposite());
                if (killerGroup != null) continue;
                //make atari move
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c, board);
                if (suicidal) continue;
                if (b.AtariTargets.Count == 0) continue;
                //ensure all atari targets are connected groups
                if (b.AtariTargets.Any(a => targetGroups.Any(t => t.Points.Contains(a.Points.First()))))
                {
                    //check for double atari
                    if (DoubleAtariCapture(b))
                    {
                        eyeGroups.Clear();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Double atari by opponent breaks eye.
        /// <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_Nie60" />
        /// </summary>
        public static Boolean DoubleAtariCapture(Board board)
        {
            //more than one atari target
            if (board.AtariTargets.Count <= 1) return false;
            foreach (Group targetGroup in board.AtariTargets)
            {
                //get liberty point for target group
                (Boolean unEscapable, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(board, targetGroup);
                if (unEscapable) continue;
                //check if any atari targets left
                if (!board.AtariTargets.Any(t => escapeBoard.GetGroupLiberties(t.Points.First()) == 1))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get targets of survival group. Can specify other targets than specified in game info.
        /// Specify another target <see cref="UnitTestProject.LifeCheckTest.LifeCheckTest_Scenario_GuanZiPu_B3" />
        /// </summary>
        public static List<Point> GetTargets(Board board, List<Point> target)
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
                target = LifeCheck.GetTargets(board, board.GameInfo.targetPoints);
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

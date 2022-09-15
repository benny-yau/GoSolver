using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class AtariHelper
    {
        /// <summary>
        /// Find target groups where the neighbour group of the move is reduced to liberty of one only.
        /// Resolved atari is where the group of the move has increased liberty from one.
        /// <see cref="UnitTestProject.FindAndResolveAtariTest.FindAndResolveAtariTest_Scenario_XuanXuanGo_Q18358" />
        /// </summary>
        public static void FindAndResolveAtari(GameTryMove tryMove)
        {
            Board currentBoard = tryMove.CurrentGame.Board;
            Board tryBoard = tryMove.TryGame.Board;
            //find atari
            Board.FindAtari(tryBoard);
            //resolve atari
            Board.ResolveAtari(currentBoard, tryBoard);
        }

        /// <summary>
        /// Check if any atari on neighbour groups, including ko. 
        /// Check for ko <see cref="UnitTestProject.SuicidalRedundantMoveTest.SuicidalRedundantMoveTest_Scenario_WindAndTime_Q30315" />
        /// </summary>
        public static Boolean AtariByGroup(Board board, Group atariGroup, Boolean excludeKo = true)
        {
            return AtariByGroup(atariGroup, board, excludeKo).Any();
        }

        /// <summary>
        /// Atari by group.
        /// Ensure only one ko fight group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A85" />
        /// Check for ko liberty <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A85_2" />
        /// </summary>
        public static List<Group> AtariByGroup(Group atariGroup, Board board, Boolean excludeKo = true)
        {
            Content c = atariGroup.Content;
            List<Group> targetGroups = board.GetNeighbourGroups(atariGroup).Where(gr => gr.Liberties.Count == 1).ToList();
            if (excludeKo)
                return targetGroups;
            //check for ko
            List<Group> koFightGroups = targetGroups.Where(n => KoHelper.IsKoFight(board, n)).ToList();

            for (int i = koFightGroups.Count - 1; i >= 0; i--)
            {
                Group group = koFightGroups[i];
                Board b = ImmovableHelper.CaptureSuicideGroup(board, group);
                if (b == null)
                {
                    targetGroups.Remove(group);
                    continue;
                }
                //ensure only one ko fight group
                if (koFightGroups.Count == 1 && !KoHelper.KoContentEnabled(c, board.GameInfo))
                {
                    //check for ko liberty
                    Boolean koLibertyFound = atariGroup.Liberties.Any(n => !n.Equals(group.Points.First()) && KoHelper.IsKoFight(b, n, c).Item1);
                    if (!koLibertyFound)
                        targetGroups.Remove(group);
                }
            }
            return targetGroups;
        }

        /// <summary>
        /// Double atari on target groups.
        /// </summary>
        public static Boolean DoubleAtariOnTargetGroups(Board board, List<Group> targetGroups)
        {
            if (targetGroups.Count == 0) return false;
            Content c = targetGroups.First().Content;
            //get distinct liberties of target groups
            List<Point> liberties = board.GetLibertiesOfGroups(targetGroups.Where(t => t.Liberties.Count == 2).ToList()).Distinct().ToList();

            foreach (Point liberty in liberties)
            {
                //make atari move
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, c.Opposite(), board, false);
                if (suicidal) continue;
                Boolean atariOnTargetGroup = b.AtariTargets.Any(a => targetGroups.Any(t => t.Equals(board.GetGroupAt(a.Points.First()))));
                if (!atariOnTargetGroup) continue;
                if (board.CapturedList.Count > 0) return true;
                //double atari with any target group
                if (b.AtariTargets.Count >= 2)
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

    }
}

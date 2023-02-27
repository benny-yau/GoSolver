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
        public static Boolean AtariByGroup(Board board, Group atariGroup, Boolean koEnabled = true)
        {
            return AtariByGroup(atariGroup, board, koEnabled).Any();
        }

        /// <summary>
        /// Atari by group.
        /// Ensure only one ko fight group <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A85" />
        /// Check for ko liberty <see cref="UnitTestProject.ImmovableTest.ImmovableTest_Scenario_Corner_A85_2" />
        /// </summary>
        public static List<Group> AtariByGroup(Group atariGroup, Board board, Boolean koEnabled = true)
        {
            Content c = atariGroup.Content;
            List<Group> targetGroups = board.GetNeighbourGroups(atariGroup).Where(gr => gr.Liberties.Count == 1).ToList();
            if (koEnabled)
                return targetGroups;
            //check for ko
            targetGroups.RemoveAll(t => KoHelper.IsKoFight(board, t));
            return targetGroups;
        }

        /// <summary>
        /// Double atari on target groups.
        /// </summary>
        public static Boolean DoubleAtariOnTargetGroups(Board board, List<Group> targetGroups)
        {
            if (targetGroups.Count == 0) return false;
            Content c = targetGroups.First().Content;
            //get groups with two liberties only
            targetGroups = targetGroups.Where(t => t.Liberties.Count == 2).ToList();
            //get distinct liberties of target groups
            List<Point> liberties = board.GetLibertiesOfGroups(targetGroups).Distinct().ToList();

            //double atari
            IEnumerable<Board> moveBoards = GameHelper.GetMoveBoards(board, liberties, c.Opposite());
            if (moveBoards.Any(b => DoubleAtariWithoutEscape(b)))
                return true;
            return false;
        }

        /// <summary>
        /// Double atari without escape.
        /// </summary>
        public static Boolean DoubleAtariWithoutEscape(Board board)
        {
            if (board.AtariTargets.Count <= 1) return false;
            foreach (Group targetGroup in board.AtariTargets)
            {
                //make escape move for target group
                (Boolean unEscapable, _, Board escapeBoard) = ImmovableHelper.UnescapableGroup(board, targetGroup);
                if (unEscapable) continue;
                //check if any atari targets left
                if (board.AtariTargets.Any(t => escapeBoard.GetGroupLiberties(t).Count == 1))
                    return true;
            }
            return false;
        }

    }
}

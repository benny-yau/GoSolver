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
        /// Find and resolve atari.
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
            if (moveBoards.Any(b => DoubleAtariWithoutEscape(b) || b.CapturedList.Count > 0 || Board.ResolveAtari(board, b)))
                return true;
            return false;
        }

        /// <summary>
        /// Double atari without escape.
        /// </summary>
        public static Boolean DoubleAtariWithoutEscape(Board board)
        {
            if (board.AtariTargets.Count <= 1) return false;
            if (ImmovableHelper.IsSuicidalWithoutKo(board)) return false;

            //check if both targets escapable
            foreach (Group targetGroup in board.AtariTargets)
            {
                //check escape by capture
                Board captureBoard = ImmovableHelper.EscapeByCapture(board, targetGroup, false);
                if (captureBoard != null && !board.AtariTargets.Any(t => captureBoard.GetGroupLiberties(t).Count == 1))
                    return false;

                //make move at liberty
                Board escapeBoard = ImmovableHelper.MakeMoveAtLiberty(board, targetGroup, targetGroup.Content);
                if (escapeBoard != null && escapeBoard.MoveGroupLiberties > 1 && !board.AtariTargets.Any(t => escapeBoard.GetGroupLiberties(t).Count == 1))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Is weak neighbour group.
        /// </summary>
        public static Boolean IsWeakNeighbourGroup(Board tryBoard, Group moveGroup)
        {
            if (WallHelper.IsNonKillableFromSetupMoves(tryBoard, moveGroup))
                return false;

            if (tryBoard.GetNeighbourGroups(moveGroup).Any(gr => IsWeakGroup(tryBoard, gr)))
                return true;
            return false;
        }

        /// <summary>
        /// Is weak group.
        /// </summary>
        public static Boolean IsWeakGroup(Board tryBoard, Group group)
        {
            if (group.Liberties.Count > 2) return false;
            foreach (Point liberty in group.Liberties)
            {
                if (WallHelper.IsNonKillableGroup(tryBoard, group)) continue;
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, group.Content.Opposite(), tryBoard, true);
                if (suicidal) continue;
                if (group.Liberties.Count == 2 && (WallHelper.IsNonKillableGroup(b) || KoHelper.IsNonKillableGroupKoFight(b, b.MoveGroup))) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is atari without suicide.
        /// </summary>
        public static Boolean IsAtariWithoutSuicide(Board tryBoard)
        {
            return tryBoard.AtariTargets.Count > 0 && !ImmovableHelper.IsSuicidalWithoutKo(tryBoard);
        }
    }
}

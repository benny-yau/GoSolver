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
        /// Double atari without escape.
        /// </summary>
        public static Boolean DoubleAtariWithoutEscape(Board board)
        {
            if (ImmovableHelper.IsSuicidalWithoutKo(board)) return false;
            if (board.AtariTargets.Count == 0) return false;
            List<Group> groups = board.GetGroupsFromStoneNeighbours().Where(n => n.Liberties.Count >= 2 && n.Liberties.Count <= 3 && ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(board, n)).ToList();
            groups = groups.Union(board.AtariTargets).ToList();
            if (groups.Count < 2) return false;

            //check if atari targets escapable
            foreach (Group targetGroup in board.AtariTargets)
            {
                //check escape by capture
                Board b = ImmovableHelper.EscapeByCapture(board, targetGroup, false);
                if (b != null && !groups.Any(t => ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(b, t)))
                {
                    //check double atari on escape board
                    if (!LinkHelper.DoubleAtariOnTargetGroups(b, groups))
                        return false;
                }
                //make move at liberty
                Board b2 = ImmovableHelper.MakeMoveAtLiberty(board, targetGroup, targetGroup.Content);
                if (b2 != null && !groups.Any(t => ImmovableHelper.TwoAndThreeLibertiesConnectAndDie(b2, t)))
                {
                    //check double atari on escape board
                    if (!LinkHelper.DoubleAtariOnTargetGroups(b2, groups))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Is weak neighbour group.
        /// </summary>
        public static Boolean IsWeakNeighbourGroup(Board tryBoard, Group suicideGroup)
        {
            if (WallHelper.IsNonKillableFromSetupMoves(tryBoard, suicideGroup))
                return false;

            if (tryBoard.GetNeighbourGroups(suicideGroup).Any(ngroup => IsWeakGroup(tryBoard, ngroup)))
                return true;
            return false;
        }

        /// <summary>
        /// Is weak group.
        /// </summary>
        public static Boolean IsWeakGroup(Board tryBoard, Group ngroup)
        {
            if (ngroup.Liberties.Count != 2) return false;
            foreach (Point liberty in ngroup.Liberties)
            {
                (Boolean suicidal, Board b) = ImmovableHelper.IsSuicidalMove(liberty, ngroup.Content.Opposite(), tryBoard, true);
                if (suicidal) continue;
                if (WallHelper.IsNonKillableGroup(b) || KoHelper.IsNonKillableGroupKoFight(b, b.MoveGroup)) continue;
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

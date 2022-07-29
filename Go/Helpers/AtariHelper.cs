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

        public static List<Group> AtariByGroup(Group atariGroup, Board board, Boolean excludeKo = true)
        {
            List<Group> targetGroups = board.GetNeighbourGroups(atariGroup).Where(gr => gr.Liberties.Count == 1).ToList();
            if (excludeKo)
                return targetGroups;
            //check for ko
            for (int i = targetGroups.Count - 1; i >= 0; i--)
            {
                Group group = targetGroups[i];
                if (group.Points.Count != 1 || group.Liberties.Count != 1) continue;
                Point p = group.Points.First();
                if (!board.GetStoneNeighbours(p).Any(n => EyeHelper.FindEye(board, n, group.Content))) continue;
                Board b = ImmovableHelper.CaptureSuicideGroup(board, group);
                if (b == null)
                    targetGroups.Remove(group);
                else if (KoHelper.IsKoFight(b) && !KoHelper.KoContentEnabled(atariGroup.Content, board.GameInfo))
                    targetGroups.Remove(group);
            }
            return targetGroups;
        }

        /// <summary>
        /// Check if any ko atari by neighbour groups. Exclude point for double ko check to find other ko.
        /// </summary>
        public static (Boolean, Board) KoAtariByNeighbour(Board board, List<Group> targetGroups, Point? excludePoint = null)
        {
            foreach (Group group in targetGroups)
            {
                //check for ko
                if (group.Points.Count != 1) continue;
                Point p = group.Points.First();
                if (excludePoint != null && p.Equals(excludePoint)) continue;
                Board b = KoHelper.IsCaptureKoFight(board, group, true);
                if (b == null) continue;
                if (!Board.ResolveAtari(board, b)) continue;
                return (true, b);
            }
            return (false, null);
        }

        public static (Boolean, Board) CheckAtariMove(Board b, Point p, Content c)
        {
            Board board = b.MakeMoveOnNewBoard(p, c);
            if (board == null) return (false, null);
            return (board.IsAtariMove, board);
        }
    }
}

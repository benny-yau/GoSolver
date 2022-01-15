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
        public static Boolean AtariByGroup(Board board, Group atariGroup)
        {
            return AtariByGroup(atariGroup, board).Any();
        }

        public static List<Group> AtariByGroup(Group atariGroup, Board board, Boolean excludeKo = false)
        {
            List<Group> targetGroups = board.GetNeighbourGroups(atariGroup).Where(gr => board.GetGroupLiberties(gr) == 1).ToList();
            //check for ko
            for (int i = targetGroups.Count - 1; i >= 0; i--)
            {
                Group group = targetGroups[i];
                if (group.Points.Count != 1) continue;
                Point p = group.Points.First();
                Board b = ImmovableHelper.CaptureSuicideGroup(p, board);
                if (b != null && KoHelper.IsKoFight(b))
                {
                    if (excludeKo)
                        targetGroups.Remove(group);
                    else
                    {
                        if (!KoHelper.KoContentEnabled(atariGroup.Content, board.GameInfo))
                            targetGroups.Remove(group);

                        if (board.MakeMoveOnNewBoard(b.Move.Value, atariGroup.Content) == null)
                            targetGroups.Remove(group);
                    }
                }
            }
            return targetGroups;
        }

        /// <summary>
        /// Check if any stones within move group atari on current board. 
        /// </summary>
        public static HashSet<Group> AtariByPreviousGroup(Board currentBoard, Board tryBoard)
        {
            List<Group> moveGroups = LinkHelper.GetPreviousMoveGroup(currentBoard, tryBoard);
            HashSet<Group> groups = new HashSet<Group>();
            foreach (Group group in moveGroups)
            {
                AtariByGroup(group, currentBoard).ForEach(g => groups.Add(g));
            }
            return groups;
        }

        /// <summary>
        /// Check if any ko atari by neighbour groups. Exclude point for double ko check to find other ko.
        /// </summary>
        public static (Boolean, Board) KoAtariByNeighbour(Board board, Group atariGroup, Point? excludePoint = null)
        {
            List<Group> targetGroups = board.GetNeighbourGroups(atariGroup).Where(gr => board.GetGroupLiberties(gr) == 1).ToList();
            foreach (Group targetGroup in targetGroups)
            {
                //check for ko
                if (targetGroup.Points.Count != 1) continue;
                Point p = targetGroup.Points.First();

                if (excludePoint != null && p.Equals(excludePoint)) continue;
                Board b = ImmovableHelper.CaptureSuicideGroup(p, board);
                if (b != null && KoHelper.IsKoFight(b))
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

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
        /// Atari by group.
        /// </summary>
        public static List<Group> AtariByGroup(Board board, Group atariGroup = null, Boolean koEnabled = true)
        {
            if (atariGroup == null) atariGroup = board.MoveGroup;
            Content c = atariGroup.Content;
            List<Group> targetGroups = board.OneLibertyNeighbourGroup(atariGroup);
            if (koEnabled)
                return targetGroups;
            //check for ko
            targetGroups.RemoveAll(t => KoHelper.IsKoFight(board, t));
            return targetGroups;
        }

        /// <summary>
        /// Is weak neighbour group.
        /// </summary>
        public static Boolean IsWeakNeighbourGroup(Board tryBoard, Group group = null)
        {
            if (group == null) group = tryBoard.MoveGroup;
            else group = tryBoard.GetCurrentGroup(group);
            if (WallHelper.IsNonKillableGroup(tryBoard, group))
                return false;
            if (tryBoard.GetNeighbourGroups(group).Any(n => IsWeakGroup(tryBoard, group, n)))
                return true;
            return false;
        }

        /// <summary>
        /// Is weak group.
        /// </summary>
        public static Boolean IsWeakGroup(Board tryBoard, Group targetGroup, Group ngroup)
        {
            Content c = ngroup.Content;
            if (ngroup.Liberties.Count != 2) return false;

            foreach (Board b in GameHelper.GetMoveBoards(tryBoard, ngroup.Liberties, c.Opposite()))
            {
                if (ImmovableHelper.CheckConnectAndDie(b, b.MoveGroup, false)) continue;
                if (WallHelper.IsNonKillableOrKo(b)) continue;
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

        /// <summary>
        /// Is double atari.
        /// </summary>
        public static Boolean IsDoubleAtari(Board board, Point p, Content c)
        {
            return board.OneLibertyGroup(p, c).Count() > 1;
        }

        /// <summary>
        /// Double kill atari without escape.
        /// </summary>
        public static Boolean DoubleKillAtariWithoutEscape(Board board)
        {
            if (board.AtariTargets.Count == 0) return false;
            List<Group> groups = board.GetGroupsFromStoneNeighbours().Where(n => !WallHelper.IsStrongGroup(board, n)).ToList();
            groups = groups.Union(board.AtariTargets).ToList();
            if (groups.Count < 2) return false;

            //check if atari targets escapable
            foreach (Group targetGroup in board.AtariTargets)
            {
                //check escape by capture
                Board b = ImmovableHelper.EscapeByCapture(board, targetGroup, false);
                if (b != null && WallHelper.StrongGroups(b, groups))
                {
                    if (groups.All(n => b.GetNeighbourGroups(b.CapturedList.First()).Contains(b.GetCurrentGroup(n))))
                        return false;
                    if (WallHelper.IsHostileGroup(b, targetGroup))
                        return false;
                }
                //make move at liberty
                Board b2 = ImmovableHelper.MakeMoveAtLiberty(board, targetGroup);
                if (b2 == null) continue;
                List<Group> ngroups = groups.Select(n => b2.GetCurrentGroup(n)).Distinct().ToList();
                if (ngroups.Count < 2) 
                    return false;
                if (WallHelper.StrongGroups(b2, groups))
                {
                    if (WallHelper.IsHostileGroup(b2, targetGroup))
                        return false;
                }
            }
            return true;
        }

    }
}

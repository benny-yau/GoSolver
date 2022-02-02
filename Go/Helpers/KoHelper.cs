using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class KoHelper
    {

        /// <summary>
        /// Enables one way ko check - either survive or kill
        /// If objective is survive with ko or kill, then ko for survive is enabled, else if objective is survive or kill with ko, then ko for kill is enabled
        /// </summary>
        /// <returns></returns>
        public static Boolean KoSurvivalEnabled(SurviveOrKill surviveOrKill, GameInfo gameInfo)
        {
            if (surviveOrKill == SurviveOrKill.Survive)
                return (gameInfo.Survival == SurviveOrKill.SurviveWithKo || gameInfo.Survival == SurviveOrKill.Kill);
            else if (surviveOrKill == SurviveOrKill.Kill)
                return (gameInfo.Survival == SurviveOrKill.KillWithKo || gameInfo.Survival == SurviveOrKill.Survive);
            return false;
        }

        public static Boolean KoContentEnabled(Content c, GameInfo gameInfo)
        {
            Content killContent = GameHelper.GetContentForSurviveOrKill(gameInfo, SurviveOrKill.Kill);
            return KoSurvivalEnabled((c == killContent) ? SurviveOrKill.Kill : SurviveOrKill.Survive, gameInfo);
        }

        /// <summary>
        /// Start ko and resolve ko fight.
        /// </summary>
        public static void StartAndResolveKoFight(GameTryMove tryMove)
        {
            StartKoFight(tryMove);
            ResolveKoFight(tryMove);
        }

        /// <summary>
        /// Is Ko fight, including both pre-ko and ko.
        /// </summary>
        public static Boolean IsKoFight(Board board)
        {
            return (board.GetStoneNeighbours().Any(n => EyeHelper.FindEye(board, n, board[board.Move.Value])) && board.IsSinglePoint() && board.MoveGroupLiberties == 1);
        }

        public static Boolean IsReverseKoFight(Board board)
        {
            List<Point> eyePoints = board.GetStoneNeighbours().Where(n => EyeHelper.FindEye(board, n, board[board.Move.Value])).ToList();
            foreach (Point eyePoint in eyePoints)
                if (board.GetStoneNeighbours(eyePoint.x, eyePoint.y).Any(n => board.GetGroupAt(n).Liberties.Count == 1)) return true;
            return false;
        }


        /// <summary>
        /// <see cref="UnitTestProject.PerformanceBenchmarkTest.PerformanceBenchmarkTest_Scenario_TianLongTu_Q17160" />
        /// </summary>
        public static Boolean StartKoFight(GameTryMove tryMove)
        {
            Board board = tryMove.TryGame.Board;
            if (IsKoFight(board))
            {
                tryMove.IsKoFight = true;
                return true;
            }
            return false;
        }

        public static Boolean ResolveKoFight(GameTryMove tryMove)
        {
            GameTryMove opponentMove = tryMove.MakeMoveWithOpponentAtSamePoint();
            if (opponentMove == null) return false;

            Board board = opponentMove.TryGame.Board;
            if (IsKoFight(board))
            {
                tryMove.IsResolveKoFight = true;
                return true;
            }
            return false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public partial class ResultHelper
    {
        /// <summary>
        /// Display game ended message from flags in confirm alive result.
        /// </summary>
        public static String GameEndedMessage(ConfirmAliveResult result, Game g)
        {
            String msg = "";
            if (Game.TimeOut(g))
                msg = "Application timeout. " + Environment.NewLine + "Please try again.";
            else if (result.HasFlag(ConfirmAliveResult.Answer) || result.HasFlag(ConfirmAliveResult.SolutionDisplayed))
            {
                Boolean isKo = (g.GameInfo.Survival == SurviveOrKill.KillWithKo || g.GameInfo.Survival == SurviveOrKill.SurviveWithKo);
                if (result.HasFlag(ConfirmAliveResult.SolutionDisplayed))
                    msg = "Solution complete" + (isKo ? " (Ko)." : ".");
                else
                    msg = "Question solved" + (isKo ? " (Ko)." : ".");
            }
            else if (result.HasFlag(ConfirmAliveResult.CorrectedSolution))
                msg = "Incorrect move. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.KoAlive))
                msg = "Computer Ko move. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.BothAlive))
                msg = "Both alive. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.TargetKilled))
                msg = "Target killed. Try again.";
            else if (result.HasFlag(ConfirmAliveResult.TargetSurvived))
                msg = "Target survived. Try again.";
            return msg;
        }


    }

}

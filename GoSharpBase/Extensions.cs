using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public static class ContentExtensions
    {
        [DebuggerStepThroughAttribute()]
        public static Content Opposite(this Content c)
        {
            if (c == Content.Empty || c == Content.Unknown) 
                throw new Exception();
            return c == Content.Black ? Content.White : Content.Black;
        }
    }

    public static class SurviveKillExtensions
    {
        [DebuggerStepThroughAttribute()]
        public static SurviveOrKill Opposite(this SurviveOrKill s)
        {
            return s == SurviveOrKill.Kill ? SurviveOrKill.Survive : SurviveOrKill.Kill;
        }

        [DebuggerStepThroughAttribute()]
        public static KoCheck Opposite(this KoCheck s)
        {
            return s == KoCheck.Kill ? KoCheck.Survive : KoCheck.Kill;
        }
    }

    public static class UserComputerExtensions
    {
        [DebuggerStepThroughAttribute()]
        public static PlayerOrComputer Opposite(this PlayerOrComputer s)
        {
            return s == PlayerOrComputer.Player ? PlayerOrComputer.Computer : PlayerOrComputer.Player;
        }
    }

}

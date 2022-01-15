using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    /// <summary>
    /// Starting move to survive or kill
    /// </summary>
    public enum SurviveOrKill
    {
        Survive,
        Kill,
        SurviveWithKo,
        KillWithKo,
        Unknown
    }

    /// <summary>
    /// Starting move as player or computer
    /// </summary>
    public enum PlayerOrComputer
    {
        Player,
        Computer
    }

    /// <summary>
    /// Represents the content of a board position, or the color of territory.
    /// </summary>
    public enum Content
    {
        Empty,
        Black,
        White,
        Unknown
    }

    /// <summary>
    /// Represents if current ko move is kill or survive
    /// </summary>
    public enum KoCheck
    {
        Kill,
        Survive,
        None
    }

    /// <summary>
    /// Result for make move.
    /// </summary>
    public enum MakeMoveResult
    {
        Unknown,
        Suicide,
        KoBlocked,
        Legal,
        NotEmpty,
        Pass
    }

    /// <summary>
    /// Result on confirm alive, including user prompts and messages.
    /// </summary>
    public enum ConfirmAliveResult
    {
        Unknown = 0,
        Dead = 1,
        KoAlive = 2,
        BothAlive = 4,
        Alive = 8,
        UseSolution = 16,
        Answer = 32,
        SolutionDisplayed = 64,
        CorrectedSolution = 128,
        Incorrect = 256,
        Mapped = 512,
        TargetKilled = 1024,
        TargetSurvived = 2048,
        NoSolution = 4096
    }

}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Provides setup information used in the game.
    /// </summary>
    [Serializable]
    public class GameInfo
    {
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }

        public SurviveOrKill Survival { get; set; }
        public Content StartContent { get; set; }
        public PlayerOrComputer UserFirst { get; set; }
        public int SearchDepth { get; set; }
        public ConfirmAliveResult RecursionResult { get; set; }

        public String ScenarioName;
        public List<SetupMove> SetupMoves = new List<SetupMove>();
        public List<Point> targetPoints = new List<Point>();
        public List<Point> survivalPoints = new List<Point>();
        public List<Point> movablePoints = new List<Point>();
        public List<Point> killMovablePoints = new List<Point>();
        public List<List<Point>> solutionPoints = new List<List<Point>>();
        public List<CorrectedList> correctedSolutions = new List<CorrectedList>();
        public List<List<Point>> dictatePoints = new List<List<Point>>();

        private List<List<Point>> combinedSolutions;
        public List<List<Point>> CombinedSolutions
        {
            get
            {
                if (correctedSolutions.Count == 0)
                    return solutionPoints;
                else
                {
                    if (combinedSolutions == null)
                        combinedSolutions = solutionPoints.Union(correctedSolutions).ToList();
                    return combinedSolutions;
                }
            }
        }

        Boolean[,] isMovablePoint;
        public Boolean[,] IsMovablePoint
        {
            get
            {
                if (isMovablePoint == null)
                {
                    isMovablePoint = new Boolean[BoardSizeX, BoardSizeY];
                    this.movablePoints.ForEach(p => isMovablePoint[p.x, p.y] = true);
                }
                return isMovablePoint;
            }
            set
            {
                isMovablePoint = value;
            }
        }

        Boolean[,] isKillMovablePoint;
        public Boolean[,] IsKillMovablePoint
        {
            get
            {
                if (isKillMovablePoint == null)
                {
                    isKillMovablePoint = new Boolean[BoardSizeX, BoardSizeY];
                    this.movablePoints.ForEach(p => isKillMovablePoint[p.x, p.y] = true);
                    this.killMovablePoints.ForEach(p => isKillMovablePoint[p.x, p.y] = true);
                }
                return isKillMovablePoint;
            }
            set
            {
                isKillMovablePoint = value;
            }
        }

        [NonSerialized]
        private dynamic playerMoveJson;
        /// <summary>
        /// Mapped json for player move first for two levels.
        /// </summary>
        public dynamic PlayerMoveJson
        {
            get
            {
                if (playerMoveJson == null)
                    playerMoveJson = new JArray();
                return playerMoveJson;
            }
            set
            {
                if (value == null)
                    throw new Exception("Player move cannot be null");
                if (value is string)
                    playerMoveJson = JsonConvert.DeserializeObject(value);
                else
                    playerMoveJson = value;
            }
        }

        [NonSerialized]
        private dynamic playerMoveJsonExtension;
        /// <summary>
        /// Mapped json for player move first for three levels.
        /// </summary>
        public dynamic PlayerMoveJsonExtension
        {
            get
            {
                if (playerMoveJsonExtension == null)
                    playerMoveJsonExtension = new JArray();
                return playerMoveJsonExtension;
            }
            set
            {
                if (value == null)
                    throw new Exception("Player move extension cannot be null");
                if (value is string)
                    playerMoveJsonExtension = JsonConvert.DeserializeObject(value);
                else
                    playerMoveJsonExtension = value;
            }
        }

        [NonSerialized]
        private dynamic challengeMoveJson;
        /// <summary>
        /// Mapped json for computer move first for two levels.
        /// </summary>
        public dynamic ChallengeMoveJson
        {
            get
            {
                if (challengeMoveJson == null)
                    challengeMoveJson = new JArray();
                return challengeMoveJson;
            }
            set
            {
                if (value == null)
                    throw new Exception("Challenge move cannot be null");
                if (value is string)
                    challengeMoveJson = JsonConvert.DeserializeObject(value);
                else
                    challengeMoveJson = value;
            }
        }

        [NonSerialized]
        private dynamic challengeMoveJsonExtension;
        /// <summary>
        /// Mapped json for computer move first for three levels.
        /// </summary>
        public dynamic ChallengeMoveJsonExtension
        {
            get
            {
                if (challengeMoveJsonExtension == null)
                    challengeMoveJsonExtension = new JArray();
                return challengeMoveJsonExtension;
            }
            set
            {
                if (value == null)
                    throw new Exception("Challenge move extension cannot be null");
                if (value is string)
                    challengeMoveJsonExtension = JsonConvert.DeserializeObject(value);
                else
                    challengeMoveJsonExtension = value;
            }
        }

        private dynamic runtimeScript_KillMove;
        /// <summary>
        /// Runtime script for removing kill moves dynamically. Previously used to remove neutral point moves.
        /// </summary>
        public dynamic RuntimeScript_KillMove
        {
            get
            {
                return runtimeScript_KillMove;
            }
            set
            {
                if (value is string && EnableFullLoading)
                    runtimeScript_KillMove = CSScriptLibrary.CSScript.Evaluator.LoadCode(value);
                else
                    runtimeScript_KillMove = value;
            }
        }

        private dynamic runtimeScript_SurvivalMove;

        /// <summary>
        /// Runtime script for removing survival moves dynamically.
        /// </summary>
        public dynamic RuntimeScript_SurvivalMove
        {
            get
            {
                return runtimeScript_SurvivalMove;
            }
            set
            {
                if (value is string && EnableFullLoading)
                    runtimeScript_SurvivalMove = CSScriptLibrary.CSScript.Evaluator.LoadCode(value);
                else
                    runtimeScript_SurvivalMove = value;
            }
        }

        public static readonly object _lockFullLoading = new object();
        private static Boolean enableFullLoading = true;

        /// <summary>
        /// Enable full loading of json mapping and scripts only on start of each game. Applies only to PlayerMoveExtension and ChallengeMoveExtension which requires getting resource string from ResourceHelper.
        /// </summary>
        public static Boolean EnableFullLoading
        {
            get
            {
                lock (_lockFullLoading)
                    return enableFullLoading;
            }
            set
            {
                lock (_lockFullLoading)
                    enableFullLoading = value;
            }
        }

        /// <summary>
        ///  Constructs a default GameInfo object, 19x19 board, black as the starting player, and objective of game as survive.
        ///  As rule of thumb, set search depth at 14 for typical scenarios for initial mapping and increase it to 18 or more on mapping verification. 
        /// </summary>
        public GameInfo(SurviveOrKill survive = SurviveOrKill.Survive, Content start = Content.Black, int searchDepth = 14)
        {
            StartContent = start;
            BoardSizeX = BoardSizeY = 19;
            Survival = survive;
            UserFirst = PlayerOrComputer.Player;
            SearchDepth = searchDepth;
        }
    }

    [Serializable]
    public class SetupMove
    {
        public Point Move { get; set; }
        public Content Content { get; set; }

        public SetupMove(Point move, Content content)
        {
            this.Move = move;
            this.Content = content;
        }

        public override string ToString()
        {
            return "Move: " + Move.ToString() + " Content: " + Content;
        }
    }

    [Serializable]
    public class CorrectedList : List<Point>
    {
        public CorrectedList(IEnumerable<Point> points) : base(points) { }
    }
}

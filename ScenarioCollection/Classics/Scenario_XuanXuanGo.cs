using Go;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScenarioCollection
{
    partial class Scenario
    {

        /*
 14 . . . O . . . . . . . . . . . . . . . 
 15 O O O . . . . . . . . . . . . . . . . 
 16 . X X O O . O . . . . . . . . . . . . 
 17 . . . X . . . . . . . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A1()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(6, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 16));
            gi.killMovablePoints.Add(new Point(6, 17));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(3, 18), new Point(4, 17), new Point(5, 17), new Point(2, 18), new Point(1, 17), new Point(2, 17), new Point(4, 18), new Point(5, 18), new Point(4, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(3, 18), new Point(4, 17), new Point(5, 17), new Point(2, 18), new Point(4, 18), new Point(5, 18), new Point(1, 17), new Point(2, 17), new Point(4, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A1_PlayerMoveExtension");

            return g;

        }

        /*
 13 . O O . . . . . . . . . . . . . . . . 
 14 . X O . . . . . . . . . . . . . . . . 
 15 . X O . . . . . . . . . . . . . . . . 
 16 X . X O . . . . . . . . . . . . . . . 
 17 . . X O . O . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A2()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(5, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 18));
            gi.killMovablePoints.Add(new Point(0, 12));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(2, 18), new Point(0, 14), new Point(0, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";
            return g;
        }

        /*
 13 . . X . . . . . . . . . . . . . . . . 
 14 . X . . . . . . . . . . . . . . . . . 
 15 . O X X . . . . . . . . . . . . . . . 
 16 O . O X . X . . . . . . . . . . . . . 
 17 . . O O X . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A3()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/848/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(4, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(0, 14), new Point(0, 15), new Point(0, 17), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(0, 15), new Point(0, 14), new Point(0, 13), new Point(0, 15), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(1, 18), new Point(1, 17), new Point(0, 13), new Point(0, 15), new Point(0, 17), new Point(0, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";

            return g;
        }

        /*
 14 O O . O . . . . . . . . . . . . . . . 
 15 X X O . . . . . . . . . . . . . . . . 
 16 . . X O O . . O . . . . . . . . . . . 
 17 . X . X O . . . . . . . . . . . . . . 
 18 . . X . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A4()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.White);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(7, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(1, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(4, 18), new Point(3, 18), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(0, 17) });


            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":-2,\"y\":-2}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":-2,\"y\":-2}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":-2,\"y\":-2}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]}]";
            return g;
        }

        /*
 13 . O . . . . . . . . . . . . . . . . . 
 14 . . . . . . . . . . . . . . . . . . . 
 15 . O O O O . O . . . . . . . . . . . . 
 16 . X X . X O . . . . . . . . . . . . . 
 17 . . . . X O . O . . . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A6()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(4, 18), new Point(3, 18), new Point(3, 16), new Point(3, 17), new Point(2, 17), new Point(2, 18), new Point(1, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(3, 16), new Point(3, 17), new Point(4, 18) });
            
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(3, 17), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(3, 17), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 17), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 17), new Point(3, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 17), new Point(3, 18) });


            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A6_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A6_ChallengeMoveExtension");
            return g;
        }

        /*
 13 X X X . . . . . . . . . . . . . . . . 
 14 . O X . . . . . . . . . . . . . . . . 
 15 O . O X . . . . . . . . . . . . . . . 
 16 . . O X . X . . . . . . . . . . . . . 
 17 . . O O X . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A7()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 18));
            gi.movablePoints.Add(new Point(4, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(1, 16), new Point(0, 17), new Point(1, 17), new Point(0, 14), new Point(3, 18), new Point(1, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A7_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A7_ChallengeMoveExtension");

            return g;
        }


        /*
 13 . . X . . . . . . . . . . . . . . . . 
 14 . X . . . . . . . . . . . . . . . . . 
 15 . O X X . . . . . . . . . . . . . . . 
 16 . O O O X X X . . . . . . . . . . . . 
 17 . . . . O O X . . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A8()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(2, 17), new Point(1, 18), new Point(3, 18), new Point(5, 18), new Point(0, 15), new Point(0, 17), new Point(1, 17), new Point(4, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]}]";
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A8_PlayerMoveExtension");
            return g;
        }

        /*
 13 . O . . . . . . . . . . . . . . . . . 
 14 . . . . . . . . . . . . . . . . . . . 
 15 . O O O O . . . . . . . . . . . . . . 
 16 . X X X O . O . O . . . . . . . . . . 
 17 . . . . X O . . . . . . . . . . . . . 
 18 . . . X . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A11()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(8, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(6, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(5, 18), new Point(1, 17), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(1, 17), new Point(2, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(0, 16), new Point(1, 17), new Point(2, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(0, 16), new Point(5, 18), new Point(1, 17), new Point(0, 18), new Point(2, 18), new Point(3, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A11_PlayerMoveExtension");

            return g;
        }


        /*
 13 . X X X . . . . . . . . . . . . . . . 
 14 . X O O X . . . . . . . . . . . . . . 
 15 . O . O X . . . . . . . . . . . . . . 
 16 . . . O X . . . . . . . . . . . . . . 
 17 . O O X . X . . . . . . . . . . . . . 
 18 . . . X . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A14()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 16), new Point(2, 15), new Point(0, 15), new Point(1, 16), new Point(0, 17), new Point(0, 16), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 16), new Point(2, 15), new Point(0, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":-2,\"y\":-2}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}}]}]";
            return g;
        }


        /*
 14 . O O O O . . . . . . . . . . . . . . 
 15 . X . X X O . . . . . . . . . . . . . 
 16 . X . . . O . . O . . . . . . . . . . 
 17 X X . . . O . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A17()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(8, 16, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(6, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(3, 17), new Point(4, 16), new Point(3, 16), new Point(2, 15), new Point(2, 16), new Point(2, 18), new Point(1, 18), new Point(3, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A17_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A17_ChallengeMoveExtension");
            
            return g;
        }


        /*
 14 . . . X X . X . . . . . . . . . . . . 
 15 X X X O . X . . . . . . . . . . . . . 
 16 O O O . . O X . . . . . . . . . . . . 
 17 . . O . . O X . X . . . . . . . . . . 
 18 . . O . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A20()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 15));
            gi.movablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(3, 17), new Point(3, 16), new Point(4, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A20_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A20_ChallengeMoveExtension");
            return g;
        }


        /*
 14 . . X . X . . . . . . . . . . . . . . 
 15 . X . . . X X . . . . . . . . . . . . 
 16 . O O O O O X . . . . . . . . . . . . 
 17 . . . . . . X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A23()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);


            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                {
                    if (g.Board[x, y] == Content.Empty)
                        gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(4, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(5, 18), new Point(4, 17), new Point(2, 17), new Point(1, 17), new Point(0, 16), new Point(0, 17), new Point(1, 18) });

            gi.dictatePoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(0, 16), new Point(0, 17), new Point(1, 18), new Point(1, 17), new Point(2, 18), new Point(4, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A23_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A23_ChallengeMoveExtension");

            return g;
        }

        /*
 12 X X X . . . . . . . . . . . . . . . . 
 13 . O . X . . . . . . . . . . . . . . . 
 14 . . . . X . . . . . . . . . . . . . . 
 15 . . O O X . . . . . . . . . . . . . . 
 16 . . . . X . . . . . . . . . . . . . . 
 17 . O O X . X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A26()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 12, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 13; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(0, 17), new Point(1, 15), new Point(0, 16), new Point(3, 16), new Point(0, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(0, 17), new Point(1, 15), new Point(0, 16), new Point(3, 16), new Point(1, 16), new Point(0, 13), new Point(1, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A26_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A26_ChallengeMoveExtension");
            return g;
        }




        /*
 12 X X X . . . . . . . . . . . . . . . . 
 13 O O O X . . . . . . . . . . . . . . . 
 14 . . . . X . . . . . . . . . . . . . . 
 15 . X O O X . . . . . . . . . . . . . . 
 16 . . . . X . . . . . . . . . . . . . . 
 17 . O . X . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A27()
        {
            var gi = new GameInfo(SurviveOrKill.SurviveWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 12, Content.Black);
            g.SetupMove(0, 13, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(1, 15, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(2, 16), new Point(0, 15), new Point(2, 17), new Point(1, 18), new Point(2, 18), new Point(2, 14), new Point(3, 14), new Point(0, 17), new Point(0, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(2, 16), new Point(0, 15), new Point(2, 17), new Point(1, 18), new Point(3, 14), new Point(0, 17), new Point(2, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(2, 16), new Point(0, 15), new Point(0, 16) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(2, 16), new Point(0, 15), new Point(2, 17), new Point(1, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(2, 16), new Point(0, 15), new Point(2, 17), new Point(1, 18), new Point(0, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A27_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A27_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . . . . . . . . . . . . . . . . . . 
 15 . X X X X X . . . . . . . . . . . . . 
 16 X X O O . O X . X . . . . . . . . . . 
 17 O O . . . O X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A33()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(8, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(4, 18), new Point(4, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(4, 18), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(4, 18), new Point(2, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(4, 18), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(2, 17), new Point(3, 17), new Point(2, 18), new Point(4, 18), new Point(1, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(1, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Dead\"}]}]";

            return g;
        }


        /*
 11 . . O . . . . . . . . . . . . . . . . 
 12 O O . . . . . . . . . . . . . . . . . 
 13 X X O O . . . . . . . . . . . . . . . 
 14 . X X O . . . . . . . . . . . . . . . 
 15 . . . . O . . . . . . . . . . . . . . 
 16 . X O O . . . . . . . . . . . . . . . 
 17 . X X O . . . . . . . . . . . . . . . 
 18 . . . O . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_A19()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 12, Content.White);
            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 15, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(0, 15), new Point(2, 18), new Point(1, 18), new Point(0, 18), new Point(0, 16) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":12},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}}]}]";
            return g;
        }

        /*
  8 . O . . . . . . . . . . . . . . . . . 
  9 . . . . . . . . . . . . . . . . . . . 
 10 . O O . . . . . . . . . . . . . . . . 
 11 . X X O . . . . . . . . . . . . . . . 
 12 . . X O . . . . . . . . . . . . . . . 
 13 . . X O . . . . . . . . . . . . . . . 
 14 X . X O . . . . . . . . . . . . . . . 
 15 . X O . . . . . . . . . . . . . . . . 
 16 . X O . . . . . . . . . . . . . . . . 
 17 . O . O . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_B4()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 8, Content.White);
            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 14) };
            for (int x = 0; x <= 1; x++)
            {
                for (int y = 10; y <= 17; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 9));
            gi.killMovablePoints.Add(new Point(0, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 11), new Point(0, 12), new Point(1, 13), new Point(0, 16), new Point(1, 14), new Point(0, 10), new Point(0, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":0,\"y\":11},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}}]},{\"FirstMove\":{\"x\":1,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":9},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":11}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":9}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":12}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":1,\"y\":12},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";

            return g;
        }

        /*
 13 . . . X . X . . . . . . . . . . . . . 
 14 . . . . . . . X . . . . . . . . . . . 
 15 . X . X X X O . X . . . . . . . . . . 
 16 . . X O O O . O X . . . . . . . . . . 
 17 . X O . . . . O X . . . . . . . . . . 
 18 . . O . . . . . X . . . . . . . . . . 
        */
        public Game Scenario_XuanXuanQiJing_Weiqi101_2282()
        {
            //https://www.101weiqi.com/book/30126/21849/2282/

            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 3; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 17));
            gi.movablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(6, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(6, 17), new Point(5, 17), new Point(4, 18), new Point(5, 18), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(4, 18), new Point(5, 18), new Point(6, 17), new Point(5, 17), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(4, 18), new Point(5, 18), new Point(5, 17), new Point(6, 17), new Point(4, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_2282_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_2282_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . X . X X . . . . . . . . . . . . . . 
 15 . . X . . X . . . . . . . . . . . . . 
 16 X X O O O . X . . . . . . . . . . . . 
 17 O O . . . O X . . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_2398()
        {
            //https://www.101weiqi.com/book/30126/21849/2398/

            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 17), new Point(4, 17), new Point(4, 18), new Point(5, 16), new Point(3, 17), new Point(5, 18), new Point(2, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 17), new Point(4, 17), new Point(4, 18), new Point(5, 18) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_2398_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_2398_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X . . . . . . . . . . . . . . . . 
 15 . . . X X X X X X X . . . . . . . . . 
 16 . X X O O X O O O X . . . . . . . . . 
 17 . . . O . O . . O X . . . . . . . . . 
 18 . . . . X . . O O X . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_Q6710()
        {
            //https://www.101weiqi.com/book/1349/3037/6710/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(6, 17), new Point(3, 18), new Point(6, 18), new Point(7, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(6, 17), new Point(3, 18), new Point(6, 18), new Point(7, 17), new Point(6, 18), new Point(6, 17), new Point(5, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}}]}]";

            return g;
        }

        /*
 10 . O O . . . . . . . . . . . . . . . . 
 11 . X O . . . . . . . . . . . . . . . . 
 12 . X O . . . . . . . . . . . . . . . . 
 13 . X X O O . . . . . . . . . . . . . . 
 14 . O X . X O . . . . . . . . . . . . . 
 15 . . X . X O . . . . . . . . . . . . . 
 16 . . X X O O . . . . . . . . . . . . . 
 17 . O O O . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_B19()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 1; x++)
            {
                for (int y = 10; y <= 17; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 14));
            gi.movablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 18));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(0, 9));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 16), new Point(3, 14), new Point(1, 15), new Point(0, 14), new Point(0, 11), new Point(0, 13) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":3,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":9},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":0,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":11},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":11},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}}]}]";
            return g;
        }

        /*
 15 . O O O O . O . . . . . . . . . . . . 
 16 . O X X . . . . . . . . . . . . . . . 
 17 . X . X . X O O . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A34()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/7729/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            gi.survivalPoints.Add(new Point(1, 17));
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 18));
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(5, 16));
            gi.movablePoints.Add(new Point(5, 17));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.Add(new Point(6, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(0, 17), new Point(5, 18), new Point(4, 18), new Point(4, 16), new Point(1, 18), new Point(4, 17), new Point(6, 18), new Point(3, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A34_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A34_ChallengeMoveExtension");
            return g;
        }


        /*
 14 O O O O O O . . . . . . . . . . . . . 
 15 O X X X X X O . . . . . . . . . . . . 
 16 . O . . . . O . . . . . . . . . . . . 
 17 X O X X X X . O . . . . . . . . . . . 
 18 . O X . . X . O . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A36()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.White);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(7, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 18) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(6, 17));
            gi.killMovablePoints.Add(new Point(6, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 16), new Point(3, 16), new Point(5, 16), new Point(0, 16), new Point(0, 18), new Point(0, 16), new Point(0, 17), new Point(0, 16), new Point(1, 16) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16}},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}}]}]";
            return g;

        }

        /*
 11 . O O O O . . . . . . . . . . . . . . 
 12 . X X X O . . . . . . . . . . . . . . 
 13 . . X . X O . . . . . . . . . . . . . 
 14 . . X X X O . . . . . . . . . . . . . 
 15 . X O O O O . . . . . . . . . . . . . 
 16 X O . . . . . . . . . . . . . . . . . 
 17 . O . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A115_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18427/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 10);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 14) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 11; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 10));
            gi.killMovablePoints.Add(new Point(0, 17));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(0, 12), new Point(1, 14), new Point(0, 15), new Point(0, 11) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(0, 12), new Point(1, 14), new Point(0, 15), new Point(0, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":0,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":12}}]},{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}}]}]";
            return g;
        }


        /*
 12 O O O O . . . . . . . . . . . . . . . 
 13 . X X . O . . . . . . . . . . . . . . 
 14 . . X . O . . . . . . . . . . . . . . 
 15 . X . X O . . . . . . . . . . . . . . 
 16 . . X X O . . . . . . . . . . . . . . 
 17 O O X O . O . . . . . . . . . . . . . 
 18 . X O O . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A75_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18387/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 12, Content.White);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(3, 14), new Point(0, 14), new Point(0, 15), new Point(0, 13), new Point(0, 16), new Point(0, 18), new Point(1, 18), new Point(1, 17), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(3, 14), new Point(0, 13), new Point(0, 14), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(3, 14), new Point(0, 15), new Point(0, 14), new Point(0, 13) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}}]}]";
            return g;
        }


        /*
 11 . . . O . . . . . . . . . . . . . . . 
 12 O O O . . . . . . . . . . . . . . . . 
 13 . X X O O . . . . . . . . . . . . . . 
 14 . . . X . O . . . . . . . . . . . . . 
 15 . X X O O . . . . . . . . . . . . . . 
 16 . . X . . . . . . . . . . . . . . . . 
 17 . X X O O . . . . . . . . . . . . . . 
 18 . . O . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A37_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18351/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 12, Content.White);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 15) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 14));
            gi.movablePoints.Add(new Point(3, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 16));
            gi.killMovablePoints.Add(new Point(4, 18));
            gi.killMovablePoints.Add(new Point(4, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(0, 13), new Point(0, 14), new Point(0, 15), new Point(2, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A37_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A37_101Weiqi_ChallengeMoveExtension");
            return g;
        }


        /*
 11 . O . . . . . . . . . . . . . . . . . 
 12 . . . . . . . . . . . . . . . . . . . 
 13 . O O . . . . . . . . . . . . . . . . 
 14 . X X O O O . . . . . . . . . . . . . 
 15 . . . X . . O . . . . . . . . . . . . 
 16 . . . X O X O . . . . . . . . . . . . 
 17 . . X X X . O . . . . . . . . . . . . 
 18 . X O O O O . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_Q18341()
        {
            //https://www.101weiqi.com/book/1349/3037/18341/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 18, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.survivalPoints.Add(new Point(4, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(1, 17), new Point(0, 15), new Point(0, 14), new Point(2, 15), new Point(2, 16), new Point(0, 13) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_Q18341_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_Q18341_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . . . O O O . . . . . . . . . . . 
 14 . . . O O X X O . . . . . . . . . . . 
 15 . . O X X O X O . . . . . . . . . . . 
 16 . . O X . . X X O . . . . . . . . . . 
 17 . . O X . X . . O . . . . . . . . . . 
 18 . . . . . . . O . . . . . . . . . . . 
        */
        public Game Scenario_XuanXuanQiJing_A46()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 3; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 18));
            gi.movablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(5, 18), new Point(4, 17), new Point(5, 16), new Point(6, 18), new Point(6, 17), new Point(4, 16), new Point(5, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";

            return g;
        }

        /*
 11 O O O O O O . . . . . . . . . . . . . 
 12 . X X X . X O O . . . . . . . . . . . 
 13 X O . . . X X O . . . . . . . . . . . 
 14 . X X . . . X O . . . . . . . . . . . 
 15 O O O X X X O . . . . . . . . . . . . 
 16 . . . O O O O . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_Q18474()
        {
            //https://www.101weiqi.com/book/1349/3037/18474/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 11, Content.White);
            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 11, Content.White);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 12, Content.White);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.White);
            gi.targetPoints = new List<Point>() { new Point(3, 15) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 12; y <= 14; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 12), new Point(4, 14), new Point(5, 14), new Point(0, 14), new Point(2, 13), new Point(0, 12) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 12), new Point(4, 14), new Point(5, 14), new Point(0, 12), new Point(2, 13), new Point(0, 14) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":2,\"y\":13},\"SecondMove\":{\"x\":3,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}}]},{\"FirstMove\":{\"x\":4,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}}]}]";
            return g;
        }

        /*
 11 . . . . . X X X . . . . . . . . . . . 
 12 . . . . X . O . X . . . . . . . . . . 
 13 . . . . X O . O X . . . . . . . . . . 
 14 . . . . X O O O X . . . . . . . . . . 
 15 . . X . X . . . X . . . . . . . . . . 
 16 . . . X X O O O X . . . . . . . . . . 
 17 . . X O O . . O X . X . . . . . . . . 
 18 . . X . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A49()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();


            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 11, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 11, Content.Black);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 12, Content.Black);
            g.SetupMove(8, 13, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(10, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 4; x <= 8; x++)
            {
                for (int y = 12; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 17));
            gi.movablePoints.Add(new Point(3, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(5, 17), new Point(5, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(5, 17), new Point(5, 18), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(5, 17), new Point(5, 18), new Point(7, 12) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(5, 17), new Point(5, 18), new Point(5, 12) });

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 12), new Point(7, 12), new Point(7, 18), new Point(6, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 12), new Point(5, 12), new Point(7, 18), new Point(6, 18), new Point(5, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A49_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A49_ChallengeMoveExtension");
            return g;
        }


        /*
 10 . . . . . . . . . . . . . . . . . . . 
 11 . . . X X X X . . . . . . . . . . . . 
 12 . . X . O O X . . . . . . . . . . . . 
 13 . . X O . . O X . . . . . . . . . . . 
 14 . . X O . . O X . . . . . . . . . . . 
 15 . X . . . . O X . . . . . . . . . . . 
 16 . X O O O O X X . . . . . . . . . . . 
 17 . . X X X X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario3dan16()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);


            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(6, 12, Content.Black);
            g.SetupMove(6, 11, Content.Black);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(4, 11, Content.Black);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 2; x <= 6; x++)
            {
                for (int y = 12; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 14), new Point(5, 15), new Point(5, 13), new Point(2, 15), new Point(4, 15), new Point(3, 15), new Point(3, 12) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":12},\"SecondMove\":{\"x\":5,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":13},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":15}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":13},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":12}}]}]";
            return g;
        }


        /*
 10 . . . . X X X . . . . . . . . . . . . 
 11 . . X X . O X X . . . . . . . . . . . 
 12 . . X O . O O O X . . . . . . . . . . 
 13 . X O O . . . O X . . . . . . . . . . 
 14 . X O . . . O X X . . . . . . . . . . 
 15 . X X O O . O X . . . . . . . . . . . 
 16 . . . X X X X . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_A52()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();


            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 10, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 10, Content.Black);
            g.SetupMove(5, 11, Content.White);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 10, Content.Black);
            g.SetupMove(6, 11, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 11, Content.Black);
            g.SetupMove(7, 12, Content.White);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(8, 12, Content.Black);
            g.SetupMove(8, 13, Content.Black);
            g.SetupMove(8, 14, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 13) };
            for (int x = 3; x <= 7; x++)
            {
                for (int y = 11; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 15), new Point(5, 14), new Point(4, 13), new Point(4, 12), new Point(4, 14), new Point(3, 14), new Point(5, 13) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":11},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":6,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":12}}]},{\"FirstMove\":{\"x\":4,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":11}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":6,\"y\":13},\"SecondMove\":{\"x\":5,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":12}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":6,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}}]},{\"FirstMove\":{\"x\":4,\"y\":11},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":12},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":11}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":13},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":11}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}},{\"ThirdMove\":{\"x\":6,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":12}}]},{\"FirstMove\":{\"x\":6,\"y\":13},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":12},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":11},\"FourthMove\":{\"x\":4,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":12}}]}]";
            return g;
        }


        /*
 14 . O . O . O . . . . . . . . . . . . . 
 15 X O . O . . . . . . . . . . . . . . . 
 16 O X X X O O . . . . . . . . . . . . . 
 17 . . . . X O . . . . . . . . . . . . . 
 18 . . . . X . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B9()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(2, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(0, 17), new Point(1, 17), new Point(0, 14), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(0, 17), new Point(1, 17), new Point(0, 14), new Point(0, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B9_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B9_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . . . X . . . . . . . . . . . . . . . 
 14 . X X . . . . . . . . . . . . . . . . 
 15 . . O X X . . . . . . . . . . . . . . 
 16 . . O O O X X . . . . . . . . . . . . 
 17 . . . X . O X . X . . . . . . . . . . 
 18 . . . . O . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_B10()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 17));
            gi.movablePoints.Add(new Point(5, 18));
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(4, 17), new Point(2, 17), new Point(5, 18), new Point(1, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B10_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B10_ChallengeMoveExtension");
            return g;
        }


        /*
 11 . . O . . . . . . . . . . . . . . . . 
 12 . . . O O . . . . . . . . . . . . . . 
 13 . O O X . . . . . . . . . . . . . . . 
 14 O O X X . O . . . . . . . . . . . . . 
 15 X X . X . . . . . . . . . . . . . . . 
 16 . . O . . O . . . . . . . . . . . . . 
 17 . . . X O . O . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B12()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(4, 16));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(4, 14));
            gi.killMovablePoints.Add(new Point(4, 13));
            gi.survivalPoints.Add(new Point(3, 17));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(2, 17), new Point(1, 18), new Point(2, 15), new Point(0, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B12_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B12_ChallengeMoveExtension");

            return g;
        }

        /*
 12 . X X X X . . . . . . . . . . . . . . 
 13 . O X O O X X . . . . . . . . . . . . 
 14 . . O . . O X . . . . . . . . . . . . 
 15 . X . O O . X . . . . . . . . . . . . 
 16 . . O . X . X . . . . . . . . . . . . 
 17 . O X X . X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_Q18331()
        {
            //https://www.101weiqi.com/book/1349/3037/18331/
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 14) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 12; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            for (int x = 3; x <= 5; x++)
            {
                for (int y = 13; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 16));
            gi.killMovablePoints.Add(new Point(0, 11));
            gi.killMovablePoints.Add(new Point(3, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(0, 17), new Point(1, 18), new Point(0, 15) });
            gi.dictatePoints.Add(new List<Point>() { new Point(1, 14), new Point(1, 16), new Point(0, 16), new Point(0, 17), new Point(0, 18), new Point(1, 18), new Point(2, 18), new Point(0, 15) });
            gi.survivalPoints.Add(new Point(1, 17));

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18331_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18331_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . O . . . . . . . . . . . . . . . . . 
 14 . . O . O . . . . . . . . . . . . . . 
 15 . . . . X O O . . . . . . . . . . . . 
 16 O O O . X X O . O . . . . . . . . . . 
 17 X X X . . . X O . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
        */
        public Game Scenario_XuanXuanGo_Q18358()
        {
            //https://www.101weiqi.com/book/1349/3037/18358/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(8, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(4, 17), new Point(6, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(4, 17), new Point(3, 16), new Point(3, 17), new Point(6, 18), new Point(5, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(4, 17), new Point(2, 18), new Point(3, 18), new Point(3, 16), new Point(3, 17), new Point(6, 18), new Point(5, 18), new Point(5, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]}]";
            return g;
        }

        /*
 13 . O . . . O . . . . . . . . . . . . . 
 14 . . O O O . . . . . . . . . . . . . . 
 15 . O X X X O O . . . . . . . . . . . . 
 16 O O X . . X O O . O . . . . . . . . . 
 17 X X . . . X X X O . . . . . . . . . . 
 18 . . . . . . O O . O . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A53()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();


            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 16), new Point(5, 17), };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(7, 18));
            gi.movablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(2, 17), new Point(8, 18), new Point(3, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(2, 17), new Point(8, 18), new Point(3, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(2, 17), new Point(8, 18), new Point(3, 18), new Point(2, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":8,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A53_PlayerMoveExtension");
            return g;
        }


        /*
 13 . . O . . O O . O . . . . . . . . . . 
 14 . . . O O . X O . . . . . . . . . . . 
 15 . . O X . . X O . . . . . . . . . . . 
 16 . . O X . . X O . . . . . . . . . . . 
 17 . . O X . X . X O O . . . . . . . . . 
 18 . . . . . . . . X . . . . . . . . . .
        */
        public Game Scenario_XuanXuanGo_A57()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 13, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.Black);
            g.SetupMove(9, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 3; x <= 7; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(2, 18));
            gi.movablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(7, 18), new Point(3, 18), new Point(4, 18), new Point(5, 15), new Point(4, 15), new Point(5, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(7, 18), new Point(3, 18), new Point(4, 18), new Point(6, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(7, 18), new Point(4, 15), new Point(4, 16), new Point(4, 18), new Point(3, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(4, 15), new Point(4, 16), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(5, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(7, 18), new Point(5, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(7, 18), new Point(6, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A57_PlayerMoveExtension");


            return g;
        }


        /*
 14 . . . . O O O O O O . . . . . . . . . 
 15 . O . O X . O X X . O . . . . . . . . 
 16 . . O . X . X . . X O . . . . . . . . 
 17 . O X X . . X X X O . . . . . . . . . 
 18 . O . . . . X . O . O . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_A61()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 14, Content.White);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 15, Content.White);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 3; x <= 9; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 17), new Point(4, 18), new Point(5, 18), new Point(3, 16), new Point(3, 18), new Point(4, 17), new Point(4, 18), new Point(5, 15), new Point(5, 16), new Point(8, 16) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":9,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A61_PlayerMoveExtension");

            return g;
        }

        /*
 11 . . . . . . . O O . . . . . . . . . . 
 12 . . . . . . O X X O O . . . . . . . . 
 13 . . . . . . O X . X O . . . . . . . . 
 14 . . . . . . O X X X O . . . . . . . . 
 15 . . . O O O . X O O O . . . . . . . . 
 16 . . . O X X . X O . . . . . . . . . . 
 17 . . . . . . . X O . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A59()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(7, 11, Content.White);
            g.SetupMove(7, 12, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 11, Content.White);
            g.SetupMove(8, 12, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 12, Content.White);
            g.SetupMove(9, 13, Content.Black);
            g.SetupMove(9, 14, Content.Black);
            g.SetupMove(9, 15, Content.White);
            g.SetupMove(10, 12, Content.White);
            g.SetupMove(10, 13, Content.White);
            g.SetupMove(10, 14, Content.White);
            g.SetupMove(10, 15, Content.White);
            gi.targetPoints = new List<Point>() { new Point(7, 17) };
            for (int x = 3; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 18));
            gi.movablePoints.Add(new Point(8, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(2, 17));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(5, 18), new Point(7, 18), new Point(4, 17), new Point(6, 17), new Point(6, 18), new Point(6, 16) });
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(6, 15), new Point(6, 16), new Point(6, 18), new Point(7, 18), new Point(5, 18), new Point(4, 17) }));
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(6, 15), new Point(6, 16), new Point(6, 18), new Point(7, 18), new Point(3, 18), new Point(4, 17) }));
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(6, 15), new Point(6, 16), new Point(3, 18), new Point(5, 18), new Point(6, 18), new Point(7, 18) }));

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A59_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A59_ChallengeMoveExtension");
            return g;
        }
        /*
  8 . O O O . . . . . . . . . . . . . . . 
  9 . . X O . . . . . . . . . . . . . . . 
 10 . X X X O . . . . . . . . . . . . . . 
 11 . . O X O . . . . . . . . . . . . . . 
 12 . . X O . . . . . . . . . . . . . . . 
 13 . . X O . . . . . . . . . . . . . . . 
 14 . X O . . . . . . . . . . . . . . . . 
 15 . X O . . . . . . . . . . . . . . . . 
 16 . O O . . . . . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A66()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/91/18509/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 8, Content.White);
            g.SetupMove(1, 10, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 8, Content.White);
            g.SetupMove(2, 9, Content.Black);
            g.SetupMove(2, 10, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 8, Content.White);
            g.SetupMove(3, 9, Content.White);
            g.SetupMove(3, 10, Content.Black);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(4, 10, Content.White);
            g.SetupMove(4, 11, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 10) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 9; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(0, 17));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 13), new Point(0, 12), new Point(1, 12), new Point(1, 13) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A66_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A66_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . O O . . . . . . . . . . . . . . . . 
 15 . . . O O O . . . . . . . . . . . . . 
 16 . X . . X O . O . . . . . . . . . . . 
 17 . X . . X X O . O . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_7245()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/7245/

            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(4, 17), new Point(1, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(3, 16), new Point(4, 18), new Point(3, 18), new Point(2, 16), new Point(6, 18), new Point(2, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(3, 16), new Point(3, 18), new Point(3, 17), new Point(2, 18), new Point(4, 18), new Point(0, 16) });
            
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_7245_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_7245_ChallengeMoveExtension");
            return g;
        }

        /*
  9 . O . . . . . . . . . . . . . . . . . 
 10 . O O . . . . . . . . . . . . . . . . 
 11 O X O . . . . . . . . . . . . . . . . 
 12 . X O O . . . . . . . . . . . . . . . 
 13 . . X O . . . . . . . . . . . . . . . 
 14 . . X . O . . . . . . . . . . . . . . 
 15 O O X . O . . . . . . . . . . . . . . 
 16 X O X . O . . . . . . . . . . . . . . 
 17 X X X O . . . . . . . . . . . . . . . 
 18 . O O O . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_B17()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 11, Content.White);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(3, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 10; y <= 17; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 9));
            gi.killMovablePoints.Add(new Point(0, 18));
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(3, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 12), new Point(1, 13), new Point(0, 10), new Point(0, 13), new Point(1, 14), new Point(0, 11), new Point(0, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 12), new Point(1, 13), new Point(0, 10), new Point(0, 13), new Point(0, 14), new Point(0, 11), new Point(1, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 12), new Point(1, 13), new Point(0, 10), new Point(0, 13), new Point(1, 14), new Point(0, 11), new Point(1, 12) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":9}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":12},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":10},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":10}}]},{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":9}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":10},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":9},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":10},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":9},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":13}}]}]";
            return g;
        }

        /*
 14 . . O O O . . . . O . . . . . . . . . 
 15 . O X X O . . O O . . . . . . . . . . 
 16 . O O X X O O X X O O O . . . . . . . 
 17 . . . . . X X . X O X O . . . . . . . 
 18 . . . . . . . . . X X . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_18467_101Weiqi()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/91/18467/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 14, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(9, 18, Content.Black);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);
            g.SetupMove(11, 16, Content.White);
            g.SetupMove(11, 17, Content.White);
            g.SetupMove(1, 13, Content.White);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 0; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                {
                    if (g.Board[x, y] == Content.Empty)
                        gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.Add(new Point(11, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(3, 17), new Point(4, 18), new Point(8, 18), new Point(11, 18), new Point(7, 17), new Point(3, 18), new Point(2, 17), new Point(2, 18), new Point(1, 17), new Point(1, 18), new Point(0, 17), new Point(4, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(3, 17), new Point(4, 18), new Point(8, 18), new Point(3, 18), new Point(2, 17), new Point(11, 18), new Point(7, 17), new Point(2, 18), new Point(1, 17), new Point(1, 18), new Point(0, 17), new Point(4, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_18467_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_18467_101Weiqi_ChallengeMoveExtension");


            return g;
        }

        /*
 13 . . O O O . . . . . . . . . . . . . . 
 14 . O X X . . O . . . . . . . . . . . . 
 15 . O X . X X O . . . . . . . . . . . . 
 16 O O X X . O X O . . . . . . . . . . . 
 17 O X O . . O X . . . . . . . . . . . . 
 18 . X O . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B6()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(0, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 1; x <= 6; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 18));
            gi.movablePoints.Add(new Point(4, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 18));
            gi.killMovablePoints.Add(new Point(5, 14));
            gi.killMovablePoints.Add(new Point(7, 17));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(7, 17), new Point(6, 18), new Point(4, 18), new Point(3, 18), new Point(3, 17), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(6, 18), new Point(7, 18), new Point(7, 17), new Point(6, 18), new Point(4, 18), new Point(3, 18), new Point(3, 17), new Point(4, 17) });//challenge solution

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]}]";
            return g;
        }

        /*
 12 . . . O . . . . . . . . . . . . . . . 
 13 . O O . . . . . . . . . . . . . . . . 
 14 X . X O . . . . . . . . . . . . . . . 
 15 . . X O O O . . . . . . . . . . . . . 
 16 O O X X X X O . . . . . . . . . . . . 
 17 . . O O X . O . . . . . . . . . . . . 
 18 . . . . X . O . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B7()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            gi.survivalPoints.Add(new Point(1, 16));
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 17));
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(0, 17), new Point(0, 18), new Point(3, 18), new Point(1, 14), new Point(1, 15), new Point(0, 15), new Point(2, 18), new Point(0, 13), new Point(1, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B7_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B7_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X . X . . . . . . . . . . . . . . 
 15 . X . . . . X X . . . . . . . . . . . 
 16 X O O O O O O X . . . . . . . . . . . 
 17 . X O . X . O X . . . . . . . . . . . 
 18 . . . . . X O X . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_18402()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/18402/
            //XuanXuanGo_B13
            var gi = new GameInfo(SurviveOrKill.SurviveWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(5, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(0, 17), new Point(0, 18), new Point(1, 18), new Point(2, 18), new Point(5, 17), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 18), new Point(4, 18), new Point(3, 18), new Point(1, 18), new Point(2, 18), new Point(5, 17), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 18), new Point(4, 18), new Point(3, 18), new Point(1, 18), new Point(2, 18), new Point(0, 15) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 18), new Point(4, 18), new Point(3, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 18), new Point(4, 18), new Point(3, 18), new Point(0, 15) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 18), new Point(0, 15) });

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(5, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(0, 17), new Point(0, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(0, 17), new Point(0, 18), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(0, 17), new Point(0, 18), new Point(1, 18), new Point(2, 18), new Point(0, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18402_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18402_ChallengeMoveExtension");


            return g;
        }

        /*
 12 . . . . O . . . . . . . . . . . . . . 
 13 O O O O . O . . . . . . . . . . . . . 
 14 . . X . O . . . . . . . . . . . . . . 
 15 . X . X O . . . . . . . . . . . . . . 
 16 . . . X O . . . . . . . . . . . . . . 
 17 . . X . O . . . . . . . . . . . . . . 
 18 . . . . O . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_18410()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/18410/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 13, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 16), new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(1, 16), new Point(0, 16), new Point(0, 15), new Point(0, 14), new Point(1, 17), new Point(2, 16), new Point(1, 14), new Point(3, 14), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(1, 16), new Point(0, 16), new Point(1, 17), new Point(2, 16), new Point(1, 14), new Point(3, 14), new Point(0, 15), new Point(0, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18410_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18410_ChallengeMoveExtension");
            return g;
        }

        /*
 12 . . X . . . . . . . . . . . . . . . . 
 13 . X . X . . . . . . . . . . . . . . . 
 14 . . . . X . . . . . . . . . . . . . . 
 15 O O O O X . . . . . . . . . . . . . . 
 16 O X X X O X X . O . . . . . . . . . . 
 17 . . X O O . . O . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_1887()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/1887/

            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(4, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(1, 14));
            gi.killMovablePoints.Add(new Point(2, 14));
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.movablePoints.Add(new Point(7, 18));

            gi.survivalPoints.Add(new Point(1, 15));
            gi.survivalPoints.Add(new Point(2, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(0, 17), new Point(4, 18), new Point(1, 17), new Point(3, 18), new Point(2, 18), new Point(2, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_1887_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_1887_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . . . . . O . O . . . . . . . . . . 
 15 . . . . O . . . . . . . . . . . . . . 
 16 . O O O . X X X O O O O . . . . . . . 
 17 . O X X . X O O X X X O . . . . . . . 
 18 . O X X . . . . . . . O . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B31()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(11, 16, Content.White);
            g.SetupMove(11, 17, Content.White);
            g.SetupMove(11, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 2; x <= 10; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.Add(new Point(6, 15));
            gi.killMovablePoints.Add(new Point(7, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(5, 18), new Point(9, 18), new Point(7, 18), new Point(6, 18), new Point(8, 17), new Point(4, 17), new Point(4, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(5, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(5, 18), new Point(9, 18), new Point(7, 18), new Point(4, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B31_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B31_ChallengeMoveExtension");

            return g;
        }


        /*
  9 . X X X . . . . . . . . . . . . . . . 
 10 . O O X . . . . . . . . . . . . . . . 
 11 . . . O X X . . . . . . . . . . . . . 
 12 . . X O O . X . . . . . . . . . . . . 
 13 . X X O . O X . . . . . . . . . . . . 
 14 . . O O O . X . . . . . . . . . . . . 
 15 . O . X X X . . . . . . . . . . . . . 
 16 . X X . . . . . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B32()
        {
            //Scenario_XuanXuanQiJing_B26
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 9, Content.Black);
            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 9, Content.Black);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 9, Content.Black);
            g.SetupMove(3, 10, Content.Black);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 11, Content.Black);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 12, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 14) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 11; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 9));
            gi.movablePoints.Add(new Point(0, 10));
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(1, 10));
            gi.movablePoints.Add(new Point(2, 10));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(0, 17));

            gi.survivalPoints.Add(new Point(2, 12));
            gi.survivalPoints.Add(new Point(1, 10));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(0, 12), new Point(0, 14), new Point(0, 10), new Point(0, 13), new Point(0, 11), new Point(1, 12), new Point(1, 11), new Point(2, 11) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B32_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B32_ChallengeMoveExtension");
            return g;
        }

        /*
11 . . . . . . . . X . . . . . . . . . . 
12 . . . . . X . X . . . . . . . . . . . 
13 . . . X . X O . X . . . . . . . . . . 
14 . . . . . O . O . . . . . . . . . . . 
15 . . X X O O O O X X X . . . . . . . . 
16 . . X O X X . . O O X . . . . . . . . 
17 . . X O X . O . . O X . . . . . . . . 
18 . . . . . . . . X O X . . . . . . . .
  */
        public Game Scenario_XuanXuanGo_B33()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 12, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(8, 11, Content.Black);
            g.SetupMove(8, 13, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 18, Content.Black);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(9, 18, Content.White);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(4, 15) };
            for (int x = 3; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(6, 13));
            gi.movablePoints.Add(new Point(6, 14));
            gi.movablePoints.Add(new Point(7, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.Add(new Point(4, 14));
            gi.killMovablePoints.Add(new Point(6, 12));
            gi.killMovablePoints.Add(new Point(8, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(8, 17), new Point(7, 16), new Point(7, 13), new Point(5, 18), new Point(3, 18), new Point(7, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(8, 17), new Point(7, 16), new Point(7, 13), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(8, 17), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B33_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B33_ChallengeMoveExtension");
            return g;
        }

        /*
 12 . . . . X . . . . . . . . . . . . . . 
 13 . . . . . . X . . . . . . . . . . . . 
 14 . . . X . O . X . X . . . . . . . . . 
 15 . . . . O . O . . . . . . . . . . . . 
 16 . X X X O O O X X . X . . . . . . . . 
 17 . X O O X X . O O X . . . . . . . . . 
 18 . O . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B35()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 14, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(7, 17) };
            for (int x = 0; x <= 9; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 14));
            gi.movablePoints.Add(new Point(5, 14));
            gi.movablePoints.Add(new Point(6, 14));
            gi.movablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.Add(new Point(4, 13));
            gi.killMovablePoints.Add(new Point(5, 13));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(7, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(2, 18), new Point(3, 18), new Point(5, 18), new Point(6, 18), new Point(2, 18), new Point(6, 17), new Point(3, 17), new Point(8, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(2, 18), new Point(3, 18), new Point(5, 18), new Point(6, 17), new Point(2, 18), new Point(6, 18), new Point(3, 17), new Point(8, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(2, 18), new Point(3, 18), new Point(5, 18), new Point(6, 18), new Point(2, 18), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(2, 18), new Point(3, 18), new Point(5, 18), new Point(6, 17), new Point(2, 18), new Point(3, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B35_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B35_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . X X . . . . . . . . . . . . . . 
 14 . X X O O X X . . . . . . . . . . . . 
 15 . X O . . O X . . . . . . . . . . . . 
 16 X X O . O . X . . . . . . . . . . . . 
 17 X O O . . O X . X . . . . . . . . . . 
 18 . . . . . . O . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A64()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.White);
            g.SetupMove(8, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 1; x <= 6; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(3, 17), new Point(3, 16), new Point(4, 15) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15},\"Result\":\"Dead\"}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A64_PlayerMoveExtension");

            return g;
        }

        /*
  13 O O O . O . . . . . . . . . . . . . . 
  14 . . . O . . . . . . . . . . . . . . . 
  15 X . X O . . . . . . . . . . . . . . . 
  16 . . . . O . . . . . . . . . . . . . . 
  17 . X . X O . . . . . . . . . . . . . . 
  18 . . X O O . . . . . . . . . . . . . .
          */
        public Game Scenario_XuanXuanGo_A82_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/62625/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.White);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 18), new Point(1, 14), new Point(1, 15), new Point(2, 16), new Point(1, 16), new Point(2, 17), new Point(3, 16), new Point(2, 17), new Point(2, 16), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 16), new Point(1, 16), new Point(1, 15), new Point(1, 14), new Point(2, 14), new Point(0, 18), new Point(1, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(1, 14), new Point(2, 16), new Point(1, 16), new Point(2, 14), new Point(0, 18), new Point(1, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(1, 14), new Point(2, 14), new Point(1, 16), new Point(2, 16), new Point(0, 18), new Point(1, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A82_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A82_101Weiqi_ChallengeMoveExtension");
            return g;
        }


        /*
  8 . . O . . . . . . . . . . . . . . . . 
  9 . O . . . . . . . . . . . . . . . . . 
 10 . X O . . . . . . . . . . . . . . . . 
 11 . X O . . . . . . . . . . . . . . . . 
 12 . . X O . . . . . . . . . . . . . . . 
 13 . . X O . . . . . . . . . . . . . . . 
 14 . . . X O . . . . . . . . . . . . . . 
 15 . X . X O . . . . . . . . . . . . . . 
 16 O . X X O . . . . . . . . . . . . . . 
 17 . O O O O . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A67_101Weiqi()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/91/18492/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.Black);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 8, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 13) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 10; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 9));
            gi.movablePoints.Add(new Point(0, 17));
            gi.movablePoints.Add(new Point(0, 18));
            gi.movablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(2, 14), new Point(0, 14), new Point(1, 13), new Point(1, 16) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A67_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A67_101Weiqi_ChallengeMoveExtension");
            return g;
        }


        /*
 10 O O O . . . . . . . . . . . . . . . . 
 11 . X . O . . . . . . . . . . . . . . . 
 12 . . X O . . . . . . . . . . . . . . . 
 13 . X . . O . . . . . . . . . . . . . . 
 14 . . . X O . . . . . . . . . . . . . . 
 15 X . . X O . . . . . . . . . . . . . . 
 16 . X X O . . . . . . . . . . . . . . . 
 17 O O O . O . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A136_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18446/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 10, Content.White);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 13) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 11; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 14), new Point(1, 14), new Point(2, 15), new Point(2, 13), new Point(0, 11), new Point(0, 12), new Point(0, 13), new Point(0, 14), new Point(2, 11), new Point(1, 15), new Point(1, 12) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 14), new Point(1, 14), new Point(2, 15), new Point(2, 13), new Point(0, 13), new Point(0, 12), new Point(0, 11) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}}]},{\"FirstMove\":{\"x\":2,\"y\":11},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}}]},{\"FirstMove\":{\"x\":2,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":12},\"SecondMove\":{\"x\":2,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":12},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":13}}]},{\"FirstMove\":{\"x\":2,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":1,\"y\":12}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":11},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":2,\"y\":11},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":11}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":3,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":11}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":3,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":13}}]},{\"FirstMove\":{\"x\":3,\"y\":13},\"SecondMove\":{\"x\":2,\"y\":13},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":11},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]}]";
            return g;
        }

        /*
 14 . . . . . . . O O O O . . . . . . . . 
 15 . . . . . . O X X X O . . . . . . . . 
 16 . O . O O O O . . . O . . . . . . . . 
 17 . . O X X X X X . X X O . . . . . . . 
 18 . . . . . . . . . . . O . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A138_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18448/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(9, 14, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 14, Content.White);
            g.SetupMove(10, 15, Content.White);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(11, 17, Content.White);
            g.SetupMove(11, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 3; x <= 10; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 15));
            gi.movablePoints.Add(new Point(8, 15));
            gi.movablePoints.Add(new Point(9, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(10, 18), new Point(9, 18), new Point(6, 18), new Point(7, 18), new Point(8, 17), new Point(8, 16), new Point(9, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(10, 18), new Point(9, 18), new Point(6, 18), new Point(7, 18), new Point(8, 17), new Point(8, 16), new Point(7, 16) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(7, 18), new Point(10, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(7, 18), new Point(8, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(7, 18), new Point(9, 18), new Point(10, 18), new Point(8, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A138_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A138_101Weiqi_ChallengeMoveExtension");

            return g;
        }

        /*
 11 O O O O O O . . . . . . . . . . . . . 
 12 X X O X X . O . . . . . . . . . . . . 
 13 O X X . . X O . . . . . . . . . . . . 
 14 . X . X X O . . . . . . . . . . . . . 
 15 . . X O O O . . . . . . . . . . . . . 
 16 . O X O . . . . . . . . . . . . . . . 
 17 . X O . O . . . . . . . . . . . . . . 
 18 . X O . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A53_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18365/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 11, Content.White);
            g.SetupMove(0, 12, Content.Black);
            g.SetupMove(0, 13, Content.White);
            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 11, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 13, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 14) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 12; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            for (int x = 3; x <= 5; x++)
            {
                for (int y = 12; y <= 14; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 14), new Point(0, 16), new Point(0, 14), new Point(4, 13), new Point(0, 15), new Point(0, 17), new Point(0, 18), new Point(1, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A53_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A53_101Weiqi_ChallengeMoveExtension");
            return g;
        }

        /*
 11 . . O . . . . . . . . . . . . . . . . 
 12 . O . . . . . . . . . . . . . . . . . 
 13 . X O O . . . . . . . . . . . . . . . 
 14 . X X . O . . . . . . . . . . . . . . 
 15 . . . X O . . . . . . . . . . . . . . 
 16 . . . X O . . . . . . . . . . . . . . 
 17 . O X O O . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A28_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18345/
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 26);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 11));
            gi.killMovablePoints.Add(new Point(4, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 16), new Point(2, 16), new Point(1, 18), new Point(0, 17), new Point(3, 14), new Point(0, 16), new Point(3, 18), new Point(2, 15), new Point(0, 13), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 16), new Point(2, 16), new Point(1, 18), new Point(0, 16), new Point(3, 14), new Point(0, 17), new Point(3, 18), new Point(2, 15), new Point(0, 13), new Point(0, 15) });

            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(2, 18), new Point(1, 16), new Point(2, 16), new Point(1, 15), new Point(0, 17), new Point(0, 13), new Point(0, 14), new Point(0, 16), new Point(1, 18), new Point(3, 14), new Point(2, 15), new Point(0, 18), new Point(0, 12), new Point(3, 18), new Point(1, 11) }));
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(2, 18), new Point(1, 16), new Point(2, 16), new Point(1, 15), new Point(0, 17), new Point(0, 13), new Point(0, 14), new Point(3, 14), new Point(2, 15), new Point(0, 16), new Point(1, 18), new Point(0, 18), new Point(0, 12), new Point(3, 18), new Point(1, 11) }));
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(2, 18), new Point(1, 16), new Point(2, 16), new Point(1, 15), new Point(0, 17), new Point(0, 13), new Point(0, 14), new Point(0, 16), new Point(1, 18), new Point(0, 18), new Point(0, 12), new Point(3, 18), new Point(1, 11) }));
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(2, 18), new Point(1, 16), new Point(2, 16), new Point(1, 15), new Point(0, 17), new Point(0, 13), new Point(0, 14), new Point(0, 16), new Point(1, 18), new Point(0, 18), new Point(0, 12), new Point(3, 14), new Point(2, 15), new Point(3, 18), new Point(1, 11) }));

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A28_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A28_101Weiqi_ChallengeMoveExtension");
            return g;
        }

        /*
 11 . O . O . . . . . . . . . . . . . . . 
 12 . . . . O . O . . . . . . . . . . . . 
 13 X X X X . . . . . . . . . . . . . . . 
 14 . O . X O O X X . X . . . . . . . . . 
 15 X O . X X X O . . . . . . . . . . . . 
 16 . X X X . . O O X X . X . . . . . . . 
 17 X O O O . . . O . . X . . . . . . . . 
 18 . O . . O . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A42()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 14, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(11, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(6, 16) };
            gi.survivalPoints.Add(new Point(2, 17));
            for (int x = 0; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(11, 18));
            gi.killMovablePoints.Add(new Point(7, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(5, 18), new Point(7, 18), new Point(8, 17), new Point(9, 18), new Point(8, 18), new Point(5, 17), new Point(5, 16), new Point(4, 17), new Point(4, 16), new Point(3, 18), new Point(6, 17), new Point(4, 18), new Point(5, 18), new Point(6, 18), new Point(7, 18), new Point(0, 16), new Point(5, 18), new Point(0, 18), new Point(2, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(5, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(5, 18), new Point(7, 18), new Point(8, 17), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(5, 18), new Point(7, 18), new Point(8, 17), new Point(8, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A42_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A42_ChallengeMoveExtension");
            return g;
        }

        /*
  8 . . . . . O X . . . . . . . . . . . . 
  9 . . X X O O X . . . . . . . . . . . . 
 10 . . O O X X O . . . . . . . . . . . . 
 11 . . O X . X O . . . . . . . . . . . . 
 12 . . O X X X O . . . . . . . . . . . . 
 13 . . O . X O O . . . . . . . . . . . . 
 14 . . . O X . . . . . . . . . . . . . . 
 15 . . O . X O O O O . . . . . . . . . . 
 16 . . . O X . . X X O O O . . . . . . . 
 17 . . . O . . X . . X X O . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_18497()
        {
            //https://www.101weiqi.com/book/1349/3037/18497/

            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 9, Content.Black);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 9, Content.Black);
            g.SetupMove(3, 10, Content.White);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 9, Content.White);
            g.SetupMove(4, 10, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 8, Content.White);
            g.SetupMove(5, 9, Content.White);
            g.SetupMove(5, 10, Content.Black);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 8, Content.Black);
            g.SetupMove(6, 9, Content.Black);
            g.SetupMove(6, 10, Content.White);
            g.SetupMove(6, 11, Content.White);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(11, 16, Content.White);
            g.SetupMove(11, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 16) };
            for (int x = 3; x <= 11; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 11));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 13));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(5, 14));
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.Add(new Point(12, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 17), new Point(4, 17), new Point(8, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18497_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_18497_ChallengeMoveExtension");
            return g;
        }

        /*
  9 . O O O . . . . . . . . . . . . . . . 
 10 . O X O . . . . . . . . . . . . . . . 
 11 . X X X O O . . . . . . . . . . . . . 
 12 . . . . X O . . . . . . . . . . . . . 
 13 . . O X . O . . . . . . . . . . . . . 
 14 . X X O . . . . . . . . . . . . . . . 
 15 . X O O . . . . . . . . . . . . . . . 
 16 . X O . . . . . . . . . . . . . . . . 
 17 . X O . . . . . . . . . . . . . . . . 
 18 . O O . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_B51()
        {
            //https://www.101weiqi.com/q/18476/
            //Q-18476 
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 9, Content.White);
            g.SetupMove(2, 10, Content.Black);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 9, Content.White);
            g.SetupMove(3, 10, Content.White);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(5, 11, Content.White);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(5, 13, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 14) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 10; y <= 18; y++)
                {
                    if (g.Board[x, y] == Content.Empty)
                        gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(2, 13));
            gi.movablePoints.Add(new Point(3, 12));
            gi.movablePoints.Add(new Point(3, 13));
            gi.movablePoints.Add(new Point(4, 12));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 9));
            gi.killMovablePoints.Add(new Point(4, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 13), new Point(1, 13), new Point(0, 11), new Point(2, 12), new Point(0, 12), new Point(0, 13), new Point(3, 12) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 13), new Point(1, 13), new Point(0, 11), new Point(2, 12), new Point(0, 12), new Point(0, 13), new Point(0, 10), new Point(1, 12), new Point(3, 12) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 13), new Point(0, 13), new Point(4, 13), new Point(3, 12), new Point(0, 12), new Point(0, 11), new Point(0, 14), new Point(1, 12) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 13), new Point(0, 13), new Point(0, 12), new Point(0, 11), new Point(4, 13), new Point(3, 12), new Point(0, 14), new Point(1, 12) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 13), new Point(0, 13), new Point(0, 12), new Point(0, 11), new Point(0, 14), new Point(1, 12), new Point(4, 13), new Point(3, 12) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_B51_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_B51_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . O O . O . . . . . . . . . . . . 
 14 . . . O X X . O O O O . . . . . . . . 
 15 . . O . . . X X X X O . . . . . . . . 
 16 . . O X . X O O . . O . . . . . . . . 
 17 . . O X X . O . X . O . . . . . . . . 
 18 . . O O X X . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A171_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18478/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 14, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(10, 14, Content.White);
            g.SetupMove(10, 15, Content.White);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 3; x <= 10; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 13));
            gi.killMovablePoints.Add(new Point(11, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 16), new Point(9, 16), new Point(9, 17), new Point(7, 18), new Point(4, 15), new Point(3, 15), new Point(5, 15), new Point(4, 16), new Point(5, 15), new Point(6, 14), new Point(5, 13), new Point(4, 15), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 16), new Point(9, 16), new Point(9, 17), new Point(7, 18), new Point(4, 15), new Point(3, 15), new Point(5, 15), new Point(4, 16), new Point(5, 15), new Point(6, 14), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 16), new Point(9, 16), new Point(9, 17), new Point(7, 18), new Point(8, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A171_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A171_101Weiqi_ChallengeMoveExtension");
            return g;
        }

        /*
 8 . O . . . . . . . . . . . . . . . . . 
 9 . . . . . . . . . . . . . . . . . . . 
10 X O O O . . . . . . . . . . . . . . . 
11 . X X X O O . . . . . . . . . . . . . 
12 . . . . X . . . . . . . . . . . . . . 
13 . . . . X . O . . . . . . . . . . . . 
14 . X X . X O . . . . . . . . . . . . . 
15 X O . O O O . . . . . . . . . . . . . 
16 . O . . . . . . . . . . . . . . . . . 
17 . . . . . . . . . . . . . . . . . . . 
18 . . . . . . . . . . . . . . . . . . .
        */
        public Game Scenario_XuanXuanQiJing_B57()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 20);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 10, Content.Black);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 8, Content.White);
            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(3, 10, Content.White);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 11, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 13, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 14) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 11; y <= 14; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 10));
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 9));
            gi.killMovablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.Add(new Point(5, 12));
            gi.killMovablePoints.Add(new Point(5, 13));
            gi.killMovablePoints.Add(new Point(2, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 11), new Point(0, 12), new Point(2, 12), new Point(3, 14), new Point(3, 12), new Point(0, 14), new Point(1, 12), new Point(0, 11), new Point(0, 13), new Point(1, 13), new Point(0, 9) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 11), new Point(0, 12), new Point(3, 14), new Point(3, 13), new Point(2, 12) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 11), new Point(0, 12), new Point(2, 12), new Point(3, 14), new Point(2, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 11), new Point(0, 12), new Point(2, 12), new Point(3, 14), new Point(1, 12) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 11), new Point(0, 12), new Point(2, 12), new Point(3, 14), new Point(3, 12), new Point(0, 14), new Point(1, 12), new Point(0, 11), new Point(0, 9), new Point(0, 13), new Point(2, 13) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_B57_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_B57_ChallengeMoveExtension");
            return g;
        }

        /*
 11 . X X X X X . . . . . . . . . . . . . 
 12 . O O O . . . . . . . . . . . . . . . 
 13 . . . . X X . . . . . . . . . . . . . 
 14 . . . . . X . . . . . . . . . . . . . 
 15 . . O O O X . . . . . . . . . . . . . 
 16 . O X X O X . . . . . . . . . . . . . 
 17 O X . X X . . . . . . . . . . . . . . 
 18 . . X . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_B3()
        {
            //https://www.101weiqi.com/q/2609/
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 8, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 11, Content.Black);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 12) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 11; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 14));
            gi.movablePoints.Add(new Point(4, 15));
            gi.movablePoints.Add(new Point(4, 16));
            gi.movablePoints.Add(new Point(0, 17));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 10));
            gi.killMovablePoints.Add(new Point(4, 12));
            gi.killMovablePoints.Add(new Point(0, 18));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(0, 15), new Point(0, 16), new Point(3, 14), new Point(0, 12), new Point(0, 14), new Point(3, 13), new Point(2, 14), new Point(2, 13), new Point(4, 14), new Point(0, 13), new Point(1, 15), new Point(2, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 14), new Point(0, 18), new Point(1, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B3_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_B3_ChallengeMoveExtension");

            return g;
        }

        /*
  9 . O . . . . . . . . . . . . . . . . . 
 10 . . . . . . . . . . . . . . . . . . . 
 11 X O O . . . . . . . . . . . . . . . . 
 12 . X X O . . . . . . . . . . . . . . . 
 13 . . X O . . . . . . . . . . . . . . . 
 14 . . . O . . . . . . . . . . . . . . . 
 15 . X X O . . . . . . . . . . . . . . . 
 16 . . X O . . . . . . . . . . . . . . . 
 17 X X O . O . . . . . . . . . . . . . . 
 18 X X O . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_A39()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 11, Content.Black);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(0, 18, Content.Black);
            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 11; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 10));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 14), new Point(0, 12), new Point(0, 15), new Point(1, 14), new Point(1, 16), new Point(0, 16), new Point(1, 16), new Point(1, 17), new Point(1, 18), new Point(0, 16), new Point(0, 17), new Point(0, 18) });//1a

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 14), new Point(0, 12), new Point(0, 15), new Point(1, 14), new Point(1, 16), new Point(0, 16), new Point(1, 16), new Point(1, 17), new Point(0, 17) });//1b

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 15), new Point(0, 12), new Point(0, 10) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 15), new Point(0, 12), new Point(0, 14), new Point(0, 16), new Point(1, 16), new Point(1, 17), new Point(1, 18), new Point(0, 16), new Point(0, 17), new Point(0, 18) });//2a

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 15), new Point(0, 12), new Point(0, 14), new Point(0, 16), new Point(1, 16), new Point(1, 17), new Point(0, 17) });//2b

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 15), new Point(0, 12), new Point(1, 16), new Point(0, 16), new Point(1, 16), new Point(1, 17), new Point(1, 18), new Point(0, 16), new Point(0, 17), new Point(0, 18) });//3a
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 15), new Point(0, 12), new Point(1, 16), new Point(0, 16), new Point(1, 16), new Point(1, 17), new Point(0, 17) });//3b

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 13), new Point(2, 14), new Point(0, 15), new Point(0, 12), new Point(1, 16), new Point(0, 16), new Point(0, 14), new Point(1, 13), new Point(1, 16) });//4a

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":10}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":13},\"SecondMove\":{\"x\":2,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":14}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A39_PlayerMoveExtension");
            return g;
        }
        /*
  8 . O . . . . . . . . . . . . . . . . . 
  9 X O . O . . . . . . . . . . . . . . . 
 10 . X O . . . . . . . . . . . . . . . . 
 11 . X O . . . . . . . . . . . . . . . . 
 12 . . X O . . . . . . . . . . . . . . . 
 13 . . X . O . . . . . . . . . . . . . . 
 14 . . . . O . . . . . . . . . . . . . . 
 15 . X X . O . . . . . . . . . . . . . . 
 16 X O O O . . . . . . . . . . . . . . . 
 17 . O . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
        */
        public Game Scenario_XuanXuanGo_A41()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/91/18507/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 9, Content.Black);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 8, Content.White);
            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.Black);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 10, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 9, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 13) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 10; y <= 15; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(3, 13));
            gi.movablePoints.Add(new Point(3, 14));
            gi.movablePoints.Add(new Point(3, 15));
            gi.movablePoints.Add(new Point(0, 9));
            gi.movablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(0, 17));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 13), new Point(1, 13), new Point(0, 12), new Point(2, 14), new Point(0, 14), new Point(0, 15), new Point(0, 13), new Point(0, 10), new Point(1, 12), new Point(1, 14), new Point(0, 8), new Point(0, 11), new Point(0, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(1, 13), new Point(1, 12), new Point(0, 10) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A41_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A41_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . . . . . . . O . . . . . . . . . . . 
 14 . . . . . . O . O . . . . . . . . . . 
 15 . . . . . O . . . O O O . . . . . . . 
 16 . . . O O X X X X . X O . . . . . . . 
 17 . O . O X . . . . . X O . . . . . . . 
 18 . . . . . X . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_Q18500()
        {
            //https://www.101weiqi.com/book/1349/3037/18500/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 17, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 15, Content.White);
            g.SetupMove(10, 15, Content.White);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(11, 15, Content.White);
            g.SetupMove(11, 16, Content.White);
            g.SetupMove(11, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(6, 16) };
            for (int x = 3; x <= 11; x++)
            {
                for (int y = 16; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(12, 18));
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.Add(new Point(6, 15));
            gi.killMovablePoints.Add(new Point(7, 15));
            gi.killMovablePoints.Add(new Point(8, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(9, 16), new Point(9, 17), new Point(6, 18), new Point(7, 17), new Point(7, 18), new Point(8, 17), new Point(5, 17), new Point(6, 17), new Point(3, 18), new Point(9, 18), new Point(4, 18), new Point(7, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(8, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(9, 16), new Point(9, 17), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(9, 16), new Point(9, 17), new Point(4, 18) });

            gi.dictatePoints.Add(new List<Point>() { new Point(8, 18), new Point(10, 18), new Point(6, 18), new Point(9, 16), new Point(3, 18), new Point(5, 17), new Point(8, 17), new Point(7, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_Q18500_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_Q18500_ChallengeMoveExtension");
            return g;
        }


        /*
  8 O O O . . . . . . . . . . . . . . . . 
  9 X X . O . . . . . . . . . . . . . . . 
 10 . . X O . . . . . . . . . . . . . . . 
 11 . . X O . . . . . . . . . . . . . . . 
 12 . . X O . . . . . . . . . . . . . . . 
 13 . . . . O . . . . . . . . . . . . . . 
 14 . X X X O . . . . . . . . . . . . . . 
 15 X O O O O . . . . . . . . . . . . . . 
 16 O . . . . . . . . . . . . . . . . . . 
 17 . O . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_B74()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/91/18499/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 8, Content.White);
            g.SetupMove(0, 9, Content.Black);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 8, Content.White);
            g.SetupMove(1, 9, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 8, Content.White);
            g.SetupMove(2, 10, Content.Black);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 9, Content.White);
            g.SetupMove(3, 10, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 12) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 9; y <= 14; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 10), new Point(0, 10), new Point(0, 14), new Point(1, 13), new Point(1, 11), new Point(1, 12), new Point(3, 13) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_B74_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_B74_ChallengeMoveExtension");

            return g;
        }


        /*
  9 . O O . . . . . . . . . . . . . . . . 
 10 . X . O . . . . . . . . . . . . . . . 
 11 . . X O . . . . . . . . . . . . . . . 
 12 . . X . . . . . . . . . . . . . . . . 
 13 . . X O O . . . . . . . . . . . . . . 
 14 . . . X O . . . . . . . . . . . . . . 
 15 . X X . O . . . . . . . . . . . . . . 
 16 . O O O . . . . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
        */
        public Game Scenario_XuanXuanGo_A151_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18461/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 9, Content.White);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 10, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 9; y <= 16; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(3, 15));
            gi.movablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(0, 17));
            gi.killMovablePoints.Add(new Point(3, 12));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(0, 10), new Point(0, 11), new Point(1, 12), new Point(1, 13), new Point(0, 12), new Point(0, 13), new Point(2, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(0, 10), new Point(0, 11), new Point(1, 12), new Point(1, 13), new Point(0, 12), new Point(0, 13), new Point(2, 10), new Point(0, 9), new Point(2, 14) });

            gi.dictatePoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(0, 10), new Point(0, 11), new Point(1, 12), new Point(3, 15), new Point(1, 14), new Point(0, 16), new Point(1, 13) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A151_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A151_101Weiqi_ChallengeMoveExtension");
            return g;
        }

    }
}

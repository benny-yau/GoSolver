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
 14 X X X X . . . . . . . . . . . . . . . 
 15 X O O O X . . . . . . . . . . . . . . 
 16 O . . . X . . . . . . . . . . . . . . 
 17 O . O X . . . . . . . . . . . . . . . 
 18 . . O X . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q14967()
        {
            //https://www.101weiqi.com/book/guanzipu/100/14967/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 12);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(2, 16) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":18}},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18}},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18}}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]}]";
            return g;
        }

        /*

 13 O O O . O . . . . . . . . . . . . . . 
 14 O X X O . . . . . . . . . . . . . . . 
 15 X . . X O . . . . . . . . . . . . . . 
 16 X . X . O . . . . . . . . . . . . . . 
 17 . . X O . O . . . . . . . . . . . . . 
 18 . X . O . . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_Q19020()
        {
            //https://www.101weiqi.com/book/guanzipu/100/19020/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 10);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.White);
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 15), new Point(1, 17), new Point(3, 16), new Point(2, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]}]";
            return g;
        }

        /*
 15 . . . O O O O O O . . . . . . . . . . 
 16 . . O . X O X X . O . . . . . . . . . 
 17 . . O X . X O . X O . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q19336()
        {
            //https://www.101weiqi.com/book/30126/21825/19336/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(6, 16) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(3, 16), new Point(2, 18), new Point(4, 17), new Point(7, 17), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(3, 16), new Point(2, 18), new Point(4, 17), new Point(7, 17), new Point(8, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(3, 16), new Point(2, 18), new Point(4, 17), new Point(7, 17), new Point(9, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(3, 16), new Point(2, 18), new Point(7, 17), new Point(7, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(3, 16), new Point(2, 18), new Point(8, 18), new Point(7, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(3, 16), new Point(2, 18), new Point(9, 18), new Point(7, 17) });

            gi.PlayerMoveJson = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q19336_PlayerMoveExtension");
            gi.ChallengeMoveJson = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q19336_ChallengeMoveExtension");
            return g;

        }


        /*
 12 . . X . . . . . . . . . . . . . . . . 
 13 . X . . . . . . . . . . . . . . . . . 
 14 . . X O O . O . . . . . . . . . . . . 
 15 O O O X O . . . . . . . . . . . . . . 
 16 X O X X X O . . . . . . . . . . . . . 
 17 . X . O X O . . . . . . . . . . . . . 
 18 . . . X X O . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A2()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(5, 18, Content.White);
            g.SetupMove(6, 14, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 18) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 14));
            gi.survivalPoints.Add(new Point(1, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(2, 18), new Point(1, 18), new Point(0, 18), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(2, 18), new Point(1, 18), new Point(0, 18), new Point(2, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17}},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":18}},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18}},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18}},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18}},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":18}}]";
            return g;
        }

        /*
 15 . . . O O O O . O . . . . . . . . . . 
 16 O O O . X X X O . . . . . . . . . . . 
 17 . X X . . . X O . . . . . . . . . . . 
 18 . . . . X X X O . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A3()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 15, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 18) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(4, 17), new Point(3, 18), new Point(1, 18), new Point(0, 17), new Point(2, 18), new Point(0, 18), new Point(2, 18), new Point(5, 17), new Point(3, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";
            return g;
        }

        /*
 13 . . O . . . . . . . . . . . . . . . . 
 14 . . . . O O . . . . . . . . . . . . . 
 15 . . O O . X O . . . . . . . . . . . . 
 16 . . O X . X O . . . . . . . . . . . . 
 17 . O X . X X O . . . . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A4()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 15));
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 16), new Point(0, 17), new Point(2, 18), new Point(3, 17), new Point(4, 18), new Point(3, 17), new Point(4, 15), new Point(4, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A4_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A4_ChallengeMoveExtension");

            return g;
        }

        /*
 15 O O O O . . . . . . . . . . . . . . . 
 16 X . X . O . O . . . . . . . . . . . . 
 17 X . X . X O . . . . . . . . . . . . . 
 18 . . . . X . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A6()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(1, 16), new Point(1, 17), new Point(3, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]}]";
            return g;
        }

        /*
 14 O O O O . . . . . . . . . . . . . . . 
 15 . X X X O . . . . . . . . . . . . . . 
 16 . . . X O . . . . . . . . . . . . . . 
 17 . X . . O . . . . . . . . . . . . . . 
 18 . . . X O . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A7()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(2, 17), new Point(1, 18), new Point(2, 18), new Point(0, 16), new Point(1, 16), new Point(0, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}}]}]";

            return g;
        }

        /*
 11 . . . . . X . . . . . . . . . . . . . 
 12 . . X . X . . X . . . . . . . . . . . 
 13 . . . . . . O X . . . . . . . . . . . 
 14 . X X . O . . O X . . . . . . . . . . 
 15 . O O O . . . O X . . . . . . . . . . 
 16 . O . X O O O X . . . . . . . . . . . 
 17 . O X . X X X . X . . . . . . . . . . 
 18 . O . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q1970()
        {
            //GuanZiPu_A8
            //https://www.101weiqi.com/book/guanzipu/102/1970/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 12, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 3; x <= 6; x++)
            {
                for (int y = 14; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 13));
            gi.movablePoints.Add(new Point(5, 13));
            gi.movablePoints.Add(new Point(6, 13));

            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(0, 17));
            gi.movablePoints.Add(new Point(0, 18));
            gi.movablePoints.Add(new Point(5, 12));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(3, 13));
            gi.killMovablePoints.Add(new Point(6, 12));
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.Add(new Point(2, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 15), new Point(5, 15), new Point(6, 14), new Point(5, 14), new Point(3, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q1970_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q1970_ChallengeMoveExtension");

            return g;
        }
        /*
 13 . O . . . . . . . . . . . . . . . . . 
 14 . . O O O . . . . . . . . . . . . . . 
 15 . O . X X O . . . . . . . . . . . . . 
 16 . X . . X O . . . . . . . . . . . . . 
 17 . X . . X O . O . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A9()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(7, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(6, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 16), new Point(2, 17), new Point(4, 18), new Point(3, 18), new Point(2, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(2, 16), new Point(2, 17), new Point(4, 18), new Point(3, 18), new Point(2, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A9_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A9_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . O O O . O . O . . . . . . . . . . . 
 15 . O X X O . O . . . . . . . . . . . . 
 16 . X O X X X X O . . . . . . . . . . . 
 17 . X O . . . . O . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A12()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(2, 18), new Point(3, 17), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(3, 17), new Point(4, 17), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(1, 18), new Point(0, 15), new Point(3, 18), new Point(4, 18), new Point(4, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A12_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A12_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . . O . . . . . . . . . . . . . . . . 
 14 . O . . . . . . . . . . . . . . . . . 
 15 . X O O O O . . . . . . . . . . . . . 
 16 . X O X . . O . . . . . . . . . . . . 
 17 . . X . X X O . . . . . . . . . . . . 
 18 . . . . . . O . . . . . . . . . . . .
        */
        public Game Scenario_GuanZiPu_A14()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(0, 15), new Point(0, 18), new Point(3, 17), new Point(3, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(0, 15), new Point(0, 18), new Point(1, 17), new Point(1, 18), new Point(0, 14) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(0, 15), new Point(0, 18), new Point(1, 17), new Point(1, 18), new Point(3, 17), new Point(3, 18), new Point(5, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(0, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(3, 17), new Point(3, 18), new Point(5, 18), new Point(4, 16), new Point(4, 18), new Point(0, 15), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(1, 18), new Point(1, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(1, 17), new Point(0, 17), new Point(1, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(3, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A14_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A14_ChallengeMoveExtension");

            return g;
        }


        /*
 12 . . O . . . . . . . . . . . . . . . . 
 13 . . . O O . . . . . . . . . . . . . . 
 14 . O O X X O . . . . . . . . . . . . . 
 15 . O X . X O . . . . . . . . . . . . . 
 16 . O X . X O . . . . . . . . . . . . . 
 17 . X . . . O . . . . . . . . . . . . . 
 18 . . . X . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q18710()
        {
            //https://www.101weiqi.com/book/30126/21825/18710/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 15));
            gi.movablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.Add(new Point(6, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(0, 17), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(3, 16), new Point(3, 17), new Point(0, 17), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(3, 16), new Point(3, 17), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(3, 17), new Point(4, 17), new Point(0, 17), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(4, 17), new Point(3, 17), new Point(0, 17), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(4, 17), new Point(3, 17), new Point(3, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(4, 17), new Point(3, 17), new Point(3, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q18710_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q18710_ChallengeMoveExtension");
            return g;
        }


        /*
 14 . . . . . O O O . . . . . . . . . . . 
 15 . . . O O . X X O O . . . . . . . . . 
 16 . . O X X . X O X O . . . . . . . . . 
 17 . . O X . . X O X . O . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q19013()
        {
            //https://www.101weiqi.com/book/30126/21825/19013/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 15, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(10, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 15));
            gi.movablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(11, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(4, 18), new Point(5, 18), new Point(5, 16), new Point(2, 18), new Point(5, 15), new Point(5, 17), new Point(7, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q19013_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q19013_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . . . . O O . . . . . . . . . . . . 
 15 . O . O O X . O . . . . . . . . . . . 
 16 . . O . X . . X O . . . . . . . . . . 
 17 . O X . X . . X O . . . . . . . . . . 
 18 . . X . X . . . . . . . . . . . . . . 
        */
        public Game Scenario_GuanZiPu_Q19012()
        {
            //https://www.101weiqi.com/book/guanzipu/100/19012/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 15));
            gi.movablePoints.Add(new Point(6, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.Add(new Point(1, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(7, 18), new Point(6, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(7, 18), new Point(6, 18), new Point(1, 18), new Point(3, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(1, 18), new Point(3, 18), new Point(7, 18), new Point(6, 18), new Point(5, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(3, 17), new Point(7, 18), new Point(6, 18), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 17), new Point(7, 18), new Point(6, 18), new Point(5, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(3, 16), new Point(3, 17), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(1, 18), new Point(3, 17), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(3, 17), new Point(3, 16), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(3, 17), new Point(3, 16), new Point(1, 18), new Point(3, 18), new Point(5, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}}]}]";

            return g;
        }

        /*
 15 . O O O . . O O . . . . . . . . . . . 
 16 . O X X O O X X O . . . . . . . . . . 
 17 . . . . X X . X O . . . . . . . . . . 
 18 . . . . . X . X O . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q18796()
        {
            //https://www.101weiqi.com/book/guanzipu/97/18796/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 17), new Point(6, 17), new Point(1, 17), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 17), new Point(6, 17), new Point(1, 17), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 17), new Point(6, 17), new Point(1, 17), new Point(3, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 17), new Point(6, 18), new Point(1, 17), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 17), new Point(6, 18), new Point(1, 17), new Point(6, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 17), new Point(6, 18), new Point(1, 17), new Point(3, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";
            return g;
        }

        /*
 14 . . X . X X X . . . . . . . . . . . . 
 15 . . . X O O . X X . . . . . . . . . . 
 16 . . X O . . . O X . . . . . . . . . . 
 17 . . X O . O . O X . . . . . . . . . . 
 18 . . X O . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q2622()
        {
            //https://www.101weiqi.com/book/guanzipu/103/2622/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 4; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 16), new Point(6, 17), new Point(4, 18), new Point(7, 18), new Point(5, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]}]";
            return g;
        }

        /*
 14 . . . . . O . . . . . . . . . . . . . 
 15 . . . O O . . O . . . . . . . . . . . 
 16 . . O X . X X X O O O . . . . . . . . 
 17 . . O X . . X . X X O . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Weiqi101_19138()
        {
            //https://www.101weiqi.com/book/guanzipu/102/19138/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();


            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(10, 17, Content.White);


            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 10; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(11, 18));
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.Add(new Point(6, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(9, 18), new Point(8, 18), new Point(6, 18), new Point(10, 18), new Point(7, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(9, 18), new Point(8, 18), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(9, 18), new Point(8, 18), new Point(7, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(6, 18), new Point(9, 18), new Point(7, 18), new Point(3, 18), new Point(7, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 17), new Point(6, 18), new Point(9, 18), new Point(3, 18), new Point(4, 18), new Point(7, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(7, 18), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Weiqi101_19138_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Weiqi101_19138_ChallengeMoveExtension");
            return g;
        }



        /*
 13 . O . . . . . . . . . . . . . . . . . 
 14 . . O O . . . . . . . . . . . . . . . 
 15 . O X . O O . . . . . . . . . . . . . 
 16 O . X . X X O O . . . . . . . . . . . 
 17 O X . . X . X O . . . . . . . . . . . 
 18 . . . . . . X O . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A15()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(7, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(2, 15));
            gi.movablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(3, 17), new Point(3, 16), new Point(4, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";
            return g;
        }

        /*
 15 X X X X X . X . . . . . . . . . . . . 
 16 X O . O O X . . . . . . . . . . . . . 
 17 O O . . . O X . . . . . . . . . . . . 
 18 . . . . O . X . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A16()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.survivalPoints.Add(new Point(3, 16));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 17), new Point(4, 17), new Point(3, 17), new Point(5, 18), new Point(2, 18), new Point(4, 17) });


            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"BothAlive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"BothAlive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"BothAlive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"BothAlive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"BothAlive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]}]";
            return g;
        }


        /*
 14 . O . O O . . . . . . . . . . . . . . 
 15 . X O . X O . . . . . . . . . . . . . 
 16 . X O . X O . . . . . . . . . . . . . 
 17 . . X . X O . . . . . . . . . . . . . 
 18 . . . . X O . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A19()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(5, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(3, 17), new Point(0, 17), new Point(1, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]}]";
            return g;
        }

        /*
 13 . O O . . . . . . . . . . . . . . . . 
 14 X X O . O . . . . . . . . . . . . . . 
 15 . . X X . O . . . . . . . . . . . . . 
 16 . . . . O . . . . . . . . . . . . . . 
 17 . X . . O . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A20()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 17), new Point(2, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 18));
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 16), new Point(3, 16), new Point(2, 17), new Point(3, 17), new Point(1, 18), new Point(1, 16), new Point(3, 18) });
            gi.dictatePoints.Add(new List<Point>() { new Point(2, 16), new Point(3, 16), new Point(2, 17), new Point(3, 17), new Point(1, 18), new Point(3, 18), new Point(1, 16), new Point(0, 17), new Point(1, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A20_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A20_ChallengeMoveExtension");

            return g;
        }

        /*
 11 . . O O O . . . . . . . . . . . . . . 
 12 . O . X X O . . . . . . . . . . . . . 
 13 . O X . . . . . . . . . . . . . . . . 
 14 . O X X X O O . O . . . . . . . . . . 
 15 . . O X . X O . . . . . . . . . . . . 
 16 . . O X . X . . O . . . . . . . . . . 
 17 . . O O X . . . . O . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A25()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 3; x <= 6; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 12));
            gi.movablePoints.Add(new Point(3, 13));
            gi.movablePoints.Add(new Point(4, 13));

            gi.movablePoints.Add(new Point(7, 17));
            gi.movablePoints.Add(new Point(7, 18));
            gi.movablePoints.Add(new Point(8, 18));

            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 16));
            gi.killMovablePoints.Add(new Point(8, 17));
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.Add(new Point(5, 13));
            gi.killMovablePoints.Add(new Point(2, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 16), new Point(6, 17), new Point(4, 18), new Point(5, 18), new Point(5, 17) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A25_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A25_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . . X X X . . . . . . . . . . . . . . 
 15 X X O O O X . . . . . . . . . . . . . 
 16 O O O . O X . . . . . . . . . . . . . 
 17 . X X X O X . . . . . . . . . . . . . 
 18 . O . . O X . . . . . . . . . . . . .
        */
        public Game Scenario_GuanZiPu_B1()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 12);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(3, 18), new Point(0, 17), new Point(0, 18), new Point(1, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17}},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17}},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]}]";
            return g;
        }

        /*
 12 . . X X X X X . . . . . . . . . . . . 
 13 . X . . . . . X . . . . . . . . . . . 
 14 . X O . . . . X . . . . . . . . . . . 
 15 . X O X . X . X . . . . . . . . . . . 
 16 . O O O O O O X . . . . . . . . . . . 
 17 . O . X X X X . . . . . . . . . . . . 
 18 . O X . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_B7()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 12, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 2; x <= 6; x++)
            {
                for (int y = 13; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(0, 17));
            gi.movablePoints.Add(new Point(0, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(2, 17));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 14), new Point(4, 14), new Point(4, 13), new Point(3, 13), new Point(3, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B7_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B7_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . . . . O O . . . . . . . . . . . 
 14 . . O . O O X X O . . . . . . . . . . 
 15 . . . O X X . X O . . . . . . . . . . 
 16 . . O X . . X X O . . . . . . . . . . 
 17 . . O X . O X O O . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_B15()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 3; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(6, 18), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B15_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B15_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . X X X . . . . . . . . . . . . . . 
 14 . X O O O X X . . . . . . . . . . . . 
 15 . X O . O O X . . . . . . . . . . . . 
 16 . X O O X X O O O . . . . . . . . . . 
 17 . X O . . . X . . . O . . . . . . . . 
 18 . X . O X . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_B18()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 16);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(10, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(4, 16) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 15));
            gi.movablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 17));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.survivalPoints.Add(new Point(2, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(5, 17), new Point(6, 18), new Point(7, 17), new Point(8, 17), new Point(7, 18), new Point(4, 17), new Point(2, 18), new Point(5, 18), new Point(4, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(5, 17), new Point(6, 18), new Point(7, 17), new Point(4, 17), new Point(2, 18), new Point(5, 18), new Point(7, 18), new Point(8, 17), new Point(4, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(5, 17), new Point(6, 18), new Point(7, 17), new Point(4, 17), new Point(2, 18), new Point(5, 18), new Point(7, 18), new Point(8, 18), new Point(4, 18), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B18_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B18_ChallengeMoveExtension");
            return g;
        }

        /*
  9 . O O O . . . . . . . . . . . . . . . 
 10 . X X X O . . . . . . . . . . . . . . 
 11 . X O X O . . . . . . . . . . . . . . 
 12 . . . . X O . . . . . . . . . . . . . 
 13 . . X O O . . . . . . . . . . . . . . 
 14 . X X X O . . . . . . . . . . . . . . 
 15 . O . . O . . . . . . . . . . . . . . 
 16 . O . O . . . . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A145_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/18456/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.Black);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 9, Content.White);
            g.SetupMove(2, 10, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(3, 9, Content.White);
            g.SetupMove(3, 10, Content.Black);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 10, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 12, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 14) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 9; y <= 14; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(4, 12));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 12), new Point(3, 12), new Point(0, 14), new Point(0, 13), new Point(1, 12) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A145_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A145_101Weiqi_ChallengeMoveExtension");
            return g;
        }

        /*
 13 X X X X . . . . . . . . . . . . . . . 
 14 . O O . X . . . . . . . . . . . . . . 
 15 . . . . X . . . . . . . . . . . . . . 
 16 . O . O X . . . . . . . . . . . . . . 
 17 . . O X X . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_B3Q63_101Weiqi()
        {
            //Scenario_GuanZiPu_A10
            //https://www.101weiqi.com/book/guanzipu/103/2310/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 15), new Point(0, 17), new Point(3, 15), new Point(3, 14), new Point(2, 16), new Point(1, 15), new Point(2, 15), new Point(0, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B3Q63_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B3Q63_101Weiqi_ChallengeMoveExtension");

            return g;
        }

        /*
 14 X X X X X . . . . . . . . . . . . . . 
 15 O O O O . X . . . . . . . . . . . . . 
 16 O X . . O X . . . . . . . . . . . . . 
 17 X X O O O X . . . . . . . . . . . . . 
 18 . . . . X X . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A2Q71_101Weiqi()
        {
            //https://www.101weiqi.com/book/guanzipu/95/959/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 18), new Point(2, 18), new Point(2, 16), new Point(0, 18), new Point(1, 18), new Point(1, 17), new Point(0, 17), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(2, 16), new Point(3, 18), new Point(1, 18), new Point(0, 18), new Point(1, 18), new Point(1, 17), new Point(0, 17), new Point(0, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]}]";
            return g;
        }

        /*
 11 . . O . . . . . . . . . . . . . . . . 
 12 . . . . . . . . . . . . . . . . . . . 
 13 . . . . . . . . . . . . . . . . . . . 
 14 . O O O O . . . . . . . . . . . . . . 
 15 . . X X O . . . . . . . . . . . . . . 
 16 . . . X O . . . . . . . . . . . . . . 
 17 . X . X O . O . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_SiHuoDaQuan_CornerA117()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(6, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 14; y <= 18; y++)
                {
                    if (g.Board[x, y] == Content.Empty)
                        gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(1, 17));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(5, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 18), new Point(1, 16), new Point(1, 15), new Point(0, 15) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetMappedJsonExtensionString("Scenario_SiHuoDaQuan_CornerA117_PlayerMoveExtension");

            return g;
        }

        /*
 13 . O O O O . . . . . . . . . . . . . . 
 14 . . X X . O . . . . . . . . . . . . . 
 15 X . . . X O . . . . . . . . . . . . . 
 16 . . X X . O . . . . . . . . . . . . . 
 17 O X O O O . . . . . . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A13_Ext()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);


            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 13; y <= 17; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 18));
            gi.movablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(1, 14), new Point(0, 16), new Point(0, 14), new Point(1, 16), new Point(2, 15), new Point(2, 18), new Point(0, 18), new Point(1, 16) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":12},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}}]}]";

            return g;
        }

        /*
 14 . X X X . . . . . . . . . . . . . . . 
 15 . X O O X . . . . . . . . . . . . . . 
 16 . O . O X . X . . . . . . . . . . . . 
 17 . . . O O X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
        */
        public Game Scenario_XuanXuanGo_A15()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/15823/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.Add(new Point(0, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(0, 16), new Point(1, 17), new Point(2, 17), new Point(0, 15), new Point(2, 16), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A15_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A15_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . O . . . . . . . . . . . . . . . . . 
 14 . . . O O . . . . . . . . . . . . . . 
 15 . O O X X O . . . . . . . . . . . . . 
 16 . X X . X O . . . . . . . . . . . . . 
 17 . . . . X O . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A16()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(6, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(2, 18), new Point(4, 18), new Point(1, 17), new Point(0, 17), new Point(2, 17), new Point(1, 18), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(2, 18), new Point(4, 18), new Point(1, 17), new Point(0, 17), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(2, 18), new Point(4, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(4, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 18), new Point(2, 18), new Point(4, 18), new Point(5, 18), new Point(3, 16), new Point(1, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A16_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A16_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . X X X . . . . . . . . . . . . . . . 
 14 . O O . X . . . . . . . . . . . . . . 
 15 . . . O X . . . . . . . . . . . . . . 
 16 . . O X . . . . . . . . . . . . . . . 
 17 . . O X . X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A2Q28_101Weiqi()
        {
            //Scenario_GuanZiPu_A5
            //https://www.101weiqi.com/book/guanzipu/95/2371/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.Add(new Point(4, 18));


            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(1, 16), new Point(1, 15), new Point(0, 17), new Point(3, 14), new Point(3, 18), new Point(2, 18), new Point(0, 15), new Point(0, 14), new Point(0, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(1, 16), new Point(1, 15), new Point(0, 17), new Point(3, 14), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(1, 16), new Point(1, 15), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(0, 17), new Point(1, 16), new Point(0, 14) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(2, 15), new Point(0, 15), new Point(0, 14), new Point(1, 18), new Point(2, 18), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(2, 15), new Point(0, 15), new Point(0, 14), new Point(0, 17), new Point(1, 16), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(2, 15), new Point(1, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(2, 15), new Point(2, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(1, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(0, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 15), new Point(2, 18), new Point(1, 18), new Point(0, 17), new Point(0, 13), new Point(3, 14), new Point(2, 15), new Point(1, 16), new Point(1, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 15), new Point(2, 18), new Point(1, 18), new Point(1, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 15), new Point(1, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 15), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(0, 15), new Point(0, 16) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(2, 18), new Point(1, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(2, 18), new Point(1, 18), new Point(3, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(2, 18), new Point(1, 18), new Point(0, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(2, 18), new Point(1, 18), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(0, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(0, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A2Q28_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A2Q28_101Weiqi_ChallengeMoveExtension");
            return g;
        }

        /*
 14 X X X . X . . . . . . . . . . . . . . 
 15 . O . X . . . . . . . . . . . . . . . 
 16 . O . O X . . . . . . . . . . . . . . 
 17 . . . O X . . . . . . . . . . . . . . 
 18 . . O . X . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A18()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 15), new Point(2, 16), new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(0, 15), new Point(3, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]}]";
            return g;
        }


        /*
 14 . O O . O . . . . . . . . . . . . . . 
 15 . X . O . O . . . . . . . . . . . . . 
 16 . X . X X O . . . . . . . . . . . . . 
 17 X X . . . O . . . . . . . . . . . . . 
 18 . . . . . O . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_A28()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 20);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(5, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 18), new Point(1, 18), new Point(2, 17), new Point(0, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A28_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A28_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . X X X . . . . . . . . . . . . . . . 
 15 . O O O X . . . . . . . . . . . . . . 
 16 . . X O X . . . . . . . . . . . . . . 
 17 . . O X O X X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A30()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(0, 15), new Point(3, 18), new Point(2, 18), new Point(0, 16), new Point(4, 18), new Point(3, 17), new Point(3, 18), new Point(0, 14), new Point(3, 17), new Point(1, 17), new Point(1, 18), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(0, 15), new Point(3, 18), new Point(2, 18), new Point(0, 14), new Point(4, 18), new Point(3, 17), new Point(3, 18), new Point(0, 16), new Point(3, 17), new Point(1, 17), new Point(1, 18), new Point(0, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":15}},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15}}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A30_PlayerMoveExtension");
            return g;
        }

        /*
 14 . O . O O . O . . . . . . . . . . . . 
 15 . . O X . X O . O . . . . . . . . . . 
 16 O O X X . . X X O . . . . . . . . . . 
 17 O X . X . X O O . O . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A32()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 1; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 15));
            gi.movablePoints.Add(new Point(0, 18));
            gi.movablePoints.Add(new Point(5, 15));
            gi.movablePoints.Add(new Point(7, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 14));
            gi.killMovablePoints.Add(new Point(7, 15));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(3, 18), new Point(4, 15), new Point(5, 16), new Point(4, 16), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(3, 18), new Point(4, 15), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(3, 18), new Point(4, 15), new Point(5, 16), new Point(4, 16), new Point(0, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";
            return g;
        }

        /*
 13 . . . . . O O . . . . . . . . . . . . 
 14 . . . O O X X O O . . . . . . . . . . 
 15 . . O . X . . X O . . . . . . . . . . 
 16 . . O X . . X X O . . . . . . . . . . 
 17 . . O X . X . . O . . . . . . . . . . 
 18 . . . . . . . O . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_Q18472()
        {
            //https://www.101weiqi.com/book/1349/3037/18472/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.Add(new Point(1, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 15), new Point(6, 15), new Point(4, 18), new Point(4, 17), new Point(5, 16), new Point(3, 18), new Point(5, 18), new Point(6, 18), new Point(6, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}}]}]";
            return g;
        }

        /*
 12 . . X X X X . . . . . . . . . . . . . 
 13 . X O O O X X . . . . . . . . . . . . 
 14 . X O . . O X . . . . . . . . . . . . 
 15 . X O O . . O X . . . . . . . . . . . 
 16 . . X O . . O X . . . . . . . . . . . 
 17 . X . X O . O X . X . . . . . . . . . 
 18 . . . X . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A30()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(9, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 3; x <= 6; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 14), new Point(5, 15), new Point(4, 16), new Point(4, 15), new Point(5, 17), new Point(5, 16), new Point(5, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"KoAlive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Alive\"}]}]";
            //Total time taken: 89885
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"}]}]";

            return g;

        }


        /*
 14 . . . X X . . . . . . . . . . . . . . 
 15 . . . X O X X X X X . . . . . . . . . 
 16 . . X . O . . O O X . . . . . . . . . 
 17 . . X O . . O . O O X . . . . . . . . 
 18 . . . . . . . . . . X . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A31()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(7, 16), new Point(3, 17) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(5, 16), new Point(5, 18), new Point(4, 18), new Point(6, 16), new Point(7, 18), new Point(9, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A31_PlayerMoveExtension");

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":9,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":9,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":9,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":9,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}}]}]";
            return g;
        }

        /*
 12 . . X . . . . . . . . . . . . . . . . 
 13 . X . X . . . . . . . . . . . . . . . 
 14 . X O X . . . . . . . . . . . . . . . 
 15 . . O X . X . X . . . . . . . . . . . 
 16 . . O O X . . . X . . . . . . . . . . 
 17 . . . O X O . X . . . . . . . . . . . 
 18 . . . . O . . . . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanGo_Q18340()
        {
            //https://www.101weiqi.com/book/1349/3037/18340/
            var gi = new GameInfo(SurviveOrKill.SurviveWithKo, Content.White, 16);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 18));
            gi.movablePoints.Add(new Point(5, 18));
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(5, 17));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(2, 13));
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(5, 16));
            gi.killMovablePoints.Add(new Point(6, 17));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(1, 18), new Point(2, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_Q18340_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_Q18340_ChallengeMoveExtension");

            return g;
        }


        /*
 11 . O . . . . . . . . . . . . . . . . . 
 12 . . . . . . . . . . . . . . . . . . . 
 13 . O O . . . . . . . . . . . . . . . . 
 14 . X . O . . . . . . . . . . . . . . . 
 15 . . X O . . . . . . . . . . . . . . . 
 16 . . X O . . . . . . . . . . . . . . . 
 17 . . X O . O . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A4Q11_101Weiqi()
        {
            //https://www.101weiqi.com/book/guanzipu/97/4235/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 11, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(5, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.Add(new Point(4, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(1, 18), new Point(0, 17), new Point(1, 16), new Point(2, 14), new Point(0, 13), new Point(0, 15), new Point(0, 14), new Point(0, 16), new Point(1, 15), new Point(3, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A4Q11_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A4Q11_101Weiqi_ChallengeMoveExtension");
            return g;
        }

        /*
 11 . . . . . X X . . . . . . . . . . . . 
 12 . X X X X O . X . . . . . . . . . . . 
 13 . O O O O . O X . . . . . . . . . . . 
 14 . . . . . . O X . . . . . . . . . . . 
 15 . O O O O O X X . . . . . . . . . . . 
 16 . X X X X X . . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A48()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 11, Content.Black);
            g.SetupMove(6, 13, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(7, 12, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 12; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 11));
            gi.killMovablePoints.Add(new Point(0, 17));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 14), new Point(6, 12), new Point(5, 14), new Point(5, 13), new Point(4, 14), new Point(0, 15), new Point(0, 13) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A48_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A48_ChallengeMoveExtension");

            return g;
        }



        /*
 12 . . X . X X . . . . . . . . . . . . . 
 13 . . . X O . X . . . . . . . . . . . . 
 14 . . X O . O X . . . . . . . . . . . . 
 15 . . X O . O X . . . . . . . . . . . . 
 16 . . X O . O X X X . . . . . . . . . . 
 17 . X X O . . O O O X X . . . . . . . . 
 18 . X O . . . . . X . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_A40()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 3; x <= 5; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            for (int x = 6; x <= 9; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(4, 17), new Point(3, 18), new Point(5, 17), new Point(4, 16), new Point(6, 18), new Point(7, 18), new Point(5, 17), new Point(9, 18), new Point(5, 18), new Point(4, 17), new Point(5, 13) });

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(4, 17), new Point(3, 18), new Point(5, 17), new Point(4, 16), new Point(6, 18), new Point(7, 18), new Point(5, 17), new Point(9, 18), new Point(5, 18), new Point(4, 17), new Point(4, 14) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":10,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}}]},{\"FirstMove\":{\"x\":5,\"y\":13},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":13},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A40_PlayerMoveExtension");

            return g;
        }


        /*
 10 . O . . . . . . . . . . . . . . . . . 
 11 . . . . . . . . . . . . . . . . . . . 
 12 . O . O O O . . . . . . . . . . . . . 
 13 . O X X X . . . . . . . . . . . . . . 
 14 . X . . X X O . . . . . . . . . . . . 
 15 . . O O O X O . . . . . . . . . . . . 
 16 . X X X X O . . . . . . . . . . . . . 
 17 . X O O O . O . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_Weiqi101_A40()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 10, Content.White);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 13; y <= 17; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 18));
            gi.movablePoints.Add(new Point(1, 18));
            gi.movablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 18));
            gi.killMovablePoints.Add(new Point(2, 12));
            gi.killMovablePoints.Add(new Point(5, 13));
            gi.killMovablePoints.Add(new Point(0, 12));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 18), new Point(1, 15), new Point(0, 15), new Point(0, 16), new Point(0, 18), new Point(0, 16) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":2,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":12}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_Weiqi101_A40_PlayerMoveExtension");
            return g;
        }

        /*
 14 . . . X X X . . . . . . . . . . . . . 
 15 . . X . O O X X X . . . . . . . . . . 
 16 . . . . O X O O O X X . X . . . . . . 
 17 . . X . O X . . . O O X . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A55()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.White);
            g.SetupMove(11, 17, Content.Black);
            g.SetupMove(12, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(6, 16) };
            for (int x = 3; x <= 11; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(5, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(3, 16));
            gi.killMovablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.Add(new Point(12, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(7, 17), new Point(8, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(5, 18), new Point(3, 17), new Point(3, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(7, 17), new Point(8, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(5, 18), new Point(3, 17), new Point(3, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(7, 17), new Point(8, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(5, 18), new Point(3, 17), new Point(2, 18) });


            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A55_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A55_ChallengeMoveExtension");
            return g;
        }


        /*
 14 . X . X . . . . . . . . . . . . . . . 
 15 . X . . . X X . . . . . . . . . . . . 
 16 X O O O O O X . . . . . . . . . . . . 
 17 . X O . . . O X . . . . . . . . . . . 
 18 . . O . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_Q14981()
        {
            //https://www.101weiqi.com/book/guanzipu/107/14981/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(4, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(8, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(0, 17), new Point(0, 18), new Point(1, 18), new Point(0, 17), new Point(0, 15), new Point(0, 17), new Point(4, 17), new Point(0, 16), new Point(5, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q14981_PlayerMoveExtension");
            return g;
        }


        /*
 12 . . X . . . . . . . . . . . . . . . . 
 13 . X . . . . . . . . . . . . . . . . . 
 14 . . . X X . . . . . . . . . . . . . . 
 15 O O O O X O . . . . . . . . . . . . . 
 16 . . X X O . O . . . . . . . . . . . . 
 17 . X . X O . . . . . . . . . . . . . . 
 18 O . X . O . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_Q18860()
        {
            //https://www.101weiqi.com/book/guanzipu/98/18860/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 18, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 14));
            gi.movablePoints.Add(new Point(2, 14));
            gi.survivalPoints.Add(new Point(1, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(3, 18), new Point(0, 17), new Point(2, 14), new Point(1, 18), new Point(0, 16), new Point(1, 18), new Point(0, 18), new Point(0, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16}},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]}]";
            return g;
        }

        /*
 13 . X . X . . . . . . . . . . . . . . . 
 14 . O X . . . . . . . . . . . . . . . . 
 15 . X O X X . . . . . . . . . . . . . . 
 16 . X O O O X X . . . . . . . . . . . . 
 17 . O . . . O X . . . . . . . . . . . . 
 18 . . . . . O . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q14906()
        {
            //https://www.101weiqi.com/book/guanzipu/98/14906/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(5, 18, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 14));
            gi.movablePoints.Add(new Point(0, 13));
            gi.movablePoints.Add(new Point(5, 17));
            gi.movablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.Add(new Point(6, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(0, 16), new Point(0, 14), new Point(2, 18), new Point(3, 18), new Point(3, 17), new Point(2, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(0, 16), new Point(0, 14), new Point(2, 18), new Point(0, 15), new Point(0, 17), new Point(3, 18), new Point(3, 17), new Point(2, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(0, 16), new Point(3, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q14906_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q14906_ChallengeMoveExtension");
            return g;
        }

        /*
 13 X X X X . . X . . . . . . . . . . . . 
 14 O O O O X . . . . . . . . . . . . . . 
 15 . . . O X . X . . . . . . . . . . . . 
 16 . X O X X O X . . . . . . . . . . . . 
 17 . . O O X O . X . . . . . . . . . . . 
 18 . . . . O . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_Q14971()
        {
            //https://www.101weiqi.com/book/30126/21825/14971/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 18));
            gi.movablePoints.Add(new Point(5, 18));
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(5, 17));
            gi.movablePoints.Add(new Point(5, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.Add(new Point(6, 17));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(0, 17), new Point(1, 15), new Point(3, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(6, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(0, 17), new Point(0, 16), new Point(0, 15) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 17), new Point(1, 18), new Point(0, 16), new Point(0, 15), new Point(2, 15), new Point(3, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(1, 17), new Point(3, 18), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(1, 17), new Point(3, 18), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(1, 17), new Point(3, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(1, 17), new Point(3, 18), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(1, 17), new Point(3, 18), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(2, 15), new Point(1, 17), new Point(3, 18), new Point(5, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(0, 17) });

            //gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(1, 17), new Point(1, 18), new Point(1, 15), new Point(2, 15), new Point(0, 17), new Point(0, 15), new Point(3, 18), new Point(0, 18) })); //eternal life

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q14971_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_Q14971_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . O . O . . . . . . . . . . . . . . 
 15 . . . O . . O . . . . . . . . . . . . 
 16 O O O . X X O . . . . . . . . . . . . 
 17 X X X . . . . . O . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A44_101Weiqi()
        {
            //https://www.101weiqi.com/book/1349/3037/19187/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(8, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 17; y <= 18; y++)
                {
                    if (g.Board[x, y] == Content.Empty)
                        gi.movablePoints.Add(new Point(x, y));
                }
            }

            gi.movablePoints.Add(new Point(3, 16));
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 17));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(5, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 16), new Point(3, 18), new Point(3, 17), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 16), new Point(3, 18), new Point(3, 17), new Point(4, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 16), new Point(4, 18), new Point(3, 18), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 16), new Point(4, 18), new Point(3, 18), new Point(6, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(1, 18), new Point(3, 16), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(1, 18), new Point(3, 16), new Point(6, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A44_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A44_101Weiqi_ChallengeMoveExtension");

            return g;
        }

        /*
 15 O O O O O O . O . . . . . . . . . . . 
 16 . X . . X . O . . . . . . . . . . . . 
 17 . . X . . X X O O . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A17()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(3, 17), new Point(3, 16), new Point(2, 16) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(3, 16), new Point(3, 17), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(3, 16), new Point(3, 17), new Point(6, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(5, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(2, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(6, 18) });


            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(3, 17), new Point(1, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A17_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A17_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . O O O . . . . . . . . . . . . . . . 
 15 . X X X O . . . . . . . . . . . . . . 
 16 . . . O O O . O . . . . . . . . . . . 
 17 . . X X X X O . O . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A18()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(5, 18));
            gi.movablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(1, 17), new Point(2, 16), new Point(5, 18), new Point(4, 18), new Point(2, 18), new Point(6, 18), new Point(1, 18), new Point(0, 16), new Point(0, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(1, 17), new Point(2, 16), new Point(5, 18), new Point(4, 18), new Point(2, 18), new Point(6, 18), new Point(0, 18), new Point(0, 16), new Point(1, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(1, 17), new Point(2, 16), new Point(5, 18), new Point(4, 18), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(1, 17), new Point(2, 16), new Point(5, 18), new Point(4, 18), new Point(2, 18), new Point(6, 18), new Point(0, 15) });


            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(1, 17), new Point(2, 16), new Point(0, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(0, 15), new Point(0, 16), new Point(1, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A18_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A18_ChallengeMoveExtension");

            return g;
        }


        /*
 12 . O . O . . O O . . . . . . . . . . . 
 13 . . O X O O X X O . . . . . . . . . . 
 14 . O X X . X . . O . . . . . . . . . . 
 15 O O X . . . X X O . . . . . . . . . . 
 16 . X . X X X O O . . . . . . . . . . . 
 17 . X X O O O . . . . . . . . . . . . . 
 18 . . . O . . . . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A26()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 14);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 12, Content.White);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 13, Content.White);
            g.SetupMove(8, 14, Content.White);
            g.SetupMove(8, 15, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            for (int x = 3; x <= 7; x++)
            {
                for (int y = 13; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 15), new Point(4, 14), new Point(5, 15), new Point(3, 15), new Point(5, 15), new Point(6, 14), new Point(0, 17), new Point(0, 16), new Point(1, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 15), new Point(4, 14), new Point(5, 15), new Point(3, 15), new Point(5, 15), new Point(6, 14), new Point(1, 18), new Point(2, 18), new Point(0, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}}]},{\"FirstMove\":{\"x\":7,\"y\":14},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":14}}]}]";
            return g;
        }

        /*
 12 . . . . . O O . . . . . . . . . . . . 
 13 . . . O O X X O O . . . . . . . . . . 
 14 . . O . X . . X . O . . . . . . . . . 
 15 . . . O O X X . . O . . . . . . . . . 
 16 . . O X X X . . . O . . . . . . . . . 
 17 . . . O O . X X X X O . . . . . . . . 
 18 . . . . . X . . . O . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A29()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 14);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 12, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 13, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 14, Content.White);
            g.SetupMove(9, 15, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.White);
            g.SetupMove(10, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 4; x <= 8; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.Add(new Point(3, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 16), new Point(8, 16), new Point(7, 15), new Point(8, 15), new Point(8, 14), new Point(6, 16), new Point(7, 15), new Point(5, 14), new Point(6, 14) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":8,\"y\":14},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":15},\"SecondMove\":{\"x\":7,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":8,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":14},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}}]},{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":14}}]},{\"FirstMove\":{\"x\":8,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":7,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}}]},{\"FirstMove\":{\"x\":6,\"y\":14},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":14}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}}]},{\"FirstMove\":{\"x\":8,\"y\":14},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":8,\"y\":15},\"SecondMove\":{\"x\":8,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":8,\"y\":14},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":14},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":14}}]}]";

            //gi.PlayerMoveJsonExtension = ResourceHelper.GetMappedJsonString("Scenario_GuanZiPu_A29_PlayerMoveExtension");
            //gi.ChallengeMoveJsonExtension = ResourceHelper.GetMappedJsonString("Scenario_GuanZiPu_A29_ChallengeMoveExtension");

            return g;
        }


        /*
 13 . . . X X X X . . . . . . . . . . . . 
 14 . X X O O . O X X . . . . . . . . . . 
 15 . . X O . . O . O X . . . . . . . . . 
 16 . X O O . O . . O X . . . . . . . . . 
 17 . X O . . O . . O X . . . . . . . . . 
 18 . X X X X X . O O X . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A27()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 17), new Point(6, 18), new Point(6, 16), new Point(7, 16), new Point(4, 16), new Point(4, 17), new Point(5, 15), new Point(4, 15), new Point(5, 14) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16},\"Result\":\"Alive\"}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":14},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14},\"Result\":\"Dead\"},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18},\"Result\":\"Dead\"}]}]";

            return g;
        }

        /*
 13 . X X . . . . . . . . . . . . . . . . 
 14 . O . . X . . . . . . . . . . . . . . 
 15 . O O O . . . . . . . . . . . . . . . 
 16 . X X O . X X . . . . . . . . . . . . 
 17 . . . X O O X . . . . . . . . . . . . 
 18 . . . X . . . . . . . . . . . . . . .
        */
        public Game Scenario_GuanZiPu_B3()
        {
            //https://www.101weiqi.com/q/78738/
            var gi = new GameInfo(SurviveOrKill.SurviveWithKo, Content.White, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 18));
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(4, 16));
            gi.movablePoints.Add(new Point(4, 18));
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.Add(new Point(2, 14));
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(0, 17), new Point(4, 16), new Point(2, 17), new Point(2, 18), new Point(0, 16) });

            gi.RuntimeScript_SurvivalMove = @"using System;
                                    using Go;
                                    using System.Collections.Generic;
                                     public class Script
                                     {
                                         public List<GameTryMove> ScriptReducedList(List<GameTryMove> sortedGameTryMoves, Game currentGame, Game m = null)
                                         {

if (currentGame.Board[1, 16] == Content.Black && LifeCheck.ConfirmAlive(currentGame.Board, new List<Point>() { new Point(1, 16)}) == ConfirmAliveResult.Alive)
    sortedGameTryMoves.Clear(); //killer group is alive - survival group is dead

    return sortedGameTryMoves;
                                         }
                                     }";

            gi.RuntimeScript_KillMove = @"using System;
                                    using Go;
                                    using System.Collections.Generic;
                                     public class Script
                                     {
                                         public List<GameTryMove> ScriptReducedList(List<GameTryMove> sortedGameTryMoves, Game currentGame, Game m = null)
                                         {

if (currentGame.Board[1, 16] == Content.Empty && currentGame.Board[3, 17] == Content.Empty)
    sortedGameTryMoves.Clear(); //killer group is dead = survival group is alive

    return sortedGameTryMoves;
                                         }
                                     }";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B3_PlayerMoveExtension");

            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_B3_ChallengeMoveExtension");
            return g;
        }


        /*
 14 . . . . X X X . . . . . . . . . . . . 
 15 . X X X O O . X X X . . . . . . . . . 
 16 . X . O . . . O O X . . . . . . . . . 
 17 . X O . . O . O . X . . . . . . . . . 
 18 . X . . . . . . X . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16525()
        {
            //https://www.101weiqi.com/book/tianlongtu/31/16525/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 18, Content.Black);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 17));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(2, 18), new Point(6, 18), new Point(4, 17), new Point(3, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(2, 18), new Point(6, 18), new Point(6, 15), new Point(6, 16), new Point(4, 17), new Point(3, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(2, 18), new Point(6, 18), new Point(6, 16), new Point(6, 15), new Point(4, 17), new Point(3, 17), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16525_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16525_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . X X X X . X . . . . . . . . . . . 
 15 . . X O O . X . . . . . . . . . . . . 
 16 . X O . . . O X X . . . . . . . . . . 
 17 . X O . O . O O X . . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_Nie137()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 16), new Point(4, 16), new Point(3, 17), new Point(3, 18), new Point(2, 18), new Point(3, 16), new Point(5, 17), new Point(5, 15), new Point(5, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetMappedJsonExtensionString("Scenario_Nie137_PlayerMoveExtension");

            return g;
        }

        /*
 13 . . . O . . . . . . . . . . . . . . . 
 14 O O O . . . . . . . . . . . . . . . . 
 15 X X . . . . . . . . . . . . . . . . . 
 16 X X O O O O O O . . . . . . . . . . . 
 17 . . X X X X X X O O O . . . . . . . . 
 18 O O O . . . . O . . . . . . . . . . . 
         */
        public Game Scenario_XuanXuanQiJing_A38()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.White);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 18, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 8; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(1, 15));
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(1, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(6, 18), new Point(1, 17), new Point(8, 18), new Point(2, 15), new Point(3, 18), new Point(0, 17), new Point(1, 16), new Point(0, 16), new Point(0, 15), new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(1, 15), new Point(0, 18), new Point(1, 17), new Point(1, 18), new Point(0, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A38_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_A38_ChallengeMoveExtension");
            return g;
        }


        /*
 14 O O . . . . . . . . . . . . . . . . . 
 15 . X O . . . . . O . . . . . . . . . . 
 16 . X O O O O O O . . . . . . . . . . . 
 17 . O X X X X X X O O O . . . . . . . . 
 18 . . . . . . . O . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_B36()
        {
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 20);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 8; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(1, 15));
            gi.movablePoints.Add(new Point(1, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 18), new Point(0, 16), new Point(6, 18), new Point(5, 18), new Point(2, 18), new Point(8, 18), new Point(3, 18), new Point(4, 18), new Point(0, 15), new Point(7, 18), new Point(0, 18), new Point(1, 16) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanQiJing_B36_PlayerMoveExtension");

            return g;
        }


        /*
 12 . X . X . . . . . . . . . . . . . . . 
 13 . . . X . X . X . . . . . . . . . . . 
 14 . O O . . . . . X . . . . . . . . . . 
 15 . . O O O O O O X . . . . . . . . . . 
 16 O O X X X X O X O O . O . . . . . . . 
 17 X X . . . X O X . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanQiJing_B25()
        {
            //https://www.101weiqi.com/book/xuanxuanqijin/90/18366/
            //https://www.101weiqi.com/book/guanzipu/111/18366/

            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();


            g.SetupMove(0, 16, Content.White);
            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(11, 16, Content.White);


            gi.targetPoints = new List<Point>() { new Point(7, 17) };
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 17));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(7, 18), new Point(2, 17), new Point(3, 17), new Point(2, 18), new Point(3, 18), new Point(5, 18), new Point(4, 18), new Point(8, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(7, 18), new Point(2, 17), new Point(3, 17), new Point(5, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(8, 18), new Point(8, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(8, 18), new Point(2, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(7, 18), new Point(2, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(3, 17), new Point(8, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(3, 17), new Point(7, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(3, 17), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(3, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(3, 17), new Point(8, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_XuanXuanQiJing_B25_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_XuanXuanQiJing_B25_ChallengeMoveExtension");

            return g;
        }


        /*
 14 . . . . . O . O . . . . . . . . . . . 
 15 . . O O . O . O . . . . . . . . . . . 
 16 . O X X X X . X O . . . . . . . . . . 
 17 . O X . . . . X O . O . . . . . . . . 
 18 . . X . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A35()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(10, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(2, 18) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(6, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(6, 14));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(6, 16), new Point(6, 17), new Point(7, 18), new Point(6, 18), new Point(5, 18), new Point(6, 15), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(6, 18), new Point(6, 16), new Point(6, 17), new Point(5, 18), new Point(6, 15), new Point(4, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A35_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A35_ChallengeMoveExtension");
            return g;
        }


        /*
 15 . . O . . O O O O . . . . . . . . . . 
 16 . . . O O X X X X O O . O . . . . . . 
 17 . . O . X . . . . X . O . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A36()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(11, 17, Content.White);
            g.SetupMove(12, 16, Content.White);

            gi.targetPoints = new List<Point>() { new Point(6, 16) };
            for (int x = 2; x <= 11; x++)
            {
                for (int y = 17; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(12, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(10, 18), new Point(6, 17), new Point(7, 17), new Point(8, 18), new Point(10, 17), new Point(11, 18), new Point(8, 17), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(10, 18), new Point(6, 17), new Point(7, 17), new Point(8, 18), new Point(10, 17), new Point(5, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(7, 17), new Point(6, 17), new Point(5, 18), new Point(3, 17), new Point(2, 18), new Point(5, 17), new Point(8, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(7, 17), new Point(6, 17), new Point(5, 18), new Point(3, 17), new Point(8, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A36_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A36_ChallengeMoveExtension");
            return g;
        }



        /*
 15 . . . . O O O O O . . . . . . . . . . 
 16 . . . O X . . X X O O O . . . . . . . 
 17 . . O O X . X . . X . . O . . . . . . 
 18 . . O X X . . . . . . . . . . . . . .
         */
        public Game Scenario_GuanZiPu_A37()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);

            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.White);
            g.SetupMove(11, 16, Content.White);
            g.SetupMove(12, 17, Content.White);

            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 3; x <= 11; x++)
            {
                for (int y = 16; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(12, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(13, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(7, 17), new Point(6, 18), new Point(5, 16), new Point(6, 16), new Point(7, 18), new Point(5, 18), new Point(5, 17), new Point(9, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 18), new Point(7, 18), new Point(8, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A37_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A37_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . O O . . . . . . . . . . . . . . . . 
 14 . . . O . . . . . . . . . . . . . . . 
 15 . X X O . . . . . . . . . . . . . . . 
 16 . . O X O O O . O . . . . . . . . . . 
 17 . . O X X X X O . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A54()
        {
            //https://www.101weiqi.com/q/19204/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.White);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            gi.survivalPoints.Add(new Point(1, 15));
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(7, 18));
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 15));
            gi.movablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(1, 14));
            gi.killMovablePoints.Add(new Point(2, 14));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.survivalPoints.Add(new Point(1, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 16), new Point(1, 17), new Point(0, 17), new Point(6, 18), new Point(0, 16), new Point(5, 18), new Point(0, 18), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 17), new Point(1, 18), new Point(2, 18), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A54_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A54_ChallengeMoveExtension");
            return g;
        }

        /*
  9 . O O O . . . . . . . . . . . . . . . 
 10 . X X . . . . . . . . . . . . . . . . 
 11 . . X . O . . . . . . . . . . . . . . 
 12 . X . O . . . . . . . . . . . . . . . 
 13 . . . X O . . . . . . . . . . . . . . 
 14 . . X X O . . . . . . . . . . . . . . 
 15 . X . O . . . . . . . . . . . . . . . 
 16 . O O . . . . . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_XuanXuanGo_A67()
        {
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 9, Content.White);
            g.SetupMove(1, 10, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 9, Content.White);
            g.SetupMove(2, 10, Content.Black);
            g.SetupMove(2, 11, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 9, Content.White);
            g.SetupMove(3, 12, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 12) };
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 9; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 13));
            gi.movablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 8));
            gi.killMovablePoints.Add(new Point(0, 17));
            gi.killMovablePoints.Add(new Point(3, 10));
            gi.killMovablePoints.Add(new Point(3, 11));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(2, 15), new Point(0, 16), new Point(1, 13), new Point(2, 12), new Point(1, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 14), new Point(2, 15), new Point(0, 16), new Point(1, 13), new Point(2, 12), new Point(0, 11), new Point(0, 10), new Point(1, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A67_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetXuanXuanQiJingMappedJsonExtensionString("Scenario_XuanXuanGo_A67_ChallengeMoveExtension");

            return g;
        }

        /*
 14 O O . . O . . . . . . . . . . . . . . 
 15 . . O O . O . . . . . . . . . . . . . 
 16 . X . . X O . . . . . . . . . . . . . 
 17 . . . X . X O . . . . . . . . . . . . 
 18 . . . . . X O . . . . . . . . . . . . 
         */
        public Game Scenario_GuanZiPu_A2Q29_101Weiqi()
        {
            //https://www.101weiqi.com/book/guanzipu/95/18657/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.White, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(3, 17), new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(1, 18), new Point(2, 16), new Point(4, 17), new Point(4, 18), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(4, 17), new Point(2, 18), new Point(2, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(0, 16), new Point(0, 17), new Point(4, 17), new Point(2, 18), new Point(2, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(1, 17), new Point(0, 16), new Point(0, 17), new Point(4, 17), new Point(2, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(1, 17), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(1, 17), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(1, 17), new Point(2, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(1, 17), new Point(2, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A2Q29_101Weiqi_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetGuanZiPuMappedJsonExtensionString("Scenario_GuanZiPu_A2Q29_101Weiqi_ChallengeMoveExtension");

            return g;
        }

        /*
 12 . . X X X . X . . . . . . . . . . . . 
 13 . X . O O X . . . . . . . . . . . . . 
 14 . X O . . O X . . . . . . . . . . . . 
 15 . X O . . O X . . . . . . . . . . . . 
 16 . . X O . . O X . . . . . . . . . . . 
 17 . X X O . . O X . X . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game ScenarioHighLevel28()
        {
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);

            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(6, 12, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(9, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            gi.movablePoints = new List<Point>();
            for (int x = 2; x <= 6; x++)
            {
                for (int y = 13; y <= 18; y++)
                {
                    gi.movablePoints.Add(new Point(x, y));
                }
            }
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.Add(new Point(1, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(3, 18), new Point(4, 18), new Point(3, 15), new Point(4, 16), new Point(4, 15), new Point(5, 16), new Point(2, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(4, 15), new Point(3, 15), new Point(4, 14), new Point(5, 16), new Point(2, 13), new Point(3, 14), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(3, 18), new Point(4, 18), new Point(4, 15), new Point(3, 15), new Point(4, 14), new Point(5, 16), new Point(2, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(4, 15), new Point(3, 15), new Point(4, 14), new Point(5, 16), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(3, 18), new Point(4, 18), new Point(2, 13), new Point(3, 15), new Point(4, 14), new Point(3, 14), new Point(5, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(3, 18), new Point(4, 18), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 18), new Point(3, 18), new Point(4, 18), new Point(4, 15), new Point(3, 15), new Point(4, 14), new Point(5, 16), new Point(4, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetMappedJsonExtensionString("ScenarioHighLevel28_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetMappedJsonExtensionString("ScenarioHighLevel28_ChallengeMoveExtension");

            return g;
        }



    }
}

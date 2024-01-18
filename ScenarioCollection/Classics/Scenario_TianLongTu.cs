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
 13 X X X X X . . . . . . . . . . . . . . 
 14 O X O O X . . . . . . . . . . . . . . 
 15 . O X O X . . . . . . . . . . . . . . 
 16 . . . O X . . . . . . . . . . . . . . 
 17 . . O O O X X . . . . . . . . . . . . 
 18 . . . . . O X . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q15082()
        {
            //https://www.101weiqi.com/book/tianlongtu/32/15082/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(2, 16), new Point(0, 17), new Point(0, 15), new Point(0, 16), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 16), new Point(2, 16), new Point(0, 17), new Point(0, 15), new Point(0, 16), new Point(2, 15), new Point(1, 17), new Point(1, 18), new Point(3, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q15082_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q15082_ChallengeMoveExtension");

            return g;
        }

        /*
 12 . X . . . . . . . . . . . . . . . . . 
 13 X . . . . . . . . . . . . . . . . . . 
 14 . X X X . . . . . . . . . . . . . . . 
 15 . O O . X . . . . . . . . . . . . . . 
 16 O . . O X . . . . . . . . . . . . . . 
 17 . . O . X . . . . . . . . . . . . . . 
 18 . . . . X . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q14916()
        {
            //https://www.101weiqi.com/book/tianlongtu/32/14916/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 15) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 17), new Point(1, 17), new Point(3, 15), new Point(1, 16), new Point(0, 17), new Point(0, 15), new Point(0, 14), new Point(0, 18), new Point(0, 15), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 17), new Point(1, 17), new Point(3, 15), new Point(1, 16), new Point(0, 17), new Point(0, 15), new Point(0, 14), new Point(0, 18), new Point(0, 15), new Point(2, 18) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q14916_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q14916_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . . . . X X X . . . . . . . . . . . . 
 14 . . . X O O O X . . . . . . . . . . . 
 15 . . X . O . . X . . . . . . . . . . . 
 16 . . X O . . O . X . . . . . . . . . . 
 17 . . X O . . . O X . . . . . . . . . . 
 18 . X . . . . . O X . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16605()
        {
            //https://www.101weiqi.com/book/tianlongtu/32/16605/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 16), new Point(6, 15), new Point(6, 17), new Point(7, 16), new Point(3, 18), new Point(5, 18), new Point(6, 18), new Point(4, 18), new Point(3, 15), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 16), new Point(6, 15), new Point(3, 18), new Point(5, 18), new Point(6, 17), new Point(7, 16), new Point(6, 18), new Point(4, 18), new Point(3, 15), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(6, 17), new Point(7, 16), new Point(5, 16), new Point(6, 15), new Point(3, 18), new Point(5, 18), new Point(6, 18), new Point(4, 18), new Point(3, 15), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 18), new Point(5, 16), new Point(3, 18), new Point(4, 18), new Point(3, 15), new Point(2, 18) });

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 16), new Point(6, 15), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 16), new Point(6, 15), new Point(4, 18), new Point(3, 18), new Point(6, 17), new Point(7, 16), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 16), new Point(6, 15), new Point(6, 17), new Point(7, 16), new Point(3, 15), new Point(4, 16), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 16), new Point(6, 15), new Point(6, 17), new Point(7, 16), new Point(3, 15), new Point(4, 16), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(6, 17), new Point(7, 16), new Point(5, 16), new Point(6, 15), new Point(3, 15), new Point(4, 16), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(6, 17), new Point(7, 16), new Point(5, 16), new Point(6, 15), new Point(3, 15), new Point(4, 16), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16605_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16605_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X X X X X . X . . . . . . . . . . 
 15 . X O O O O X . . . . . . . . . . . . 
 16 . X O . . O X . X . . . . . . . . . . 
 17 . X X O . . O O X . . . . . . . . . . 
 18 . . . X O . . . X . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16574()
        {
            //https://www.101weiqi.com/book/tianlongtu/31/16574/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 17));
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 16));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(2, 18), new Point(5, 17), new Point(5, 18), new Point(4, 16), new Point(3, 16), new Point(7, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]}]";
            return g;
        }

        /*
 15 . . X X X X X X . . . . . . . . . . . 
 16 . X O O O O O . X . . . . . . . . . . 
 17 . X O . . . . O X X . . . . . . . . . 
 18 . X . . . . . . O X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16444()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16444/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(5, 17), new Point(6, 18), new Point(7, 18), new Point(7, 16), new Point(6, 17), new Point(3, 18), new Point(2, 18), new Point(5, 18), new Point(4, 18), new Point(5, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";
            return g;
        }

        /*
 14 . . . X X X . . . . . . . . . . . . . 
 15 . . . . O . X X X X . . . . . . . . . 
 16 . . X X O . . O O X . . . . . . . . . 
 17 . X O O . O . . O X . . . . . . . . . 
 18 . X O O . . . . O X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16470()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16470/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 4; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 17));
            gi.movablePoints.Add(new Point(3, 17));
            gi.movablePoints.Add(new Point(2, 18));
            gi.movablePoints.Add(new Point(3, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(4, 18), new Point(6, 16), new Point(6, 17), new Point(6, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]}]";


            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}}]}]";

            return g;
        }

        /*
 14 . . . . X X X . . . . . . . . . . . . 
 15 . . X X O O . X X . . . . . . . . . . 
 16 . X O . . . . O X . . . . . . . . . . 
 17 . X O . O O . O X . . . . . . . . . . 
 18 . X . . . . . . X . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16483()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16483/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
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
            g.SetupMove(8, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 15), new Point(3, 16), new Point(3, 18), new Point(6, 16), new Point(6, 18), new Point(7, 18), new Point(4, 16) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}}]}]";
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";
            return g;
        }


        /*
 12 . . O . . . . . . . . . . . . . . . . 
 13 . O . O O O . . . . . . . . . . . . . 
 14 . . X X X O . . . . . . . . . . . . . 
 15 X X X . X . . . . . . . . . . . . . . 
 16 . O . . X O . . . . . . . . . . . . . 
 17 . . O X O . O . . . . . . . . . . . . 
 18 . . . . . O . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q14992()
        {
            //https://www.101weiqi.com/book/tianlongtu/32/14992/
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 12, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 18, Content.White);
            g.SetupMove(6, 17, Content.White);
            gi.targetPoints = new List<Point>() { new Point(2, 15) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(1, 14));
            gi.killMovablePoints.Add(new Point(2, 13));
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 18), new Point(1, 17), new Point(2, 16), new Point(3, 16), new Point(2, 18), new Point(0, 16), new Point(4, 18), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 18), new Point(1, 17), new Point(2, 16), new Point(3, 16), new Point(2, 18), new Point(0, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";



            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":13},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":13},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}}]}]";
            return g;
        }

        /*
 14 . . X X X . . . . . . . . . . . . . . 
 15 . X . O O X X X . . . . . . . . . . . 
 16 . X O . X O O O X . . . . . . . . . . 
 17 . X O . O . . . X . . . . . . . . . . 
 18 . X . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16520()
        {
            //https://www.101weiqi.com/book/tianlongtu/31/16520/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(3, 17), new Point(3, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(6, 17), new Point(5, 17), new Point(3, 17), new Point(3, 18), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(6, 17), new Point(5, 17), new Point(2, 15) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 18), new Point(6, 17), new Point(5, 17), new Point(3, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]}]";
            return g;
        }

        /*
 13 . . . O O O . . . . . . . . . . . . . 
 14 . . . . X . . . . . . . . . . . . . . 
 15 . O O . X . O O . . . . . . . . . . . 
 16 . O X . . O X . O . . . . . . . . . . 
 17 . O X . . X . X O . . . . . . . . . . 
 18 . O . . . . . X O . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16487()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16487/
            var gi = new GameInfo(SurviveOrKill.SurviveWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            gi.targetPoints = new List<Point>() { new Point(5, 17), new Point(2, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.Add(new Point(5, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 16), new Point(5, 15), new Point(3, 18), new Point(2, 18), new Point(3, 16), new Point(4, 18), new Point(4, 17), new Point(3, 17) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":14}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":14},\"SecondMove\":{\"x\":5,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";
            return g;
        }


        /*
 13 . X X X . . . . . . . . . . . . . . . 
 14 . X O . X . . . . . . . . . . . . . . 
 15 . . O . X . . . . . . . . . . . . . . 
 16 . . . O X . . . . . . . . . . . . . . 
 17 . O . . O X . . . . . . . . . . . . . 
 18 . . . O . X . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q15017()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/15017/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(1, 16), new Point(0, 16), new Point(0, 17), new Point(1, 18), new Point(2, 17), new Point(4, 18), new Point(3, 15), new Point(3, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q15017_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q15017_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . X X X . . . . . . . . . . . . . . . 
 15 X O O X . . . . . . . . . . . . . . . 
 16 . . . . X X X X . . . . . . . . . . . 
 17 . O . O O O O X . . . . . . . . . . . 
 18 . . . . . . . X . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16446()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16446/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 15));
            gi.movablePoints.Add(new Point(2, 15));

            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(2, 16), new Point(2, 17), new Point(2, 18), new Point(0, 17), new Point(1, 16), new Point(1, 18), new Point(0, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16446_PlayerMoveExtension");

            return g;
        }


        /*
 14 . X X X . . . . . . . . . . . . . . . 
 15 O . . . . . . . . . . . . . . . . . . 
 16 . O . X X . X . . . . . . . . . . . . 
 17 . O . O . X . . . . . . . . . . . . . 
 18 . . . . O . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16456()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16456/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(1, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(2, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(2, 16), new Point(5, 18), new Point(2, 18), new Point(3, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]}]";
            return g;
        }

        /*
 12 O O . . . . . . . . . . . . . . . . . 
 13 . X O O . . . . . . . . . . . . . . . 
 14 X . X O . . . . . . . . . . . . . . . 
 15 . . . O . . . . . . . . . . . . . . . 
 16 . X . O . . . . . . . . . . . . . . . 
 17 . . X X O . . . . . . . . . . . . . . 
 18 . . . O O . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q15054()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/15054/
            var gi = new GameInfo(SurviveOrKill.Survive, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 12, Content.White);
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 12, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);

            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 13; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 15), new Point(1, 17), new Point(1, 18), new Point(0, 17), new Point(0, 16), new Point(2, 16), new Point(2, 18), new Point(0, 18), new Point(2, 17) });


            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":1,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":14}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]}]";


            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":13},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":13},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";
            return g;
        }


        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . . . . . . . . . . . . . . . . . . . 
 15 . X X X X X . . . . . . . . . . . . . 
 16 . O O O O . X . . . . . . . . . . . . 
 17 . . . O X O X . . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16693()
        {
            //https://www.101weiqi.com/book/tianlongtu/33/16693/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 16), new Point(5, 18), new Point(4, 18), new Point(0, 16), new Point(0, 17), new Point(3, 18), new Point(1, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 16), new Point(0, 16), new Point(0, 17), new Point(5, 18), new Point(4, 18), new Point(3, 18), new Point(1, 18), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 18), new Point(5, 16), new Point(0, 16), new Point(0, 17), new Point(3, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]}]";
            return g;
        }


        /*
 14 . . . X X X X X X . . . . . . . . . . 
 15 . . X . O O O O . X . . . . . . . . . 
 16 . . X O . . X X O X . . . . . . . . . 
 17 . . X . . . O . O X . . . . . . . . . 
 18 . . . . . . . . O X . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16490()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16490/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(8, 18) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(3, 17), new Point(3, 18), new Point(5, 17), new Point(7, 17), new Point(8, 15), new Point(4, 16), new Point(5, 16), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16490_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16490_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X X X X . . . . . . . . . . . . . 
 15 . X . O O X . . . . . . . . . . . . . 
 16 . X O . . X . . . . . . . . . . . . . 
 17 O O . . O O X . . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16711()
        {
            //https://www.101weiqi.com/book/tianlongtu/33/16711/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 14);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 1; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 18));
            gi.movablePoints.Add(new Point(0, 17));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(4, 16), new Point(4, 18), new Point(5, 18), new Point(2, 17), new Point(3, 18), new Point(2, 18), new Point(1, 18), new Point(4, 18) });

            gi.PlayerMoveJson = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16711_PlayerMoveExtension");
            gi.ChallengeMoveJson = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16711_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . . X X . X X . . . . . . . . . . . 
 15 . X X X O . O X X X . . . . . . . . . 
 16 . X O O . O O O O X . . . . . . . . . 
 17 . X X O . . X . O X . . . . . . . . . 
 18 . X O . . . . . O X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16867()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16867/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
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
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(5, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 13));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 18), new Point(5, 17), new Point(4, 16), new Point(7, 18), new Point(4, 17), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16867_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16867_ChallengeMoveExtension");
            return g;
        }


        /*
 13 X X . . . . . . . . . . . . . . . . . 
 14 O X X X X X . . . . . . . . . . . . . 
 15 . O X . O X . . . . . . . . . . . . . 
 16 . O O X O . X . . . . . . . . . . . . 
 17 . . . O X . X . . . . . . . . . . . . 
 18 . O . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16760()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/16760/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(3, 15), new Point(0, 17), new Point(5, 17), new Point(5, 18), new Point(4, 18) });
            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}}]}]";
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16760_PlayerMoveExtension");

            return g;
        }

        /*
 14 . . X . . . . . . . . . . . . . . . . 
 15 . X . X X X X . . . . . . . . . . . . 
 16 . X O O . O . X . . . . . . . . . . . 
 17 X O . . . . O X . . . . . . . . . . . 
 18 . O . . . . O X . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16748()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/16748/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 17, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.Add(new Point(2, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(4, 16), new Point(3, 17), new Point(3, 18), new Point(5, 17), new Point(6, 16), new Point(2, 17), new Point(4, 18), new Point(2, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16748_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16748_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . . . X X . . . . . . . . . . . . . 
 15 . . X X O O X X X . . . . . . . . . . 
 16 . . X O . O O . . X . . . . . . . . . 
 17 . . X O . X O . O X . . . . . . . . . 
 18 . X O . . . . . O X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16768()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/16768/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(4, 16), new Point(6, 18), new Point(7, 18), new Point(7, 16), new Point(7, 17), new Point(4, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":8,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":8,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}}]}]";
            return g;
        }

        /*
 14 . . . X X X X X . . . . . . . . . . . 
 15 . . . X O O O O X X X . . . . . . . . 
 16 . . X O . X O . O O X . . . . . . . . 
 17 . . X O O . O . . . X . . . . . . . . 
 18 . . X X X O . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q15618()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/15618/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 18, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 4; x <= 10; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(11, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 16), new Point(5, 17), new Point(5, 16), new Point(7, 18), new Point(7, 17), new Point(8, 17), new Point(9, 17), new Point(9, 18), new Point(8, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":9,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":10,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":7,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":8,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":8,\"y\":17},\"SecondMove\":{\"x\":9,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":7,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":8,\"y\":17}}]},{\"FirstMove\":{\"x\":11,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":7,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":8,\"y\":18}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":11,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":8,\"y\":17}},{\"ThirdMove\":{\"x\":8,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":8,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":17}},{\"ThirdMove\":{\"x\":10,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":8,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":8,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":9,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16}},{\"FirstMove\":{\"x\":10,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16}}]";

            return g;
        }

        /*
 14 . . . X . . X X X . . . . . . . . . . 
 15 . . . . X X . O O X . . . . . . . . . 
 16 . . X X O O X . O X . . . . . . . . . 
 17 . . X O . . O . O X . . . . . . . . . 
 18 . . . . . . . . O . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16747()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/16747/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(6, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(5, 17), new Point(6, 15), new Point(4, 18), new Point(6, 18), new Point(7, 16), new Point(9, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(5, 17), new Point(6, 15), new Point(4, 18), new Point(4, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":9,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":9,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":9,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";

            return g;
        }


        /*
 14 X X . . . . . . . . . . . . . . . . . 
 15 O . X X X X . . . . . . . . . . . . . 
 16 O . O . O X . . . . . . . . . . . . . 
 17 . . O . O X . . . . . . . . . . . . . 
 18 . . . . O X . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16851()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16851/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.White);
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

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 16), new Point(1, 17), new Point(2, 18), new Point(1, 15), new Point(1, 18), new Point(3, 17), new Point(0, 17) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}}]}]";
            return g;
        }

        /*
 14 . . . . . . . X X X . . . . . . . . . 
 15 . . . . X X X . O X . . . . . . . . . 
 16 . . X . X O O . O X . X . . . . . . . 
 17 . . X O O . . . O O X . . . . . . . . 
 18 . . . . . . . . . . X . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16850()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16850/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 14, Content.Black);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);
            g.SetupMove(11, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 16));
            gi.movablePoints.Add(new Point(7, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 16));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 17), new Point(5, 17), new Point(6, 18), new Point(9, 18), new Point(7, 18), new Point(7, 15), new Point(7, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16850_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16850_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . . X X X X . . . . . . . . . . . . 
 15 . . X . O O O X X . . . . . . . . . . 
 16 . . X O . . . O X . . . . . . . . . . 
 17 . . X O X . . O X . . . . . . . . . . 
 18 . . O . . . . O . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16859()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16859/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 16), new Point(3, 15), new Point(5, 16), new Point(6, 16), new Point(5, 18), new Point(6, 17), new Point(3, 18), new Point(4, 18), new Point(5, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16859_PlayerMoveExtension");

            return g;
        }


        /*
 13 . . . . . . X X X X . . . . . . . . . 
 14 . . X X X X . O . . . . . . . . . . . 
 15 . . X O O O . . O X . . . . . . . . . 
 16 . . X O . O . O X . X . . . . . . . . 
 17 . X O O . . O X X . . . . . . . . . . 
 18 . X . . . . . X . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16902()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16902/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();


            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            g.SetupMove(8, 13, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 13, Content.Black);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(8, 14));
            gi.movablePoints.Add(new Point(8, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 17), new Point(6, 18), new Point(2, 18), new Point(4, 18), new Point(3, 18), new Point(5, 17), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16902_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16902_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . X . . X X X . . . . . . . . . . . . 
 14 X . X X . O X . . . . . . . . . . . . 
 15 X O . O . O . . . . . . . . . . . . . 
 16 X O . . O O X . . . . . . . . . . . . 
 17 . O . . O X . X . . . . . . . . . . . 
 18 . . . . . X . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16936()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16936/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 14));
            gi.killMovablePoints.Add(new Point(6, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(2, 15), new Point(2, 16), new Point(3, 17), new Point(3, 18), new Point(4, 18), new Point(2, 18), new Point(3, 16), new Point(2, 17), new Point(3, 16), new Point(4, 14), new Point(4, 15) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16936_PlayerMoveExtension");

            return g;
        }

        /*
 14 . . X X X X . . . . . . . . . . . . . 
 15 . X . O O X . X X . . . . . . . . . . 
 16 . X O . . O O O X . . . . . . . . . . 
 17 . X X O . . . O X . . . . . . . . . . 
 18 . . . . . . . O X . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16931()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16931/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(6, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(5, 18), new Point(2, 18), new Point(5, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":15}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":15},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16931_PlayerMoveExtension");

            return g;
        }

        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . X X X X . . . . . . . . . . . . . . 
 15 O X O O . X . . . . . . . . . . . . . 
 16 . O . . X . X . . . . . . . . . . . . 
 17 . . . O . O X . . . . . . . . . . . . 
 18 . O . . . . X . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16924()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16924/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 16), new Point(2, 15) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(1, 17), new Point(3, 16), new Point(2, 16), new Point(3, 18), new Point(4, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(2, 17), new Point(1, 17), new Point(3, 16), new Point(2, 16), new Point(3, 18), new Point(4, 17), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16924_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16924_ChallengeMoveExtension");

            return g;
        }

        /*
 14 X X X X . . . . . . . . . . . . . . . 
 15 . O O . X . . . . . . . . . . . . . . 
 16 . . . . X . . . . . . . . . . . . . . 
 17 . O . O X . X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16973()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16973/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(2, 18), new Point(0, 15), new Point(0, 16), new Point(2, 16), new Point(3, 16), new Point(3, 15) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":15},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16973_PlayerMoveExtension");
            return g;
        }

        /*
 13 . . . X X X . . . . . . . . . . . . . 
 14 . . X O O O X X . . . . . . . . . . . 
 15 . . X O . O O X . . . . . . . . . . . 
 16 . . X . O . . . X . . . . . . . . . . 
 17 . . X O . . O . X . . . . . . . . . . 
 18 . . . . . . . . X . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16975()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16975/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 24);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(4, 16) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(5, 18), new Point(6, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(6, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16975_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16975_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . . . X . . . . . . . . . . . . . . 
 15 . . X X . X X X X . . . . . . . . . . 
 16 . X O O X O O O O X . . . . . . . . . 
 17 . X . . O . . . O X . . . . . . . . . 
 18 . X O . . . . . . X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17078()
        {
            //https://www.101weiqi.com/book/tianlongtu/37/17078/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 17), new Point(8, 18), new Point(4, 18), new Point(6, 18), new Point(5, 17), new Point(4, 15), new Point(3, 17), new Point(4, 16), new Point(2, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(6, 17), new Point(8, 18), new Point(4, 18), new Point(6, 18), new Point(3, 17), new Point(4, 15), new Point(5, 17), new Point(4, 16), new Point(2, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17078_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17078_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . . . . X X X . . . . . . . . . . . . 
 15 . X X . . . . . . . . . . . . . . . . 
 16 . O X . O . X . . . . . . . . . . . . 
 17 . O O . . O X . . . . . . . . . . . . 
 18 . . . . . . X . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16754()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/16754/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(5, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(5, 18), new Point(4, 18), new Point(2, 18), new Point(3, 16), new Point(1, 18), new Point(5, 16), new Point(3, 18), new Point(3, 17), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(0, 17), new Point(5, 18), new Point(4, 18), new Point(2, 18), new Point(3, 16), new Point(3, 18), new Point(3, 17), new Point(4, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(0, 16), new Point(0, 17), new Point(2, 18), new Point(3, 16), new Point(1, 18), new Point(5, 16), new Point(3, 18), new Point(3, 17), new Point(4, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(0, 16), new Point(0, 17), new Point(2, 18), new Point(3, 16), new Point(3, 18), new Point(3, 17), new Point(4, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16754_PlayerMoveExtension");

            return g;
        }

        /*
 14 . . . . . . . . X X X . . . . . . . . 
 15 . . . X X X X X O O X . . . . . . . . 
 16 . . X O . O O O . O X . . . . . . . . 
 17 . . X O . . . . X O X . . . . . . . . 
 18 . . . . . . . . . O . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16827()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16827/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 14, Content.Black);
            g.SetupMove(9, 15, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(9, 18, Content.White);
            g.SetupMove(10, 14, Content.Black);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(10, 17));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 17), new Point(6, 17), new Point(4, 17), new Point(4, 16), new Point(3, 18), new Point(5, 18), new Point(7, 18), new Point(4, 18), new Point(8, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16827_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16827_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . . . X . . . . . . . . . . . . . . 
 15 . . . X . X X X X X X . . . . . . . . 
 16 . . X O O O O . . O X . . . . . . . . 
 17 . . X O . . . O . O X . . . . . . . . 
 18 . . . . . . . . O X X . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16872()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16872/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(9, 18, Content.Black);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(5, 17), new Point(6, 18), new Point(7, 18), new Point(7, 16), new Point(6, 17), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(5, 17), new Point(6, 18), new Point(7, 18), new Point(4, 18), new Point(3, 18), new Point(7, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(8, 17), new Point(5, 17), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16872_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16872_ChallengeMoveExtension");

            return g;
        }

        /*
 12 . X . . . . . . . . . . . . . . . . . 
 13 X . . . . . . . . . . . . . . . . . . 
 14 O X X X . . . . . . . . . . . . . . . 
 15 O O O . . X . . . . . . . . . . . . . 
 16 . . . O O X . . . . . . . . . . . . . 
 17 . . . O X X . . . . . . . . . . . . . 
 18 . . . X O . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16985()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16985/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(0, 15, Content.White);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 0; x <= 3; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.Add(new Point(5, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(2, 18), new Point(1, 17), new Point(1, 16), new Point(0, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":15},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":15}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":15}},{\"ThirdMove\":{\"x\":3,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16985_PlayerMoveExtension");
            return g;
        }


        /*
 14 . X . . . . . . . . . . . . . . . . . 
 15 . . X X X X X . . . . . . . . . . . . 
 16 X X O O . O O X . . . . . . . . . . . 
 17 O O . . . . O X . . . . . . . . . . . 
 18 . . . . . . . X . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q17077()
        {
            //https://www.101weiqi.com/book/tianlongtu/37/17077/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 16, Content.Black);
            g.SetupMove(0, 17, Content.White);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(2, 17), new Point(3, 17), new Point(2, 18), new Point(4, 18), new Point(1, 18), new Point(0, 18), new Point(2, 18) });
            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}}]}]";


            return g;
        }

        /*
 13 . . . X . . . . . . . . . . . . . . . 
 14 . . X . . . . . . . . . . . . . . . . 
 15 . X . O X X X X X . X . . . . . . . . 
 16 . X . . O O . . O X . . . . . . . . . 
 17 . . X O . . O . O O X . . . . . . . . 
 18 . . X . . . . . . . X . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q17081()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17081/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 15));
            gi.movablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(8, 18), new Point(5, 17), new Point(4, 18), new Point(6, 18), new Point(6, 16), new Point(7, 16), new Point(7, 17), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(8, 18), new Point(6, 16), new Point(7, 17), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17081_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17081_ChallengeMoveExtension");

            return g;
        }

        /*
 14 X X . . X . . . . . . . . . . . . . . 
 15 . O X X . . X . . . . . . . . . . . . 
 16 . O O . X . X . . . . . . . . . . . . 
 17 . . . . O O X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q17154()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17154/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 16));
            gi.movablePoints.Add(new Point(3, 16));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(5, 16));
            gi.killMovablePoints.Add(new Point(7, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 17), new Point(2, 17), new Point(3, 17), new Point(1, 18), new Point(1, 17), new Point(3, 18), new Point(2, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 15), new Point(0, 17), new Point(1, 18), new Point(3, 17), new Point(2, 17), new Point(1, 17), new Point(3, 18), new Point(2, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17154_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17154_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . X . . . . . . . . . . . . . . . . . 
 15 X O X X X X X . . . . . . . . . . . . 
 16 . O O X O O O X . . . . . . . . . . . 
 17 . . . O . . O X . . . . . . . . . . . 
 18 . . . . . . . X . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17136()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17136/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17), new Point(2, 16) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(2, 17), new Point(4, 17), new Point(4, 18), new Point(5, 18), new Point(5, 17), new Point(6, 18), new Point(1, 18), new Point(0, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(2, 17), new Point(4, 17), new Point(4, 18), new Point(6, 18), new Point(5, 17), new Point(5, 18), new Point(1, 18), new Point(0, 17) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":13}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":14},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17136_PlayerMoveExtension");
            return g;
        }

        /*
 10 . . X X X X . . . . . . . . . . . . . 
 11 . X O O O O X X . . . . . . . . . . . 
 12 . X . . . . O O X . . . . . . . . . . 
 13 . X . . O . . . X . . . . . . . . . . 
 14 . X . . . . O X . . . . . . . . . . . 
 15 . X O O O O . X . . . . . . . . . . . 
 16 . . X X . X X . . . . . . . . . . . . 
 17 . . . . . . . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q17241()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/17241/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 10, Content.Black);
            g.SetupMove(2, 11, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 10, Content.Black);
            g.SetupMove(3, 11, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(4, 10, Content.Black);
            g.SetupMove(4, 11, Content.White);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 10, Content.Black);
            g.SetupMove(5, 11, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(6, 11, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 11, Content.Black);
            g.SetupMove(7, 12, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(8, 12, Content.Black);
            g.SetupMove(8, 13, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 11) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 12; y <= 15; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 16));
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 12), new Point(6, 13), new Point(3, 14), new Point(4, 14), new Point(5, 13), new Point(2, 14), new Point(3, 12) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17241_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17241_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . . X X X X . . . . . . . . . . . 
 14 . . X X O . . . X . . . . . . . . . . 
 15 . . X O . . O . . . . . . . . . . . . 
 16 . . X O . . O X X . . . . . . . . . . 
 17 . X O . O . . O X . . . . . . . . . . 
 18 . X . . . . O . X . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17250()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/17250/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 18, Content.White);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 2; x <= 6; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 17));
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(7, 14));
            gi.killMovablePoints.Add(new Point(7, 15));
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 15), new Point(5, 14), new Point(4, 18), new Point(3, 18), new Point(4, 16), new Point(3, 17), new Point(5, 17), new Point(5, 18), new Point(2, 18), new Point(4, 18), new Point(6, 17), new Point(5, 16), new Point(6, 17) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17250_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17250_ChallengeMoveExtension");
            return g;
        }

        /*
 12 . X X X X X . . . . . . . . . . . . . 
 13 X O O O O O X X . . . . . . . . . . . 
 14 X . . . . . O X . . . . . . . . . . . 
 15 . X O . . . . O X . . . . . . . . . . 
 16 . X O O O . O . X . . . . . . . . . . 
 17 . X X . . . . X . . . . . . . . . . . 
 18 . . . X X X X . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16604()
        {
            //https://www.101weiqi.com/book/tianlongtu/32/16604/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 12, Content.Black);
            g.SetupMove(1, 13, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 18, Content.Black);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 13, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 12, Content.Black);
            g.SetupMove(5, 13, Content.White);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 1; x <= 7; x++)
            {
                for (int y = 14; y <= 17; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 14), new Point(5, 15), new Point(6, 15), new Point(5, 16), new Point(5, 17), new Point(6, 17), new Point(4, 17), new Point(7, 16), new Point(5, 14), new Point(2, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 14), new Point(5, 15), new Point(6, 15), new Point(5, 16), new Point(5, 17), new Point(6, 17), new Point(4, 17), new Point(2, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 14), new Point(5, 15), new Point(6, 15), new Point(5, 16), new Point(5, 17), new Point(4, 17), new Point(6, 17), new Point(7, 16), new Point(5, 14), new Point(2, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 14), new Point(5, 15), new Point(6, 15), new Point(5, 16), new Point(5, 17), new Point(4, 17), new Point(6, 17), new Point(2, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 14), new Point(5, 15), new Point(6, 15), new Point(5, 16), new Point(5, 17), new Point(2, 14) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 14), new Point(4, 14), new Point(5, 15), new Point(6, 15), new Point(2, 14), new Point(1, 14), new Point(5, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16604_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16604_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X X X X X X . . . . . . . . . . . 
 15 . . X O O O O . X . . . . . . . . . . 
 16 . X O . . . . O X . . . . . . . . . . 
 17 . X O . O . O X . X . . . . . . . . . 
 18 . X O . X . . X . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17239()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/17239/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(4, 18, Content.Black);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(5, 17), new Point(5, 16), new Point(4, 16), new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(4, 17), new Point(7, 15), new Point(5, 17), new Point(4, 18), new Point(6, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":15}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":6,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":6,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":16}},{\"FirstMove\":{\"x\":7,\"y\":15},\"SecondMove\":{\"x\":5,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":15},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16}},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":16}}]";

            return g;
        }

        /*
 14 . . X . X X X . . . . . . . . . . . . 
 15 . . X . O . . X X X . . . . . . . . . 
 16 . . X O . . O O O X . . . . . . . . . 
 17 . . X O . O X . O X . . . . . . . . . 
 18 . . . . . . . . . X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17255()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/17255/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 20);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(6, 16) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(3, 14));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(8, 18), new Point(5, 18), new Point(4, 16), new Point(4, 17) });
            gi.dictatePoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 18), new Point(6, 18), new Point(8, 18), new Point(5, 18), new Point(2, 18), new Point(5, 16), new Point(5, 15), new Point(4, 17), new Point(3, 18), new Point(6, 15) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17255_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17255_ChallengeMoveExtension");
            return g;
        }

        /*
 15 X X X X X . . . . . . . . . . . . . . 
 16 O O . O O X X X . . . . . . . . . . . 
 17 . . O . . O O X . . . . . . . . . . . 
 18 . . . . . . X X . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q2176()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/2176/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(0, 16, Content.White);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 6; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 18), new Point(0, 17), new Point(4, 17), new Point(2, 16), new Point(1, 17), new Point(4, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 18), new Point(0, 17), new Point(4, 17), new Point(2, 16), new Point(1, 17), new Point(5, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 18), new Point(0, 17), new Point(4, 17), new Point(2, 16), new Point(1, 17), new Point(3, 17) });

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 18), new Point(0, 17), new Point(4, 17), new Point(4, 18), new Point(5, 18), new Point(2, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(1, 18), new Point(0, 17), new Point(4, 17), new Point(5, 18), new Point(4, 18), new Point(2, 16) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";
            return g;
        }

        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . X . . . . . . . . . . . . . . . . . 
 15 X O X X X X . . . . . . . . . . . . . 
 16 . O X . O O X X . . . . . . . . . . . 
 17 . O O . . . . X . . . . . . . . . . . 
 18 . . . . O . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17183()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/17183/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 18, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 15));
            gi.movablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(0, 17), new Point(0, 16), new Point(2, 18), new Point(0, 14), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(3, 17), new Point(3, 16), new Point(0, 17), new Point(0, 16), new Point(2, 18), new Point(0, 14), new Point(5, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17183_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17183_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X X X X . . . . . . . . . . . . . 
 15 . . X O O O X X . . . . . . . . . . . 
 16 . X O . . . . O X . . . . . . . . . . 
 17 . X O . O . . O X X X . . . . . . . . 
 18 . X O . . . . . O O X . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17112()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17112/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(10, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(4, 17) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(8, 18));
            gi.movablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(5, 16), new Point(3, 16), new Point(4, 16), new Point(6, 18), new Point(6, 17), new Point(4, 18) });

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":3,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":5,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":5,\"y\":16}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":16},\"SecondMove\":{\"x\":3,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":6,\"y\":16},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":17},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":7,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":6,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":7,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":5,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":17},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":6,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":6,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":7,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":16},\"FourthMove\":{\"x\":6,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":6,\"y\":16}}]}]";

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17112_PlayerMoveExtension");

            return g;
        }

        /*
 12 X . . . . . . . . . . . . . . . . . . 
 13 O X X . . . . . . . . . . . . . . . . 
 14 O O . . . . . . . . . . . . . . . . . 
 15 . O X X X X X . . . . . . . . . . . . 
 16 . . O O O O X . . . . . . . . . . . . 
 17 . O . . . X . X . . . . . . . . . . . 
 18 . . X . . . X . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16925()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16925/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 12, Content.Black);
            g.SetupMove(0, 13, Content.White);
            g.SetupMove(0, 14, Content.White);
            g.SetupMove(1, 11, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 18, Content.Black);
            g.SetupMove(7, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }

            gi.movablePoints.Add(new Point(0, 13));
            gi.movablePoints.Add(new Point(0, 14));
            gi.movablePoints.Add(new Point(1, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(2, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(0, 16), new Point(4, 17), new Point(0, 17), new Point(3, 18), new Point(1, 16), new Point(2, 17), new Point(1, 18) });

            gi.PlayerMoveJson = "[{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":16},\"SecondMove\":{\"x\":4,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":2,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":14},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":18}}]},{\"FirstMove\":{\"x\":2,\"y\":12},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":5,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":5,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]},{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":14},\"FourthMove\":{\"x\":0,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":12},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}}]}]";

            gi.ChallengeMoveJson = "[{\"FirstMove\":{\"x\":1,\"y\":16},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":2,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":0,\"y\":17},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":1,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":4,\"y\":17}}]},{\"FirstMove\":{\"x\":3,\"y\":17},\"SecondMove\":{\"x\":3,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}}]},{\"FirstMove\":{\"x\":4,\"y\":17},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":4,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":14}}]},{\"FirstMove\":{\"x\":0,\"y\":15},\"SecondMove\":{\"x\":1,\"y\":18},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":3,\"y\":18}}]},{\"FirstMove\":{\"x\":0,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":0,\"y\":15}}]},{\"FirstMove\":{\"x\":3,\"y\":18},\"SecondMove\":{\"x\":0,\"y\":17},\"SecondLevel\":[{\"ThirdMove\":{\"x\":1,\"y\":16},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":16}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":3,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":5,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}}]},{\"FirstMove\":{\"x\":5,\"y\":18},\"SecondMove\":{\"x\":1,\"y\":16},\"SecondLevel\":[{\"ThirdMove\":{\"x\":2,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":4,\"y\":17},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":0,\"y\":17},\"FourthMove\":{\"x\":4,\"y\":18}},{\"ThirdMove\":{\"x\":1,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":15},\"FourthMove\":{\"x\":2,\"y\":14}},{\"ThirdMove\":{\"x\":4,\"y\":18},\"FourthMove\":{\"x\":1,\"y\":18}},{\"ThirdMove\":{\"x\":3,\"y\":18},\"FourthMove\":{\"x\":2,\"y\":17}},{\"ThirdMove\":{\"x\":0,\"y\":18},\"FourthMove\":{\"x\":4,\"y\":17}},{\"ThirdMove\":{\"x\":3,\"y\":17},\"FourthMove\":{\"x\":1,\"y\":18}}]}]";
            return g;
        }

        /*
 14 . . . . . . . X X . . . . . . . . . . 
 15 . . X X X X X O O X . . . . . . . . . 
 16 . . X O . . O . O X . X . . . . . . . 
 17 . X O . O . . . O O X . . . . . . . . 
 18 . X O . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16466()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16466/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(11, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(4, 17), new Point(8, 17) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(11, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(5, 16), new Point(6, 17), new Point(4, 16), new Point(4, 18), new Point(5, 18), new Point(6, 18), new Point(3, 17), new Point(7, 17), new Point(7, 16), new Point(8, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(5, 16), new Point(6, 17), new Point(4, 16), new Point(4, 18), new Point(5, 18), new Point(6, 18), new Point(3, 17), new Point(8, 18), new Point(9, 18), new Point(7, 17), new Point(7, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16466_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16466_ChallengeMoveExtension");

            return g;
        }



        /*
 14 . . . . . . . X X . X . . . . . . . . 
 15 . . . X X X X O O O X . . . . . . . . 
 16 . . X O O . . O . O X . . . . . . . . 
 17 . . X O . . . . O X X . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16571()
        {
            //https://www.101weiqi.com/book/tianlongtu/31/16571/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 15, Content.White);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 14, Content.Black);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(7, 15) };
            for (int x = 2; x <= 9; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.Add(new Point(9, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 18), new Point(8, 18), new Point(5, 17), new Point(6, 17), new Point(6, 18), new Point(5, 16), new Point(6, 16), new Point(7, 17), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16571_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16571_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . . . . X X . . . . . . . . . . . . . 
 14 . . . . . . . . . . . . . . . . . . . 
 15 . . X X O X X X X . . . . . . . . . . 
 16 . . X O . O O O X . X . . . . . . . . 
 17 . X . O . . . . O X . . . . . . . . . 
 18 . X . . . . . . O . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16594()
        {
            //https://www.101weiqi.com/book/tianlongtu/32/16594/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(10, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(4, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.Add(new Point(4, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(6, 17), new Point(4, 17), new Point(4, 18), new Point(6, 18), new Point(5, 18), new Point(7, 18), new Point(7, 17), new Point(4, 16) });
            gi.ChallengeMoveJson = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16594_ChallengeMove");

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16594_PlayerMoveExtension");

            return g;
        }

        /*
 14 . . . . . . X X . . . . . . . . . . . 
 15 . . . X X X O O X X . . . . . . . . . 
 16 . . X . O O . . O . . . . . . . . . . 
 17 . . X O . . . . O X X . . . . . . . . 
 18 . . . . . . . . . O . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16735()
        {
            //https://www.101weiqi.com/book/tianlongtu/33/16735/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 20);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.White);
            g.SetupMove(10, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(4, 16) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(10, 18));
            gi.killMovablePoints.Add(new Point(9, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(6, 16), new Point(7, 16), new Point(6, 17), new Point(5, 17), new Point(6, 18), new Point(7, 18), new Point(3, 18), new Point(4, 18), new Point(2, 18), new Point(3, 16), new Point(9, 16) });

            gi.dictatePoints.Add(new List<Point>() { new Point(6, 17), new Point(6, 16), new Point(5, 17), new Point(4, 17), new Point(7, 17), new Point(6, 18), new Point(3, 18), new Point(4, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16735_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16735_ChallengeMoveExtension");

            return g;
        }


        /*
 14 . X X X X . . . . . . . . . . . . . . 
 15 . . O O . X . . . . . . . . . . . . . 
 16 . . . . O X . . . . . . . . . . . . . 
 17 . O . O X X . . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q2834()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/2834/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(1, 17), new Point(2, 15) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(0, 16), new Point(0, 15), new Point(1, 15), new Point(0, 18), new Point(2, 18), new Point(3, 18), new Point(0, 16), new Point(2, 17), new Point(3, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(2, 18), new Point(3, 18), new Point(0, 16), new Point(0, 15), new Point(1, 15), new Point(0, 18), new Point(0, 16) });
            gi.solutionPoints.Add(new List<Point>() { new Point(0, 17), new Point(1, 16), new Point(0, 16), new Point(0, 15), new Point(2, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q2834_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q2834_ChallengeMoveExtension");
            return g;
        }

        /*
 13 X X X X . . . . . . . . . . . . . . . 
 14 X . O O X X X . . . . . . . . . . . . 
 15 X O . . O O X . . . . . . . . . . . . 
 16 . X O . . O X . . X . . . . . . . . . 
 17 . X O . . O X . X . . . . . . . . . . 
 18 . O . . . . O O . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q16738()
        {
            //https://www.101weiqi.com/book/tianlongtu/33/16738/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(0, 13, Content.Black);
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(0, 15, Content.Black);
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(6, 18, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 16, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 16) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(6, 18));
            gi.movablePoints.Add(new Point(7, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.killMovablePoints.Add(new Point(7, 17));
            gi.survivalPoints.Add(new Point(1, 17));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 18), new Point(4, 18), new Point(2, 18), new Point(3, 17), new Point(4, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16738_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16738_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . X . . . . . . . . . . . . . . . . . 
 15 . . X X X X X . . . . . . . . . . . . 
 16 . X O O O O O X X X . . . . . . . . . 
 17 . X O X . . . O O X . . . . . . . . . 
 18 . O . . . . . . . X . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q27661()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/27661/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.White);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17) };
            for (int x = 0; x <= 8; x++)
            {
                for (int y = 17; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 16));

            gi.solutionPoints.Add(new List<Point>() { new Point(8, 18), new Point(6, 18), new Point(7, 18), new Point(6, 17), new Point(4, 17), new Point(4, 18), new Point(2, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q27661_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q27661_ChallengeMoveExtension");
            return g;
        }

        /*
 12 . . X X X . . . . . . . . . . . . . . 
 13 . X O O . X . . . . . . . . . . . . . 
 14 . X X O . . . . . . . . . . . . . . . 
 15 . X O O . X . . . . . . . . . . . . . 
 16 . O . . O X . . . . . . . . . . . . . 
 17 . . . . O X . X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
        */
        public Game Scenario_TianLongTu_Q16424()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/16424/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.White);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(3, 12, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 12, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(2, 15) };
            for (int x = 0; x <= 5; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 14));
            gi.killMovablePoints.Add(new Point(6, 18));
            gi.killMovablePoints.Add(new Point(4, 13));
            gi.killMovablePoints.Add(new Point(4, 14));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 17), new Point(0, 16), new Point(2, 17), new Point(2, 16), new Point(4, 18), new Point(3, 18), new Point(3, 17), new Point(5, 18), new Point(4, 15), new Point(3, 16), new Point(1, 18) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16424_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16424_ChallengeMoveExtension");

            return g;
        }

        /*
 14 X X X X X . . . . . . . . . . . . . . 
 15 . O . O . X . . . . . . . . . . . . . 
 16 . . . . O X . . . . . . . . . . . . . 
 17 . O . . O X . . . . . . . . . . . . . 
 18 . . . . . X . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16919()
        {
            //https://www.101weiqi.com/book/tianlongtu/36/16919/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(0, 14, Content.Black);
            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(1, 17, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.Black);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(5, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(1, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 18), new Point(3, 17), new Point(3, 18), new Point(2, 18), new Point(2, 16), new Point(2, 15), new Point(1, 16) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16919_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16919_ChallengeMoveExtension");
            return g;
        }

        /*
 14 . . X X X X X X X . . . . . . . . . . 
 15 . X O O O . O O X . . . . . . . . . . 
 16 . X . . . O . O X . X . . . . . . . . 
 17 . X O O . . . . O X . . . . . . . . . 
 18 . X X O . X . O O O X . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q15301()
        {
            //https://www.101weiqi.com/book/tianlongtu/30/15301/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(3, 18, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 18, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(8, 18, Content.White);
            g.SetupMove(9, 17, Content.Black);
            g.SetupMove(9, 18, Content.White);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 18, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 2; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(9, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);

            gi.solutionPoints.Add(new List<Point>() { new Point(6, 17), new Point(7, 17), new Point(2, 16), new Point(3, 16), new Point(5, 15), new Point(5, 17), new Point(4, 17), new Point(4, 16), new Point(6, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q15301_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q15301_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . X X X X X X . . . . . . . . . . . 
 15 . . X O O O O . X . . . . . . . . . . 
 16 . X O . . . . O X . . . . . . . . . . 
 17 . X O . O . . X . X . . . . . . . . . 
 18 . X O . . . . X . . . . . . . . . . . 
             */
        public Game Scenario_TianLongTu_Q16673()
        {
            //https://www.101weiqi.com/book/tianlongtu/33/16673/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 14, Content.Black);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.Black);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 15) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(6, 17), new Point(3, 17), new Point(3, 16), new Point(5, 16), new Point(5, 18), new Point(6, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16673_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16673_ChallengeMoveExtension");
            return g;
        }

        /*
 15 . . X . . X X X . X X . . . . . . . . 
 16 . . . X X O O . . O X . . . . . . . . 
 17 . . X O O . . O . O X . X . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q2174()
        {
            //https://www.101weiqi.com/book/tianlongtu/37/2174/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 18);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 16, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 16, Content.Black);
            g.SetupMove(4, 17, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.White);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 15, Content.Black);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(12, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 3; x <= 10; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 18));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(11, 18));
            gi.killMovablePoints.Add(new Point(8, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(9, 18), new Point(8, 18), new Point(8, 16), new Point(5, 18), new Point(6, 17), new Point(3, 18), new Point(7, 16), new Point(5, 17), new Point(8, 17), new Point(6, 18), new Point(7, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q2174_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q2174_ChallengeMoveExtension");
            return g;
        }


        /*
 13 . X . . . . . . . . . . . . . . . . . 
 14 . O X X X . . . . . . . . . . . . . . 
 15 . O O O X . X X . . . . . . . . . . . 
 16 . . . O O O O X . . . . . . . . . . . 
 17 . . X O X X X . . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q2413()
        {
            //https://www.101weiqi.com/book/tianlongtu/34/2413/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();
            g.SetupMove(1, 13, Content.Black);
            g.SetupMove(1, 14, Content.White);
            g.SetupMove(1, 15, Content.White);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(3, 17) };
            for (int x = 0; x <= 4; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(0, 13));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(0, 12));
            gi.killMovablePoints.Add(new Point(5, 18));
            gi.killMovablePoints.Add(new Point(5, 15));

            gi.solutionPoints.Add(new List<Point>() { new Point(1, 18), new Point(3, 18), new Point(0, 17), new Point(0, 14), new Point(0, 16), new Point(1, 16), new Point(1, 17) });
            gi.correctedSolutions.Add(new CorrectedList(new List<Point>() { new Point(3, 18), new Point(1, 17), new Point(1, 18), new Point(2, 16), new Point(2, 18), new Point(4, 18), new Point(5, 18), new Point(0, 16) }));

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q2413_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q2413_ChallengeMoveExtension");

            return g;
        }

        /*
 13 . . X X X X . . . . . . . . . . . . . 
 14 . . X O O O X X . . . . . . . . . . . 
 15 . . X O . X O . X . . . . . . . . . . 
 16 . X O . O . . O X . . . . . . . . . . 
 17 . X O . X . O X X . X . . . . . . . . 
 18 . X O . . . . O . . . . . . . . . . . 
        */
        public Game Scenario_TianLongTu_Q17143()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17143/
            var gi = new GameInfo(SurviveOrKill.KillWithKo, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 16, Content.Black);
            g.SetupMove(1, 17, Content.Black);
            g.SetupMove(1, 18, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.Black);
            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.White);
            g.SetupMove(2, 17, Content.White);
            g.SetupMove(2, 18, Content.White);
            g.SetupMove(3, 13, Content.Black);
            g.SetupMove(3, 14, Content.White);
            g.SetupMove(3, 15, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(4, 14, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(4, 17, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(6, 14, Content.Black);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 16, Content.White);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(7, 18, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 16, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(10, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(2, 17), new Point(3, 15) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 14; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(8, 18));
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 15), new Point(3, 17), new Point(5, 17), new Point(5, 16), new Point(6, 16), new Point(5, 18), new Point(6, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 15), new Point(3, 17), new Point(5, 17), new Point(5, 16), new Point(4, 18), new Point(6, 16), new Point(6, 18) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17143_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17143_ChallengeMoveExtension");

            return g;
        }


        /*
 11 . . . X . X X X . . . . . . . . . . . 
 12 . . X . O . O . X . . . . . . . . . . 
 13 . . X O . . . O X . . . . . . . . . . 
 14 . X O . . . . O X . . . . . . . . . . 
 15 . X O . . . O X . . . . . . . . . . . 
 16 . . X O O O O X . . . . . . . . . . . 
 17 . . X X . X X X . . . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17198()
        {
            //https://www.101weiqi.com/book/tianlongtu/39/17198/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(1, 14, Content.Black);
            g.SetupMove(1, 15, Content.Black);
            g.SetupMove(2, 12, Content.Black);
            g.SetupMove(2, 13, Content.Black);
            g.SetupMove(2, 14, Content.White);
            g.SetupMove(2, 15, Content.White);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 11, Content.Black);
            g.SetupMove(3, 13, Content.White);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.Black);
            g.SetupMove(4, 12, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 11, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(5, 17, Content.Black);
            g.SetupMove(6, 11, Content.Black);
            g.SetupMove(6, 12, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(6, 17, Content.Black);
            g.SetupMove(7, 11, Content.Black);
            g.SetupMove(7, 13, Content.White);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 15, Content.Black);
            g.SetupMove(7, 16, Content.Black);
            g.SetupMove(7, 17, Content.Black);
            g.SetupMove(8, 12, Content.Black);
            g.SetupMove(8, 13, Content.Black);
            g.SetupMove(8, 14, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(3, 16) };
            for (int x = 2; x <= 7; x++)
            {
                for (int y = 12; y <= 16; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 11));
            gi.killMovablePoints.Add(new Point(4, 17));

            gi.solutionPoints.Add(new List<Point>() { new Point(4, 14), new Point(5, 14), new Point(4, 13), new Point(5, 13), new Point(4, 15), new Point(3, 12), new Point(5, 12), new Point(3, 14), new Point(6, 13) });
            gi.solutionPoints.Add(new List<Point>() { new Point(4, 14), new Point(5, 14), new Point(4, 13), new Point(5, 13), new Point(4, 15), new Point(3, 12), new Point(5, 12), new Point(3, 14), new Point(4, 11), new Point(3, 15), new Point(6, 13) });
            gi.dictatePoints.Add(new List<Point>() { new Point(4, 14), new Point(5, 12), new Point(3, 14), new Point(3, 15), new Point(4, 13), new Point(3, 12), new Point(5, 14), new Point(6, 14), new Point(7, 12) });
            gi.dictatePoints.Add(new List<Point>() { new Point(4, 14), new Point(5, 14), new Point(4, 13), new Point(3, 12), new Point(5, 12), new Point(5, 13), new Point(4, 15), new Point(3, 14) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17198_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17198_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . . . X X X . . . . . . . . . . . 
 14 . . . X X O O X . . . . . . . . . . . 
 15 . . X . O . . O X X . . . . . . . . . 
 16 . . X . O . O . . . . . . . . . . . . 
 17 . . X O . . . O X X . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q17160()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17160/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 15, Content.Black);
            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 14, Content.Black);
            g.SetupMove(4, 15, Content.White);
            g.SetupMove(4, 16, Content.White);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(6, 13, Content.Black);
            g.SetupMove(6, 14, Content.White);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 15, Content.Black);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(4, 16), new Point(6, 16) };
            for (int x = 4; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(3, 16));
            gi.movablePoints.Add(new Point(3, 17));
            gi.movablePoints.Add(new Point(3, 18));
            gi.movablePoints.Add(new Point(2, 18));
            gi.movablePoints.Add(new Point(5, 14));
            gi.movablePoints.Add(new Point(6, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(3, 15));
            gi.killMovablePoints.Add(new Point(9, 16));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(7, 18), new Point(6, 18), new Point(8, 16), new Point(7, 16), new Point(5, 15), new Point(6, 15), new Point(5, 16), new Point(8, 18), new Point(3, 18) });
            gi.solutionPoints.Add(new List<Point>() { new Point(5, 17), new Point(4, 17), new Point(5, 18), new Point(7, 18), new Point(8, 16), new Point(7, 16), new Point(5, 15) });
            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17160_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17160_ChallengeMoveExtension");
            return g;
        }

        /*
 13 . . . . X X . X X . . . . . . . . . . 
 14 . . . X . O . O X . . . . . . . . . . 
 15 . . . X . O O . O X . . . . . . . . . 
 16 . . X O . . X . O X . . . . . . . . . 
 17 . . X O . . O O X X . . . . . . . . . 
 18 . . . . . . . . . . . . . . . . . . .
         */
        public Game Scenario_TianLongTu_Q16860()
        {
            //https://www.101weiqi.com/book/tianlongtu/35/16860/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 14, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 13, Content.Black);
            g.SetupMove(5, 13, Content.Black);
            g.SetupMove(5, 14, Content.White);
            g.SetupMove(5, 15, Content.White);
            g.SetupMove(6, 15, Content.White);
            g.SetupMove(6, 16, Content.Black);
            g.SetupMove(6, 17, Content.White);
            g.SetupMove(7, 13, Content.Black);
            g.SetupMove(7, 14, Content.White);
            g.SetupMove(7, 17, Content.White);
            g.SetupMove(8, 13, Content.Black);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 16, Content.White);
            g.SetupMove(8, 17, Content.Black);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 16, Content.Black);
            g.SetupMove(9, 17, Content.Black);
            gi.targetPoints = new List<Point>() { new Point(6, 17) };
            for (int x = 3; x <= 8; x++)
            {
                for (int y = 15; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(2, 18));
            gi.movablePoints.Add(new Point(6, 13));
            gi.movablePoints.Add(new Point(5, 14));
            gi.movablePoints.Add(new Point(6, 14));
            gi.movablePoints.Add(new Point(7, 14));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(4, 14));
            gi.killMovablePoints.Add(new Point(6, 12));
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(9, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(7, 16), new Point(7, 15), new Point(5, 18), new Point(3, 18), new Point(4, 16), new Point(4, 17), new Point(5, 16), new Point(4, 15), new Point(5, 17) });
            gi.solutionPoints.Add(new List<Point>() { new Point(7, 16), new Point(7, 15), new Point(5, 18), new Point(3, 18), new Point(4, 16), new Point(4, 17), new Point(7, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16860_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q16860_ChallengeMoveExtension");

            return g;
        }

        /*
 14 . . . . . . . X X . . . . . . . . . . 
 15 . . . X X X X O O X . . . . . . . . . 
 16 . . X O . O O . . . X . . . . . . . . 
 17 . . X O . . . . O O X . X . . . . . . 
 18 . . . . . . . . . . . . . . . . . . . 
         */
        public Game Scenario_TianLongTu_Q17132()
        {
            //https://www.101weiqi.com/book/tianlongtu/38/17132/
            var gi = new GameInfo(SurviveOrKill.Kill, Content.Black, 22);
            var g = new Game(gi);
            gi.ScenarioName = GetCurrentMethod();

            g.SetupMove(2, 16, Content.Black);
            g.SetupMove(2, 17, Content.Black);
            g.SetupMove(3, 15, Content.Black);
            g.SetupMove(3, 16, Content.White);
            g.SetupMove(3, 17, Content.White);
            g.SetupMove(4, 15, Content.Black);
            g.SetupMove(5, 15, Content.Black);
            g.SetupMove(5, 16, Content.White);
            g.SetupMove(6, 15, Content.Black);
            g.SetupMove(6, 16, Content.White);
            g.SetupMove(7, 14, Content.Black);
            g.SetupMove(7, 15, Content.White);
            g.SetupMove(8, 14, Content.Black);
            g.SetupMove(8, 15, Content.White);
            g.SetupMove(8, 17, Content.White);
            g.SetupMove(9, 15, Content.Black);
            g.SetupMove(9, 17, Content.White);
            g.SetupMove(10, 16, Content.Black);
            g.SetupMove(10, 17, Content.Black);
            g.SetupMove(12, 17, Content.Black);

            gi.targetPoints = new List<Point>() { new Point(5, 16) };
            for (int x = 2; x <= 10; x++)
            {
                for (int y = 16; y <= 18; y++)
                    gi.movablePoints.Add(new Point(x, y));
            }
            gi.movablePoints.Add(new Point(7, 15));
            gi.movablePoints.Add(new Point(8, 15));
            gi.killMovablePoints.AddRange(gi.movablePoints);
            gi.killMovablePoints.Add(new Point(1, 18));
            gi.killMovablePoints.Add(new Point(11, 18));

            gi.solutionPoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(6, 18), new Point(7, 18), new Point(7, 17), new Point(6, 17), new Point(5, 17), new Point(6, 18) });
            gi.dictatePoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(6, 18), new Point(6, 17), new Point(7, 18), new Point(7, 17), new Point(5, 17), new Point(8, 18), new Point(7, 16), new Point(4, 18), new Point(6, 18), new Point(7, 18), new Point(9, 18) });
            gi.dictatePoints.Add(new List<Point>() { new Point(3, 18), new Point(4, 17), new Point(5, 18), new Point(6, 18), new Point(6, 17), new Point(7, 18), new Point(7, 17), new Point(5, 17), new Point(8, 18), new Point(7, 16), new Point(9, 18), new Point(7, 18), new Point(6, 18), new Point(4, 18), new Point(10, 18), new Point(7, 18) });

            gi.PlayerMoveJson = gi.PlayerMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17132_PlayerMoveExtension");
            gi.ChallengeMoveJson = gi.ChallengeMoveJsonExtension = ResourceHelper.GetKweonKabYongMappedJsonExtensionString("Scenario_TianLongTu_Q17132_ChallengeMoveExtension");

            return g;
        }
    }
}

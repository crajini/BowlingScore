using BowlingScore.Controllers;
using BowlingScore.Models;
using BowlingScore.ScoreCalculator;
using BowlingScore.ScoreCalculator.Interface;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BowlingScore.Tests
{
    [TestFixture]
    public class BowlingControllerTests
    {
        private IScore _score;
        private BowlingController _controller;

        [SetUp]
        public void Setup()
        {
            _score = new Score();
            _controller = new BowlingController(_score);
        }

        [TestCase("10,10,10,10,10,10,10, 10, 10, 10, 10, 10", true, ExpectedResult = "300", Description = "Validate a perfect Game")]
        [TestCase("1,1,1,1,1,1,1,1,1,1,1,1", false, ExpectedResult = "12", Description = "6 frames completed, all throws 1 pin down")]
        [TestCase("1,1,1,1,9,1,2,8,9,1,10,10", false, ExpectedResult = "*", Description = "7 frames completed, with 2 strikes, 3 spares")]
        [TestCase("1,8,7,2,5,4,10,6,3,6,3,1,2", false, ExpectedResult = "67", Description = "7 frames completed, with 1 strikes, 3 spares")]
        [TestCase("0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0", true, ExpectedResult = "0", Description = "Gutter-ball game")]
        public string ValidateScore(string pins, bool gameCompleted)
        {
            //Assign
            var inputDownedPins = new List<int>();
            for (int i = 0; i < pins.Split(",").Length; i++)
                inputDownedPins.Add(Convert.ToInt32(pins.Split(",")[i]));
            var model = new RollingViewModel { PinsDowned = inputDownedPins };

            //Act
            var results =_controller.Scores(model);

            //Assert
            Assert.AreEqual(gameCompleted, results.Result.GameCompleted);
            return results.Result.FrameProgressScores[results.Result.FrameProgressScores.Count - 1];
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Pong;

public class ScoreTests
{
    private GameHandler _handler = new GameHandler();

    [Test]
    public void ShouldScoreLeft()
    {
        _handler.scoreLeft = 0;
        _handler.scoreRight = 0;

        _handler.ScorePoint(true);

        Assert.AreEqual(_handler.scoreLeft, 1);
        Assert.AreEqual(_handler.scoreRight, 0);
    }

    [Test]
    public void ShouldScoreRight()
    {
        _handler.scoreLeft = 0;
        _handler.scoreRight = 0;

        _handler.ScorePoint(false);

        Assert.AreEqual(_handler.scoreLeft, 0);
        Assert.AreEqual(_handler.scoreRight, 1);
    }

}

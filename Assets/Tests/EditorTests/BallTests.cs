using NUnit.Framework;
using UnityEngine;

using Pong;

namespace EditorTests
{

    public class BallTests
    {
        private GameHandler _handler = new GameHandler();

        [Test]
        public void ShouldInitializeBall()
        {
            GameObject ballObj = new GameObject();
            Rigidbody2D ball = ballObj.AddComponent<Rigidbody2D>();

            _handler.InitializeBall(ball);

            Assert.AreEqual(ball.transform.position, Vector3.zero);
            Assert.AreNotEqual(ball.velocity, Vector2.zero);
        }

    }

}

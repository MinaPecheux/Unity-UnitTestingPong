using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Pong;

namespace EditorTests
{

    public class PaddleTests
    {
        private GameHandler _handler = new GameHandler();

        [Test]
        public void ShouldCreatePaddles()
        {
            (GameObject left, GameObject right) = _handler.CreatePaddles();

            Assert.IsNotNull(left);
            Assert.IsNotNull(right);
        }

        [Test]
        public void ShouldPlacePaddles()
        {
            (GameObject left, GameObject right) = _handler.CreatePaddles();

            Assert.AreEqual(left.transform.position.x, -8);
            Assert.AreEqual(right.transform.position.x, 8);
        }

        [Test]
        public void ShouldInitializePaddles()
        {
            (GameObject left, GameObject right) = _handler.CreatePaddles();

            PaddleManager leftPm = left.GetComponent<PaddleManager>();
            PaddleManager rightPm = right.GetComponent<PaddleManager>();

            Assert.IsNotNull(leftPm);
            Assert.IsNotNull(rightPm);
            Assert.IsTrue(leftPm.isLeft);
            Assert.IsFalse(rightPm.isLeft);
        }

        [UnityTest]
        public IEnumerator ShouldMovePaddleUp()
        {
            // increase timeScale to execute the test quickly
            Time.timeScale = 20f;

            (GameObject left, _) = _handler.CreatePaddles();
            PaddleManager pm = left.GetComponent<PaddleManager>();

            // move paddle up
            float startY = left.transform.position.y;
            float time = 0f;
            while (time < 1)
            {
                pm.MoveUp();
                time += Time.fixedDeltaTime;
                yield return null;
            }

            Assert.Greater(left.transform.position.y, startY);

            // reset timeScale
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator ShouldMovePaddleDown()
        {
            // increase timeScale to execute the test quickly
            Time.timeScale = 20f;

            (GameObject left, _) = _handler.CreatePaddles();
            PaddleManager pm = left.GetComponent<PaddleManager>();

            // move paddle down
            float startY = left.transform.position.y;
            float time = 0f;
            while (time < 1)
            {
                pm.MoveDown();
                time += Time.fixedDeltaTime;
                yield return null;
            }

            Assert.Less(left.transform.position.y, startY);

            // reset timeScale
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator ShouldKeepPaddleBelowTop()
        {
            // increase timeScale to execute the test quickly
            Time.timeScale = 20f;

            (GameObject left, _) = _handler.CreatePaddles();
            PaddleManager pm = left.GetComponent<PaddleManager>();

            // move paddle up
            float time = 0f;
            while (time < 3)
            {
                pm.MoveUp();
                time += Time.fixedDeltaTime;
                yield return null;
            }

            Assert.LessOrEqual(left.transform.position.y, 4f);

            // reset timeScale
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator ShouldKeepPaddleAboveBottom()
        {
            // increase timeScale to execute the test quickly
            Time.timeScale = 20f;

            (GameObject left, _) = _handler.CreatePaddles();
            PaddleManager pm = left.GetComponent<PaddleManager>();

            // move paddle down
            float time = 0f;
            while (time < 3)
            {
                pm.MoveDown();
                time += Time.fixedDeltaTime;
                yield return null;
            }

            Assert.GreaterOrEqual(left.transform.position.y, -4f);

            // reset timeScale
            Time.timeScale = 1f;
        }


    }

}

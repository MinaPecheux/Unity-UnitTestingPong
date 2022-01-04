using UnityEngine;

namespace Pong
{

    public class GameHandler
    {
        private GameObject _paddlePrefab;
        public GameObject PaddlePrefab
        {
            get
            {
                if (_paddlePrefab == null)
                    _paddlePrefab = Resources.Load<GameObject>("Prefabs/Paddle");
                return _paddlePrefab;
            }
        }

        public int scoreLeft = 0;
        public int scoreRight = 0;

        public (GameObject, GameObject) CreatePaddles()
        {
            GameObject left = GameObject.Instantiate(PaddlePrefab);
            left.transform.position = new Vector2(-8, 0);

            GameObject right = GameObject.Instantiate(PaddlePrefab);
            right.transform.position = new Vector2(8, 0);
            right.GetComponent<PaddleManager>().isLeft = false;

            return (left, right);
        }

        public void InitializeBall(Rigidbody2D ball)
        {
            ball.transform.position = Vector2.zero;
            float angle = Random.Range(0, 15f) * Mathf.Deg2Rad;
            float r = Random.Range(0f, 1f);
            if (r < 0.25f)
                angle = 180f - angle;
            else if (r < 0.5f)
                angle += 180f;
            else if (r < 0.75f)
                angle = 360f - angle;
            float strength = 10f;
            ball.velocity = new Vector2(
                strength * Mathf.Cos(angle),
                strength * Mathf.Sin(angle));
        }

        public void ScorePoint(bool left)
        {
            if (left) scoreLeft++;
            else scoreRight++;
        }
    }

}

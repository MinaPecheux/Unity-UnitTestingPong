using UnityEngine;

namespace Pong
{

    public class PaddleManager : MonoBehaviour
    {
        private static float _speed = 0.25f;

        public bool isLeft;

        private KeyCode _upKey;
        private KeyCode _downKey;

        private void Start()
        {
            _upKey = isLeft ? KeyCode.W : KeyCode.UpArrow;
            _downKey = isLeft ? KeyCode.S : KeyCode.DownArrow;
        }

        void Update()
        {
            if (Input.GetKey(_upKey))
                MoveUp();
            else if (Input.GetKey(_downKey))
                MoveDown();
        }

        public void MoveUp()
        {
            transform.Translate(Vector2.up * _speed);
            if (transform.position.y > 4f)
            {
                Vector3 p = transform.position;
                p.y = 4f;
                transform.position = p;
            }
        }

        public void MoveDown()
        {
            transform.Translate(-Vector2.up * _speed);
            if (transform.position.y < -4f)
            {
                Vector3 p = transform.position;
                p.y = -4f;
                transform.position = p;
            }
        }
    }

}
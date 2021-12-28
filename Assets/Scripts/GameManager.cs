using UnityEngine;
using UnityEngine.UI;

namespace Pong
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private Rigidbody2D _ball;
        [SerializeField] private Text _scoreLeftText;
        [SerializeField] private Text _scoreRightText;

        private GameHandler _funcs;

        private void Start()
        {
            instance = this;

            _funcs = new GameHandler();

            _funcs.CreatePaddles();
            _funcs.InitializeBall(_ball);
        }

        public void ScorePoint(bool left)
        {
            _funcs.ScorePoint(left);
            _scoreLeftText.text = _funcs.scoreLeft.ToString();
            _scoreRightText.text = _funcs.scoreRight.ToString();
            _funcs.InitializeBall(_ball);
        }
    }

}

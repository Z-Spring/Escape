using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace GameUI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Transform gameOverUI;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        [Space(10)] [Header("Heart Beat")] [SerializeField]
        private Transform heartBeatImage;

        [SerializeField] private Transform heartBeatFastLine;
        [SerializeField] private Transform heartBeatDeadLine;

        private void Start()
        {
            gameOverUI.gameObject.SetActive(false);
            restartButton.onClick.AddListener(OnRestartButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
            GameManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            gameOverUI.gameObject.SetActive(true);
            //heartbeatLine
            heartBeatFastLine.gameObject.SetActive(false);
            heartBeatDeadLine.gameObject.SetActive(true);
            heartBeatImage.gameObject.SetActive(true);
        }

        private void OnRestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           
            // GameManager.Instance.SetGameState(GameManager.GameState.Start);
        }

        private void OnExitButtonClick()
        {
            Application.Quit(0);
        }
    }
}
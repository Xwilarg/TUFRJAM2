using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.GameOver
{
    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager S;

        private void Awake()
        {
            S = this;
        }

        [SerializeField]
        private GameObject _gameOver, _win, _loose;

        public bool DidGameEnd = false;

        public void Win()
        {
            _gameOver.SetActive(true);
            _win.SetActive(true);
            DidGameEnd = true;
            StartCoroutine(Restart());
        }

        public void Loose()
        {
            _gameOver.SetActive(true);
            _loose.SetActive(true);
            DidGameEnd = true;
            StartCoroutine(Restart());
        }

        private IEnumerator Restart()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Main");
        }
    }
}

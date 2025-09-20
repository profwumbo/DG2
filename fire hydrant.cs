using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class firehydrant : MonoBehaviour
{
    public TMP_Text scoreText;
    public int scoreAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            GameManager.Instance.AddScore(scoreAmount);

           
            if (scoreText != null)
            {
                scoreText.text = "Score: " + GameManager.Instance.GetScore();
            }

            Debug.Log("Player won! Current score: " + GameManager.Instance.GetScore());
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}

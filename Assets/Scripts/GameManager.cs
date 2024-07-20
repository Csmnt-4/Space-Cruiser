using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverScreen;
    public GameObject hoopSpawner;
    public GameObject playerShip;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public Button highscoreSubmitButton;
    public TextMeshProUGUI highscoreSubmitButtonText;

    public TMP_InputField usernameInputField;
    public TextMeshProUGUI usernameInputFieldText;

    public Button highscoreTableButton;
    public Button restartButton;

    private int score = 0;
    private float timer = 20.0f;
    private bool isGameOver = false;
    private FirebaseFirestore firestore;

    [SerializeField]
    private bool firestoreIsLoaded = false;

    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string GenerateUniqueID(int length = 10)
    {
        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[Random.Range(0, chars.Length)]);
        }
        return result.ToString();
    }

    public void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                firestore = FirebaseFirestore.DefaultInstance;
                firestoreIsLoaded = true;
            }
            else
            {
                Debug.LogError("Failed to initialize Firebase");
            }
        });
        UpdateScoreText();
        UpdateTimerText();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                GameOver();
            }
        }
    }

    public void CollectParticle()
    {
        score += 10;
        timer += 0.7f;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Clamp(timer, 0, timer).ToString("0");
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        playerShip.SetActive(false);
        hoopSpawner.GetComponent<HoopSpawner>().spawnerEnabled = false;
        hoopSpawner.SetActive(false);

        scoreText.SetText("");
        timerText.SetText("");
        finalScoreText.text = "Score: " + score;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            if (firestoreIsLoaded)
            {
                ShowUsernameInput();
                isGameOver = true;
            }
        }

        if (firestoreIsLoaded)
        {
            highscoreTableButton.GetComponent<Button>().interactable = true;
        }

        highScoreText.text = "High Score: " + highScore.ToString();

        PlayerPrefs.Save();
    }

    public void SubmitHighScore()
    {
        string username = usernameInputField.text;
        if (!string.IsNullOrEmpty(username))
        {
            HighScoreEntry entry = new HighScoreEntry { name = username.ToUpper(), score = score };
            firestore.Collection("highscores").Document(GenerateUniqueID()).SetAsync(entry).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    HideUsernameInput();
                }
            });
        }
    }

    private void ShowUsernameInput()
    {
        usernameInputField.gameObject.GetComponent<TMP_InputField>().interactable = true;
        highscoreSubmitButton.gameObject.GetComponent<Button>().interactable = true;
        usernameInputFieldText.gameObject.GetComponent<TextMeshProUGUI>().color = new Color(244, 196, 49);
        highscoreSubmitButtonText.gameObject.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255);
    }

    private void HideUsernameInput()
    {
        usernameInputField.gameObject.GetComponent<TMP_InputField>().interactable = false;
        highscoreSubmitButton.gameObject.GetComponent<Button>().interactable = false;
        usernameInputFieldText.gameObject.GetComponent<TextMeshProUGUI>().color = new Color(154, 115, 54);
        highscoreSubmitButtonText.gameObject.GetComponent<TextMeshProUGUI>().color = new Color(154, 154, 154);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHighScoreTable()
    {
        SceneManager.LoadScene(2);
    }
}

[FirestoreData]
public class HighScoreEntry
{
    [FirestoreProperty]
    public string name { get; set; }

    [FirestoreProperty]
    public int score { get; set; }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class DatabaseController : MonoBehaviour
{
    private FirebaseFirestore firestore;
    private bool firestoreIsLoaded;

    [SerializeReference]
    public GameObject highScoreTable;

    [SerializeReference]
    private Transform highScoreEntryContainer;

    [SerializeReference]
    public GameObject highScoreEntryPrefab;

    [SerializeReference]
    public TextMeshProUGUI debug;

    private void Start()
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
                debug.text = "Failed to initialize Firebase";
            }
        });
    }

    private void Update()
    {
        ShowHighScoreTable();
    }

    private void ShowHighScoreTable()
    {
        if (firestoreIsLoaded)
        {
            firestore.Collection("highscores").OrderByDescending("score").GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    foreach (Transform child in highScoreEntryContainer)
                    {
                        Destroy(child.gameObject);
                    }

                    QuerySnapshot snapshot = task.Result;

                    foreach (DocumentSnapshot document in snapshot.Documents)
                    {
                        HighScoreEntry entry = document.ConvertTo<HighScoreEntry>();
                        GameObject entryObject = Instantiate(highScoreEntryPrefab, highScoreEntryContainer);
                        TextMeshProUGUI[] texts = entryObject.GetComponentsInChildren<TextMeshProUGUI>();
                        texts[0].text = entry.name;
                        texts[1].text = entry.score.ToString();
                    }
                    highScoreTable.SetActive(true);
                }
                else
                {
                    Debug.LogError("Failed to retrieve highscores");
                }
            });
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
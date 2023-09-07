using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchLogic : MonoBehaviour
{
    static MatchLogic Instance;

    public int maxPoints = 3;
    public TextMeshProUGUI pointsText;
    public GameObject levelCompleteUI;
    public Button nextRoundButton; // Use Button component for the "Next Round" button

    // Array of UI images for each round
    public Image[] roundImages;

    private int points = 0;
    private int currentRound = 0;

    private void Start()
    {
        Instance = this;
        UpdatePointsText();
        nextRoundButton.onClick.AddListener(StartNextRound); // Register the button's click event
    }

    void UpdatePointsText()
    {
        pointsText.text = points + "/" + maxPoints;
        if (points == maxPoints)
        {
            ClearLines();
            levelCompleteUI.SetActive(true);
            nextRoundButton.gameObject.SetActive(true); // Show the button
        }
    }

    // Called when the "Next Round" button is clicked
    public void StartNextRound()
    {
        levelCompleteUI.SetActive(false);
        nextRoundButton.gameObject.SetActive(false); // Hide the button for now
        currentRound++;

        if (currentRound < roundImages.Length)
        {
            SetRoundImageActive(true); // Activate the next round image
            points = 0;
            pointsText.text = "0/3";
            ClearLines(); // Clear lines from the previous round
        }
        else
        {
            Debug.Log("All rounds completed");
        }
    }

    // Set the visibility of the round image
    void SetRoundImageActive(bool isActive)
    {
        if (currentRound >= 0 && currentRound < roundImages.Length)
        {
            for (int i = 0; i < roundImages.Length; i++)
            {
                roundImages[i].gameObject.SetActive(i == currentRound);
            }
        }
    }

    void ClearLines()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
    }

    public static void AddPoint()
    {
        AddPoints(1);
    }

    public static void AddPoints(int points)
    {
        Instance.points += points;
        Instance.UpdatePointsText();
    }
}

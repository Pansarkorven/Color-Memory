using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonGame : MonoBehaviour
{
    public Button[] buttons; // Assign 4 color buttons in the inspector
    public Button startButton; // Start button
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI scoreText; // New score text

    private List<int> sequence = new List<int>();
    private List<int> playerInput = new List<int>();
    private bool playerTurn = false;
    private int currentStep = 0;
    private int score = 0; // Player's score

    void Start()
    {
        infoText.text = "Press Start to Play!";
        scoreText.text = "Score: 0";
        startButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        sequence.Clear();
        playerInput.Clear();
        score = 0; 
        scoreText.text = "Score: " + score;
        infoText.text = "Watch the sequence!";
        startButton.gameObject.SetActive(false); 
        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        playerTurn = false;
        sequence.Add(Random.Range(0, 4));
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < sequence.Count; i++)
        {
            int index = sequence[i];
            buttons[index].image.color = Color.white;
            yield return new WaitForSeconds(0.7f);
            ResetButtonColors();
            yield return new WaitForSeconds(0.7f);

            playerTurn = true;
            infoText.text = "Your turn!";
            playerInput.Clear();
            currentStep = 0;
        }
    }

    public void ButtonPressed(int index)
    {
        if (!playerTurn) return;

        playerInput.Add(index);
        buttons[index].image.color = Color.white;
        StartCoroutine(ResetButtonAfterDelay(index));

        if (playerInput[currentStep] != sequence[currentStep])
        {
            infoText.text = "Game Over!";
            startButton.gameObject.SetActive(true); 
            Invoke("ResetGame", 2f);
            return;
        }

        currentStep++;

        if (playerInput.Count == sequence.Count)
        {
            score++; // Increase score
            scoreText.text = "Score: " + score;
            infoText.text = "Good job! Next round!";
            StartCoroutine(ShowSequence());
        }
    }

    IEnumerator ResetButtonAfterDelay(int index)
    {
        yield return new WaitForSeconds(0.3f);
        ResetButtonColors();
    }

    void ResetButtonColors()
    {
        buttons[0].image.color = Color.red;
        buttons[1].image.color = Color.blue;
        buttons[2].image.color = Color.green;
        buttons[3].image.color = Color.yellow;
    }

    void ResetGame()
    {
        infoText.text = "Press Start to Play!";
        scoreText.text = "Score: 0"; 
    }
}

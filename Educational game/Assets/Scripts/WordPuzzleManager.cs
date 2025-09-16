using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class WordPuzzleManager : MonoBehaviour
{
    public static WordPuzzleManager Instance;

    [Header("Puzzle Words")]
    public List<string> wordList = new List<string>() { "CAT", "MAT", "TREE","BIKE","LOL" };
    private string targetWord;

    [Header("UI")]
    public TextMeshProUGUI collectedWordText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI targetWordText;     // shows the current puzzle word

    private List<char> collectedLetters = new List<char>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        PickNewWord();
    }

    public void CollectLetter(char letter)
    {
        collectedLetters.Add(letter);

        // Display collected letters
        collectedWordText.text = "Collected: " + string.Join(" ", collectedLetters);

        // Check puzzle
        if (collectedLetters.Count == targetWord.Length)
        {
            string formedWord = new string(collectedLetters.ToArray());

            if (formedWord == targetWord)
            {
                resultText.text = " Correct! You formed " + targetWord;
            }
            else
            {
                resultText.text = " Wrong! Try again.";
            }

            // Start a new round
            Invoke(nameof(PickNewWord), 2f); // delay can player can read the text 
        }
    }

    private void PickNewWord()
    {
        collectedLetters.Clear();
        collectedWordText.text = "Collected: ";

        // Pick a random word from the list
        targetWord = wordList[Random.Range(0, wordList.Count)];

        // Show puzzle word (optional, or hide for challenge mode)
        if (targetWordText != null)
            targetWordText.text = "Puzzle: " + targetWord;

        resultText.text = "";
    }

    public string GetTargetWord()
    {
        return targetWord;
    }

}

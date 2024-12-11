using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Typer : MonoBehaviour
{
    [SerializeField] TMP_Text wordOutput = null;
    [SerializeField] Wordbank wordBank = null;
    [SerializeField] HPManager playerHP = null;
    [SerializeField] AnimationController enemyAnimator;
    [SerializeField] AnimationController playerAnimator;
    [SerializeField] float enemyHitDelay = 0.2f;
    [SerializeField] string nextLevel;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    void Start()
    {
        SetNextWord();
    }

    void Update()
    {
        CheckInput();
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(String typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordDone())
            {
                if (wordBank.isEmpty())
                {
                    EndLevel();
                }

                else
                {
                    playerAnimator.PlayAttack();
                    Invoke(nameof(EnemyHit), enemyHitDelay);
                    SetNextWord();
                }
            }
        }
        else
        {
            enemyAnimator.PlayAttack();
            playerHP.TakeHit();
            SetNextWord();

            if (wordBank.isEmpty())
            {
                Invoke(nameof(EndLevel), 1.0f);
                enabled = false;
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordDone()
    {
        return remainingWord.Length == 0;
    }

    private void SetNextWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);

    }

    private void EnemyHit()
    {
        enemyAnimator.PlayHit();
    }

    private void EndLevel()
    {
        playerAnimator.PlayAttack();
        enemyAnimator.PlayDeath();
        Invoke(nameof(NextLevel), 0.5f);
        enabled = false;
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
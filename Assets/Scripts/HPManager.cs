using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    private int HP = 3;  // Not editable in inspector due to non-customaizabilty - only 3 HP points are possible due to design an corresponding heart sprites.
    bool hitFlag = false;
    AnimationController playerAnimator = null;

    [Tooltip("The typer object of the game.")]
    [SerializeField]
    Typer typer = null;

    [Tooltip("The image assigned for showcasing current health.")]
    [SerializeField]
    Image heartUI = null;

    [Tooltip("The heart image's possible sprites (use according to max HP size only).")]
    [SerializeField]
    List<Sprite> heartSprites = null;

    [Tooltip("Ammount of time to disable the typer to not get hit again (immunity time).")]
    [SerializeField]
    public float typerDisableTime = 1.0f;

    [Tooltip("Interval to wait before actual player hit.")]
    [SerializeField]
    float takeHitDelay = 0.5f;

    [Tooltip("Interval to wait before resetting the scene after player death.")]
    [SerializeField]
    private float deathDelay = 1.0f;

    void Start()
    {
        playerAnimator = GetComponent<AnimationController>();
    }

    public void TakeHit()
    {
        Invoke(nameof(GetHit), takeHitDelay);
    }

    public void GetHit()
    {
        if (!hitFlag)
        {
            hitFlag = true;
            typer.enabled = false;

            if (HP > 0)
            {
                HP--;
            }

            heartUI.sprite = heartSprites[HP];

            if (HP > 0)
            {
                playerAnimator.PlayHit();
                Invoke(nameof(EnableTyper), typerDisableTime);
            }
            else
            {
                playerAnimator.PlayDeath();
                Invoke(nameof(ResetOnDeath), deathDelay);
            }
        }
    }

    public void EnableTyper()
    {
        typer.enabled = true;
        hitFlag = false;
    }

    private void ResetOnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
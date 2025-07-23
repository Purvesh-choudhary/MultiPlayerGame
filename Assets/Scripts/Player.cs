using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    const string UppercutPunch = "Punch_Jab_UpperCut";
    const string RightPunch = "Punch_Jab_Right";
    const string LeftPunch = "Punch_Jab_Left";
    const string KO = "KO";
    const string Hurt = "Hurt";

    

    enum PlayerTeam
    {
        PlayerA,
        PlayerB
    }

    public enum PlayerState
    {
        Idle,
        Punch,
        PowerPunch,
        Block
    }

    [SerializeField] PlayerTeam playerTeam;
    [SerializeField] PlayerState playerState = PlayerState.Idle;

    [SerializeField] Transform OponentPlayer;
    [SerializeField] Animator animator;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite idleSprite, punch1Sprite, punch2Sprite, powerPunchSprite, blockSprite;

    [SerializeField] KeyCode punch1, punch2, powerpunch, block;

    [SerializeField] Slider health_Slider;
    [SerializeField] int currentHealth = 100;

    public bool isBlocking = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(punch1))
        {
            animator.Play(RightPunch);
            Punch();
            isBlocking = false;

        }else if (Input.GetKeyDown(punch2))
        {
            animator.Play(LeftPunch);
            Punch();
            isBlocking = false;

        }
        else if (Input.GetKey(block))
        {
            isBlocking = true;

        }
        else if (Input.GetKeyDown(powerpunch))
        {
            PowerPunch();
            isBlocking = false;

        }
        else
        {
            isBlocking = false;
        }

        animator.SetBool("IsBlocking",isBlocking);
    }



    void Punch()
    {
        Debug.Log($"Punched");
        GameManager.Instance.WhoIsPunching(this);        
    }

    void PowerPunch()
    {
        Debug.Log($"Power Punched !");
        animator.Play(UppercutPunch);
        GameManager.Instance.WhoIsPoWerPunching(this);

    }

    // void Block()
    // {
    //     Debug.Log($"Blocked");
    //     spriteRenderer.sprite = blockSprite;

    // }

    // void Idle()
    // {
    //     spriteRenderer.sprite = idleSprite;
    // }



    public void Hit(int amount = 1)
    {
        currentHealth -= amount;
        health_Slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            animator.Play(KO);
        }
    } 




}

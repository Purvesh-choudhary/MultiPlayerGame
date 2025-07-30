using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Local_Player : MonoBehaviour
{

    const string PowerPunch = "Punch_Jab_UpperCut";
    const string RightPunch = "Punch_Jab_Right";
    const string LeftPunch = "Punch_Jab_Left";
    const string KO = "KO";
    const string Hurt = "Hurt";
    const string Hurt2 = "HurtAlt";

    const int NORMALPUNCHDAMAGE = 1;
    const int POWERPUNCHDAMAGE = 5;

    enum PlayerTeam
    {
        PlayerA,
        PlayerB,
        Computer
    }

    [SerializeField] PlayerTeam playerTeam;
    [SerializeField] Animator animator;

    [Header("Controls")]
    [SerializeField] KeyCode punch1;
    [SerializeField] KeyCode punch2, powerpunch, block;

    // [SerializeField] float moveSpeed;

    Slider health_Slider;
    [SerializeField] int currentHealth = 100;

    public bool isBlocking = false;
    public bool canPunch;
    public bool isDead = false;

    [SerializeField] float maxPunchDelay = 1f;
    float punchTimer;

    

    // Simple AI Settings
    [Header("Simple AI Settings")]
    [SerializeField] float aiActionTime = 0.5f; // How often AI does something
    [SerializeField] int aiAttackChance = 70; // Percentage chance to attack (0-100)
    [SerializeField] int aiBlockChance = 20; // Percentage chance to block (0-100)
    [SerializeField] int aiPowerPunchChance = 15; // Percentage chance for power punch (0-100)
    
    private float aiTimer = 0f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (playerTeam != PlayerTeam.Computer)
        {
            health_Slider = GameObject.FindGameObjectWithTag(playerTeam.ToString()).GetComponent<Slider>();
        }
        else
        {
            health_Slider = GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Slider>();
        }
        
        aiTimer = aiActionTime;
    }

    void Update()
    {
        if (isDead) return;

        // ----  Movement Mechanics  ----
        // float horizontal = Input.GetAxis("Horizontal");                         
        // Vector3 moveDirection = new Vector3(horizontal, 0f, 0f);
        // transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (playerTeam != PlayerTeam.Computer)
        {
            HandlePlayerInput();
        }
        else if (playerTeam == PlayerTeam.Computer)
        {
            ComputerAI();
        }

        // Update punch timer
        if (punchTimer <= 0)
        {
            canPunch = true;
        }
        if (canPunch == false)
        {
            punchTimer -= Time.deltaTime;
        }
        
        animator.SetBool("IsBlocking", isBlocking);
    }

    void HandlePlayerInput()
    {
        if (Input.GetKeyDown(punch1) && canPunch)
        {
            Punch(RightPunch, NORMALPUNCHDAMAGE);
        }
        else if (Input.GetKeyDown(punch2) && canPunch)
        {
            Punch(LeftPunch, NORMALPUNCHDAMAGE);
        }
        else if (Input.GetKeyDown(powerpunch) && canPunch)
        {
            Punch(PowerPunch, POWERPUNCHDAMAGE);
        }
        else if (Input.GetKey(block))
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false; // IDLE
        }
    }

    void ComputerAI()
    {
        aiTimer -= Time.deltaTime;
        
        if (aiTimer <= 0)
        {
            MakeDecision();
            aiTimer = aiActionTime; 
        }
    }

    void MakeDecision()
    {
        int randomChoice = Random.Range(1, 101);
        
        if (canPunch)
        {
            if (randomChoice <= aiAttackChance)
            {
                // Decide what type of attack
                int attackType = Random.Range(1, 101);
                
                if (attackType <= aiPowerPunchChance)
                {
                    Punch(PowerPunch, POWERPUNCHDAMAGE);
                }
                else if (attackType <= 50)
                {
                    Punch(RightPunch, NORMALPUNCHDAMAGE);
                }
                else
                {
                    Punch(LeftPunch, NORMALPUNCHDAMAGE);
                }
                isBlocking = false;
                return;
            }
        }
        
        // If not attacking, check if AI should block
        if (randomChoice <= aiBlockChance)
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
    }

    void Punch(string punchType, int damageAmount)
    {
        canPunch = false;
        isBlocking = false;
        punchTimer = maxPunchDelay;
        animator.Play(punchType);
        Local_GameManager.Instance.WhoIsPunching(this, damageAmount);

    }

    public void Hit(int damageAmount = 1)
    {
        currentHealth -= damageAmount;
        health_Slider.value = currentHealth;
        if (damageAmount == NORMALPUNCHDAMAGE)
        {
            animator.Play(Hurt);
        }
        else
        {
            animator.Play(Hurt2);
        }

        if (currentHealth <= 0)
        {
            animator.Play(KO);
            isDead = true;
            Local_GameManager.Instance.WhoDie(this);
        }
    }
}
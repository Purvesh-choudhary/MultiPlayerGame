using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FishNet.Object;

public class Player : NetworkBehaviour
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

    [SerializeField] PlayerTeam playerTeam;
    [SerializeField] Animator animator;

    [Header("Controls")]
    [SerializeField] KeyCode punch1, punch2, powerpunch, block;
    [SerializeField] float moveSpeed;

    Slider health_Slider;
    [SerializeField] int currentHealth = 100;

    public bool isBlocking = false;
    public bool canPunch;
    bool isDead = false;

    [SerializeField] float maxPunchDelay = 1f;
    float punchTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health_Slider = GameObject.FindGameObjectWithTag(playerTeam.ToString()).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsOwner) return;


        float horizontal = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontal, 0f, 0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (!isDead)
        {
            if (Input.GetKeyDown(punch1) && canPunch)
            {
                canPunch = false;
                punchTimer = maxPunchDelay;
                animator.Play(RightPunch);
                Punch();
                isBlocking = false;
            }
            else if (Input.GetKeyDown(punch2) && canPunch)
            {
                canPunch = false;
                punchTimer = maxPunchDelay;
                animator.Play(LeftPunch);
                Punch();
                isBlocking = false;
            }
            else if (Input.GetKeyDown(powerpunch) && canPunch)
            {
                canPunch = false;
                punchTimer = maxPunchDelay;
                PowerPunch();
                isBlocking = false;
            }
            else if (Input.GetKey(block))
            {
                isBlocking = true;
            }
            else
            {
                isBlocking = false;
            }


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

    public void Hit(int amount = 1)
    {
        currentHealth -= amount;
        health_Slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            animator.Play(KO);
            isDead = true;
        }
    }




}

using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

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

    [SerializeField] SpriteRenderer spriteRenderer;


    [SerializeField] Sprite idleSprite, punch1Sprite, punch2Sprite, powerPunchSprite, blockSprite;

    [SerializeField] KeyCode punch, powerpunch, block;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(punch))
        {
            playerState = PlayerState.Punch;
            
        }
        else if (Input.GetKey(block))
        {
            playerState = PlayerState.Block;
        }
        else if (Input.GetKey(powerpunch))
        {
            playerState = PlayerState.PowerPunch;
        }
        else{
            playerState = PlayerState.Idle;
        }


        switch (playerState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Punch:
                Punch();
                break;
            case PlayerState.PowerPunch:
                PowerPunch();
                break;
            case PlayerState.Block:
                Block();
                break;
        }

    }



    void Punch()
    {
        Debug.Log($"Punched");
        int r = Random.Range(0, 50);
        if (r % 2 == 0)
        {
            spriteRenderer.sprite = punch1Sprite;
        }
        else
        {
            spriteRenderer.sprite = punch2Sprite;
        }
    }

    void PowerPunch()
    {
        Debug.Log($"Power Punched !");
        spriteRenderer.sprite = powerPunchSprite;
    }

    void Block()
    {
        Debug.Log($"Blocked");
        spriteRenderer.sprite = blockSprite;
    }

    void Idle()
    {
        spriteRenderer.sprite = idleSprite;
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Player player1, player2;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhoIsPunching(Player player)
    {
        if (player == player1)
        {
            if (!player2.isBlocking)
            {
                player2.Hit();
            }
        }
        else
        {
            if (!player1.isBlocking)
            {
                player1.Hit();
            }
        }
    }

    public void WhoIsPoWerPunching(Player player)
    {
        if (player == player1)
        {
            if (!player2.isBlocking)
            {
                player2.Hit(5);
            }
        }
        else
        {
             if (!player1.isBlocking)
            {
                player1.Hit(5);
            }
        }
        
    }
    
    
}

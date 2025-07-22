using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool isThereAnyPlayer =  GameObject.FindGameObjectWithTag("Player");
        Vector3 spawnPos, spawnScale;

        if (isThereAnyPlayer)
        {
            spawnPos = new Vector3(2f, 0f, 0f);
            spawnScale = new Vector3(-1, 1, 1);
        }
        else
        {
            spawnPos = new Vector3(-2f, 0f, 0f);
            spawnScale = new Vector3(1, 1, 1);

        }
        GameObject newPlayer = PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity);
        newPlayer.transform.localScale = spawnScale;
    }
}

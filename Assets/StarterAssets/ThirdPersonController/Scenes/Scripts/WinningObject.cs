using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinningObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform Cup;
    public event  EventHandler onWinning;
    public static   WinningObject Instance { get; private set; }
    bool gameOver = false;
    // Update is called once per frame
    private void Awake()
    {
       Instance = this;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>() != null) {
           gameOver = true;
        }
    }
    public bool GameOver() {
        return gameOver;
    }
}

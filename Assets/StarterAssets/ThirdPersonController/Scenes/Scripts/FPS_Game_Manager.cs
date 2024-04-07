using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FPS_Game_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private enum State
    {
        START, WIN, END
    }
    private State state;
    public event EventHandler OnStateChanged;
    public event EventHandler OnWinning;
    public static FPS_Game_Manager instance {  get; private set; }
  
    private void Awake()
    {
        instance = this; state = State.START;

    }
   

    private void Update()
    {
        switch (state)
        {
            case State.START:


                if (WinningObject.Instance.GameOver())
                {
                    state = State.WIN;
                    OnWinning?.Invoke(this, EventArgs.Empty);

                }
                if(ThirdPersonController._instance.isDead())
                {
                    state = State.END;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.WIN:
                OnWinning?.Invoke(this, EventArgs.Empty);
                break;
            case State.END:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;

        }
    }
    public bool IsGameEnd()
    {
        return state == State.END;
    }
    public bool IsGameWin()
    {
        return state == State.WIN;
    }
    public bool IsGameStart()
    { return state == State.START; }
}

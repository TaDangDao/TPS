using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Game_Winning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] private Button playAgain;
    private void Start()
    {
        FPS_Game_Manager.instance.OnWinning += Instance_OnStateChanged;
        playAgain.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        Hide();
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (FPS_Game_Manager.instance.IsGameWin())
        {
            Show();

        }
        else
        {
            Hide();
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

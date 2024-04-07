using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;
    private void Awake()
    {
        PlayButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
            // Ẩn đi con trỏ chuột
            Cursor.visible = false;

        });
        QuitButton.onClick.AddListener(() =>
        {

            Application.Quit();
        });
    }
}

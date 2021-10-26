using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtons : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;
    
    private void Awake()
    {
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.AddListener(QuitGame);        
    }
    private void Update()
    {
        if(EventSystem.current!=null)
        {
            //Debug.Log(EventSystem.current.currentSelectedGameObject);
        }    
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Debug.Log("This is QuitGame!");
        Application.Quit();
    }    
}
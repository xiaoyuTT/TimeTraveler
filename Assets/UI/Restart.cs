using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public static bool PlayerDie = false;
    public GameObject GameUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Paused()
    {
        GameUI.SetActive(true);
        Time.timeScale = 0.0f;
        PlayerDie = true;
    }
    public void Res()
    {
        PlayerDie = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(5);
    }
}

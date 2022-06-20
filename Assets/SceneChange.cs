using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Onclick(int n)
    {
        SceneManager.LoadScene(n);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickBut : MonoBehaviour
{
    // Start is called before the first frame update
    public void restart() 
    {
        SceneManager.LoadScene(0);
    }
    public void quitGame() 
    {
        Application.Quit();
    }
}

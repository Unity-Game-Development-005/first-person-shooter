
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    //public GameObject continueButton;




    // Start is called before the first frame update
    void Start()
    {
        /*if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            if(PlayerPrefs.GetString("CurrentLevel") == "")
            {
                continueButton.SetActive(false);
            }
        } 
        
        else
        {
            continueButton.SetActive(false);
        }*/
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);

        //PlayerPrefs.SetString("CurrentLevel", "");

        //PlayerPrefs.SetString(firstLevel + "_cp", "");
    }


    public void QuitGame()
    {
        Application.Quit();
    }


} // end of class

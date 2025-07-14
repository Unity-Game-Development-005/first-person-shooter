
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public string firstLevel;

    public GameObject continueButton;




    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            if (PlayerPrefs.GetString("CurrentLevel") == "")
            {
                continueButton.SetActive(false);
            }
        }

        else
        {
            continueButton.SetActive(false);
        }
    }


    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetString("CurrentLevel", "");

        PlayerPrefs.SetString(firstLevel + "_cp", "");
    }


    public void QuitGame()
    {
        Application.Quit();
    }


} // end of class

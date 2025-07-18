
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitController : MonoBehaviour
{
    public string nextLevel;

    public float waitToEndLevel;




    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.gameController.levelEnding = true;

            StartCoroutine(EndLevelCoroutine());

            //AudioManager.instance.PlayLevelVictory();
        }
    }

    private IEnumerator EndLevelCoroutine()
    {
        //PlayerPrefs.SetString(nextLevel + "_cp", "");

        //PlayerPrefs.SetString("CurrentLevel", nextLevel);

        yield return new WaitForSeconds(waitToEndLevel);

        SceneManager.LoadScene(nextLevel);
    }


} // end of class

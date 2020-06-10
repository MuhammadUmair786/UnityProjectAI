using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu1 : MonoBehaviour
{
    public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
			if(GameIsPaused){
				Resume();
			}else{
				Pause();			}
		}    }
	public void Resume(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	public void Pause(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	public void GoToMainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
	public void Exit(){
		Debug.Log("End Game!");
		Application.Quit();
	}
}

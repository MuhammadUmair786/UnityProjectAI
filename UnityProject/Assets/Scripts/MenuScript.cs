 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame(){
		SceneManager.LoadScene(1);
	}
	public void Exit(){
		Debug.Log("End Game!");
		Application.Quit();
	}
	public void Level2(){
		SceneManager.LoadScene(2);
	}
	public void Level3(){
		SceneManager.LoadScene(3);
	}
	
	
	
}

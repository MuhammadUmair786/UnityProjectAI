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
	public void UzairGame(){
		SceneManager.LoadScene(2);
	}
	
	
}

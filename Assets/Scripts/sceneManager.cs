using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{

    public string newScene;

    public void ChangeScene(){
        SceneManager.LoadScene(newScene);
 }
}

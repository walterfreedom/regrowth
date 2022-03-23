using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveScene : MonoBehaviour
{
    public string scene;
    public void movescene()
    {
        SceneManager.LoadScene(scene);
        var manager = GameObject.Find("manager");
        manager.GetComponent<SaveScore>().score = 0;
    }
}

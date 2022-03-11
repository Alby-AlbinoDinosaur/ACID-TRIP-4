using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class settingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void pause(){

    }
    public void loadScene(int sceneNo)
    {
        
        //SceneManager.LoadScene(sceneNo);
        StartCoroutine(transition(sceneNo));
    }
    IEnumerator transition(int sceneNo){

        yield return new WaitForSeconds(1.1f);
        //AudioManager.instance.Stop("title");
        SceneManager.LoadScene(sceneNo);
    }
    public void loadSceneStr(string sceneNa)
    {

        //SceneManager.LoadScene(sceneNo);
        StartCoroutine(transitionStr(sceneNa));
    }
    IEnumerator transitionStr(string sceneNa)
    {

        yield return new WaitForSeconds(1.1f);
        //AudioManager.instance.Stop("title");
        SceneManager.LoadScene(sceneNa);
    }
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDemo : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("title");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string scene)
    {
        
        //SceneManager.LoadScene(sceneNo);
        StartCoroutine(transition(scene));
    }

    IEnumerator transition(string scene){

        yield return new WaitForSeconds(1.1f);
        AudioManager.instance.Stop("title");
        SceneManager.LoadScene(scene);
    }
}

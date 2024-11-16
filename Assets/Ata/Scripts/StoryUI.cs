using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryUI : MonoBehaviour
{
    public float delayBeforeTransition = 18f; 

    void Start()
    {

        StartCoroutine(TransitionToNextScene());
    }

    IEnumerator TransitionToNextScene()
    {
  
        yield return new WaitForSeconds(delayBeforeTransition);
   
        SceneManager.LoadScene("SampleScene");
    }
}

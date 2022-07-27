using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneScript : MonoBehaviour
{
    private IEnumerator coroutine;
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        coroutine = Wait(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }

}

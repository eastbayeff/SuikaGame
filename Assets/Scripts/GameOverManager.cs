using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    private bool _acceptingInput = false;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        _acceptingInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_acceptingInput) return;

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Title");

        else if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Main");
    }
}

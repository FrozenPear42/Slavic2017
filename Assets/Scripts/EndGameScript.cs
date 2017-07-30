using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

    private float stayTime;
    public float endGameAfterStaying;

    private AsyncOperation endSceneOperation;

	// Use this for initialization
	void Start () {
        endSceneOperation = SceneManager.LoadSceneAsync("EndScene");
        endSceneOperation.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(stayTime > endGameAfterStaying)
        {
            endSceneOperation.allowSceneActivation = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("EndScene"));
        }
        if (Input.GetKey("p")) {
            endSceneOperation.allowSceneActivation = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("EndScene"));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            stayTime = 0f;
            other.gameObject.GetComponent<EndGameScreenFlash>().MineHit();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
            stayTime += Time.deltaTime;
    }
}

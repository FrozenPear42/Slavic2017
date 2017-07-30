using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

    private float stayTime;
    public float endGameAfterStaying;

    public CanvasGroup myCG;
    private bool flash = false;

    private AsyncOperation endSceneOperation;

	// Use this for initialization
	void Start () {
        endSceneOperation = SceneManager.LoadSceneAsync("EndScene");
        endSceneOperation.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (flash)
	    {
	        myCG.alpha = myCG.alpha + Time.deltaTime;
	        if (myCG.alpha >= 1)
	        {
	            flash = false;
	        }
	    }

        if (stayTime > endGameAfterStaying)
        {
            endSceneOperation.allowSceneActivation = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("EndScene"));
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            stayTime = 0f;
            MineHit();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
            stayTime += Time.deltaTime;
    }

    public void MineHit()
    {
        flash = true;
        myCG.alpha = 0;
    }
}

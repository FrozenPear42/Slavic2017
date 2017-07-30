using UnityEngine;
using UnityEngine.UI;  // add to the top
using System.Collections;

public class EndGameScreenFlash : MonoBehaviour
{

    public CanvasGroup myCG;
    private bool flash = false;

    void Update()
    {
        if (flash)
        {
            myCG.alpha = myCG.alpha + Time.deltaTime;
            if (myCG.alpha >= 1)
            {
                flash = false;
            }
        }
    }

    public void MineHit()
    {
        flash = true;
        myCG.alpha = 0;
    }
}
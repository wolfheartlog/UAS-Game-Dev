using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour
{
    public void Finish()
    {
        LockPlayer();
        StartCoroutine(Exit());
    }

    private void LockPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().SetBool("isMoving", false);
    }
    private IEnumerator Exit()
    {
        float _time = Time.deltaTime * 0.5f;
        GameObject blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        UnityEngine.UI.Image blackScreenImage = blackScreen.GetComponent<UnityEngine.UI.Image>();
        
            for (float i = 0; i <= 1; i += _time)
            {
                blackScreenImage.color = new Color(0, 0, 0, i);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

        Application.Quit();
    }
}

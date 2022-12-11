using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSword : MonoBehaviour
{
    private FinishController finish;
    private void Start()
    {
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishController> ();
    }

    public void ActivateLevelSword()
    {
        finish.Activate();
        gameObject.SetActive(false);
    }
}

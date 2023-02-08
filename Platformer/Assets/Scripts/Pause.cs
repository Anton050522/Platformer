using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject gamePauseCanvas;

    public void PauseHandler()
    {
        gamePauseCanvas.SetActive(true);   

        Time.timeScale = 0f; //���������� ������� ���������� ���� ��� �� ���������
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseHandler();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        Exit();
    }

    void Exit()
    {
        #if UNITY_EDITOR
                // Если игра запущена в редакторе Unity, завершаем игровой режим
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}

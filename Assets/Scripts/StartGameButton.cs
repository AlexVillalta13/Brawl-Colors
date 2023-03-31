using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] MenuManager menuManager;

    private void OnEnable()
    {
        startButton.onClick.AddListener(menuManager.StartGame);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(menuManager.StartGame);
    }
}

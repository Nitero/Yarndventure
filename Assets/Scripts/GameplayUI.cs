using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages the visibility of Gameplay related UI elements
public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Image crosshair;
    [SerializeField] private Text timerText;

    public void Show()
    {
        crosshair.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        crosshair.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }
}
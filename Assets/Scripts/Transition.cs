using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Utility class for Game flow transitions
//
// All in all this class loads the appropriate scene or quits the game.
public static class Transition
{
    // Loads the Main Menu Scene
    public static void Start()
    {
        SceneManager.LoadScene(0);
    }

    // Loads the next scene in build settings
    public static void Next() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void Quit() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
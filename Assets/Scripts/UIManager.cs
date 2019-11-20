using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool game; 
    [SerializeField]
    private GameObject player;        //Public variable to store a reference to the player game object
    public List<GameObject> enemies; 
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    [SerializeField]
    private Canvas pauseCanvas;
    private bool paused;
    [SerializeField]
    private Slider healthSlider; 

    // Use this for initialization
    void Start()
    {
        if (game)
        {
            //Calculate and store the offset value by getting the distance between the player's position and camera's position.
            offset = transform.position - player.transform.position;

            GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach(GameObject e in temp)
                enemies.Add(e); 
        }
    }

    private void Update()
    {
        if (game)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (!paused)
                    PauseGame();
                else
                    ResumeGame();
            }

            healthSlider.value = player.GetComponent<Dresden>().Health / player.GetComponent<Dresden>().MAX_HEALTH;
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (game) 
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = player.transform.position + offset;
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void PauseGame()
    {
        player.GetComponent<Dresden>().Paused = true;
        foreach (GameObject e in enemies)
            e.GetComponent<Enemy>().Paused = true;

        pauseCanvas.enabled = true;

        paused = true;
    }

    public void ResumeGame()
    {
        player.GetComponent<Dresden>().Paused = false;
        foreach (GameObject e in enemies)
            e.GetComponent<Enemy>().Paused = false;

        pauseCanvas.enabled = false;

        paused = false; 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}

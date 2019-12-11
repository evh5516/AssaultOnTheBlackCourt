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
    [SerializeField]
    private Slider chargeBar;

    [SerializeField]
    private GameObject blankPickupPrefab;
    
    private Queue<(Pickup, GameObject)> activePickups = new Queue<(Pickup, GameObject)>();
    
    private Vector3[] pickupPositions = {
        new Vector3(60, 20, 0),
        new Vector3(115, 20, 0), 
        new Vector3(170, 20, 0) 
    };

    private int nextPickupPos = 0; 

    public Queue<(Pickup, GameObject)> ActivePickups
    {
        get { return activePickups; }
    }

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

        Queue<(Pickup, GameObject)> lastActivePickups = GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().ActivePickups;
        //try
        //{
        //    lastActivePickups = GameObject.Find("DataManager").GetComponent<DataManager>().ActivePickups;
        //}
        //catch
        //{
        //lastActivePickups = GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().ActivePickups;
        //}

        for (int i = 0; i < lastActivePickups.Count; i++)
        {
            AddPickup(lastActivePickups.Dequeue().Item1);
            i--;
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
            chargeBar.value = player.GetComponent<SpellCast>().HoldStrength / player.GetComponent<SpellCast>().PowerLimits[1];
            Image[] tempComponents = chargeBar.GetComponentsInChildren<Image>();
            if (chargeBar.value >= 0.5f && chargeBar.value < 1.0f)
            { 
                tempComponents[0].color = new Color(0, 1, 0, 0.5f);
                tempComponents[1].color = Color.green;
            }
            else if (chargeBar.value < 0.5f)
            {
                tempComponents[0].color = new Color(0, 1, 1, 0.5f);
                tempComponents[1].color = Color.cyan;
            }
            else
            {
                tempComponents[1].color = Color.yellow;
            }
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

    public void AddPickup(Pickup newPickup)
    {
        if (nextPickupPos == 3)
        {
            nextPickupPos = 0;
            (Pickup, GameObject) removedPickup = activePickups.Dequeue();
            removedPickup.Item1.ReverseEffect();
            //player.GetComponent<Dresden>().activePickups.Dequeue(); 
            Destroy(removedPickup.Item1.gameObject);
            Destroy(removedPickup.Item2);
        }
        newPickup.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        newPickup.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        GameObject newPickupUI = Instantiate(blankPickupPrefab, pickupPositions[nextPickupPos], Quaternion.Euler(0, 0, 0));
        newPickupUI.GetComponent<Image>().sprite = newPickup.gameObject.GetComponent<SpriteRenderer>().sprite;

        newPickupUI.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        newPickupUI.gameObject.transform.SetParent(GameObject.Find("ActiveItems").transform);

        newPickup.pickedUp = true;

        activePickups.Enqueue((newPickup, newPickupUI));

        nextPickupPos++; 
    }
}

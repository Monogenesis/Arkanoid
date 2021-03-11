using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public GameObject previewGrid;
    public GameObject blockPrefab;
    public Text scoreText;
    public Text liveText;
    public GameObject startText;
    public GameObject gameOverObject;
    public GameObject paddle;
    private int score;
    private int lives = 3;
    private static Controller controller;

    private void Start()
    {
        gameOverObject.SetActive(false);
        loadBlocks();
        previewGrid.SetActive(false);
        startText.SetActive(true);
        Time.timeScale = 0;
    }

    public static Controller GameController
    {
        get { return controller; }
        set { if(controller != null)
            {
                Destroy(value.gameObject);
            }
            else
            {
                controller = value;
            } 
        }
    }
    private void Awake()
    {
        GameController = this;
    }
    public  int Score
    {
        get { return score; }
        set { score = value; }
    }
     public  int Lives
    {
        get { return lives; }
        set { lives = value;
            liveText.text = "Lives: " + Lives;
            if(lives <= 0)
            {
                gameOverObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void addScore(int value)
    {
        Score += value;
        scoreText.text = "Score: " + Score;
    }
 
    public void restartGame()
    {
        foreach(Block block in Block.allActiveBlocks)
        {
            Destroy(block.gameObject);
        }
        Block.allActiveBlocks.Clear();
        gameOverObject.SetActive(false);
        Score = 0;
        Lives = 3;
        paddle.transform.position = new Vector3(0, 0.5f, -8);
        Time.timeScale = 0;
        startText.SetActive(true);
        loadBlocks();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
        {
            startText.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void loadBlocks()
    {
        foreach(Transform block in previewGrid.GetComponentInChildren<Transform>())
        {
           GameObject newBlock = Instantiate(blockPrefab, block.position, Quaternion.identity);
            newBlock.GetComponent<Block>().hitpoints = Random.Range(1, 5);
        }
    }
    public void exitApplication()
    {
        Application.Quit();
    }
}

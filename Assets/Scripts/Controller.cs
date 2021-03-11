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
    private int score;
    private int lives = 3;
    private static Controller controller;

    private void Start()
    {
        loadBlocks();
        previewGrid.SetActive(false);
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
        }
    }

    public void addScore(int value)
    {
        Score += value;
        scoreText.text = "Score: " + Score;
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

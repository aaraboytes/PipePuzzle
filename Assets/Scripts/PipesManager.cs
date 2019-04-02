using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipesManager : MonoBehaviour
{
    public static PipesManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [Header("Grid")]
    public GameObject cell;
    public int rows, columns;
    public int cellSize;
    int totalCells;
    int currentPipes;
    public List<GameObject> pipes = new List<GameObject>();
    public Vector2 xLimit;
    public Vector2 yLimit;
    GameObject currentHold = null;

    [Header("Ball")]
    public GameObject ball;
    public float ballForce;
    public float ballTimer;
    float timer = 0;

    [Header("UI")]
    public GameObject successPanel;

    private void Start()
    {
        totalCells = rows * columns - pipes.Count;
        GameObject grid = new GameObject("Grid");
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                Vector2 newPos;
                newPos.x = xLimit[0] + j * cellSize;
                newPos.y = yLimit[1] + i * -cellSize;
                GameObject currentCell = Instantiate(cell, newPos, Quaternion.identity);
                currentCell.transform.parent = grid.transform;
            }
        }
        ball.SetActive(false);
        successPanel.SetActive(false);
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        #region Pick obj
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit2D.collider)
            {
                currentHold = hit2D.collider.gameObject;
            }
        }
        #endregion
        #region Remove obj
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Removing obj");
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit2D.collider)
            {
                GameObject removeablePipe = hit2D.collider.gameObject;
                if (!removeablePipe.GetComponent<Pipe>().antiremoveable)
                {
                    pipes.Remove(removeablePipe);
                    currentPipes--;
                    Destroy(removeablePipe);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
            currentHold = null;
        #endregion
        #region Reposition current hold obj
        if (currentHold != null && !ball.activeInHierarchy)
        {
            //Store current pipe positions
            float storedX = currentHold.transform.position.x;
            float storedY = currentHold.transform.position.y;
            mousePos = FindPos(mousePos);
            if (mousePos.x < xLimit[0] || mousePos.x > xLimit[1])
                mousePos.x = storedX;
            if (mousePos.y < yLimit[0] || mousePos.y > yLimit[1])
                mousePos.y = storedY;

            //Dont overlap
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit2D.collider)
            {
                mousePos.x = storedX;
                mousePos.y = storedY;
            }
            //Move
            mousePos.z = 0;
            currentHold.transform.position = mousePos;
        }
        #endregion

        #region Reactivate reposition
        if (ball.activeInHierarchy)
        {
            Rigidbody2D ballBody = ball.GetComponent<Rigidbody2D>();
            float absVel = Mathf.Abs(ballBody.velocity.x) + Mathf.Abs(ballBody.velocity.y);
            if (absVel < 0.1f)
                timer += Time.deltaTime;
            if (timer > ballTimer)
            {
                HideBall();
            }
        }
        #endregion

        #region Reset scene
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        #endregion
    }
    public void HideBall()
    {
        timer = 0;
        ball.SetActive(false);
        foreach (GameObject pipe in pipes)
        {
            if (pipe.GetComponent<Pipe>())
                pipe.GetComponent<Pipe>().Reset();
        }
    }
    public void GeneratePipe(GameObject pipe)
    {
        if (currentPipes < totalCells && !ball.activeInHierarchy)
        {
            Vector3 newPos = Vector3.zero;

            //Check overlap
            RaycastHit2D hit2D = Physics2D.Raycast(newPos, Vector3.zero);
            if (hit2D.collider)
            {
                for(int i = 0; i < rows; i++)
                {
                    for(int j = 0; j < columns; j++)
                    {
                        Vector2 suposedNewPos;
                        suposedNewPos.x = j * cellSize + xLimit[0];
                        suposedNewPos.y = i * cellSize + yLimit[0];
                        hit2D = Physics2D.Raycast(suposedNewPos, Vector3.zero);
                        if (!hit2D.collider)
                        {
                            newPos = suposedNewPos;
                            break;
                        }
                    }
                }
            }

            GameObject currentPipe = Instantiate(pipe, newPos, pipe.transform.rotation);
            currentPipes++;
            pipes.Add(currentPipe);
        }
    }
    public Vector2 FindPos(Vector2 pos)
    {
        Vector2 newPos = Vector2.zero;
        bool flagX = false;
        bool flagY = false;
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                float x = xLimit[0] + j * cellSize;
                float y = yLimit[1] + i * -cellSize;
                if(pos.x > x && pos.x < x + cellSize)
                {
                    newPos.x = x;
                    flagX = true;
                }
                if (pos.y > y && pos.y < y + cellSize)
                {
                    newPos.y = y;
                    flagY = true;
                }
                if (flagX && flagY)
                    break;
            }
        }
        return newPos;
    }
    public void StartGame(Pipe startPipe)
    {
        timer = 0;
        foreach(GameObject pipe in pipes)
        {
            if(pipe.GetComponent<Pipe>())
                pipe.GetComponent<Pipe>().Reset();
        }
        startPipe.Travel();
    }
    public void GameOver()
    {

    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PhotonView))]
public class PlayerMovement : Photon.MonoBehaviour {

    public float moveSpeed = 2f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Vertical;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private int xChange;
    private int yChange;
    private bool isMoving = false;
    private bool inCombat = false;
    private bool inWorld = true;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private float t;
    private float factor;
    PlayerController player;
    World world;
    MainGameSceneController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameSceneController>();
        player = this.gameObject.GetComponent<PlayerController>();
        startPosition = new Vector3(player.gameData.locX, player.gameData.locY, 0);
        transform.position = startPosition;
        world = WorldController.World;
        player.gameData.gameState = "world";
        GameDataService.updateGameData(player.gameData);
    }

    public void Update()
    {
        if (!photonView.isMine)
        { 
            return;
        }

        if (player.gameData.gameState.Equals("world"))
        {
            inWorld = true;
        } else
        {
            inWorld = false;
        }

        xChange = 0;
        yChange = 0;

        if (!isMoving && inWorld)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("trying to move via mouse click.");
                // movement by mouse takes priority
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                if (Mathf.Abs(mouseWorldPos.x - player.transform.position.x) > Mathf.Abs(mouseWorldPos.y - player.transform.position.y))
                {
                    // horiz movement
                    if (mouseWorldPos.x > player.transform.position.x)
                    {
                        xChange += 1;
                    }
                    else
                    {
                        xChange -= 1;
                    }
                }
                else
                {
                    // vert movement
                    if (mouseWorldPos.y > player.transform.position.y)
                    {
                        yChange += 1;
                    }
                    else
                    {
                        yChange -= 1;
                    }
                }

            }

            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // movement by keyboard
                Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                if (!allowDiagonals)
                {
                    if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                    {
                        input.y = 0;
                        if (input.x > 0)
                        {
                            xChange += 1;
                        }
                        else
                        {
                            xChange -= 1;
                        }
                    }
                    else
                    {
                        input.x = 0;
                        if (input.y > 0)
                        {
                            yChange += 1;
                        }
                        else
                        {
                            yChange -= 1;
                        }
                    }
                }
            }
            //Debug.Log("After move input, xChange = " + xChange + " and yChange = " + yChange);
            if ((xChange != 0 || yChange != 0))
            {
                Debug.Log("Current x: " + player.gameData.locX + " Current y: " + player.gameData.locY);
                Debug.Log("Trying to move to: " + ((int)player.gameData.locX + xChange).ToString() + ", " + ((int)player.gameData.locY + yChange).ToString() + " Tile type: " + world.GetTileAt((int)player.gameData.locX + xChange, (int)player.gameData.locY + yChange).GroundType.ToString());
                GroundType groundType = world.GetTileAt((int)player.gameData.locX + xChange, (int)player.gameData.locY + yChange).GroundType;
                if (groundType == GroundType.WATER || groundType == GroundType.MOUNTAIN)
                {
                    Debug.Log("Cannot move, impassable tile.");
                }
                else
                {
                    GameDataService.updateGameData(player.gameData);
                    StartCoroutine(move(transform));
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            Debug.Log("Player collided with an enemy!");
            player.gameData.gameState = "combat";
            player.gameData.combatEncounterId = collision.gameObject.GetComponent<EnemyController>().encounter.encounterId;
            GameDataService.updateGameData(player.gameData);
            gameController.combatWindow.SetActive(true);
            gameController.combatWindow.GetComponent<CombatController>().StartNewCombat(player.gameData);
        }
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(xChange) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(yChange) * gridSize);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(xChange) * gridSize,
                startPosition.y + System.Math.Sign(yChange) * gridSize, startPosition.z);
        }
        player.gameData.locX = (int)endPosition.x;
        player.gameData.locY = (int)endPosition.y;

        if (allowDiagonals && correctDiagonalSpeed && xChange != 0 && yChange != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        
        isMoving = false;
        
        if (player.gameData.gameState.Equals("combat"))
        {
            inCombat = true;
        }
        yield return 0;
    }
}

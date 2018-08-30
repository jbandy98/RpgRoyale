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
    public Vector3 startPosition;
    public Vector3 endPosition;
    private float t;
    private float factor;
    PlayerManager player;
    World world;

    private void Start()
    {
        player = this.gameObject.GetComponent<PlayerManager>();
        startPosition = new Vector3(player.data.location.x, player.data.location.y, 0);
        transform.position = startPosition;
        world = WorldController.World;
    }

    public void Update()
    {
        if (!photonView.isMine)
        { 
            return;
        }

        xChange = 0;
        yChange = 0;

        if (!isMoving)
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
            Debug.Log("After move input, xChange = " + xChange + " and yChange = " + yChange);
            if ((xChange != 0 || yChange != 0))
            {
                Debug.Log("Current x: " + player.data.location.x + " Current y: " + player.data.location.y);
                Debug.Log("Trying to move to: " + ((int)player.data.location.x + xChange).ToString() + ", " + ((int)player.data.location.y + yChange).ToString() + " Tile type: " + world.GetTileAt((int)player.data.location.x + xChange, (int)player.data.location.y + yChange).GroundType.ToString());
                GroundType groundType = world.GetTileAt((int)player.data.location.x + xChange, (int)player.data.location.y + yChange).GroundType;
                if (groundType == GroundType.WATER || groundType == GroundType.MOUNTAIN)
                {
                    Debug.Log("Cannot move, impassable tile.");
                }
                else
                {
                    StartCoroutine(move(transform));
                }
            }
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
        player.data.location = new Vector3Int((int)endPosition.x, (int)endPosition.y, 0);

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
        
        yield return 0;
    }
}

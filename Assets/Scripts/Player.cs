using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Transform player;
    Rigidbody2D body;
    Camera mainCam;
    public bool CanJump = true;
    public float speed = 10f;
    void Start()
    {
        player = GetComponent<Transform>();
        body = player.GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        mainCam = Camera.main;
    }
    void FixedUpdate()// Fixed update is better for movement because it's linked to the physics engine. So if the game is laggy. This will be laggy too. Making it look alot more smooth.
    {
        float inputX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; 
        player.position += player.right * inputX;
        Vector3 Goto = new Vector3(player.position.x,player.position.y,-10) - mainCam.transform.position;
        mainCam.transform.position += Goto * (speed/2) * Time.deltaTime;


        CanJump = CheckJump();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)//This is not in the fixed update because it doesn't work properly there.
        {
            body.AddForce(player.up * (speed * 32));
            CanJump = false;
        }

        if (Input.GetKeyDown("q")) // This is to save
        {
            print("Game Saved!");
            GameData SaveThis = new GameData(transform.position);
            SavingM.Save.SaveGame(SaveThis);
        }
        if (Input.GetKeyDown("e")) // This is to load
        {
            print("Loaded!");
            GameData PlayerData = SavingM.Save.LoadGame();
            if (PlayerData != null)
                Convert(PlayerData);
            else print("Couldn't load!");
        }
    }
    void Convert(GameData data)
    {
        player.position = new Vector2(data.x,data.y);
    }
    bool CheckJump()
    {
        int layer = 1 << 6;
        layer = ~layer;
        RaycastHit2D hit = Physics2D.Raycast(player.position, -player.up, 1.2f, layer);
        Debug.DrawRay(player.position, -player.up * 1.2f, Color.red);
        if (hit.collider != null)
        {
            //print(hit.collider.name);
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject[] menuButtons;

    PlayerInput playerInput;
    public PlayerCamera playerCamera;

    public bool isPaused;

    public RectTransform handItem;
    public bool itemSelected;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        isPaused = false;

        //Sets all menu elements to false
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Check for pause button

        if (Input.GetKey(playerInput.pauseKey))    //If statement that will play if the user presses the 'escape' key, and the player is not a ghost.
        {
            pauseMenu();                                               //Runs the pauseMenu(); method.
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerCamera.enabled = true;
            isPaused = true;

            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].SetActive(false);
            }
        }

    }

    //The comments are from my grade 11 culminating lmao (I'm too lazy to get rid of them)
    void pauseMenu()                                              // pasueMenu method that can be called at anytime.
    {
        Cursor.lockState = CursorLockMode.None;                   // Unlocks the mouse cursor so that the user is able to select an option in the menu with their cursor.
        Cursor.visible = true;                                    // Sets the cursor to visible, taken from: https://forum.unity.com/threads/mouse-cursor-locked-in-centre-but-still-visible-build-only.516903/
        playerCamera.enabled = false;
        isPaused = true;

        for (int i = 0; i < menuButtons.Length; i++)              //For loop that runs as many times as the length of the menuButtons array, which stores all the components of the pause menu.
        {
            menuButtons[i].SetActive(true);                       // Sets the [i] index of the menuButtons array to SetActive. This will enable all the elements of the array.
        }



        //For dragging items around

        Vector3 mousePos = Input.mousePosition;

        if(mousePos.x > (handItem.position.x - handItem.rect.width/2) && mousePos.x < (handItem.position.x + handItem.rect.width / 2))
        {
            if(mousePos.y > (handItem.position.y - handItem.rect.height / 2) && mousePos.y < (handItem.position.y + handItem.rect.height / 2))
            {

                if(Input.GetKey(playerInput.pickUp))
                {
                    handItem.position = new Vector2(mousePos.x, mousePos.y);

                }

            }

        }

    }

}

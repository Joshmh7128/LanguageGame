using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TileClass : MonoBehaviour
{
    // our tile generator manager
    [SerializeField] TileGenerator tileGenerator;
    [SerializeField] Color localColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshPro localTextBlack;
    [SerializeField] TextMeshPro localTextWhite;
    [SerializeField] string nextScene; // which scene are we moving to next?

    // tile scripting variable
    [SerializeField] bool boolCheckSingleton; // used to make sure the player can't flip a bool infinitely
    [SerializeField] bool invertedDoor; // used to make a door work in the opposite way of it's button for puzzle purposes
    [SerializeField] int doorID; // for using multiple doors in the same puzzle
    [Header("INPUT SWITCH FOR DOOR")]
    [SerializeField] TileClass associatedBool; // used to make sure the player can't flip a bool infinitely

    // this script is here to be placed on every tile
    public enum TileTypes {
        floor, // null
        wall, // stops player from walking
        goal, // finish level
        booleanT, // boolean
        booleanF, // boolean
        symbolA, // one of three generated symbols for lock component puzzles
        symbolB, // one of three generated symbols for lock component puzzles
        symbolC, // one of three generated symbols for lock component puzzles
        door // unlocks when certain parameters are met
    };

    public TileTypes TileType;

    private void Start()
    {
        // set our associatedBool so that we don't fucking break down
        if (TileType != TileTypes.door)
        {
            associatedBool = this;
        }

        // make sure we can have our bool flipped
        boolCheckSingleton = true;

        // make sure we have a sprite renderer
        if (spriteRenderer == null)
        { spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); }

        // find tilemanager if it's null
        if (tileGenerator == null)
        {
            tileGenerator = GameObject.Find("TileManager").GetComponent<TileGenerator>();
        }
        // add ourselves to the tileGenerator's list of all tiles
        tileGenerator.allTiles.Add(this);
    }

    // set our color
    public void SetColor()
    {
        Debug.Log("Setting tile colors...");

        // set the color
        spriteRenderer.color = tileGenerator.tileColors[(doorID+2)+(int)TileType];

        // set our text
        switch (TileType)
        {
            // walls have one character
            case TileTypes.wall:
                localTextBlack.text = tileGenerator.characterSetList[(int)TileType].ToString();
                localTextWhite.text = tileGenerator.characterSetList[(int)TileType].ToString();
                break;

            // goals have three of the same character
            // GGG
            case TileTypes.goal:
                string goalText = tileGenerator.characterSetList[(int)TileType].ToString() + tileGenerator.characterSetList[(int)TileType].ToString() + tileGenerator.characterSetList[(int)TileType].ToString();
                localTextBlack.text = goalText;
                localTextWhite.text = goalText;
                break;

            // doors have several different letters
            // DX
            case TileTypes.door:
                string doorText = tileGenerator.characterSetList[(int)TileTypes.door].ToString() + tileGenerator.characterSetList[(int)TileTypes.door + doorID].ToString();
                localTextBlack.text = doorText;
                localTextWhite.text = doorText;
                break;

            // when a boolean block is true, use the door symbol and the true symbol
            // DXA
            case TileTypes.booleanT:
                string booleanTtext = tileGenerator.characterSetList[(int)TileTypes.door].ToString() + tileGenerator.characterSetList[(int)TileTypes.door + doorID].ToString() + tileGenerator.characterSetList[(int)TileTypes.booleanT].ToString();
                localTextBlack.text = booleanTtext;
                localTextWhite.text = booleanTtext;
                break;

            // when a boolean block is true, use the door symbol and the false symbol
            // DXC
            case TileTypes.booleanF:
                string booleanFtext = tileGenerator.characterSetList[(int)TileTypes.door].ToString() + tileGenerator.characterSetList[(int)TileTypes.door + doorID].ToString() + tileGenerator.characterSetList[(int)TileTypes.booleanF].ToString();
                localTextBlack.text = booleanFtext;
                localTextWhite.text = booleanFtext;
                break;
               
                /*
        symbolA, // one of three generated symbols for lock component puzzles
        symbolB, // one of three generated symbols for lock component puzzles
        symbolC, // one of three generated symbols for lock component puzzles*/
        }
    }

    private void FixedUpdate()
    {

        // if we're a door, match the status of our switch
        if (TileType == TileTypes.door)
        {
            if (invertedDoor == false)
            {
                // if the bool is true, door is up
                if (associatedBool.TileType == TileTypes.booleanT)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                    // turn on our collider
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }

                // if the bool is false, door is down
                if (associatedBool.TileType == TileTypes.booleanF)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.1f);
                    // turn off our collider
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }

            if (invertedDoor == true)
            {
                // if the bool is true, door is up
                if (associatedBool.TileType == TileTypes.booleanT)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.1f);
                    // turn on our collider
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }

                // if the bool is false, door is down
                if (associatedBool.TileType == TileTypes.booleanF)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
                    // turn off our collider
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }

    // collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TileType == TileTypes.goal)
        {
            // load into our next scene
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            Debug.Log("Goal hit");
        }

        if (TileType == TileTypes.door)
        {

        }

        if ((TileType == TileTypes.booleanF) && (boolCheckSingleton == true))
        {
            TileType = TileTypes.booleanT;
            boolCheckSingleton = false;
        }

        if ((TileType == TileTypes.booleanT) && (boolCheckSingleton == true))
        {
            TileType = TileTypes.booleanF;
            boolCheckSingleton = false;
        }

        // check for color or text changes
        SetColor();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        boolCheckSingleton = true;
        // check for color or text changes
        SetColor();
    }
}

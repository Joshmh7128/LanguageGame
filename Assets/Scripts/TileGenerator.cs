using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    /// 
    /// this script is used to generate tile symbols and how tiles look
    /// 

    // list of all tiles in the scene
    public List<TileClass> allTiles;

    // here is our alphabet
    public string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // setup colors for each of our block types, found in our TileClass
    Color wallColor, goalColor, doorColor, booleanTColor, booleanFColor, symbolAColor, symbolBColor, symbolCColor;
    public List<Color> tileColors;
    bool colorSetSingleton;
    // setup symbols to be used for overlaying on our symbols
    public char wallChar, goalChar, booleanTChar, booleanFChar, doorChar, symAChar, symBChar, symCChar;
    public List<char> characterSetList;
    [SerializeField] List<char> usedCharactersList; // use this to check and see if a character is already used

    // start runs when the object is enabled
    private void Start()
    {
        // don't destroy on load because we will need this later
        // DontDestroyOnLoad(gameObject);

        // add our colors to our colors list
        tileColors.Add(wallColor);
        tileColors.Add(goalColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor);
        tileColors.Add(doorColor); // add more door colors just in case we need them
        tileColors.Add(booleanTColor);
        tileColors.Add(booleanFColor);
        tileColors.Add(symbolAColor);
        tileColors.Add(symbolBColor);
        tileColors.Add(symbolCColor);

        // add characters to the setup list
        #region // Adding Characters
        characterSetList.Add(wallChar);
        characterSetList.Add(goalChar);
        characterSetList.Add(booleanTChar);
        characterSetList.Add(booleanFChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar);
        characterSetList.Add(doorChar); // added a lot of extra inputs for more letters to be generated for use
        characterSetList.Add(symAChar);
        characterSetList.Add(symBChar);
        characterSetList.Add(symCChar);
        #endregion

        // set our color set singleton
        colorSetSingleton = false;

        // set our characters per puzzle piece type
        int i;

        for (i = 0; i < characterSetList.Count; i++)
        {
            char randLetter = st[Random.Range(0, st.Length)];

            while (usedCharactersList.Contains(randLetter))
            {
                randLetter = st[Random.Range(0, st.Length)];
                break;
            }

            // if that letter has not been used before...
            if (!(usedCharactersList.Contains(randLetter)))
            {
                characterSetList[i] = randLetter;
                usedCharactersList.Add(randLetter);
                Debug.Log("using " + randLetter);
            }
        }

        // run PostStart
        PostStart();
    }

    private void PostStart()
    {
        // cycle through the list and randomize the colors
        int i;
        int colorCount = tileColors.Count;
        for (i = 0; i < colorCount; i++)
        {
            tileColors[i] = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1, 1, 1);
        }
    }

    private void Update()
    {
        // check to see if we have done this yet
        if (colorSetSingleton == false)
        {
            // set the colors of our tiles
            SetColor();
        }
        colorSetSingleton = true;
    }

    private void SetColor()
    {
        Debug.Log("Generator Initiating Color Set...");
        // tell all our tiles in the scene to check
        int i = 0;
        foreach (TileClass tile in allTiles)
        {
            allTiles[i].SetColor();
            i++;
        }
    }
}

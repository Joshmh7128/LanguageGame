using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables

    // how much we move
    [SerializeField] int movementIncriment;
    // our stage limits
    [SerializeField] int horizontalLimit;
    [SerializeField] int verticalLimit;
    // mvoement raycast booleans
    [SerializeField] bool upFree; 
    [SerializeField] bool downFree; 
    [SerializeField] bool leftFree; 
    [SerializeField] bool rightFree; 

    // Update is called once per frame
    void Update()
    {
        // when any of the WASD keys are pressed down, move to another tile
        if (Input.GetKeyDown(KeyCode.W))
        {
            if ((transform.position.y < verticalLimit) && (upFree == true))
            transform.position += new Vector3(0, movementIncriment, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if ((transform.position.y > -verticalLimit) && (downFree == true))
            transform.position += new Vector3(0, -movementIncriment, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if ((transform.position.x > -horizontalLimit) && (leftFree == true))
            transform.position += new Vector3(-movementIncriment, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if ((transform.position.x < horizontalLimit) && (rightFree == true))
            transform.position += new Vector3(movementIncriment, 0, 0);
        }

    }

    private void FixedUpdate()
    {

        // cast rays to see which way we can move
        float castAmount = 0.4f;

        // Debug.DrawRay(transform.position + new Vector3(0, 0.55f), new Vector2(0, castAmount));
        // Debug.DrawRay(transform.position + new Vector3(0, -0.55f), new Vector2(0, -castAmount));
        // Debug.DrawRay(transform.position + new Vector3(-0.55f, 0), new Vector2(-castAmount, 0));
        // Debug.DrawRay(transform.position + new Vector3(0.55f, 0), new Vector2(castAmount, 0));

        // up
        RaycastHit2D upCheck = Physics2D.Raycast(transform.position + new Vector3(0, 0.55f), Vector2.up, castAmount);
        if (upCheck.transform != null)
        {
            // get our the current tileclass collision
            if (upCheck.transform.gameObject.CompareTag("wall"))
            {
                // Debug.Log("wall detected up");
                upFree = false;
            }
            
            if (!upCheck.transform.gameObject.CompareTag("wall"))
            {
                // Debug.Log("wall detected up");
                upFree = true;
            }

        }
        else
        {
            upFree = true;
        }

        // down
        RaycastHit2D downCheck = Physics2D.Raycast(transform.position + new Vector3(0, -0.55f), Vector2.down, castAmount);
        if (downCheck.transform != null)
        {
            // get our the current tileclass collision
            if (downCheck.transform.gameObject.CompareTag("wall"))
            {
                // Debug.Log("wall detected down");
                downFree = false;
            }

            if (!downCheck.transform.gameObject.CompareTag("wall"))
                {
                // Debug.Log("wall detected down");
                downFree = true;
            }
        }
        else
        {
            downFree = true;
        }

        // left
        RaycastHit2D leftCheck = Physics2D.Raycast(transform.position + new Vector3(-0.55f, 0), Vector2.left, castAmount);
        if (leftCheck.transform != null)
        {
            // get our the current tileclass collision
            if (leftCheck.transform.gameObject.CompareTag("wall"))
                {
                // Debug.Log("wall detected left");
                leftFree = false;
            }

            if (!leftCheck.transform.gameObject.CompareTag("wall"))
                {
                // Debug.Log("wall detected left");
                leftFree = true;
            }
        }
        else
        {
            leftFree = true;
        }

        // right
        RaycastHit2D rightCheck = Physics2D.Raycast(transform.position + new Vector3(0.55f, 0), Vector2.right, castAmount);
        if (rightCheck.transform != null)
        {
            // get our the current tileclass collision
            if (rightCheck.transform.gameObject.CompareTag("wall"))
            {
                // Debug.Log("wall detected right");
                rightFree = false;
            }

            if (!rightCheck.transform.gameObject.CompareTag("wall"))
            {
                // Debug.Log("wall detected right");
                rightFree = true;
            }
        }
        else
        {
            rightFree = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    public List<GameObject> stack = new List<GameObject>();
    public Vector3 topOfStack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Turn a defeated enemy into an ally and add it to the player stack
    public void addAlly(GameObject enemy)
    {
        GameObject newAlly = Instantiate(enemy, player.playerTransform, false);
        float allyHeight = newAlly.GetComponent<BoxCollider2D>().bounds.size.y;
        topOfStack += new Vector3(0, allyHeight, 0);
        newAlly.transform.localPosition = topOfStack;
        topOfStack += new Vector3(0, allyHeight * 0.5f, 0);
        stack.Add(newAlly);
        whatAlly(newAlly, true);
    }

    //Remove an ally from the stack
    public void removeAlly(int i)
    {
        GameObject removedAlly = stack[i];
        if (removedAlly.tag.Equals("SlimeAlly"))
            PlayerController.instance.currentNumJumps--;
        Vector3 removeHeight = new Vector3(0, removedAlly.GetComponent<BoxCollider2D>().bounds.size.y * 1.5f, 0);
        Destroy(stack[i]);
        stack.RemoveAt(i);
        topOfStack -= removeHeight;
        whatAlly(removedAlly, false);
    }

    //Check what ally is being added/removed
    public void whatAlly(GameObject ally, bool adding)
    {
        switch (ally.tag)
        {
            case "SlimeAlly":
                if (adding)
                    player.totalNumJumps++;
                else
                    player.totalNumJumps--;
                break;

            case "GhostAlly":
                if (adding)
                    changeGWLayer(true);
                else if (!containsAlly("GhostAlly"))
                    changeGWLayer(false);
                break;

            case "SnakeAlly":
                if (adding)
                    player.removeFriction(true);
                else if (!containsAlly("SnakeAlly"))
                    player.removeFriction(false);
                break;

            case "DemonAlly":
                if (adding)
                    player.touchLava = true;
                else if (!containsAlly("DemonAlly"))
                    player.touchLava = false;
                break;

            case "RockAlly":
                if (adding)
                    player.canJump = false;
                else if (!containsAlly("RockAlly"))
                    player.canJump = true;
                break;

            case "MushroomAlly":
                if (adding)
                    seeShroomPlat(true);
                else if (!containsAlly("MushroomAlly"))
                    seeShroomPlat(false);
                break;

            case "SwitcherooAlly":
                if (adding)
                    player.controlsReversed = true;
                else if (!containsAlly("SwitcherooAlly"))
                    player.controlsReversed = false;
                break;
        }
    }

    //Check if the stack contains a type of ally
    public bool containsAlly(string tag)
    {
        for(int i=0; i<stack.Count; i++)
        {
            if (stack[i].CompareTag(tag))
                return true;
        }
        return false;
    }

    //Change the layer of the ghost walls so that they don't collide with the player/stack
    private void changeGWLayer(bool canPass)
    {
        GameObject[] ghostWalls = GameObject.FindGameObjectsWithTag("GhostWall");
        foreach(GameObject wall in ghostWalls)
        {
            if (canPass)
                wall.layer = 6;
            else
                wall.layer = 0;
        }
    }

    private void seeShroomPlat(bool canSee) {
        GameObject[] shroomFloor = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
    
        foreach(GameObject floor in shroomFloor)
        {
            if(floor.CompareTag("MushroomFloor"))
            {
                if (canSee)
                    floor.SetActive(true);
                else
                    floor.SetActive(false);
            }
        }
    }
}

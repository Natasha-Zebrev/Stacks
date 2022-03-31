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
        GameUI.instance.CheckWin(stack.Count - 1);
    }

    //Remove an ally from the stack
    public void removeAlly(int i)
    {
        GameObject removedAlly = stack[i];
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
        }
    }
}

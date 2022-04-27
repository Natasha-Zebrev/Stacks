using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private SpriteRenderer stripRenderer;
    [SerializeField] private float moveSpeed;
    private float startingY;
    private float endingY;
    private bool poweredOn = false;
    private bool usable = true;
    [SerializeField] private float upDistance;
    // Start is called before the first frame update
    void Start()
    {
        startingY = mainTransform.position.y;
        endingY = startingY + upDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if(mainTransform.position.y < endingY)
        {
            moveElevator();
            Debug.Log("This elevator is " + usable);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && PlayerController.instance.stackController.containsAlly("ElectricAlly"))
        {
            powerOn();
            Debug.Log(poweredOn);
        }
    }

    private void moveElevator()
    {
        Vector3 obsPos = mainTransform.position;
        if(poweredOn == true)
        {
            if(obsPos.y < endingY)
            {
                mainTransform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
            if(obsPos.y >= endingY)
            {
                usable = false;
                poweredOn = false;
            }
        }
    }

    public void powerOn()
    {
        if(usable == true)
        {
            stripRenderer.color = new Color(255, 255, 255);
            poweredOn = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    public float Speed 
    {
        get { return speed; }  
        set { speed= value; }

    }
    [SerializeField] float maxSwerve = 1f; 
    float lastPosX;
    float moveX;
    [SerializeField] float swerveSpeed = 5;
    [SerializeField] float ClampX;

    void Update()
    {
        GetMouseDelta();
        CharacterMove();
    }
 
    void GetMouseDelta()
    {

        if (Input.GetMouseButtonDown(0))
        {
            
            lastPosX = Input.mousePosition.x;
        }

        else if (Input.GetMouseButton(0))
        {

            moveX = Input.mousePosition.x - lastPosX;

            lastPosX = Input.mousePosition.x;

        }

        else if (Input.GetMouseButtonUp(0))
        {
            moveX = 0f;

        }
    }
    void CharacterMove()
    {
    
        float swerveAmount = moveX * Time.fixedDeltaTime * swerveSpeed;

        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerve, maxSwerve);

        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward + transform.right * swerveAmount * swerveSpeed, Time.deltaTime * speed);
       
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -ClampX, ClampX), transform.position.y, transform.position.z);
    }

}

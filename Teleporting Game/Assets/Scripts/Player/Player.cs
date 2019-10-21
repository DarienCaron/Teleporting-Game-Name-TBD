using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public FirstPersonCamera PlayerEyes;
    public PlayerController PlayerBody;


    void Start()
    {
        if (!PlayerEyes)
        {
            PlayerEyes = Camera.main.GetComponent<FirstPersonCamera>();
        }
        if (!PlayerBody)
            PlayerBody = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray = PlayerEyes.GetCenterCameraRay();

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.black);



            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<TestTele>())
                {
                    var Teleporter = hit.collider.GetComponent<TestTele>();


                    
                    PlayerBody.ForceBodyRotation(hit.normal);
                    transform.position = hit.point;



                }
            }


        }
    }




}

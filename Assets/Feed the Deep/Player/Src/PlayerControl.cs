using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;
    [SerializeField] Vector3 rotOffset;
    private Rigidbody rb;
    
    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.W)) 
		{
			Move();
		}

        Rotate();
    }

    private void Rotate(){
        Vector3 playerPos = cam.WorldToScreenPoint(transform.position);
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - playerPos.x;
        mousePos.y = mousePos.y - playerPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, cam.transform.rotation.z, 0) * Quaternion.Euler(new Vector3(0, -angle, 0)) * Quaternion.Euler(rotOffset);
    }

     private void Move(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
             float distBetweenMouseAndPlayer = Vector3.Distance(transform.position, hit.point);
             if(distBetweenMouseAndPlayer > 1.5f){
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * speed);
            }               
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;
    [SerializeField] Vector3 rotOffset;
    private Rigidbody rb;
    private Animator animator;
    private AimingLine aimingLine;

    public float width = 0.1f;
	public Color color = Color.red;
    
    void Awake(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        aimingLine = GetComponentInChildren<AimingLine>();
        HideAimingLine();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
         if (Input.GetKey(KeyCode.W)) 
		{
			Move();
		}
        else{
            if(animator.GetBool("run") == true){
                animator.SetBool("run", false);
            }
        }   
        if (Input.GetMouseButtonDown(1)){
            ShowAimingLine();
        }   
        else if (Input.GetMouseButtonUp(1)){
            HideAimingLine();
        }
    }

    private void ShowAimingLine(){
        
        aimingLine.gameObject.SetActive(true);
    }

     private void HideAimingLine(){
        
        aimingLine.gameObject.SetActive(false);
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
            Vector3 target = new Vector3();
             float distBetweenMouseAndPlayer = Vector3.Distance(transform.position, hit.point);
             target.x = hit.point.x;
             target.y = 0f;
             target.z = hit.point.z; 
             if(distBetweenMouseAndPlayer > 1.5f){
                animator.SetBool("run", true);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }
            else{
                animator.SetBool("run", false);
            }               
        }
    }
}

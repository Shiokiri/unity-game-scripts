using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float Horizontal { get => Input.GetAxis("Horizontal"); }
    public float Vertical { get => Input.GetAxis("Vertical"); }

    public float Rotate { get => Input.GetAxis("Mouse X"); }

    public bool IsDialogue = false;

    public CharacterController characterController;

    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Horizontal, 0, Vertical);
        dir = this.transform.TransformDirection(dir);
        characterController.SimpleMove(dir * 5);

        // 旋转,角色移动时候  
        Vector3 rot = new Vector3(0, Rotate, 0);
        this.transform.Rotate(rot / 2);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class NPCController : MonoBehaviour
{
    public int npcID;
    public DialogueManager dialogueManager;
    public Animator animator;


    /*
        public float angleSpeed = 0.01f;//ת���ٶ�
        public Transform target;
    */
    void Start()
    {

    }

    void Update()
    {
       
           
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);

        // ֻ�п�¡��Player���뷶Χ���Դ����Ի�
        if(other.name == "Player(Clone)")
        {
            // Debug.Log("npc�����Ի�");
            // ����Ի�
            dialogueManager.setPlayerCollider(other);
            dialogueManager.dialogbuttonInit(npcID);
            //isRotate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
			// ����̶������˳ƿ����������λ��
			other.GetComponent<ThirdPersonController>().LockCameraPosition = false;
			Cursor.lockState = CursorLockMode.Locked;
			// �˳��Ի�
			dialogueManager.DialogButtonOver();
            
        }
    }

    /* public void lookAtPlayer()
     {
        StartCoroutine("Rotate");
     }*/


    /*IEnumerator Rotate()
    {
        bool isRotate = true;
        Debug.Log("aa");
        while (isRotate)
        {
           Vector3 vec = new Vector3(target.position.x - transform.position.x, 0F, 0F);
      
        if (Vector3.Angle(vec, transform.forward) < 0.1f)
        {
            isRotate = false;
        } 
        if (isRotate)
        {
            this.transform.rotation= Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(vec), 0.1f * Time.deltaTime);
        }
        
        }
        yield return 0;

    }*/
  
}





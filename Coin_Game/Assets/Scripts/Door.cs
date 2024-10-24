using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]private Transform PreviousRoom;
    [SerializeField]private Transform NextRoom;
    [SerializeField]private CameraController cam;
 
   private void Awake()
   {
    cam = Camera.main.GetComponent<CameraController>();
   }
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x) 
            { 
                if (collision.gameObject.CompareTag("House2"))
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    cam.MoveToNewRoom(NextRoom);
                }
            }
          
        }
    } 

}

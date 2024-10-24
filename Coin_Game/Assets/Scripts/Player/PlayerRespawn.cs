using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   private Health playerHealth;
   private Health currentHealth;
   private Transform currentCheckpoint;
   private UIManager uiManager;
   private tIME currentTime;
   private tIME maxTime;
   private tIME runningTime;

   private void Awake()
   {
      playerHealth = GetComponent<Health>();
      currentHealth = GetComponent<Health>();
      uiManager = FindObjectOfType<UIManager>();
   }

   private void Update()
   {
      if (currentHealth.currentHealth == 0)
      {
         Debug.Log("die");
         uiManager.Gameover();
         transform.position = new Vector3(-10, 0, 0);
         playerHealth.Respawn(); 
         currentTime = maxTime;
         //runningTime = true;
      }
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.transform.tag == "Checkpoint")
      {
         currentCheckpoint = collision.transform;
         collision.GetComponent<Collider2D>().enabled = false;
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField]private float healthValue;
    [SerializeField] private AudioClip pickup;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.tag == "Player")
        {
            //SoundManager.instance.PlaySound(pickup);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

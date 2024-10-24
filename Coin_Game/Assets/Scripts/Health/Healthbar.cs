
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
   [SerializeField] public Health playerHealth;
   [SerializeField] public Image totalhealthbar;
   [SerializeField] public Image currenthealthbar;

   private void Start()
   {
    totalhealthbar.fillAmount = playerHealth.currentHealth / 10;
   }

   private void Update()
   {
    currenthealthbar.fillAmount = playerHealth.currentHealth / 10;
   }  
}

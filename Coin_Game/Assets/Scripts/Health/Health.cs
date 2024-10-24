using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3;
    public float currentHealth { get; private set; } = 3;
    private Animator anim;

    [Header("Death sound")]
    [SerializeField] private AudioClip deathSound;
    private UIManager uiManager;

    public void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();

    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
        }
        else
        {
            anim.SetTrigger("die");
            SoundManager.instance.PlaySound(deathSound);
            uiManager.Gameover();

        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
    }



}

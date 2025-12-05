using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    [Header("Character Stats")]
    [SerializeField] string characterName;
    [SerializeField] float cur_HP;
    [SerializeField] float max_HP;
    [SerializeField] protected float defByEquipment;
    protected int level = 1;
    private bool isDead = false;   
    public Image HealthBar;
    [SerializeField] GameObject healthBarPrefab;
    private RectTransform playerHUDRect;
    private GameObject playerHUDInstance;
    
    public Animator Animator;
    private Canvas canvas;

    protected virtual void Start()
    {
        characterName = gameObject.name;
        cur_HP = max_HP;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        playerHUDInstance = Instantiate(healthBarPrefab, canvas.transform);
        playerHUDRect = playerHUDInstance.GetComponent<RectTransform>();
        HealthBar = playerHUDInstance.transform.Find("HP/HP Bar").GetComponent<Image>();
        AssignBars(HealthBar);
    }

    public void AssignBars(Image healthBar)
    {
        HealthBar = healthBar;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (HealthBar != null)
        {
            Debug.Log("Updating health bar for " + characterName + ": " + cur_HP + "/" + max_HP);
            HealthBar.fillAmount = cur_HP / max_HP;
        }
    }
    public void TakeDamage(float Dmg)
    {
        Animator.SetTrigger("Hit");
        float finalDmg = Dmg - defByEquipment;
        cur_HP -= finalDmg;
        UpdateHealthBar();
        Debug.Log(characterName + " took " + finalDmg + " damage. Current HP: " + cur_HP + "/" + max_HP);
        IsAlive();
    }

    public void Heal(float healAmount)
    {
        cur_HP += healAmount;
        if (cur_HP > max_HP)
        {
            cur_HP = max_HP;
        }
        UpdateHealthBar();
        Debug.Log(characterName + " healed " + healAmount + " HP. Current HP: " + cur_HP + "/" + max_HP);
    }
    
    public void IsAlive()
    {

        if (cur_HP <= 0 && !isDead)
        {
            isDead = true;
            Animator.SetBool("IsDead", true);
            Debug.Log(characterName + " has been defeated.");
            StartCoroutine(DeathRoutine());
        }

    }

    IEnumerator DeathRoutine()
    {
        Units units = GetComponent<Units>();
        if (units != null)
        {
            TurnManager.Instance.RemoveUnit(units);
        }
        if (playerHUDInstance != null)
        {
            Destroy(playerHUDInstance);
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void LateUpdate()
    {
        if (playerHUDRect != null && canvas != null)
        {
            Vector3 worldPosition = transform.position + Vector3.up * 5f; // Ajusta la altura
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            playerHUDRect.position = screenPosition;
        }
    }
}

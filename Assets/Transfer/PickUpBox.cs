using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{

    [SerializeField] CapsuleCollider capsule;
    private Animator animator;
    [SerializeField] private LaserScript laserScript;

    float actualLife;
    [SerializeField] float maxLife;
    [SerializeField] float heal;
    [SerializeField] int enemyDamage;
    [SerializeField] int money;
    [SerializeField] int upgradePrice;
    [SerializeField] int healPrice;
    [SerializeField] int ammoPrice;
    [SerializeField] TextMeshProUGUI MoneyTxt;
    [SerializeField] float batteryCharge;

    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject Shop, deathScreen;

    bool gameIsPaused;
    int nbUpgrade = 1;
    [SerializeField] TextMeshProUGUI nbUpgradeTxt;
    [SerializeField] GameObject upgradeButton, interactButton;

    int RdEscape;
    private void Start()
    {
        money = 0;
        MoneyTxt.text = money.ToString();
        actualLife = maxLife;
        healthBar.SetMaxHealth(maxLife);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            RdEscape = Random.Range(1, 10);
            if (RdEscape != 1)
            {
                actualLife -= enemyDamage;
                healthBar.SetHealth(actualLife);
                if (actualLife <= 0)
                {
                    animator.SetBool("Dead", true);
                    Invoke("Death", 2f);
                }
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money"))
        {
            animator.SetTrigger("Pickup");

            if (other.gameObject.tag == "Ammo")
            {
                laserScript.Reload(batteryCharge);
            }
            else if (other.gameObject.tag == "Heal")
            {
                actualLife += heal;

                if (actualLife > maxLife)
                {
                    actualLife = maxLife;
                }

                healthBar.SetHealth(actualLife);
            }
            else
            {
                money += 10;
                MoneyTxt.text = money.ToString();
            }

            Destroy(other.gameObject);
            Invoke("DontShowInteract", 0.1f);
        }
        else if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Upgrade")
        {
            animator.SetTrigger("Use");
            Time.timeScale = 0f;
            Shop.SetActive(true);
        }

        if (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money" || other.gameObject.tag == "Upgrade")
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ammo" || other.gameObject.tag == "Heal" || other.gameObject.tag == "Money" || other.gameObject.tag == "Upgrade")
        {
            interactButton.SetActive(false);
        }
    }

    void DontShowInteract()
    {
        interactButton.SetActive(false);
    }

    //Shop
    public void Resume()
    {
        Shop.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void BuyUpgrade()
    {
        if (money >= upgradePrice)
        {
            nbUpgrade += 1;
            money -= upgradePrice;
            MoneyTxt.text = money.ToString();

            if (nbUpgrade > 3)
            {
                upgradeButton.SetActive(false);
            }

            laserScript.Upgrade();
            nbUpgradeTxt.text = nbUpgrade.ToString();
        }
    }
    public void BuyHeal()
    {
        if (money >= healPrice)
        {
            money -= healPrice;

            actualLife += heal;
            MoneyTxt.text = money.ToString();

            if (actualLife > maxLife)
            {
                actualLife = maxLife;
            }

            healthBar.SetHealth(actualLife);
        }
    }
    public void BuyAmmo()
    {
        if (money >= ammoPrice)
        {
            money -= ammoPrice;

            MoneyTxt.text = money.ToString();

            laserScript.Reload(batteryCharge);
        }
    }

    void Death()
    {
        deathScreen.SetActive(true);
    }
}

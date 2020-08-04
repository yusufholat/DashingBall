using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Video;


public class Player : MonoBehaviour
{
    int maxHealth;
    float currentHealth;
    float dashSpend;
    float energy, antienergy;
    float incrementHealth;

    public ParticleSystem crashEffect;
    public ParticleSystem dashEffect;
    public ParticleSystem deadEffect;

    string[] dashs = { "dash1", "dash2", "dash3", "dash4" };

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = SkinManager.instance.ShopSkinList[PlayerPrefs.GetInt("CurrentSkin", 0)].image;
        PlayerManager.score = 0;
        PlayerManager.maxHealth = 100;
        PlayerManager.currentHealth = 100;
        PlayerManager.goldenEnergyPower = false;
        maxHealth = 100;
        currentHealth = 100;
        dashSpend = 20;
        energy = ItemManager.instance.getCooldown("Energy");
        antienergy = ItemManager.instance.getCooldown("AntiEnergy");
        incrementHealth = 0.25f;
    }


    void Update()
    {
        if (!GameManager.gameStarded)
        {
            if (menuUIManager.shopIsOpen)
            {
                if (Input.GetMouseButtonDown(0) && !GameManager.IsPointerOverUIObject() ||
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
                {
                    Instantiate(dashEffect, transform.position, Quaternion.identity);
                    FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 4)]);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) ||
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Instantiate(dashEffect, transform.position, Quaternion.identity);
                    FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 4)]);
                }
            }
        }
        else
        {
            if (canMove() && Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.IsPointerOverUIObject() ||
           canMove() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
            {
                FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 3)]);
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                PlayerManager.score++;

                if (!PlayerManager.goldenEnergyPower)
                dashing();
            }
        }
    }

    void FixedUpdate() {
        healthIncrease();
    }

    private void healthIncrease()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += incrementHealth;
            PlayerManager.currentHealth = currentHealth;
        }
        else
        {
            currentHealth = maxHealth;
            PlayerManager.currentHealth = maxHealth;
        }
    }

    public void dashing()
    {
        if (currentHealth > dashSpend)
        {
            currentHealth -= dashSpend;
        }
    }

    public bool canMove()
    {
        if (PlayerManager.goldenEnergyPower)
            return true;

        if (currentHealth > dashSpend)
            return true;
        else return false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("Enemy") || 
            collision.gameObject.CompareTag("BigEnemy") || collision.gameObject.CompareTag("BlackHoleArea"))
        {
            Destroy(gameObject);
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            GameManager.gameOver = true;
        }
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            if(GameManager.gameStarded)
                PlayerManager.score++;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Instantiate(crashEffect, collision.contacts[0].point, Quaternion.identity);

            if(GameManager.gameStarded)
            FindObjectOfType<AudioManager>().Play("PlayerBounce");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Health"))
        {
            takeHealth();
        }
        else if (collision.gameObject.CompareTag("AntiHealth"))
        {
            takeAntiHealth();
        }
        else if (collision.gameObject.CompareTag("GoldenEnergy"))
        {
            takeGoldenEnergy();
        }
        if (collision.gameObject.CompareTag("BlackHole"))
        {
            takeBlackHole();
        }
        if (collision.gameObject.CompareTag("TimeFreeze"))
        {
            takeTimeFreeze();
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            takeShield();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySecondHitbox"))
        {
            PlayerManager.score += 5;
        }
    }




    private void takeHealth()
    {
        currentHealth += energy;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        PlayerManager.countEnergy++;
    }

    private void takeAntiHealth()
    {
        currentHealth -= antienergy;
        if (currentHealth < 0)
            currentHealth = 0;

        PlayerManager.countAntiEnergy++;
    }

    private void takeGoldenEnergy()
    {
        PlayerManager.instance.takeGoldenEnergy();
    }

    private void takeBlackHole()
    {
        PlayerManager.instance.takeBlackHole();
    }

    private void takeTimeFreeze()
    {
        PlayerManager.instance.takeTimeFreeze();
    }

    private void takeShield()
    {
        PlayerManager.instance.takeShield();
    }
}

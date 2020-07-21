using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Video;

[System.Serializable]
public class Player : MonoBehaviour
{
    int maxHealth;
    float currentHealth;
    float dashSpend;
    int energy;
    float incrementHealth;

    public ParticleSystem crashEffect;
    public ParticleSystem dashEffect;

    string[] dashs = { "dash1", "dash2", "dash3", "dash4" };

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = SkinManager.instance.ShopItemList[PlayerPrefs.GetInt("CurrentSkin")].image;
        PlayerManager.score = 0;
        PlayerManager.maxHealth = 100;
        PlayerManager.currentHealth = 100;
        maxHealth = 100;
        currentHealth = 100;
        dashSpend = 20;
        energy = 40;
        incrementHealth = 0.25f;
    }

    void LateUpdate()
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
                dashing();
            }
        }
    }

    void FixedUpdate() {
        healthIncrease();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BigEnemy"))
        {
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

    private void takeHealth()
    {
        currentHealth += energy;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void takeAntiHealth()
    {
        currentHealth -= energy;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public void dashing() {
        
        if (currentHealth > dashSpend) {
            FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 3)]);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
            currentHealth -= dashSpend;
            PlayerManager.score++;
        }
    }
    public bool canMove()
    {
        if (currentHealth > dashSpend)
            return true;
        else return false;
    }

}

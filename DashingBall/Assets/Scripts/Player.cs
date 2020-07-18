using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float maxHealth = 100;
    public float currentHealth = 0;
    public float incrementHealth = 0.15f;
    public float dashSpend = 0;
    public float energy = 0;

    public float score = 0;

    public ParticleSystem crashEffect;
    AudioSource crash;

    private void Awake()
    {
        crash = GetComponent<AudioSource>();
    }
    void LateUpdate()
    {
        if (canMove() && Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.IsPointerOverUIObject() ||
            canMove() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
        {
            dashing();
        }
        
    }

    void FixedUpdate() {
        healthIncrease();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BigEnemy"))
        {
            UIManager.gameIsOver = true;
        }
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            score++;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Instantiate(crashEffect, collision.contacts[0].point, Quaternion.identity);
            crash.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Health"))
        {
            takeHealth();
        }
    }

    private void healthIncrease()
    {
        if (currentHealth < maxHealth)
            currentHealth += incrementHealth;
        else currentHealth = maxHealth;
    }

    private void takeHealth()
    {
        currentHealth += energy;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void dashing() {
        if (currentHealth > dashSpend) {
            currentHealth -= dashSpend;
            score++;
        }
            
    }
    public bool canMove()
    {
        if (currentHealth > dashSpend)
            return true;
        else return false;
    }

}

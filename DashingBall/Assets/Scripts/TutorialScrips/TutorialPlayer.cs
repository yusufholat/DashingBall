using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
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
        GetComponent<SpriteRenderer>().sprite = SkinManager.instance.ShopSkinList[8].image;
        PlayerManager.score = 0;
        PlayerManager.maxHealth = 100;
        PlayerManager.currentHealth = 100;
        maxHealth = 100;
        currentHealth = 100;
        dashSpend = 20;
        energy = 50;
        antienergy = 20;
        incrementHealth = 0.35f;
    }


    void Update()
    {
        if (canMove() && Input.GetKeyDown(KeyCode.Mouse0) && !TutorialPlayerReflection.IsPointerOverUIObject() && TutorialManager.tutorialStarted  ||
        canMove() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !TutorialPlayerReflection.IsPointerOverUIObject() && TutorialManager.tutorialStarted)
        {
            FindObjectOfType<AudioManager>().Play(dashs[Random.Range(0, 3)]);
            Instantiate(dashEffect, transform.position, Quaternion.identity);

            if (TutorialManager.scoreSystemOn)
            {
                PlayerManager.score++;

                if (PlayerManager.score == 15 && TutorialManager.playerControlScore)
                {
                    TutorialManager.instance.ShowEnergySystemPopup();
                    TutorialManager.playerControlScore = false;
                }
            }


            if (TutorialManager.playerControlBall)
            {
                TutorialManager.playerTouchCount++;
                if(TutorialManager.playerTouchCount == 10)
                {
                    TutorialManager.playerTouchCount = 0;
                    TutorialManager.playerControlBall = false;
                    TutorialManager.instance.ShowSkorSystemPopup();
                }
            }
            if (TutorialManager.energySystemOn)
                dashing();
        }

    }

    void FixedUpdate()
    {
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
        }
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            if (TutorialManager.scoreSystemOn)
            {
                PlayerManager.score++;
                if (PlayerManager.score == 15 && TutorialManager.playerControlScore)
                {
                    TutorialManager.instance.ShowEnergySystemPopup();
                    TutorialManager.playerControlScore = false;
                }
            }
                
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            Instantiate(crashEffect, collision.contacts[0].point, Quaternion.identity);
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
        //PlayerManager.instance.takeGoldenEnergy();
    }

    private void takeBlackHole()
    {
        //PlayerManager.instance.takeBlackHole();
    }

    private void takeTimeFreeze()
    {
        //PlayerManager.instance.takeTimeFreeze();
    }

    private void takeShield()
    {
        //PlayerManager.instance.takeShield();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Player player;
    Text scoreText;
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.canMove() && !EventSystem.current.IsPointerOverGameObject() ||
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && player.canMove() && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            scoreText.text = player.score.ToString();
        }
    }
}

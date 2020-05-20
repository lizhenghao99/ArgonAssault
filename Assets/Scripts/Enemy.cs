using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX = null;
    [SerializeField] Transform parent = null;
    [SerializeField] int scorePerHit = 1200;
    [SerializeField] int hitPoint = 5;

    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;

        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hitPoint <= 0)
        {
            die();
        }
        else
        {
            hitPoint -= 1;
            scoreBoard.ScoreHit(50);
        }  
    }

    private void die()
    {
        scoreBoard.ScoreHit(scorePerHit);
        GameObject fx = Instantiate(
            deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

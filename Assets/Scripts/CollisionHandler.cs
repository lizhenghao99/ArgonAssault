using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadSceneDelay = 2f;

    [SerializeField] GameObject deathFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("reloadLevel", LoadSceneDelay);
    }

    private void reloadLevel()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

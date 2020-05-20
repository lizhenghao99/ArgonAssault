using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Effects;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 2f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3f;

    [Header("Screen position based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Player control based")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRowFactor = -15f;

    [Header("Player attack")]
    [SerializeField] GameObject bulletsLeft = null;
    [SerializeField] GameObject bulletsRight = null;

    float xThrow = 0f;
    float yThrow = 0f;

    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            Translate();
            Rotate();
            Fire();
        }
    }

    private void Fire()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            var bl = bulletsLeft.GetComponent<ParticleSystem>().emission;
            bl.enabled = true;
            var br = bulletsRight.GetComponent<ParticleSystem>().emission;
            br.enabled = true;
        }
        else
        {
            var bl = bulletsLeft.GetComponent<ParticleSystem>().emission;
            bl.enabled = false;
            var br = bulletsRight.GetComponent<ParticleSystem>().emission;
            br.enabled = false;
        }
    }

    void OnPlayerDeath() // called by string reference
    {
        print("control is frozen");
        isControlEnabled = false;
    }

    private void Rotate()
    {
        float pitch = transform.localPosition.y * positionPitchFactor
            + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRowFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void Translate()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float NewXPos = Mathf.Clamp(
            transform.localPosition.x + xOffset, -xRange, xRange);
        float NewYPos = Mathf.Clamp(
            transform.localPosition.y + yOffset, -yRange, yRange);

        transform.localPosition = new Vector3(
            NewXPos, NewYPos, transform.localPosition.z);
    }
}

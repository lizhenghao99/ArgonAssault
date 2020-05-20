using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobile : MonoBehaviour
{
    private float startPositionY = 0f;
    private float gyroscopeYPos = 0f;
    float calibrateToYPos = 0f;
    public bool gameStart;

    // Assuming your main camera is tagged as "MainCamera"
    private Camera mainCamera;

    void Start()
    {
        Input.gyro.enabled = true;
        mainCamera = Camera.main;
        startPositionY = mainCamera.transform.eulerAngles.y;
    }

    void Update()
    {
        ApplyGyroRotation();
        ApplyCalibration();

        if (gameStart)
        {
            Invoke("CalibrateToYPos", 3f);
            gameStart = false;
        }
    }

    void ApplyGyroRotation()
    {
        // Invert the X and Z rotations for a natural feel
        mainCamera.transform.rotation = Quaternion.Inverse(
            new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, Input.gyro.attitude.w));

        gyroscopeYPos = mainCamera.transform.eulerAngles.y;
    }

    void CalibrateToYPos()
    {
        calibrateToYPos = gyroscopeYPos - startPositionY;
    }

    void ApplyCalibration()
    {
        // Apply calibration on the Y-axis for smooth starting position
        mainCamera.transform.Rotate(0f, -calibrateToYPos, 0f, Space.World);
    }
}

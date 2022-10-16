using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    [SerializeField] float controlspeed=10f; 
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3f;
    [SerializeField] float positionPitchfactor = -2f;
    [SerializeField] float controlpitchfactor = -15f;
    [SerializeField] float positionYawfactor = 5f; 
    [SerializeField] float controlRollfactor = 5f;
    [SerializeField] GameObject[] lasers; 
    float yThrow;
    float xThrow;

    void Start()
    {
        
    }
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        Processfiring();

    }
    void ProcessRotation()
    {
        float pitch = transform.localPosition.y* positionPitchfactor + yThrow*controlpitchfactor;
        float yaw = transform.localPosition.x*positionYawfactor;
        float roll = xThrow*controlRollfactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessTranslation()
    {
        xThrow=Input.GetAxis("Horizontal");
        yThrow=Input.GetAxis("Vertical");
        

        float xoffset= xThrow*Time.deltaTime*controlspeed;
        float rawXpos = transform.localPosition.x + xoffset;
        float clampedXpos= Mathf.Clamp(rawXpos, -xRange, xRange);

        float yoffset= yThrow*Time.deltaTime*controlspeed;
        float rawYpos = transform.localPosition.y + yoffset;
        float clampedYpos= Mathf.Clamp(rawYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXpos,clampedYpos, transform.localPosition.z );
    }
    void Processfiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }
    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
    
}

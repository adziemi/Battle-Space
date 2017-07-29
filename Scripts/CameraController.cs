using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour {
    public Transform CameraDefaultPosition;
    public Transform SpaceShip;
    public bool CameraFreeMovement = false;
    public float CameraTurnSpeed = 6.0f;
    public PostProcessingProfile EffectsProfile;

    private float cameraDistance = 7f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private Vector3 velocity = Vector3.one;
    private float distanceDamp = 0.05f;
    private Vector3 defaultDistance = new Vector3(0, 1.5f, 4f);
    
    void LateUpdate()
    {
        if(!CameraFreeMovement)smoothFollow();
    }

    void smoothFollow(){
        Vector3 toPos = CameraDefaultPosition.position + (CameraDefaultPosition.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(gameObject.transform.position, toPos, ref velocity, distanceDamp);
        gameObject.transform.position = curPos;
        gameObject.transform.LookAt(CameraDefaultPosition, CameraDefaultPosition.up);
    }

    public void CameraMovement()
    {
        CameraFreeMovement = true;

        currentX = -Input.GetAxis("Mouse Y") + currentX;
        currentY = -Input.GetAxis("Mouse X") + currentY;
        cameraDistance = Input.GetAxis("Mouse ScrollWheel") + cameraDistance;

        Vector3 direction = new Vector3(0, 0, -cameraDistance);
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);

        gameObject.transform.position = SpaceShip.position + rotation * direction;
        gameObject.transform.LookAt(SpaceShip.position);
    }
    public void chromaticAberattion(bool turn)
    {
        switch (turn)
        {
            case true:
                {
                    float value = EffectsProfile.chromaticAberration.settings.intensity;
                    if (value < 1)
                    {
                        value += 0.4f * Time.deltaTime;
                        EffectsProfile.chromaticAberration.enabled = true;
                        var chromaticAberration = EffectsProfile.chromaticAberration.settings;
                        chromaticAberration.intensity = value;
                        EffectsProfile.chromaticAberration.settings = chromaticAberration;
                    }
                }
                break;

            case false:
                {
                    float value = EffectsProfile.chromaticAberration.settings.intensity;
                    if (value > 0)
                    {
                        value -= 1f * Time.deltaTime;
                        EffectsProfile.chromaticAberration.enabled = true;
                        var chromaticAberration = EffectsProfile.chromaticAberration.settings;
                        chromaticAberration.intensity = value;
                        EffectsProfile.chromaticAberration.settings = chromaticAberration;
                    }
                    else EffectsProfile.chromaticAberration.enabled = false;
                }
                break;
        }
    }
}

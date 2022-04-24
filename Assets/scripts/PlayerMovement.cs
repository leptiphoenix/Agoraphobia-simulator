using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    private XROrigin rig;
    [SerializeField]
    private float speed = 1f;
    void Start()
    {
        character.GetComponent<CharacterController>();
        rig.GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 directionHead = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        Vector3 directionMouvement = new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(directionMouvement * speed * Time.deltaTime);
    }
}

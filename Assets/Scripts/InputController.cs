using System.Collections;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void InputEvent();

    [Header("Movement Data")]
    [SerializeField]
    private VehicleData data;

    public InputEvent action_Primary = () => { };
    public InputEvent action_Secondary = () => { };

    private CharacterController controller;
    private Transform tf;

    private void Awake()
    {
        this.controller = this.GetComponent<CharacterController>();
        this.tf = this.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        this.StartCoroutine("ReadInput");
    }

    private void OnDisable()
    {
        this.StopCoroutine("ReadInput");
    }

    private IEnumerator ReadInput()
    {
        while (true)
        {
            this.CalculateMovement();

            if (Input.GetButtonDown("Primary Action"))
            {
                this.action_Primary();
            }
            else if (Input.GetButtonDown("Secondary Action"))
            {
                this.action_Secondary();
            }

            yield return null;
        }
    }

    private void CalculateMovement()
    {
        float horzAxis = Input.GetAxis("Horizontal");
        float vertAxis = Input.GetAxis("Vertical");

        if (vertAxis > 0)
        {
            vertAxis *= this.data.ForwardSpeed;
        }
        else
        {
            vertAxis *= this.data.ReverseSpeed;
        }

        this.tf.Rotate(0, horzAxis * this.data.RotateSpeed * Time.deltaTime, 0);
        this.controller.SimpleMove(vertAxis * this.tf.forward);
    }
}

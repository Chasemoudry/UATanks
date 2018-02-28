using System.Collections;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private VehicleData data;

    private delegate void FireButton();

    private CharacterController controller;
    private Transform tf;
    private Ray heightRay;

    private void Awake()
    {
        this.controller = this.GetComponent<CharacterController>();
        this.tf = this.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        this.StartCoroutine("ReadMovement");
    }

    private void OnDisable()
    {
        this.StopCoroutine("ReadMovement");
    }

    private IEnumerator ReadMovement()
    {
        while (true)
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

            this.heightRay = new Ray(this.tf.position, Vector3.down);

            if (Physics.Raycast(this.heightRay, this.controller.radius + 0.1f))
            {
                this.tf.Rotate(0, horzAxis * this.data.ReverseSpeed * Time.deltaTime, 0);
            }

            this.controller.SimpleMove(vertAxis * this.tf.forward);

            yield return null;
        }
    }
}

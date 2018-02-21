using System.Collections;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private VehicleData data;

    private delegate void FireButton();

    private CharacterController controller;

    private void Awake()
    {
        this.controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        this.StartCoroutine(MovementInput());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator MovementInput()
    {
        while (true)
        {
            float horzMove = Input.GetAxis("Horizontal");
            float vertMove = Input.GetAxis("Vertical");

            if (vertMove > 0)
            {
                horzMove *= this.data.ForwardSpeed * Time.deltaTime;
                vertMove *= this.data.ForwardSpeed * Time.deltaTime;
            }
            else
            {
                horzMove *= this.data.ReverseSpeed * Time.deltaTime;
                vertMove *= this.data.ReverseSpeed * Time.deltaTime;
            }

            this.controller.SimpleMove(new Vector3(horzMove, 0, vertMove));

            yield return null;
        }
    }
}

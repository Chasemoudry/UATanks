using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DurabilitySlider : MonoBehaviour
{
	private Slider m_Slider;
	private IVehicle m_Vehicle;

	private void Awake()
	{
		this.m_Slider = this.GetComponent<Slider>();
	}

	private void OnEnable()
	{
		if (GameManager.PlayerOne == null)
		{
			return;
		}

		this.m_Vehicle = GameManager.PlayerOne.GetComponent<IVehicle>();
		this.m_Vehicle.DurabilityChanged += this.UpdateSlider;

		this.m_Slider.maxValue = this.m_Vehicle.Data.MaxHealth;
		this.m_Slider.value = this.m_Vehicle.CurrentDurability;
	}

	private void UpdateSlider()
	{
		this.m_Slider.value = this.m_Vehicle.CurrentDurability;
	}
}

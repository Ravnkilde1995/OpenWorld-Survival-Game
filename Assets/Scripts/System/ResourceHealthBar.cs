using UnityEngine;
using UnityEngine.UI;

public class ResourceHealthBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = GlobalState.Instance.resourceHealth / GlobalState.Instance.resourceMaxHealth;
    }
}

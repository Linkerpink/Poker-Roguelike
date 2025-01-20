using UnityEngine;

public class UpdateShaderTimeAndSpin : MonoBehaviour
{
    public Material material;  // Assign your material in the inspector

    void Update()
    {
        // Update the _CustomTime property based on Unity's time
        if (material != null)
        {
            material.SetFloat("_CustomTime", Time.time);  // Set _CustomTime to the elapsed time

            // You can also modify _SpinTime based on time or any other factor
            float spinSpeed = Mathf.Sin(Time.time * 0.5f);  // Example of spin time oscillating over time
            material.SetFloat("_SpinTime", spinSpeed);  // Set _SpinTime to a calculated value
        }
    }
}

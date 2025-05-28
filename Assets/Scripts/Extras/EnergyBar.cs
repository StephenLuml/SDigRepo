using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public Transform fillBar;
    private float originalYScale;
    private float originalYLoc;

    private void Awake()
    {
        originalYLoc = fillBar.transform.localPosition.y;
        originalYScale = fillBar.transform.localScale.y;
    }

    private void OnEnable()
    {
        PlayerController.onChangeEnergy += SetBarPercentage;
    }
    private void OnDisable()
    {
        PlayerController.onChangeEnergy -= SetBarPercentage;
    }

    public void SetBarPercentage(int current, int max)
    {
        SetBarPercentage((float)current / max);
    }

    public void SetBarPercentage(float percent)
    {
        Vector3 scale = fillBar.transform.localScale;
        scale.y = percent * originalYScale;

        Vector3 pos = fillBar.transform.localPosition;
        pos.y = (scale.y / 2) - (originalYScale / 2);

        fillBar.transform.localScale = scale;
        fillBar.transform.localPosition = pos;
    }
}

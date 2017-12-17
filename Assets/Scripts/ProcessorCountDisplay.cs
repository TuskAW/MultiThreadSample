using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMesh))]
public class ProcessorCountDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh;

    private void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMesh>();
        }

        UpdateTextDisplay(SystemInfo.processorCount);
    }

    private void UpdateTextDisplay(int processorCount)
    {
        string displayString = "Cores: " + processorCount;

        if (textMesh != null)
        {
            textMesh.text = displayString;
        }
    }
}
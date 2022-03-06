using Cysharp.Threading.Tasks;
using UnityEngine;
using Util.UpdateManager;

public class TextVertical : MonoBehaviour
{
    public float distanceMove = 100;
    public float animationSpeed = 1;
    [SerializeField]
    CustomTextMeshProUGUI customTextMeshProUGUI;
   void Awake()
    {
        if (customTextMeshProUGUI == null)
            customTextMeshProUGUI = GetComponent<CustomTextMeshProUGUI>();

        customTextMeshProUGUI.text = "OTEDAMA";
    }

    void Update()
    {
        customTextMeshProUGUI.UGUIUpdate(distanceMove, animationSpeed, 360.0f);
    }
}
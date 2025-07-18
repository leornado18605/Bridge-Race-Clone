using UnityEngine;

public class LayerCollisManager : MonoBehaviour
{
    [Header("Name's layer")]
    [SerializeField] private string playerLayerName = "Player";
    [SerializeField] private string aiLayerName = "AI";

    private void Awake()
    {
        int playerLayer = LayerMask.NameToLayer(playerLayerName);
        int aiLayer = LayerMask.NameToLayer(aiLayerName);
        Physics.IgnoreLayerCollision(playerLayer, aiLayer, true);
    }
}
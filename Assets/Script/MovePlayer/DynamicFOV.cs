using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class FOVController : MonoBehaviour
{
    [Header("Virtual Camera cần điều chỉnh")]
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    [Header("Cài đặt FOV")]
    [SerializeField] private float baseFOV = 70f;
    [SerializeField] private float maxFOV = 95f;
    [SerializeField] private float fovIncreasePerBrick = 1.5f;
    [SerializeField] private float tweenDuration = 0.01f;

    private int brickCount = 0;

    private void Start()
    {
        if (virtualCam == null)
        {
            Debug.LogError("❌ Chưa gán Virtual Camera vào FOVController!");
            return;
        }

        virtualCam.m_Lens.FieldOfView = baseFOV;
    }

    public void UpdateFOV(int currentBrickCount)
    {
        brickCount = currentBrickCount;
        float targetFOV = Mathf.Clamp(baseFOV + brickCount * fovIncreasePerBrick, baseFOV, maxFOV);

        DOTween.To(() => virtualCam.m_Lens.FieldOfView, x => virtualCam.m_Lens.FieldOfView = x, targetFOV, tweenDuration);
        Debug.Log($"🎥 FOV cập nhật: {virtualCam.m_Lens.FieldOfView}");
    }
}
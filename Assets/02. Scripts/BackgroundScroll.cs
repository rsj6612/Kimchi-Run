using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    // [Tooltip("How set speed ? ")] 툴팁 작성하는 방법
    public float scrollSpeed = 0;

    [Header("References")]
    public MeshRenderer meshRenderer;

    
    void Start()
    {
        
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.Instance.CalculateGameSpeed() / 20 * Time.deltaTime, 0); 
        // y 축은 잠굼, x축은 스피드 수만큼 계속 더해짐 근데 값이 너무 커짐 - 프레임 마다 실행해서
        // -> Time.deltaTime - 이전 프레임으로부터 현재 프레임까지 몇 초 걸리는지 알려줌

    }
}

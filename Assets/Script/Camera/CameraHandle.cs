
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraHandle : MonoBehaviour
{
    public static CameraHandle instance;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    //[SerializeField] private Camera camera;
    //[SerializeField] private Transform player;
    //[SerializeField] private int layerMask = 2;
    //[SerializeField] private float fadeDuration = 0.5f;
    //private Renderer lastHitRenderer = null;
    //RaycastHit hit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void ChangeClipPlaneInBuilding()
    {

    }
    //private void Update()
    //{
    //    CheckRaycast();
    //}

    //public void CheckRaycast()
    //{
    //    Vector3 cameraPosition = camera.transform.position;
    //    Vector3 playerPosition = player.transform.position;

    //    //playerPosition.y += 1f;
    //    Vector3 direction = (cameraPosition - playerPosition).normalized;
    //    Ray rayCast = new Ray(cameraPosition, -direction);
    //    float distance = Vector3.Distance(cameraPosition, playerPosition);
    //    Debug.DrawRay(cameraPosition, -direction * distance, Color.green);
    //    if (Physics.Raycast(rayCast, out hit, distance))
    //    {
    //        //Kiểm tra xem raycast có va chạm với nhân vật không
    //        if (hit.transform == player)
    //        {
    //            if (lastHitRenderer != null)
    //            {
    //                StopCoroutine(FadeObject(lastHitRenderer, 1f, fadeDuration));
    //                StartCoroutine(FadeObject(lastHitRenderer, 1f, fadeDuration));
    //                SetMaterialOpaque(lastHitRenderer); //Set về opaque 
    //                lastHitRenderer = null;
    //            }
    //        }
    //        else
    //        {
    //            Renderer hitRenderer = hit.transform.GetComponent<Renderer>();
    //            if (hitRenderer != null)
    //            {

    //                if (lastHitRenderer != hitRenderer)
    //                {
    //                    if (lastHitRenderer != null)
    //                    {
    //                        // Dừng làm mờ đối tượng trước đó nếu nó không còn bị raycast trúng
    //                        StopCoroutine(FadeObject(lastHitRenderer, 1f, fadeDuration));
    //                        StartCoroutine(FadeObject(lastHitRenderer, 1f, fadeDuration));
    //                        //Set về opaque 
    //                        SetMaterialOpaque(lastHitRenderer);
    //                    }
    //                    lastHitRenderer = hitRenderer;
    //                }
    //                // đổi sang mode transparent để điều chỉnh được màu  và bắt đầu làm mờ đối tượng hiện tại
    //                SetMaterialTransparent(hitRenderer);
    //                StartCoroutine(FadeObject(hitRenderer, 0.1f, fadeDuration));
    //            }
    //        }
    //    }
    //}

    //private IEnumerator FadeObject(Renderer renderer, float targetAlpha, float duration)
    //{
    //    foreach (Material mat in renderer.materials)
    //    {
    //        Color initialColor = mat.color;
    //        float startAlpha = initialColor.a;
    //        float rate = 1.0f / duration;
    //        float progress = 0.0f;
    //        while (progress < 1.0f)
    //        {
    //            Color tempColor = initialColor;
    //            tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, progress);
    //            mat.color = tempColor;
    //            progress += rate * Time.deltaTime;
    //            yield return null;
    //        }
    //        Color finalColor = initialColor;
    //        finalColor.a = targetAlpha;
    //        mat.color = finalColor;
    //    }
    //}
    //private void SetMaterialTransparent(Renderer renderer)
    //{
    //    foreach (Material mat in renderer.materials)
    //    {
    //        mat.SetFloat("_Mode", 2);
    //        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //        mat.SetInt("_ZWrite", 0);
    //        mat.DisableKeyword("_ALPHATEST_ON");
    //        mat.EnableKeyword("_ALPHABLEND_ON");
    //        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //        mat.renderQueue = 3000;
    //    }
    //}

    //private void SetMaterialOpaque(Renderer renderer)
    //{
    //    foreach (Material mat in renderer.materials)
    //    {
    //        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
    //        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
    //        mat.SetInt("_ZWrite", 1);
    //        mat.DisableKeyword("_ALPHATEST_ON");
    //        mat.DisableKeyword("_ALPHABLEND_ON");
    //        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //        mat.renderQueue = -1;
    //    }
    //}
}




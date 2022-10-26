using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour {

    private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private GameObject topmostCanvas;

    private void Awake() {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		// 找到场景中的摄像机并绑定
		var virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
		for (int i = 0; i < virtualCameras.Length; i++)
		{
			// Debug.Log(virtualCameras[i].name);
			if (virtualCameras[i].name == "PlayerAimCamera")
			{
				aimVirtualCamera = virtualCameras[i];
			}
		}

		//UI
		topmostCanvas = GameObject.Find("Canvas");
	}

	private void Update() {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            //debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 13f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            // UI
            Transform Crosshire = topmostCanvas.transform.Find("Crosshire");
            Crosshire.GetComponent<CanvasGroup>().alpha = 1;

            if (starterAssetsInputs.shoot && GameManager.Instance.playerStats.CurrentBulletsNumber > 0)
            {
                /*
                // Hit Scan Shoot
                if (hitTransform != null) {
                    // Hit something
                    if (hitTransform.GetComponent<BulletTarget>() != null) {
                        // Hit target
                        Instantiate(vfxHitGreen, mouseWorldPosition, Quaternion.identity);
                    } else {
                        // Hit something else
                        Instantiate(vfxHitRed, mouseWorldPosition, Quaternion.identity);
                    }
                }
                */
                // Projectile Shoot
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

                starterAssetsInputs.shoot = false;
            }
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 13f));

            //UI
            Transform Crosshire = topmostCanvas.transform.Find("Crosshire");
            Crosshire.GetComponent<CanvasGroup>().alpha = 0;

            starterAssetsInputs.shoot = false;
        }

    }

}
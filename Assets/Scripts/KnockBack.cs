using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private BirdJump birdJump;
    private Camera mainCamera;
    private Vector3 cameraInitialPosition;

    private bool isKnockedBack = false;
    private float knockBackDistance = 2f;   // 뒤로 밀려나는 거리
    private float knockBackDuration = 0.5f;   // 밀려나는 시간

    void Start()
    {
        birdJump = GetComponent<BirdJump>();
        mainCamera = Camera.main;
        cameraInitialPosition = mainCamera.transform.position;
    }

    public void TriggerKnockBack()
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockBackCoroutine());
        }
    }

    private IEnumerator KnockBackCoroutine()
    {
        isKnockedBack = true;
        BackGroundLoop.instance.PauseMovement();

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = transform.position - new Vector3(0, BackGroundLoop.instance.backgroundWidth / 3, 0);

        // 새와 카메라를 위쪽으로 이동
        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / knockBackDuration);
            mainCamera.transform.position = cameraInitialPosition + (transform.position - originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        elapsedTime = 0f;
        // 새와 카메라를 원래 위치로 이동
        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / knockBackDuration);
            mainCamera.transform.position = cameraInitialPosition + (transform.position - originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
        BackGroundLoop.instance.ResumeMovement();
    }
}

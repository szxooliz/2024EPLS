using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private BirdJump birdJump;
    private Camera mainCamera;
    private Vector3 cameraInitialPosition;

    private bool isKnockedBack = false;
    private float knockBackDuration = 2f;   // 밀려나는 시간

    public static KnockBack instance;

    void Start()
    {
        birdJump = GetComponent<BirdJump>();
        mainCamera = Camera.main;
        cameraInitialPosition = mainCamera.transform.position;
        
        instance = this;
    }

    public void TriggerKnockBack()
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockBackCoroutine());
        }
    }

    public IEnumerator KnockBackCoroutine()
    {
        isKnockedBack = true;
        BackGroundLoop.instance.PauseMovement();
        Move.instance.PauseMovement();

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = transform.position - new Vector3(BackGroundLoop.instance.backgroundWidth / 3, 0, 0);

        // 새와 카메라를 왼쪽으로 이동
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
        Move.instance.ResumeMovement();
        yield return null;
    }
}

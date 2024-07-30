using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 cameraInitialPosition;

    private bool isKnockedBack = false;
    private float knockBackDuration = 1.5f;   // 밀려나는 시간

    public static KnockBack instance;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Awake()
    {
        instance = this;
    }

    public void TriggerKnockBack()
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockBackCoroutine());
        }
    }

    public IEnumerator KnockBackCoroutine(GameObject clover = null)
    {
        if (clover != null) { clover.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0); }
        isKnockedBack = true;
        BackGroundLoop.instance.PauseMovement();
        Move.instance.PauseMovement();

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position; // 원래 캐릭터의 위치
        Debug.Log(originalPosition);
        Vector3 targetPosition = transform.position - new Vector3(BackGroundLoop.instance.backgroundWidth / 3, 0, 0);  // 왼쪽으로 밀려난 후 캐릭터의 위치
        Debug.Log(targetPosition);
        cameraInitialPosition = mainCamera.transform.position;

        // 캐릭터와 카메라를 왼쪽으로 이동
        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / knockBackDuration);
            mainCamera.transform.position = cameraInitialPosition + (transform.position - originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        // 새와 카메라를 처음 속도대로 왼쪽으로 이동

        PlayerMove.instance.ResumeMovement();
        CameraMove.instance.ResumeMovement();

        // 캐릭터가 원래 위치로 돌아올 때까지 이동
        while (transform.position.x < originalPosition.x)
        {
            yield return null; // 다음 프레임까지 대기
        }

        PlayerMove.instance.PauseMovement();
        CameraMove.instance.PauseMovement();

        isKnockedBack = false;
        BackGroundLoop.instance.ResumeMovement();
        Move.instance.ResumeMovement();

        // if (clover != null) { Destroy(clover);}
        yield return null;
    }
}

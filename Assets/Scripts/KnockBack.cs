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
    }

    private void Awake()
    {
        instance = this;
    }

    public void TriggerKnockBack()
    {
        if (!isKnockedBack)
        {
            Debug.Log("시작");
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

        // 캐릭터와 카메라를 왼쪽으로 이동
        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / knockBackDuration);
            mainCamera.transform.position = cameraInitialPosition + (transform.position - originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // 새와 카메라를 처음 속도대로 왼쪽으로 이동
        while (originalPosition.x == targetPosition.x)
        {
            PlayerMove.instance.ResumeMovement();
        }
        PlayerMove.instance.PauseMovement();

        isKnockedBack = false;
        BackGroundLoop.instance.ResumeMovement();
        Move.instance.ResumeMovement();
        yield return null;
    }
}

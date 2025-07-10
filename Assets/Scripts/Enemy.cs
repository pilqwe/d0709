using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 데미지 이펙트 프리팹
    public GameObject damageEffectPrefab;

    // 스프라이트 렌더러 컴포넌트 참조
    public SpriteRenderer spriteRenderer;

    // 플래시 효과 색상
    public Color flashColor = Color.red; // 피격 시 깜빡일 색상

    // 플래시 지속 시간
    public float flashDuration = 0.1f; // 깜빡임 지속 시간

    // 원래 색상 저장
    private Color originalColor; // 원래 색상 저장

    // 체력
    [SerializeField]
    public float enemyHp = 1;

    // 적의 이동 속도
    public float moveSpeed = 1f; // 이동 속도

    // 코인 프리팹
    public GameObject coin;

    // 이펙트 프리팹
    public GameObject effect;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 컴포넌트 참조
        originalColor = spriteRenderer.color; // 원래 색상 저장
    }

    // 적의 번쩍 시 깜빡임 효과
    public void Flash()
    {
        StopAllCoroutines(); // 기존 코루틴 중지
        StartCoroutine(FlashRoutine()); // 새로운 플래시 시작
    }

    // 플래시 시 색상 변경 코루틴
    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor; // 플래시 색상으로 변경
        yield return new WaitForSeconds(flashDuration); // 지정된 시간 대기
        spriteRenderer.color = originalColor; // 원래 색상으로 복원
    }

    // 이동 속도 설정
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -7f)
        {
            Destroy(gameObject); // 적이 화면 아래로 나가면 파괴
        }
    }

    // 이동 속도 설정 메서드
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // 미사일과 충돌 시 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Missile")
        {
            Missile missile = collision.GetComponent<Missile>();
            StopAllCoroutines(); // 기존 코루틴 중지
            StartCoroutine("HitColor"); // 피격 색상 코루틴 시작
            Flash(); // 깜빡임 효과

            enemyHp -= enemyHp = missile.missileDamage; // 체력 감소
            if (enemyHp <= 0)
            {
                Destroy(gameObject); // 적 파괴
                Instantiate(coin, transform.position, Quaternion.identity); // 코인 생성
                Instantiate(effect, transform.position, Quaternion.identity); // 이펙트 생성

                TakeDamage(missile.missileDamage); // 데미지 받기 호출
            }
        }
    }

    // 데미지 받을 때 처리 로직
    IEnumerator HitColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    // 데미지 받을 때 처리
    void TakeDamage(int damage)
    {
        DamagePopupManager.Instance.CreateDamageText(damage, transform.position);
    }
}

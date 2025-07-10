using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f; // 플레이어 이동 속도
    int missIndex = 0; // 플레이어가 미스한 횟수
    public GameObject[] missilePrefabs; // 미사일 프리팹 배열
    public Transform spPosition; // 미사일 발사 위치
    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastshotTime = 0f; // 마지막 발사 시간
    private Animator animator; // 애니메이터 컴포넌트
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 초기화
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 수평 입력 값
        Vector3 moveTo = new Vector3(horizontalInput, 0, 0);
        transform.position += moveTo * moveSpeed * Time.deltaTime; // 플레이어 이동

        if (horizontalInput < 0)
        {
            animator.Play("Left");
        }
        else if (horizontalInput > 0)
        {
            animator.Play("Right");
        }
        else
        {
            animator.Play("Idle");
        }
        Shoot();
    }

    void Shoot()
    {
        if (Time.time - lastshotTime > shootInterval)
        {
            Instantiate(missilePrefabs[missIndex], spPosition.position, Quaternion.identity);
            lastshotTime = Time.time; // 마지막 발사 시간 업데이트
        }
    }

    public void Missileup()
    {
        missIndex++;
        shootInterval = shootInterval - 0.1f;
        if (shootInterval <= 0.1f)
        {
            shootInterval = 0.1f; // 최소 발사 간격 설정
        }
        if (missIndex >= missilePrefabs.Length)
        {
            missIndex = missilePrefabs.Length - 1; // 미사일 인덱스가 배열 범위를 벗어나지 않도록 제한
        }
    }
}

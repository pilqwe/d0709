using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    public int missileDamage = 1;

    [SerializeField]
    GameObject Expeffect;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (transform.position.y > 7f)
        {
            Destroy(gameObject); // 미사일이 화면 밖으로 나가면 파괴
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("적과 충돌");
            GameObject effect = Instantiate(Expeffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // 이펙트가 1초 후에 파괴
            Destroy(gameObject); // 미사일 파괴
        }
    }
}

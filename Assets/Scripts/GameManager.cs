using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int coin = 0;

    public TextMeshProUGUI textMeshProCoin; // 코인 텍스트 UI

    public GameObject gameOverPanel; // ✅ 게임 오버 UI 패널
    public Button restartButton;     // ✅ 재시작 버튼


    public static GameManager Instance { get; private set; }// 싱글톤 인스턴스
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트 파괴
        }

    }

    public void ShowCoinCount()
    {

        coin++; // 코인 개수 증가
        textMeshProCoin.SetText(coin.ToString()); // 코인 개수 표시

        if (coin % 2 == 0)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Missileup(); // 플레이어 미사일 업그레이드
            }

        }
    }

    // ✅ 게임 오버 처리
    public void GameOver()
    {
        Time.timeScale = 0f; // 게임 정지
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // ✅ 게임 재시작
    public void RestartGame()
    {
        Time.timeScale = 1f; // 다시 실행
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }
}






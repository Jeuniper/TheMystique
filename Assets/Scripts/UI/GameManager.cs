using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    public void OnStartGameHandler(string sceneName)
    {
        //”√¿¥ªª≥°æ∞
        SceneManager.LoadScene(sceneName);
    }

    public void Interaction()
    {
        player.GetComponent<PlayerCountroller>().Attack();
    }
}

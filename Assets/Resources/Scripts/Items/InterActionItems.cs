using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterActionItems : MonoBehaviour
{
    // Start is called before the first frame update
    public Button attackButton;
    public Button speakButton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("碰撞");
        }
        attackButton.gameObject.SetActive(false);//不能攻击，要说话
        speakButton.gameObject.SetActive(true);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("离开");
        attackButton.gameObject.SetActive(true);
        speakButton.gameObject.SetActive(false);
    }
}

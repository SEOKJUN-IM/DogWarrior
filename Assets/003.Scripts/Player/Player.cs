using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public GameObject sword;
    public GameObject shield;

    void Awake()
    {
        GameManager.Instance.Player = this;
    }

    void Update()
    {
        sword.transform.localScale = Vector3.one * 1.5f;
        shield.transform.localScale = Vector3.one * 1.5f;
    }
}

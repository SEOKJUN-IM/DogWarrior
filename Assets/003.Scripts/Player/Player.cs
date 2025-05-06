using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    void Awake()
    {
        GameManager.Instance.Player = this;
    }
}

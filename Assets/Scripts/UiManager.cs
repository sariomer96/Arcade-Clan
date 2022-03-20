using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject startBtn;
    // Start is called before the first frame update

    public void StartGame()
    {
        characterController.Speed = 2.8f;
        startBtn.SetActive(false);

    }
}

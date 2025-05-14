using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public Transform player; // ������ ������, �� ������� ������ ��������� ������
    public float smoothTime = 0.3F; // ����� ����������� �������� ������
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // ���������� ����� ������� ������
        Vector3 targetPosition = player.TransformPoint(new Vector3(0, 0, -10));
        // ������ ���������� ������ � ������� ������ � ������ �����������
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

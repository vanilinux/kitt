using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public Transform player; // Объект игрока, за которым должна следовать камера
    public float smoothTime = 0.3F; // Время сглаживания движения камеры
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Определяем новую позицию камеры
        Vector3 targetPosition = player.TransformPoint(new Vector3(0, 0, -10));
        // Плавно перемещаем камеру к позиции игрока с учетом сглаживания
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

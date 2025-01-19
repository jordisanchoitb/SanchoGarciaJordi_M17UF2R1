using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRotationPlayer : MonoBehaviour
{
    private const float rotationSpeed = float.MaxValue;
    private const float fortyFiveDegrees = 45f;
    private const float zeroDegrees = 0f;
    [Header("Attributes")]
    [SerializeField]
    private bool isFireWeapon = false;
    private GameObject player;
    private float currentAngle = 0f;
    private float fixedAngle = 0f;


    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnEnable()
    {
        fixedAngle = isFireWeapon ? zeroDegrees : fortyFiveDegrees;
    }

    private void OnDisable()
    {
        fixedAngle = 0f;
    }

    private void Update()
    {
        Vector2 mousePosition = GetMousePosition();

        Vector2 playerPosition = player.transform.position;
        Vector2 direction = (mousePosition - playerPosition).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);
        float radians = currentAngle * Mathf.Deg2Rad;
        Vector2 newPosition = new Vector2(
            playerPosition.x + Mathf.Cos(radians) * distanceToPlayer,
            playerPosition.y + Mathf.Sin(radians) * distanceToPlayer
        );

        transform.SetPositionAndRotation(newPosition, Quaternion.Euler(0, 0, currentAngle - fixedAngle));
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public float GetCurrentAngle()
    {
        return currentAngle;
    }
}

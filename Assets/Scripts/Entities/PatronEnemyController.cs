using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronEnemyController : MonoBehaviour, IObjectPlatform {
    [Header("Componentes")]
    [SerializeField] Transform entity;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

     IObjectPlatform objectPlatform;
    [SerializeField] GameObject objectPlatformGO;

    [Header("Variables")]
    [SerializeField] float moveSpeed = 2.0f;

    private Vector3 currentTarget;

    [SerializeField] Vector2 axisEnityXZ;

    Vector3 lookAtDirection;
    float angle;
    Quaternion rotation;

    [SerializeField] bool onlyOneMovement = false;
    [SerializeField] bool startMovement = false;

    private void Start() {
        if(objectPlatformGO != null) objectPlatform = objectPlatformGO.GetComponent<IObjectPlatform>();

        currentTarget = endPosition.localPosition;
        LookAt();
    }

    private void FixedUpdate() {
        if(startMovement) {
            Move();
        }
    }

    void Move() {
        entity.localPosition = Vector3.MoveTowards(entity.localPosition, currentTarget, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(entity.localPosition, currentTarget) < 0.1f && !onlyOneMovement) {
            if (currentTarget == endPosition.localPosition) currentTarget = startPosition.localPosition;
            else currentTarget = endPosition.localPosition;

            LookAt();
        }
    }

    void LookAt() {
        lookAtDirection = currentTarget - entity.localPosition;

        angle = Mathf.Atan2(lookAtDirection.x, lookAtDirection.z) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(axisEnityXZ.x, angle, axisEnityXZ.y);

        entity.localRotation = rotation;

        //entity.LookAt(currentTarget);
        //entity.transform.localEulerAngles = new Vector3(axisEnityXZ.x, entity.transform.localEulerAngles.y, axisEnityXZ.y);
    }

    public void EnableMovement(bool enable) {
        startMovement = enable;
    }

    public void ResetObject() {
        EnableMovement(false);
        currentTarget = endPosition.localPosition;
        LookAt();
        entity.localPosition = startPosition.localPosition;

        if(objectPlatform != null)
            objectPlatform.ResetObject();
    }

    public void ActiveObject() {
        EnableMovement(true);
    }
}

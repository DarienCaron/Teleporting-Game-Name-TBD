﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ground Variables")]

 
    public FloatReference MovementAccelerationSpeed;
    public FloatReference MaxWalkSpeed;
    public FloatReference DecelerationSpeed;


    public FloatReference MaxRunSpeed;
    public FloatReference RunSpeedModifier;



    [Space(25)]
    [Header("In Air Values")]

    public float InAirMovementAcceleration = 30.0f;
    public float InAirMaxHorizontalSpeed = 20.0f;
    public float InAirMaxVeritcalSpeed = 50.0f;

    public float GravityAccel = -10.0f;

    


    [Space(45)]

    [Header("Rotation Variables")]

    public float YawRotation = 55.0f;


    [Space(25)]

    [Header("Ground Check Values")]

    public FloatReference GroundCheckStartOffsetY = new FloatReference(0.5f);
    public float CheckForGroundRadius = 0.5f;
    public float GroundResolutionOverlap = 0.05f;

    public IntReference MinAllowedSurfaceAngle = new IntReference(15);




    public GameObject FootLocation;

    [Space(35)]
    [Header("Constants")]

    public const float GroundHitAdjustment = 0.1f;
    public const string PlayerMask = "Player";



    void Start()
    {

        m_GroundCheckMask = ~LayerMask.GetMask(PlayerMask, "Ignore Raycast");

        m_RigidBody = GetComponent<Rigidbody>();

        m_Velocity = Vector3.zero;

        PlayerEyes = GetComponentInChildren<FirstPersonCamera>();


    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        
        m_Velocity = m_RigidBody.velocity;

        UpdateGroundInfo();

        Vector3 localMoveDir = Controller.GetMoveDir();
        localMoveDir.Normalize();


        switch (m_MovementState)
        {
            case MovementState.OnGround:
                UpdateOnGround(localMoveDir);
                break;
            case MovementState.InAir:
                UpdateInAir(localMoveDir);
                break;
            case MovementState.Disable:
                break;
        }


        RotatePlayer();










    }
    private void LateUpdate()
    {
        
    }



    public void ForceBodyRotation(Vector3 dir)
    {
        var q = Quaternion.LookRotation(dir, Vector3.up);
       
        PlayerEyes.SetYRotation(q.eulerAngles.y);
        PlayerEyes.SetXRotation(q.eulerAngles.x);
    }

    private void RotatePlayer()
    {
        #region Test_Rotation_UsingCameraDir
        // EXPERIMENTAL, WOULD NEED TO ADJUST CAMERA ROTATION LOGIC.
        //Vector3 goalRotateDir = m_Velocity - GroundVelocity;
        //goalRotateDir.y = 0.0f;

        //if(goalRotateDir.sqrMagnitude < m_PlayerEyes.RotationSpeed * m_PlayerEyes.RotationSpeed)
        //{
        //    Vector3 controlRotation = m_PlayerEyes.BodyRotation;

        //    controlRotation.x = 0.0f;

        //    goalRotateDir = Quaternion.Euler(controlRotation) * Vector3.forward;
        //    goalRotateDir.y = 0.0f;
        //}

        //Vector3 newRotateDir = MathUtilities.SlerpTo(m_PlayerEyes.RotationSmoothing, transform.forward, goalRotateDir, Time.fixedDeltaTime);

        //transform.rotation = Quaternion.LookRotation(newRotateDir);
        #endregion

      

        m_RigidBody.MoveRotation(Quaternion.Euler(new Vector3(0, PlayerEyes.CurrentYRotation, 0)));
        PlayerEyes.UpdateCameraRotation();

    }




    void UpdateGroundInfo()
    {
        GroundAngularVelocity = Vector3.zero;
        GroundVelocity = Vector3.zero;
        GroundNormal = Vector3.forward;

        m_CenterHeight = transform.position.y;

        float footHeight = FootLocation.transform.position.y;
        float halfCapsuleHeight = m_CenterHeight - footHeight;

        Vector3 rayStart = transform.position;
        rayStart.y += GroundCheckStartOffsetY.GetValue();

        Vector3 rayDir = Vector3.down;

        float rayDist = halfCapsuleHeight + GroundCheckStartOffsetY.GetValue() - CheckForGroundRadius;

        RaycastHit[] hitInfos = Physics.SphereCastAll(rayStart, CheckForGroundRadius, rayDir, rayDist + GroundHitAdjustment, m_GroundCheckMask);

        RaycastHit groundHitInfo = new RaycastHit();

        bool validGroundFound = false;

        float minGroundDist = float.MaxValue;

        foreach (RaycastHit hits in hitInfos)
        {

            // Replace the math equation below with a helper function. This is to calculate the vertical angle.
            float surfaceAngle = MathUtilities.CalculateVerticalAngle(hits.normal);

            if (surfaceAngle < MinAllowedSurfaceAngle.GetValue() || hits.distance <= 0.0f)
            {
                continue;
            }
            if (hits.distance < minGroundDist)
            {
                minGroundDist = hits.distance;
                groundHitInfo = hits;
                validGroundFound = true;
            }

        }

        if (!validGroundFound)
        {
            if (m_MovementState != MovementState.Disable)
            {
                SetMovementState(MovementState.InAir);
            }
            return;
        }

        Vector3 bottomAtHitPoint = MathUtilities.ProjectToBottomOfCapsule(groundHitInfo.point, transform.position, halfCapsuleHeight * 2.0f, CheckForGroundRadius);

        float stepUpAmount = groundHitInfo.point.y - bottomAtHitPoint.y;
        m_CenterHeight += stepUpAmount - GroundResolutionOverlap;

        GroundNormal = groundHitInfo.normal;

        if (m_MovementState != MovementState.Disable)
        {
            SetMovementState(MovementState.OnGround);
        }

    }

    void UpdateInAir(Vector3 localMoveDir)
    {
        if (localMoveDir.sqrMagnitude > MathUtilities.CompareEpsilon)
        {
            Vector3 moveAccel = CalculateMoveAccel(localMoveDir);

            moveAccel *= InAirMovementAcceleration;


            m_Velocity += moveAccel * Time.fixedDeltaTime;


            m_Velocity = MathUtilities.HorizontalClamp(m_Velocity, InAirMaxHorizontalSpeed);

            m_Velocity.y = Mathf.Clamp(m_Velocity.y, -InAirMaxVeritcalSpeed, InAirMaxVeritcalSpeed);
        }

        m_Velocity.y += GravityAccel * Time.fixedDeltaTime;

        ApplyVelocity(m_Velocity);

    }

    void UpdateOnGround(Vector3 localMoveDir)
    {
        if (localMoveDir.sqrMagnitude > MathUtilities.CompareEpsilon)
        {
            Vector3 localVelocity = m_Velocity - GroundVelocity;

            Vector3 moveAccel = CalculateMoveAccel(localMoveDir);
            Vector3 groundTangent = moveAccel - Vector3.Project(moveAccel, GroundNormal);

            groundTangent.Normalize();

            moveAccel = groundTangent;

            Vector3 velocityAlongMoveDir = Vector3.Project(localVelocity, moveAccel);


            if (Vector3.Dot(velocityAlongMoveDir, moveAccel) > 0.0f)
            {
                localVelocity = MathUtilities.LerpTo(DecelerationSpeed.ConstantValue, localVelocity, velocityAlongMoveDir, Time.fixedDeltaTime);

            }
            else
            {
                localVelocity = MathUtilities.LerpTo(DecelerationSpeed.ConstantValue, localVelocity, Vector3.zero, Time.fixedDeltaTime);
            }





            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveAccel *= MovementAccelerationSpeed.GetValue() * RunSpeedModifier.GetValue();
                localVelocity = Vector3.ClampMagnitude(localVelocity, MaxRunSpeed.ConstantValue);
                
            }
            else
            {
                moveAccel *= MovementAccelerationSpeed.GetValue();
                localVelocity = Vector3.ClampMagnitude(localVelocity, MaxWalkSpeed.GetValue());
                
            }

            localVelocity += moveAccel * Time.fixedDeltaTime;
            m_Velocity = localVelocity + GroundVelocity;


        }
        else
        {
            UpdateStopping(DecelerationSpeed.ConstantValue);
        }

        ApplyVelocity(m_Velocity);

        Vector3 playerCenter = transform.position;


        playerCenter.y = MathUtilities.LerpTo(DecelerationSpeed.ConstantValue, playerCenter.y, m_CenterHeight, Time.fixedDeltaTime);

        transform.position = playerCenter;


    }


    void UpdateStopping(float stopEaseSpeed)
    {
        m_Velocity = MathUtilities.LerpTo(stopEaseSpeed, m_Velocity, GroundVelocity, Time.fixedDeltaTime);
    }

    void ApplyVelocity(Vector3 velocity)
    {
        Vector3 velocityDiff = velocity - m_RigidBody.velocity;
        m_RigidBody.AddForce(velocityDiff, ForceMode.VelocityChange);
    }




    Vector3 CalculateMoveAccel(Vector3 localMoveDir)
    {
        Vector3 moveAccel = localMoveDir;
        moveAccel = transform.TransformDirection(moveAccel);
        return moveAccel;
    }

    void SetMovementState(MovementState state)
    {
        switch (state)
        {
            case MovementState.OnGround:
                break;
            case MovementState.InAir:

                break;
            case MovementState.Disable:
                m_Velocity = Vector3.zero;
                ApplyVelocity(m_Velocity);
                break;
        }
        m_MovementState = state;
    }








    public Vector3 GroundVelocity { get; private set; }
    public Vector3 GroundAngularVelocity { get; private set; }
    public Vector3 GroundNormal { get; private set; }
    public FirstPersonCamera PlayerEyes { get; private set; }

    public bool CanPlayerMove { get; private set; }


    public enum MovementState
    {
        OnGround,
        InAir,
        Disable
    }

    public InputManager Controller { get; set; }



    MovementState m_MovementState;
    Rigidbody m_RigidBody;
    Vector3 m_Velocity;
    float m_CenterHeight;
    int m_GroundCheckMask;
    FirstPersonCamera m_PlayerEyes;




    bool m_IsCrouching; // Temp
}

using UnityEngine;
﻿
﻿namespace Develop.Player.Entity
﻿{
﻿    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Develop/PlayerConfig")]
﻿    public class PlayerConfig : ScriptableObject
﻿    {
﻿        public float WalkSpeed => _walkSpeed;
﻿        public float RunSpeed => _runSpeed;
﻿        public float Damping => _damping;
﻿        public float EndSpeed => _endSpeed;
﻿
﻿        public float MaxAngle => _maxAngel;
﻿        public float LookSpeed => _lookSpeed;
﻿        public float DecelerationRate => _decelerationRate;
﻿        public float GroundCheckDistance => _groundCheckDistance;
﻿        public LayerMask GroundLayer => _groundLayer;
﻿        
﻿        public float ImpactStrength => _impactStrength;
﻿        public float DiagonalThreshold => _diagonalThreshold;
﻿        
﻿        public float GunImpactForce => _gunImpactForce;
﻿        public Vector3 GunImpactOffset => _gunImpactOffset;

﻿        public float JumpForce => _jumpForce;
﻿        public int MaxJumps => _maxJumps;

﻿        public float SlideSpeed => _slideSpeed;
﻿        public float SlideDuration => _slideDuration;
﻿        public float SlideColliderHeight => _slideColliderHeight;
﻿        public float SlideColliderCenterY => _slideColliderCenterY;


﻿        [Header("MovementState")]
﻿        [SerializeField] private float _walkSpeed;
﻿        [SerializeField] private float _runSpeed;
﻿        [SerializeField] private float _damping;
﻿        [SerializeField] private float _endSpeed;
﻿        [Header("LookState")]
﻿        [SerializeField] private float _maxAngel;
﻿        [SerializeField] private float _lookSpeed;
﻿        [Header("SlideState")]
﻿        [SerializeField] private float _decelerationRate;
﻿        [SerializeField] private float _groundCheckDistance;
﻿        [SerializeField] private LayerMask _groundLayer;
﻿        [Header("Impact Settings")]
﻿        [SerializeField] private float _impactStrength = 0.2f;
﻿        [SerializeField] private float _diagonalThreshold = 0.7f;
﻿        [Header("Gun Impact Settings")]
﻿        [SerializeField] private float _gunImpactForce = 0.1f; // 銃の衝撃の強さ
﻿        [SerializeField] private Vector3 _gunImpactOffset = new Vector3(0f, 0.05f, -0.1f); // 銃の衝撃のオフセット
﻿        [Header("Jump Settings")]
﻿        [SerializeField] private float _jumpForce = 5f;
﻿        [SerializeField] private int _maxJumps = 1;
﻿        [Header("Slide Settings")]
﻿        [SerializeField] private float _slideSpeed = 8f;
﻿        [SerializeField] private float _slideDuration = 1.5f;
﻿        [SerializeField] private float _slideColliderHeight = 1f;
﻿        [SerializeField] private float _slideColliderCenterY = 0.5f;

﻿
﻿    }
﻿}
﻿
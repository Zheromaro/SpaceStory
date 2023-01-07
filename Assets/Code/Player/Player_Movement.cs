using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;
using SpaceGame.Core.SaveSystem;

namespace SpaceGame.Player
{
    public class Player_Movement : MonoBehaviour, IDataPersistence
    {
        #region GetTheComponent

        private InputAction Input_Move;
        private Rigidbody2D rb;
        private Animator animator;
        #endregion

        #region movement

        [Space(8)]
        [Header("ForMovement")]
        [HideInInspector] public Vector2 movement;
        [HideInInspector] public float Manual = 1f;
        public bool freeHorizontalMovement = false;
        public bool freeVerticalMovement = true;
        private float x;
        private float y;
        #endregion

        #region speed

        [Space(8)]
        public float speed;
        [SerializeField] private float plusAmount;
        [SerializeField] private float minusAmount;

        private float checkForSpeed;
        #endregion

        //------------------------------------------------------------------

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Input_Move = InputManager.inputActions.Player.Movement;

            checkForSpeed = speed;
        }

        public void LoadData(GameData data)
        {
            this.transform.position = data.playerPosition;
        }

        public void SaveData(GameData data)
        {
            // Nothing...
        }

        private void Update()
        {
            if (freeHorizontalMovement)
            {
                movement.x = Input_Move.ReadValue<Vector2>().x;
            }
            else
            {
                x = Input_Move.ReadValue<Vector2>().x * plusAmount;
                movement.x = Manual + x;
            }

            if (freeVerticalMovement)
            {
                movement.y = Input_Move.ReadValue<Vector2>().y;
            }
            else
            {
                y = Input_Move.ReadValue<Vector2>().y * minusAmount;
                movement.y = Manual + y;
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            if (movement.x > 0.1f || movement.x < -0.1f || movement.y > 0.1f || movement.y < -0.1f)
            {
                rb.AddForce(new Vector2(movement.x * speed, movement.y * speed), ForceMode2D.Impulse);
            }
        }
    }
}
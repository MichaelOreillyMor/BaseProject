using UnityEngine;

namespace GFFramework.PlayerControlles
{
    /// <summary>
    /// The GameObject controlled directly by the player
    /// </summary>
    public abstract class BasePlayerController : MonoBehaviour
    {
        [SerializeField]
        protected Rigidbody rBody;

        [SerializeField]
        protected BoxCollider boxCollider;

        [SerializeField]
        private float speed;

        private Vector3 velocity;

        private bool canGetInput;

        public void Setup(/* some params */)
        {
            EnableInput(true);
            Debug.Log("Setup " + name);
        }

        public void Unsetup()
        {
            EnableInput(false);
            Debug.Log("Unsetup " + name);
        }

        public void EnableInput(bool enable) 
        {
            canGetInput = enable;
        }

        public void SetDirection(Vector2 dir)
        {
            if (canGetInput)
            {
                velocity = new Vector3(dir.x * speed, 0, dir.y * speed);
            }
        }

        private void FixedUpdate()
        {
            if (velocity != Vector3.zero)
            {
                rBody.MovePosition(transform.position + velocity * Time.deltaTime);//Time.deltaTime not necesary, but then speed needs to be like 0.001f
            }
        }

        void OnCollisionEnter(Collision collision)
        {

        }
    }
}

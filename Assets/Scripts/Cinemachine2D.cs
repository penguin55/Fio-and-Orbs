using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Cinemachine2D : MonoBehaviour {

    public static Cinemachine2D cinemachine;

    private Vector2 velocity;

    public GameObject target; //Object which will following
    private Rigidbody2D velocityControl;

    public float baseSpeed;
    private float speedMove;

    #region Bounding Cam

        #region SizeCam
        public float right;
        public float left;
        public float up;
        public float down;
        #endregion

    public Transform rightBound;
    public Transform leftBound;
    public Transform upBound;
    public Transform downBound;
    #endregion

    private void Start()
    {
        cinemachine = this;
        velocityControl = target.GetComponent<Rigidbody2D>();
        speedMove = baseSpeed;
    }

    private void Update()
    {
        if (transform.GetComponent<Camera>().orthographic)
        {
            try
            {

                float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, speedMove);
                float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, speedMove);

                transform.position = new Vector3(posX, posY, -10);

                if (/** Bounding Cam Here */true) //For bounding cam
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound.position.x + left, rightBound.position.x - right), Mathf.Clamp(transform.position.y, downBound.position.y + down, upBound.position.y - up), Mathf.Clamp(-10, -10, -10));
                }
            }
            catch (System.Exception er)
            {

            }
        }
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        velocityControl = target.GetComponent<Rigidbody2D>();
    }
}

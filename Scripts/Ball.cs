
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    
    //state 
    Vector2 paddleToBallVector;

    // Cached Component references  
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    

    bool hasStarted = false;


    void Start()
    {
        //subtract the ball position from paddle position to get the difference
        paddleToBallVector = transform.position - paddle1.transform.position;
        //finds audioSourceComponent once
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
        if (!hasStarted)
        {
            LockBallToPaddle();
            //you can only launch the ball once
            LaunchOnMouseClick();
        }
      
       
        

    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush,yPush);
           
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // tweaking velocity of a ball so it doesn't get stuck
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f,randomFactor), 
             Random.Range(0f, randomFactor));
        //play a sounds effect
        if (hasStarted)
        {
            //random clip of type AudioClip
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }



}

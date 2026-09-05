using UnityEngine;

// Put this on the target. Scores a point when a ball hits it, then hops somewhere new.
public class Target : MonoBehaviour
{
    public int pointsPerHit = 1;

    // The target slides side to side and up and down, but stays the same distance away.
    // Moving it toward or away from the viewer is jarring in a headset.
    public float horizontalRange = 4f;
    public float verticalRange = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BallPrefab>() == null)
        {
            return;
        }

        if (ScoreManager.Instance == null)
        {
            Debug.LogError("Target: no ScoreManager found in the scene.", this);
        }
        else
        {
            ScoreManager.Instance.AddPoints(pointsPerHit);
        }

        Destroy(collision.gameObject);
        MoveToNewSpot();
    }

    void MoveToNewSpot()
    {
        transform.position = startPosition + new Vector3(
            Random.Range(-horizontalRange, horizontalRange),
            Random.Range(-verticalRange, verticalRange),
            0f);
    }
}

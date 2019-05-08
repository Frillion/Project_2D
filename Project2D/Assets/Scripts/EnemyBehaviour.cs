using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyBehaviour : MonoBehaviour
{
    //The Traget To Get To
    public Transform target;
    //How Many Times Per Second The AI Updates Its Path
    public float uprate = 2f;
    //The Path To The Target
    public Path pathtotarget;
    //The Speed Of The AI
    public float AIspeed;
    //This Is So You Can Select How Force Is Applied To The AI
    public ForceMode2D fmode;
    //The Max Distance From The AI To A Waypoint For It To Continue To The Next Waypoint
    public float nextwaypointDistance = 3f;
    //Checks If The AI Has Reached A Destination
    [HideInInspector]
    public bool pathhasended = false;
    //Where The AI Is Moving Towards
    int currentwaypoint = 0;


    Seeker aIseeker;
    Rigidbody2D airb;

    private void Start()
    {
        aIseeker = GetComponent<Seeker>();
        airb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.Log("No Player Found");
            return;
        }

        Seeker.StartPath(transform.position, target.position, OnPathComplete());

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (pathtotarget == null)
        {
            return;
        }

        if (currentwaypoint >= pathtotarget.vectorPath.Count)
        {
            if (pathhasended)
            {
                return;
            }
            Debug.Log("End of Path reached");
            pathhasended = true;
            return;
        }
        pathhasended = false;

        Vector3 direction = (pathtotarget.vectorPath[currentwaypoint] - transform.position).normalized;
        direction *= AIspeed * Time.fixedDeltaTime;

        airb.AddForce(direction,fmode);
        float dist = Vector3.Distance(transform.position, pathtotarget.vectorPath[currentwaypoint]);
        if (dist < nextwaypointDistance)
        {
            currentwaypoint++;
            return;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            yield return false;
        }
        Seeker.StartPath(transform.position, target.position,OnPathComplete);

        yield return new WaitForSeconds(1f / uprate);

        StartCoroutine(UpdatePath());
    }

    public OnPathDelegate OnPathComplete(Path p)
    {
        Debug.Log("Got A Path. did it have an error" + p.error);
        if (!p.error)
        {
            pathtotarget = p;
            currentwaypoint = 0;
            
        }
        
    }
}

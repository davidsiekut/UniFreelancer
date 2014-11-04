using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSystem : MonoBehaviour
{
    const float MAX_DETECT_RANGE = 1000.0f;
    const float CLOSE_DETECT_RANGE = 400.0f;

    public GameObject LockPrefab;
    public AudioClip Boop;
    public AudioClip Beep;

    public GameObject ReticlePrefab;
    public GameObject ReticleLargePrefab;
    public GameObject OOBPrefab;

    // object pools
    List<GameObject> reticlePool;
    int reticlePoolCursor;
    List<GameObject> reticleLargePool;
    int reticleLargePoolCursor;
    List<GameObject> oobPool;
    int oobPoolCursor;

    // targeting
    bool lockRequested = false;
    float targetDistance = 300.0f; // distance to lock on
    GameObject frontTarget;
    bool frontLockingOn = false;
    float frontTargetBuffer;
    float _frontTargetBuffer = 5.0f; // time to lose lock
    bool frontLockReady = false;

    Camera cam;

    void Start()
    {
        frontTargetBuffer = _frontTargetBuffer;
        reticlePool = new List<GameObject>();
        reticleLargePool = new List<GameObject>();
        oobPool = new List<GameObject>();
        cam = Camera.main;
        resetTargetTransform();
    }

    public GameObject GetFrontLockTarget()
    {
        if (frontTarget != null && frontLockReady)
        {
            return frontTarget;
        }

        return null;
    }

    public void RequestLock()
    {
        lockRequested = true;
    }

    IEnumerator CoLockOn(GameObject piece, float delay, Vector3 initial)
    {
        yield return new WaitForSeconds(delay);

        float timer = 0.0f;
        Vector3 final = new Vector3(0, 0, 10f);
        Vector3 scaleInitial = new Vector3(10f, 10f, 10f);
        Vector3 scaleFinal = new Vector3(1f, 1f, 1f);

        Quaternion initialRotation = Random.rotation;
        Quaternion finalRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        piece.renderer.enabled = true;

        while (piece.transform.localPosition != final)
        {
            piece.transform.localPosition = Vector3.Lerp(initial, final, timer);
            piece.transform.localScale = Vector3.Lerp(scaleInitial, scaleFinal, timer);
            piece.transform.localRotation = Quaternion.Slerp(initialRotation, finalRotation, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        if (delay == 1.5f)
        {
            frontLockReady = true;
            GameController.HUDSound.PlayOneShot(Beep);
        }
        else
        {
            GameController.HUDSound.PlayOneShot(Boop);
        }

        yield return null;
    }

    void resetTargetTransform()
    {
        frontLockReady = false;
        frontTarget = null;

        StopAllCoroutines();

        LockPrefab.transform.GetChild(0).renderer.enabled = false;
        LockPrefab.transform.GetChild(1).renderer.enabled = false;
        LockPrefab.transform.GetChild(2).renderer.enabled = false;
        LockPrefab.transform.GetChild(3).renderer.enabled = false;

        LockPrefab.transform.position = Vector3.zero;
        LockPrefab.transform.localPosition = Vector3.zero;

        //TargetPrefab.transform.GetChild(0).position = new Vector3(0, 0, 0f);
        //TargetPrefab.transform.GetChild(1).position = new Vector3(0, 0, 0f);
        //TargetPrefab.transform.GetChild(2).position = new Vector3(0, 0, 0f);
        //TargetPrefab.transform.GetChild(3).position = new Vector3(0, 0, 0f);

        LockPrefab.transform.GetChild(0).localPosition = new Vector3(0, 0, -500f);
        LockPrefab.transform.GetChild(1).localPosition = new Vector3(0, 0, -500f);
        LockPrefab.transform.GetChild(2).localPosition = new Vector3(0, 0, -500f);
        LockPrefab.transform.GetChild(3).localPosition = new Vector3(0, 0, -500f);

        //TargetPrefab.transform.GetChild(0).rotation = Quaternion.identity;
        //TargetPrefab.transform.GetChild(1).rotation = Quaternion.identity;
        //TargetPrefab.transform.GetChild(2).rotation = Quaternion.identity;
        //TargetPrefab.transform.GetChild(3).rotation = Quaternion.identity;

        LockPrefab.transform.GetChild(0).localRotation = Quaternion.identity;
        LockPrefab.transform.GetChild(1).localRotation = Quaternion.identity;
        LockPrefab.transform.GetChild(2).localRotation = Quaternion.identity;
        LockPrefab.transform.GetChild(3).localRotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug.DrawRay(ray.origin, ray.direction * targetDistance, Color.yellow);
        RaycastHit hit;
        if (lockRequested)
        {
            lockRequested = false;
            // through front crosshair

            if (Physics.Raycast(ray, out hit, targetDistance))
            {
                //Debug.DrawRay(ray.origin, ray.direction * targetDistance, Color.green);

                // if its a different target
                if (hit.collider.gameObject != frontTarget && hit.collider.tag == "Enemy")
                {
                    resetTargetTransform();

                    frontTarget = hit.collider.gameObject;

                    StartCoroutine(CoLockOn(LockPrefab.transform.GetChild(0).gameObject, 0f, new Vector3(7f, -7f, 0f)));
                    StartCoroutine(CoLockOn(LockPrefab.transform.GetChild(1).gameObject, 0.5f, new Vector3(-7f, -7f, 0f)));
                    StartCoroutine(CoLockOn(LockPrefab.transform.GetChild(2).gameObject, 1f, new Vector3(7f, 7f, 0f)));
                    StartCoroutine(CoLockOn(LockPrefab.transform.GetChild(3).gameObject, 1.5f, new Vector3(-7f, 7f, 0f)));
                }
            }
        }

        // through front crosshair
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            frontTargetBuffer = _frontTargetBuffer;
            //frontTarget = hit.collider.gameObject;
        }

        frontTargetBuffer -= Time.deltaTime;
        if (frontTargetBuffer < 0)
        {
            resetTargetTransform();
        }
    }

    void LateUpdate()
    {
        // kind of a hack but it works, maybe make nice later
        if (frontTarget != null && !frontTarget.renderer.isVisible)
        {
            resetTargetTransform();
        }
        else
        {
            LockPrefab.transform.GetChild(0).renderer.enabled = true;
            LockPrefab.transform.GetChild(1).renderer.enabled = true;
            LockPrefab.transform.GetChild(2).renderer.enabled = true;
            LockPrefab.transform.GetChild(3).renderer.enabled = true;

        }

        resetPool();

        foreach (GameObject g in GameController.Entities)
        {
            if (g != null && g.tag != "Player" && Vector3.Distance(g.transform.position, GameController.Player.transform.position) < MAX_DETECT_RANGE)
            {
                Vector3 screen = cam.WorldToScreenPoint(g.transform.position);

                if (screen.z > 0
                    && screen.x > 0 && screen.x < Screen.width
                    && screen.y > 0 && screen.y < Screen.height)
                {
                    GameObject reticle;

                    if (Vector3.Distance(g.transform.position, GameController.Player.transform.position) < CLOSE_DETECT_RANGE)
                    {
                        reticle = getReticleLarge();

                    }
                    else
                    {
                        reticle = getReticle();
                    }

                    reticle.transform.localPosition = cam.ScreenToViewportPoint(screen);
                    reticle.guiText.text = (int)Vector3.Distance(g.transform.position, GameController.Player.transform.position) + " m";
                }
                else
                {
                    if (screen.z < 0)
                        screen *= -1;

                    // move (0,0) to center of screen
                    Vector3 center = new Vector3(Screen.width, Screen.height, 0f) / 2;
                    screen -= center;

                    float angle = Mathf.Atan2(screen.y, screen.x);
                    angle -= 90 * Mathf.Deg2Rad;

                    float cos = Mathf.Cos(angle);
                    float sin = -Mathf.Sin(angle);

                    screen = center + new Vector3(sin * 150f, cos * 150f, 0f);

                    float m = cos / sin;

                    Vector3 bounds = center * 0.98f;

                    if (cos > 0)
                    {
                        // up
                        screen = new Vector3(bounds.y / m, bounds.y, 0);
                    }
                    else
                    {
                        // down
                        screen = new Vector3(-bounds.y / m, -bounds.y, 0);
                    }

                    // sides
                    if (screen.x > bounds.x)
                    {
                        // right
                        screen = new Vector3(bounds.x, bounds.x * m, 0);
                    }
                    else if (screen.x < -bounds.x)
                    {
                        // left
                        screen = new Vector3(-bounds.x, -bounds.x * m, 0);
                    }

                    // revert to original coords
                    screen += center;

                    GameObject oob = getOOB();
                    oob.transform.localPosition = cam.ScreenToViewportPoint(screen);
                }
            }
        }

        cleanPool();

        if (frontTarget != null)
        {
            // keep following the target
            Vector3 positionOnScreen = cam.WorldToScreenPoint(frontTarget.transform.position);
            positionOnScreen.x = positionOnScreen.x - Screen.width / 2;
            positionOnScreen.y = positionOnScreen.y - Screen.height / 2;
            positionOnScreen.z += 10;
            LockPrefab.transform.localPosition = positionOnScreen;
        }
    }

    void resetPool()
    {
        reticlePoolCursor = 0;
        reticleLargePoolCursor = 0;
        oobPoolCursor = 0;
    }

    void cleanPool()
    {
        while (reticlePool.Count > reticlePoolCursor)
        {
            // last entry
            GameObject g = reticlePool[reticlePool.Count - 1];
            reticlePool.Remove(g);
            Destroy(g);
        }

        while (reticleLargePool.Count > reticleLargePoolCursor)
        {
            GameObject g = reticleLargePool[reticleLargePool.Count - 1];
            reticleLargePool.Remove(g);
            Destroy(g);
        }

        while (oobPool.Count > oobPoolCursor)
        {
            GameObject g = oobPool[oobPool.Count - 1];
            oobPool.Remove(g);
            Destroy(g);
        }
    }

    GameObject getReticle()
    {
        GameObject g;

        if (reticlePoolCursor < reticlePool.Count)
        {
            g = reticlePool[reticlePoolCursor];
        }
        else
        {
            g = GameObject.Instantiate(ReticlePrefab) as GameObject;
            g.transform.parent = this.transform;
            reticlePool.Add(g);
        }

        reticlePoolCursor++;

        return g;
    }

    GameObject getReticleLarge()
    {
        GameObject g;

        if (reticleLargePoolCursor < reticleLargePool.Count)
        {
            g = reticleLargePool[reticleLargePoolCursor];
        }
        else
        {
            g = GameObject.Instantiate(ReticleLargePrefab) as GameObject;
            g.transform.parent = this.transform;
            reticleLargePool.Add(g);
        }

        reticleLargePoolCursor++;

        return g;
    }

    GameObject getOOB()
    {
        GameObject g;

        if (oobPoolCursor < oobPool.Count)
        {
            g = oobPool[oobPoolCursor];
        }
        else
        {
            g = GameObject.Instantiate(OOBPrefab) as GameObject;
            g.transform.parent = this.transform;
            oobPool.Add(g);
        }

        oobPoolCursor++;

        return g;
    }
}
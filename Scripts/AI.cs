using UnityEngine;

public class AI : MonoBehaviour {
    public Spaceship spaceship;
    public Transform target;
    public int attitude = 0; // 0 - peacefull, 1 - hostile
    float range = 100f;
    private float distanceToEnemy;

	void Update () {
        switch (attitude)
        {
            case 0: {
                    FindTarget();
                    GoToTarget();
                }break;
            case 1: {
                    FindTarget();
                    GoToTarget();
                    AttackTarget();
                }
                break;
        }
    }

    public void FindTarget() {
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");
        distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

        if (distanceToEnemy <= range)
        {
            target = enemy.transform;
        }
        else
        {
            target = null;
        }       
    }
    public void AimTarget() {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, 5f * Time.deltaTime).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
    public void AttackTarget() {
        if (target == null) return;
        RaycastHit hit;
        if (Physics.Raycast(spaceship.transform.position, spaceship.transform.forward, out hit))
        {
            if(hit.collider.tag == "Player")
                if (spaceship.allowShoot) { spaceship.Fire(); StartCoroutine(spaceship.reload()); }
        }
        
    }
    public void GoToTarget() {
        if (target == null) return;
        AimTarget();
        if (distanceToEnemy > 30f) { spaceship.breaks(false); spaceship.moveForeward(0.2f); }
        else spaceship.breaks(true);
    }
    
}

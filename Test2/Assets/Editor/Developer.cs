using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Developer : EditorWindow
{

    [MenuItem("Developer/Setup/Zombie")]
    public static void SetupZombie()
    {
        GameObject selected = Selection.activeTransform.gameObject;
        Zombie zombie = selected.AddComponent<Zombie>();
        DamageReceiver receiver = selected.AddComponent<DamageReceiver>();
        receiver.multiplier = 1;
        receiver.life = zombie;
        CharacterController control = selected.AddComponent<CharacterController>();
        control.center = new Vector3(0, 1, 0);
        selected.AddComponent<NavMeshAgent>();
        Transform head = TransformHelper.GetTransformChild("Head_jnt", selected.transform);
        SphereCollider headSphere = head.gameObject.AddComponent<SphereCollider>();
        headSphere.center = new Vector3(-0.5f, 0, 0);
        headSphere.radius = 0.5f;
        DamageReceiver headReceiver = head.gameObject.AddComponent<DamageReceiver>();
        headReceiver.multiplier = 2;
        headReceiver.life = zombie;
        GameObject shootPoint = new GameObject("shootPoint");
        shootPoint.transform.parent = selected.transform;
        shootPoint.transform.localPosition = new Vector3(0, 1.29f, 0.7f);
        zombie.shootPoint = shootPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

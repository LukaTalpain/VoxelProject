using System.Runtime.CompilerServices;
using UnityEngine;
using Voxels;

public class CameraRaycast : MonoBehaviour
{
    public Camera cam;
    private void Update()
    {
        if  (Input.GetMouseButtonDown(0))
        {
            BreakRay();
        }
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
    private void BreakRay ()
    {
        RaycastHit target = ShootRayCast();
        Vector3 point = target.point;
        Vector3 normal = target.normal;
        Facing face = GetFace(normal);
        print ("face : "+face.ToString()+" x : " + point.x + " y : "+ point.y + " z : "+ point.z);
        if (point != Vector3.zero)
        {
            if (face == Facing.Top)
            {
                Vector3 newPos = new Vector3 ((int)point.x, (int)point.y -1, (int) point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);
                print(" x : " + newPos.x + " y : " + newPos.y + " z : " + newPos.z);
            }



            //target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(point);
        }

    }
    private void PlaceRay()
    {

    }

    private RaycastHit ShootRayCast()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("Touché : " + hit.collider.gameObject.name);
            return hit;
        }
        else
        {
            print("pas cooooool du otut ");
            return new RaycastHit();
        }

    }

    private Facing GetFace(Vector3 normal)
    {
        if (normal == Vector3.up) return Facing.Top;
        else if (normal == Vector3.down) return Facing.Bottom;
        else if (normal == Vector3.forward) return Facing.North;
        else if (normal == Vector3.back) return Facing.South;
        else if (normal == Vector3.right) return Facing.East;
        else if (normal == Vector3.left) return Facing.West;
        else return Facing.Top; // fallback
    }


}







using UnityEngine;
using Voxels;

public class CameraRaycast : MonoBehaviour
{
    public Blocks blockToPlace;
    public Camera cam;
    private void Update()
    {
        if  (Input.GetMouseButtonDown(0))
        {
            BreakRay();
        }
        if (Input.GetMouseButtonDown(1))
        {
            PlaceRay();
        }
    }
    private void BreakRay ()
    {
        RaycastHit target = ShootRayCast();
        Vector3 point = target.point;
        Vector3 normal = target.normal;
        Facing face = GetFace(normal);
        if (point != Vector3.zero)
        {
            if (face == Facing.Top)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y - 1, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);
            }
            else if (face == Facing.North)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z-1);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);
            }
            else if (face == Facing.South)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);
            }
            else if (face == Facing.East)
            {
                Vector3 newPos = new Vector3((int)point.x - 1, (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);
            }
            else if (face == Facing.West)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);

            }
            else if (face == Facing.Bottom)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().DestroyBlock(newPos);
            }

        }

    }
    private void PlaceRay()
    {
        RaycastHit target = ShootRayCast();
        Vector3 point = target.point;
        Vector3 normal = target.normal;
        Facing face = GetFace(normal);
        if (point != Vector3.zero)
        {
            if (face == Facing.Top)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().PlaceBlock(newPos, blockToPlace);
            }
            else if (face == Facing.North)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().PlaceBlock(newPos, blockToPlace);
            }
            else if (face == Facing.South)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y, (int)point.z-1);
                target.transform.gameObject.GetComponent<ChunkComponent>().PlaceBlock(newPos, blockToPlace);
            }
            else if (face == Facing.East)
            {
                Vector3 newPos = new Vector3((int)point.x , (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().PlaceBlock(newPos, blockToPlace);
            }
            else if (face == Facing.West)
            {
                Vector3 newPos = new Vector3((int)point.x -1 , (int)point.y, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().PlaceBlock(newPos, blockToPlace);

            }
            else if (face == Facing.Bottom)
            {
                Vector3 newPos = new Vector3((int)point.x, (int)point.y - 1, (int)point.z);
                target.transform.gameObject.GetComponent<ChunkComponent>().PlaceBlock(newPos, blockToPlace);
            }

        }

    }

    private RaycastHit ShootRayCast()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            return hit;
        }
        else
        {
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






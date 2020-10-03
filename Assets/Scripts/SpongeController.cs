using UnityEngine;

public class SpongeController : MonoBehaviour
{
    private Vector3 _mousePosition;
    private float _posZ;
    [SerializeField]private Vector3 _spongeOffset;

    private void Update()
    {
        #region Camera Raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.collider.tag == "PaintBucket")
            {
                Vector3 worldPos2 = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 2);
                transform.position = worldPos2;
                return;
            }
            Vector3 worldPos = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z) - _spongeOffset;
            //Vector3 worldPos = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
            transform.position = worldPos;
        }
        #endregion

        //_mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(_mousePosition);
        //transform.position = new Vector3(_mousePosition.x, _mousePosition.y, -2);

    }
}

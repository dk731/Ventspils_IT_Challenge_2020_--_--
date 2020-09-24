using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraFollower : MonoBehaviour
{

    private GameObject myCameraObj;
    private Camera myCamera;

    private RenderTexture myRender = null;
    private RectTransform myRt;

    private GameObject objToFollow;

    public GameObject selectedObj;
    void Start()
    {
        myRt = GetComponent<RectTransform>();
        myCameraObj = new GameObject();
        myCameraObj.transform.position = Vector3.zero;
        myRender = new RenderTexture((int)myRt.rect.width, (int)myRt.rect.height, 24);


        myCamera = myCameraObj.AddComponent<Camera>();

        myCamera.targetDisplay = 2;

        GetComponent<RawImage>().texture = myRender;
        myCamera.targetTexture = myRender;
        selectedObj = Instantiate(selectedObj);
        selectedObj.transform.position = Vector3.zero;
        selectedObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Fire1") != 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f) && hit.collider.gameObject.tag == "Particle")
            {
                objToFollow = hit.collider.gameObject;

                selectedObj.transform.parent = objToFollow.transform;
                selectedObj.transform.localPosition = Vector3.zero;
                selectedObj.SetActive(true);
                myCameraObj.transform.parent = objToFollow.transform;
                myCameraObj.transform.localPosition = Vector3.zero;
            }
        }

        if (objToFollow)
        {

            myCameraObj.transform.LookAt(objToFollow.GetComponent<Rigidbody>().velocity + myCameraObj.transform.position);

            myCamera.Render();
        }

    }
}

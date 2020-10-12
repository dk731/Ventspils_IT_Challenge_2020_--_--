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

    private string followerType;

    public GameObject textHolder;

    public Font infoFont;

    private Dictionary<string, Text> valuesToDisplay = new Dictionary<string, Text>();

    void Start()
    {
        textHolder = transform.GetChild(0).gameObject;
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

    Text CreateNewText(Vector2 pos, Vector2 size)
    {
        GameObject tmp = new GameObject();
        tmp.AddComponent<RectTransform>();
        Text returnText = tmp.AddComponent<Text>();
        tmp.transform.parent = textHolder.transform;

        RectTransform rt = tmp.GetComponent<RectTransform>();

        pos.x += size.x / 2.0f;
        pos.y -= size.y / 2.0f;

        rt.sizeDelta = size;
        rt.localPosition = pos;

        returnText.font = infoFont;
        returnText.color = Color.black;
        returnText.text = "default text";

        return returnText;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Fire1") != 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Clickable")))
            {
                foreach (Transform obj in textHolder.transform)
                    Destroy(obj.gameObject);

                objToFollow = hit.collider.gameObject;

                selectedObj.transform.parent = objToFollow.transform;
                selectedObj.transform.localPosition = Vector3.zero;
                selectedObj.SetActive(true);
                myCameraObj.transform.parent = objToFollow.transform;
                myCameraObj.transform.localPosition = Vector3.zero;
                followerType = hit.collider.gameObject.tag;

                valuesToDisplay.Clear();
                if (followerType == "Head")
                {
                    valuesToDisplay["Temperature"] = CreateNewText(new Vector2(0, 0), new Vector2(300, 30));
                    valuesToDisplay["Cycle"] = CreateNewText(new Vector2(0, -50), new Vector2(300, 30));
                    //valuesToDisplay["Breath Efficiency"] = CreateNewText(new Vector2(0, -100), new Vector2(300, 30));
                }
                else if (followerType == "Particle")
                {
                    valuesToDisplay["Temperature"] = CreateNewText(new Vector2(0, 0), new Vector2(300, 30));
                    valuesToDisplay["Oxygencon"] = CreateNewText(new Vector2(0, -50), new Vector2(300, 30));
                    valuesToDisplay["CO2con"] = CreateNewText(new Vector2(0, -100), new Vector2(300, 30));
                }
            }
        }

        if (objToFollow)
        {
            if (followerType == "Head")
            {
                myCameraObj.transform.LookAt(objToFollow.transform.GetChild(0).transform.position);
                BreathScript tmps = objToFollow.GetComponent<BreathScript>();
                valuesToDisplay["Temperature"].text = "Body Temperature: " + tmps.bodyTemp * 10.0f + " C°";
                valuesToDisplay["Cycle"].text = "Breath Cycle: " + tmps.cycleName;
                //valuesToDisplay["Breath Efficiency"].text = (tmps.breathEfficenty * 100.0f).ToString("0.00") + " %";
            }
            else if (followerType == "Particle")
            {
                myCameraObj.transform.LookAt(objToFollow.GetComponent<Rigidbody>().velocity + myCameraObj.transform.position);
                ParticleScript tmps = objToFollow.GetComponent<ParticleScript>();
                valuesToDisplay["Temperature"].text = "Particle Temperature: " + objToFollow.GetComponent<Rigidbody>().velocity.magnitude * 10.0f + " C°";
                valuesToDisplay["Oxygencon"].text = "Oxygen level: " + tmps.o2Con * 100f + "%";
                valuesToDisplay["CO2con"].text = "CO2 level: " + (1 - tmps.o2Con) * 100f + "%";

            }




        }

    }
}

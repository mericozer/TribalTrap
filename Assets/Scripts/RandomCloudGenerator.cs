using System.Collections.Generic;
using UnityEngine;

public class RandomCloudGenerator : MonoBehaviour
{
    [SerializeField] private Collider cloudBox;

    [SerializeField] private float cloudAmount = 256;

    [SerializeField] private List<GameObject> clouds;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cloudAmount; i++)
        {
            int selectedCloud = Random.Range(0, clouds.Count);
            Transform cloud = Instantiate(clouds[selectedCloud], GetRandomPoint(), Quaternion.identity, cloudBox.transform).transform;
            Vector3 scale = Vector3.one * Random.Range(0.2f, 0.5f);
            scale.x /= cloudBox.transform.localScale.x;
            scale.y /= cloudBox.transform.localScale.y;
            scale.z /= cloudBox.transform.localScale.z;
            cloud.localScale = scale;
            cloud.eulerAngles = Vector3.up * Random.Range(0f, 180f);
        }
    }

    private Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(cloudBox.bounds.min.x, cloudBox.bounds.max.x),
            Random.Range(cloudBox.bounds.min.y, cloudBox.bounds.max.y),
            Random.Range(cloudBox.bounds.min.z, cloudBox.bounds.max.z));
    }
}

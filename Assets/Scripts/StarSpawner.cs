using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour {

    public GameObject star;
    public Flare[] flares;
    public Color[] colors = { new Color32(61, 61, 119,255) };
    public int numStars = 100;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < numStars; i++) {
            float x = Random.Range(-100, 50);
            float y = Random.Range(-40, -10);
            float z = Random.Range(-90, -10);
            float rot = Random.Range(0, 360);

            GameObject go = Instantiate(star, transform);
            LensFlare lf = go.GetComponent<LensFlare>();
            lf.flare = flares[Random.Range(0, flares.Length)];
            lf.color = colors[Random.Range(0, colors.Length)];
            lf.brightness = Random.Range(0.05f, 0.30f);
            go.transform.position = new Vector3(x, y, z);
            go.transform.rotation = Quaternion.Euler(0, rot, 0);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    Material dissolveMat;
    Material wireframeMat;
    private void Start()
    {
        wireframeMat = GetComponent<Renderer>().materials[0];
        dissolveMat = GetComponent<Renderer>().materials[1];
        Dissolve();
    }

    public void Dissolve()
    {
        StartCoroutine(DissolveAnim());
    }

    private IEnumerator DissolveAnim()
    {
        dissolveMat.SetFloat("_DissolveValue", 0.0f);
        wireframeMat.SetFloat("_WireframeWidth", 1.0f);
        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
                yield return null; // wait 1 frame
                dissolveMat.SetFloat("_DissolveValue", t);
                wireframeMat.SetFloat("_WireframeWidth", 1-t);
        }
        dissolveMat.SetFloat("_DissolveValue", 1.0f);
        wireframeMat.SetFloat("_WireframeWidth", 0.0f);
    }
}

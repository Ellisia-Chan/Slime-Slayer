using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDistruction : MonoBehaviour
{
    
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private string VFXSortingLayer = "Default";
    [SerializeField] private int VFXSortingLayerOrder = 0;

    private ParticleSystemRenderer ps;
    private GameObject destructionVFX;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<DamageSource>()) {
            destructionVFX = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            SetSortingLayer(destructionVFX);
            Destroy(gameObject);
        }
    }

    private void SetSortingLayer(GameObject obj) {
        ps = obj.GetComponent<ParticleSystemRenderer>();
        if (ps != null) {
            ps.sortingLayerName = VFXSortingLayer;
            ps.sortingOrder = VFXSortingLayerOrder;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magz : MonoBehaviour
{
    [SerializeField] private int loaded;
    [SerializeField] private Bullet prefab;
    [SerializeField] private List<Bullet> m_;
    [SerializeField] private Transform bulletParent;

    // Start is called before the first frame update
    private void Start()
    {
        m_ = new List<Bullet>();
        for (int i = 0; i < loaded; i++)
        {
            var bullet = Instantiate(prefab);
            bullet.transform.parent = bulletParent;
            m_.Add(bullet);
            bullet.gameObject.SetActive(false);
        }
    }

    public Bullet GetAvailableBullet()
    {
        for (int i = 0; i < m_.Count; i++)
        {
            if (!m_[i].gameObject.activeInHierarchy) return m_[i];
        }
        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM : MonoBehaviour
{
    public SpriteRenderer haha;

    private void Awake()
    {
 
    }	

    private void Start()
    {
        
    }

	private void Update()
	{   
        if(Input.GetMouseButton(0))
		{
            float xx1 = haha.bounds.size.x;
            float xx2 = haha.bounds.size.y;
            float xx3 = haha.bounds.size.z;
            float aa = haha.size.x;
            float bb = haha.size.y;
            float cc = haha.sprite.rect.width;
            float dd = haha.sprite.rect.height;
            float ee = haha.sprite.rect.x;
            float ff = haha.sprite.rect.y;
            float gg = haha.sprite.border.x;
            float hh = haha.sprite.border.y;
            float ii = haha.sprite.border.z;
            float jj = haha.sprite.border.w;
        }
	}
}

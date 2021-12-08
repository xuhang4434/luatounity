using UnityEngine;
/// <summary>
/// ��ͨ��������
/// </summary>
/// <typeparam name="T">���Ͳ���</typeparam>
public abstract class Singleton<T> where T : class , new()
{
	private static T instance;

	public static T Instance()
	{
		if(instance == null)
		{
			instance = new T();
		}
		return instance;
	}
}

/// <summary>
/// MonoBehaviour���ุ��
/// </summary>
/// <typeparam name="T">���Ͳ���</typeparam>
public abstract class SingletonMono<T> : MonoBehaviour where T :MonoBehaviour
{
	private static T instance;
	
	public static T Instance()
	{
		if(instance == null)
		{
			GameObject go = new GameObject(typeof(T).ToString());
			instance = go.AddComponent<T>();
			GameObject.DontDestroyOnLoad(instance.gameObject);
		}

		return instance;
	}
}

using UnityEngine;
/// <summary>
/// 普通单例父类
/// </summary>
/// <typeparam name="T">泛型参数</typeparam>
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
/// MonoBehaviour单类父类
/// </summary>
/// <typeparam name="T">泛型参数</typeparam>
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

using UnityEngine;
using System.Collections;

public interface ISavable<T> {
	T GetSavableObject();
	void LoadFrom(T data);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
  [SerializeField]
  private Transform pointPrefab;
  
  [SerializeField, Range(10, 100)]
  int resolution = 10;

  void Awake ()
  {
      float step = 2f / resolution;
      var position = Vector3.zero;
      var scale = Vector3.one * step;
      for (int i = 0; i < resolution; i++) {
          Transform point = Instantiate(pointPrefab);
          position.x = (i + 0.5f) * step - 1f;
          //y = x squared
          position.y = position.x * position.x * position.x;
          point.localPosition = position;
          point.localScale = scale;
          //set instantiated objects to parent
          point.SetParent(transform, false);
      }
  }
}

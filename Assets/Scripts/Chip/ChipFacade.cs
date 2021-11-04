using System;
using Logic;
using UnityEngine;

namespace Chip
{
  public class ChipFacade : MonoBehaviour
  {
    public GameObject ActiveFX;
    public Type Type;

    private int _id;

    public void SetId(int id) => 
      _id = id;

    public void Deactivate() => 
      ActiveFX.SetActive(false);

    public void Activate() => 
      ActiveFX.SetActive(true);
  }
}
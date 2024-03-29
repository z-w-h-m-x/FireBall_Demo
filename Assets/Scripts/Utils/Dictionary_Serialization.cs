using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Serialization<TKey,TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;
    Dictionary<TKey,TValue> target;

    public Dictionary<TKey,TValue> ToDictionary(){return target;}

    public Serialization(Dictionary<TKey,TValue> target){
        this.target = target;
    }

    public void OnBeforeSerialize(){
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize(){
        var conut = Math.Min(keys.Count,values.Count);
        target = new Dictionary<TKey, TValue>(conut);

        for(var i = 0;i < conut; i++){
            target.Add(keys[i],values[i]);
        }

    }
}
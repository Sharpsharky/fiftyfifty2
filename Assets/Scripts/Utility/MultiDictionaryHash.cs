using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ColdCry
{
    namespace Utility
    {
        public class MultiDictionaryHash<TKey, TValue> : MultiDictionary<TKey, TValue, HashSet<TValue>>
        {
            public override TValue Add(TKey key, TValue value)
            {
                if (data.ContainsKey( key )) {
                    data[key].Add( value );
                } else {
                    data.Add( key, new HashSet<TValue> { value } );
                }
                ValueCount++;
                return value;
            }

            public override TValue[] AddAll(TKey key, TValue[] values)
            {
                if (data.ContainsKey( key )) {
                    foreach ( TValue value in values ) {
                        data[key].Add( value );
                    }
                } else {
                    HashSet<TValue> set = new HashSet<TValue>(values);
                    data.Add( key, set );
                }
                ValueCount += values.Length;
                return values;
            }

        }
    }
}
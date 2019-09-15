using ColdCry.Exception;

using ColdCry.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColdCry.Graphic
{
    public static class Graphics
    {
        public static readonly string PLATFORMS_PATH = "Graphics/Platforms/";
        public static readonly string HEAD_PATHS = "Graphics/Animals/Heads/";

        private static MultiDictionaryList<PlatformType, Sprite> platforms = new MultiDictionaryList<PlatformType, Sprite>();
        private static Dictionary<HeadType, Sprite> heads = new Dictionary<HeadType, Sprite>();


        /// <summary>
        /// Loads all graphics from files 
        /// </summary>
        public static void LoadGraphics()
        {
            if (Loaded) {
                platforms.Clear();
            }
            Loaded = true;

            _LoadMultiGraphics( platforms );
            _LoadGraphics( heads );
        }

        public static Sprite GetPlatform(int size)
        {
            return platforms.Get( Utility.Random.RandomEnum<PlatformType>(), size );
        }

        public static Sprite GetHead(HeadType type)
        {
            return heads[type];
        }

        public static Sprite GetRandomHead()
        {
            return GetHead( Utility.Random.RandomEnum<HeadType>() );
        }

        public static string GetFilePath(PlatformType type)
        {
            return PLATFORMS_PATH + type.ToString() + "/";
        }

        public static string GetFilePath(HeadType type)
        {
            return HEAD_PATHS + type.ToString();
        }

        private static void _LoadGraphics<E>(Dictionary<E, Sprite> dict) where E : IConvertible
        {
            foreach (E value in Collections.GetValues<E>()) {
                string path = _GetFilePath( value );
                Sprite sprite = Resources.Load<Sprite>( path );
                if (sprite) {
                    dict.Add( value, sprite );
                } else {
                    Debug.LogWarning( "Missing graphics file at path: " + path );
                }
            }
        }

        private static string _GetFilePath<E>(E e) where E : IConvertible
        {
            object parsed = Enum.Parse( typeof( E ), e.ToString() );
            if (Objects.IsTypeOf<PlatformType>( e )) {
                return GetFilePath( (PlatformType) parsed );
            } else if (Objects.IsTypeOf<HeadType>( e )) {
                return GetFilePath( (HeadType) parsed );
            }
            throw new MissingTypeException( "Missing implementation or wrong enum type: " + e.GetType() );
        }

        private static void _LoadMultiGraphics<E>(MultiDictionaryList<E, Sprite> mdl) where E : IConvertible
        {
            foreach (E value in Collections.GetValues<E>()) {
                string path = _GetFilePath( value );
                Sprite[] sprites = Resources.LoadAll<Sprite>( path );
                if (sprites != null) {
                    foreach (Sprite sprite in sprites) {
                        mdl.Add( value, sprite );
                    }
                }
            }
        }

        public static bool Loaded { get; private set; } = false;
    }
}



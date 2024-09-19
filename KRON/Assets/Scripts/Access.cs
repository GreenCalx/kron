using UnityEngine;

public class Access
{
    private class AccessCache
    {
        private GameObject GO_PLAYER;
        private GameObject GO_MGR;
        private GameObject GO_PUX;

        private static AccessCache inst;
        public static AccessCache Instance
        {
            get { return inst ?? (inst = new AccessCache()); }
            private set { inst = value; }
        }

        public GameObject checkCacheObject(string iHolder, ref GameObject iStorage)
        {
            GameObject handler = null;
            handler = !!iStorage ? iStorage : GameObject.Find(iHolder);
            if (!iStorage && !!handler)
                iStorage = handler;
            return handler;
        }
        public T getObject<T>(string iHolder, bool iComponentIsInChildren)
        {
            GameObject handler = null;

            if (iHolder == Constants.GO_PLAYER)
            {
                handler = checkCacheObject(iHolder, ref GO_PLAYER);
            }
            else if (iHolder == Constants.GO_MGR)
            {
                handler = checkCacheObject(iHolder, ref GO_MGR);
            }
            else if (iHolder == Constants.GO_PUX)
            {
                handler = checkCacheObject(iHolder, ref GO_PUX);
            }
            else
            {
                handler = GameObject.Find(iHolder);
            }
            if (!!iComponentIsInChildren)
                return !!handler ? handler.GetComponentInChildren<T>(true) : default(T);
            return !!handler ? handler.GetComponent<T>() : default(T);
        }

        public void invalidate()
        {
            GO_PLAYER = null;
            GO_MGR = null;
            GO_PUX = null;
        }
    }//! cache

    private static AccessCache cache = AccessCache.Instance;
    public static void invalidate()
    {
        cache.invalidate();
    }
    public static T Get<T>()
    {
        if (typeof(T)==typeof(PlayerController)) 
            return cache.getObject<T>(Constants.GO_PLAYER, true);
        if (typeof(T)==typeof(SoundManager)) 
            return cache.getObject<T>(Constants.GO_MGR, true);
        if (typeof(T)==typeof(PlayerUI)) 
            return cache.getObject<T>(Constants.GO_PUX, true);
        return default(T);
    }

    public static PlayerController Player() { return Get<PlayerController>(); }
    public static PlayerUI PUX() { return Get<PlayerUI>(); }
    public static SoundManager SoundManager() { return Get<SoundManager>(); }

    public static CameraManager CameraManager() { return Get<CameraManager>(); }
}
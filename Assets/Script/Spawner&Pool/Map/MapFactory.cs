using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackType
{
    Track0 =0,
    Track1,
    /*Track2,
    Track3,
    Track4,
    Track5,
    Track6,
*/}
public class MapFactory : Singleton<MapFactory>
{
    Track0Pool track0pool;
    Track1Pool track1pool;
    /*Track2Pool track2pool;
    Track3Pool track3pool;
    Track4Pool track4pool;
    Track5Pool track5pool;
    Track6Pool track6pool;
*/

    protected override void PreInitialize()
    {
        track0pool = GetComponentInChildren<Track0Pool>();
        track1pool = GetComponentInChildren<Track1Pool>();
        /*track2pool = GetComponentInChildren<Track2Pool>();
        track3pool = GetComponentInChildren<Track3Pool>();
        track4pool = GetComponentInChildren<Track4Pool>();
        track5pool = GetComponentInChildren<Track5Pool>();
        track6pool = GetComponentInChildren<Track6Pool>();
*/    }

    protected override void Initialize()
    {
        track0pool?.Initialize();
        track1pool?.Initialize();
        /*track2pool?.Initialize();
        track3pool?.Initialize();
        track4pool?.Initialize();
        track5pool?.Initialize();
        track6pool?.Initialize();
*/    }

    public GameObject GetObject(TrackType type)
    {
        GameObject result = null;
        switch (type)
        {
            case TrackType.Track0:
                result = GetTrack0().gameObject;
                break;
            case TrackType.Track1:
                result = GetTrack1().gameObject;
                break;
            /*case TrackType.Track2:
                result = GetTrack2().gameObject;
                break;
            case TrackType.Track3:
                result = GetTrack3().gameObject;
                break;
            case TrackType.Track4:
                result = GetTrack4().gameObject;
                break;
            case TrackType.Track5:
                result = GetTrack5().gameObject;
                break;
            case TrackType.Track6:
                result = GetTrack6().gameObject;
                break;
*/        }
        return result;
    }


    public Land1 GetTrack0() => track0pool?.GetObject();
    public Land2 GetTrack1() => track1pool?.GetObject();
    /*public Land3 GetTrack2() => track2pool?.GetObject();
    public Track3 GetTrack3() => track3pool?.GetObject();
    public Land5 GetTrack4() => track4pool?.GetObject();
    public Land6 GetTrack5() => track5pool?.GetObject();
    public Land7 GetTrack6() => track6pool?.GetObject();*/

}

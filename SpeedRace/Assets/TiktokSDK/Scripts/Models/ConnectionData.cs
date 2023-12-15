
using System;

[Serializable]
public class ConnectionData
{
    public int eventId;
    public string eventType;
    public Connect data;
}

[Serializable]
public class Connect
{
    public string state;
}
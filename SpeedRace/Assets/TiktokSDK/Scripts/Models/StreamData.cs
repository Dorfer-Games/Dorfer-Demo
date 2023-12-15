using System;

[Serializable]
public class StreamData
{
    public string command;
    public Options options;

    public StreamData(string command, string service, string uniqId)
    {
        this.command = command;
        options = new Options(service, uniqId);
    }

}
[Serializable]
public class Options
{
    public string service;
    public string uniqueId;

    public Options(string service, string uniqueId)
    {
        this.service = service;
        this.uniqueId = uniqueId;
    }
}
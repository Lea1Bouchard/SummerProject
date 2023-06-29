using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string EventDescription;
}

public class ExampleGameEvent : GameEvent
{
    public string Example;

    public ExampleGameEvent(string name)
    {
        Example = name;
    }
}

public class FetchGameEvent : GameEvent
{
    public string FetchedItem;

    public FetchGameEvent(string name)
    {
        FetchedItem = name;
    }
}

public class KillGameEvent : GameEvent
{
    public string KilledEnemie;

    public KillGameEvent(string name)
    {
        KilledEnemie = name;
    }
}

public class TalkGameEvent : GameEvent
{
    public string TalkedTo;

    public TalkGameEvent(string name)
    {
        TalkedTo = name;
    }
}

public class BringGameEvent : GameEvent
{
    public string BroughtItem;
    public string Location;


    public BringGameEvent(string item, string location)
    {
        BroughtItem = item;
        Location = location;
    }
}
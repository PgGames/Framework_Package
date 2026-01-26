using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGFrammework.Runtime;

public class EventDemo : MonoBehaviour
{
    public enum Demo1 { one, two, there }
    public enum Demo2 { one, two, there }

    private void Awake()
    {
    }
    private void OnEnable()
    {
        GameEntity.Event.Subscribe(Demo1.one, Event_Demo1_One);
        GameEntity.Event.Subscribe(Demo1.two, Event_Demo1_Two);
        GameEntity.Event.Subscribe(Demo1.there, Event_Demo1_There);


        GameEntity.Event.Subscribe(Demo2.one, Event_Demo2_One);
        GameEntity.Event.Subscribe(Demo2.two, Event_Demo2_Two);
        GameEntity.Event.Subscribe(Demo2.there, Event_Demo2_There);
    }
    private void OnDisable()
    {
        GameEntity.Event.Unsubscribe(Demo1.one, Event_Demo1_One);
        GameEntity.Event.Unsubscribe(Demo1.two, Event_Demo1_Two);
        GameEntity.Event.Unsubscribe(Demo1.there, Event_Demo1_There);

        GameEntity.Event.Unsubscribe(Demo2.one, Event_Demo2_One);
        GameEntity.Event.Unsubscribe(Demo2.two, Event_Demo2_Two);
        GameEntity.Event.Unsubscribe(Demo2.there, Event_Demo2_There);
    }


    public void Event_Fire_Demo1_One() { GameEntity.Event.Fire(Demo1.one, null);  }
    public void Event_Fire_Demo1_Two() { GameEntity.Event.Fire(Demo1.two, null);  }
    public void Event_Fire_Demo1_There() { GameEntity.Event.Fire(Demo1.there, null); }
    public void Event_Fire_Demo2_One() { GameEntity.Event.Fire(Demo2.one, null); }
    public void Event_Fire_Demo2_Two() { GameEntity.Event.Fire(Demo2.two, null); }
    public void Event_Fire_Demo2_There() { GameEntity.Event.Fire(Demo2.there, null); }



    private void Event_Demo1_One(EventDate date)
    {
        Debug.LogError("Demo1 one");
    }
    private void Event_Demo1_Two(EventDate date)
    {
        Debug.LogError("Demo1 two");
    }
    private void Event_Demo1_There(EventDate date)
    {
        Debug.LogError("Demo1 there");
    }
    private void Event_Demo2_One(EventDate date)
    {
        Debug.LogError("Demo2 one");
    }
    private void Event_Demo2_Two(EventDate date)
    {
        Debug.LogError("Demo2 two");
    }
    private void Event_Demo2_There(EventDate date)
    {
        Debug.LogError("Demo2 there");
    }
}

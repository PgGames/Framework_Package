using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGFrammework.Runtime
{
    public class EventPool
    {
        Dictionary<int, List<EventDelegate>> _allEvent = new Dictionary<int, List<EventDelegate>>();


        public void Fire(int eventId,object data)
        {
            if (!_allEvent.ContainsKey(eventId))
            {
                FrameworkLog.Warning($"eventid {eventId} is not listen");
            }
            else
            {
                EventDate eventDate = new EventDate(data);

                var array = _allEvent[eventId];
                for (int i = 0; i < array.Count; i++)
                {
                    array[i]?.Invoke(eventDate);
                }
            }
        }
        public bool Check(int eventId, EventDelegate eventDelegate)
        {
            if (_allEvent.ContainsKey(eventId))
            {
                return _allEvent[eventId].Contains(eventDelegate);
            }
            return false;
        }
        public int Count(int eventId)
        {
            if (_allEvent.ContainsKey(eventId))
            {
                return _allEvent[eventId].Count;
            }
            return 0;
        }
        public void Subscribe(int eventId, EventDelegate eventCallback)
        {
            if (_allEvent.ContainsKey(eventId))
            {
                _allEvent[eventId].Add(eventCallback);
            }
            else 
            {
                _allEvent.Add(eventId, new List<EventDelegate>() { eventCallback });
            }
        }
        public void Unsubscribe(int eventId, EventDelegate eventCallback)
        {
            if (_allEvent.ContainsKey(eventId))
            {
                _allEvent[eventId].Remove(eventCallback);
            }
            else
            {
                FrameworkLog.Warning($"Method :{eventCallback.Method.Name} is not subscribe event !");
            }
        }
    }
}

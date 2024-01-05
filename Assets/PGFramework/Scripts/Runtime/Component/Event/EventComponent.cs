using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGFrammework.Runtime
{
	public class EventComponent : FrameworkComponent, IEventComponent
	{
		private Dictionary<uint, List<EventDelegate>> _allEvent = new Dictionary<uint, List<EventDelegate>>();

        public override void Init()
        {
        }

        public void Fire(uint eventID, object data)
		{
			if (!_allEvent.ContainsKey(eventID))
			{
				FrameworkLog.Warning($"eventid {eventID} is not listen");
			}
			else
			{
				if (_allEvent[eventID].Count == 0)
				{
					FrameworkLog.Warning($"eventid {eventID} is not listen");
				}
				else
				{
					EventDate tempEvent = new EventDate(eventID, data);

					var array = _allEvent[eventID];
					for (int i = 0; i < array.Count; i++)
					{
						array[i].Invoke(tempEvent);
					}
				}
			}
		}

		public bool Check(uint eventID, EventDelegate eventCallback)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				if (_allEvent[eventID].Contains(eventCallback))
				{
					return true;
				}
			}
			return false;
		}

		public int Count(uint eventID)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				return _allEvent[eventID].Count;
			}
			return 0;
		}


		public void Subscribe(uint eventID, EventDelegate eventCallback)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				if (_allEvent[eventID].Contains(eventCallback))
				{
					FrameworkLog.Warning($"eventid :{eventID},method:{eventCallback.Method.Name} subscribe repeat ");
				}
				else
				{
					_allEvent[eventID].Add(eventCallback);
				}
			}
			else
			{
				List<EventDelegate> delegates = new List<EventDelegate>();
				delegates.Add(eventCallback);

				_allEvent.Add(eventID, delegates);
			}
		}

		public void Unsubscribe(uint eventID, EventDelegate eventCallback)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				if (_allEvent[eventID].Contains(eventCallback))
				{
					_allEvent[eventID].Remove(eventCallback);
				}
				else
				{
					FrameworkLog.Warning($"Method :{eventCallback.Method.Name} is not subscribe event !");
				}
			}
			else
			{
				FrameworkLog.Warning($"Event {eventID} is not subscribe event !");
			}
		}
	}

}

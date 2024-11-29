using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGFrammework.Runtime
{
	public class EventComponent : FrameworkComponent, IEventComponent
	{
		//private Dictionary<int, List<EventDelegate>> _allEvent = new Dictionary<int, List<EventDelegate>>();

		private Dictionary<int, EventPool> _allEvent = new Dictionary<int, EventPool>();


        public override void Init()
        {
        }

		public void Fire(Enum id, object data)
		{
			Fire((int)id.GetType().GetHashCode(), Convert.ToInt32(id), data);
		}
		public bool Check(Enum id, EventDelegate eventCallback)
		{
			return Check(id.GetType().GetHashCode(), Convert.ToInt32(id), eventCallback);
		}
		public int Count(Enum id)
		{
			return Count(id.GetType().GetHashCode(), Convert.ToInt32(id));
		}
		public void Subscribe(Enum id, EventDelegate eventCallback)
		{
			Subscribe(id.GetType().GetHashCode(), Convert.ToInt32(id), eventCallback);
		}
		public void Unsubscribe(Enum id, EventDelegate eventCallback)
		{
			Unsubscribe(id.GetType().GetHashCode(), Convert.ToInt32(id), eventCallback);
		}




		private void Fire(int eventID,int id, object data)
		{
			if (!_allEvent.ContainsKey(eventID))
			{
				FrameworkLog.Warning($"eventid {eventID} is not listen");
			}
			else
			{
				_allEvent[eventID].Fire(id, data);
			}
		}

		private bool Check(int eventID, int id, EventDelegate eventCallback)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				return _allEvent[eventID].Check(id, eventCallback);
			}
			return false;
		}

		private int Count(int eventID,int id)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				return _allEvent[eventID].Count(id);
			}
			return 0;
		}
		/// <summary>
		/// 订阅事件
		/// </summary>
		/// <param name="eventID"></param>
		/// <param name="eventCallback"></param>
		public void Subscribe(int eventID,int id, EventDelegate eventCallback)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				_allEvent[eventID].Subscribe(id, eventCallback);
			}
			else
			{
				EventPool eventPool = new EventPool();
				eventPool.Subscribe(id, eventCallback);

				_allEvent.Add(eventID, eventPool);
			}
		}

		public void Unsubscribe(int eventID, int id, EventDelegate eventCallback)
		{
			if (_allEvent.ContainsKey(eventID))
			{
				_allEvent[eventID].Unsubscribe(id, eventCallback);
			}
			else
			{
				FrameworkLog.Warning($"Event {eventID} is not subscribe event !");
			}
		}
	}

}

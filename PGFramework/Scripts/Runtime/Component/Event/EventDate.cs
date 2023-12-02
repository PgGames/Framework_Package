using System.Collections;
using UnityEngine;

namespace PGFrammework.Runtime
{
	public delegate void EventDelegate(EventDate date);

	public class EventDate
	{
		public EventDate(uint varID, object vardate)
		{
			mEventID = varID;
			mEventDate = vardate;
		}
		private uint mEventID;
		private object mEventDate;

		public uint EventId { get { return mEventID; } }
		public object Date { get { return mEventDate; } }
	}

}
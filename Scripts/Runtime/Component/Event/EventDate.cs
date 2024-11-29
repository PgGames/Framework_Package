using System.Collections;
using UnityEngine;

namespace PGFrammework.Runtime
{
	public delegate void EventDelegate(EventDate date);

	public class EventDate
	{
		public EventDate(int varID, object vardate)
		{
			mEventID = varID;
			mEventDate = vardate;
		}
		public EventDate(object vardate)
		{
			mEventDate = vardate;
		}

		private int mEventID;
		private object mEventDate;

		public int EventId { get { return mEventID; } }
		public object Date { get { return mEventDate; } }
	}

}
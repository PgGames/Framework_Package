using System;
using System.Collections.Generic;
using System.Linq;

namespace PGFrammework.Runtime
{
    public interface IEventComponent
    {
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventID">事件id</param>
        /// <param name="date">数据</param>
        void Fire(uint eventID, object date);
        /// <summary>
        /// 获取数量监听数量
        /// </summary>
        /// <param name="eventID">事件id</param>
        /// <returns>事件数量</returns>
        int Count(uint eventID);
        /// <summary>
        /// 检测事件是否存在
        /// </summary>
        /// <param name="eventID">事件id</param>
        /// <param name="eventCallback">事件回调</param>
        /// <returns>事件监听的存在与否</returns>
        bool Check(uint eventID, EventDelegate eventCallback);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventID">事件id</param>
        /// <param name="eventCallback">事件回调</param>
        void Subscribe(uint eventID, EventDelegate eventCallback);
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="eventID">事件id</param>
        /// <param name="eventCallback">事件回调</param>
        void Unsubscribe(uint eventID, EventDelegate eventCallback);
    }

}

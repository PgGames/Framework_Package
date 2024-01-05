using System.Collections;
using UnityEngine;

namespace PGFrammework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Debug")]
    public class DebugComponent : FrameworkComponent
    {
        public override void Init()
        {
        }

        public static bool IsStop = false;


        public void Log(string varMessage)
        {
        }
        public void Warning(string varMessage)
        {
        }
        public void Error(string varMessage)
        {
        }
    }
}
using System;
using Missions;

namespace Services
{
    public struct ActiveChain
    {
        public MissionChain Chain { get; set; }
        public int Index { get; set; }
        public Action OnCompleted { get; set; }
    }
}
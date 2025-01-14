using System;
using Base.Particles;
using Game;
using UnityEngine;

namespace Base.Events
{
    public static class EventsParticle
    {
        public static Action<ParticleType, Color, Vector3> PlayParticle;
    }
}
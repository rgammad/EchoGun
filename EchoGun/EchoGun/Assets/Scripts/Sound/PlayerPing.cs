using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerPing : MonoBehaviour {


    private class Ping {
        const float pingSpeed = 50;
        const float pingFalloffDuration = 1;

        private readonly Vector2 position;
        public Vector2 Position { get { return position; } }

        private readonly float pingTime;
        public float PingTime { get { return pingTime; } }

        private readonly float range;
        public float Range { get { return range; } }

        public float CurrentRange { get { return Mathf.Min(Range, TimeSincePing * pingSpeed); } }

        private readonly float lifetime;
        public float Lifetime { get { return lifetime; } }

        public float TimeSincePing { get { return Time.time - PingTime; } }

        public bool Expired { get { return TimeSincePing >= Lifetime; } }

        public Ping(Vector2 position, float pingTime, float range) {
            this.position = position;
            this.pingTime = pingTime;
            this.range = range;
            this.lifetime = pingFalloffDuration + (range / pingSpeed);
        }

        public Ping(Vector2 position, float range) : this(position, Time.time, range) { }

        //TODO: lifetime
    }

    static List<Ping> pings = new List<Ping>();

    /// <summary>
    /// Re-used array to be passed to the shader with all current ping positions.
    /// </summary>
    static Vector4[] pingPositions = new Vector4[Tags.ShaderParams.maxGlobalPingCount];

    /// <summary>
    /// Re-used array to be passed to the shader with all current ping lifetimes.
    /// </summary>
    static float[] timeSincePings = new float[Tags.ShaderParams.maxGlobalPingCount];

    /// <summary>
    /// Re-used array to be passed to the shader with all current ping lifetimes.
    /// </summary>
    static float[] pingRanges = new float[Tags.ShaderParams.maxGlobalPingCount];

    void Start () {
        //set arrays to max length values, and ping count to zero.
        Shader.SetGlobalFloat(Tags.ShaderParams.globalPingCount, 0);
        Shader.SetGlobalVectorArray(Tags.ShaderParams.globalPingPos, pingPositions);
        Shader.SetGlobalFloatArray(Tags.ShaderParams.globalTimeSincePing, timeSincePings);
        Shader.SetGlobalFloatArray(Tags.ShaderParams.globalPingRange, pingRanges);
    }

    public static void CreatePing(Vector2 position, float range) {
        Ping newPing = new Ping(position, range);
        pings.Add(newPing);

        //if we're over capacity

        //first remove all expired pings
        if (pings.Count > Tags.ShaderParams.maxGlobalPingCount) {
            pings.RemoveAll(ping => ping.Expired);
        }

        //then start removing the oldest non-expired pings
        while (pings.Count > Tags.ShaderParams.maxGlobalPingCount) {
            pings.RemoveAt(0);
        }

        SetShaderVariables();
    }

    private void Update() {
        SetShaderVariables();
    }

    static void SetShaderVariables () {

        //remove expired pings
        pings.RemoveAll(ping => ping.Expired);

        int index = 0;
        //update array values
        foreach(Ping ping in pings) {
            pingPositions[index] = ping.Position;
            timeSincePings[index] = ping.TimeSincePing;
            pingRanges[index] = ping.CurrentRange;
            index++;
        }

        Debug.Log(pings.Count);

        Shader.SetGlobalFloat(Tags.ShaderParams.globalPingCount, pings.Count);
        Shader.SetGlobalVectorArray(Tags.ShaderParams.globalPingPos, pingPositions);
        Shader.SetGlobalFloatArray(Tags.ShaderParams.globalTimeSincePing, timeSincePings);
        Shader.SetGlobalFloatArray(Tags.ShaderParams.globalPingRange, pingRanges);
    }
}

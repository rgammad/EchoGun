using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerPing : MonoBehaviour {

    private class Ping {
        private readonly Vector2 position;
        public Vector2 Position { get { return position; } }

        private readonly float pingTime;
        public float PingTime { get { return pingTime; } }

        public float TimeSincePing { get { return Time.time - PingTime; } }

        public Ping(Vector2 position, float pingTime) {
            this.position = position;
            this.pingTime = pingTime;
        }

        public Ping(Vector2 position) : this(position, Time.time) { }

        //TODO: lifetime
    }

    float lastPingTime = -9999;

    Queue<Ping> pings = new Queue<Ping>();

    /// <summary>
    /// Re-used array to be passed to the shader with all current ping positions.
    /// </summary>
    Vector4[] pingPositions = new Vector4[Tags.ShaderParams.maxGlobalPingCount];

    /// <summary>
    /// Re-used array to be passed to the shader with all current ping lifetimes.
    /// </summary>
    float[] timeSincePings = new float[Tags.ShaderParams.maxGlobalPingCount];

    void Start () {
        //set arrays to max length values, and ping count to zero.
        Shader.SetGlobalFloat(Tags.ShaderParams.globalPingCount, 0);
        Shader.SetGlobalVectorArray(Tags.ShaderParams.globalPingPos, pingPositions);
        Shader.SetGlobalFloatArray(Tags.ShaderParams.globalTimeSincePing, timeSincePings);
    }

    public void CreatePing(Vector2 position) {
        Ping newPing = new Ping(position);
        pings.Enqueue(newPing);
        while(pings.Count > Tags.ShaderParams.maxGlobalPingCount) {
            pings.Dequeue();
        }
        //TODO: TTL-correct data structure (probably priority queue)

        SetShaderVariables();
    }

    private void Update() {
        SetShaderVariables();
    }

    void SetShaderVariables () {
        int index = 0;

        //update array values
        foreach(Ping ping in pings) {
            pingPositions[index] = ping.Position;
            timeSincePings[index] = ping.TimeSincePing;
            index++;
        }

        Debug.Log(pings.Count);

        Shader.SetGlobalFloat(Tags.ShaderParams.globalPingCount, pings.Count);
        Shader.SetGlobalVectorArray(Tags.ShaderParams.globalPingPos, pingPositions);
        Shader.SetGlobalFloatArray(Tags.ShaderParams.globalTimeSincePing, timeSincePings);
    }
}

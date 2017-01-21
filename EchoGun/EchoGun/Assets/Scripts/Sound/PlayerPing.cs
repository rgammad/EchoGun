using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class PlayerPing : MonoBehaviour {

    float lastPingTime = -9999;

	void Start () {
        Shader.SetGlobalVector(Tags.ShaderParams.globalPingPos, Vector4.zero);
        Shader.SetGlobalFloat(Tags.ShaderParams.globalTimeSincePing, Time.time - lastPingTime);
    }

    public void Ping(Vector2 position) {
        Shader.SetGlobalVector(Tags.ShaderParams.globalPingPos, position);
        lastPingTime = Time.time;
        Shader.SetGlobalFloat(Tags.ShaderParams.globalTimeSincePing, Time.time - lastPingTime);
    }

    private void Update() {
        Shader.SetGlobalFloat(Tags.ShaderParams.globalTimeSincePing, Time.time - lastPingTime);
    }
}

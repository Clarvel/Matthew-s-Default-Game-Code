using UnityEngine;
using System.Collections;

static public class Modulo{
	// this is dumb :/
	static public int Mod(int a, int b){
		return (int)(((uint)a) % b);
	}
    static public int Mod(float a, int b) {
        return (int)(((uint)a) % b);
    }
	static public float Radial(float a){
		// -180 to +180
		return a % 180f;
	}
	static public float Circular(float a){
		// 0 to 360
		return Modulo.Mod(a, 360);
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyFlowfield : MonoBehaviour
{
	FastNoise _fastNoise;
	public Vector2Int _gridSize;
    public float _increament;
    public Vector2 _offset, _offsetSpeed;
    private Vector2[,] _flowfieldDirection;
    // Start is called before the first frame update
    void Start()
    {
        _flowfieldDirection = new Vector2[_gridSize.x, _gridSize.y];
        _fastNoise = new FastNoise();   
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateFlowfieldDirections();
    }

    void CalculateFlowfieldDirections() {
        float xOff = 0f;
        for (int x = 0; x < _gridSize.x; ++x) {
            float yOff = 0f;
            for (int y = 0; y < _gridSize.y; ++y) {
                float noise = _fastNoise.GetSimplex(xOff + _offset.x, yOff + _offset.y) + 1;
                Vector2 noiseDirection = new Vector2(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI));
                _flowfieldDirection[x, y] = noiseDirection.normalized;
                yOff += _increament; 
            }
            xOff += _increament;
        }
    }

    private void OnDrawGizmos() {
        for (int x = 0; x < _gridSize.x; ++x) {
            for (int y = 0; y < _gridSize.y; ++y) {
                Gizmos.color = Color.white;
                Vector2 pos = new Vector2(x, y) + (Vector2)transform.position;
                Vector2 endpos = pos + _flowfieldDirection[x, y];
                Gizmos.DrawLine(pos, endpos);
            }
        }
    }

    public Vector2 getDirection(FlockAgent agent) {
        int pos_x = (int)Mathf.Floor(agent.transform.position.x - transform.position.x);
        int pos_y = (int)Mathf.Floor(agent.transform.position.y - transform.position.y);
        return _flowfieldDirection[pos_x, pos_y];
    }
}

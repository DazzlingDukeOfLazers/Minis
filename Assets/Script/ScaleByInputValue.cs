using UnityEngine;
using UnityEngine.InputSystem;

sealed class ScaleByInputValue : MonoBehaviour
{
    public float _maxScale = 1.0f;
    public float _minScale = 0.01f;
    [SerializeField] Transform _transform  = null;
    [SerializeField] InputAction _ScaleXYZ = null;
    [SerializeField] InputAction _ScaleX   = null;
    [SerializeField] InputAction _ScaleY   = null;



    void OnEnable()
    {
        _ScaleXYZ.performed += ScaleXYZCallback;
        _ScaleXYZ.Enable();

        _ScaleX.performed += ScaleXCallback;
        _ScaleX.Enable();

        _ScaleY.performed += ScaleYCallback;
        _ScaleY.Enable();
    }

    void OnDisable()
    {
        _ScaleXYZ.performed -= ScaleXYZCallback;
        _ScaleXYZ.Disable();

        _ScaleX.performed -= ScaleXCallback;
        _ScaleX.Disable();

        _ScaleY.performed -= ScaleYCallback;
        _ScaleY.Disable();
    }

    void ScaleXYZCallback(InputAction.CallbackContext ctx)
    {       
        _maxScale = ctx.ReadValue<float>() * 2;
        Vector3 vLocalScale =  _transform.localScale;
                
        //float ratioXtoMax = vLocalScale.x / _maxScale;
        //Debug.Log($"vLocalScale[{vLocalScale}] and _maxScale[{_maxScale}] before transform");

        if (
                (vLocalScale.x >= vLocalScale.y ) &&
                (vLocalScale.x >= vLocalScale.z) )
                {
                    float ratioXY = vLocalScale.x / vLocalScale.y;
                    float ratioXZ = vLocalScale.x / vLocalScale.z;
                     vLocalScale.x = _maxScale;
                     vLocalScale.y = _maxScale / ratioXY;
                     vLocalScale.z = _maxScale / ratioXZ;
                     //Debug.Log($" {vLocalScale.x} X is biggest");
                }

        else if (
                (vLocalScale.y >= vLocalScale.x ) &&
                (vLocalScale.y >= vLocalScale.z) )
                {
                    float ratioYX = vLocalScale.y / vLocalScale.x;
                    float ratioYZ = vLocalScale.y / vLocalScale.z;
                     vLocalScale.y = _maxScale;
                     vLocalScale.x = _maxScale / ratioYX;
                     vLocalScale.z = _maxScale / ratioYZ;
                     //Debug.Log($" {vLocalScale.y} Y is biggest");
                }
        
        else if (
                (vLocalScale.z >= vLocalScale.x ) &&
                (vLocalScale.z >= vLocalScale.y) )
                {
                    float ratioZX = vLocalScale.z / vLocalScale.x;
                    float ratioZY = vLocalScale.z / vLocalScale.y;
                     vLocalScale.z = _maxScale;
                     vLocalScale.x = _maxScale * ratioZX;
                     vLocalScale.y = _maxScale * ratioZY;
                     //Debug.Log($" {vLocalScale.z} Z is biggest");
                }
        
        
        //Debug.Log($"vLocalScale[{vLocalScale}] and _maxScale[{_maxScale}] after transform");
        _transform.localScale = vLocalScale;      
        
       
    }

    void ScaleXCallback(InputAction.CallbackContext ctx)
    {
        //Vector3 vLocalScale =  Vector3.one;
        Vector3 vLocalScale =  _transform.localScale;
        vLocalScale.x =  Mathf.Max(_minScale, ctx.ReadValue<float>() * _maxScale);

        // Scale = Action * x * Max
        _transform.localScale = vLocalScale;
    }

    void ScaleYCallback(InputAction.CallbackContext ctx)
    {
        //Vector3 vLocalScale =  Vector3.one;
        Vector3 vLocalScale =  _transform.localScale;
        vLocalScale.y = Mathf.Max(_minScale, ctx.ReadValue<float>() * _maxScale);

        // Scale = Action * y * Max
        _transform.localScale = vLocalScale;
    }

}

        // Vector3 vScale = _transform.localScale;
        // vScale.y = ctx.ReadValue<float>(); // TODO add scaling
        // _transform.localScale =  vScale;
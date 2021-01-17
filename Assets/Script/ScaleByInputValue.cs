using UnityEngine;
using UnityEngine.InputSystem;

sealed class ScaleByInputValue : MonoBehaviour
{
    public float _maxScale = 1.0f;
    public float _minScale = 0.1f;
    [SerializeField] Transform _transform  = null;
    [SerializeField] InputAction _ScaleXYZ = null;
    [SerializeField] InputAction _ScaleX   = null;


    void OnEnable()
    {
        _ScaleXYZ.performed += ScaleXYZCallback;
        _ScaleXYZ.Enable();

        _ScaleX.performed += ScaleXCallback;
        _ScaleX.Enable();
    }

    void OnDisable()
    {
        _ScaleXYZ.performed -= ScaleXYZCallback;
        _ScaleXYZ.Disable();

        _ScaleXYZ.performed -= ScaleXCallback;
        _ScaleX.Disable();
    }

    void ScaleXYZCallback(InputAction.CallbackContext ctx)
    {
        float fScaleVal = ctx.ReadValue<float>() * _maxScale; 
        //Vector3 vLocalScale = _transform.localScale;     

        Vector3 vLocalScale =  _transform.localScale;
        //vLocalScale.x = fScaleVal;
        vLocalScale.y = fScaleVal;
        vLocalScale.z = fScaleVal; 
     
        // Scale = Action * self * Max
        _transform.localScale = vLocalScale;

    }

    void ScaleXCallback(InputAction.CallbackContext ctx)
    {
        //Vector3 vLocalScale =  Vector3.one;
        Vector3 vLocalScale =  _transform.localScale;
        vLocalScale.x = ctx.ReadValue<float>() * _maxScale;

        // Scale = Action * x * Max
        _transform.localScale = vLocalScale;
    }
}

        // Vector3 vScale = _transform.localScale;
        // vScale.y = ctx.ReadValue<float>(); // TODO add scaling
        // _transform.localScale =  vScale;
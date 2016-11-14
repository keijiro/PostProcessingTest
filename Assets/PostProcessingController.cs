using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] Transform _focusPoint;

    public Transform focusPoint {
        get { return _focusPoint; }
        set { _focusPoint = value; }
    }

    Camera _camera;
    PostProcessingProfile _profile;
    DepthOfFieldModel.Settings _dof;

    void OnEnable()
    {
        var target = GetComponent<PostProcessingBehaviour>();

        _camera = GetComponent<Camera>();
        _profile = Instantiate<PostProcessingProfile>(target.profile);
        _dof = _profile.depthOfField.settings;

        target.profile = _profile;
    }

    void Update()
    {
        bool dofUpdate = false;

        if (_focusPoint != null)
        {
            var cam = _camera.transform;
            _dof.focusDistance = Vector3.Dot(_focusPoint.position - cam.position, cam.forward);
            dofUpdate = true;
        }

        if (dofUpdate) _profile.depthOfField.settings = _dof;
    }
}

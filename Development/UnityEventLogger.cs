using UnityEngine;

namespace Utilities.Development
{
    /// <summary>
    /// Logs all Unity event methods called on this GameObject for debugging purposes.
    /// Attach this component to any GameObject to monitor its lifecycle events.
    /// </summary>
    public class UnityEventLogger : MonoBehaviour
    {
        private string objectInfo => $"{name}/{GetInstanceID()}";


        #region Initialization Events
        private void Awake()
        {
            Debug.Log($"{objectInfo}: Awake", this);
        }

        private void OnEnable()
        {
            Debug.Log($"{objectInfo}: OnEnable", this);
        }

        private void Start()
        {
            Debug.Log($"{objectInfo}: Start", this);
        }
        #endregion


        #region Update Events
        private void FixedUpdate()
        {
            Debug.Log($"{objectInfo}: FixedUpdate", this);
        }

        private void Update()
        {
            Debug.Log($"{objectInfo}: Update", this);
        }

        private void LateUpdate()
        {
            Debug.Log($"{objectInfo}: LateUpdate", this);
        }

        #endregion


        #region Physics Events (3D)
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"{objectInfo}: OnCollisionEnter - {collision.gameObject.name}", this);
        }

        private void OnCollisionStay(Collision collision)
        {
            Debug.Log($"{objectInfo}: OnCollisionStay - {collision.gameObject.name}", this);
        }

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log($"{objectInfo}: OnCollisionExit - {collision.gameObject.name}", this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{objectInfo}: OnTriggerEnter - {other.gameObject.name}", this);
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log($"{objectInfo}: OnTriggerStay - {other.gameObject.name}", this);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"{objectInfo}: OnTriggerExit - {other.gameObject.name}", this);
        }
        #endregion


        #region Physics Events (2D)
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log($"{objectInfo}: OnCollisionEnter2D - {collision.gameObject.name}", this);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            Debug.Log($"{objectInfo}: OnCollisionStay2D - {collision.gameObject.name}", this);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Debug.Log($"{objectInfo}: OnCollisionExit2D - {collision.gameObject.name}", this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"{objectInfo}: OnTriggerEnter2D - {other.gameObject.name}", this);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log($"{objectInfo}: OnTriggerStay2D - {other.gameObject.name}", this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log($"{objectInfo}: OnTriggerExit2D - {other.gameObject.name}", this);
        }

        #endregion


        #region Mouse Events
        private void OnMouseEnter()
        {
            Debug.Log($"{objectInfo}: OnMouseEnter", this);
        }

        private void OnMouseOver()
        {
            Debug.Log($"{objectInfo}: OnMouseOver", this);
        }

        private void OnMouseExit()
        {
            Debug.Log($"{objectInfo}: OnMouseExit", this);
        }

        private void OnMouseDown()
        {
            Debug.Log($"{objectInfo}: OnMouseDown", this);
        }

        private void OnMouseUp()
        {
            Debug.Log($"{objectInfo}: OnMouseUp", this);
        }

        private void OnMouseUpAsButton()
        {
            Debug.Log($"{objectInfo}: OnMouseUpAsButton", this);
        }

        private void OnMouseDrag()
        {
            Debug.Log($"{objectInfo}: OnMouseDrag", this);
        }
        #endregion


        #region Rendering Events
        private void OnBecameVisible()
        {
            Debug.Log($"{objectInfo}: OnBecameVisible", this);
        }

        private void OnBecameInvisible()
        {
            Debug.Log($"{objectInfo}: OnBecameInvisible", this);
        }

        private void OnPreCull()
        {
            Debug.Log($"{objectInfo}: OnPreCull", this);
        }

        private void OnPreRender()
        {
            Debug.Log($"{objectInfo}: OnPreRender", this);
        }

        private void OnRenderObject()
        {
            Debug.Log($"{objectInfo}: OnRenderObject", this);
        }

        private void OnPostRender()
        {
            Debug.Log($"{objectInfo}: OnPostRender", this);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Debug.Log($"{objectInfo}: OnRenderImage", this);
            Graphics.Blit(source, destination);
        }

        private void OnWillRenderObject()
        {
            Debug.Log($"{objectInfo}: OnWillRenderObject", this);
        }

        private void OnDrawGizmos()
        {
            Debug.Log($"{objectInfo}: OnDrawGizmos", this);
        }

        private void OnDrawGizmosSelected()
        {
            Debug.Log($"{objectInfo}: OnDrawGizmosSelected", this);
        }
        #endregion


        #region GUI Events
        private void OnGUI()
        {
            Debug.Log($"{objectInfo}: OnGUI", this);
        }

        #endregion


        #region Application Events
        private void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log($"{objectInfo}: OnApplicationFocus - {hasFocus}", this);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Debug.Log($"{objectInfo}: OnApplicationPause - {pauseStatus}", this);
        }

        private void OnApplicationQuit()
        {
            Debug.Log($"{objectInfo}: OnApplicationQuit", this);
        }
        #endregion


        #region Transform Events
        private void OnTransformParentChanged()
        {
            Debug.Log($"{objectInfo}: OnTransformParentChanged", this);
        }

        private void OnTransformChildrenChanged()
        {
            Debug.Log($"{objectInfo}: OnTransformChildrenChanged", this);
        }
        #endregion


        #region Animation Events
        private void OnAnimatorMove()
        {
            Debug.Log($"{objectInfo}: OnAnimatorMove", this);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Debug.Log($"{objectInfo}: OnAnimatorIK - Layer {layerIndex}", this);
        }
        #endregion


        #region Audio Events
        private void OnAudioFilterRead(float[] data, int channels)
        {
            Debug.Log($"{objectInfo}: OnAudioFilterRead - Channels: {channels}", this);
        }

        #endregion


        #region Canvas Events
        private void OnCanvasGroupChanged()
        {
            Debug.Log($"{objectInfo}: OnCanvasGroupChanged", this);
        }

        private void OnRectTransformDimensionsChange()
        {
            Debug.Log($"{objectInfo}: OnRectTransformDimensionsChange", this);
        }
        #endregion


        #region Validation Events
        private void OnValidate()
        {
            Debug.Log($"{objectInfo}: OnValidate", this);
        }

        private void Reset()
        {
            Debug.Log($"{objectInfo}: Reset", this);
        }
        #endregion


        #region Particle Events
        private void OnParticleCollision(GameObject other)
        {
            Debug.Log($"{objectInfo}: OnParticleCollision - {other.name}", this);
        }

        private void OnParticleTrigger()
        {
            Debug.Log($"{objectInfo}: OnParticleTrigger", this);
        }
        #endregion


        #region Joint Events
        private void OnJointBreak(float breakForce)
        {
            Debug.Log($"{objectInfo}: OnJointBreak - Force: {breakForce}", this);
        }

        private void OnJointBreak2D(Joint2D brokenJoint)
        {
            Debug.Log($"{objectInfo}: OnJointBreak2D - {brokenJoint.name}", this);
        }
        #endregion


        #region Controller Events
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Debug.Log($"{objectInfo}: OnControllerColliderHit - {hit.gameObject.name}", this);
        }
        #endregion


        #region Server/Network Events (Legacy)
        private void OnConnectedToServer()
        {
            Debug.Log($"{objectInfo}: OnConnectedToServer", this);
        }

        private void OnDisconnectedFromServer()
        {
            Debug.Log($"{objectInfo}: OnDisconnectedFromServer", this);
        }

        private void OnPlayerConnected()
        {
            Debug.Log($"{objectInfo}: OnPlayerConnected", this);
        }

        private void OnPlayerDisconnected()
        {
            Debug.Log($"{objectInfo}: OnPlayerDisconnected", this);
        }

        private void OnServerInitialized()
        {
            Debug.Log($"{objectInfo}: OnServerInitialized", this);
        }
        #endregion


        #region Destruction Events
        private void OnDisable()
        {
            Debug.Log($"{objectInfo}: OnDisable", this);
        }

        private void OnDestroy()
        {
            Debug.Log($"{objectInfo}: OnDestroy", this);
        }
        #endregion
    }
}

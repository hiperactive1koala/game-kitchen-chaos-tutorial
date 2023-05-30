using System;
using UnityEngine;
// ReSharper disable PossibleNullReferenceException


public class LookAtCamera : MonoBehaviour
{
      private enum Mode { LookAt, LookAtInverted, CameraForward, CameraForwardInverted }

      [SerializeField] private Mode _mode;

      private void LateUpdate()
      {
            switch (_mode)
            {
                  case Mode.LookAt:
                        transform.LookAt(Camera.main.transform);
                        break;
                  case Mode.LookAtInverted:
                        var position = transform.position;
                        Vector3 dirFromCamera = position - Camera.main.transform.position;
                        transform.LookAt(position + dirFromCamera);
                        break;
                  case Mode.CameraForward:
                        transform.forward = Camera.main.transform.forward;
                        break;
                  case Mode.CameraForwardInverted:
                        transform.forward = -Camera.main.transform.forward;
                        break;
                  default:
                        throw new ArgumentOutOfRangeException();
            }
      }
}

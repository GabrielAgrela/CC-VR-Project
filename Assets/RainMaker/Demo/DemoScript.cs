using UnityEngine;
using System.Collections;

namespace DigitalRuby.RainMaker
{
    public class DemoScript : MonoBehaviour
    {
        public RainScript RainScript;
        public UnityEngine.UI.Toggle MouseLookToggle;
        public UnityEngine.UI.Toggle FlashlightToggle;
        public UnityEngine.UI.Slider RainSlider;
        public Light Flashlight;
        public GameObject Sun;

        private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        private RotationAxes axes = RotationAxes.MouseXAndY;
        private float sensitivityX = 15F;
        private float sensitivityY = 15F;
        private float minimumX = -360F;
        private float maximumX = 360F;
        private float minimumY = -60F;
        private float maximumY = 60F;
        private float rotationX = 0F;
        private float rotationY = 0F;
        private Quaternion originalRotation;

        private void UpdateRain()
        {
            if (RainScript != null)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    RainScript.RainIntensity = 0.0f;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    RainScript.RainIntensity = 0.2f;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    RainScript.RainIntensity = 0.5f;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    RainScript.RainIntensity = 0.8f;
                }
            }
        }

        public void RainSliderChanged(float val)
        {
            RainScript.RainIntensity = val;
        }

        public void MouseLookChanged(bool val)
        {
            MouseLookToggle.isOn = val;
        }

        public void FlashlightChanged(bool val)
        {
            FlashlightToggle.isOn = val;
            Flashlight.enabled = val;
        }

        public void DawnDuskSliderChanged(float val)
        {
            Sun.transform.rotation = Quaternion.Euler(val, 0.0f, 0.0f);
        }

        // Use this for initialization
        private void Start()
        {
            originalRotation = transform.localRotation;
            RainScript.RainIntensity = RainSlider.value = 0.5f;
            RainScript.EnableWind = true;
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateRain();
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }

            return Mathf.Clamp(angle, min, max);
        }
    }
}
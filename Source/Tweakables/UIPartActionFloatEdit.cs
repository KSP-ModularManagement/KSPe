﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Globalization;

namespace KSPAPIExtensions
{

    [UI_FloatEdit]
    public class UIPartActionFloatEdit : UIPartActionFieldItem
    {
        public SpriteText fieldName;
        public SpriteText fieldValue;
        public UIButton incLargeDown;
        public SpriteText incLargeDownLabel;
        public UIButton incSmallDown;
        public SpriteText incSmallDownLabel;
        public UIButton incSmallUp;
        public SpriteText incSmallUpLabel;
        public UIButton incLargeUp;
        public SpriteText incLargeUpLabel;
        public UIProgressSlider slider;

        private float value;
        private uint controlState;

        public static UIPartActionFloatEdit CreateTemplate()
        {
            // Create the control
            GameObject editGo = new GameObject("UIPartActionFloatEdit", typeof(UIPartActionFloatEdit));
            UIPartActionFloatEdit edit = editGo.GetComponent<UIPartActionFloatEdit>();
            editGo.SetActive(false);

            // TODO: since I don'type have access to EZE GUI, I'm copying out bits from other existing GUIs 
            // if someone does have access, they could do this better although really it works pretty well.
            UIPartActionButton evtp = UIPartActionController.Instance.eventItemPrefab;
            GameObject srcTextGo = evtp.transform.Find("Text").gameObject;
            GameObject srcBackgroundGo = evtp.transform.Find("Background").gameObject;
            GameObject srcButtonGo = evtp.transform.Find("Btn").gameObject;

            UIPartActionFloatRange paFlt = (UIPartActionFloatRange)UIPartActionController.Instance.fieldPrefabs.Find(cls => cls.GetType() == typeof(UIPartActionFloatRange));
            GameObject srcSliderGo = paFlt.transform.Find("Slider").gameObject;


            // Start building our control
            GameObject backgroundGo = (GameObject)Instantiate(srcBackgroundGo);
            backgroundGo.transform.parent = editGo.transform;

            GameObject sliderGo = (GameObject)Instantiate(srcSliderGo);
            sliderGo.transform.parent = editGo.transform;
            sliderGo.transform.localScale = new Vector3(0.65f, 1, 1);
            edit.slider = sliderGo.GetComponent<UIProgressSlider>();
            edit.slider.ignoreDefault = true;


            GameObject fieldNameGo = (GameObject)Instantiate(srcTextGo);
            fieldNameGo.transform.parent = editGo.transform;
            fieldNameGo.transform.localPosition = new Vector3(40, -8, 0);
            edit.fieldName = fieldNameGo.GetComponent<SpriteText>();

            GameObject fieldValueGo = (GameObject)Instantiate(srcTextGo);
            fieldValueGo.transform.parent = editGo.transform;
            fieldValueGo.transform.localPosition = new Vector3(110, -8, 0);
            edit.fieldValue = fieldValueGo.GetComponent<SpriteText>();


            GameObject incLargeDownGo = (GameObject)Instantiate(srcButtonGo);
            incLargeDownGo.transform.parent = edit.transform;
            incLargeDownGo.transform.localScale = new Vector3(0.45f, 1.1f, 1f);
            incLargeDownGo.transform.localPosition = new Vector3(11.5f, -9, 0); //>11
            edit.incLargeDown = incLargeDownGo.GetComponent<UIButton>();

            GameObject incLargeDownLabelGo = (GameObject)Instantiate(srcTextGo);
            incLargeDownLabelGo.transform.parent = editGo.transform;
            incLargeDownLabelGo.transform.localPosition = new Vector3(5.5f, -7, 0); // <6
            edit.incLargeDownLabel = incLargeDownLabelGo.GetComponent<SpriteText>();
            edit.incLargeDownLabel.Text = "<<";


            GameObject incSmallDownGo = (GameObject)Instantiate(srcButtonGo);
            incSmallDownGo.transform.parent = edit.transform;
            incSmallDownGo.transform.localScale = new Vector3(0.35f, 1.1f, 1f);
            incSmallDownGo.transform.localPosition = new Vector3(29, -9, 0); // <31.5
            edit.incSmallDown = incSmallDownGo.GetComponent<UIButton>();

            GameObject incSmallDownLabelGo = (GameObject)Instantiate(srcTextGo);
            incSmallDownLabelGo.transform.parent = editGo.transform;
            incSmallDownLabelGo.transform.localPosition = new Vector3(25.5f, -7, 0); //<28
            edit.incSmallDownLabel = incSmallDownLabelGo.GetComponent<SpriteText>();
            edit.incSmallDownLabel.Text = "<";

            GameObject incSmallUpGo = (GameObject)Instantiate(srcButtonGo);
            incSmallUpGo.transform.parent = edit.transform;
            incSmallUpGo.transform.localScale = new Vector3(0.35f, 1.1f, 1f);
            incSmallUpGo.transform.localPosition = new Vector3(170, -9, 0);
            edit.incSmallUp = incSmallUpGo.GetComponent<UIButton>();

            GameObject incSmallUpLabelGo = (GameObject)Instantiate(srcTextGo);
            incSmallUpLabelGo.transform.parent = editGo.transform;
            incSmallUpLabelGo.transform.localPosition = new Vector3(167.5f, -7, 0); //<168
            edit.incSmallUpLabel = incSmallUpLabelGo.GetComponent<SpriteText>();
            edit.incSmallUpLabel.Text = ">";

            GameObject incLargeUpGo = (GameObject)Instantiate(srcButtonGo);
            incLargeUpGo.transform.parent = edit.transform;
            incLargeUpGo.transform.localScale = new Vector3(0.45f, 1.1f, 1f);
            incLargeUpGo.transform.localPosition = new Vector3(187.5f, -9, 0); // >187
            edit.incLargeUp = incLargeUpGo.GetComponent<UIButton>();

            GameObject incLargeUpLabelGo = (GameObject)Instantiate(srcTextGo);
            incLargeUpLabelGo.transform.parent = editGo.transform;
            incLargeUpLabelGo.transform.localPosition = new Vector3(181.5f, -7, 0); //<182
            edit.incLargeUpLabel = incLargeUpLabelGo.GetComponent<SpriteText>();
            edit.incLargeUpLabel.Text = ">>";
            return edit;
        }


        protected UI_FloatEdit fieldInfo
        {
            get
            {
                return (UI_FloatEdit)control;
            }
        }

        public override void Setup(UIPartActionWindow window, Part part, PartModule partModule, UI_Scene scene, UI_Control control, BaseField field)
        {
            base.Setup(window, part, partModule, scene, control, field);
            incLargeDown.SetValueChangedDelegate(obj => buttons_ValueChanged(false, true));
            incSmallDown.SetValueChangedDelegate(obj => buttons_ValueChanged(false, false));
            incSmallUp.SetValueChangedDelegate(obj => buttons_ValueChanged(true, false));
            incLargeUp.SetValueChangedDelegate(obj => buttons_ValueChanged(true, true));
            slider.SetValueChangedDelegate(slider_OnValueChanged);

            // so update runs.
            this.value = GetFieldValue() + 0.1f;
            UpdateFieldInfo();
        }

        private void buttons_ValueChanged(bool up, bool large)
        {
            float increment = (large ? fieldInfo.incrementLarge : fieldInfo.incrementSmall);
            float excess = value % increment;
            float newValue;
            if (up)
            {
                if (increment - excess < fieldInfo.incrementSlide / 2)
                    newValue = value - excess + increment * 2;
                else
                    newValue = value - excess + increment;
            }
            else
            {
                if (excess < fieldInfo.incrementSlide / 2)
                    newValue = value - excess - increment;
                else
                    newValue = value - excess;
            }
            SetValueFromGUI(newValue);
        }

        private void slider_OnValueChanged(IUIObject obj)
        {
            float valueLow, valueHi;
            SliderRange(value, out valueLow, out valueHi);

            float newValue = Mathf.Lerp(valueLow, valueHi, slider.Value);

            if (fieldInfo.incrementLarge == 0 || valueHi == fieldInfo.maxValue) 
            {
                if (newValue > valueHi)
                    newValue = valueHi;
            }
            else if (newValue > valueHi - fieldInfo.incrementSlide)
                newValue = valueHi - fieldInfo.incrementSlide;

            SetValueFromGUI(newValue);
        }

        private void SetValueFromGUI(float newValue)
        {
            if (fieldInfo.incrementSlide != 0)
                newValue = Mathf.Round(newValue / fieldInfo.incrementSlide) * fieldInfo.incrementSlide;

            if (newValue < fieldInfo.minValue)
                newValue = fieldInfo.minValue;
            else if (newValue > fieldInfo.maxValue)
                newValue = fieldInfo.maxValue;

            UpdateValueDisplay(newValue);

            field.SetValue(newValue, field.host);
            if (scene == UI_Scene.Editor)
                SetSymCounterpartValue(newValue);
        }

        private void SliderRange(float value, out float valueLow, out float valueHi)
        {
            if (fieldInfo.incrementLarge == 0)
            {
                valueLow = fieldInfo.minValue;
                valueHi = fieldInfo.maxValue;
                return;
            }

            if (fieldInfo.incrementSmall == 0)
            {
                valueLow = Mathf.Floor((value + fieldInfo.incrementSlide / 2f) / fieldInfo.incrementLarge) * fieldInfo.incrementLarge;
                valueHi = valueLow + fieldInfo.incrementLarge;
                if (valueLow == fieldInfo.maxValue)
                {
                    valueHi = valueLow;
                    valueLow -= fieldInfo.incrementLarge;
                }
            }
            else
            {
                valueLow = Mathf.Floor((value + fieldInfo.incrementSlide / 2f) / fieldInfo.incrementSmall) * fieldInfo.incrementSmall;
                valueHi = valueLow + fieldInfo.incrementSmall;
                if (valueLow == fieldInfo.maxValue)
                {
                    valueHi = valueLow;
                    valueLow -= fieldInfo.incrementSmall;
                }
            }
        }

        private float GetFieldValue()
        {
            if (isModule)
            {
                return field.GetValue<float>(this.partModule);
            }
            return field.GetValue<float>(this.part);
        }

        public override void UpdateItem()
        {
            // update from fieldName. No listeners.
            fieldName.Text = field.guiName;

            // Update the value.
            float value = GetFieldValue();
            if (value != this.value)
                UpdateValueDisplay(value);

            uint newHash = fieldInfo.GetHashedState();
            if (controlState != newHash)
            {
                UpdateFieldInfo();
                controlState = newHash;
            }
        }

        private void UpdateValueDisplay(float value)
        {
            this.value = value;
            if (fieldInfo.incrementSlide != 0)
                value = Mathf.Round(value / fieldInfo.incrementSlide) * fieldInfo.incrementSlide;

            float valueLow, valueHi;
            SliderRange(value, out valueLow, out valueHi);
            slider.Value = Mathf.InverseLerp(valueLow, valueHi, value);

            fieldValue.Text = value.ToStringExt(field.guiFormat) + field.guiUnits;
        }

        private void UpdateFieldInfo()
        {
            if (fieldInfo.incrementLarge == 0.0)
            {
                incLargeDown.gameObject.SetActive(false);
                incLargeDownLabel.gameObject.SetActive(false);
                incLargeUp.gameObject.SetActive(false);
                incLargeUpLabel.gameObject.SetActive(false);

                incSmallDown.gameObject.SetActive(false);
                incSmallDownLabel.gameObject.SetActive(false);
                incSmallUp.gameObject.SetActive(false);
                incSmallUpLabel.gameObject.SetActive(false);

                slider.transform.localScale = Vector3.one;
                fieldName.transform.localPosition = new Vector3(6, -8, 0);
            }
            else if (fieldInfo.incrementSmall == 0.0)
            {
                incLargeDown.gameObject.SetActive(true);
                incLargeDownLabel.gameObject.SetActive(true);
                incLargeUp.gameObject.SetActive(true);
                incLargeUpLabel.gameObject.SetActive(true);

                incSmallDown.gameObject.SetActive(false);
                incSmallDownLabel.gameObject.SetActive(false);
                incSmallUp.gameObject.SetActive(false);
                incSmallUpLabel.gameObject.SetActive(false);

                slider.transform.localScale = new Vector3(0.81f, 1, 1);
                fieldName.transform.localPosition = new Vector3(24, -8, 0); //>23
            }
            else
            {
                incLargeDown.gameObject.SetActive(true);
                incLargeDownLabel.gameObject.SetActive(true);
                incLargeUp.gameObject.SetActive(true);
                incLargeUpLabel.gameObject.SetActive(true);

                incSmallDown.gameObject.SetActive(true);
                incSmallDownLabel.gameObject.SetActive(true);
                incSmallUp.gameObject.SetActive(true);
                incSmallUpLabel.gameObject.SetActive(true);

                slider.transform.localScale = new Vector3(0.64f, 1, 1);
                fieldName.transform.localPosition = new Vector3(40, -8, 0);
            }

            if (fieldInfo.incrementSlide == 0)
                slider.gameObject.SetActive(false);
            else
                slider.gameObject.SetActive(true);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class UI_FloatEdit : UI_Control
    {
        private static string UIControlName = "UIPartActionFloatEdit";

        public float minValue = float.NegativeInfinity;
        public float maxValue = float.PositiveInfinity;

        public float incrementLarge = 0;
        public float incrementSmall = 0;
        public float incrementSlide = 0;

        public override void Load(ConfigNode node, object host)
        {
            base.Load(node, host);
            if (!ParseFloat(out minValue, node, "minValue", UIControlName, null))
            {
                minValue = float.NegativeInfinity;
            }
            if (!ParseFloat(out maxValue, node, "maxValue", UIControlName, null))
            {
                maxValue = float.PositiveInfinity;
            }
            if (!ParseFloat(out incrementLarge, node, "incrementLarge", UIControlName, null))
            {
                incrementLarge = 0.0f;
            }
            if (!ParseFloat(out incrementSmall, node, "incrementSmall", UIControlName, null))
            {
                incrementSmall = 0.0f;
            }
            if (!ParseFloat(out incrementSlide, node, "incrementSlide", UIControlName, null))
            {
                incrementSlide = 0.0f;
            }
        }

        public override void Save(ConfigNode node, object host)
        {
            base.Save(node, host);
            if (minValue != float.NegativeInfinity)
                node.AddValue("minValue", minValue.ToString());
            if (maxValue != float.PositiveInfinity)
                node.AddValue("maxValue", maxValue.ToString());
            if (incrementLarge != 0.0f)
                node.AddValue("incrementLarge", incrementLarge.ToString());
            if (incrementSmall != 0.0f)
                node.AddValue("incrementSmall", incrementSmall.ToString());
            if (incrementSlide != 0.0f)
                node.AddValue("incrementSlide", incrementSlide.ToString());
        }

        internal unsafe uint GetHashedState()
        {
            fixed (float* minValue = &this.minValue, maxValue = &this.maxValue, incrementLarge = &this.incrementLarge, incrementSmall = &this.incrementSmall, incrementSlide = &this.incrementSlide)
            {
                return *((uint*)minValue) ^ *((uint*)maxValue) ^ *((uint*)incrementLarge) ^ *((uint*)incrementSmall) ^ *((uint*)incrementSlide);
            }
        }
    }
}
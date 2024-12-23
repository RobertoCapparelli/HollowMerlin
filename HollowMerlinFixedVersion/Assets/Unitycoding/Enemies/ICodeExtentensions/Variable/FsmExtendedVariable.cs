using UnityEngine;
using System.Collections;
using System;

namespace ICode
{
    [System.Serializable]
    public class FsmExtendedVariable : FsmVariable
    {

        #region Field

        [SerializeField]
        private string variableName;
        public string VariableName
        {
            get { return variableName; }
        }
        [SerializeField]
        private ExtendedVariableType type;
        [SerializeField]
        public ExtendedVariableType Type
        {
            get { return type; }
        }

        public override Type VariableType
        {
            get { return typeof(Type); }
        }

    [SerializeField]
        private bool boolValue;
        [SerializeField]
        private float floatValue;
        [SerializeField]
        private GameObject gameObjectValue;
        [SerializeField]
        private int intValue;
        [SerializeField]
        private uint uIntValue;
        [SerializeField]
        private string stringValue;
        [SerializeField]
        private Vector2 vector2Value;
        [SerializeField]
        private Vector3 vector3Value;
        #endregion

        #region Methods

        public FsmExtendedVariable(string variableName, ExtendedVariableType type, object value)
        {
            this.variableName = variableName;
            this.type = type;
            SetFsmValue(value);
        }

        public object GetFsmValue()
        {
            switch (type)
            {
                case ExtendedVariableType.Bool:
                    return boolValue;
                case ExtendedVariableType.Float:
                    return floatValue;
                case ExtendedVariableType.GameObject:
                    return gameObjectValue;
                case ExtendedVariableType.Int:
                    return intValue;
                case ExtendedVariableType.String:
                    return stringValue;
                case ExtendedVariableType.Vector2:
                    return vector2Value;
                case ExtendedVariableType.Vector3:
                    return vector3Value;
                case ExtendedVariableType.UInt:
                    return uIntValue;
                default:
                    return null;
            }
        }



        private void SetFsmValue(object value)
        {
            switch (type)
            {
                case ExtendedVariableType.Bool:
                    boolValue = (bool)value;
                    break;
                case ExtendedVariableType.Float:
                    floatValue = (float)value;
                    break;
                case ExtendedVariableType.GameObject:
                    gameObjectValue = (GameObject)value;
                    break;
                case ExtendedVariableType.Int:
                    intValue = (int)value;
                    break;
                case ExtendedVariableType.String:
                    stringValue = (string)value;
                    break;
                case ExtendedVariableType.Vector2:
                    vector2Value = (Vector2)value;
                    break;
                case ExtendedVariableType.Vector3:
                    vector3Value = (Vector3)value;
                    break;
                case ExtendedVariableType.UInt:
                    uIntValue = (uint)value;
                    break;
            }
        }


        #endregion

        public static bool StrictEquals(ExtendedVariable a, ExtendedVariable b)
        {
            if (a.Type != b.Type) return false;
            return a.GetValue().Equals(b.GetValue());
        }

        public override void SetValue(object value)
        {
            SetFsmValue(value);
            if (m_OnVariableChange != null)
            {
                m_OnVariableChange.Invoke(GetFsmValue());
            }
        }

        public override object GetValue()
        {
            return GetFsmValue();
        }
    }
    }
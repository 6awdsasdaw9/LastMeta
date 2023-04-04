using System;
using Code.Logic.Interactive;
using UnityEditor.VersionControl;

namespace Code.UI.Windows
{
    [Serializable]
    public class TypedInteractiveObjectWindow
    {
        public InteractiveObjectType Type;
        public InteractiveObjectWindow InteractiveObjectWindow;
    }
}
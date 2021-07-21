using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class BaseDisplay : MonoBehaviour
    {
        protected Text uiText;
        protected virtual void Awake()
        {
            uiText = GetComponent<Text>();
        }
    }
}

using System;
using MainGame.ScriptableConfigs;
using UnityEditor;
using UnityEngine;

namespace MainGame
{
    [CustomEditor(typeof(SpellConfigListSO))]
    public class SpellConfigListSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SpellConfigListSO spellConfigListSo = (SpellConfigListSO)target;

            if (GUILayout.Button("Collect Spell Configs"))
            {
                spellConfigListSo.SpellConfigs.Clear();
                Type type = typeof(SpellConfigSO);  
                
                string[] guids = AssetDatabase.FindAssets("t:" + type.Name);

                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    SpellConfigSO instance = AssetDatabase.LoadAssetAtPath<SpellConfigSO>(path);
                    if (instance != null)
                    {
                        spellConfigListSo.SpellConfigs.Add(instance);
                    }
                }
            }
        }
    }
}

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        public class SubWaveBuilder : OdinEditorWindow
        {

            private static Texture2D tex;

            WaveScriptable toBuild;
            bool showRandomPanel = false;
            bool showPanel = false;

            [MenuItem("Tools/Sub Wave Builder")]
            private static void OpenWindow()
            {
                var window = GetWindow<SubWaveBuilder>();

                tex = (Texture2D)EditorGUIUtility.Load("PlayArea.png");

                window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
            }
            [PropertyOrder(-10)]
            [ButtonGroup]
            [Button(ButtonSizes.Large)]
            public void CreateRandomSubwave()
            {
                toBuild = CreateInstance("RandomWaveScriptable") as WaveScriptable;
                showRandomPanel = true;
                showPanel = false;
                randompanel.Reset();
            }
            [PropertyOrder(-9)]
            [ButtonGroup]
            [Button(ButtonSizes.Large)]
            public void CreateDeterministicSubwave() {
                toBuild = CreateInstance("WaveScriptable") as WaveScriptable;
                showPanel = true;
                showRandomPanel = false;
                panel.Reset();
            }
            [PropertyOrder(-8)]
            [InfoBox("To get started, hit one of the buttons to create a new wave. Otherwise, select an existing subwave to edit it.")]
            [ShowInInspector]
            public WaveScriptable WaveToEdit {
                set
                {
                    if (value != null)
                    {
                        toBuild = value;
                        if (toBuild.GetType() == typeof(RandomWaveScriptable))
                        {
                            RandomWaveScriptable w = (RandomWaveScriptable)toBuild;
                            showPanel = false;
                            showRandomPanel = true;
                            randompanel.enemies = w.fishCount;
                            randompanel.name = w.name;
                            randompanel.speedMultiplier = w.speedMultiplier;
                            randompanel.timeBetweenEnemies = w.timeBetweenFish;
                            randompanel.enemyTypes = w.fishInWave;
                        }
                        else
                        {
                            showPanel = true;
                            showRandomPanel = false;
                            panel.name = toBuild.name;
                            panel.speedMultiplier = toBuild.speedMultiplier;
                            panel.timeBetweenEnemies = toBuild.timeBetweenFish;
                            panel.enemyTypes = toBuild.fishInWave;
                        }
                    }
                    else
                    {
                        toBuild = null;
                        showPanel = false;
                        showRandomPanel = false;
                    }
                }
                get
                {
                    if(toBuild != null)
                    {
                        return toBuild;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            [ShowIf("showPanel")]
            [HideLabel]
            [BoxGroup(GroupName = "Wave Attributes")]
            public SubWavePanel panel;
            [ShowIf("showRandomPanel")]
            [HideLabel]
            [BoxGroup(GroupName = "Wave Attributes")]
            public RandomSubWavePanel randompanel;

            [Serializable]
            public class SubWavePanel
            {
                [InfoBox("You are now building a deterministic sub wave. You must manually lay out each of the enemy locations.")]
                public string name;
                public float speedMultiplier;
                public float timeBetweenEnemies;

                [HorizontalGroup]
                public SubWaveGraphic background;

                [HorizontalGroup]//(Width = 0.5f)]
                [InfoBox("Click the picture to add a new fish.")]
                [NonSerialized, OdinSerialize, ShowInInspector]
                public FishContainer[] enemyTypes;

                public void Reset()
                {
                    name = null;
                    speedMultiplier = 1;
                    timeBetweenEnemies = 0;
                    enemyTypes = new FishContainer[0];
                    //background = new InteractableTexture();
                }
            }

            [Serializable]
            public class RandomSubWavePanel
            {
                [InfoBox("You are now building a random sub wave. You must specify paremeters for each enemy type, and specify a quantity.")]
                public string name;
                public float speedMultiplier;
                public float timeBetweenEnemies;
                public int enemies;

                [HorizontalGroup]
                public SubWaveGraphic background;

                [HorizontalGroup]//(Width = 0.5f)]
                [InfoBox("Hit the plus to add a new enemy type. For random subwaves, its best to leave the position null.")]
                [NonSerialized, OdinSerialize, ShowInInspector]
                public FishContainer[] enemyTypes;

                public void Reset()
                {
                    name = null;
                    speedMultiplier = 1;
                    timeBetweenEnemies = 0;
                    enemies = 0;
                    enemyTypes = new FishContainer[0];
                }
            }


            [Serializable]
            public class SubWaveGraphic
            {
                [HideLabel, OnInspectorGUI("DrawPreview")]
                public Texture2D texture = tex;

                private void DrawPreview()
                {
                    if (tex == null) return;
                    GUILayout.Label(tex, GUILayout.Width(250));
                }

            }

        }


    }
}


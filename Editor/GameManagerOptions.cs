using KenTank.GameManager;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameManager))]
public class GameManagerOptions : Editor
{
    public VisualTreeAsset visualTree;
    GameManager self => (GameManager)target;

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        // Create the default inspector
        var defaultInspector = new VisualElement();
        var serializedObject = new SerializedObject(target);
        var iterator = serializedObject.GetIterator();

        if (iterator.NextVisible(true))
        {
            do
            {
                var propertyField = new PropertyField(iterator.Copy());
                propertyField.SetEnabled(iterator.propertyPath != "m_Script");
                defaultInspector.Add(propertyField);
            } while (iterator.NextVisible(false));
        }

        root.Add(defaultInspector);
        visualTree.CloneTree(root);

        var volume = root.Q<Slider>("musicVolume");
        volume.value = self.sceneManager.musicVolume;
        volume.RegisterValueChangedCallback((value) => {
            self.sceneManager.musicVolume = value.newValue;
        });

        var music = root.Q<ObjectField>("musicClip");
        music.objectType = typeof(AudioClip);
        music.value = self.sceneManager.sceneMusic;
        music.RegisterValueChangedCallback((value) => {
            self.sceneManager.sceneMusic = value.newValue as AudioClip;
        });

        var transtition = root.Q<ObjectField>("transtition");
        transtition.objectType = typeof(Transtition);
        transtition.value = self.transtitionManager.transition;
        transtition.RegisterValueChangedCallback((value) => {
            self.transtitionManager.transition = value.newValue as Transtition;
        });

        return root;
    }
}

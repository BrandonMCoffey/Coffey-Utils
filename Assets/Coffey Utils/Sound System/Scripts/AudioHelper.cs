namespace SoundSystem.Scripts
{
    public static class AudioHelper
    {
        // Play the clip with given properties
        public static void PlayClip(SfxProperties properties)
        {
            properties.Print();
            if (properties.Null) return;
            var controller = PoolController();
            controller.SetSourceProperties(properties.Clip, properties.Volume, properties.Pitch, properties.Loop, properties.SpatialBlend);
            if (properties.MixerGroup != null) controller.SetMixer(properties.MixerGroup);
            controller.SetPosition(properties.Position);
            if (properties.Parent != null) controller.SetParent(properties.Parent);
            controller.Play();
        }

        // Borrow a Controller from the Sfx Pool
        private static AudioSourceController PoolController()
        {
            var controller = SoundManager.Instance.GetController();
            controller.Reset();
            controller.Claimed = true;
            return controller;
        }
    }
}
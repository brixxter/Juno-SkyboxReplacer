namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ModApi;
    using ModApi.Common;
    using ModApi.Mods;
    using UnityEngine;
    
    
    using UnityEngine.UI;

    /// <summary>
    /// A singleton object representing this mod that is instantiated and initialize when the mod is loaded.
    /// </summary>
    public class Mod : ModApi.Mods.GameMod
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Mod"/> class from being created.
        /// </summary>
        private Mod() : base()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the mod object.
        /// </summary>
        /// <value>The singleton instance of the mod object.</value>
        public static Mod Instance { get; } = GetModInstance<Mod>();

        protected override void OnModInitialized()
        {
            base.OnModInitialized();
            Game.Instance.SceneManager.SceneLoaded += SceneManagerOnSceneLoaded;
        }

        void SceneManagerOnSceneLoaded(object sender, EventArgs e)
        {
            if (Game.InFlightScene) Game.Instance.FlightScene.GameObject.AddComponent<SkyboxScript>();
            if (Game.InPlanetStudioScene) Assets.Scripts.PlanetStudio.PlanetStudioScript.Instance.gameObject.AddComponent<SkyboxScript>();
        }
    }
}
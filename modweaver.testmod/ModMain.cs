using System;
using System.IO;
using HarmonyLib;
using modweaver.core;


namespace modweaver.testmod {
    [ModMainClass]
    // ReSharper disable once UnusedType.Global (because it is used!!)
    public class ModMain : Mod {
        public override void Init() {
            //test logger
            Logger.Trace("Trace!");
            Logger.Debug("Debug!");
            Logger.Info("Info!");
            Logger.Warn("Warn!");
            Logger.Error("Error!");
            
            Logger.Info("Test mod init method is called.");
            Harmony harmony = new Harmony(Metadata.id); 
            Logger.Info("Running patches!");
            harmony.PatchAll();
            Logger.Info("Patched!");
        }

        public override void Ready() {
            Logger.Info("Test mod is ready!");
            
            Config.set("test1", 42);
            Config.set("test2", true, "otherfile");

            var test1 = Config.get("test1", -1);
            var test2 = Config.get("test2", false, "otherfile");
            
            Logger.Debug("Config test values: {V1}, {V2}", test1, test2);
        }

        public override void OnGUI(ModsMenuPopup ui) {
            ui.CreateDivider();
            ui.CreateTitle("I'm a cool mod :3");
        }
    }

    [HarmonyPatch(typeof(VersionNumberTextMesh), "Start")]
    public static class VNTMPatch {
        
        [HarmonyPostfix]
        public static void Postfix(ref VersionNumberTextMesh __instance) {
            __instance.textMesh.text += "\nhello world :3";
        }
    }
}
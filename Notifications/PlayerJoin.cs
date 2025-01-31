using HarmonyLib;
using iiMenu.Mods;
using iiMenu.Notifications;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.IO;
using UnityEngine;
using static iiMenu.Menu.Main;

namespace iiMenu.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class JoinPatch
    {
        private static void Prefix(Player newPlayer)
        {
            if (newPlayer != oldnewplayer)
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=green>JOIN</color><color=grey>] </color><color=white>Name: " + newPlayer.NickName + "</color>");

                if (Safety.LogPlayerNames == true)
                {
                    File.WriteAllText("iisStupidMenu/iiMenu_PlayerLog.txt", File.ReadAllText("iisStupidMenu/iiMenu_PlayerLog.txt") + "\n" + "<" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "> [JOIN] " + newPlayer.NickName);
                }

                if (customSoundOnJoin)
                {
                    if (!Directory.Exists("iisStupidMenu"))
                    {
                        Directory.CreateDirectory("iisStupidMenu");
                    }
                    File.WriteAllText("iisStupidMenu/iiMenu_CustomSoundOnJoin.txt", "PlayerJoin");
                }
                oldnewplayer = newPlayer;
            }
        }

        private static Player oldnewplayer;
    }
}
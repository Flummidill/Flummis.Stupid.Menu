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
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    public class LeavePatch
    {
        private static void Prefix(Player otherPlayer)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer && otherPlayer != a)
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=red>LEAVE</color><color=grey>]</color> <color=white>Name: " + otherPlayer.NickName + "</color>");

                if (Safety.LogPlayerNames == true)
                {
                    File.WriteAllText("iisStupidMenu/iiMenu_PlayerLog.txt", File.ReadAllText("iisStupidMenu/iiMenu_PlayerLog.txt") + "\n" + "<" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "> [JOIN] " + otherPlayer.NickName);
                }

                if (customSoundOnJoin)
                {
                    if (!Directory.Exists("iisStupidMenu"))
                    {
                        Directory.CreateDirectory("iisStupidMenu");
                    }
                    File.WriteAllText("iisStupidMenu/iiMenu_CustomSoundOnJoin.txt", "PlayerLeave");
                }
                lastPlayerCount = -1;
                a = otherPlayer;
            }
        }

        private static Player a;
    }
}
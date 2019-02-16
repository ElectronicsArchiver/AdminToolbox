﻿using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace AdminToolbox
{
	using API;
	using API.Extentions;

	class RoundEventHandler : IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerRoundRestart, IEventHandlerCheckRoundEnd
	{
		private Plugin plugin;
		static IConfigFile Config => ConfigManager.Manager.Config;
		internal string intercomReady = Config.GetStringValue("admintoolbox_intercomready_text", string.Empty),
			intercomRestart = Config.GetStringValue("admintoolbox_intercomrestart_text", string.Empty),
			intercomTransmit = Config.GetStringValue("admintoolbox_intercomtransmit_text", string.Empty);

		public RoundEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}
		public void OnRoundStart(RoundStartEvent ev)
		{
			AdminToolbox.isRoundFinished = false;
			if (ConfigManager.Manager.Config.GetBoolValue("admintoolbox_round_info", true, false))
			{
				plugin.Info("Round: " + ++AdminToolbox.RoundCount + " started.");
				plugin.Info("Players this round: " + ev.Server.GetPlayers().Count);
			}
			AdminToolbox.AddMissingPlayerVariables();
			AdminToolbox.atfileManager.PlayerStatsFileManager(ev.Server.GetPlayers(), Managers.ATFileManager.PlayerFile.Write);
			AdminToolbox._logStartTime = DateTime.Now.Year.ToString() + "-" + ((DateTime.Now.Month >= 10) ? DateTime.Now.Month.ToString() : ("0" + DateTime.Now.Month.ToString())) + "-" + ((DateTime.Now.Day >= 10) ? DateTime.Now.Day.ToString() : ("0" + DateTime.Now.Day.ToString())) + " " + ((DateTime.Now.Hour >= 10) ? DateTime.Now.Hour.ToString() : ("0" + DateTime.Now.Hour.ToString())) + "." + ((DateTime.Now.Minute >= 10) ? DateTime.Now.Minute.ToString() : ("0" + DateTime.Now.Minute.ToString())) + "." + ((DateTime.Now.Second >= 10) ? DateTime.Now.Second.ToString() : ("0" + DateTime.Now.Second.ToString()));
			AdminToolbox.warpManager.RefreshWarps();

			AdminToolbox.roundStatsRecorded = false;

			if (intercomReady != string.Empty)
				ev.Server.Map.SetIntercomContent(IntercomStatus.Ready, intercomReady);
			if (intercomRestart != string.Empty)
				ev.Server.Map.SetIntercomContent(IntercomStatus.Restarting, intercomRestart);
			if (intercomTransmit != string.Empty)
				ev.Server.Map.SetIntercomContent(IntercomStatus.Transmitting, intercomTransmit);
		}


		public void OnCheckRoundEnd(CheckRoundEndEvent ev)
		{
			if (AdminToolbox.lockRound)
				ev.Status = ROUND_END_STATUS.ON_GOING;
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			bool realRoundEnd(RoundEndEvent myEvent)
			{
				//Temp fix for the OnRoundEnd triggering on RoundStart bug
				if (myEvent.Round.Duration >= 3)
					return true;
				else
					return false;
			}
			if (realRoundEnd(ev))
			{
				AdminToolbox.isRoundFinished = true;
				AdminToolbox.lockRound = false;
				if (ConfigManager.Manager.Config.GetBoolValue("admintoolbox_round_info", true, false))
				{
					plugin.Info("Round: " + AdminToolbox.RoundCount + " has ended.");
					int minutes = (int)(ev.Round.Duration / 60), duration = ev.Round.Duration;
					if (duration < 60)
						plugin.Info("Round lasted for: " + duration + " sec");
					else
						plugin.Info("Round lasted for: " + minutes + " min, " + (duration - (minutes * 60)) + " sec");
				}
				AdminToolbox.AddMissingPlayerVariables();
				foreach (KeyValuePair<string, PlayerSettings> kp in AdminToolbox.ATPlayerDict)
				{
					kp.Value.PlayerStats.RoundsPlayed++;
				}
			}

		}

		public void OnRoundRestart(RoundRestartEvent ev)
		{
			AdminToolbox.lockRound = false;
			if (AdminToolbox.ATPlayerDict.Count > 0)
				AdminToolbox.ATPlayerDict.ResetPlayerBools();
				
			foreach (KeyValuePair<string, PlayerSettings> kp in AdminToolbox.ATPlayerDict)
				kp.Value.PlayerStats.MinutesPlayed += DateTime.Now.Subtract(kp.Value.JoinTime).TotalSeconds;
			AdminToolbox.atfileManager.PlayerStatsFileManager(AdminToolbox.ATPlayerDict.Keys.ToArray(), Managers.ATFileManager.PlayerFile.Write);
			AdminToolbox.logManager.ManageDatedATLogs();
		}
	}
}

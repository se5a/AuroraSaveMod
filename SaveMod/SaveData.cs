using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aurora
{
	public delegate void SaveCompleteEventHandler(SaveCompleteData saveCompleteData);

	public struct SaveCompleteData
	{
		public GClass0 Game;
		public TimeSpan CopyBackupTime;
		public TimeSpan SaveToDiskSQLTime;
	}

	public static class SaveGameMethods
	{
		private static bool _isIOBuisy;
		private static readonly Object _lockobj = new Object();

		public static bool GetIsIOBuisy()
		{
			bool buisyIO = true;
			lock (_lockobj)
			{
				buisyIO = _isIOBuisy;
			}

			return buisyIO;
		}

		public static void SetIsIOBuisy(bool ioISBusy)
		{
			lock (_lockobj)
			{
				_isIOBuisy = ioISBusy;
			}
		}

		public static SaveData SaveToMemory(GClass0 game)
		{

			while (GetIsIOBuisy())
			{
				Thread.Sleep(250);
			}
			SetIsIOBuisy(true);
			
			var sw = new Stopwatch();
			sw.Start();
			Cursor.Current = Cursors.WaitCursor;
			var saveData = new SaveData(game);
			Cursor.Current = Cursors.Default;
			var memsaveTime = sw.Elapsed;
			sw.Stop();
			
			string message = "SaveToMemTime = " + sw.Elapsed.TotalSeconds;
			GClass21 race = game.gclass21_0;
			game.gclass85_0.method_2(GEnum123.const_0, message, race, null, 0.0, 0.0, AuroraEventCategory.All);
			
			return saveData;

		}

		public static async void SaveToSQLDatabase(GClass0 game, SaveData save)
		{
			TimeSpan copyBackupTime = TimeSpan.Zero;
			var sw = new Stopwatch();
			sw.Start();
			
			//get a reference for the main thread. 
			var SyncContext = SynchronizationContext.Current;
			
			string message = "Starting SQL Save To Disk as a seperate task...";
			GClass21 race = game.gclass21_0;
			game.gclass85_0.method_2(GEnum123.const_0, message, race, null, 0.0, 0.0, AuroraEventCategory.All);

			//split off another thread to do the IO task.
			Task t = Task.Factory.StartNew(() =>
			{
				try
				{
					BackupOldSaves();
					sw.Stop();
					copyBackupTime = sw.Elapsed;
					sw.Restart();
					var conStr = "Data Source=AuroraQSDB.db;Version=3;New=False;Compress=True;";

					using (SQLiteConnection sqliteConnection = new SQLiteConnection(conStr)) //GClass202.string_1))
					{
						sqliteConnection.Open();
						using (SQLiteTransaction sqliteTransaction = sqliteConnection.BeginTransaction())
						{

							save.SaveToSQL(sqliteConnection, save.GameID);


							sqliteTransaction.Commit();
						}

						sqliteConnection.Close();
					}
				}
				catch (OleDbException oleDbException_)
				{
					GClass202.smethod_72(oleDbException_, 1425);
				}
				catch (Exception exception_)
				{
					GClass202.smethod_68(exception_, 1426);
				}
				
				SetIsIOBuisy(false);
				sw.Stop();
				var data = new SaveCompleteData()
				{
					Game = game,
					CopyBackupTime = copyBackupTime,
					SaveToDiskSQLTime = sw.Elapsed,
				};
				//task thread marshals this call to the main thread.
				SyncContext.Post(InvokeSaveComplete, data);

			});

			//main thread listens for event, and calls OnSaveCompleteEvent() function.
			SaveCompleteEvent += OnSaveCompleteEvent;

		}

		/// <summary>
		/// This is all so we don't have to mess around with putting thread locks on the event log. 
		/// </summary>
		#region Thread Marshaling and event calling
		
		public static event SaveCompleteEventHandler SaveCompleteEvent;
		private static void InvokeSaveComplete(object data)
		{
			//invoke the "SaveCompleteEvent" on the main thread. 
			SaveCompleteData saveCompleteData = (SaveCompleteData)data;
			SaveCompleteEvent?.Invoke(saveCompleteData);
		}
		private static void OnSaveCompleteEvent(SaveCompleteData data)
		{
			var et = GEnum123.const_0; 
			GClass21 race = data.Game.gclass21_0;
			string message1 = "BackupSaveTime = " + data.CopyBackupTime.TotalSeconds;
			string message2 = "SaveToDiskTime = " + data.SaveToDiskSQLTime.TotalSeconds;
			AuroraEventCategory cat = AuroraEventCategory.All;
				
			data.Game.gclass85_0.method_2(et, message1, race, null, 0.0, 0.0, cat);
			data.Game.gclass85_0.method_2(et, message2, race, null, 0.0, 0.0, cat);
		}
		
		#endregion
		
		public static void BackupOldSaves()
		{
			try
			{
				if (File.Exists("AuroraQSDBPreviousSaveBackup.db"))
				{
					File.Delete("AuroraQSDBPreviousSaveBackup.db");
				}

				if (File.Exists("AuroraQSDBSaveBackup.db"))
				{
					File.Copy("AuroraQSDBSaveBackup.db", "AuroraQSDBPreviousSaveBackup.db", true);
					File.Delete("AuroraQSDBSaveBackup.db");
				}
				if(File.Exists("AuroraQSDB.db"))
				{
					File.Copy("AuroraQSDB.db", "AuroraQSDBSaveBackup.db", true);
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1427);
			}

		}
	}

	public class SaveData
	{
		public int GameID;
		public int PlayerRaceID;
        public SaveGameData SaveGameData;
        public SaveRace SaveRacesData;
        public SaveSystem SaveSystemsData;
        public SaveSurveyLocation SaveSurveyLocationsData;
        public SaveStar SaveStarsData;
        public SaveJumpPoint SaveJumpPointsData;
        public SaveWaypoint SaveWayPointsData;
        public SaveMassDriverPackets SaveMDPacketsData;
        public SaveLagrangePoint SaveLagrangePointsData;
        public SavePopTradeBalance SavePopTradeData;
        public SaveShippingWealthData SaveShippingWealthDataData;
        public SaveMoveOrders SaveMoveOrdersData;
        public SaveOrderTemplate SaveOrderTemplatesData;
        public SaveMoveOrderTemplate SaveMoveOrderTemplatesData;
        
        public SaveSystemBody SaveSystemBodiesData;
        public DelSystemBody DeleteSysBodyData;
        public UpdateSystemBodies UpdateSystemBodiesData;
        
        public SaveSystemBodySurveys SaveSystemBodySurveysData;
        public SaveMissileGeoSurvey SaveMissileGeoSurveysData;
        public SaveFleet SaveFleetsData;
        public SavePopulationInstallations SavePopulationInstallationsData;
        public SaveShipCargo SaveShipCargoData;
        public SavePopulation SavePopulationsData;
        public SaveMineralDeposit SaveMineralDepositsData;
        public SaveShipClass SaveShipClassesData;
        public SaveSystemBodyName SaveSystemBodyNamesData;
        public SaveSurvivors SaveSurvivorsData;
        public SaveAncientConstruct SaveAncentConstrucctData;
        public SaveRuinRace SaveRuinRacesData;
        public SaveShip SaveShipsData;
        public SaveAcidAttack SaveAcidAttacksData;
        public SaveContacts SaveContactsData;
        public SaveWrecks SaveWrecksData;
        public SaveIndustrialProjects SaveIndustrialProjectsData;
        public SaveMissileSalvo SaveSalvosData;
        public SaveMissiles SaveMissileTypesData;
        public SaveAtmosphericGas SaveAtmosphereData;
        public SaveHullDescription SaveHullsData;
        public SaveCommander SaveCommandersData;
        public SaveShipyard SaveShipyardsData;
        public SaveShipyardTask SaveShipyardTasksData;
        public SaveSpecies SaveSpeciesData;
        public SaveWealthData SaveWealthDataData;
        public SaveShippingLines SaveShippingLinesData;
        public SaveNavalAdminCommand SaveNavalAdminCommandsData;
        public SaveSubFleets SaveSubFleetsData;
        public SaveLifepods SaveLifepodsData;
        public SaveGroundUnitTraining SaveGroundUnitTrainingTasksData;
        public SaveResearchProject SaveResearchProjectsData;
        public SaveSectorCommand SaveSectorsData;
        public SaveAlienRaces SaveAlienRacesData;
        public SaveSurveyRecords SaveSurveyRecordsData;
        public SaveAlienPopulation SaveAlienPopulationsData;
        public SaveAlienClass SaveAlienClassesData;
        public SaveAlienRaceSensor SaveAlienSensorsData;
        public SaveAlienShip SaveAlienShipsData;
        public SaveAlienGroundUnitClass SaveAlienGroundUnitClassesData;
        public SaveRaceTech SaveRaceTechData;
        public SaveTechSystem SaveTechSystemsData;
        public SaveShipDesignComponents SaveShipComponentsData;
        public SaveIncrements SaveIncrementsData;
        public SaveGameLog SaveGameLogData;
        public SaveEventColour SaveEventColoursData;
        public SaveHideEvents SaveHideEventsData;
        public SaveDesignPhilosophy SaveDesignPhilosophiesData;
        public SaveRanks SaveRanksData;
        public SaveMapLabel SaveMapLabelsData;
        public SaveShipTech SaveShipTechData;
        public SaveGroundUnitClass SaveGroundUnitClassesData;
        public SaveGroundUnitFormation SaveGroundUnitFormationsData;
        public SaveGroundUnitFormationTemplate SaveFormationTemplatesData;
        public SaveGroundUnitFormationElement SaveFormationElementsData;
        public SaveGroundUnitTrainingQueue SaveGUTrainingQ;
        public SaveWindowPosition SaveWindowPositionsData;
        public SaveTechProgressionRace SaveRaceTechProgressionData;
        public SaveDamageControlQueue SaveDamageControlQueueData;
        public SaveRaceMedals SaveRaceMedalData;
        public SaveMedalConditionAssignment SaveMedalConditionalData;
        public SaveAetherRift SaveAetherRiftData;
		

		public SaveData(GClass0 game)
		{
			GameID = game.int_57;
			SaveGameData = new SaveGameData(game);
	        SaveRacesData = new SaveRace(game);
	        SaveSystemsData = new SaveSystem(game);
	        SaveSurveyLocationsData = new SaveSurveyLocation(game);
	        SaveStarsData = new SaveStar(game);
	        SaveJumpPointsData = new SaveJumpPoint(game);
	        SaveWayPointsData = new SaveWaypoint(game);
	        SaveMDPacketsData = new SaveMassDriverPackets(game);
	        SaveLagrangePointsData = new SaveLagrangePoint(game);
	        SaveAetherRiftData = new SaveAetherRift(game);
	        SavePopTradeData = new SavePopTradeBalance(game);
	        SaveShippingWealthDataData = new SaveShippingWealthData(game);
	        SaveMoveOrdersData = new SaveMoveOrders(game);
	        SaveOrderTemplatesData = new SaveOrderTemplate(game);
	        SaveMoveOrderTemplatesData = new SaveMoveOrderTemplate(game);
	        DeleteSysBodyData = new DelSystemBody(game);
	        SaveSystemBodiesData = new SaveSystemBody(game);
	        UpdateSystemBodiesData = new UpdateSystemBodies(game);
	        SaveSystemBodySurveysData = new SaveSystemBodySurveys(game);
	        SaveMissileGeoSurveysData = new SaveMissileGeoSurvey(game);
	        SaveFleetsData = new SaveFleet(game);
	        SavePopulationInstallationsData = new SavePopulationInstallations(game);
	        SaveShipCargoData = new SaveShipCargo(game);
	        SavePopulationsData = new SavePopulation(game);
	        SaveMineralDepositsData = new SaveMineralDeposit(game);
	        SaveShipClassesData = new SaveShipClass(game);
	        SaveSystemBodyNamesData = new SaveSystemBodyName(game);
	        SaveSurvivorsData = new SaveSurvivors(game);
	        SaveAncentConstrucctData = new SaveAncientConstruct(game); //older versions called this "Anomalies"
	        SaveRuinRacesData = new SaveRuinRace(game);
	        SaveShipsData = new SaveShip(game);
	        SaveAcidAttacksData = new SaveAcidAttack(game);
	        SaveContactsData = new SaveContacts(game);
	        SaveWrecksData = new SaveWrecks(game);
	        SaveIndustrialProjectsData = new SaveIndustrialProjects(game);
	        SaveSalvosData = new SaveMissileSalvo(game);
	        SaveMissileTypesData = new SaveMissiles(game);
	        SaveAtmosphereData = new SaveAtmosphericGas(game);
	        SaveHullsData = new SaveHullDescription(game);
	        SaveCommandersData = new SaveCommander(game);
	        SaveShipyardsData = new SaveShipyard(game);
	        SaveShipyardTasksData = new SaveShipyardTask(game);
	        SaveSpeciesData = new SaveSpecies(game);
	        SaveWealthDataData = new SaveWealthData(game);
	        SaveShippingLinesData = new SaveShippingLines(game);
	        SaveNavalAdminCommandsData = new SaveNavalAdminCommand(game);
	        SaveSubFleetsData = new SaveSubFleets(game);
	        SaveLifepodsData = new SaveLifepods(game);
	        SaveGroundUnitTrainingTasksData = new SaveGroundUnitTraining(game);
	        SaveGUTrainingQ = new SaveGroundUnitTrainingQueue(game); //this is not in older versions
	        SaveResearchProjectsData = new SaveResearchProject(game);
	        SaveSectorsData = new SaveSectorCommand(game);
	        SaveAlienRacesData = new SaveAlienRaces(game);
	        SaveAlienPopulationsData = new SaveAlienPopulation(game);
	        SaveAlienClassesData = new SaveAlienClass(game);
	        SaveAlienSensorsData = new SaveAlienRaceSensor(game);
	        SaveAlienShipsData = new SaveAlienShip(game);
	        SaveAlienGroundUnitClassesData = new SaveAlienGroundUnitClass(game);
	        SaveRaceTechData = new SaveRaceTech(game);
	        SaveTechSystemsData = new SaveTechSystem(game);
	        SaveShipComponentsData = new SaveShipDesignComponents(game);
	        SaveIncrementsData = new SaveIncrements(game);
	        SaveGameLogData  = new SaveGameLog(game);
	        SaveEventColoursData = new SaveEventColour(game);
	        SaveHideEventsData = new SaveHideEvents(game);
	        SaveSurveyRecordsData = new SaveSurveyRecords(game);
	        SaveDesignPhilosophiesData = new SaveDesignPhilosophy(game);
	        SaveRanksData = new SaveRanks(game);
	        SaveMapLabelsData = new SaveMapLabel(game);
	        SaveShipTechData = new SaveShipTech(game);
	        SaveGroundUnitClassesData = new SaveGroundUnitClass(game);
	        SaveGroundUnitFormationsData = new SaveGroundUnitFormation(game);
	        SaveFormationTemplatesData = new SaveGroundUnitFormationTemplate(game);
	        SaveFormationElementsData = new SaveGroundUnitFormationElement(game);
	        SaveWindowPositionsData = new SaveWindowPosition(game);
	        SaveRaceTechProgressionData = new SaveTechProgressionRace(game);
	        SaveDamageControlQueueData = new SaveDamageControlQueue(game);
	        SaveRaceMedalData = new SaveRaceMedals(game);
	        SaveMedalConditionalData = new SaveMedalConditionAssignment(game);

	        
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			SaveGameData.SaveToSQL(sqliteConnection_0);
	        SaveRacesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSystemsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSurveyLocationsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveStarsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveJumpPointsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveWayPointsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMDPacketsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveLagrangePointsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAetherRiftData.SaveToSQL(sqliteConnection_0, gameID);
	        SavePopTradeData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShippingWealthDataData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMoveOrdersData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveOrderTemplatesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMoveOrderTemplatesData.SaveToSQL(sqliteConnection_0, gameID);
	        DeleteSysBodyData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSystemBodiesData.SaveToSQL(sqliteConnection_0, gameID);
	        UpdateSystemBodiesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSystemBodySurveysData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMissileGeoSurveysData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveFleetsData.SaveToSQL(sqliteConnection_0, gameID);
	        SavePopulationInstallationsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipCargoData.SaveToSQL(sqliteConnection_0, gameID);
	        SavePopulationsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMineralDepositsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipClassesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSystemBodyNamesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSurvivorsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAncentConstrucctData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveRuinRacesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAcidAttacksData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveContactsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveWrecksData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveIndustrialProjectsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSalvosData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMissileTypesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAtmosphereData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveHullsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveCommandersData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipyardsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipyardTasksData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSpeciesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveWealthDataData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShippingLinesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveNavalAdminCommandsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSubFleetsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveLifepodsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveGroundUnitTrainingTasksData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveGUTrainingQ.SaveToSQL(sqliteConnection_0, gameID);
	        SaveResearchProjectsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSectorsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAlienRacesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAlienPopulationsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAlienClassesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAlienSensorsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAlienShipsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveAlienGroundUnitClassesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveRaceTechData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveTechSystemsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipComponentsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveIncrementsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveGameLogData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveEventColoursData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveHideEventsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveSurveyRecordsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveDesignPhilosophiesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveRanksData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMapLabelsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveShipTechData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveGroundUnitClassesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveGroundUnitFormationsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveFormationTemplatesData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveFormationElementsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveWindowPositionsData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveRaceTechProgressionData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveDamageControlQueueData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveRaceMedalData.SaveToSQL(sqliteConnection_0, gameID);
	        SaveMedalConditionalData.SaveToSQL(sqliteConnection_0, gameID);

		}
	}

	/// <summary>
	/// method_54
	/// </summary>
	public class SaveGameData
	{
		public struct GameData
		{
			public int GameID;
			public int AutoJumpGates;
			public int CivilianShippingLinesActive;
			public int AllowCivilianHarvesters;
			public int TechFromConquest;
			public int DefaultRaceID;
			public int DifficultyModifier;
			public int ResearchSpeed;
			public int TerraformingSpeed;
			public int SurveySpeed;
			public string GameName;
			public decimal GameTime;
			public int GenerateNonTNOnly;
			public int GenerateNPRs;
			public int HumanNPRs;
			public int InexpFleets;
			public int Invaders;
			public decimal LastGrowthTime;
			public decimal LastGroundCombatTime;
			public int LastViewed;
			public int LocalSystemChance;
			public int LocalSystemSpread;
			public int MinComets;
			public int NewRuinCreationChance;
			public int NoOverhauls;
			public int NonPlayerSystemDetection;
			public GEnum33 SolDisaster;
			public int NPRsGeneratePrecursors;
			public int NPRsGenerateSwarm;
			public int NPRsGenerateRifts;
			public int NumberOfSystems;
			public int OrbitalMotion;
			public int OrbitalMotionAst;
			public decimal PlayerExplorationTime;
			public int PoliticalAdmirals;
			public int Precursors;
			public int Rakhas;
			public int MinConstructionPeriod;
			public int MinGroundCombatPeriod;
			public int RaceChance;
			public int RaceChanceNPR;
			public int RealisticPromotions;
			public string SMPassword;
			public int StarSwarm;
			public int StartYear;
			public int SubPulseLength;
			public decimal TruceCountdown;
			public int UseKnownStars;
			public bool CurrentGroundCombat;
			public int MaxEventCount;
			public int MaxEventDays;
			public int ConstellationNames;
		}

		public GameData GameDataStore;

		public SaveGameData(GClass0 game)
		{
			GameDataStore = new GameData()
			{
				GameID = game.int_57,
				AutoJumpGates = game.int_77,
				CivilianShippingLinesActive = game.int_68,
				AllowCivilianHarvesters = game.int_69,
				TechFromConquest = game.int_92,
				DefaultRaceID = game.int_74,
				DifficultyModifier = game.int_58,
				ResearchSpeed = game.int_93,
				TerraformingSpeed = game.int_94,
				SurveySpeed = game.int_95,
				GameName = game.string_1,
				GameTime = game.decimal_0,
				GenerateNonTNOnly = game.int_84,
				GenerateNPRs = game.int_82,
				HumanNPRs = game.int_83,
				InexpFleets = game.int_76,
				Invaders = game.int_81,
				LastGrowthTime = game.decimal_1,
				LastGroundCombatTime = game.decimal_2,
				LastViewed = 1,
				LocalSystemChance = game.int_61,
				LocalSystemSpread = game.int_62,
				MinComets = game.int_70,
				NewRuinCreationChance = game.int_67,
				NoOverhauls = game.int_85,
				NonPlayerSystemDetection = game.int_88,
				SolDisaster = game.genum33_0,
				NPRsGeneratePrecursors = game.int_90,
				NPRsGenerateSwarm = game.int_89,
				NPRsGenerateRifts = game.int_91,
				NumberOfSystems = game.int_59,
				OrbitalMotion = game.int_71,
				OrbitalMotionAst = game.int_72,
				PlayerExplorationTime = game.decimal_5,
				PoliticalAdmirals = game.int_73,
				Precursors = game.int_78,
				Rakhas = game.int_79,
				MinConstructionPeriod = game.int_63,
				MinGroundCombatPeriod = game.int_64,
				RaceChance = game.int_65,
				RaceChanceNPR = game.int_66,
				RealisticPromotions = game.int_86,
				SMPassword = game.string_0,
				StarSwarm = game.int_80,
				StartYear = game.int_60,
				SubPulseLength = game.int_99,
				TruceCountdown = game.decimal_6,
				UseKnownStars = game.int_87,
				CurrentGroundCombat = game.bool_11,
				MaxEventCount = game.int_97,
				MaxEventDays = game.int_96,
				ConstellationNames = game.int_75,
			};
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Game WHERE GameID = " + GameDataStore.GameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					sqliteCommand.CommandText = "UPDATE FCT_Game SET LastViewed = 0";
					sqliteCommand.ExecuteNonQuery();
					sqliteCommand.CommandText =
						"INSERT INTO FCT_Game (GameID, AutoJumpGates, CivilianShippingLinesActive, AllowCivilianHarvesters, TechFromConquest, DefaultRaceID, DifficultyModifier, ResearchSpeed, TerraformingSpeed, SurveySpeed, GameName, GameTime, GenerateNonTNOnly, GenerateNPRs, HumanNPRs, InexpFleets, Invaders, LastGrowthTime, LastGroundCombatTime, LastViewed, LocalSystemChance, LocalSystemSpread, \r\n                    MinComets, NewRuinCreationChance, NoOverhauls, NonPlayerSystemDetection, SolDisaster, NPRsGeneratePrecursors, NPRsGenerateSwarm, NPRsGenerateRifts, NumberOfSystems, ConstellationNames, \r\n                    OrbitalMotion, OrbitalMotionAst, PlayerExplorationTime, PoliticalAdmirals, Precursors, Rakhas, MinConstructionPeriod, MinGroundCombatPeriod, RaceChance,  RaceChanceNPR, RealisticPromotions, SMPassword, StarSwarm, StartYear, SubPulseLength, TruceCountdown, UseKnownStars, CurrentGroundCombat, MaxEventDays, MaxEventCount ) \r\n                    VALUES ( @GameID, @AutoJumpGates, @CivilianShippingLinesActive, @AllowCivilianHarvesters, @TechFromConquest, @DefaultRaceID, @DifficultyModifier, @ResearchSpeed, @TerraformingSpeed, @SurveySpeed, @GameName, @GameTime, @GenerateNonTNOnly, @GenerateNPRs, @HumanNPRs, @InexpFleets, @Invaders, @LastGrowthTime, @LastGroundCombatTime, @LastViewed, @LocalSystemChance, @LocalSystemSpread, \r\n                    @MinComets, @NewRuinCreationChance, @NoOverhauls, @NonPlayerSystemDetection, @SolDisaster, @NPRsGeneratePrecursors, @NPRsGenerateSwarm, @NPRsGenerateRifts, @NumberOfSystems, @ConstellationNames,\r\n                    @OrbitalMotion, @OrbitalMotionAst, @PlayerExplorationTime, @PoliticalAdmirals, @Precursors, @Rakhas, @MinConstructionPeriod, @MinGroundCombatPeriod, @RaceChance, @RaceChanceNPR, @RealisticPromotions, @SMPassword, @StarSwarm, @StartYear, @SubPulseLength, @TruceCountdown, @UseKnownStars, @CurrentGroundCombat, @MaxEventDays, @MaxEventCount )";
					sqliteCommand.Parameters.AddWithValue("@GameID", GameDataStore.GameID);
					sqliteCommand.Parameters.AddWithValue("@AutoJumpGates", GameDataStore.AutoJumpGates);
					sqliteCommand.Parameters.AddWithValue("@CivilianShippingLinesActive",
						GameDataStore.CivilianShippingLinesActive);
					sqliteCommand.Parameters.AddWithValue("@AllowCivilianHarvesters", GameDataStore.AllowCivilianHarvesters);
					sqliteCommand.Parameters.AddWithValue("@TechFromConquest", GameDataStore.TechFromConquest);
					sqliteCommand.Parameters.AddWithValue("@DefaultRaceID", GameDataStore.DefaultRaceID);
					sqliteCommand.Parameters.AddWithValue("@DifficultyModifier", GameDataStore.DifficultyModifier);
					sqliteCommand.Parameters.AddWithValue("@ResearchSpeed", GameDataStore.ResearchSpeed);
					sqliteCommand.Parameters.AddWithValue("@TerraformingSpeed", GameDataStore.TerraformingSpeed);
					sqliteCommand.Parameters.AddWithValue("@SurveySpeed", GameDataStore.SurveySpeed);
					sqliteCommand.Parameters.AddWithValue("@GameName", GameDataStore.GameName);
					sqliteCommand.Parameters.AddWithValue("@GameTime", GameDataStore.GameTime);
					sqliteCommand.Parameters.AddWithValue("@GenerateNonTNOnly", GameDataStore.GenerateNonTNOnly);
					sqliteCommand.Parameters.AddWithValue("@GenerateNPRs", GameDataStore.GenerateNPRs);
					sqliteCommand.Parameters.AddWithValue("@HumanNPRs", GameDataStore.HumanNPRs);
					sqliteCommand.Parameters.AddWithValue("@InexpFleets", GameDataStore.InexpFleets);
					sqliteCommand.Parameters.AddWithValue("@Invaders", GameDataStore.Invaders);
					sqliteCommand.Parameters.AddWithValue("@LastGrowthTime", GameDataStore.LastGrowthTime);
					sqliteCommand.Parameters.AddWithValue("@LastGroundCombatTime", GameDataStore.LastGroundCombatTime);
					sqliteCommand.Parameters.AddWithValue("@LastViewed", GameDataStore.LastViewed);
					sqliteCommand.Parameters.AddWithValue("@LocalSystemChance", GameDataStore.LocalSystemChance);
					sqliteCommand.Parameters.AddWithValue("@LocalSystemSpread", GameDataStore.LocalSystemSpread);
					sqliteCommand.Parameters.AddWithValue("@MinComets", GameDataStore.MinComets);
					sqliteCommand.Parameters.AddWithValue("@NewRuinCreationChance", GameDataStore.NewRuinCreationChance);
					sqliteCommand.Parameters.AddWithValue("@NoOverhauls", GameDataStore.NoOverhauls);
					sqliteCommand.Parameters.AddWithValue("@NonPlayerSystemDetection", GameDataStore.NonPlayerSystemDetection);
					sqliteCommand.Parameters.AddWithValue("@SolDisaster", GameDataStore.SolDisaster);
					sqliteCommand.Parameters.AddWithValue("@NPRsGeneratePrecursors", GameDataStore.NPRsGeneratePrecursors);
					sqliteCommand.Parameters.AddWithValue("@NPRsGenerateSwarm", GameDataStore.NPRsGenerateSwarm);
					sqliteCommand.Parameters.AddWithValue("@NPRsGenerateRifts", GameDataStore.NPRsGenerateRifts);
					sqliteCommand.Parameters.AddWithValue("@NumberOfSystems", GameDataStore.NumberOfSystems);
					sqliteCommand.Parameters.AddWithValue("@OrbitalMotion", GameDataStore.OrbitalMotion);
					sqliteCommand.Parameters.AddWithValue("@OrbitalMotionAst", GameDataStore.OrbitalMotionAst);
					sqliteCommand.Parameters.AddWithValue("@PlayerExplorationTime", GameDataStore.PlayerExplorationTime);
					sqliteCommand.Parameters.AddWithValue("@PoliticalAdmirals", GameDataStore.PoliticalAdmirals);
					sqliteCommand.Parameters.AddWithValue("@Precursors", GameDataStore.Precursors);
					sqliteCommand.Parameters.AddWithValue("@Rakhas", GameDataStore.Rakhas);
					sqliteCommand.Parameters.AddWithValue("@MinConstructionPeriod", GameDataStore.MinConstructionPeriod);
					sqliteCommand.Parameters.AddWithValue("@MinGroundCombatPeriod", GameDataStore.MinGroundCombatPeriod);
					sqliteCommand.Parameters.AddWithValue("@RaceChance", GameDataStore.RaceChance);
					sqliteCommand.Parameters.AddWithValue("@RaceChanceNPR", GameDataStore.RaceChanceNPR);
					sqliteCommand.Parameters.AddWithValue("@RealisticPromotions", GameDataStore.RealisticPromotions);
					sqliteCommand.Parameters.AddWithValue("@SMPassword", GameDataStore.SMPassword);
					sqliteCommand.Parameters.AddWithValue("@StarSwarm", GameDataStore.StarSwarm);
					sqliteCommand.Parameters.AddWithValue("@StartYear", GameDataStore.StartYear);
					sqliteCommand.Parameters.AddWithValue("@SubPulseLength", GameDataStore.SubPulseLength);
					sqliteCommand.Parameters.AddWithValue("@TruceCountdown", GameDataStore.TruceCountdown);
					sqliteCommand.Parameters.AddWithValue("@UseKnownStars", GameDataStore.UseKnownStars);
					sqliteCommand.Parameters.AddWithValue("@CurrentGroundCombat", GameDataStore.CurrentGroundCombat);
					sqliteCommand.Parameters.AddWithValue("@MaxEventCount", GameDataStore.MaxEventCount);
					sqliteCommand.Parameters.AddWithValue("@MaxEventDays", GameDataStore.MaxEventDays);
					sqliteCommand.Parameters.AddWithValue("@ConstellationNames", GameDataStore.ConstellationNames);
					sqliteCommand.ExecuteNonQuery();
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1428);
			}
		}
	}

	/// <summary>
	/// method_55
	/// </summary>
	public class SaveRace
	{
		public struct RaceData
		{
			public int RaceID;
			public bool ConventionalStart;
			public bool NPR;
			public GEnum6 SpecialNPRID;
			public bool PreIndustrial;
			public int RaceGridSize;
			public string RaceName;
			public string RaceTitle;
			public decimal WealthPoints;
			public decimal PreviousWealth;
			public decimal StartingWealth;
			public decimal AnnualWealth;
			public int GovTypeID;
			public int UnreadMessages;
			public string FlagPic;
			public string HullPic;
			public string SpaceStationPic;
			public int Contacts;
			public int Colour;
			public int Red;
			public int Green;
			public int Blue;
			public string Password;
			public int ThemeID;
			public decimal ClassThemeID;
			public decimal MissileThemeID;
			public decimal GroundThemeID;
			public decimal SystemThemeID;
			public decimal DesignThemeID;
			public int DisplayGrade;
			public int ShowHighlight;
			public int MapRed;
			public int MapGreen;
			public int MapBlue;
			public int FleetViewOption;
			public int SelectRace;
			public int FleetsVisible;
			public int LastMapSystemViewed;
			public int chkAllowAny;
			public int chkAutoAssign;
			public int chkTons;
			public int StandardTour;
			public decimal LastAssignment;
			public int CurrentXenophobia;
			public decimal AcademyCrewmen;
			public decimal MaintenanceCapacity;
			public decimal OrdnanceCapacity;
			public decimal FighterCapacity;
			public decimal ShipBuilding;
			public decimal FuelProduction;
			public decimal ConstructionProduction;
			public int MaximumOrbitalMiningDiameter;
			public decimal OrdnanceProduction;
			public decimal FighterProduction;
			public decimal MineProduction;
			public int Research;
			public int PlanetarySensorStrength;
			public int TrainingLevel;
			public int GUStrength;
			public decimal TerraformingRate;
			public decimal ColonizationSkill;
			public int StartTechPoints;
			public decimal StartBuildPoints;
			public decimal WealthCreationRate;
			public decimal EconomicProdModifier;
			public decimal ShipyardOperations;
			public int MSPProduction;
			public int MaxRefuellingRate;
			public int MaxOrdnanceTransferRate;
			public decimal UnderwayReplenishmentRate;
			public int Hostile;
			public int Neutral;
			public int Friendly;
			public int Allied;
			public int Civilian;
			public bool HideCMCPop;
			public bool ShowPopStar;
			public bool ShowPopSystemBody;
			public bool PopByFunction;
			public int chkPlanets;
			public int chkDwarf;
			public int chkMoons;
			public int chkAst;
			public int chkWP;
			public int chkStarOrbits;
			public int chkPlanetOrbits;
			public int chkDwarfOrbits;
			public int chkMoonOrbits;
			public int chkAsteroidOrbits;
			public int chkStarNames;
			public int chkPlanetNames;
			public int chkDwarfNames;
			public int chkMoonNames;
			public int chkAstNames;
			public int chkFleets;
			public int chkMoveTail;
			public int chkColonies;
			public int chkCentre;
			public int chkSL;
			public int chkWaypoint;
			public int chkOrder;
			public int chkNoOverlap;
			public int chkActiveSensors;
			public int chkTracking;
			public int chkActiveOnly;
			public int chkShowDist;
			public int chkSBSurvey;
			public int chkMinerals;
			public int chkCometPath;
			public int chkAstColOnly;
			public int chkAstMinOnly;
			public int chkTAD;
			public int chkFiringRanges;
			public int chkAllRace;
			public int chkDisplayAllForms;
			public int chkSalvoOrigin;
			public int chkSalvoTarget;
			public int chkEscorts;
			public int chkFireControlRange;
			public int PassiveSensor;
			public int ActiveSensor;
			public int DetRange;
			public int chkHideIDs;
			public int chkHideSL;
			public int chkEvents;
			public int chkPackets;
			public int chkMPC;
			public int chkLifepods;
			public int chkWrecks;
			public int chkHostileSensors;
			public int chkGeoPoints;
			public int chkBearing;
			public int chkCoordinates;
			public int chkLostContacts;
			public int chkLostContactsOneYear;
			public int chkLostContactsOneDay;
			public int chkSystemOnly;
			public int chkShowCivilianOOB;
			public int chkHostile;
			public int chkFriendly;
			public int chkAllied;
			public int chkNeutral;
			public int chkCivilian;
			public int chkContactsCurrentSystem;
			public int chkPassive10;
			public int chkPassive100;
			public int chkPassive1000;
			public int chkPassive10000;
			public int chkUnexJP;
			public int chkJPSurveyStatus;
			public int chkSurveyLocationPoints;
			public int chkSurveyedSystemBodies;
			public int chkHabRangeWorlds;
			public int chkHabRangeWorldsLowG;
			public int chkLowCCNormalG;
			public int chkMediumCCNormalG;
			public int chkLowCCLowG;
			public int chkMediumCCLowG;
			public int chkDistanceFromSelected;
			public int chkWarshipLocation;
			public int chkAllFleetLocations;
			public int chkKnownAlienForces;
			public int chkAlienControlledSystems;
			public int chkPopulatedSystem;
			public int chkBlocked;
			public int chkMilitaryRestricted;
			public int chkDisplayMineralSearch;
			public int chkShipyardComplexes;
			public int chkNavalHeadquarters;
			public int chkSectors;
			public int chkPossibleDormantJP;
			public int chkSecurityStatus;
			public int chkDiscoveryDate;
			public int chkML;
			public int chkGroundSurveyLocations;
			public int chkHideOrbitFleets;
			public int chkNumCometsPlanetlessSystem;
			public string RankScientist;
			public string RankAdministrator;
			public int CargoShuttleLoadModifier;
			public int GroundFormationConstructionRate;
			public int ResearchTargetCost;
			public decimal CurrentResearchTotal;
			public int ColonyDensity;
			public bool NeutralRace;
			public double TonnageSent;
			public int LastProgressionOrder;
			public ResearchQueueData[] ResearchQueueStore;
			public PausedResearchData[] PausedResearchStore;
			public SwarmResearchData[] SwarmResearchStore;
			public KnownRuinRaceData[] KnownRuinRaceStore;
			public GroundUnitSeriesData[] GroundUnitSeriesStore;
			public GroundUnitSeriesClassData[] GroundUnitSeriesClassStore;
			public BannedBodiesData[] BannedBodiesStore;
			public RaceNameThemesData[] RaceNameThemesStore;
			public RaceGroundCombatData[] RaceGroundCombatStore;
			public RaceOperationalGroupElementsData[] RaceOperationalGroupElementsStore;
		}
		RaceData[] RaceStore;

		public struct ResearchQueueData
		{
			public int PopulationID;
			public int TechSystemID;
			public int CurrentProjectID;
			public int ResearchOrder;
		}
		

		public struct PausedResearchData
		{
			public int TechSystemID;
			public int RaceID;
			public int PointsAccumulated;
		}
		

		public struct SwarmResearchData
		{
			public int RaceID;
			public int ResearchPoints;
			public int TechSystemID;
		}
		

		public struct KnownRuinRaceData
		{
			public int RuinRaceID;
			public int RaceID;
		}
		

		public struct GroundUnitSeriesData
		{
			public int GroundUnitSeriesID;
			public string Description;
			public int RaceID;
		}
		

		public struct GroundUnitSeriesClassData
		{
			public int GroundUnitSeriesID;
			public int GroundUnitClassID;
			public int Priority;
			public int RaceID;
		}
		

		public struct BannedBodiesData
		{
			public int SystemBodyID;
			public int RaceID;
		}
		

		public struct RaceNameThemesData
		{
			public int RaceID;
			public int NameThemeID;
			public int Chance;
		}
		

		public struct RaceGroundCombatData
		{
			public int RaceID;
			public int SystemBodyID;
			public int ConsecutiveCombatRounds;
		}
		
		public struct RaceOperationalGroupElementsData
		{
			public int RaceID;
			public GEnum102 OperationalGroupID;
			public int NumShips;
			public GEnum117 AutomatedDesignID;
			public bool KeyElement;
		}
		


		public SaveRace(GClass0 game)
		{
			
			decimal num = 0m;
			decimal num2 = 0m;
			decimal num3 = 0m;
			decimal num4 = 0m;
			decimal num5 = 0m;

			RaceStore = new RaceData[game.dictionary_34.Count];
			int i = 0;
			foreach (GClass21 gclass in game.dictionary_34.Values)
			{
				num = 0m;
				num2 = 0m;
				num3 = 0m;
				num4 = 0m;
				num5 = 0m;
				if (gclass.gclass128_0 != null)
				{
					num = gclass.gclass128_0.int_0;
				}
				if (gclass.gclass128_3 != null)
				{
					num5 = gclass.gclass128_3.int_0;
				}
				if (gclass.gclass128_2 != null)
				{
					num4 = gclass.gclass128_2.int_0;
				}
				if (gclass.gclass128_1 != null)
				{
					num2 = gclass.gclass128_1.int_0;
				}
				if (gclass.gclass14_0 != null)
				{
					num3 = gclass.gclass14_0.int_0;
				}

				ResearchQueueData[] researchQueueStore = new ResearchQueueData[gclass.list_5.Count];
				int j = 0;
				foreach (GClass149 gclass2 in gclass.list_5)
				{
					var dataObj1 = new ResearchQueueData()
					{
						PopulationID = gclass2.gclass132_0.int_5,
						TechSystemID = gclass2.gclass147_0.int_0,
						CurrentProjectID = gclass2.gclass144_0.int_1,
						ResearchOrder = gclass2.int_0,
					};
					researchQueueStore[j] = dataObj1;
					j++;
				}
				
				PausedResearchData[] pausedResearchStore = new PausedResearchData[gclass.list_6.Count];
				j = 0;
				foreach (GClass150 gclass3 in gclass.list_6)
				{
					var dataObj1 = new PausedResearchData()
					{
						TechSystemID = gclass3.gclass147_0.int_0,
						RaceID = gclass.RaceID,
						PointsAccumulated = gclass3.int_0,
					};
					pausedResearchStore[j] = dataObj1;
					j++;
				}

				SwarmResearchData[] swarmResearchStore = new SwarmResearchData[gclass.list_7.Count];
				j = 0;
				foreach (GClass151 gclass4 in gclass.list_7)
				{
					var dataObj1 = new SwarmResearchData()
					{
						RaceID = gclass4.gclass21_0.RaceID,
						ResearchPoints = gclass4.int_0,
						TechSystemID = gclass4.gclass147_0.int_0,
					};
					swarmResearchStore[j] = dataObj1;
					j++;
				}
				
				KnownRuinRaceData[] knownRuinRaceStore = new KnownRuinRaceData[gclass.list_2.Count];
				j = 0;
				foreach (int num6 in gclass.list_2)
				{
					var dataObj1 = new KnownRuinRaceData()
					{
						RuinRaceID = num6,
						RaceID = gclass.RaceID,
					};
					knownRuinRaceStore[j] = dataObj1;
					j++;
				}
				
				GroundUnitSeriesData[] groundUnitSeriesStore = new GroundUnitSeriesData[gclass.dictionary_7.Count];
				j = 0;
				foreach (GClass86 gclass5 in gclass.dictionary_7.Values)
				{
					var dataObj1 = new GroundUnitSeriesData()
					{
						GroundUnitSeriesID = gclass5.int_0,
						Description = gclass5.string_0,
						RaceID = gclass.RaceID,
					};
					groundUnitSeriesStore[j] = dataObj1;
					j++;
				}
				
				GroundUnitSeriesClassData[] groundUnitSeriesClassStore = new GroundUnitSeriesClassData[gclass.list_11.Count];
				j = 0;
				foreach (GClass87 gclass6 in gclass.list_11)
				{
					var dataObj1 = new GroundUnitSeriesClassData()
					{
						GroundUnitSeriesID = gclass6.gclass86_0.int_0,
						GroundUnitClassID = gclass6.gclass93_0.int_0,
						Priority = gclass6.int_0,
						RaceID = gclass.RaceID,
					};
					groundUnitSeriesClassStore[j] = dataObj1;
					j++;
				}
				
				BannedBodiesData[] bannedBodiesStore = new BannedBodiesData[gclass.list_9.Count];
				j = 0;
				foreach (GClass1 gclass7 in gclass.list_9)
				{
					var dataObj1 = new BannedBodiesData()
					{
						SystemBodyID = gclass7.int_0,
						RaceID = gclass.RaceID,
					};
					bannedBodiesStore[j] = dataObj1;
					j++;
				}
				
				RaceNameThemesData[] raceNameThemesStore = new RaceNameThemesData[gclass.list_1.Count];
				j = 0;
				foreach (GClass44 gclass8 in gclass.list_1)
				{
					var dataObj1 = new RaceNameThemesData()
					{
						RaceID = gclass.RaceID,
						NameThemeID = gclass8.gclass57_0.int_0,
						Chance = gclass8.int_0,
					};
					raceNameThemesStore[j] = dataObj1;
					j++;
				}
				
				RaceGroundCombatData[] raceGroundCombatStore = new RaceGroundCombatData[gclass.dictionary_6.Count];
				j = 0;
				foreach (GClass45 gclass9 in gclass.dictionary_6.Values)
				{
					var dataObj1 = new RaceGroundCombatData()
					{
						RaceID = gclass.RaceID,
						SystemBodyID = gclass9.gclass1_0.int_0,
						ConsecutiveCombatRounds = gclass9.int_0,
					};
					raceGroundCombatStore[j] = dataObj1;
					j++;
				}
				
				RaceOperationalGroupElementsData[] raceOperationalGroupElementsStore = new RaceOperationalGroupElementsData[gclass.list_0.Count];
				j = 0;
				foreach (GClass10 gclass10 in gclass.list_0)
				{
					var dataObj1 = new RaceOperationalGroupElementsData()
					{
						RaceID = gclass.RaceID,
						OperationalGroupID = gclass10.gclass8_0.genum102_0,
						NumShips = gclass10.int_0,
						AutomatedDesignID = gclass10.gclass13_0.genum117_0,
						KeyElement = gclass10.bool_0,
					};
					raceOperationalGroupElementsStore[j] = dataObj1;
					j++;
				}
					
					
					
				
				var dataObj = new RaceData()
				{
					RaceID = gclass.RaceID,
					ConventionalStart = gclass.bool_0,
					NPR = gclass.bool_1,
					SpecialNPRID = gclass.genum6_0,
					PreIndustrial = gclass.bool_2,
					RaceGridSize = gclass.int_0,
					RaceName = gclass.string_0,
					RaceTitle = gclass.RaceTitle,
					WealthPoints = gclass.decimal_0,
					PreviousWealth = gclass.decimal_1,
					StartingWealth = gclass.decimal_2,
					AnnualWealth = gclass.decimal_4,
					GovTypeID = gclass.int_1,
					UnreadMessages = gclass.int_2,
					FlagPic = gclass.string_1,
					HullPic = gclass.string_3,
					SpaceStationPic = gclass.string_4,
					Contacts = gclass.int_3,
					Colour = gclass.int_4,
					Red = gclass.int_5,
					Green = gclass.int_6,
					Blue = gclass.int_7,
					Password = gclass.string_5,
					ThemeID = gclass.int_8,
					ClassThemeID = num,
					MissileThemeID = num5,
					GroundThemeID = num4,
					SystemThemeID = num2,
					DesignThemeID = num3,
					DisplayGrade = gclass.int_12,
					ShowHighlight = gclass.int_13,
					MapRed = gclass.int_14,
					MapGreen = gclass.int_15,
					MapBlue = gclass.int_16,
					FleetViewOption = gclass.int_17,
					SelectRace = gclass.int_18,
					FleetsVisible = gclass.int_19,
					LastMapSystemViewed = gclass.int_20,
					chkAllowAny = gclass.int_21,
					chkAutoAssign = gclass.int_22,
					chkTons = gclass.int_23,
					StandardTour = gclass.int_24,
					LastAssignment = gclass.decimal_3,
					CurrentXenophobia = gclass.int_25,
					AcademyCrewmen = gclass.decimal_8,
					MaintenanceCapacity = gclass.decimal_21,
					OrdnanceCapacity = gclass.decimal_9,
					FighterCapacity = gclass.decimal_10,
					ShipBuilding = gclass.decimal_11,
					FuelProduction = gclass.decimal_12,
					ConstructionProduction = gclass.decimal_13,
					MaximumOrbitalMiningDiameter = gclass.int_11,
					OrdnanceProduction = gclass.decimal_14,
					FighterProduction = gclass.decimal_15,
					MineProduction = gclass.decimal_16,
					Research = gclass.int_28,
					PlanetarySensorStrength = gclass.int_29,
					TrainingLevel = gclass.int_30,
					GUStrength = gclass.int_31,
					TerraformingRate = gclass.decimal_17,
					ColonizationSkill = gclass.decimal_18,
					StartTechPoints = gclass.int_32,
					StartBuildPoints = gclass.decimal_23,
					WealthCreationRate = gclass.decimal_19,
					EconomicProdModifier = gclass.EconomicProdModifier,
					ShipyardOperations = gclass.decimal_20,
					MSPProduction = gclass.int_33,
					MaxRefuellingRate = gclass.int_34,
					MaxOrdnanceTransferRate = gclass.int_35,
					UnderwayReplenishmentRate = gclass.decimal_22,
					Hostile = gclass.int_36,
					Neutral = gclass.int_37,
					Friendly = gclass.int_38,
					Allied = gclass.int_39,
					Civilian = gclass.int_40,
					HideCMCPop = gclass.bool_6,
					ShowPopStar = gclass.bool_4,
					ShowPopSystemBody = gclass.bool_5,
					PopByFunction = gclass.bool_7,
					chkPlanets = GClass202.smethod_25(gclass.chkPlanets),
					chkDwarf = GClass202.smethod_25(gclass.chkDwarf),
					chkMoons = GClass202.smethod_25(gclass.chkMoons),
					chkAst = GClass202.smethod_25(gclass.chkAst),
					chkWP = GClass202.smethod_25(gclass.chkWP),
					chkStarOrbits = GClass202.smethod_25(gclass.chkStarOrbits),
					chkPlanetOrbits = GClass202.smethod_25(gclass.chkPlanetOrbits),
					chkDwarfOrbits = GClass202.smethod_25(gclass.chkDwarfOrbits),
					chkMoonOrbits = GClass202.smethod_25(gclass.chkMoonOrbits),
					chkAsteroidOrbits = GClass202.smethod_25(gclass.chkAsteroidOrbits),
					chkStarNames = GClass202.smethod_25(gclass.chkStarNames),
					chkPlanetNames = GClass202.smethod_25(gclass.chkPlanetNames),
					chkDwarfNames = GClass202.smethod_25(gclass.chkDwarfNames),
					chkMoonNames = GClass202.smethod_25(gclass.chkMoonNames),
					chkAstNames = GClass202.smethod_25(gclass.chkAstNames),
					chkFleets = GClass202.smethod_25(gclass.chkFleets),
					chkMoveTail = GClass202.smethod_25(gclass.chkMoveTail),
					chkColonies = GClass202.smethod_25(gclass.chkColonies),
					chkCentre = GClass202.smethod_25(gclass.chkCentre),
					chkSL = GClass202.smethod_25(gclass.chkSL),
					chkWaypoint = GClass202.smethod_25(gclass.chkWaypoint),
					chkOrder = GClass202.smethod_25(gclass.chkOrder),
					chkNoOverlap = GClass202.smethod_25(gclass.chkNoOverlap),
					chkActiveSensors = GClass202.smethod_25(gclass.chkActiveSensors),
					chkTracking = GClass202.smethod_25(gclass.chkTracking),
					chkActiveOnly = GClass202.smethod_25(gclass.chkActiveOnly),
					chkShowDist = GClass202.smethod_25(gclass.chkShowDist),
					chkSBSurvey = GClass202.smethod_25(gclass.chkSBSurvey),
					chkMinerals = GClass202.smethod_25(gclass.chkMinerals),
					chkCometPath = GClass202.smethod_25(gclass.chkCometPath),
					chkAstColOnly = GClass202.smethod_25(gclass.chkAstColOnly),
					chkAstMinOnly = GClass202.smethod_25(gclass.chkAstMinOnly),
					chkTAD = GClass202.smethod_25(gclass.chkTAD),
					chkFiringRanges = GClass202.smethod_25(gclass.chkFiringRanges),
					chkAllRace = GClass202.smethod_25(gclass.chkAllRace),
					chkDisplayAllForms = GClass202.smethod_25(gclass.chkDisplayAllForms),
					chkSalvoOrigin = GClass202.smethod_25(gclass.chkSalvoOrigin),
					chkSalvoTarget = GClass202.smethod_25(gclass.chkSalvoTarget),
					chkEscorts = GClass202.smethod_25(gclass.chkEscorts),
					chkFireControlRange = GClass202.smethod_25(gclass.chkFireControlRange),
					PassiveSensor = GClass202.smethod_25(gclass.PassiveSensor),
					ActiveSensor = GClass202.smethod_25(gclass.ActiveSensor),
					DetRange = GClass202.smethod_25(gclass.DetRange),
					chkHideIDs = GClass202.smethod_25(gclass.chkHideIDs),
					chkHideSL = GClass202.smethod_25(gclass.chkHideSL),
					chkEvents = GClass202.smethod_25(gclass.chkEvents),
					chkPackets = GClass202.smethod_25(gclass.chkPackets),
					chkMPC = GClass202.smethod_25(gclass.chkMPC),
					chkLifepods = GClass202.smethod_25(gclass.chkLifepods),
					chkWrecks = GClass202.smethod_25(gclass.chkWrecks),
					chkHostileSensors = GClass202.smethod_25(gclass.chkHostileSensors),
					chkGeoPoints = GClass202.smethod_25(gclass.chkGeoPoints),
					chkBearing = GClass202.smethod_25(gclass.chkBearing),
					chkCoordinates = GClass202.smethod_25(gclass.chkCoordinates),
					chkLostContacts = GClass202.smethod_25(gclass.chkLostContacts),
					chkLostContactsOneYear = GClass202.smethod_25(gclass.chkLostContactsOneYear),
					chkLostContactsOneDay = GClass202.smethod_25(gclass.chkLostContactsOneDay),
					chkSystemOnly = GClass202.smethod_25(gclass.chkSystemOnly),
					chkShowCivilianOOB = GClass202.smethod_25(gclass.chkShowCivilianOOB),
					chkHostile = GClass202.smethod_25(gclass.chkHostile),
					chkFriendly = GClass202.smethod_25(gclass.chkFriendly),
					chkAllied = GClass202.smethod_25(gclass.chkAllied),
					chkNeutral = GClass202.smethod_25(gclass.chkNeutral),
					chkCivilian = GClass202.smethod_25(gclass.chkCivilian),
					chkContactsCurrentSystem = GClass202.smethod_25(gclass.chkContactsCurrentSystem),
					chkPassive10 = GClass202.smethod_25(gclass.chkPassive10),
					chkPassive100 = GClass202.smethod_25(gclass.chkPassive100),
					chkPassive1000 = GClass202.smethod_25(gclass.chkPassive1000),
					chkPassive10000 = GClass202.smethod_25(gclass.chkPassive10000),
					chkUnexJP = GClass202.smethod_25(gclass.chkUnexJP),
					chkJPSurveyStatus = GClass202.smethod_25(gclass.chkJPSurveyStatus),
					chkSurveyLocationPoints = GClass202.smethod_25(gclass.chkSurveyLocationPoints),
					chkSurveyedSystemBodies = GClass202.smethod_25(gclass.chkSurveyedSystemBodies),
					chkHabRangeWorlds = GClass202.smethod_25(gclass.chkHabRangeWorlds),
					chkHabRangeWorldsLowG = GClass202.smethod_25(gclass.chkHabRangeWorldsLowG),
					chkLowCCNormalG = GClass202.smethod_25(gclass.chkLowCCNormalG),
					chkMediumCCNormalG = GClass202.smethod_25(gclass.chkMediumCCNormalG),
					chkLowCCLowG = GClass202.smethod_25(gclass.chkLowCCLowG),
					chkMediumCCLowG = GClass202.smethod_25(gclass.chkMediumCCLowG),
					chkDistanceFromSelected = GClass202.smethod_25(gclass.chkDistanceFromSelected),
					chkWarshipLocation = GClass202.smethod_25(gclass.chkWarshipLocation),
					chkAllFleetLocations = GClass202.smethod_25(gclass.chkAllFleetLocations),
					chkKnownAlienForces = GClass202.smethod_25(gclass.chkKnownAlienForces),
					chkAlienControlledSystems = GClass202.smethod_25(gclass.chkAlienControlledSystems),
					chkPopulatedSystem = GClass202.smethod_25(gclass.chkPopulatedSystem),
					chkBlocked = GClass202.smethod_25(gclass.chkBlocked),
					chkMilitaryRestricted = GClass202.smethod_25(gclass.chkMilitaryRestricted),
					chkDisplayMineralSearch = GClass202.smethod_25(gclass.chkDisplayMineralSearch),
					chkShipyardComplexes = GClass202.smethod_25(gclass.chkShipyardComplexes),
					chkNavalHeadquarters = GClass202.smethod_25(gclass.chkNavalHeadquarters),
					chkSectors = GClass202.smethod_25(gclass.chkSectors),
					chkPossibleDormantJP = GClass202.smethod_25(gclass.chkPossibleDormantJP),
					chkSecurityStatus = GClass202.smethod_25(gclass.chkSecurityStatus),
					chkDiscoveryDate = GClass202.smethod_25(gclass.chkDiscoveryDate),
					chkML = GClass202.smethod_25(gclass.chkML),
					chkGroundSurveyLocations = GClass202.smethod_25(gclass.chkGroundSurveyLocations),
					chkHideOrbitFleets = GClass202.smethod_25(gclass.chkHideOrbitFleets),
					chkNumCometsPlanetlessSystem = GClass202.smethod_25(gclass.chkNumCometsPlanetlessSystem),
					RankScientist = gclass.string_9,
					RankAdministrator = gclass.string_8,
					CargoShuttleLoadModifier = gclass.int_9,
					GroundFormationConstructionRate = gclass.int_10,
					ResearchTargetCost = gclass.int_26,
					CurrentResearchTotal = gclass.decimal_5,
					ColonyDensity = gclass.int_27,
					NeutralRace = gclass.bool_3,
					TonnageSent = gclass.double_4,
					LastProgressionOrder = gclass.int_51,
					ResearchQueueStore = researchQueueStore,
					PausedResearchStore = pausedResearchStore,
					SwarmResearchStore = swarmResearchStore,
					KnownRuinRaceStore = knownRuinRaceStore,
					GroundUnitSeriesStore = groundUnitSeriesStore,
					GroundUnitSeriesClassStore = groundUnitSeriesClassStore,
					BannedBodiesStore = bannedBodiesStore,
					RaceNameThemesStore = raceNameThemesStore,
					RaceGroundCombatStore = raceGroundCombatStore,
					RaceOperationalGroupElementsStore = raceOperationalGroupElementsStore,
				};
				RaceStore[i] = dataObj;
				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Race WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ResearchQueue WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_PausedResearch WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_SwarmResearch WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_KnownRuinRace WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_RaceNameThemes WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_BannedBodies WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_WindowPosition WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_RaceGroundCombat WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_GroundUnitSeries WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_GroundUnitSeriesClass WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_RaceOperationalGroupElements WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in RaceStore)
					{
						try
						{

							sqliteCommand.CommandText =
								"INSERT INTO FCT_Race (RaceID, GameID, ConventionalStart, NPR, SpecialNPRID, PreIndustrial, RaceGridSize, RaceName, RaceTitle, WealthPoints, PreviousWealth, StartingWealth, AnnualWealth, GovTypeID, UnreadMessages, FlagPic, HullPic, SpaceStationPic, Contacts, Colour, Red, Green, Blue, Password, ThemeID, ClassThemeID, MissileThemeID, GroundThemeID, SystemThemeID, DesignThemeID, ColonyDensity, \r\n                        DisplayGrade, ShowHighlight, MapRed, MapGreen, MapBlue, FleetViewOption, SelectRace, FleetsVisible, LastMapSystemViewed, chkAllowAny, chkAutoAssign, chkTons, StandardTour, LastAssignment, CurrentXenophobia, AcademyCrewmen, MaintenanceCapacity, OrdnanceCapacity, FighterCapacity, ShipBuilding, FuelProduction, ConstructionProduction, MaximumOrbitalMiningDiameter,\r\n                        OrdnanceProduction, FighterProduction, MineProduction, Research, PlanetarySensorStrength, TrainingLevel, GUStrength, TerraformingRate, ColonizationSkill, StartTechPoints, StartBuildPoints, WealthCreationRate, EconomicProdModifier, ShipyardOperations, MSPProduction, MaxRefuellingRate, MaxOrdnanceTransferRate, UnderwayReplenishmentRate, Hostile, Neutral, Friendly,\r\n                        Allied, Civilian, HideCMCPop, PopByFunction, chkPlanets, chkDwarf, chkMoons, chkAst, chkWP, chkStarOrbits, chkPlanetOrbits, chkDwarfOrbits, chkMoonOrbits, chkAsteroidOrbits, chkStarNames, chkPlanetNames, chkDwarfNames, chkMoonNames, chkAstNames, chkFleets, chkMoveTail, chkColonies, chkCentre, chkSL, chkWaypoint, chkOrder, chkNoOverlap, chkActiveSensors, chkML, chkGroundSurveyLocations,\r\n                        chkTracking, chkActiveOnly, chkShowDist, chkSBSurvey, chkMinerals, chkCometPath, chkAstColOnly, chkAstMinOnly, chkTAD, chkFiringRanges, chkAllRace, chkDisplayAllForms, chkSalvoOrigin, chkSalvoTarget, chkEscorts, chkFireControlRange, PassiveSensor, ActiveSensor, DetRange, chkHideIDs, chkHideSL, chkEvents, chkPackets, chkMPC, chkLifepods, chkWrecks, chkHostileSensors, chkGeoPoints, chkBearing, \r\n                        chkCoordinates, chkLostContacts, chkLostContactsOneYear, chkLostContactsOneDay, chkSystemOnly, chkShowCivilianOOB, chkHostile, chkFriendly, chkAllied, chkNeutral, chkCivilian, chkContactsCurrentSystem, chkPassive10, chkPassive100, chkPassive1000, chkPassive10000,  chkUnexJP, chkJPSurveyStatus, chkSurveyLocationPoints, chkSurveyedSystemBodies, chkHabRangeWorlds, chkHabRangeWorldsLowG, chkLowCCNormalG, chkMediumCCNormalG, chkLowCCLowG, chkNumCometsPlanetlessSystem,\r\n                        chkMediumCCLowG, chkDistanceFromSelected, chkWarshipLocation, chkAllFleetLocations, chkKnownAlienForces, chkAlienControlledSystems, chkPopulatedSystem, chkBlocked, chkShipyardComplexes, chkNavalHeadquarters, chkSectors, chkPossibleDormantJP, chkSecurityStatus, chkDiscoveryDate, RankScientist, RankAdministrator, CargoShuttleLoadModifier, GroundFormationConstructionRate, \r\n                        ResearchTargetCost, CurrentResearchTotal, NeutralRace, chkMilitaryRestricted, chkDisplayMineralSearch, ShowPopStar, ShowPopSystemBody, TonnageSent, LastProgressionOrder, chkHideOrbitFleets) \r\n                        VALUES(@RaceID, @GameID, @ConventionalStart, @NPR, @SpecialNPRID, @PreIndustrial, @RaceGridSize, @RaceName, @RaceTitle, @WealthPoints, @PreviousWealth, @StartingWealth, @AnnualWealth, @GovTypeID, @UnreadMessages, @FlagPic, @HullPic, @SpaceStationPic, @Contacts, @Colour, @Red, @Green, @Blue, @Password, @ThemeID, @ClassThemeID, @MissileThemeID, @GroundThemeID, @SystemThemeID, @DesignThemeID, @ColonyDensity,\r\n                        @DisplayGrade, @ShowHighlight, @MapRed, @MapGreen, @MapBlue, @FleetViewOption, @SelectRace, @FleetsVisible, @LastMapSystemViewed, @chkAllowAny, @chkAutoAssign, @chkTons, @StandardTour, @LastAssignment, @CurrentXenophobia, @AcademyCrewmen, @MaintenanceCapacity, @OrdnanceCapacity, @FighterCapacity, @ShipBuilding, @FuelProduction, @ConstructionProduction, @MaximumOrbitalMiningDiameter,\r\n                        @OrdnanceProduction, @FighterProduction, @MineProduction, @Research, @PlanetarySensorStrength, @TrainingLevel, @GUStrength, @TerraformingRate, @ColonizationSkill, @StartTechPoints, @StartBuildPoints, @WealthCreationRate, @EconomicProdModifier, @ShipyardOperations, @MSPProduction, @MaxRefuellingRate, @MaxOrdnanceTransferRate, @UnderwayReplenishmentRate, @Hostile, @Neutral, @Friendly,\r\n                        @Allied, @Civilian, @HideCMCPop, @PopByFunction, @chkPlanets, @chkDwarf, @chkMoons, @chkAst, @chkWP, @chkStarOrbits, @chkPlanetOrbits, @chkDwarfOrbits, @chkMoonOrbits, @chkAsteroidOrbits, @chkStarNames, @chkPlanetNames, @chkDwarfNames, @chkMoonNames, @chkAstNames, @chkFleets, @chkMoveTail, @chkColonies, @chkCentre, @chkSL, @chkWaypoint, @chkOrder, @chkNoOverlap, @chkActiveSensors, @chkML, @chkGroundSurveyLocations,\r\n                        @chkTracking, @chkActiveOnly, @chkShowDist, @chkSBSurvey, @chkMinerals, @chkCometPath, @chkAstColOnly, @chkAstMinOnly, @chkTAD, @chkFiringRanges, @chkAllRace, @chkDisplayAllForms, @chkSalvoOrigin, @chkSalvoTarget, @chkEscorts, @chkFireControlRange, @PassiveSensor, @ActiveSensor, @DetRange, @chkHideIDs, @chkHideSL, @chkEvents, @chkPackets, @chkMPC, @chkLifepods, @chkWrecks, @chkHostileSensors, @chkGeoPoints, @chkBearing,\r\n                        @chkCoordinates, @chkLostContacts, @chkLostContactsOneYear, @chkLostContactsOneDay, @chkSystemOnly, @chkShowCivilianOOB, @chkHostile, @chkFriendly, @chkAllied, @chkNeutral, @chkCivilian, @chkContactsCurrentSystem, @chkPassive10, @chkPassive100, @chkPassive1000, @chkPassive10000, @chkUnexJP, @chkJPSurveyStatus, @chkSurveyLocationPoints, @chkSurveyedSystemBodies, @chkHabRangeWorlds, @chkHabRangeWorldsLowG, @chkLowCCNormalG, @chkMediumCCNormalG, @chkLowCCLowG, @chkNumCometsPlanetlessSystem,\r\n                        @chkMediumCCLowG, @chkDistanceFromSelected, @chkWarshipLocation, @chkAllFleetLocations, @chkKnownAlienForces, @chkAlienControlledSystems, @chkPopulatedSystem, @chkBlocked, @chkShipyardComplexes, @chkNavalHeadquarters, @chkSectors, @chkPossibleDormantJP, @chkSecurityStatus, @chkDiscoveryDate, @RankScientist, @RankAdministrator, @CargoShuttleLoadModifier, @GroundFormationConstructionRate, \r\n                        @ResearchTargetCost, @CurrentResearchTotal, @NeutralRace, @chkMilitaryRestricted, @chkDisplayMineralSearch, @ShowPopStar, @ShowPopSystemBody, @TonnageSent, @LastProgressionOrder, @chkHideOrbitFleets)";
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ConventionalStart", dataObj.ConventionalStart);
							sqliteCommand.Parameters.AddWithValue("@NPR", dataObj.NPR);
							sqliteCommand.Parameters.AddWithValue("@SpecialNPRID", dataObj.SpecialNPRID);
							sqliteCommand.Parameters.AddWithValue("@PreIndustrial", dataObj.PreIndustrial);
							sqliteCommand.Parameters.AddWithValue("@RaceGridSize", dataObj.RaceGridSize);
							sqliteCommand.Parameters.AddWithValue("@RaceName", dataObj.RaceName);
							sqliteCommand.Parameters.AddWithValue("@RaceTitle", dataObj.RaceTitle);
							sqliteCommand.Parameters.AddWithValue("@WealthPoints", dataObj.WealthPoints);
							sqliteCommand.Parameters.AddWithValue("@PreviousWealth", dataObj.PreviousWealth);
							sqliteCommand.Parameters.AddWithValue("@StartingWealth", dataObj.StartingWealth);
							sqliteCommand.Parameters.AddWithValue("@AnnualWealth", dataObj.AnnualWealth);
							sqliteCommand.Parameters.AddWithValue("@GovTypeID", dataObj.GovTypeID);
							sqliteCommand.Parameters.AddWithValue("@UnreadMessages", dataObj.UnreadMessages);
							sqliteCommand.Parameters.AddWithValue("@FlagPic", dataObj.FlagPic);
							sqliteCommand.Parameters.AddWithValue("@HullPic", dataObj.HullPic);
							sqliteCommand.Parameters.AddWithValue("@SpaceStationPic", dataObj.SpaceStationPic);
							sqliteCommand.Parameters.AddWithValue("@Contacts", dataObj.Contacts);
							sqliteCommand.Parameters.AddWithValue("@Colour", dataObj.Colour);
							sqliteCommand.Parameters.AddWithValue("@Red", dataObj.Red);
							sqliteCommand.Parameters.AddWithValue("@Green", dataObj.Green);
							sqliteCommand.Parameters.AddWithValue("@Blue", dataObj.Blue);
							sqliteCommand.Parameters.AddWithValue("@Password", dataObj.Password);
							sqliteCommand.Parameters.AddWithValue("@ThemeID", dataObj.ThemeID);
							sqliteCommand.Parameters.AddWithValue("@ClassThemeID", dataObj.ClassThemeID);
							sqliteCommand.Parameters.AddWithValue("@MissileThemeID", dataObj.MissileThemeID);
							sqliteCommand.Parameters.AddWithValue("@GroundThemeID", dataObj.GroundThemeID);
							sqliteCommand.Parameters.AddWithValue("@SystemThemeID", dataObj.SystemThemeID);
							sqliteCommand.Parameters.AddWithValue("@DesignThemeID", dataObj.DesignThemeID);
							sqliteCommand.Parameters.AddWithValue("@DisplayGrade", dataObj.DisplayGrade);
							sqliteCommand.Parameters.AddWithValue("@ShowHighlight", dataObj.ShowHighlight);
							sqliteCommand.Parameters.AddWithValue("@MapRed", dataObj.MapRed);
							sqliteCommand.Parameters.AddWithValue("@MapGreen", dataObj.MapGreen);
							sqliteCommand.Parameters.AddWithValue("@MapBlue", dataObj.MapBlue);
							sqliteCommand.Parameters.AddWithValue("@FleetViewOption", dataObj.FleetViewOption);
							sqliteCommand.Parameters.AddWithValue("@SelectRace", dataObj.SelectRace);
							sqliteCommand.Parameters.AddWithValue("@FleetsVisible", dataObj.FleetsVisible);
							sqliteCommand.Parameters.AddWithValue("@LastMapSystemViewed", dataObj.LastMapSystemViewed);
							sqliteCommand.Parameters.AddWithValue("@chkAllowAny", dataObj.chkAllowAny);
							sqliteCommand.Parameters.AddWithValue("@chkAutoAssign", dataObj.chkAutoAssign);
							sqliteCommand.Parameters.AddWithValue("@chkTons", dataObj.chkTons);
							sqliteCommand.Parameters.AddWithValue("@StandardTour", dataObj.StandardTour);
							sqliteCommand.Parameters.AddWithValue("@LastAssignment", dataObj.LastAssignment);
							sqliteCommand.Parameters.AddWithValue("@CurrentXenophobia", dataObj.CurrentXenophobia);
							sqliteCommand.Parameters.AddWithValue("@AcademyCrewmen", dataObj.AcademyCrewmen);
							sqliteCommand.Parameters.AddWithValue("@MaintenanceCapacity", dataObj.MaintenanceCapacity);
							sqliteCommand.Parameters.AddWithValue("@OrdnanceCapacity", dataObj.OrdnanceCapacity);
							sqliteCommand.Parameters.AddWithValue("@FighterCapacity", dataObj.FighterCapacity);
							sqliteCommand.Parameters.AddWithValue("@ShipBuilding", dataObj.ShipBuilding);
							sqliteCommand.Parameters.AddWithValue("@FuelProduction", dataObj.FuelProduction);
							sqliteCommand.Parameters.AddWithValue("@ConstructionProduction",
								dataObj.ConstructionProduction);
							sqliteCommand.Parameters.AddWithValue("@MaximumOrbitalMiningDiameter",
								dataObj.MaximumOrbitalMiningDiameter);
							sqliteCommand.Parameters.AddWithValue("@OrdnanceProduction", dataObj.OrdnanceProduction);
							sqliteCommand.Parameters.AddWithValue("@FighterProduction", dataObj.FighterProduction);
							sqliteCommand.Parameters.AddWithValue("@MineProduction", dataObj.MineProduction);
							sqliteCommand.Parameters.AddWithValue("@Research", dataObj.Research);
							sqliteCommand.Parameters.AddWithValue("@PlanetarySensorStrength",
								dataObj.PlanetarySensorStrength);
							sqliteCommand.Parameters.AddWithValue("@TrainingLevel", dataObj.TrainingLevel);
							sqliteCommand.Parameters.AddWithValue("@GUStrength", dataObj.GUStrength);
							sqliteCommand.Parameters.AddWithValue("@TerraformingRate", dataObj.TerraformingRate);
							sqliteCommand.Parameters.AddWithValue("@ColonizationSkill", dataObj.ColonizationSkill);
							sqliteCommand.Parameters.AddWithValue("@StartTechPoints", dataObj.StartTechPoints);
							sqliteCommand.Parameters.AddWithValue("@StartBuildPoints", dataObj.StartBuildPoints);
							sqliteCommand.Parameters.AddWithValue("@WealthCreationRate", dataObj.WealthCreationRate);
							sqliteCommand.Parameters.AddWithValue("@EconomicProdModifier",
								dataObj.EconomicProdModifier);
							sqliteCommand.Parameters.AddWithValue("@ShipyardOperations", dataObj.ShipyardOperations);
							sqliteCommand.Parameters.AddWithValue("@MSPProduction", dataObj.MSPProduction);
							sqliteCommand.Parameters.AddWithValue("@MaxRefuellingRate", dataObj.MaxRefuellingRate);
							sqliteCommand.Parameters.AddWithValue("@MaxOrdnanceTransferRate",
								dataObj.MaxOrdnanceTransferRate);
							sqliteCommand.Parameters.AddWithValue("@UnderwayReplenishmentRate",
								dataObj.UnderwayReplenishmentRate);
							sqliteCommand.Parameters.AddWithValue("@Hostile", dataObj.Hostile);
							sqliteCommand.Parameters.AddWithValue("@Neutral", dataObj.Neutral);
							sqliteCommand.Parameters.AddWithValue("@Friendly", dataObj.Friendly);
							sqliteCommand.Parameters.AddWithValue("@Allied", dataObj.Allied);
							sqliteCommand.Parameters.AddWithValue("@Civilian", dataObj.Civilian);
							sqliteCommand.Parameters.AddWithValue("@HideCMCPop", dataObj.HideCMCPop);
							sqliteCommand.Parameters.AddWithValue("@ShowPopStar", dataObj.ShowPopStar);
							sqliteCommand.Parameters.AddWithValue("@ShowPopSystemBody", dataObj.ShowPopSystemBody);
							sqliteCommand.Parameters.AddWithValue("@PopByFunction", dataObj.PopByFunction);
							sqliteCommand.Parameters.AddWithValue("@chkPlanets", dataObj.chkPlanets);
							sqliteCommand.Parameters.AddWithValue("@chkDwarf", dataObj.chkDwarf);
							sqliteCommand.Parameters.AddWithValue("@chkMoons", dataObj.chkMoons);
							sqliteCommand.Parameters.AddWithValue("@chkAst", dataObj.chkAst);
							sqliteCommand.Parameters.AddWithValue("@chkWP", dataObj.chkWP);
							sqliteCommand.Parameters.AddWithValue("@chkStarOrbits", dataObj.chkStarOrbits);
							sqliteCommand.Parameters.AddWithValue("@chkPlanetOrbits", dataObj.chkPlanetOrbits);
							sqliteCommand.Parameters.AddWithValue("@chkDwarfOrbits", dataObj.chkDwarfOrbits);
							sqliteCommand.Parameters.AddWithValue("@chkMoonOrbits", dataObj.chkMoonOrbits);
							sqliteCommand.Parameters.AddWithValue("@chkAsteroidOrbits", dataObj.chkAsteroidOrbits);
							sqliteCommand.Parameters.AddWithValue("@chkStarNames", dataObj.chkStarNames);
							sqliteCommand.Parameters.AddWithValue("@chkPlanetNames", dataObj.chkPlanetNames);
							sqliteCommand.Parameters.AddWithValue("@chkDwarfNames", dataObj.chkDwarfNames);
							sqliteCommand.Parameters.AddWithValue("@chkMoonNames", dataObj.chkMoonNames);
							sqliteCommand.Parameters.AddWithValue("@chkAstNames", dataObj.chkAstNames);
							sqliteCommand.Parameters.AddWithValue("@chkFleets", dataObj.chkFleets);
							sqliteCommand.Parameters.AddWithValue("@chkMoveTail", dataObj.chkMoveTail);
							sqliteCommand.Parameters.AddWithValue("@chkColonies", dataObj.chkColonies);
							sqliteCommand.Parameters.AddWithValue("@chkCentre", dataObj.chkCentre);
							sqliteCommand.Parameters.AddWithValue("@chkSL", dataObj.chkSL);
							sqliteCommand.Parameters.AddWithValue("@chkWaypoint", dataObj.chkWaypoint);
							sqliteCommand.Parameters.AddWithValue("@chkOrder", dataObj.chkOrder);
							sqliteCommand.Parameters.AddWithValue("@chkNoOverlap", dataObj.chkNoOverlap);
							sqliteCommand.Parameters.AddWithValue("@chkActiveSensors", dataObj.chkActiveSensors);
							sqliteCommand.Parameters.AddWithValue("@chkTracking", dataObj.chkTracking);
							sqliteCommand.Parameters.AddWithValue("@chkActiveOnly", dataObj.chkActiveOnly);
							sqliteCommand.Parameters.AddWithValue("@chkShowDist", dataObj.chkShowDist);
							sqliteCommand.Parameters.AddWithValue("@chkSBSurvey", dataObj.chkSBSurvey);
							sqliteCommand.Parameters.AddWithValue("@chkMinerals", dataObj.chkMinerals);
							sqliteCommand.Parameters.AddWithValue("@chkCometPath", dataObj.chkCometPath);
							sqliteCommand.Parameters.AddWithValue("@chkAstColOnly", dataObj.chkAstColOnly);
							sqliteCommand.Parameters.AddWithValue("@chkAstMinOnly", dataObj.chkAstMinOnly);
							sqliteCommand.Parameters.AddWithValue("@chkTAD", dataObj.chkTAD);
							sqliteCommand.Parameters.AddWithValue("@chkFiringRanges", dataObj.chkFiringRanges);
							sqliteCommand.Parameters.AddWithValue("@chkAllRace", dataObj.chkAllRace);
							sqliteCommand.Parameters.AddWithValue("@chkDisplayAllForms", dataObj.chkDisplayAllForms);
							sqliteCommand.Parameters.AddWithValue("@chkSalvoOrigin", dataObj.chkSalvoOrigin);
							sqliteCommand.Parameters.AddWithValue("@chkSalvoTarget", dataObj.chkSalvoTarget);
							sqliteCommand.Parameters.AddWithValue("@chkEscorts", dataObj.chkEscorts);
							sqliteCommand.Parameters.AddWithValue("@chkFireControlRange", dataObj.chkFireControlRange);
							sqliteCommand.Parameters.AddWithValue("@PassiveSensor", dataObj.PassiveSensor);
							sqliteCommand.Parameters.AddWithValue("@ActiveSensor", dataObj.ActiveSensor);
							sqliteCommand.Parameters.AddWithValue("@DetRange", dataObj.DetRange);
							sqliteCommand.Parameters.AddWithValue("@chkHideIDs", dataObj.chkHideIDs);
							sqliteCommand.Parameters.AddWithValue("@chkHideSL", dataObj.chkHideSL);
							sqliteCommand.Parameters.AddWithValue("@chkEvents", dataObj.chkEvents);
							sqliteCommand.Parameters.AddWithValue("@chkPackets", dataObj.chkPackets);
							sqliteCommand.Parameters.AddWithValue("@chkMPC", dataObj.chkMPC);
							sqliteCommand.Parameters.AddWithValue("@chkLifepods", dataObj.chkLifepods);
							sqliteCommand.Parameters.AddWithValue("@chkWrecks", dataObj.chkWrecks);
							sqliteCommand.Parameters.AddWithValue("@chkHostileSensors", dataObj.chkHostileSensors);
							sqliteCommand.Parameters.AddWithValue("@chkGeoPoints", dataObj.chkGeoPoints);
							sqliteCommand.Parameters.AddWithValue("@chkBearing", dataObj.chkBearing);
							sqliteCommand.Parameters.AddWithValue("@chkCoordinates", dataObj.chkCoordinates);
							sqliteCommand.Parameters.AddWithValue("@chkLostContacts", dataObj.chkLostContacts);
							sqliteCommand.Parameters.AddWithValue("@chkLostContactsOneYear",
								dataObj.chkLostContactsOneYear);
							sqliteCommand.Parameters.AddWithValue("@chkLostContactsOneDay",
								dataObj.chkLostContactsOneDay);
							sqliteCommand.Parameters.AddWithValue("@chkSystemOnly", dataObj.chkSystemOnly);
							sqliteCommand.Parameters.AddWithValue("@chkShowCivilianOOB", dataObj.chkShowCivilianOOB);
							sqliteCommand.Parameters.AddWithValue("@chkHostile", dataObj.chkHostile);
							sqliteCommand.Parameters.AddWithValue("@chkFriendly", dataObj.chkFriendly);
							sqliteCommand.Parameters.AddWithValue("@chkAllied", dataObj.chkAllied);
							sqliteCommand.Parameters.AddWithValue("@chkNeutral", dataObj.chkNeutral);
							sqliteCommand.Parameters.AddWithValue("@chkCivilian", dataObj.chkCivilian);
							sqliteCommand.Parameters.AddWithValue("@chkContactsCurrentSystem",
								dataObj.chkContactsCurrentSystem);
							sqliteCommand.Parameters.AddWithValue("@chkPassive10", dataObj.chkPassive10);
							sqliteCommand.Parameters.AddWithValue("@chkPassive100", dataObj.chkPassive100);
							sqliteCommand.Parameters.AddWithValue("@chkPassive1000", dataObj.chkPassive1000);
							sqliteCommand.Parameters.AddWithValue("@chkPassive10000", dataObj.chkPassive10000);
							sqliteCommand.Parameters.AddWithValue("@chkUnexJP", dataObj.chkUnexJP);
							sqliteCommand.Parameters.AddWithValue("@chkJPSurveyStatus", dataObj.chkJPSurveyStatus);
							sqliteCommand.Parameters.AddWithValue("@chkSurveyLocationPoints",
								dataObj.chkSurveyLocationPoints);
							sqliteCommand.Parameters.AddWithValue("@chkSurveyedSystemBodies",
								dataObj.chkSurveyedSystemBodies);
							sqliteCommand.Parameters.AddWithValue("@chkHabRangeWorlds", dataObj.chkHabRangeWorlds);
							sqliteCommand.Parameters.AddWithValue("@chkHabRangeWorldsLowG",
								dataObj.chkHabRangeWorldsLowG);
							sqliteCommand.Parameters.AddWithValue("@chkLowCCNormalG", dataObj.chkLowCCNormalG);
							sqliteCommand.Parameters.AddWithValue("@chkMediumCCNormalG", dataObj.chkMediumCCNormalG);
							sqliteCommand.Parameters.AddWithValue("@chkLowCCLowG", dataObj.chkLowCCLowG);
							sqliteCommand.Parameters.AddWithValue("@chkMediumCCLowG", dataObj.chkMediumCCLowG);
							sqliteCommand.Parameters.AddWithValue("@chkDistanceFromSelected",
								dataObj.chkDistanceFromSelected);
							sqliteCommand.Parameters.AddWithValue("@chkWarshipLocation", dataObj.chkWarshipLocation);
							sqliteCommand.Parameters.AddWithValue("@chkAllFleetLocations",
								dataObj.chkAllFleetLocations);
							sqliteCommand.Parameters.AddWithValue("@chkKnownAlienForces", dataObj.chkKnownAlienForces);
							sqliteCommand.Parameters.AddWithValue("@chkAlienControlledSystems",
								dataObj.chkAlienControlledSystems);
							sqliteCommand.Parameters.AddWithValue("@chkPopulatedSystem", dataObj.chkPopulatedSystem);
							sqliteCommand.Parameters.AddWithValue("@chkBlocked", dataObj.chkBlocked);
							sqliteCommand.Parameters.AddWithValue("@chkMilitaryRestricted",
								dataObj.chkMilitaryRestricted);
							sqliteCommand.Parameters.AddWithValue("@chkDisplayMineralSearch",
								dataObj.chkDisplayMineralSearch);
							sqliteCommand.Parameters.AddWithValue("@chkShipyardComplexes",
								dataObj.chkShipyardComplexes);
							sqliteCommand.Parameters.AddWithValue("@chkNavalHeadquarters",
								dataObj.chkNavalHeadquarters);
							sqliteCommand.Parameters.AddWithValue("@chkSectors", dataObj.chkSectors);
							sqliteCommand.Parameters.AddWithValue("@chkPossibleDormantJP",
								dataObj.chkPossibleDormantJP);
							sqliteCommand.Parameters.AddWithValue("@chkSecurityStatus", dataObj.chkSecurityStatus);
							sqliteCommand.Parameters.AddWithValue("@chkDiscoveryDate", dataObj.chkDiscoveryDate);
							sqliteCommand.Parameters.AddWithValue("@chkML", dataObj.chkML);
							sqliteCommand.Parameters.AddWithValue("@chkGroundSurveyLocations",
								dataObj.chkGroundSurveyLocations);
							sqliteCommand.Parameters.AddWithValue("@chkHideOrbitFleets", dataObj.chkHideOrbitFleets);
							sqliteCommand.Parameters.AddWithValue("@chkNumCometsPlanetlessSystem",
								dataObj.chkNumCometsPlanetlessSystem);
							sqliteCommand.Parameters.AddWithValue("@RankScientist", dataObj.RankScientist);
							sqliteCommand.Parameters.AddWithValue("@RankAdministrator", dataObj.RankAdministrator);
							sqliteCommand.Parameters.AddWithValue("@CargoShuttleLoadModifier",
								dataObj.CargoShuttleLoadModifier);
							sqliteCommand.Parameters.AddWithValue("@GroundFormationConstructionRate",
								dataObj.GroundFormationConstructionRate);
							sqliteCommand.Parameters.AddWithValue("@ResearchTargetCost", dataObj.ResearchTargetCost);
							sqliteCommand.Parameters.AddWithValue("@CurrentResearchTotal",
								dataObj.CurrentResearchTotal);
							sqliteCommand.Parameters.AddWithValue("@ColonyDensity", dataObj.ColonyDensity);
							sqliteCommand.Parameters.AddWithValue("@NeutralRace", dataObj.NeutralRace);
							sqliteCommand.Parameters.AddWithValue("@TonnageSent", dataObj.TonnageSent);
							sqliteCommand.Parameters.AddWithValue("@LastProgressionOrder",
								dataObj.LastProgressionOrder);
							sqliteCommand.ExecuteNonQuery();
						}
						catch (Exception exception_)
						{
							GClass202.smethod_69(exception_, 3235, dataObj.RaceID);
						}
						foreach (var dataObj1 in dataObj.ResearchQueueStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ResearchQueue (GameID, PopulationID, TechSystemID, CurrentProjectID, ResearchOrder ) VALUES ( @GameID, @PopulationID, @TechSystemID, @CurrentProjectID, @ResearchOrder )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj1.PopulationID);
							sqliteCommand.Parameters.AddWithValue("@TechSystemID", dataObj1.TechSystemID);
							sqliteCommand.Parameters.AddWithValue("@CurrentProjectID", dataObj1.CurrentProjectID);
							sqliteCommand.Parameters.AddWithValue("@ResearchOrder", dataObj1.ResearchOrder);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.PausedResearchStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_PausedResearch (TechSystemID, RaceID, GameID, PointsAccumulated ) VALUES ( @TechSystemID, @RaceID, @GameID, @PointsAccumulated )";
							sqliteCommand.Parameters.AddWithValue("@TechSystemID", dataObj1.TechSystemID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@PointsAccumulated", dataObj1.PointsAccumulated);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.SwarmResearchStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_SwarmResearch (GameID, RaceID, ResearchPoints, TechSystemID ) VALUES ( @GameID, @RaceID, @ResearchPoints, @TechSystemID )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@ResearchPoints", dataObj1.ResearchPoints);
							sqliteCommand.Parameters.AddWithValue("@TechSystemID", dataObj1.TechSystemID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.KnownRuinRaceStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_KnownRuinRace (RuinRaceID, RaceID, GameID ) VALUES ( @RuinRaceID, @RaceID, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@RuinRaceID", dataObj1.RuinRaceID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.GroundUnitSeriesStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_GroundUnitSeries (GroundUnitSeriesID, Description, RaceID, GameID ) VALUES ( @GroundUnitSeriesID, @Description, @RaceID, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@GroundUnitSeriesID", dataObj1.GroundUnitSeriesID);
							sqliteCommand.Parameters.AddWithValue("@Description", dataObj1.Description);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.GroundUnitSeriesClassStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_GroundUnitSeriesClass (GroundUnitSeriesID, GroundUnitClassID, Priority, RaceID, GameID ) VALUES ( @GroundUnitSeriesID, @GroundUnitClassID, @Priority, @RaceID, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@GroundUnitSeriesID", dataObj1.GroundUnitSeriesID);
							sqliteCommand.Parameters.AddWithValue("@GroundUnitClassID", dataObj1.GroundUnitClassID);
							sqliteCommand.Parameters.AddWithValue("@Priority", dataObj1.Priority);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.BannedBodiesStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_BannedBodies (SystemBodyID, RaceID, GameID ) VALUES ( @SystemBodyID, @RaceID, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@SystemBodyID", dataObj1.SystemBodyID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.RaceNameThemesStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_RaceNameThemes (GameID, RaceID, NameThemeID, Chance ) VALUES ( @GameID, @RaceID, @NameThemeID, @Chance )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@NameThemeID", dataObj1.NameThemeID);
							sqliteCommand.Parameters.AddWithValue("@Chance", dataObj1.Chance);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.RaceGroundCombatStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_RaceGroundCombat (RaceID, SystemBodyID, ConsecutiveCombatRounds, GameID ) VALUES ( @RaceID, @SystemBodyID, @ConsecutiveCombatRounds, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@SystemBodyID", dataObj1.SystemBodyID);
							sqliteCommand.Parameters.AddWithValue("@ConsecutiveCombatRounds",
								dataObj1.ConsecutiveCombatRounds);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.RaceOperationalGroupElementsStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_RaceOperationalGroupElements (GameID, RaceID, OperationalGroupID, NumShips, AutomatedDesignID, KeyElement ) VALUES (@GameID, @RaceID, @OperationalGroupID, @NumShips, @AutomatedDesignID, @KeyElement )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@OperationalGroupID", dataObj1.OperationalGroupID);
							sqliteCommand.Parameters.AddWithValue("@NumShips", dataObj1.NumShips);
							sqliteCommand.Parameters.AddWithValue("@AutomatedDesignID", dataObj1.AutomatedDesignID);
							sqliteCommand.Parameters.AddWithValue("@KeyElement", dataObj1.KeyElement);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_2)
			{
				GClass202.smethod_68(exception_2, 1429);
			}
		}
	}
	
	/// <summary>
	/// method_56
	/// </summary>
	public class SaveSystem
	{
		public struct SystemData
		{
			public int SystemID;
			public int SystemNumber;
			public double Age;
			public int AbundanceModifier;
			public int Stars;
			public int JumpPointSurveyPoints;
			public int SystemTypeID;
			public int DustDensity;
			public bool SolSystem;
			public int NoSensorChecks;
		}
		SystemData[] SystemStore;


		public SaveSystem(GClass0 game)
		{
			SystemStore = new SystemData[game.dictionary_9.Count];
			int i = 0;
			foreach (GClass178 gclass in game.dictionary_9.Values)
			{
				var dataObj = new SystemData()
				{
					SystemID = gclass.int_0,
					SystemNumber = gclass.int_1,
					Age = gclass.double_0,
					AbundanceModifier = gclass.int_2,
					Stars = gclass.int_3,
					JumpPointSurveyPoints = gclass.int_4,
					SystemTypeID = gclass.int_5,
					DustDensity = gclass.int_6,
					SolSystem = gclass.bool_0,
					NoSensorChecks = gclass.int_7,
				};
				SystemStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_System WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in SystemStore )
					{
						try
						{
							sqliteCommand.CommandText = "INSERT INTO FCT_System (SystemID, SystemNumber, Age, AbundanceModifier, Stars, GameID, JumpPointSurveyPoints, SystemTypeID, DustDensity, SolSystem, NoSensorChecks ) \r\n                                VALUES ( @SystemID, @SystemNumber, @Age, @AbundanceModifier, @Stars, @GameID, @JumpPointSurveyPoints, @SystemTypeID, @DustDensity, @SolSystem, @NoSensorChecks)";
							sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
							sqliteCommand.Parameters.AddWithValue("@SystemNumber", dataObj.SystemNumber);
							sqliteCommand.Parameters.AddWithValue("@Age", dataObj.Age);
							sqliteCommand.Parameters.AddWithValue("@AbundanceModifier", dataObj.AbundanceModifier);
							sqliteCommand.Parameters.AddWithValue("@Stars", dataObj.Stars);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@JumpPointSurveyPoints", dataObj.JumpPointSurveyPoints);
							sqliteCommand.Parameters.AddWithValue("@SystemTypeID", dataObj.SystemTypeID);
							sqliteCommand.Parameters.AddWithValue("@DustDensity", dataObj.DustDensity);
							sqliteCommand.Parameters.AddWithValue("@SolSystem", dataObj.SolSystem);
							sqliteCommand.Parameters.AddWithValue("@NoSensorChecks", dataObj.NoSensorChecks);
							sqliteCommand.ExecuteNonQuery();
						}
						catch (Exception exception_)
						{
							GClass202.smethod_69(exception_, 3249, dataObj.SystemID);
						}
					}
				}

			}
			catch (Exception exception_2)
			{
				GClass202.smethod_68(exception_2, 1430);
			}
		}
	}
	
	/// <summary>
	/// method_57
	/// </summary>
	public class SaveRaceMedals
	{
		public struct RaceMedalsData
		{
			public int RaceID;
			public string MedalFileName;
			public string MedalName;
			public string MedalDescription;
			public int MedalPoints;
			public bool MultipleAwards;
			public int MedalID;
			public string Abbreviation;
		}
		RaceMedalsData[] RaceMedalsStore;


		public SaveRaceMedals(GClass0 game)
		{
			RaceMedalsStore = new RaceMedalsData[game.dictionary_32.Count];
			int i = 0;
			foreach (GClass41 gclass in game.dictionary_32.Values)
			{
				var dataObj = new RaceMedalsData()
				{
					RaceID = gclass.gclass21_0.RaceID,
					MedalFileName = gclass.string_2,
					MedalName = gclass.string_0,
					MedalDescription = gclass.string_1,
					MedalPoints = gclass.int_0,
					MultipleAwards = gclass.bool_0,
					MedalID = gclass.int_1,
					Abbreviation = gclass.string_3,
				};
				RaceMedalsStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_RaceMedals WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in RaceMedalsStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_RaceMedals (RaceID, MedalFileName, MedalName, MedalDescription, MedalPoints, GameID, MultipleAwards, MedalID, Abbreviation ) \r\n                        VALUES ( @RaceID, @MedalFileName, @MedalName, @MedalDescription, @MedalPoints, @GameID, @MultipleAwards, @MedalID, @Abbreviation )";
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@MedalFileName", dataObj.MedalFileName);
						sqliteCommand.Parameters.AddWithValue("@MedalName", dataObj.MedalName);
						sqliteCommand.Parameters.AddWithValue("@MedalDescription", dataObj.MedalDescription);
						sqliteCommand.Parameters.AddWithValue("@MedalPoints", dataObj.MedalPoints);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@MultipleAwards", dataObj.MultipleAwards);
						sqliteCommand.Parameters.AddWithValue("@MedalID", dataObj.MedalID);
						sqliteCommand.Parameters.AddWithValue("@Abbreviation", dataObj.Abbreviation);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1431);
			}
		}
	}
	
	/// <summary>
	/// method_58
	/// </summary>
	public class SaveMedalConditionAssignment
	{
		public struct MedalConditionAssignmentData
		{
			public int MedalConditionID;
			public int MedalID;
		}
		MedalConditionAssignmentData[] MedalConditionAssignmentStore;


		public SaveMedalConditionAssignment(GClass0 game)
		{
			MedalConditionAssignmentStore = new MedalConditionAssignmentData[game.list_1.Count];
			int i = 0;
			foreach (GClass43 gclass in game.list_1)
			{
				var dataObj = new MedalConditionAssignmentData()
				{
					MedalConditionID = gclass.gclass42_0.int_0,
					MedalID = gclass.gclass41_0.int_1,
				};
				MedalConditionAssignmentStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MedalConditionAssignment WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in MedalConditionAssignmentStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_MedalConditionAssignment (MedalConditionID, MedalID, GameID ) \r\n                        VALUES ( @MedalConditionID, @MedalID, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@MedalConditionID", dataObj.MedalConditionID);
						sqliteCommand.Parameters.AddWithValue("@MedalID", dataObj.MedalID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1432);
			}
		}
	}


	public class SaveWindowPosition
	{
		public WindowPosition[] WindowPositions;

		public struct WindowPosition
		{
			public string WindowName;
			public int Left;
			public int Top;
		}

		public SaveWindowPosition(GClass0 game)
		{
			WindowPositions = new WindowPosition[game.dictionary_73.Count];
			int i = 0;
			foreach (GClass184 gclass in game.dictionary_73.Values)
			{
				WindowPosition dataObj = new WindowPosition()
				{
					WindowName = gclass.string_0,
					Left = gclass.int_0,
					Top = gclass.int_1
				};
				WindowPositions[i] = dataObj;
				i++;

			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_WindowPosition WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var winPos in WindowPositions)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_WindowPosition (WindowName, Left, Top, GameID ) \r\n                        VALUES ( @WindowName, @Left, @Top, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@WindowName", winPos.WindowName);
						sqliteCommand.Parameters.AddWithValue("@Left", winPos.Left);
						sqliteCommand.Parameters.AddWithValue("@Top", winPos.Top);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1433);
			}
		}
	}

	/// <summary>
	/// method_60
	/// </summary>
	public class SaveGroundUnitFormationElement
	{
		public struct GroundUnitFormationElementData
		{
			public int FormationID;
			public int Units;
			public int ClassID;
			public int TemplateID;
			public int SpeciesID;
			public decimal Morale;
			public decimal FortificationLevel;
			public int CurrentSupply;
			public AuroraTargetSelection TargetSelection;
			public int FiringDistribution;
			public int ElementID;
		}
		GroundUnitFormationElementData[] GroundUnitFormationElementStore;

		public struct ElementRechargeData
		{
			public int ElementID;
			public int RechargeRemaining;
		}
		ElementRechargeData[] ElementRechargeStore;

		public struct STODetectedData
		{
			public int ElementID;
			public int DetectingRaceID;
		}
		STODetectedData[] STODetectedStore;

		public struct GroundUnitFormationElementTemplatesData
		{
			public int FormationTemplateID;
			public int Units;
			public int ClassID;
		}
		GroundUnitFormationElementTemplatesData[] GroundUnitFormationElementTemplatesStore;

		public SaveGroundUnitFormationElement(GClass0 game)
		{
			
			//this.GroundUnitFormations.Values.SelectMany<GroundUnitFormation, GroundUnitFormationElement>((Func<GroundUnitFormation, IEnumerable<GroundUnitFormationElement>>) (x => (IEnumerable<GroundUnitFormationElement>) x.FormationElements)).ToList<GroundUnitFormationElement>())
			
			int i = 0;
			var list = game.dictionary_69.Values.SelectMany<GClass95, GClass38>((Func<GClass95, IEnumerable<GClass38>>)(x => (IEnumerable<GClass38>) x.list_0)).ToList<GClass38>();
			GroundUnitFormationElementStore = new GroundUnitFormationElementData[list.Count];
			foreach(GClass38 gclass in list)
			{
				ElementRechargeStore = new ElementRechargeData[gclass.list_0.Count];
				int j = 0;
				foreach (int num in gclass.list_0)
				{
					var dataObj1 = new ElementRechargeData()
					{
						ElementID = gclass.int_0,
						RechargeRemaining = num,
					};
					ElementRechargeStore[j] = dataObj1;
					j++;
				}
				STODetectedStore = new STODetectedData[gclass.list_1.Count];
				j = 0;
				foreach (GClass21 gclass2 in gclass.list_1)
				{
					var dataObj2 = new STODetectedData()
					{
						ElementID = gclass.int_0,
						DetectingRaceID = gclass2.RaceID,
					};
					STODetectedStore[j] = dataObj2;
					j++;
				}
				var dataObj = new GroundUnitFormationElementData()
				{
					FormationID = gclass.gclass95_0.int_0,
					Units = gclass.int_1,
					ClassID = gclass.gclass93_0.int_0,
					TemplateID = 0,
					SpeciesID = gclass.gclass172_0.int_0,
					Morale = gclass.decimal_0,
					FortificationLevel = gclass.decimal_1,
					CurrentSupply = gclass.int_5,
					TargetSelection = gclass.auroraTargetSelection_0,
					FiringDistribution = gclass.int_6,
					ElementID = gclass.int_0,
				};
				GroundUnitFormationElementStore[i] = dataObj;
				i++;
			}

			i = 0;
			var list3 = game.dictionary_70.Values.SelectMany<GClass94, GClass38>((Func<GClass94, IEnumerable<GClass38>>)(x => (IEnumerable<GClass38>) x.list_0)).ToList<GClass38>();
			GroundUnitFormationElementTemplatesStore = new GroundUnitFormationElementTemplatesData[list3.Count];
			foreach(GClass38 gclass3 in list3)
			{
				var dataObj = new GroundUnitFormationElementTemplatesData()
				{
					FormationTemplateID = gclass3.gclass94_0.int_0,
					Units = gclass3.int_1,
					ClassID = gclass3.gclass93_0.int_0,
				};
				GroundUnitFormationElementTemplatesStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GroundUnitFormationElement WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_GroundUnitFormationElementTemplates WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ElementRecharge WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_STODetected WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in GroundUnitFormationElementStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_GroundUnitFormationElement (GameID, FormationID, Units, ClassID, TemplateID, SpeciesID, Morale, FortificationLevel, CurrentSupply, TargetSelection, FiringDistribution, ElementID ) \r\n                        VALUES ( @GameID, @FormationID, @Units, @ClassID, @TemplateID, @SpeciesID, @Morale, @FortificationLevel, @CurrentSupply, @TargetSelection, @FiringDistribution, @ElementID)";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@FormationID", dataObj.FormationID);
						sqliteCommand.Parameters.AddWithValue("@Units", dataObj.Units);
						sqliteCommand.Parameters.AddWithValue("@ClassID", dataObj.ClassID);
						sqliteCommand.Parameters.AddWithValue("@TemplateID", dataObj.TemplateID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@Morale", dataObj.Morale);
						sqliteCommand.Parameters.AddWithValue("@FortificationLevel", dataObj.FortificationLevel);
						sqliteCommand.Parameters.AddWithValue("@CurrentSupply", dataObj.CurrentSupply);
						sqliteCommand.Parameters.AddWithValue("@TargetSelection", dataObj.TargetSelection);
						sqliteCommand.Parameters.AddWithValue("@FiringDistribution", dataObj.FiringDistribution);
						sqliteCommand.Parameters.AddWithValue("@ElementID", dataObj.ElementID);
						sqliteCommand.ExecuteNonQuery();
					}
					foreach (var dataObj in ElementRechargeStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_ElementRecharge ( GameID, ElementID, RechargeRemaining ) VALUES ( @GameID, @ElementID, @RechargeRemaining )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ElementID", dataObj.ElementID);
						sqliteCommand.Parameters.AddWithValue("@RechargeRemaining", dataObj.RechargeRemaining);
						sqliteCommand.ExecuteNonQuery();
					}
					foreach (var dataObj in STODetectedStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_STODetected ( GameID, ElementID, DetectingRaceID ) VALUES ( @GameID, @ElementID, @DetectingRaceID )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ElementID", dataObj.ElementID);
						sqliteCommand.Parameters.AddWithValue("@DetectingRaceID", dataObj.DetectingRaceID);
						sqliteCommand.ExecuteNonQuery();
					}
					foreach (var dataObj in GroundUnitFormationElementTemplatesStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_GroundUnitFormationElementTemplates (GameID, FormationTemplateID, ClassID, Units ) VALUES ( @GameID, @FormationTemplateID, @ClassID, @Units)";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@FormationTemplateID", dataObj.FormationTemplateID);
						sqliteCommand.Parameters.AddWithValue("@Units", dataObj.Units);
						sqliteCommand.Parameters.AddWithValue("@ClassID", dataObj.ClassID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1434);
			}
		}
	}


	public class SaveGroundUnitFormationTemplate
	{
		public struct GroundUnitFormationTemplate
		{
			public int RaceID;
			public string Name;
			public string Abbreviation;
			public int TemplateID;
			public int RequiredRank;
			public GEnum113 AutomatedTemplateID;
			public int FormationsTrained;
		}

		GroundUnitFormationTemplate[] GroundUnitFormationTemplates;


		public SaveGroundUnitFormationTemplate(GClass0 game)
		{
			GroundUnitFormationTemplates = new GroundUnitFormationTemplate[game.dictionary_70.Count()];
			int i = 0;
			foreach (GClass94 gclass in game.dictionary_70.Values)
			{
				int num = 0;
				if (gclass.gclass60_0 != null)
				{
					num = gclass.gclass60_0.int_0;
				}

				var dataObj = new GroundUnitFormationTemplate()
				{
					RaceID = gclass.gclass21_0.RaceID,
					Name = gclass.Name,
					Abbreviation = gclass.string_1,
					TemplateID = gclass.int_0,
					RequiredRank = num,
					AutomatedTemplateID = gclass.genum113_0,
					FormationsTrained = gclass.int_1,
				};
				GroundUnitFormationTemplates[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GroundUnitFormationTemplate WHERE GameID = " + gameID,
					sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var GroundUnitFormationTemplate in GroundUnitFormationTemplates)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_GroundUnitFormationTemplate (GameID, RaceID, Name, Abbreviation, TemplateID, RequiredRank, AutomatedTemplateID, FormationsTrained ) \r\n                        VALUES ( @GameID, @RaceID, @Name, @Abbreviation, @TemplateID, @RequiredRank, @AutomatedTemplateID, @FormationsTrained)";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", GroundUnitFormationTemplate.RaceID);
						sqliteCommand.Parameters.AddWithValue("@Name", GroundUnitFormationTemplate.Name);
						sqliteCommand.Parameters.AddWithValue("@Abbreviation",
							GroundUnitFormationTemplate.Abbreviation);
						sqliteCommand.Parameters.AddWithValue("@TemplateID", GroundUnitFormationTemplate.TemplateID);
						sqliteCommand.Parameters.AddWithValue("@RequiredRank",
							GroundUnitFormationTemplate.RequiredRank);
						sqliteCommand.Parameters.AddWithValue("@AutomatedTemplateID",
							GroundUnitFormationTemplate.AutomatedTemplateID);
						sqliteCommand.Parameters.AddWithValue("@FormationsTrained",
							GroundUnitFormationTemplate.FormationsTrained);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1438);
			}
		}
	}

	public class SaveGroundUnitFormation
	{
		public struct GroundUnitFormation
		{
			public int FormationID;
			public string Name;
			public string Abbreviation;
			public int RaceID;
			public int OriginalTemplateID;
			public int ReplacementTemplateID;
			public int PopulationID;
			public int ShipID;
			public int ParentFormationID;
			public GEnum47 BoardingStatus;
			public bool ActiveSensorsOn;
			public AuroraGroundFormationFieldPosition FieldPosition;
			public int RequiredRank;
			public int AssignedFormationID;
			public bool Civilian;
			public bool UseForReplacements;
			public int ReplacementPriority;
		}

		GroundUnitFormation[] GroundUnitFormations;


		public SaveGroundUnitFormation(GClass0 game)
		{
			GroundUnitFormations = new GroundUnitFormation[game.dictionary_69.Count()];
			int i = 0;
			foreach (GClass95 gclass in game.dictionary_69.Values)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				if (gclass.gclass132_0 != null)
				{
					num2 = gclass.gclass132_0.int_5;
				}

				if (gclass.gclass39_0 != null)
				{
					num = gclass.gclass39_0.int_8;
				}

				if (gclass.gclass95_0 != null)
				{
					num3 = gclass.gclass95_0.int_0;
				}

				if (gclass.gclass95_1 != null)
				{
					num4 = gclass.gclass95_1.int_0;
				}

				if (gclass.gclass60_0 != null)
				{
					num5 = gclass.gclass60_0.int_0;
				}

				if (gclass.gclass94_0 != null)
				{
					num6 = gclass.gclass94_0.int_0;
				}

				if (gclass.gclass94_1 != null)
				{
					num7 = gclass.gclass94_1.int_0;
				}

				var dataObj = new GroundUnitFormation()
				{
					FormationID = gclass.int_0,
					Name = gclass.Name,
					Abbreviation = gclass.string_1,
					RaceID = gclass.gclass21_0.RaceID,
					OriginalTemplateID = num6,
					ReplacementTemplateID = num7,
					PopulationID = num2,
					ShipID = num,
					ParentFormationID = num3,
					BoardingStatus = gclass.genum47_0,
					ActiveSensorsOn = gclass.bool_2,
					FieldPosition = gclass.auroraGroundFormationFieldPosition_0,
					RequiredRank = num5,
					AssignedFormationID = num4,
					Civilian = gclass.bool_3,
					UseForReplacements = gclass.bool_4,
					ReplacementPriority = gclass.int_4,
				};
				GroundUnitFormations[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GroundUnitFormation WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{

					foreach (var GroundUnitFormation in GroundUnitFormations)
					{
						try
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_GroundUnitFormation (FormationID, Name, Abbreviation, RaceID, GameID, OriginalTemplateID, ReplacementTemplateID, PopulationID, ShipID, ParentFormationID, BoardingStatus, ActiveSensorsOn, FieldPosition, RequiredRank, AssignedFormationID, Civilian, UseForReplacements, ReplacementPriority ) \r\n                            VALUES ( @FormationID, @Name, @Abbreviation, @RaceID, @GameID, @OriginalTemplateID, @ReplacementTemplateID, @PopulationID, @ShipID, @ParentFormationID, @BoardingStatus, @ActiveSensorsOn, @FieldPosition, @RequiredRank, @AssignedFormationID, @Civilian, @UseForReplacements, @ReplacementPriority)";
							sqliteCommand.Parameters.AddWithValue("@FormationID", GroundUnitFormation.FormationID);
							sqliteCommand.Parameters.AddWithValue("@Name", GroundUnitFormation.Name);
							sqliteCommand.Parameters.AddWithValue("@Abbreviation", GroundUnitFormation.Abbreviation);
							sqliteCommand.Parameters.AddWithValue("@RaceID", GroundUnitFormation.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@OriginalTemplateID",
								GroundUnitFormation.OriginalTemplateID);
							sqliteCommand.Parameters.AddWithValue("@ReplacementTemplateID",
								GroundUnitFormation.ReplacementTemplateID);
							sqliteCommand.Parameters.AddWithValue("@PopulationID", GroundUnitFormation.PopulationID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", GroundUnitFormation.ShipID);
							sqliteCommand.Parameters.AddWithValue("@ParentFormationID",
								GroundUnitFormation.ParentFormationID);
							sqliteCommand.Parameters.AddWithValue("@BoardingStatus",
								GroundUnitFormation.BoardingStatus);
							sqliteCommand.Parameters.AddWithValue("@ActiveSensorsOn",
								GroundUnitFormation.ActiveSensorsOn);
							sqliteCommand.Parameters.AddWithValue("@FieldPosition", GroundUnitFormation.FieldPosition);
							sqliteCommand.Parameters.AddWithValue("@RequiredRank", GroundUnitFormation.RequiredRank);
							sqliteCommand.Parameters.AddWithValue("@AssignedFormationID",
								GroundUnitFormation.AssignedFormationID);
							sqliteCommand.Parameters.AddWithValue("@Civilian", GroundUnitFormation.Civilian);
							sqliteCommand.Parameters.AddWithValue("@UseForReplacements",
								GroundUnitFormation.UseForReplacements);
							sqliteCommand.Parameters.AddWithValue("@ReplacementPriority",
								GroundUnitFormation.ReplacementPriority);
							sqliteCommand.ExecuteNonQuery();
						}
						catch (Exception exception_)
						{
							GClass202.smethod_68(exception_, 3250);
							break;
						}
					}
				}
			}
			catch (Exception exception_2)
			{
				GClass202.smethod_68(exception_2, 1439);
			}
		}
	}

	public class SaveGroundUnitClass
	{
		public struct GroundUnitClass
		{
			public GEnum109 BaseType;
			public int GroundUnitClassID;
			public int TechSystemID;
			public string ClassName;
			public int ArmourType;
			public int ComponentA;
			public int ComponentB;
			public int ComponentC;
			public int ComponentD;
			public decimal ArmourStrengthModifier;
			public decimal WeaponStrengthModifier;
			public decimal Size;
			public decimal Cost;
			public int STOWeapon;
			public int MaxWeaponRange;
			public int MaxFireControlRange;
			public int ActiveSensorRange;
			public decimal SensorStrength;
			public int TrackingSpeed;
			public int ECCM;
			public GEnum112 GUClassType;
			public decimal UnitSupplyCost;
			public int RechargeTime;
			public decimal ConstructionRating;
			public int HQCapacity;
			public bool NonCombatClass;
			public decimal Duranium;
			public decimal Neutronium;
			public decimal Corbomite;
			public decimal Tritanium;
			public decimal Boronide;
			public decimal Mercassium;
			public decimal Vendarite;
			public decimal Sorium;
			public decimal Uridium;
			public decimal Corundium;
			public decimal Gallicite;
			public int[] GUCapGroundUnitClassID;
			public GEnum110[] GUCapCapabilityID;
		}

		GroundUnitClass[] GroundUnitClasss;


		public SaveGroundUnitClass(GClass0 game)
		{
			GroundUnitClasss = new GroundUnitClass[game.dictionary_68.Count()];
			int i = 0;
			foreach (GClass93 gclass in game.dictionary_68.Values)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				if (gclass.list_0.Count > 0)
				{
					num = (int)gclass.list_0[0].genum111_0;
				}

				if (gclass.list_0.Count > 1)
				{
					num2 = (int)gclass.list_0[1].genum111_0;
				}

				if (gclass.list_0.Count > 2)
				{
					num3 = (int)gclass.list_0[2].genum111_0;
				}

				if (gclass.list_0.Count > 3)
				{
					num4 = (int)gclass.list_0[3].genum111_0;
				}

				if (gclass.gclass206_0 != null)
				{
					num5 = gclass.gclass206_0.int_0;
				}

				int count = gclass.dictionary_0.Count;
				int[] gUCapGroundUnitClassID = new int[count];
				GEnum110[] gUCapCapabilityID = new GEnum110[count];
				int j = 0;
				foreach (GClass90 gclass2 in gclass.dictionary_0.Values)
				{
					gUCapGroundUnitClassID[j] = gclass.int_0;
					gUCapCapabilityID[j] = gclass2.genum110_0;
					j++;
				}

				var dataObj = new GroundUnitClass()
				{
					BaseType = gclass.gclass88_0.genum109_0,
					GroundUnitClassID = gclass.int_0,
					TechSystemID = gclass.gclass147_0.int_0,
					ClassName = gclass.ClassName,
					ArmourType = gclass.gclass89_0.int_0,
					ComponentA = num,
					ComponentB = num2,
					ComponentC = num3,
					ComponentD = num4,
					ArmourStrengthModifier = gclass.decimal_0,
					WeaponStrengthModifier = gclass.decimal_1,
					Size = gclass.decimal_2,
					Cost = gclass.decimal_3,
					STOWeapon = num5,
					MaxWeaponRange = gclass.int_1,
					MaxFireControlRange = gclass.int_2,
					ActiveSensorRange = gclass.int_3,
					SensorStrength = gclass.decimal_5,
					TrackingSpeed = gclass.int_4,
					ECCM = gclass.int_5,
					GUClassType = gclass.genum112_0,
					UnitSupplyCost = gclass.decimal_4,
					RechargeTime = gclass.int_6,
					ConstructionRating = gclass.decimal_6,
					HQCapacity = gclass.int_7,
					NonCombatClass = gclass.bool_0,
					Duranium = gclass.gclass114_0.decimal_0,
					Neutronium = gclass.gclass114_0.decimal_1,
					Corbomite = gclass.gclass114_0.decimal_2,
					Tritanium = gclass.gclass114_0.decimal_3,
					Boronide = gclass.gclass114_0.decimal_4,
					Mercassium = gclass.gclass114_0.decimal_5,
					Vendarite = gclass.gclass114_0.decimal_6,
					Sorium = gclass.gclass114_0.decimal_7,
					Uridium = gclass.gclass114_0.decimal_8,
					Corundium = gclass.gclass114_0.decimal_9,
					Gallicite = gclass.gclass114_0.decimal_10,
					GUCapGroundUnitClassID = gUCapGroundUnitClassID,
					GUCapCapabilityID = gUCapCapabilityID,
				};
				GroundUnitClasss[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GroundUnitClass WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var GroundUnitClass in GroundUnitClasss)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_GroundUnitClass (BaseType, GroundUnitClassID, TechSystemID, ClassName, ArmourType, ComponentA, ComponentB, ComponentC, ComponentD, ArmourStrengthModifier, WeaponStrengthModifier, Size, Cost, GameID, STOWeapon, MaxWeaponRange, MaxFireControlRange, ActiveSensorRange, SensorStrength, TrackingSpeed, ECCM, GUClassType, UnitSupplyCost, RechargeTime, ConstructionRating, HQCapacity, NonCombatClass,\r\n                                    Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite) \r\n                        VALUES ( @BaseType, @GroundUnitClassID, @TechSystemID, @ClassName, @ArmourType, @ComponentA, @ComponentB, @ComponentC, @ComponentD, @ArmourStrengthModifier, @WeaponStrengthModifier, @Size, @Cost, @GameID, @STOWeapon, @MaxWeaponRange, @MaxFireControlRange, @ActiveSensorRange, @SensorStrength, @TrackingSpeed, @ECCM, @GUClassType, @UnitSupplyCost, @RechargeTime, @ConstructionRating, @HQCapacity, @NonCombatClass,\r\n                                    @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite)";
						sqliteCommand.Parameters.AddWithValue("@BaseType", GroundUnitClass.BaseType);
						sqliteCommand.Parameters.AddWithValue("@GroundUnitClassID", GroundUnitClass.GroundUnitClassID);
						sqliteCommand.Parameters.AddWithValue("@TechSystemID", GroundUnitClass.TechSystemID);
						sqliteCommand.Parameters.AddWithValue("@ClassName", GroundUnitClass.ClassName);
						sqliteCommand.Parameters.AddWithValue("@ArmourType", GroundUnitClass.ArmourType);
						sqliteCommand.Parameters.AddWithValue("@ComponentA", GroundUnitClass.ComponentA);
						sqliteCommand.Parameters.AddWithValue("@ComponentB", GroundUnitClass.ComponentB);
						sqliteCommand.Parameters.AddWithValue("@ComponentC", GroundUnitClass.ComponentC);
						sqliteCommand.Parameters.AddWithValue("@ComponentD", GroundUnitClass.ComponentD);
						sqliteCommand.Parameters.AddWithValue("@ArmourStrengthModifier",
							GroundUnitClass.ArmourStrengthModifier);
						sqliteCommand.Parameters.AddWithValue("@WeaponStrengthModifier",
							GroundUnitClass.WeaponStrengthModifier);
						sqliteCommand.Parameters.AddWithValue("@Size", GroundUnitClass.Size);
						sqliteCommand.Parameters.AddWithValue("@Cost", GroundUnitClass.Cost);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@STOWeapon", GroundUnitClass.STOWeapon);
						sqliteCommand.Parameters.AddWithValue("@MaxWeaponRange", GroundUnitClass.MaxWeaponRange);
						sqliteCommand.Parameters.AddWithValue("@MaxFireControlRange",
							GroundUnitClass.MaxFireControlRange);
						sqliteCommand.Parameters.AddWithValue("@ActiveSensorRange", GroundUnitClass.ActiveSensorRange);
						sqliteCommand.Parameters.AddWithValue("@SensorStrength", GroundUnitClass.SensorStrength);
						sqliteCommand.Parameters.AddWithValue("@TrackingSpeed", GroundUnitClass.TrackingSpeed);
						sqliteCommand.Parameters.AddWithValue("@ECCM", GroundUnitClass.ECCM);
						sqliteCommand.Parameters.AddWithValue("@GUClassType", GroundUnitClass.GUClassType);
						sqliteCommand.Parameters.AddWithValue("@UnitSupplyCost", GroundUnitClass.UnitSupplyCost);
						sqliteCommand.Parameters.AddWithValue("@RechargeTime", GroundUnitClass.RechargeTime);
						sqliteCommand.Parameters.AddWithValue("@ConstructionRating",
							GroundUnitClass.ConstructionRating);
						sqliteCommand.Parameters.AddWithValue("@HQCapacity", GroundUnitClass.HQCapacity);
						sqliteCommand.Parameters.AddWithValue("@NonCombatClass", GroundUnitClass.NonCombatClass);
						sqliteCommand.Parameters.AddWithValue("@Duranium", GroundUnitClass.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", GroundUnitClass.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", GroundUnitClass.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", GroundUnitClass.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", GroundUnitClass.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", GroundUnitClass.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", GroundUnitClass.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", GroundUnitClass.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", GroundUnitClass.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", GroundUnitClass.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", GroundUnitClass.Gallicite);
						for (int o = 0; o < GroundUnitClass.GUCapGroundUnitClassID.Length; o++)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_GroundUnitCapability ( GroundUnitClassID, CapabilityID, GameID ) VALUES ( @GroundUnitClassID, @CapabilityID, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@GroundUnitClassID",
								GroundUnitClass.GUCapGroundUnitClassID[o]);
							sqliteCommand.Parameters.AddWithValue("@CapabilityID",
								GroundUnitClass.GUCapCapabilityID[o]);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1440);
			}
		}
	}

	public class SaveMapLabel
	{
		public struct MapLabel
		{
			public string Caption;
			public int Colour;
			public Font FontBold;
			public Font FontItalic;
			public Font FontName;
			public Font FontSize;
			public int RaceID;
			public double Xcor;
			public double Ycor;
		}

		MapLabel[] MapLabels;


		public SaveMapLabel(GClass0 game)
		{
			//var func = new Func<GClass21, IEnumerable<GClass113>>) (x => (IEnumerable<GClass113>) x.MapLabels)).ToList<GClass113>()
			int i = 0;
			var list = game.dictionary_34.Values
				.SelectMany<GClass21, GClass113>(
					(Func<GClass21, IEnumerable<GClass113>>)(x => (IEnumerable<GClass113>)x.list_8))
				.ToList<GClass113>();
			MapLabels = new MapLabel[list.Count()];
			foreach (GClass113 gclass in list)
			{
				var dataObj = new MapLabel()
				{
					Caption = gclass.string_0,
					Colour = gclass.color_0.ToArgb(),
					FontBold = gclass.font_0,
					FontItalic = gclass.font_0,
					FontName = gclass.font_0,
					FontSize = gclass.font_0,
					RaceID = gclass.gclass21_0.RaceID,
					Xcor = gclass.double_2,
					Ycor = gclass.double_3,
				};
				MapLabels[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MapLabel WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var MapLabel in MapLabels)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_MapLabel (Caption, Colour, FontBold, FontItalic, FontName, FontSize, GameID, RaceID, Xcor, Ycor) \r\n                        VALUES ( @Caption, @Colour, @FontBold, @FontItalic, @FontName, @FontSize, @GameID, @RaceID, @Xcor, @Ycor) ";
						sqliteCommand.Parameters.AddWithValue("@Caption", MapLabel.Caption);
						sqliteCommand.Parameters.AddWithValue("@Colour", MapLabel.Colour);
						sqliteCommand.Parameters.AddWithValue("@FontBold", MapLabel.FontBold);
						sqliteCommand.Parameters.AddWithValue("@FontItalic", MapLabel.FontItalic);
						sqliteCommand.Parameters.AddWithValue("@FontName", MapLabel.FontName);
						sqliteCommand.Parameters.AddWithValue("@FontSize", MapLabel.FontSize);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", MapLabel.RaceID);
						sqliteCommand.Parameters.AddWithValue("@Xcor", MapLabel.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", MapLabel.Ycor);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1441);
			}
		}
	}



	/// <summary>
	/// method_65
	/// </summary>
	public class SaveSurveyLocation
	{
		public struct SurveyLocationData
		{
			public int SurveyLocationID;
			public int SystemID;
			public int LocationNumber;
			public double Xcor;
			public double Ycor;
			public RaceSurveyLocationData[] RaceSurveyLocationStore;
		}
		SurveyLocationData[] SurveyLocationStore;

		public struct RaceSurveyLocationData
		{
			public int RaceID;
			public int SystemID;
			public int LocationNumber;
		}

		public SaveSurveyLocation(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_9.Values.SelectMany<GClass178, GClass189>((Func<GClass178, IEnumerable<GClass189>>)(x => (IEnumerable<GClass189>) x.dictionary_0.Values)).ToList<GClass189>();
			SurveyLocationStore = new SurveyLocationData[list.Count];
			foreach(GClass189 gclass in list)
			{
				RaceSurveyLocationData[] raceSurveyLocationStore = new RaceSurveyLocationData[gclass.list_0.Count];
				var j = 0;
				foreach (int num in gclass.list_0)
				{
					var dataObj1 = new RaceSurveyLocationData()
					{
						RaceID = num,
						SystemID = gclass.int_1,
						LocationNumber = gclass.int_2,
					};
					raceSurveyLocationStore[j] = dataObj1;
					j++;
				}
				
				var dataObj = new SurveyLocationData()
				{
					SurveyLocationID = gclass.int_0,
					SystemID = gclass.int_1,
					LocationNumber = gclass.int_2,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					RaceSurveyLocationStore = raceSurveyLocationStore,
				};
				SurveyLocationStore[i] = dataObj;
				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_SurveyLocation WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_RaceSurveyLocation WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in SurveyLocationStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_SurveyLocation (SurveyLocationID, GameID, SystemID, LocationNumber, Xcor, Ycor ) VALUES ( @SurveyLocationID, @GameID, @SystemID, @LocationNumber, @Xcor, @Ycor)";
						sqliteCommand.Parameters.AddWithValue("@SurveyLocationID", dataObj.SurveyLocationID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@LocationNumber", dataObj.LocationNumber);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.RaceSurveyLocationStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_RaceSurveyLocation ( RaceID, GameID, SystemID, LocationNumber ) VALUES ( @RaceID, @GameID, @SystemID, @LocationNumber )";
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj1.SystemID);
							sqliteCommand.Parameters.AddWithValue("@LocationNumber", dataObj1.LocationNumber);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1442);
			}
		}
	}


	public class SaveStar
	{
		public struct StarData
		{
			public int StarID;
			public int SystemID;
			public int StarTypeID;
			public string Name;
			public int Protostar;
			public double Xcor;
			public double Ycor;
			public int Component;
			public int OrbitingComponent;
			public double Bearing;
			public double Luminosity;
			public double OrbitalPeriod;
			public double OrbitalDistance;
			public GEnum33 DisasterStatus;
		}

		StarData[] StarDataStore;


		public SaveStar(GClass0 game)
		{
			StarDataStore = new StarData[game.dictionary_10.Count()];
			int i = 0;
			foreach (GClass175 gclass in game.dictionary_10.Values)
			{
				var dataObj = new StarData()
				{
					StarID = gclass.int_0,
					SystemID = gclass.int_1,
					StarTypeID = gclass.gclass193_0.int_0,
					Name = gclass.string_0,
					Protostar = gclass.int_2,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					Component = gclass.int_3,
					OrbitingComponent = gclass.int_4,
					Bearing = gclass.double_3,
					Luminosity = gclass.double_4,
					OrbitalPeriod = gclass.double_5,
					OrbitalDistance = gclass.double_2,
					DisasterStatus = gclass.genum33_0,
				};
				StarDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Star WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var StarData in StarDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Star (StarID, GameID, SystemID, StarTypeID, Name, Protostar, Xcor, Ycor, Component, OrbitingComponent, Bearing, Luminosity, OrbitalPeriod, OrbitalDistance, DisasterStatus ) \r\n                        VALUES ( @StarID, @GameID, @SystemID, @StarTypeID, @Name, @Protostar, @Xcor, @Ycor, @Component, @OrbitingComponent, @Bearing, @Luminosity, @OrbitalPeriod, @OrbitalDistance, @DisasterStatus )";
						sqliteCommand.Parameters.AddWithValue("@StarID", StarData.StarID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", StarData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@StarTypeID", StarData.StarTypeID);
						sqliteCommand.Parameters.AddWithValue("@Name", StarData.Name);
						sqliteCommand.Parameters.AddWithValue("@Protostar", StarData.Protostar);
						sqliteCommand.Parameters.AddWithValue("@Xcor", StarData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", StarData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@Component", StarData.Component);
						sqliteCommand.Parameters.AddWithValue("@OrbitingComponent", StarData.OrbitingComponent);
						sqliteCommand.Parameters.AddWithValue("@Bearing", StarData.Bearing);
						sqliteCommand.Parameters.AddWithValue("@Luminosity", StarData.Luminosity);
						sqliteCommand.Parameters.AddWithValue("@OrbitalPeriod", StarData.OrbitalPeriod);
						sqliteCommand.Parameters.AddWithValue("@OrbitalDistance", StarData.OrbitalDistance);
						sqliteCommand.Parameters.AddWithValue("@DisasterStatus", StarData.DisasterStatus);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1443);
			}
		}
	}

	public class SaveJumpPoint
	{
		public struct JumpPointData
		{
			public int WarpPointID;
			public int SystemID;
			public double Distance;
			public int Bearing;
			public int WPLink;
			public double Xcor;
			public double Ycor;
			public int JumpGateStrength;
			public int JumpGateRaceID;
		}

		JumpPointData[] JumpPointDataStore;


		public SaveJumpPoint(GClass0 game)
		{
			JumpPointDataStore = new JumpPointData[game.dictionary_12.Count()];
			int i = 0;
			foreach (GClass111 gclass in game.dictionary_12.Values)
			{
				var dataObj = new JumpPointData()
				{
					WarpPointID = gclass.int_0,
					SystemID = gclass.gclass178_0.int_0,
					Distance = gclass.double_0,
					Bearing = gclass.int_1,
					WPLink = gclass.int_4,
					Xcor = gclass.double_1,
					Ycor = gclass.double_2,
					JumpGateStrength = gclass.int_2,
					JumpGateRaceID = gclass.int_3,
				};
				JumpPointDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_JumpPoint WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var JumpPointData in JumpPointDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_JumpPoint (WarpPointID, GameID, SystemID, Distance, Bearing, WPLink, Xcor, Ycor, JumpGateStrength, JumpGateRaceID ) VALUES ( @WarpPointID, @GameID, @SystemID, @Distance, @Bearing, @WPLink, @Xcor, @Ycor, @JumpGateStrength, @JumpGateRaceID)";
						sqliteCommand.Parameters.AddWithValue("@WarpPointID", JumpPointData.WarpPointID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", JumpPointData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@Distance", JumpPointData.Distance);
						sqliteCommand.Parameters.AddWithValue("@Bearing", JumpPointData.Bearing);
						sqliteCommand.Parameters.AddWithValue("@WPLink", JumpPointData.WPLink);
						sqliteCommand.Parameters.AddWithValue("@Xcor", JumpPointData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", JumpPointData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@JumpGateStrength", JumpPointData.JumpGateStrength);
						sqliteCommand.Parameters.AddWithValue("@JumpGateRaceID", JumpPointData.JumpGateRaceID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1444);
			}
		}
	}

	public class SaveOrderTemplate
	{
		public struct OrderTemplateData
		{
			public int OrderTemplateID;
			public int RaceID;
			public int StartSystemID;
			public string TemplateName;
		}

		OrderTemplateData[] OrderTemplateDataStore;


		public SaveOrderTemplate(GClass0 game)
		{
			OrderTemplateDataStore = new OrderTemplateData[game.dictionary_31.Count()];
			int i = 0;
			foreach (GClass130 gclass in game.dictionary_31.Values)
			{
				var dataObj = new OrderTemplateData()
				{
					OrderTemplateID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					StartSystemID = gclass.gclass180_0.gclass178_0.int_0,
					TemplateName = gclass.string_0,
				};
				OrderTemplateDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_OrderTemplate WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var OrderTemplateData in OrderTemplateDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_OrderTemplate (GameID, OrderTemplateID, RaceID, StartSystemID, TemplateName ) VALUES ( @GameID, @OrderTemplateID, @RaceID, @StartSystemID, @TemplateName)";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@OrderTemplateID", OrderTemplateData.OrderTemplateID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", OrderTemplateData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@StartSystemID", OrderTemplateData.StartSystemID);
						sqliteCommand.Parameters.AddWithValue("@TemplateName", OrderTemplateData.TemplateName);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1445);
			}
		}
	}

	public class SaveAcidAttack
	{
		public struct AcidAttackData
		{
			public int ArmourColumn;
			public int TargetShipID;
			public int PointOfDamageTime;
			public int RemainingDamage;
			public decimal LastDamageTime;
		}

		AcidAttackData[] AcidAttackDataStore;


		public SaveAcidAttack(GClass0 game)
		{
			AcidAttackDataStore = new AcidAttackData[game.list_5.Count()];
			int i = 0;
			foreach (GClass25 gclass in game.list_5)
			{
				var dataObj = new AcidAttackData()
				{
					ArmourColumn = gclass.int_0,
					TargetShipID = gclass.gclass39_0.int_8,
					PointOfDamageTime = gclass.int_1,
					RemainingDamage = gclass.int_2,
					LastDamageTime = gclass.decimal_0,
				};
				AcidAttackDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AcidAttack WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var AcidAttackData in AcidAttackDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_AcidAttack (ArmourColumn, GameID, TargetShipID, PointOfDamageTime, RemainingDamage, LastDamageTime ) VALUES ( @ArmourColumn, @GameID, @TargetShipID, @PointOfDamageTime, @RemainingDamage, @LastDamageTime)";
						sqliteCommand.Parameters.AddWithValue("@ArmourColumn", AcidAttackData.ArmourColumn);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@TargetShipID", AcidAttackData.TargetShipID);
						sqliteCommand.Parameters.AddWithValue("@PointOfDamageTime", AcidAttackData.PointOfDamageTime);
						sqliteCommand.Parameters.AddWithValue("@RemainingDamage", AcidAttackData.RemainingDamage);
						sqliteCommand.Parameters.AddWithValue("@LastDamageTime", AcidAttackData.LastDamageTime);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1446);
			}
		}
	}

	public class SaveShipTech
	{
		public struct ShipTechData
		{
			public int ShipID;
			public int TechID;
			public decimal TechPoints;
		}

		ShipTechData[] ShipTechDataStore;


		public SaveShipTech(GClass0 game)
		{
			//ShipTechData = GClass163
			//Ship = GClass39
			int i = 0;
			var ShipsList = game.dictionary_4;
			var list = ShipsList.Values
				.SelectMany<GClass39, GClass163>(
					(Func<GClass39, IEnumerable<GClass163>>)(x => (IEnumerable<GClass163>)x.list_15))
				.ToList<GClass163>();
			ShipTechDataStore = new ShipTechData[list.Count()];
			foreach (GClass163 gclass in list)
			{
				var dataObj = new ShipTechData()
				{
					ShipID = gclass.gclass39_0.int_8,
					TechID = gclass.gclass147_0.int_0,
					TechPoints = gclass.decimal_0,
				};
				ShipTechDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShipTechData WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var ShipTechDataData in ShipTechDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_ShipTechData (GameID, ShipID, TechID, TechPoints ) VALUES ( @GameID, @ShipID, @TechID, @TechPoints)";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ShipID", ShipTechDataData.ShipID);
						sqliteCommand.Parameters.AddWithValue("@TechID", ShipTechDataData.TechID);
						sqliteCommand.Parameters.AddWithValue("@TechPoints", ShipTechDataData.TechPoints);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1447);
			}
		}
	}
	
	/// <summary>
	/// method_71
	/// </summary>
	public class SaveTechProgressionRace
	{
		public struct TechProgressionData
		{
			public TechProgressionValues[] TechProgressionValueStore;
		}
		TechProgressionData[] TechProgressionStore;

		public struct TechProgressionValues
		{
			public int ProgressionOrder;
			public int RaceID;
		}
		
		public SaveTechProgressionRace(GClass0 game)
		{
			int i = 0;
			TechProgressionStore = new TechProgressionData[game.list_3.Count];
			foreach (GClass18 gclass in game.list_3)
			{
				int j = 0;
				TechProgressionValues[] tpvStore = new TechProgressionValues[gclass.list_0.Count];
				foreach (GClass21 gclass2 in gclass.list_0)
				{
					var dataObj1 = new TechProgressionValues()
					{
						ProgressionOrder = gclass.int_0,
						RaceID = gclass2.RaceID,
					};
					tpvStore[j] = dataObj1;
					j++;
				}

				TechProgressionStore[i] = new TechProgressionData()
				{
					TechProgressionValueStore = tpvStore,
				};
				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_TechProgressionRace WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in TechProgressionStore)
					{

						foreach (var dataObj1 in dataObj.TechProgressionValueStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_TechProgressionRace (ProgressionOrder, RaceID, GameID ) VALUES ( @ProgressionOrder, @RaceID, @GameID)";
							sqliteCommand.Parameters.AddWithValue("@ProgressionOrder", dataObj1.ProgressionOrder);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj1.RaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1448);
			}
		}
	}


	public class SaveWaypoint
	{
		public struct WaypointData
		{
			public int WaypointID;
			public int RaceID;
			public int SystemID;
			public int OrbitBodyID;
			public decimal CreationTime;
			public double Xcor;
			public double Ycor;
			public int Number;
			public WayPointType WaypointType;
			public string Name;
			public int JumpPointID;
		}

		WaypointData[] WaypointDataStore;


		public SaveWaypoint(GClass0 game)
		{
			WaypointDataStore = new WaypointData[game.dictionary_13.Count()];
			int i = 0;
			foreach (GClass190 gclass in game.dictionary_13.Values)
			{
				var dataObj = new WaypointData()
				{
					WaypointID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					SystemID = gclass.gclass178_0.int_0,
					OrbitBodyID = gclass.int_1,
					CreationTime = gclass.decimal_0,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					Number = gclass.int_2,
					WaypointType = gclass.wayPointType_0,
					Name = gclass.Name,
					JumpPointID = gclass.method_0(),
				};
				WaypointDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Waypoint WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var WaypointData in WaypointDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Waypoint (WaypointID, GameID, RaceID, SystemID, OrbitBodyID, CreationTime, Xcor, Ycor, Number, WaypointType, Name, JumpPointID ) VALUES ( @WaypointID, @GameID, @RaceID, @SystemID, @OrbitBodyID, @CreationTime, @Xcor, @Ycor, @Number, @WaypointType, @Name, @JumpPointID)";
						sqliteCommand.Parameters.AddWithValue("@WaypointID", WaypointData.WaypointID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", WaypointData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", WaypointData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@OrbitBodyID", WaypointData.OrbitBodyID);
						sqliteCommand.Parameters.AddWithValue("@CreationTime", WaypointData.CreationTime);
						sqliteCommand.Parameters.AddWithValue("@Xcor", WaypointData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", WaypointData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@Number", WaypointData.Number);
						sqliteCommand.Parameters.AddWithValue("@WaypointType", WaypointData.WaypointType);
						sqliteCommand.Parameters.AddWithValue("@Name", WaypointData.Name);
						sqliteCommand.Parameters.AddWithValue("@JumpPointID", WaypointData.JumpPointID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1449);
			}
		}
	}

	public class SaveMassDriverPackets
	{
		public struct MassDriverPacketsData
		{
			public int PacketID;
			public int RaceID;
			public int SysID;
			public int DestID;
			public decimal TotalSize;
			public decimal Speed;
			public double Xcor;
			public double Ycor;
			public double LastXcor;
			public double LastYcor;
			public double IncrementStartX;
			public double IncrementStartY;
			public decimal Duranium;
			public decimal Neutronium;
			public decimal Corbomite;
			public decimal Tritanium;
			public decimal Boronide;
			public decimal Mercassium;
			public decimal Vendarite;
			public decimal Sorium;
			public decimal Uridium;
			public decimal Corundium;
			public decimal Gallicite;
		}

		MassDriverPacketsData[] MassDriverPacketsDataStore;


		public SaveMassDriverPackets(GClass0 game)
		{
			MassDriverPacketsDataStore = new MassDriverPacketsData[game.dictionary_7.Count()];
			int i = 0;
			foreach (GClass117 gclass in game.dictionary_7.Values)
			{
				var dataObj = new MassDriverPacketsData()
				{
					PacketID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					SysID = gclass.gclass178_0.int_0,
					DestID = gclass.gclass132_0.int_5,
					TotalSize = gclass.decimal_1,
					Speed = gclass.decimal_0,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					LastXcor = gclass.double_2,
					LastYcor = gclass.double_3,
					IncrementStartX = gclass.double_4,
					IncrementStartY = gclass.double_5,
					Duranium = gclass.gclass114_0.decimal_0,
					Neutronium = gclass.gclass114_0.decimal_1,
					Corbomite = gclass.gclass114_0.decimal_2,
					Tritanium = gclass.gclass114_0.decimal_3,
					Boronide = gclass.gclass114_0.decimal_4,
					Mercassium = gclass.gclass114_0.decimal_5,
					Vendarite = gclass.gclass114_0.decimal_6,
					Sorium = gclass.gclass114_0.decimal_7,
					Uridium = gclass.gclass114_0.decimal_8,
					Corundium = gclass.gclass114_0.decimal_9,
					Gallicite = gclass.gclass114_0.decimal_10,
				};
				MassDriverPacketsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MassDriverPackets WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var MassDriverPacketsData in MassDriverPacketsDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_MassDriverPackets (PacketID, GameID, RaceID, SysID, DestID, TotalSize, Speed, Xcor, Ycor, LastXcor, LastYcor, IncrementStartX, IncrementStartY, Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite ) \r\n                        VALUES ( @PacketID, @GameID, @RaceID, @SysID, @DestID, @TotalSize, @Speed, @Xcor, @Ycor, @LastXcor, @LastYcor, @IncrementStartX, @IncrementStartY, @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite)";
						sqliteCommand.Parameters.AddWithValue("@PacketID", MassDriverPacketsData.PacketID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", MassDriverPacketsData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SysID", MassDriverPacketsData.SysID);
						sqliteCommand.Parameters.AddWithValue("@DestID", MassDriverPacketsData.DestID);
						sqliteCommand.Parameters.AddWithValue("@TotalSize", MassDriverPacketsData.TotalSize);
						sqliteCommand.Parameters.AddWithValue("@Speed", MassDriverPacketsData.Speed);
						sqliteCommand.Parameters.AddWithValue("@Xcor", MassDriverPacketsData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", MassDriverPacketsData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@LastXcor", MassDriverPacketsData.LastXcor);
						sqliteCommand.Parameters.AddWithValue("@LastYcor", MassDriverPacketsData.LastYcor);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartX",
							MassDriverPacketsData.IncrementStartX);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartY",
							MassDriverPacketsData.IncrementStartY);
						sqliteCommand.Parameters.AddWithValue("@Duranium", MassDriverPacketsData.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", MassDriverPacketsData.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", MassDriverPacketsData.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", MassDriverPacketsData.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", MassDriverPacketsData.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", MassDriverPacketsData.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", MassDriverPacketsData.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", MassDriverPacketsData.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", MassDriverPacketsData.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", MassDriverPacketsData.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", MassDriverPacketsData.Gallicite);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1450);
			}
		}
	}

	public class SaveLagrangePoint
	{
		public struct LagrangePointData
		{
			public int LaGrangePointID;
			public int SystemID;
			public int StarID;
			public int PlanetID;
			public double Xcor;
			public double Ycor;
			public double Distance;
			public double Bearing;
		}

		LagrangePointData[] LagrangePointDataStore;


		public SaveLagrangePoint(GClass0 game)
		{
			LagrangePointDataStore = new LagrangePointData[game.dictionary_14.Count()];
			int i = 0;
			foreach (GClass188 gclass in game.dictionary_14.Values)
			{
				var dataObj = new LagrangePointData()
				{
					LaGrangePointID = gclass.int_0,
					SystemID = gclass.gclass178_0.int_0,
					StarID = gclass.gclass175_0.int_0,
					PlanetID = gclass.gclass1_0.int_0,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					Distance = gclass.double_2,
					Bearing = gclass.double_3,
				};
				LagrangePointDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_LagrangePoint WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var LagrangePointData in LagrangePointDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_LagrangePoint (LaGrangePointID, GameID, SystemID, StarID, PlanetID, Xcor, Ycor, Distance, Bearing ) VALUES ( @LaGrangePointID, @GameID, @SystemID, @StarID, @PlanetID, @Xcor, @Ycor, @Distance, @Bearing)";
						sqliteCommand.Parameters.AddWithValue("@LaGrangePointID", LagrangePointData.LaGrangePointID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", LagrangePointData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@StarID", LagrangePointData.StarID);
						sqliteCommand.Parameters.AddWithValue("@PlanetID", LagrangePointData.PlanetID);
						sqliteCommand.Parameters.AddWithValue("@Xcor", LagrangePointData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", LagrangePointData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@Distance", LagrangePointData.Distance);
						sqliteCommand.Parameters.AddWithValue("@Bearing", LagrangePointData.Bearing);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1451);
			}
		}
	}

	public class SavePopTradeBalance
	{
		public struct PopTradeBalanceData
		{
			public int PopulationID;
			public int TradeGoodID;
			public decimal ProductionRate;
			public decimal TradeBalance;
			public decimal LastTradeBalance;
		}

		PopTradeBalanceData[] PopTradeBalanceDataStore;


		public SavePopTradeBalance(GClass0 game)
		{
			//PopTradeBalace = GClass168
			//PopulationList = game.dictrionary_20
			//Population = GClass132
			//Population.TradeBalances is a Dictionary<int,PopTradeBalance> (<int,GClass168>)  = GClass132.dictionary_4
			int i = 0;
			var list = game.dictionary_20.Values
				.SelectMany<GClass132, GClass168>(
					(Func<GClass132, IEnumerable<GClass168>>)(x => (IEnumerable<GClass168>)x.dictionary_4.Values))
				.ToList<GClass168>();
			PopTradeBalanceDataStore = new PopTradeBalanceData[list.Count()];
			foreach (GClass168 gclass in list)
			{
				var dataObj = new PopTradeBalanceData()
				{
					PopulationID = gclass.gclass132_0.int_5,
					TradeGoodID = gclass.gclass167_0.int_0,
					ProductionRate = gclass.decimal_0,
					TradeBalance = gclass.decimal_1,
					LastTradeBalance = gclass.decimal_2,
				};
				PopTradeBalanceDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_PopTradeBalance WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var PopTradeBalanceData in PopTradeBalanceDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_PopTradeBalance (PopulationID, TradeGoodID, GameID, ProductionRate, TradeBalance, LastTradeBalance ) VALUES ( @PopulationID, @TradeGoodID, @GameID, @ProductionRate, @TradeBalance, @LastTradeBalance )";
						sqliteCommand.Parameters.AddWithValue("@PopulationID", PopTradeBalanceData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@TradeGoodID", PopTradeBalanceData.TradeGoodID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ProductionRate", PopTradeBalanceData.ProductionRate);
						sqliteCommand.Parameters.AddWithValue("@TradeBalance", PopTradeBalanceData.TradeBalance);
						sqliteCommand.Parameters.AddWithValue("@LastTradeBalance",
							PopTradeBalanceData.LastTradeBalance);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1452);
			}
		}
	}

	public class SaveShippingWealthData
	{
		public struct ShippingWealthDataData
		{
			public int ShippingLineID;
			public int ShipID;
			public bool Contract;
			public bool Colonist;
			public bool Fuel;
			public decimal Amount;
			public int TradeGood;
			public decimal TradeTime;
			public int OriginPop;
			public int DestinationPop;
		}

		ShippingWealthDataData[] ShippingWealthDataDataStore;


		public SaveShippingWealthData(GClass0 game)
		{
			//ShippingLines = dictionary_8
			//ShippingWealthData = GClass166
			//ShippingLine = GClass165			
			int i = 0;
			List<GClass166> list = game.dictionary_8.Values.SelectMany<GClass165, GClass166>((Func<GClass165, IEnumerable<GClass166>>) (x => (IEnumerable<GClass166>) x.list_0)).ToList<GClass166>();
			ShippingWealthDataDataStore = new ShippingWealthDataData[list.Count];
			foreach (GClass166 wealthData in list)
			{
				int num = 0;
				int num2 = 0;
				if (wealthData.gclass39_0 != null)
				{
					num = wealthData.gclass39_0.int_8;
				}

				if (wealthData.gclass167_0 != null)
				{
					num2 = wealthData.gclass167_0.int_0;
				}

				var dataObj = new ShippingWealthDataData()
				{
					ShippingLineID = wealthData.gclass165_0.int_0,
					ShipID = num,
					Contract = wealthData.bool_0,
					Colonist = wealthData.bool_1,
					Fuel = wealthData.bool_2,
					Amount = wealthData.decimal_1,
					TradeGood = num2,
					TradeTime = wealthData.decimal_0,
					OriginPop = wealthData.method_0(),
					DestinationPop = wealthData.method_1(),
				};
				ShippingWealthDataDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShippingWealthData WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var ShippingWealthDataData in ShippingWealthDataDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_ShippingWealthData (GameID, ShippingLineID, ShipID, Contract, Colonist, Fuel, Amount, TradeGood, TradeTime, OriginPop, DestinationPop ) VALUES ( @GameID, @ShippingLineID, @ShipID, @Contract, @Colonist, @Fuel, @Amount, @TradeGood, @TradeTime, @OriginPop, @DestinationPop )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ShippingLineID", ShippingWealthDataData.ShippingLineID);
						sqliteCommand.Parameters.AddWithValue("@ShipID", ShippingWealthDataData.ShipID);
						sqliteCommand.Parameters.AddWithValue("@Contract", ShippingWealthDataData.Contract);
						sqliteCommand.Parameters.AddWithValue("@Colonist", ShippingWealthDataData.Colonist);
						sqliteCommand.Parameters.AddWithValue("@Fuel", ShippingWealthDataData.Fuel);
						sqliteCommand.Parameters.AddWithValue("@Amount", ShippingWealthDataData.Amount);
						sqliteCommand.Parameters.AddWithValue("@TradeGood", ShippingWealthDataData.TradeGood);
						sqliteCommand.Parameters.AddWithValue("@TradeTime", ShippingWealthDataData.TradeTime);
						sqliteCommand.Parameters.AddWithValue("@OriginPop", ShippingWealthDataData.OriginPop);
						sqliteCommand.Parameters.AddWithValue("@DestinationPop", ShippingWealthDataData.DestinationPop);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1453);
			}
		}
	}

	public class SaveMoveOrders
	{
		public struct MoveOrdersData
		{
			public int MoveOrderID;
			public int RaceID;
			public int FleetID;
			public GEnum120 MoveActionID;
			public int MoveOrder;
			public int StartSystemID;
			public GEnum12 DestinationType;
			public int DestinationID;
			public decimal PopulationID;
			public GEnum122 DestinationItemType;
			public int DestinationItemID;
			public decimal MaxItems;
			public int OrderDelay;
			public int OrderDelayRemaining;
			public int OrbDistance;
			public double MinDistance;
			public decimal MinQuantity;
			public decimal NewSystemID;
			public decimal NewWarpPointID;
			public string Description;
			public bool Arrived;
			public decimal SurveyPointsRequired;
			public int TimeRequired;
			public string MessageText;
			public bool LoadSubUnits;
		}

		MoveOrdersData[] MoveOrdersDataStore;


		public SaveMoveOrders(GClass0 game)
		{
			//MoveOrder = GClass127
			//FleetList = game.dictrionary_1
			//Fleet = GClass78
			//
			List<GClass127> list = game.dictionary_1.Values.SelectMany<GClass78, GClass127>((Func<GClass78, IEnumerable<GClass127>>) (x => (IEnumerable<GClass127>) x.dictionary_0.Values)).ToList<GClass127>();

			MoveOrdersDataStore = new MoveOrdersData[list.Count()];
			decimal num = 0m;
			decimal num2 = 0m;
			decimal num3 = 0m;
			int i = 0;
			foreach (GClass127 moveOrder in list)
			{
				num = 0m;
				num2 = 0m;
				num3 = 0m;
				if (moveOrder.gclass132_0 != null)
				{
					num = moveOrder.gclass132_0.int_5;
				}

				if (moveOrder.gclass180_1 != null)
				{
					num2 = moveOrder.gclass180_1.gclass178_0.int_0;
				}

				if (moveOrder.gclass111_0 != null)
				{
					num3 = moveOrder.gclass111_0.int_0;
				}

				var dataObj = new MoveOrdersData()
				{
					MoveOrderID = moveOrder.int_0,
					RaceID = moveOrder.gclass21_0.RaceID,
					FleetID = moveOrder.gclass78_0.int_3,
					MoveActionID = moveOrder.gclass123_0.genum120_0,
					MoveOrder = moveOrder.int_1,
					StartSystemID = moveOrder.gclass180_0.gclass178_0.int_0,
					DestinationType = moveOrder.genum12_0,
					DestinationID = moveOrder.int_2,
					PopulationID = num,
					DestinationItemType = moveOrder.genum122_0,
					DestinationItemID = moveOrder.int_7,
					MaxItems = moveOrder.decimal_1,
					OrderDelay = moveOrder.int_3,
					OrderDelayRemaining = moveOrder.int_4,
					OrbDistance = moveOrder.int_5,
					MinDistance = moveOrder.double_0,
					MinQuantity = moveOrder.decimal_2,
					NewSystemID = num2,
					NewWarpPointID = num3,
					Description = moveOrder.string_0,
					Arrived = moveOrder.bool_0,
					SurveyPointsRequired = moveOrder.decimal_0,
					TimeRequired = moveOrder.int_6,
					MessageText = moveOrder.string_1,
					LoadSubUnits = moveOrder.bool_1,
				};
				MoveOrdersDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MoveOrders WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var MoveOrdersData in MoveOrdersDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_MoveOrders (MoveOrderID, GameID, RaceID, FleetID, MoveActionID, MoveOrder, StartSystemID, DestinationType, DestinationID, PopulationID, DestinationItemType, DestinationItemID, MaxItems, OrderDelay, OrderDelayRemaining, OrbDistance, MinDistance, MinQuantity, NewSystemID, NewWarpPointID, Description, Arrived, SurveyPointsRequired, TimeRequired, MessageText, LoadSubUnits ) \r\n                        VALUES ( @MoveOrderID, @GameID, @RaceID, @FleetID, @MoveActionID, @MoveOrder, @StartSystemID, @DestinationType, @DestinationID, @PopulationID, @DestinationItemType, @DestinationItemID, @MaxItems, @OrderDelay, @OrderDelayRemaining, @OrbDistance, @MinDistance, @MinQuantity, @NewSystemID, @NewWarpPointID, @Description, @Arrived, @SurveyPointsRequired, @TimeRequired, @MessageText, @LoadSubUnits )";
						sqliteCommand.Parameters.AddWithValue("@MoveOrderID", MoveOrdersData.MoveOrderID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", MoveOrdersData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@FleetID", MoveOrdersData.FleetID);
						sqliteCommand.Parameters.AddWithValue("@MoveActionID", MoveOrdersData.MoveActionID);
						sqliteCommand.Parameters.AddWithValue("@MoveOrder", MoveOrdersData.MoveOrder);
						sqliteCommand.Parameters.AddWithValue("@StartSystemID", MoveOrdersData.StartSystemID);
						sqliteCommand.Parameters.AddWithValue("@DestinationType", MoveOrdersData.DestinationType);
						sqliteCommand.Parameters.AddWithValue("@DestinationID", MoveOrdersData.DestinationID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", MoveOrdersData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@DestinationItemType",
							MoveOrdersData.DestinationItemType);
						sqliteCommand.Parameters.AddWithValue("@DestinationItemID", MoveOrdersData.DestinationItemID);
						sqliteCommand.Parameters.AddWithValue("@MaxItems", MoveOrdersData.MaxItems);
						sqliteCommand.Parameters.AddWithValue("@OrderDelay", MoveOrdersData.OrderDelay);
						sqliteCommand.Parameters.AddWithValue("@OrderDelayRemaining",
							MoveOrdersData.OrderDelayRemaining);
						sqliteCommand.Parameters.AddWithValue("@OrbDistance", MoveOrdersData.OrbDistance);
						sqliteCommand.Parameters.AddWithValue("@MinDistance", MoveOrdersData.MinDistance);
						sqliteCommand.Parameters.AddWithValue("@MinQuantity", MoveOrdersData.MinQuantity);
						sqliteCommand.Parameters.AddWithValue("@NewSystemID", MoveOrdersData.NewSystemID);
						sqliteCommand.Parameters.AddWithValue("@NewWarpPointID", MoveOrdersData.NewWarpPointID);
						sqliteCommand.Parameters.AddWithValue("@Description", MoveOrdersData.Description);
						sqliteCommand.Parameters.AddWithValue("@Arrived", MoveOrdersData.Arrived);
						sqliteCommand.Parameters.AddWithValue("@SurveyPointsRequired",
							MoveOrdersData.SurveyPointsRequired);
						sqliteCommand.Parameters.AddWithValue("@TimeRequired", MoveOrdersData.TimeRequired);
						sqliteCommand.Parameters.AddWithValue("@MessageText", MoveOrdersData.MessageText);
						sqliteCommand.Parameters.AddWithValue("@LoadSubUnits", MoveOrdersData.LoadSubUnits);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1454);
			}
		}
	}

	/// <summary>
	/// method_78
	/// </summary>
	public class SaveMoveOrderTemplate
	{
		public struct MoveOrderTemplateData
		{
			public int OrderTemplateID;
			public int MoveIndex;
			public GEnum120 MoveActionID;
			public int StartSystemID;
			public GEnum12 DestinationType;
			public int DestinationID;
			public decimal PopulationID;
			public GEnum122 DestinationItemType;
			public int DestinationItemID;
			public decimal MaxItems;
			public int OrderDelay;
			public int OrbDistance;
			public double MinDistance;
			public decimal MinQuantity;
			public decimal NewSystemID;
			public decimal NewJumpPointID;
			public string Description;
			public decimal SurveyPointsRequired;
			public string MessageText;
			public bool LoadSubUnits;
		}
		MoveOrderTemplateData[] MoveOrderTemplateDataStore;


		public SaveMoveOrderTemplate(GClass0 game)
		{
			
			decimal num = 0m;
			decimal num2 = 0m;
			decimal num3 = 0m;
			
			var list = game.dictionary_31.Values.SelectMany<GClass130, GClass131>((Func<GClass130, IEnumerable<GClass131>>)(x => (IEnumerable<GClass131>) x.list_0)).ToList<GClass131>();
			MoveOrderTemplateDataStore = new MoveOrderTemplateData[list.Count()];
			int i = 0;
			foreach (GClass131 gclass in list)
			{
				if (gclass.gclass132_0 != null)
				{
					num = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass180_1 != null)
				{
					num2 = gclass.gclass180_1.gclass178_0.int_0;
				}
				if (gclass.gclass111_0 != null)
				{
					num3 = gclass.gclass111_0.int_0;
				}
				var dataObj = new MoveOrderTemplateData()
				{
					OrderTemplateID = gclass.gclass130_0.int_0,
					MoveIndex = gclass.int_0,
					MoveActionID = gclass.gclass123_0.genum120_0,
					StartSystemID = gclass.gclass180_0.gclass178_0.int_0,
					DestinationType = gclass.genum12_0,
					DestinationID = gclass.int_3,
					PopulationID = num,
					DestinationItemType = gclass.genum122_0,
					DestinationItemID = gclass.int_1,
					MaxItems = gclass.decimal_1,
					OrderDelay = gclass.int_2,
					OrbDistance = gclass.int_4,
					MinDistance = gclass.double_0,
					MinQuantity = gclass.decimal_0,
					NewSystemID = num2,
					NewJumpPointID = num3,
					Description = gclass.string_0,
					SurveyPointsRequired = gclass.decimal_2,
					MessageText = gclass.string_1,
					LoadSubUnits = gclass.bool_0,
				};
				MoveOrderTemplateDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MoveOrderTemplate WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var MoveOrderTemplateData in MoveOrderTemplateDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_MoveOrderTemplate (OrderTemplateID, MoveIndex, GameID, MoveActionID, StartSystemID, DestinationType, DestinationID, PopulationID, DestinationItemType, DestinationItemID, MaxItems, OrderDelay, OrbDistance, MinDistance, MinQuantity, NewSystemID, NewJumpPointID, Description, SurveyPointsRequired, MessageText, LoadSubUnits ) \r\n                        VALUES ( @OrderTemplateID, @MoveIndex, @GameID, @MoveActionID, @StartSystemID, @DestinationType, @DestinationID, @PopulationID, @DestinationItemType, @DestinationItemID, @MaxItems, @OrderDelay, @OrbDistance, @MinDistance, @MinQuantity, @NewSystemID, @NewJumpPointID, @Description, @SurveyPointsRequired, @MessageText, @LoadSubUnits )";
						sqliteCommand.Parameters.AddWithValue("@OrderTemplateID", MoveOrderTemplateData.OrderTemplateID);
						sqliteCommand.Parameters.AddWithValue("@MoveIndex", MoveOrderTemplateData.MoveIndex);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@MoveActionID", MoveOrderTemplateData.MoveActionID);
						sqliteCommand.Parameters.AddWithValue("@StartSystemID", MoveOrderTemplateData.StartSystemID);
						sqliteCommand.Parameters.AddWithValue("@DestinationType", MoveOrderTemplateData.DestinationType);
						sqliteCommand.Parameters.AddWithValue("@DestinationID", MoveOrderTemplateData.DestinationID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", MoveOrderTemplateData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@DestinationItemType", MoveOrderTemplateData.DestinationItemType);
						sqliteCommand.Parameters.AddWithValue("@DestinationItemID", MoveOrderTemplateData.DestinationItemID);
						sqliteCommand.Parameters.AddWithValue("@MaxItems", MoveOrderTemplateData.MaxItems);
						sqliteCommand.Parameters.AddWithValue("@OrderDelay", MoveOrderTemplateData.OrderDelay);
						sqliteCommand.Parameters.AddWithValue("@OrbDistance", MoveOrderTemplateData.OrbDistance);
						sqliteCommand.Parameters.AddWithValue("@MinDistance", MoveOrderTemplateData.MinDistance);
						sqliteCommand.Parameters.AddWithValue("@MinQuantity", MoveOrderTemplateData.MinQuantity);
						sqliteCommand.Parameters.AddWithValue("@NewSystemID", MoveOrderTemplateData.NewSystemID);
						sqliteCommand.Parameters.AddWithValue("@NewJumpPointID", MoveOrderTemplateData.NewJumpPointID);
						sqliteCommand.Parameters.AddWithValue("@Description", MoveOrderTemplateData.Description);
						sqliteCommand.Parameters.AddWithValue("@SurveyPointsRequired", MoveOrderTemplateData.SurveyPointsRequired);
						sqliteCommand.Parameters.AddWithValue("@MessageText", MoveOrderTemplateData.MessageText);
						sqliteCommand.Parameters.AddWithValue("@LoadSubUnits", MoveOrderTemplateData.LoadSubUnits);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1455);
			}
		}
	}

	/// <summary>
	/// method_79
	/// </summary>
	public class SaveSystemBody
	{
		public struct SystemBodyData
		{
			public int SystemBodyID;
			public GEnum108 DominantTerrain;
			public int SystemID;
			public int StarID;
			public int PlanetNumber;
			public int OrbitNumber;
			public GEnum11 BodyClass;
			public double Radius;
			public int ParentBodyID;
			public GEnum2 ParentBodyType;
			public AuroraSystemBodyType BodyTypeID;
			public int Tilt;
			public AuroraTectonics TectonicActivity;
			public AuroraHydrosphereType HydroID;
			public double HydroExt;
			public int RuinID;
			public int RuinRaceID;
			public int AbandonedFactories;
			public double TrojanAsteroid;
			public double OrbitalDistance;
			public double CurrentDistance;
			public double Bearing;
			public double Density;
			public double Mass;
			public double Gravity;
			public double EscapeVelocity;
			public double Year;
			public double TidalForce;
			public double Eccentricity;
			public double DayValue;
			public double Roche;
			public double MagneticField;
			public double BaseTemp;
			public double SurfaceTemp;
			public double AtmosPress;
			public double Albedo;
			public double GHFactor;
			public double AGHFactor;
			public double Xcor;
			public double Ycor;
			public decimal RadiationLevel;
			public decimal DustLevel;
			public bool HeadingInward;
			public bool TidalLock;
			public bool Ring;
			public AuroraGroundMineralSurvey GroundMineralSurvey;
			public string Name;
			public string PlanetIcon;
			public int AsteroidBeltID;
		}
		SystemBodyData[] SystemBodyDataStore;


		public SaveSystemBody(GClass0 game)
		{
			
			int i = 0;
			//(SystemBody systemBody in this.SystemBodyList.Values.Where<SystemBody>((Func<SystemBody, bool>) (x => x.SaveStatus == AuroraSaveStatus.New)).ToList<SystemBody>()
			List<GClass1> list = game.dictionary_11.Values
				.Where<GClass1>((Func<GClass1, bool>)(x => x.genum5_0 == GEnum5.const_2)).ToList<GClass1>();
			SystemBodyDataStore = new SystemBodyData[list.Count()];
			foreach (GClass1 gclass in list)
			{
				gclass.genum5_0 = GEnum5.const_0;
				var dataObj = new SystemBodyData()
				{
					SystemBodyID = gclass.int_0,
					DominantTerrain = gclass.gclass91_0.genum108_0,
					SystemID = gclass.gclass178_0.int_0,
					StarID = gclass.int_2,
					PlanetNumber = gclass.int_3,
					OrbitNumber = gclass.int_4,
					BodyClass = gclass.genum11_0,
					Radius = gclass.double_22,
					ParentBodyID = gclass.int_5,
					ParentBodyType = gclass.genum2_0,
					BodyTypeID = gclass.auroraSystemBodyType_0,
					Tilt = gclass.int_7,
					TectonicActivity = gclass.auroraTectonics_0,
					HydroID = gclass.auroraHydrosphereType_0,
					HydroExt = gclass.double_21,
					RuinID = gclass.int_8,
					RuinRaceID = gclass.int_9,
					AbandonedFactories = gclass.int_10,
					TrojanAsteroid = gclass.double_23,
					OrbitalDistance = gclass.double_2,
					CurrentDistance = gclass.double_5,
					Bearing = gclass.double_6,
					Density = gclass.double_7,
					Mass = gclass.double_9,
					Gravity = gclass.double_8,
					EscapeVelocity = gclass.double_10,
					Year = gclass.double_11,
					TidalForce = gclass.double_12,
					Eccentricity = gclass.double_13,
					DayValue = gclass.double_14,
					Roche = gclass.double_15,
					MagneticField = gclass.double_16,
					BaseTemp = gclass.double_3,
					SurfaceTemp = gclass.double_4,
					AtmosPress = gclass.double_17,
					Albedo = gclass.double_18,
					GHFactor = gclass.double_19,
					AGHFactor = gclass.double_20,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					RadiationLevel = gclass.decimal_3,
					DustLevel = gclass.decimal_4,
					HeadingInward = gclass.bool_0,
					TidalLock = gclass.bool_1,
					Ring = gclass.bool_2,
					GroundMineralSurvey = gclass.auroraGroundMineralSurvey_0,
					Name = gclass.string_0,
					PlanetIcon = gclass.string_1,
					AsteroidBeltID = gclass.int_6,
				};
				SystemBodyDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var SystemBodyData in SystemBodyDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_SystemBody (SystemBodyID, DominantTerrain, SystemID, StarID, GameID, PlanetNumber, OrbitNumber, BodyClass, Radius, ParentBodyID, ParentBodyType, BodyTypeID, Tilt, TectonicActivity, HydroID, HydroExt, RuinID, RuinRaceID, AbandonedFactories, TrojanAsteroid, OrbitalDistance, CurrentDistance, Bearing, Density, Mass, Gravity, EscapeVelocity, Year, TidalForce, \r\n                        Eccentricity, DayValue, Roche, MagneticField, BaseTemp, SurfaceTemp, AtmosPress, Albedo, GHFactor, AGHFactor, Xcor, Ycor, RadiationLevel, DustLevel, HeadingInward, TidalLock, Ring, GroundMineralSurvey, Name, PlanetIcon, AsteroidBeltID) \r\n                        VALUES (@SystemBodyID, @DominantTerrain, @SystemID, @StarID, @GameID, @PlanetNumber, @OrbitNumber, @BodyClass, @Radius, @ParentBodyID, @ParentBodyType, @BodyTypeID, @Tilt, @TectonicActivity, @HydroID, @HydroExt, @RuinID, @RuinRaceID, @AbandonedFactories, @TrojanAsteroid, @OrbitalDistance, @CurrentDistance, @Bearing, @Density, @Mass, @Gravity, @EscapeVelocity, @Year, @TidalForce, \r\n                        @Eccentricity, @DayValue, @Roche, @MagneticField, @BaseTemp, @SurfaceTemp, @AtmosPress, @Albedo, @GHFactor, @AGHFactor, @Xcor, @Ycor, @RadiationLevel, @DustLevel, @HeadingInward, @TidalLock, @Ring, @GroundMineralSurvey, @Name, @PlanetIcon, @AsteroidBeltID )";
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", SystemBodyData.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@DominantTerrain", SystemBodyData.DominantTerrain);
						sqliteCommand.Parameters.AddWithValue("@SystemID", SystemBodyData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@StarID", SystemBodyData.StarID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@PlanetNumber", SystemBodyData.PlanetNumber);
						sqliteCommand.Parameters.AddWithValue("@OrbitNumber", SystemBodyData.OrbitNumber);
						sqliteCommand.Parameters.AddWithValue("@BodyClass", SystemBodyData.BodyClass);
						sqliteCommand.Parameters.AddWithValue("@Radius", SystemBodyData.Radius);
						sqliteCommand.Parameters.AddWithValue("@ParentBodyID", SystemBodyData.ParentBodyID);
						sqliteCommand.Parameters.AddWithValue("@ParentBodyType", SystemBodyData.ParentBodyType);
						sqliteCommand.Parameters.AddWithValue("@BodyTypeID", SystemBodyData.BodyTypeID);
						sqliteCommand.Parameters.AddWithValue("@Tilt", SystemBodyData.Tilt);
						sqliteCommand.Parameters.AddWithValue("@TectonicActivity", SystemBodyData.TectonicActivity);
						sqliteCommand.Parameters.AddWithValue("@HydroID", SystemBodyData.HydroID);
						sqliteCommand.Parameters.AddWithValue("@HydroExt", SystemBodyData.HydroExt);
						sqliteCommand.Parameters.AddWithValue("@RuinID", SystemBodyData.RuinID);
						sqliteCommand.Parameters.AddWithValue("@RuinRaceID", SystemBodyData.RuinRaceID);
						sqliteCommand.Parameters.AddWithValue("@AbandonedFactories", SystemBodyData.AbandonedFactories);
						sqliteCommand.Parameters.AddWithValue("@TrojanAsteroid", SystemBodyData.TrojanAsteroid);
						sqliteCommand.Parameters.AddWithValue("@OrbitalDistance", SystemBodyData.OrbitalDistance);
						sqliteCommand.Parameters.AddWithValue("@CurrentDistance", SystemBodyData.CurrentDistance);
						sqliteCommand.Parameters.AddWithValue("@Bearing", SystemBodyData.Bearing);
						sqliteCommand.Parameters.AddWithValue("@Density", SystemBodyData.Density);
						sqliteCommand.Parameters.AddWithValue("@Mass", SystemBodyData.Mass);
						sqliteCommand.Parameters.AddWithValue("@Gravity", SystemBodyData.Gravity);
						sqliteCommand.Parameters.AddWithValue("@EscapeVelocity", SystemBodyData.EscapeVelocity);
						sqliteCommand.Parameters.AddWithValue("@Year", SystemBodyData.Year);
						sqliteCommand.Parameters.AddWithValue("@TidalForce", SystemBodyData.TidalForce);
						sqliteCommand.Parameters.AddWithValue("@Eccentricity", SystemBodyData.Eccentricity);
						sqliteCommand.Parameters.AddWithValue("@DayValue", SystemBodyData.DayValue);
						sqliteCommand.Parameters.AddWithValue("@Roche", SystemBodyData.Roche);
						sqliteCommand.Parameters.AddWithValue("@MagneticField", SystemBodyData.MagneticField);
						sqliteCommand.Parameters.AddWithValue("@BaseTemp", SystemBodyData.BaseTemp);
						sqliteCommand.Parameters.AddWithValue("@SurfaceTemp", SystemBodyData.SurfaceTemp);
						sqliteCommand.Parameters.AddWithValue("@AtmosPress", SystemBodyData.AtmosPress);
						sqliteCommand.Parameters.AddWithValue("@Albedo", SystemBodyData.Albedo);
						sqliteCommand.Parameters.AddWithValue("@GHFactor", SystemBodyData.GHFactor);
						sqliteCommand.Parameters.AddWithValue("@AGHFactor", SystemBodyData.AGHFactor);
						sqliteCommand.Parameters.AddWithValue("@Xcor", SystemBodyData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", SystemBodyData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@RadiationLevel", SystemBodyData.RadiationLevel);
						sqliteCommand.Parameters.AddWithValue("@DustLevel", SystemBodyData.DustLevel);
						sqliteCommand.Parameters.AddWithValue("@HeadingInward", SystemBodyData.HeadingInward);
						sqliteCommand.Parameters.AddWithValue("@TidalLock", SystemBodyData.TidalLock);
						sqliteCommand.Parameters.AddWithValue("@Ring", SystemBodyData.Ring);
						sqliteCommand.Parameters.AddWithValue("@GroundMineralSurvey", SystemBodyData.GroundMineralSurvey);
						sqliteCommand.Parameters.AddWithValue("@Name", SystemBodyData.Name);
						sqliteCommand.Parameters.AddWithValue("@PlanetIcon", SystemBodyData.PlanetIcon);
						sqliteCommand.Parameters.AddWithValue("@AsteroidBeltID", SystemBodyData.AsteroidBeltID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1456);
			}
		}
	}

}

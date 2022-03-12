using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;


namespace Aurora
{
	
	/// <summary>
	/// method_80
	/// </summary>
	public class DelSystemBody
	{
		private int[] SysBodyIDs;
		public DelSystemBody(GClass0 game)
		{
			
			int i = 0;
			SysBodyIDs = new int[game.list_0.Count]; //game.DeletedSystemBodies
			foreach (GClass1 gclass in game.list_0)
			{
				SysBodyIDs[i] = gclass.int_0;
				i++;
			}
		}
		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (int sysBodyID in SysBodyIDs)
					{
						sqliteCommand.CommandText = "DELETE FROM FCT_SystemBody WHERE SystemBodyID = " + sysBodyID;
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1457);
			}
		}

	}
	
	/// <summary>
	/// method_81
	/// </summary>
	public class UpdateSystemBodies
	{
		public struct SysBodyData1
		{
			public int SystemBodyID;
			public GEnum108 DominantTerrain;
			public int AbandonedFactories;
			public int RuinID;
			public int RuinRaceID;
			public double Bearing;
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
			public AuroraGroundMineralSurvey GroundMineralSurvey;
			public string Name;
			public double HydroExt;
			public AuroraHydrosphereType HydroID;
			public double CurrentDistance;
		}
		SysBodyData1[] SysBodyData1Store;

		public struct SysBodyData2
		{
			public int SystemBodyID;
			public GEnum108 DominantTerrain;
			public int AbandonedFactories;
			public int RuinID;
			public int RuinRaceID;
			public double Bearing;
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
			public AuroraGroundMineralSurvey GroundMineralSurvey;
			public string Name;
			public double HydroExt;
			public AuroraHydrosphereType HydroID;
			public double CurrentDistance;
			public int PlanetNumber;
			public int OrbitNumber;
			public double Radius;
			public double OrbitalDistance;
			public double Density;
			public double Mass;
			public double Gravity;
			public double EscapeVelocity;
			public double Year;
			public double TidalForce;
			public double DayValue;
			public double BaseTemp;
			public bool TidalLock;
			public bool Ring;
		}
		SysBodyData2[] SysBodyData2Store;


		public UpdateSystemBodies(GClass0 game, bool changeState)
		{
			//this.SystemBodyList.Values.Where<SystemBody>((Func<SystemBody, bool>) (x => x.SaveStatus == AuroraSaveStatus.Loaded)).ToList<SystemBody>())
			//SystemBodyList = dictionary_11
			//SystemBody = GClass1;
			//SaveStatus = GClass0
			//AuroraSaveStatus.Loaded = GEnum5.const_0 (Best Guess)
			//AuroraSaveStatus.Updated = GEnum5.const_1 (Best Guess)
			
			//List of SystemBodys where SaveStatus is Loaded. 
			var list = game.dictionary_11.Values.Where<GClass1>((Func<GClass1, bool>) (x => x.genum5_0 == GEnum5.const_0)).ToList<GClass1>();
			SysBodyData1Store = new SysBodyData1[list.Count];
			int i = 0;
			foreach (GClass1 gclass in list)
			{
				var dataObj = new SysBodyData1()
				{
					SystemBodyID = gclass.int_0,
					DominantTerrain = gclass.gclass91_0.genum108_0,
					AbandonedFactories = gclass.int_10,
					RuinID = gclass.int_8,
					RuinRaceID = gclass.int_9,
					Bearing = gclass.double_6,
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
					GroundMineralSurvey = gclass.auroraGroundMineralSurvey_0,
					Name = gclass.string_0,
					HydroExt = gclass.double_21,
					HydroID = gclass.auroraHydrosphereType_0,
					CurrentDistance = gclass.double_5,
				};
				SysBodyData1Store[i] = dataObj;
				i++;
			}
			//List of SystemBodies where SaveStatus is Updated
			list = game.dictionary_11.Values.Where<GClass1>((Func<GClass1, bool>) (x => x.genum5_0 == GEnum5.const_1)).ToList<GClass1>();
			SysBodyData2Store = new SysBodyData2[list.Count];
			i = 0;
			foreach (GClass1 gclass2 in list)
			{
				var dataObj = new SysBodyData2()
				{
					SystemBodyID = gclass2.int_0,
					DominantTerrain = gclass2.gclass91_0.genum108_0,
					AbandonedFactories = gclass2.int_10,
					RuinID = gclass2.int_8,
					RuinRaceID = gclass2.int_9,
					Bearing = gclass2.double_6,
					SurfaceTemp = gclass2.double_4,
					AtmosPress = gclass2.double_17,
					Albedo = gclass2.double_18,
					GHFactor = gclass2.double_19,
					AGHFactor = gclass2.double_20,
					Xcor = gclass2.double_0,
					Ycor = gclass2.double_1,
					RadiationLevel = gclass2.decimal_3,
					DustLevel = gclass2.decimal_4,
					HeadingInward = gclass2.bool_0,
					GroundMineralSurvey = gclass2.auroraGroundMineralSurvey_0,
					Name = gclass2.string_0,
					HydroExt = gclass2.double_21,
					HydroID = gclass2.auroraHydrosphereType_0,
					CurrentDistance = gclass2.double_5,
					PlanetNumber = gclass2.int_3,
					OrbitNumber = gclass2.int_4,
					Radius = gclass2.double_22,
					OrbitalDistance = gclass2.double_2,
					Density = gclass2.double_7,
					Mass = gclass2.double_9,
					Gravity = gclass2.double_8,
					EscapeVelocity = gclass2.double_10,
					Year = gclass2.double_11,
					TidalForce = gclass2.double_12,
					DayValue = gclass2.double_14,
					BaseTemp = gclass2.double_3,
					TidalLock = gclass2.bool_1,
					Ring = gclass2.bool_2,
				};
				SysBodyData2Store[i] = dataObj;
				if(changeState)
				{
					gclass2.genum5_0 = GEnum5.const_0; //Set SaveStatus to Loaded
				}
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in SysBodyData1Store )
					{
						sqliteCommand.CommandText = "UPDATE FCT_SystemBody SET DominantTerrain = @DominantTerrain, AbandonedFactories = @AbandonedFactories, RuinID = @RuinID, RuinRaceID = @RuinRaceID, Bearing = @Bearing, SurfaceTemp = @SurfaceTemp, AtmosPress = @AtmosPress, Albedo = @Albedo, GHFactor = @GHFactor, AGHFactor = @AGHFactor, Xcor = @Xcor, Ycor = @Ycor, RadiationLevel = @RadiationLevel, DustLevel = @DustLevel, \r\n                        HeadingInward = @HeadingInward, GroundMineralSurvey = @GroundMineralSurvey, Name = @Name, HydroExt = @HydroExt, HydroID = @HydroID, CurrentDistance = @CurrentDistance \r\n                        WHERE SystemBodyID = @SystemBodyID";
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", dataObj.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@DominantTerrain", dataObj.DominantTerrain);
						sqliteCommand.Parameters.AddWithValue("@AbandonedFactories", dataObj.AbandonedFactories);
						sqliteCommand.Parameters.AddWithValue("@RuinID", dataObj.RuinID);
						sqliteCommand.Parameters.AddWithValue("@RuinRaceID", dataObj.RuinRaceID);
						sqliteCommand.Parameters.AddWithValue("@Bearing", dataObj.Bearing);
						sqliteCommand.Parameters.AddWithValue("@SurfaceTemp", dataObj.SurfaceTemp);
						sqliteCommand.Parameters.AddWithValue("@AtmosPress", dataObj.AtmosPress);
						sqliteCommand.Parameters.AddWithValue("@Albedo", dataObj.Albedo);
						sqliteCommand.Parameters.AddWithValue("@GHFactor", dataObj.GHFactor);
						sqliteCommand.Parameters.AddWithValue("@AGHFactor", dataObj.AGHFactor);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.Parameters.AddWithValue("@RadiationLevel", dataObj.RadiationLevel);
						sqliteCommand.Parameters.AddWithValue("@DustLevel", dataObj.DustLevel);
						sqliteCommand.Parameters.AddWithValue("@HeadingInward", dataObj.HeadingInward);
						sqliteCommand.Parameters.AddWithValue("@GroundMineralSurvey", dataObj.GroundMineralSurvey);
						sqliteCommand.Parameters.AddWithValue("@Name", dataObj.Name);
						sqliteCommand.Parameters.AddWithValue("@HydroExt", dataObj.HydroExt);
						sqliteCommand.Parameters.AddWithValue("@HydroID", dataObj.HydroID);
						sqliteCommand.Parameters.AddWithValue("@CurrentDistance", dataObj.CurrentDistance);
						sqliteCommand.ExecuteNonQuery();
					}
					foreach (var dataObj in SysBodyData2Store )
					{
						sqliteCommand.CommandText = "UPDATE FCT_SystemBody SET DominantTerrain = @DominantTerrain, AbandonedFactories = @AbandonedFactories, RuinID = @RuinID, RuinRaceID = @RuinRaceID, Bearing = @Bearing, SurfaceTemp = @SurfaceTemp, AtmosPress = @AtmosPress, Albedo = @Albedo, GHFactor = @GHFactor, AGHFactor = @AGHFactor, Xcor = @Xcor, Ycor = @Ycor, RadiationLevel = @RadiationLevel, DustLevel = @DustLevel, HeadingInward = @HeadingInward, \r\n                        GroundMineralSurvey = @GroundMineralSurvey, Name = @Name, HydroExt = @HydroExt, HydroID = @HydroID, PlanetNumber = @PlanetNumber, OrbitNumber = @OrbitNumber, Radius = @Radius, OrbitalDistance = @OrbitalDistance, Density = @Density, Mass = @Mass, Gravity = @Gravity, EscapeVelocity = @EscapeVelocity, Year = @Year, TidalForce = @TidalForce,\r\n                        DayValue = @DayValue, BaseTemp = @BaseTemp, TidalLock = @TidalLock, Ring = @Ring\r\n                        WHERE SystemBodyID = @SystemBodyID";
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", dataObj.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@DominantTerrain", dataObj.DominantTerrain);
						sqliteCommand.Parameters.AddWithValue("@AbandonedFactories", dataObj.AbandonedFactories);
						sqliteCommand.Parameters.AddWithValue("@RuinID", dataObj.RuinID);
						sqliteCommand.Parameters.AddWithValue("@RuinRaceID", dataObj.RuinRaceID);
						sqliteCommand.Parameters.AddWithValue("@Bearing", dataObj.Bearing);
						sqliteCommand.Parameters.AddWithValue("@SurfaceTemp", dataObj.SurfaceTemp);
						sqliteCommand.Parameters.AddWithValue("@AtmosPress", dataObj.AtmosPress);
						sqliteCommand.Parameters.AddWithValue("@Albedo", dataObj.Albedo);
						sqliteCommand.Parameters.AddWithValue("@GHFactor", dataObj.GHFactor);
						sqliteCommand.Parameters.AddWithValue("@AGHFactor", dataObj.AGHFactor);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.Parameters.AddWithValue("@RadiationLevel", dataObj.RadiationLevel);
						sqliteCommand.Parameters.AddWithValue("@DustLevel", dataObj.DustLevel);
						sqliteCommand.Parameters.AddWithValue("@HeadingInward", dataObj.HeadingInward);
						sqliteCommand.Parameters.AddWithValue("@GroundMineralSurvey", dataObj.GroundMineralSurvey);
						sqliteCommand.Parameters.AddWithValue("@Name", dataObj.Name);
						sqliteCommand.Parameters.AddWithValue("@HydroExt", dataObj.HydroExt);
						sqliteCommand.Parameters.AddWithValue("@HydroID", dataObj.HydroID);
						sqliteCommand.Parameters.AddWithValue("@CurrentDistance", dataObj.CurrentDistance);
						sqliteCommand.Parameters.AddWithValue("@PlanetNumber", dataObj.PlanetNumber);
						sqliteCommand.Parameters.AddWithValue("@OrbitNumber", dataObj.OrbitNumber);
						sqliteCommand.Parameters.AddWithValue("@Radius", dataObj.Radius);
						sqliteCommand.Parameters.AddWithValue("@OrbitalDistance", dataObj.OrbitalDistance);
						sqliteCommand.Parameters.AddWithValue("@Density", dataObj.Density);
						sqliteCommand.Parameters.AddWithValue("@Mass", dataObj.Mass);
						sqliteCommand.Parameters.AddWithValue("@Gravity", dataObj.Gravity);
						sqliteCommand.Parameters.AddWithValue("@EscapeVelocity", dataObj.EscapeVelocity);
						sqliteCommand.Parameters.AddWithValue("@Year", dataObj.Year);
						sqliteCommand.Parameters.AddWithValue("@TidalForce", dataObj.TidalForce);
						sqliteCommand.Parameters.AddWithValue("@DayValue", dataObj.DayValue);
						sqliteCommand.Parameters.AddWithValue("@BaseTemp", dataObj.BaseTemp);
						sqliteCommand.Parameters.AddWithValue("@TidalLock", dataObj.TidalLock);
						sqliteCommand.Parameters.AddWithValue("@Ring", dataObj.Ring);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1458);
			}
		}
	}

	
	/// <summary>
	/// method_82
	/// </summary>
	public class SaveSystemBodySurveys
	{
		public struct SystemBodySurveysData
		{
			public int RaceID;
			public int SystemBodyID;
		}
		SystemBodySurveysData[] SystemBodySurveysDataStore;


		public SaveSystemBodySurveys(GClass0 game)
		{
			
			int i = 0;
			//this.list_6.Where(new Func<GClass191, bool>(GClass0.<>c.<>9.method_156)).ToList<GClass191>())
			//this.SystemBodySurveyList.Where<SystemBodySurvey>((Func<SystemBodySurvey, bool>) (x => x.SaveStatus == AuroraSaveStatus.New)).ToList<SystemBodySurvey>())
			List<GClass191> list = game.list_6.Where<GClass191>((Func<GClass191, bool>) (x => x.genum5_0 == GEnum5.const_2)).ToList<GClass191>();
			SystemBodySurveysDataStore = new SystemBodySurveysData[list.Count()];
			foreach (GClass191 gclass in list)
			{
				var dataObj = new SystemBodySurveysData()
				{
					RaceID = gclass.gclass21_0.RaceID,
					SystemBodyID = gclass.gclass1_0.int_0,
				};
				SystemBodySurveysDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var Data in SystemBodySurveysDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_SystemBodySurveys ( GameID, RaceID, SystemBodyID) VALUES ( @GameID, @RaceID, @SystemBodyID )";						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", Data.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", Data.SystemBodyID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1459);
			}
		}
	}
	
	public class SaveSubFleets
	{
		public struct SubFleetsData
		{
			public int SubFleetID;
			public int RaceID;
			public int ParentFleetID;
			public int ParentSubFleetID;
			public string SubFleetName;
			public int AnchorFleetID;
			public int SpecificThreatID;
			public double AnchorFleetDistance;
			public int AnchorFleetBearingOffset;
			public bool GuardNearestHostileContact;
			public bool GuardNearestKnownWarship;
			public bool UseAnchorDestination;
		}
		SubFleetsData[] SubFleetsDataStore;


		public SaveSubFleets(GClass0 game)
		{
			SubFleetsDataStore = new SubFleetsData[game.dictionary_2.Count()];
			int i = 0;
			foreach (GClass77 gclass in game.dictionary_2.Values)
			{
				int num = 0;
				int num2 = 0;
				if (gclass.gclass78_1 != null)
				{
					num = gclass.gclass78_1.int_3;
				}
				if (gclass.gclass108_0 != null)
				{
					num2 = gclass.gclass108_0.int_0;
				}
				int num3 = 0;
				if (gclass.gclass77_0 != null)
				{
					num3 = gclass.gclass77_0.int_1;
				}
				var dataObj = new SubFleetsData()
				{
					SubFleetID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					ParentFleetID = gclass.gclass78_0.int_3,
					ParentSubFleetID = num3,
					SubFleetName = gclass.string_0,
					AnchorFleetID = num,
					SpecificThreatID = num2,
					AnchorFleetDistance = gclass.double_0,
					AnchorFleetBearingOffset = gclass.int_4,
					GuardNearestHostileContact = gclass.bool_1,
					GuardNearestKnownWarship = gclass.bool_2,
					UseAnchorDestination = gclass.bool_3,
				};
				SubFleetsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_SubFleets WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var SubFleetsData in SubFleetsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_SubFleets (SubFleetID, GameID, RaceID, ParentFleetID, ParentSubFleetID, SubFleetName, AnchorFleetID, SpecificThreatID, AnchorFleetDistance, AnchorFleetBearingOffset, GuardNearestHostileContact, GuardNearestKnownWarship, UseAnchorDestination ) \r\n                            VALUES ( @SubFleetID, @GameID, @RaceID, @ParentFleetID, @ParentSubFleetID, @SubFleetName, @AnchorFleetID, @SpecificThreatID, @AnchorFleetDistance, @AnchorFleetBearingOffset, @GuardNearestHostileContact, @GuardNearestKnownWarship, @UseAnchorDestination )";						sqliteCommand.Parameters.AddWithValue("@SubFleetID", SubFleetsData.SubFleetID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", SubFleetsData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@ParentFleetID", SubFleetsData.ParentFleetID);
						sqliteCommand.Parameters.AddWithValue("@ParentSubFleetID", SubFleetsData.ParentSubFleetID);
						sqliteCommand.Parameters.AddWithValue("@SubFleetName", SubFleetsData.SubFleetName);
						sqliteCommand.Parameters.AddWithValue("@AnchorFleetID", SubFleetsData.AnchorFleetID);
						sqliteCommand.Parameters.AddWithValue("@SpecificThreatID", SubFleetsData.SpecificThreatID);
						sqliteCommand.Parameters.AddWithValue("@AnchorFleetDistance", SubFleetsData.AnchorFleetDistance);
						sqliteCommand.Parameters.AddWithValue("@AnchorFleetBearingOffset", SubFleetsData.AnchorFleetBearingOffset);
						sqliteCommand.Parameters.AddWithValue("@GuardNearestHostileContact", SubFleetsData.GuardNearestHostileContact);
						sqliteCommand.Parameters.AddWithValue("@GuardNearestKnownWarship", SubFleetsData.GuardNearestKnownWarship);
						sqliteCommand.Parameters.AddWithValue("@UseAnchorDestination", SubFleetsData.UseAnchorDestination);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1487);
			}
		}
	}
	
	public class SaveFleet
	{
		public struct FleetData
		{
			public int FleetID;
			public string FleetName;
			public int AssignedPopulationID;
			public int ParentCommandID;
			public int OrbitBodyID;
			public int OrbitDistance;
			public double OrbitBearing;
			public int RaceID;
			public int SystemID;
			public int TradeLocation;
			public GEnum103 CivilianFunction;
			public bool NPRHomeGuard;
			public AuroraStandingOrder SpecialOrderID;
			public AuroraStandingOrder SpecialOrderID2;
			public int Speed;
			public int MaxNebulaSpeed;
			public double Xcor;
			public double Ycor;
			public double LastXcor;
			public double LastYcor;
			public decimal LastMoveTime;
			public double IncrementStartX;
			public double IncrementStartY;
			public int EntryJPID;
			public CheckState CycleMoves;
			public AuroraFleetCondition ConditionOne;
			public AuroraFleetCondition ConditionTwo;
			public int ConditionalOrderOne;
			public int ConditionalOrderTwo;
			public int JustDivided;
			public int AnchorFleetID;
			public int SpecificThreatID;
			public double AnchorFleetDistance;
			public int AnchorFleetBearingOffset;
			public bool AvoidAlienSystems;
			public bool AvoidDanger;
			public bool DisplaySensors;
			public bool DisplayWeapons;
			public GEnum102 NPROperationalGroupID;
			public int ShippingLine;
			public bool UseMaximumSpeed;
			public bool RedeployOrderGiven;
			public long MaxStandingOrderDistance;
			public bool NoSurrender;
			public bool GuardNearestHostileContact;
			public bool GuardNearestKnownWarship;
			public bool UseAnchorDestination;
			public FleetHistory[] FleetHistoryStore;
		}
		public struct FleetHistory
		{
			public int FleetID;
			public string Description;
			public decimal GameTime;
		}

		FleetData[] FleetDataStore;
		 

		public SaveFleet(GClass0 game)
		{
			FleetDataStore = new FleetData[game.dictionary_1.Count()];
			int i = 0;
			foreach (GClass78 gclass in game.dictionary_1.Values)
			{
				int num = 0;
				int num2 = 0;
				AuroraStandingOrder auroraStandingOrder = AuroraStandingOrder.NoOrder;
				AuroraStandingOrder auroraStandingOrder2 = AuroraStandingOrder.NoOrder;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				GEnum102 genum = GEnum102.const_0;
				int num7 = 0;
				int num8 = 0;
				bool flag = false;
				if (gclass.gclass132_0 != null)
				{
					num = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass1_0 != null)
				{
					num2 = gclass.gclass1_0.int_0;
				}
				if (gclass.gclass125_0 != null)
				{
					auroraStandingOrder = gclass.gclass125_0.auroraStandingOrder_0;
				}
				if (gclass.gclass125_1 != null)
				{
					auroraStandingOrder2 = gclass.gclass125_1.auroraStandingOrder_0;
				}
				if (gclass.gclass125_2 != null)
				{
					num3 = (int)gclass.gclass125_2.auroraStandingOrder_0;
				}
				if (gclass.gclass125_3 != null)
				{
					num4 = (int)gclass.gclass125_3.auroraStandingOrder_0;
				}
				if (gclass.gclass78_0 != null)
				{
					num5 = gclass.gclass78_0.int_3;
				}
				if (gclass.gclass108_0 != null)
				{
					num8 = gclass.gclass108_0.int_0;
				}
				if (gclass.gclass111_0 != null)
				{
					num6 = gclass.gclass111_0.int_0;
				}
				if (gclass.gclass8_0 != null)
				{
					genum = gclass.gclass8_0.genum102_0;
				}
				if (gclass.gclass165_0 != null)
				{
					num7 = gclass.gclass165_0.int_0;
				}
				if (gclass.gclass4_0 != null)
				{
					flag = gclass.gclass4_0.bool_1;
				}

				FleetHistory[] fleetHistories = new FleetHistory[gclass.list_0.Count];
				int j = 0;
				foreach (GClass158 gclass2 in gclass.list_0)
				{
					var datahObj = new FleetHistory()
					{
						FleetID = gclass.int_3,
						Description = gclass2.Description,
						GameTime = gclass2.decimal_0,
					};
					fleetHistories[j] = datahObj;
					j++;
				}

				var dataObj = new FleetData()
				{
					FleetID = gclass.int_3,
					FleetName = gclass.FleetName,
					AssignedPopulationID = num,
					ParentCommandID = gclass.gclass76_0.int_0,
					OrbitBodyID = num2,
					OrbitDistance = gclass.int_5,
					OrbitBearing = gclass.double_1,
					RaceID = gclass.gclass21_0.RaceID,
					SystemID = gclass.gclass180_0.gclass178_0.int_0,
					TradeLocation = gclass.int_4,
					CivilianFunction = gclass.genum103_0,
					NPRHomeGuard = gclass.bool_3,
					SpecialOrderID = auroraStandingOrder,
					SpecialOrderID2 = auroraStandingOrder2,
					Speed = gclass.int_6,
					MaxNebulaSpeed = gclass.int_7,
					Xcor = gclass.double_2,
					Ycor = gclass.double_3,
					LastXcor = gclass.double_4,
					LastYcor = gclass.double_5,
					LastMoveTime = gclass.decimal_0,
					IncrementStartX = gclass.double_6,
					IncrementStartY = gclass.double_7,
					EntryJPID = num6,
					CycleMoves = gclass.checkState_0,
					ConditionOne = gclass.gclass126_0.auroraFleetCondition_0,
					ConditionTwo = gclass.gclass126_1.auroraFleetCondition_0,
					ConditionalOrderOne = num3,
					ConditionalOrderTwo = num4,
					JustDivided = gclass.int_8,
					AnchorFleetID = num5,
					SpecificThreatID = num8,
					AnchorFleetDistance = gclass.double_0,
					AnchorFleetBearingOffset = gclass.int_2,
					AvoidAlienSystems = gclass.bool_6,
					AvoidDanger = gclass.bool_5,
					DisplaySensors = gclass.bool_7,
					DisplayWeapons = gclass.bool_8,
					NPROperationalGroupID = genum,
					ShippingLine = num7,
					UseMaximumSpeed = gclass.bool_9,
					RedeployOrderGiven = flag,
					MaxStandingOrderDistance = gclass.long_0,
					NoSurrender = gclass.bool_11,
					GuardNearestHostileContact = gclass.bool_0,
					GuardNearestKnownWarship = gclass.bool_1,
					UseAnchorDestination = gclass.bool_2,
					FleetHistoryStore = fleetHistories
				};
				FleetDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Fleet WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_FleetHistory WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var fleetData in FleetDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Fleet (FleetID, GameID, FleetName, AssignedPopulationID, ParentCommandID, OrbitBodyID, OrbitDistance, OrbitBearing, RaceID, SystemID, TradeLocation, CivilianFunction, NPRHomeGuard, SpecialOrderID, SpecialOrderID2, Speed, MaxNebulaSpeed, Xcor, Ycor, LastXcor, LastYcor, LastMoveTime, IncrementStartX, IncrementStartY,\r\n                        EntryJPID, CycleMoves, ConditionOne, ConditionTwo, ConditionalOrderOne, ConditionalOrderTwo, JustDivided, AnchorFleetID, SpecificThreatID, AnchorFleetDistance, AnchorFleetBearingOffset, AvoidAlienSystems, AvoidDanger, DisplaySensors, DisplayWeapons, NPROperationalGroupID, ShippingLine, UseMaximumSpeed, RedeployOrderGiven, \r\n                        MaxStandingOrderDistance, NoSurrender, GuardNearestHostileContact, GuardNearestKnownWarship, UseAnchorDestination) \r\n                        \r\n                        VALUES ( @FleetID, @GameID, @FleetName, @AssignedPopulationID, @ParentCommandID, @OrbitBodyID, @OrbitDistance, @OrbitBearing, @RaceID, @SystemID, @TradeLocation, @CivilianFunction, @NPRHomeGuard, @SpecialOrderID, @SpecialOrderID2, @Speed, @MaxNebulaSpeed, @Xcor, @Ycor, @LastXcor, @LastYcor, @LastMoveTime, @IncrementStartX, @IncrementStartY, \r\n                        @EntryJPID, @CycleMoves, @ConditionOne, @ConditionTwo, @ConditionalOrderOne, @ConditionalOrderTwo, @JustDivided, @AnchorFleetID, @SpecificThreatID, @AnchorFleetDistance, @AnchorFleetBearingOffset, @AvoidAlienSystems, @AvoidDanger, @DisplaySensors, @DisplayWeapons, @NPROperationalGroupID, @ShippingLine, @UseMaximumSpeed, @RedeployOrderGiven, \r\n                        @MaxStandingOrderDistance, @NoSurrender, @GuardNearestHostileContact, @GuardNearestKnownWarship, @UseAnchorDestination)";						
						sqliteCommand.Parameters.AddWithValue("@FleetID", fleetData.FleetID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@FleetName", fleetData.FleetName);
						sqliteCommand.Parameters.AddWithValue("@AssignedPopulationID", fleetData.AssignedPopulationID);
						sqliteCommand.Parameters.AddWithValue("@ParentCommandID", fleetData.ParentCommandID);
						sqliteCommand.Parameters.AddWithValue("@OrbitBodyID", fleetData.OrbitBodyID);
						sqliteCommand.Parameters.AddWithValue("@OrbitDistance", fleetData.OrbitDistance);
						sqliteCommand.Parameters.AddWithValue("@OrbitBearing", fleetData.OrbitBearing);
						sqliteCommand.Parameters.AddWithValue("@RaceID", fleetData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", fleetData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@TradeLocation", fleetData.TradeLocation);
						sqliteCommand.Parameters.AddWithValue("@CivilianFunction", fleetData.CivilianFunction);
						sqliteCommand.Parameters.AddWithValue("@NPRHomeGuard", fleetData.NPRHomeGuard);
						sqliteCommand.Parameters.AddWithValue("@SpecialOrderID", fleetData.SpecialOrderID);
						sqliteCommand.Parameters.AddWithValue("@SpecialOrderID2", fleetData.SpecialOrderID2);
						sqliteCommand.Parameters.AddWithValue("@Speed", fleetData.Speed);
						sqliteCommand.Parameters.AddWithValue("@MaxNebulaSpeed", fleetData.MaxNebulaSpeed);
						sqliteCommand.Parameters.AddWithValue("@Xcor", fleetData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", fleetData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@LastXcor", fleetData.LastXcor);
						sqliteCommand.Parameters.AddWithValue("@LastYcor", fleetData.LastYcor);
						sqliteCommand.Parameters.AddWithValue("@LastMoveTime", fleetData.LastMoveTime);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartX", fleetData.IncrementStartX);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartY", fleetData.IncrementStartY);
						sqliteCommand.Parameters.AddWithValue("@EntryJPID", fleetData.EntryJPID);
						sqliteCommand.Parameters.AddWithValue("@CycleMoves", fleetData.CycleMoves);
						sqliteCommand.Parameters.AddWithValue("@ConditionOne", fleetData.ConditionOne);
						sqliteCommand.Parameters.AddWithValue("@ConditionTwo", fleetData.ConditionTwo);
						sqliteCommand.Parameters.AddWithValue("@ConditionalOrderOne", fleetData.ConditionalOrderOne);
						sqliteCommand.Parameters.AddWithValue("@ConditionalOrderTwo", fleetData.ConditionalOrderTwo);
						sqliteCommand.Parameters.AddWithValue("@JustDivided", fleetData.JustDivided);
						sqliteCommand.Parameters.AddWithValue("@AnchorFleetID", fleetData.AnchorFleetID);
						sqliteCommand.Parameters.AddWithValue("@SpecificThreatID", fleetData.SpecificThreatID);
						sqliteCommand.Parameters.AddWithValue("@AnchorFleetDistance", fleetData.AnchorFleetDistance);
						sqliteCommand.Parameters.AddWithValue("@AnchorFleetBearingOffset", fleetData.AnchorFleetBearingOffset);
						sqliteCommand.Parameters.AddWithValue("@AvoidAlienSystems", fleetData.AvoidAlienSystems);
						sqliteCommand.Parameters.AddWithValue("@AvoidDanger", fleetData.AvoidDanger);
						sqliteCommand.Parameters.AddWithValue("@DisplaySensors", fleetData.DisplaySensors);
						sqliteCommand.Parameters.AddWithValue("@DisplayWeapons", fleetData.DisplayWeapons);
						sqliteCommand.Parameters.AddWithValue("@NPROperationalGroupID", fleetData.NPROperationalGroupID);
						sqliteCommand.Parameters.AddWithValue("@ShippingLine", fleetData.ShippingLine);
						sqliteCommand.Parameters.AddWithValue("@UseMaximumSpeed", fleetData.UseMaximumSpeed);
						sqliteCommand.Parameters.AddWithValue("@RedeployOrderGiven", fleetData.RedeployOrderGiven);
						sqliteCommand.Parameters.AddWithValue("@MaxStandingOrderDistance", fleetData.MaxStandingOrderDistance);
						sqliteCommand.Parameters.AddWithValue("@NoSurrender", fleetData.NoSurrender);
						sqliteCommand.Parameters.AddWithValue("@GuardNearestHostileContact", fleetData.GuardNearestHostileContact);
						sqliteCommand.Parameters.AddWithValue("@GuardNearestKnownWarship", fleetData.GuardNearestKnownWarship);
						sqliteCommand.Parameters.AddWithValue("@UseAnchorDestination", fleetData.UseAnchorDestination);
						sqliteCommand.ExecuteNonQuery();
						foreach (var history in fleetData.FleetHistoryStore)
						{
							sqliteCommand.CommandText = "INSERT INTO FCT_FleetHistory ( GameID, FleetID, Description, GameTime ) VALUES ( @GameID, @FleetID, @Description, @GameTime )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@FleetID", history.FleetID);
							sqliteCommand.Parameters.AddWithValue("@Description", history.Description);
							sqliteCommand.Parameters.AddWithValue("@GameTime", history.GameTime);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1460);
			}
		}
	}
	
	/// <summary>
	/// method_85
	/// </summary>
	public class SavePopulationInstallations
	{
		public struct PopulationInstallationsData
		{
			public int PopID;
			public AuroraInstallationType PlanetaryInstallationID;
			public decimal Amount;
		}
		PopulationInstallationsData[] PopulationInstallationsDataStore;


		public SavePopulationInstallations(GClass0 game)
		{
			//PopulationList = dictionary_20
			//population = GClass132
			//PopulationInstallation = GClass142
			var installations = game.dictionary_20.Values.SelectMany<GClass132, GClass142>((Func<GClass132, IEnumerable<GClass142>>) (x => (IEnumerable<GClass142>) x.dictionary_0.Values)).ToList<GClass142>();
			PopulationInstallationsDataStore = new PopulationInstallationsData[installations.Count];
			int i = 0;
			foreach (GClass142 gclass in installations)
			{
				var dataObj = new PopulationInstallationsData()
				{
					PopID = gclass.gclass132_0.int_5,
					PlanetaryInstallationID = gclass.gclass141_0.auroraInstallationType_0,
					Amount = gclass.decimal_0,
				};
				PopulationInstallationsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_PopulationInstallations WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var PopulationInstallationsData in PopulationInstallationsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_PopulationInstallations ( GameID, PopID, PlanetaryInstallationID, Amount ) VALUES ( @GameID, @PopID, @PlanetaryInstallationID, @Amount )";						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@PopID", PopulationInstallationsData.PopID);
						sqliteCommand.Parameters.AddWithValue("@PlanetaryInstallationID", PopulationInstallationsData.PlanetaryInstallationID);
						sqliteCommand.Parameters.AddWithValue("@Amount", PopulationInstallationsData.Amount);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1461);
			}
		}
	}
	
	/// <summary>
	/// method_86
	/// </summary>
	public class SaveShipCargo
	{
		public struct DataStore
		{
			public ShipCargoData[] ShipCargoStore0;
			public ShipCargoData[] ShipCargoStore1;
			public ShipCargoData[] ShipCargoStore2;
			public ShipCargoData[] ShipCargoStore3;
			public ShipCargoData[] ShipCargoStore4;
		}
		DataStore[] Store;

		public struct ShipCargoData
		{
			public int ShipID;
			public int CargoTypeID;
			public int CargoID;
			public decimal Amount;
			public int SpeciesID;
			public int StartingPop;
			public bool Neutral;
		}
		
		public SaveShipCargo(GClass0 game)
		{
			Store = new DataStore[game.dictionary_4.Count];
			int i = 0;
			foreach (GClass39 gclass in game.dictionary_4.Values)
			{
				
				ShipCargoData[] ShipCargoStore0 = new ShipCargoData[gclass.dictionary_0.Count];
				int j = 0;
				foreach (GClass142 gclass2 in gclass.dictionary_0.Values)
				{
					var dataObj1 = new ShipCargoData()
					{
						ShipID = gclass.int_8,
						CargoTypeID = 2,
						CargoID = (int)gclass2.gclass141_0.auroraInstallationType_0,
						Amount = gclass2.decimal_0,
						SpeciesID = 0,
						StartingPop = gclass2.gclass132_1.int_5,
						Neutral = false,
					};
					ShipCargoStore0[j] = dataObj1;
					j++;
				}
				
				var ShipCargoStore1 = new ShipCargoData[gclass.dictionary_1.Count];
				j = 0;
				foreach (GClass169 gclass3 in gclass.dictionary_1.Values)
				{
					var dataObj1= new ShipCargoData()
					{
						ShipID = gclass.int_8,
						CargoTypeID = 7,
						CargoID = gclass3.gclass167_0.int_0,
						Amount = gclass3.decimal_0,
						SpeciesID = 0,
						StartingPop = gclass3.gclass132_0.int_5,
						Neutral = false,
					};
					ShipCargoStore1[j] = dataObj1;
					j++;
				}
				
				var ShipCargoStore2 = new ShipCargoData[gclass.list_17.Count];
				j = 0;
				foreach (GClass162 gclass4 in gclass.list_17)
				{
					int num = 0;
					if (gclass4.gclass132_0 != null)
					{
						num = gclass4.gclass132_0.int_5;
					}
					var dataObj1= new ShipCargoData()
					{
						ShipID = gclass.int_8,
						CargoTypeID = (int)GEnum29.const_0,
						CargoID = 0,
						Amount = gclass4.int_0,
						SpeciesID = gclass4.gclass172_0.int_0,
						StartingPop = num,
						Neutral = gclass4.bool_0,
					};
					ShipCargoStore2[j] = dataObj1;
					j++;
				}
				
				var ShipCargoStore3 = new ShipCargoData[gclass.list_16.Count];
				j = 0;
				foreach (GClass68 gclass5 in gclass.list_16)
				{
					int num2 = 0;
					if (gclass5.gclass132_0 != null)
					{
						num2 = gclass5.gclass132_0.int_5;
					}
					var dataObj1= new ShipCargoData()
					{
						ShipID = gclass.int_8,
						CargoTypeID = (int)GEnum29.const_4,
						CargoID = gclass5.gclass206_0.int_0,
						Amount = gclass5.decimal_0,
						SpeciesID = 0,
						StartingPop = num2,
						Neutral = false,
					};
					ShipCargoStore3[j] = dataObj1;
					j++;
				}

				var list = gclass.gclass114_0.method_4();
				var ShipCargoStore4 = new ShipCargoData[list.Count];
				j = 0;
				foreach (GClass116 gclass6 in list)
				{
					var dataObj1= new ShipCargoData()
					{
						ShipID = gclass.int_8,
						CargoTypeID = (int)GEnum29.const_2,
						CargoID = (int)gclass6.auroraElement_0,
						Amount = gclass6.decimal_0,
						SpeciesID = 0,
						StartingPop = 0,
						Neutral = false,
					};
					ShipCargoStore4[j] = dataObj1;
					j++;
				}
				
				var dataObj = new DataStore()
				{
					ShipCargoStore0 = ShipCargoStore0,
					ShipCargoStore1 = ShipCargoStore1,
					ShipCargoStore2 = ShipCargoStore2,
					ShipCargoStore3 = ShipCargoStore3,
					ShipCargoStore4 = ShipCargoStore4,
				};
				Store[i] = dataObj;
				i++;
				
				
				
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShipCargo WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in Store)
					{

						foreach (var dataObj1 in dataObj.ShipCargoStore0)
						{
							try
							{
								sqliteCommand.CommandText =
									"INSERT INTO FCT_ShipCargo ( GameID, ShipID, CargoTypeID, CargoID, Amount, SpeciesID, StartingPop, Neutral ) VALUES ( @GameID, @ShipID, @CargoTypeID, @CargoID, @Amount, @SpeciesID, @StartingPop, @Neutral )";
								sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
								sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
								sqliteCommand.Parameters.AddWithValue("@CargoTypeID", dataObj1.CargoTypeID);
								sqliteCommand.Parameters.AddWithValue("@CargoID", dataObj1.CargoID);
								sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
								sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj1.SpeciesID);
								sqliteCommand.Parameters.AddWithValue("@StartingPop", dataObj1.StartingPop);
								sqliteCommand.Parameters.AddWithValue("@Neutral", dataObj1.Neutral);
								sqliteCommand.ExecuteNonQuery();
							}
							catch (Exception exception_)
							{
								GClass202.smethod_68(exception_, 3239);
							}
						}
						foreach (var dataObj1 in dataObj.ShipCargoStore1)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipCargo ( GameID, ShipID, CargoTypeID, CargoID, Amount, SpeciesID, StartingPop, Neutral ) VALUES ( @GameID, @ShipID, @CargoTypeID, @CargoID, @Amount, @SpeciesID, @StartingPop, @Neutral )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@CargoTypeID", dataObj1.CargoTypeID);
							sqliteCommand.Parameters.AddWithValue("@CargoID", dataObj1.CargoID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj1.SpeciesID);
							sqliteCommand.Parameters.AddWithValue("@StartingPop", dataObj1.StartingPop);
							sqliteCommand.Parameters.AddWithValue("@Neutral", dataObj1.Neutral);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ShipCargoStore2)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipCargo ( GameID, ShipID, CargoTypeID, CargoID, Amount, SpeciesID, StartingPop, Neutral ) VALUES ( @GameID, @ShipID, @CargoTypeID, @CargoID, @Amount, @SpeciesID, @StartingPop, @Neutral )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@CargoTypeID", dataObj1.CargoTypeID);
							sqliteCommand.Parameters.AddWithValue("@CargoID", dataObj1.CargoID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj1.SpeciesID);
							sqliteCommand.Parameters.AddWithValue("@StartingPop", dataObj1.StartingPop);
							sqliteCommand.Parameters.AddWithValue("@Neutral", dataObj1.Neutral);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ShipCargoStore3)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipCargo ( GameID, ShipID, CargoTypeID, CargoID, Amount, SpeciesID, StartingPop, Neutral ) VALUES ( @GameID, @ShipID, @CargoTypeID, @CargoID, @Amount, @SpeciesID, @StartingPop, @Neutral )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@CargoTypeID", dataObj1.CargoTypeID);
							sqliteCommand.Parameters.AddWithValue("@CargoID", dataObj1.CargoID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj1.SpeciesID);
							sqliteCommand.Parameters.AddWithValue("@StartingPop", dataObj1.StartingPop);
							sqliteCommand.Parameters.AddWithValue("@Neutral", dataObj1.Neutral);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ShipCargoStore4)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipCargo ( GameID, ShipID, CargoTypeID, CargoID, Amount, SpeciesID, StartingPop, Neutral ) VALUES ( @GameID, @ShipID, @CargoTypeID, @CargoID, @Amount, @SpeciesID, @StartingPop, @Neutral )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@CargoTypeID", dataObj1.CargoTypeID);
							sqliteCommand.Parameters.AddWithValue("@CargoID", dataObj1.CargoID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj1.SpeciesID);
							sqliteCommand.Parameters.AddWithValue("@StartingPop", dataObj1.StartingPop);
							sqliteCommand.Parameters.AddWithValue("@Neutral", dataObj1.Neutral);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_2)
			{
				GClass202.smethod_68(exception_2, 1462);
			}
		}
	}
	
	/// <summary>
	/// method_87
	/// </summary>
	public class SavePopulation
	{
		public struct PopulationData
		{
			public int PopulationID;
			public int RaceID;
			public int SpeciesID;
			public string PopName;
			public decimal AcademyOfficers;
			public bool Capital;
			public GEnum34 TerraformStatus;
			public bool PurchaseCivilianMinerals;
			public GEnum18 ColonistDestination;
			public decimal Efficiency;
			public int FighterDestFleetID;
			public int SpaceStationDestFleetID;
			public bool FuelProdStatus;
			public decimal FuelStockpile;
			public int GenModSpeciesID;
			public int GroundAttackID;
			public decimal LastColonyCost;
			public decimal MaintenanceStockpile;
			public bool MaintProdStatus;
			public int MassDriverDest;
			public double MaxAtm;
			public int NoStatusChange;
			public GEnum30 PoliticalStatus;
			public decimal Population;
			public decimal PreviousUnrest;
			public int ProvideColonists;
			public int ReqInf;
			public decimal StatusPoints;
			public int SystemID;
			public int SystemBodyID;
			public int TempMF;
			public GEnum50 TerraformingGasID;
			public decimal UnrestPoints;
			public decimal GroundGeoSurvey;
			public int DestroyedInstallationSize;
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
			public decimal LastDuranium;
			public decimal LastNeutronium;
			public decimal LastCorbomite;
			public decimal LastTritanium;
			public decimal LastBoronide;
			public decimal LastMercassium;
			public decimal LastVendarite;
			public decimal LastSorium;
			public decimal LastUridium;
			public decimal LastCorundium;
			public decimal LastGallicite;
			public decimal ReserveDuranium;
			public decimal ReserveNeutronium;
			public decimal ReserveCorbomite;
			public decimal ReserveTritanium;
			public decimal ReserveBoronide;
			public decimal ReserveMercassium;
			public decimal ReserveVendarite;
			public decimal ReserveSorium;
			public decimal ReserveUridium;
			public decimal ReserveCorundium;
			public decimal ReserveGallicite;
			public int AIValue;
			public bool InvasionStagingPoint;
			public int OriginalRaceID;
			public bool DoNotDelete;
			public bool MilitaryRestrictedColony;
			public string AcademyName;
			public GEnum118 BonusOne;
			public GEnum118 BonusTwo;
			public GEnum118 BonusThree;
			public bool AutoAssign;
			public int Importance;
			public PopulationWeaponData[] PopulationWeaponStore;
			public PrisonersData[] PrisonersStore;
			public PopComponentData[] PopComponentStore;
			public PopInstallationDemandData[] PopInstallationDemandStore;
			
		}
		PopulationData[] PopulationStore;

		public struct PopulationWeaponData
		{
			public int PopulationID;
			public int MissileID;
			public int Amount;
		}

		public struct PrisonersData
		{
			public int NumPrisoners;
			public int PopulationID;
			public int PrisonerRaceID;
			public int PrisonerSpeciesID;
		}

		public struct PopComponentData
		{
			public int ComponentID;
			public int PopulationID;
			public decimal Amount;
		}
		
		public struct PopInstallationDemandData
		{
			public int PopulationID;
			public AuroraInstallationType InstallationID;
			public decimal Amount;
			public bool Export;
			public bool NonEssential;
		}
		
		public struct PopMDChanges
		{					
			public int PopulationID;
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

		private List<PopMDChanges> PopMdChangesData = new List<PopMDChanges>();

		public SavePopulation(GClass0 game)
		{
			int i = 0;
			PopulationStore = new PopulationData[game.dictionary_20.Count];
			foreach (GClass132 gclass in game.dictionary_20.Values)
			{

				PopulationWeaponData[] PopulationWeaponStore = new PopulationWeaponData[gclass.list_1.Count];
				int j = 0;
				foreach (GClass121 gclass2 in gclass.list_1)
				{
					var dataObj1 = new PopulationWeaponData()
					{
						PopulationID = gclass.int_5,
						MissileID = gclass2.gclass120_0.int_0,
						Amount = gclass2.int_0,
					};
					PopulationWeaponStore[j] = dataObj1;
					j++;
				}
				
				PrisonersData[] PrisonersStore = new PrisonersData[gclass.list_3.Count];
				j = 0;
				foreach (GClass133 gclass3 in gclass.list_3)
				{
					var dataObj1 = new PrisonersData()
					{
						NumPrisoners = gclass3.int_0,
						PopulationID = gclass.int_5,
						PrisonerRaceID = gclass3.gclass21_0.RaceID,
						PrisonerSpeciesID = gclass3.gclass172_0.int_0,
					};
					PrisonersStore[j] = dataObj1;
					j++;
				}
				
				PopComponentData[] PopComponentStore = new PopComponentData[gclass.list_2.Count];
				j = 0;
				foreach (GClass68 gclass4 in gclass.list_2)
				{
					var dataObj1 = new PopComponentData()
					{
						ComponentID = gclass4.gclass206_0.int_0,
						PopulationID = gclass.int_5,
						Amount = gclass4.decimal_0,
					};
					PopComponentStore[j] = dataObj1;
					j++;
				}
				
				PopInstallationDemandData[] PopInstallationDemandStore = new PopInstallationDemandData[gclass.dictionary_5.Count];
				j = 0;
				foreach (GClass134 gclass5 in gclass.dictionary_5.Values)
				{
					var dataObj1 = new PopInstallationDemandData()
					{
						PopulationID = gclass.int_5,
						InstallationID = gclass5.gclass141_0.auroraInstallationType_0,
						Amount = gclass5.decimal_0,
						Export = gclass5.bool_0,
						NonEssential = gclass5.bool_1,
					};
					PopInstallationDemandStore[j] = dataObj1;
					j++;
				}
				

				int num = 0;
				int num2 = 0;
				int num3 = 0;
				GEnum50 genum = GEnum50.const_0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				if (gclass.gclass78_0 != null)
				{
					num = gclass.gclass78_0.int_3;
				}
				if (gclass.gclass78_1 != null)
				{
					num2 = gclass.gclass78_1.int_3;
				}
				if (gclass.gclass172_1 != null)
				{
					num3 = gclass.gclass172_1.int_0;
				}
				if (gclass.gclass199_0 != null)
				{
					genum = gclass.gclass199_0.genum50_0;
				}
				if (gclass.gclass132_0 != null)
				{
					num4 = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass6_0 != null)
				{
					num5 = (int)gclass.gclass6_0.genum94_0;
				}
				if (gclass.gclass21_1 != null)
				{
					num6 = gclass.gclass21_1.RaceID;
				}
				if (gclass.gclass78_0 != null)
				{
					num = gclass.gclass78_0.int_3;
				}
				if (gclass.gclass78_1 != null)
				{
					num2 = gclass.gclass78_1.int_3;
				}
				if (gclass.gclass172_1 != null)
				{
					num3 = gclass.gclass172_1.int_0;
				}
				if (gclass.gclass199_0 != null)
				{
					genum = gclass.gclass199_0.genum50_0;
				}
				if (gclass.gclass132_0 != null)
				{
					num4 = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass6_0 != null)
				{
					num5 = (int)gclass.gclass6_0.genum94_0;
				}
				if (gclass.gclass21_1 != null)
				{
					num6 = gclass.gclass21_1.RaceID;
				}
				var dataObj = new PopulationData()
				{
					PopulationID = gclass.int_5,
					RaceID = gclass.gclass21_0.RaceID,
					SpeciesID = gclass.gclass172_0.int_0,
					PopName = gclass.PopName,
					AcademyOfficers = gclass.decimal_7,
					Capital = gclass.bool_1,
					TerraformStatus = gclass.genum34_0,
					PurchaseCivilianMinerals = gclass.bool_2,
					ColonistDestination = gclass.genum18_0,
					Efficiency = gclass.decimal_5,
					FighterDestFleetID = num,
					SpaceStationDestFleetID = num2,
					FuelProdStatus = gclass.bool_3,
					FuelStockpile = gclass.decimal_3,
					GenModSpeciesID = num3,
					GroundAttackID = gclass.int_7,
					LastColonyCost = gclass.decimal_1,
					MaintenanceStockpile = gclass.decimal_0,
					MaintProdStatus = gclass.bool_4,
					MassDriverDest = num4,
					MaxAtm = gclass.double_0,
					NoStatusChange = gclass.int_1,
					PoliticalStatus = gclass.gclass143_0.genum30_0,
					Population = gclass.decimal_4,
					PreviousUnrest = gclass.decimal_9,
					ProvideColonists = gclass.int_2,
					ReqInf = gclass.int_3,
					StatusPoints = gclass.decimal_6,
					SystemID = gclass.gclass180_0.gclass178_0.int_0,
					SystemBodyID = gclass.gclass1_0.int_0,
					TempMF = gclass.int_4,
					TerraformingGasID = genum,
					UnrestPoints = gclass.decimal_8,
					GroundGeoSurvey = gclass.decimal_2,
					DestroyedInstallationSize = gclass.int_8,
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
					LastDuranium = gclass.gclass114_1.decimal_0,
					LastNeutronium = gclass.gclass114_1.decimal_1,
					LastCorbomite = gclass.gclass114_1.decimal_2,
					LastTritanium = gclass.gclass114_1.decimal_3,
					LastBoronide = gclass.gclass114_1.decimal_4,
					LastMercassium = gclass.gclass114_1.decimal_5,
					LastVendarite = gclass.gclass114_1.decimal_6,
					LastSorium = gclass.gclass114_1.decimal_7,
					LastUridium = gclass.gclass114_1.decimal_8,
					LastCorundium = gclass.gclass114_1.decimal_9,
					LastGallicite = gclass.gclass114_1.decimal_10,
					ReserveDuranium = gclass.gclass114_2.decimal_0,
					ReserveNeutronium = gclass.gclass114_2.decimal_1,
					ReserveCorbomite = gclass.gclass114_2.decimal_2,
					ReserveTritanium = gclass.gclass114_2.decimal_3,
					ReserveBoronide = gclass.gclass114_2.decimal_4,
					ReserveMercassium = gclass.gclass114_2.decimal_5,
					ReserveVendarite = gclass.gclass114_2.decimal_6,
					ReserveSorium = gclass.gclass114_2.decimal_7,
					ReserveUridium = gclass.gclass114_2.decimal_8,
					ReserveCorundium = gclass.gclass114_2.decimal_9,
					ReserveGallicite = gclass.gclass114_2.decimal_10,
					AIValue = num5,
					InvasionStagingPoint = gclass.bool_6,
					OriginalRaceID = num6,
					DoNotDelete = gclass.bool_7,
					MilitaryRestrictedColony = gclass.bool_8,
					AcademyName = gclass.string_1,
					BonusOne = gclass.genum118_0,
					BonusTwo = gclass.genum118_1,
					BonusThree = gclass.genum118_2,
					AutoAssign = gclass.bool_0,
					Importance = gclass.int_0,
					PopulationWeaponStore = PopulationWeaponStore,
					PrisonersStore = PrisonersStore,
					PopComponentStore = PopComponentStore,
					PopInstallationDemandStore = PopInstallationDemandStore
				};
				PopulationStore[i] = dataObj;

				if (gclass.gclass114_3.method_2())
				{
					PopMDChanges dataObj1 = new PopMDChanges()
					{
						PopulationID = gclass.int_5,
						Duranium = gclass.gclass114_3.decimal_0,
						Neutronium = gclass.gclass114_3.decimal_1,
						Corbomite = gclass.gclass114_3.decimal_2,
						Tritanium = gclass.gclass114_3.decimal_3,
						Boronide = gclass.gclass114_3.decimal_4,
						Mercassium = gclass.gclass114_3.decimal_5,
						Vendarite = gclass.gclass114_3.decimal_6,
						Sorium = gclass.gclass114_3.decimal_7,
						Uridium = gclass.gclass114_3.decimal_8,
						Corundium = gclass.gclass114_3.decimal_9,
						Gallicite = gclass.gclass114_3.decimal_10,
					};
					PopMdChangesData.Add(dataObj1);
				}

				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Population WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_PopulationWeapon WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_PopComponent WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_PopMDChanges WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_Prisoners WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_PopInstallationDemand WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in PopulationStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Population ( PopulationID, GameID, RaceID, SpeciesID, PopName, AcademyOfficers, Capital, TerraformStatus, PurchaseCivilianMinerals, ColonistDestination, Efficiency, FighterDestFleetID, SpaceStationDestFleetID, FuelProdStatus, FuelStockpile, GenModSpeciesID, GroundAttackID, LastColonyCost, MaintenanceStockpile, MaintProdStatus, MassDriverDest, MaxAtm,\r\n                        NoStatusChange, PoliticalStatus, Population, PreviousUnrest, ProvideColonists, ReqInf, StatusPoints, SystemID, SystemBodyID, TempMF, TerraformingGasID, UnrestPoints, GroundGeoSurvey, DestroyedInstallationSize, Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite,\r\n                        LastDuranium, LastNeutronium, LastCorbomite, LastTritanium, LastBoronide, LastMercassium, LastVendarite, LastSorium, LastUridium, LastCorundium, LastGallicite, ReserveDuranium, ReserveNeutronium, ReserveCorbomite, ReserveTritanium, ReserveBoronide, ReserveMercassium, ReserveVendarite, ReserveSorium, ReserveUridium, ReserveCorundium, ReserveGallicite, \r\n                        AIValue, InvasionStagingPoint, OriginalRaceID, DoNotDelete, MilitaryRestrictedColony, AcademyName, BonusOne, BonusTwo, BonusThree, AutoAssign, Importance) \r\n                        VALUES ( @PopulationID, @GameID, @RaceID, @SpeciesID, @PopName, @AcademyOfficers, @Capital, @TerraformStatus, @PurchaseCivilianMinerals, @ColonistDestination, @Efficiency, @FighterDestFleetID, @SpaceStationDestFleetID, @FuelProdStatus, @FuelStockpile, @GenModSpeciesID, @GroundAttackID, @LastColonyCost, @MaintenanceStockpile, @MaintProdStatus, @MassDriverDest, @MaxAtm,\r\n                        @NoStatusChange, @PoliticalStatus, @Population, @PreviousUnrest, @ProvideColonists, @ReqInf, @StatusPoints, @SystemID, @SystemBodyID, @TempMF, @TerraformingGasID, @UnrestPoints, @GroundGeoSurvey, @DestroyedInstallationSize, @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite,\r\n                        @LastDuranium, @LastNeutronium, @LastCorbomite, @LastTritanium, @LastBoronide, @LastMercassium, @LastVendarite, @LastSorium, @LastUridium, @LastCorundium, @LastGallicite, @ReserveDuranium, @ReserveNeutronium, @ReserveCorbomite, @ReserveTritanium, @ReserveBoronide, @ReserveMercassium, @ReserveVendarite, @ReserveSorium, @ReserveUridium, @ReserveCorundium, @ReserveGallicite, \r\n                        @AIValue, @InvasionStagingPoint, @OriginalRaceID, @DoNotDelete, @MilitaryRestrictedColony, @AcademyName, @BonusOne, @BonusTwo, @BonusThree, @AutoAssign, @Importance)";
						sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@PopName", dataObj.PopName);
						sqliteCommand.Parameters.AddWithValue("@AcademyOfficers", dataObj.AcademyOfficers);
						sqliteCommand.Parameters.AddWithValue("@Capital", dataObj.Capital);
						sqliteCommand.Parameters.AddWithValue("@TerraformStatus", dataObj.TerraformStatus);
						sqliteCommand.Parameters.AddWithValue("@PurchaseCivilianMinerals",
							dataObj.PurchaseCivilianMinerals);
						sqliteCommand.Parameters.AddWithValue("@ColonistDestination", dataObj.ColonistDestination);
						sqliteCommand.Parameters.AddWithValue("@Efficiency", dataObj.Efficiency);
						sqliteCommand.Parameters.AddWithValue("@FighterDestFleetID", dataObj.FighterDestFleetID);
						sqliteCommand.Parameters.AddWithValue("@SpaceStationDestFleetID",
							dataObj.SpaceStationDestFleetID);
						sqliteCommand.Parameters.AddWithValue("@FuelProdStatus", dataObj.FuelProdStatus);
						sqliteCommand.Parameters.AddWithValue("@FuelStockpile", dataObj.FuelStockpile);
						sqliteCommand.Parameters.AddWithValue("@GenModSpeciesID", dataObj.GenModSpeciesID);
						sqliteCommand.Parameters.AddWithValue("@GroundAttackID", dataObj.GroundAttackID);
						sqliteCommand.Parameters.AddWithValue("@LastColonyCost", dataObj.LastColonyCost);
						sqliteCommand.Parameters.AddWithValue("@MaintenanceStockpile", dataObj.MaintenanceStockpile);
						sqliteCommand.Parameters.AddWithValue("@MaintProdStatus", dataObj.MaintProdStatus);
						sqliteCommand.Parameters.AddWithValue("@MassDriverDest", dataObj.MassDriverDest);
						sqliteCommand.Parameters.AddWithValue("@MaxAtm", dataObj.MaxAtm);
						sqliteCommand.Parameters.AddWithValue("@NoStatusChange", dataObj.NoStatusChange);
						sqliteCommand.Parameters.AddWithValue("@PoliticalStatus", dataObj.PoliticalStatus);
						sqliteCommand.Parameters.AddWithValue("@Population", dataObj.Population);
						sqliteCommand.Parameters.AddWithValue("@PreviousUnrest", dataObj.PreviousUnrest);
						sqliteCommand.Parameters.AddWithValue("@ProvideColonists", dataObj.ProvideColonists);
						sqliteCommand.Parameters.AddWithValue("@ReqInf", dataObj.ReqInf);
						sqliteCommand.Parameters.AddWithValue("@StatusPoints", dataObj.StatusPoints);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", dataObj.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@TempMF", dataObj.TempMF);
						sqliteCommand.Parameters.AddWithValue("@TerraformingGasID", dataObj.TerraformingGasID);
						sqliteCommand.Parameters.AddWithValue("@UnrestPoints", dataObj.UnrestPoints);
						sqliteCommand.Parameters.AddWithValue("@GroundGeoSurvey", dataObj.GroundGeoSurvey);
						sqliteCommand.Parameters.AddWithValue("@DestroyedInstallationSize",
							dataObj.DestroyedInstallationSize);
						sqliteCommand.Parameters.AddWithValue("@Duranium", dataObj.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", dataObj.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", dataObj.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", dataObj.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", dataObj.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", dataObj.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", dataObj.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", dataObj.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", dataObj.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", dataObj.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", dataObj.Gallicite);
						sqliteCommand.Parameters.AddWithValue("@LastDuranium", dataObj.LastDuranium);
						sqliteCommand.Parameters.AddWithValue("@LastNeutronium", dataObj.LastNeutronium);
						sqliteCommand.Parameters.AddWithValue("@LastCorbomite", dataObj.LastCorbomite);
						sqliteCommand.Parameters.AddWithValue("@LastTritanium", dataObj.LastTritanium);
						sqliteCommand.Parameters.AddWithValue("@LastBoronide", dataObj.LastBoronide);
						sqliteCommand.Parameters.AddWithValue("@LastMercassium", dataObj.LastMercassium);
						sqliteCommand.Parameters.AddWithValue("@LastVendarite", dataObj.LastVendarite);
						sqliteCommand.Parameters.AddWithValue("@LastSorium", dataObj.LastSorium);
						sqliteCommand.Parameters.AddWithValue("@LastUridium", dataObj.LastUridium);
						sqliteCommand.Parameters.AddWithValue("@LastCorundium", dataObj.LastCorundium);
						sqliteCommand.Parameters.AddWithValue("@LastGallicite", dataObj.LastGallicite);
						sqliteCommand.Parameters.AddWithValue("@ReserveDuranium", dataObj.ReserveDuranium);
						sqliteCommand.Parameters.AddWithValue("@ReserveNeutronium", dataObj.ReserveNeutronium);
						sqliteCommand.Parameters.AddWithValue("@ReserveCorbomite", dataObj.ReserveCorbomite);
						sqliteCommand.Parameters.AddWithValue("@ReserveTritanium", dataObj.ReserveTritanium);
						sqliteCommand.Parameters.AddWithValue("@ReserveBoronide", dataObj.ReserveBoronide);
						sqliteCommand.Parameters.AddWithValue("@ReserveMercassium", dataObj.ReserveMercassium);
						sqliteCommand.Parameters.AddWithValue("@ReserveVendarite", dataObj.ReserveVendarite);
						sqliteCommand.Parameters.AddWithValue("@ReserveSorium", dataObj.ReserveSorium);
						sqliteCommand.Parameters.AddWithValue("@ReserveUridium", dataObj.ReserveUridium);
						sqliteCommand.Parameters.AddWithValue("@ReserveCorundium", dataObj.ReserveCorundium);
						sqliteCommand.Parameters.AddWithValue("@ReserveGallicite", dataObj.ReserveGallicite);
						sqliteCommand.Parameters.AddWithValue("@AIValue", dataObj.AIValue);
						sqliteCommand.Parameters.AddWithValue("@InvasionStagingPoint", dataObj.InvasionStagingPoint);
						sqliteCommand.Parameters.AddWithValue("@OriginalRaceID", dataObj.OriginalRaceID);
						sqliteCommand.Parameters.AddWithValue("@DoNotDelete", dataObj.DoNotDelete);
						sqliteCommand.Parameters.AddWithValue("@MilitaryRestrictedColony",
							dataObj.MilitaryRestrictedColony);
						sqliteCommand.Parameters.AddWithValue("@AcademyName", dataObj.AcademyName);
						sqliteCommand.Parameters.AddWithValue("@BonusOne", dataObj.BonusOne);
						sqliteCommand.Parameters.AddWithValue("@BonusTwo", dataObj.BonusTwo);
						sqliteCommand.Parameters.AddWithValue("@BonusThree", dataObj.BonusThree);
						sqliteCommand.Parameters.AddWithValue("@AutoAssign", dataObj.AutoAssign);
						sqliteCommand.Parameters.AddWithValue("@Importance", dataObj.Importance);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.PopulationWeaponStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_PopulationWeapon (GameID, PopulationID, MissileID, Amount ) VALUES ( @GameID, @PopulationID, @MissileID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj1.PopulationID);
							sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj1.MissileID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.PrisonersStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_Prisoners (GameID, NumPrisoners, PopulationID, PrisonerRaceID, PrisonerSpeciesID ) VALUES ( @GameID, @NumPrisoners, @PopulationID, @PrisonerRaceID, @PrisonerSpeciesID )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@NumPrisoners", dataObj1.NumPrisoners);
							sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj1.PopulationID);
							sqliteCommand.Parameters.AddWithValue("@PrisonerRaceID", dataObj1.PrisonerRaceID);
							sqliteCommand.Parameters.AddWithValue("@PrisonerSpeciesID", dataObj1.PrisonerSpeciesID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.PopComponentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_PopComponent (ComponentID, PopulationID, GameID, Amount ) VALUES ( @ComponentID, @PopulationID, @GameID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@ComponentID", dataObj1.ComponentID);
							sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj1.PopulationID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.PopInstallationDemandStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_PopInstallationDemand (PopulationID, InstallationID, Amount, GameID, Export, NonEssential ) VALUES ( @PopulationID, @InstallationID, @Amount, @GameID, @Export, @NonEssential )";
							sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj1.PopulationID);
							sqliteCommand.Parameters.AddWithValue("@InstallationID", dataObj1.InstallationID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@Export", dataObj1.Export);
							sqliteCommand.Parameters.AddWithValue("@NonEssential", dataObj1.NonEssential);
							sqliteCommand.ExecuteNonQuery();
						}
					}
					foreach (var dataObj in PopMdChangesData)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_PopMDChanges (PopulationID, GameID, Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite ) VALUES ( @PopulationID, @GameID, @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite )";
						sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@Duranium", dataObj.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", dataObj.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", dataObj.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", dataObj.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", dataObj.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", dataObj.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", dataObj.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", dataObj.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", dataObj.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", dataObj.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", dataObj.Gallicite);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1463);
			}
		}
	}
	
	/// <summary>
	/// method_88
	/// </summary>
	public class SaveMineralDeposit
	{
		public struct MineralDepositData
		{
			public AuroraElement MaterialID;
			public int SystemID;
			public int SystemBodyID;
			public decimal Amount;
			public decimal Accessibility;
			public decimal HalfOriginalAmount;
			public decimal OriginalAcc;
		}
		MineralDepositData[] MineralDepositDataStore;


		public SaveMineralDeposit(GClass0 game)
		{
			
			int i = 0;
			var list = game.dictionary_11.Values.SelectMany<GClass1, GClass115>((Func<GClass1, IEnumerable<GClass115>>)(x => (IEnumerable<GClass115>) x.dictionary_0.Values)).ToList<GClass115>();
			MineralDepositDataStore = new MineralDepositData[list.Count()];
			foreach(GClass115 gclass in list)
			{
				var dataObj = new MineralDepositData()
				{
					MaterialID = gclass.auroraElement_0,
					SystemID = game.dictionary_11[gclass.int_0].int_1,
					SystemBodyID = gclass.int_0,
					Amount = gclass.decimal_0,
					Accessibility = gclass.decimal_1,
					HalfOriginalAmount = gclass.decimal_2,
					OriginalAcc = gclass.decimal_3,
				};
				MineralDepositDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MineralDeposit WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var MineralDepositData in MineralDepositDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_MineralDeposit ( GameID, MaterialID, SystemID, SystemBodyID, Amount, Accessibility, HalfOriginalAmount, OriginalAcc ) VALUES ( @GameID, @MaterialID, @SystemID, @SystemBodyID, @Amount, @Accessibility, @HalfOriginalAmount, @OriginalAcc )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@MaterialID", MineralDepositData.MaterialID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", MineralDepositData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", MineralDepositData.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@Amount", MineralDepositData.Amount);
						sqliteCommand.Parameters.AddWithValue("@Accessibility", MineralDepositData.Accessibility);
						sqliteCommand.Parameters.AddWithValue("@HalfOriginalAmount", MineralDepositData.HalfOriginalAmount);
						sqliteCommand.Parameters.AddWithValue("@OriginalAcc", MineralDepositData.OriginalAcc);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1464);
			}
		}
	}
	
	/// <summary>
	/// method_89
	/// </summary>
	public class SaveShipClass
	{
		public struct ShipClassData
		{
			public int ShipClassID;
			public int ClassShippingLineID;
			public GEnum117 AutomatedDesignID;
			public string ClassName;
			public int RaceID;
			public int ActiveSensorStrength;
			public int ArmourThickness;
			public int ArmourWidth;
			public decimal BaseFailureChance;
			public int CargoCapacity;
			public decimal ClassCrossSection;
			public decimal ClassThermal;
			public int Collier;
			public int SeniorCO;
			public int ColonistCapacity;
			public int CommanderPriority;
			public int CommercialJumpDrive;
			public bool MilitaryEngines;
			public int ControlRating;
			public int ConscriptOnly;
			public decimal Cost;
			public int Crew;
			public decimal CrewQuartersHS;
			public int TroopTransportType;
			public int DCRating;
			public int ECM;
			public int ELINTRating;
			public int DiplomacyRating;
			public int EMSensorStrength;
			public decimal EnginePower;
			public int ESMaxDACRoll;
			public bool FighterClass;
			public bool Commercial;
			public int FuelCapacity;
			public decimal FuelEfficiency;
			public int FuelTanker;
			public int GeoSurvey;
			public int GravSurvey;
			public int Harvesters;
			public int HullDescriptionID;
			public int JGConstructionTime;
			public int JumpDistance;
			public int JumpRating;
			public bool Locked;
			public decimal MagazineCapacity;
			public int MaxChance;
			public int MaxDACRoll;
			public int MaxSpeed;
			public int MaintModules;
			public int MinimumFuel;
			public int MinimumSupplies;
			public int MiningModules;
			public int NameThemeID;
			public int NoArmour;
			public string Notes;
			public GEnum104 MainFunction;
			public int Obsolete;
			public int OtherRaceClassID;
			public decimal ParasiteCapacity;
			public int PassiveSensorStrength;
			public decimal PlannedDeployment;
			public bool PreTNT;
			public decimal ProtectionValue;
			public int RankRequired;
			public decimal ReactorPower;
			public bool RecreationalModule;
			public bool MoraleCheckRequired;
			public int RefuelPriority;
			public int ResupplyPriority;
			public int RefuellingRate;
			public int RefuellingHub;
			public decimal RequiredPower;
			public int SalvageRate;
			public decimal SensorReduction;
			public int ShieldStrength;
			public decimal Size;
			public int MaintSupplies;
			public int STSTractor;
			public int SupplyShip;
			public int Terraformers;
			public int TotalNumber;
			public int CargoShuttleStrength;
			public int TroopCapacity;
			public int WorkerCapacity;
			public int MaintPriority;
			public bool CommercialHangar;
			public int OrdnanceTransferRate;
			public int OrdnanceTransferHub;
			public int BioEnergyCapacity;
			public string PrefixName;
			public string SuffixName;
			public int RandomShipNameFromTheme;
			public ClassMaterialsData[] ClassMaterialsStore;
			public ClassOrdnanceTemplateData[] ClassOrdnanceTemplateStore;
			public ClassComponentData[] ClassComponentStore;
			public ClassSCData[] ClassSCStore;
		}
		ShipClassData[] ShipClassStore;

		public struct ClassMaterialsData
		{
			public int ClassID;
			public AuroraElement MaterialID;
			public decimal Amount;
		}

		public struct ClassOrdnanceTemplateData
		{
			public int ShipClassID;
			public int MissileID;
			public int Amount;
		}
		
		public struct ClassComponentData
		{
			public int ShipClassID;
			public int ComponentID;
			public decimal NumComponent;
			public int ChanceToHit;
			public int ElectronicCTH;
		}
		
		public struct ClassSCData
		{
			public int ShipClassID;
			public int FighterClassID;
			public int Number;
		}
		
		
		public SaveShipClass(GClass0 game)
		{
			int i = 0;
			ShipClassStore = new ShipClassData[game.dictionary_3.Count];
			foreach (GClass22 gclass in game.dictionary_3.Values)
			{
				var list = gclass.gclass114_0.method_4();
				ClassMaterialsData[] ClassMaterialsStore = new ClassMaterialsData[list.Count];
				int j = 0;
				foreach (GClass116 gclass2 in list)
				{
					var dataObj1 = new ClassMaterialsData()
					{
						ClassID = gclass.int_0,
						MaterialID = gclass2.auroraElement_0,
						Amount = gclass2.decimal_0,
					};
					ClassMaterialsStore[j] = dataObj1;
					j++;
				}
				
				ClassOrdnanceTemplateData[] ClassOrdnanceTemplateStore = new ClassOrdnanceTemplateData[gclass.list_0.Count];
				j = 0;
				foreach (GClass121 gclass3 in gclass.list_0)
				{
					var dataObj1 = new ClassOrdnanceTemplateData()
					{
						ShipClassID = gclass.int_0,
						MissileID = gclass3.gclass120_0.int_0,
						Amount = gclass3.int_0,
					};
					ClassOrdnanceTemplateStore[j] = dataObj1;
					j++;
				}
				
				ClassComponentData[] ClassComponentStore = new ClassComponentData[gclass.dictionary_0.Count];
				j = 0;
				foreach (GClass204 gclass4 in gclass.dictionary_0.Values)
				{
					var dataObj1 = new ClassComponentData()
					{
						ShipClassID = gclass.int_0,
						ComponentID = gclass4.gclass206_0.int_0,
						NumComponent = gclass4.decimal_0,
						ChanceToHit = gclass4.int_2,
						ElectronicCTH = gclass4.int_3,
					};
					ClassComponentStore[j] = dataObj1;
					j++;
				}
				
				ClassSCData[] ClassSCStore = new ClassSCData[gclass.list_1.Count];
				j = 0;
				foreach (GClass69 gclass5 in gclass.list_1)
				{
					var dataObj1 = new ClassSCData()
					{
						ShipClassID = gclass.int_0,
						FighterClassID = gclass5.gclass22_0.int_0,
						Number = gclass5.int_0,
					};
					ClassSCStore[j] = dataObj1;
					j++;
				}
				
				int num = 0;
				int num2 = 0;
				GEnum117 genum = GEnum117.const_0;
				int num3 = 0;
				if (gclass.gclass165_0 != null)
				{
					num = gclass.gclass165_0.int_0;
				}
				if (gclass.gclass146_0 != null)
				{
					num2 = (int)gclass.gclass146_0.genum119_0;
				}
				if (gclass.gclass13_0 != null)
				{
					genum = gclass.gclass13_0.genum117_0;
				}
				if (gclass.gclass128_0 != null)
				{
					num3 = gclass.gclass128_0.int_0;
				}
				int num4;
				if (gclass.gclass60_0 == null)
				{
					num4 = gclass.int_17;
				}
				else
				{
					num4 = gclass.gclass21_0.method_223(gclass.gclass60_0, AuroraCommanderType.Naval);
				}
				var dataObj = new ShipClassData()
				{
					ShipClassID = gclass.int_0,
					ClassShippingLineID = num,
					AutomatedDesignID = genum,
					ClassName = gclass.ClassName,
					RaceID = gclass.gclass21_0.RaceID,
					ActiveSensorStrength = gclass.int_1,
					ArmourThickness = gclass.int_2,
					ArmourWidth = gclass.int_3,
					BaseFailureChance = gclass.decimal_1,
					CargoCapacity = gclass.int_61,
					ClassCrossSection = gclass.decimal_2,
					ClassThermal = gclass.decimal_3,
					Collier = gclass.int_5,
					SeniorCO = gclass.int_6,
					ColonistCapacity = gclass.int_7,
					CommanderPriority = gclass.int_8,
					CommercialJumpDrive = gclass.int_14,
					MilitaryEngines = gclass.bool_8,
					ControlRating = gclass.int_9,
					ConscriptOnly = gclass.int_10,
					Cost = gclass.decimal_4,
					Crew = gclass.int_11,
					CrewQuartersHS = gclass.decimal_5,
					TroopTransportType = num2,
					DCRating = gclass.int_15,
					ECM = gclass.int_16,
					ELINTRating = gclass.int_12,
					DiplomacyRating = gclass.int_13,
					EMSensorStrength = gclass.int_18,
					EnginePower = gclass.decimal_0,
					ESMaxDACRoll = gclass.int_19,
					FighterClass = gclass.bool_1,
					Commercial = gclass.bool_2,
					FuelCapacity = gclass.int_62,
					FuelEfficiency = gclass.decimal_6,
					FuelTanker = gclass.int_20,
					GeoSurvey = gclass.int_21,
					GravSurvey = gclass.int_22,
					Harvesters = gclass.int_23,
					HullDescriptionID = gclass.gclass70_0.int_0,
					JGConstructionTime = gclass.int_25,
					JumpDistance = gclass.int_26,
					JumpRating = gclass.int_27,
					Locked = gclass.bool_3,
					MagazineCapacity = gclass.decimal_11,
					MaxChance = gclass.int_28,
					MaxDACRoll = gclass.int_29,
					MaxSpeed = gclass.int_30,
					MaintModules = gclass.int_31,
					MinimumFuel = gclass.int_44,
					MinimumSupplies = gclass.int_45,
					MiningModules = gclass.int_34,
					NameThemeID = num3,
					NoArmour = gclass.int_35,
					Notes = gclass.string_1,
					MainFunction = gclass.genum104_0,
					Obsolete = gclass.int_36,
					OtherRaceClassID = gclass.int_37,
					ParasiteCapacity = gclass.decimal_8,
					PassiveSensorStrength = gclass.int_38,
					PlannedDeployment = gclass.decimal_9,
					PreTNT = gclass.bool_4,
					ProtectionValue = gclass.decimal_10,
					RankRequired = num4,
					ReactorPower = gclass.decimal_14,
					RecreationalModule = gclass.bool_5,
					MoraleCheckRequired = gclass.bool_7,
					RefuelPriority = gclass.int_42,
					ResupplyPriority = gclass.int_43,
					RefuellingRate = gclass.int_41,
					RefuellingHub = gclass.int_52,
					RequiredPower = gclass.decimal_15,
					SalvageRate = gclass.int_39,
					SensorReduction = gclass.decimal_12,
					ShieldStrength = gclass.int_40,
					Size = gclass.decimal_13,
					MaintSupplies = gclass.int_48,
					STSTractor = gclass.int_49,
					SupplyShip = gclass.int_50,
					Terraformers = gclass.int_51,
					TotalNumber = gclass.int_54,
					CargoShuttleStrength = gclass.int_55,
					TroopCapacity = gclass.int_56,
					WorkerCapacity = gclass.int_57,
					MaintPriority = gclass.int_32,
					CommercialHangar = gclass.bool_0,
					OrdnanceTransferRate = gclass.int_46,
					OrdnanceTransferHub = gclass.int_53,
					BioEnergyCapacity = gclass.int_63,
					PrefixName = gclass.string_3,
					SuffixName = gclass.string_4,
					RandomShipNameFromTheme = gclass.int_33,
					ClassMaterialsStore = ClassMaterialsStore,
					ClassOrdnanceTemplateStore = ClassOrdnanceTemplateStore,
					ClassComponentStore = ClassComponentStore,
					ClassSCStore = ClassSCStore,
				};
				ShipClassStore[i] = dataObj;
				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShipClass WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ClassMaterials WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ClassOrdnanceTemplate WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ClassComponent WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ClassSC WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in ShipClassStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_ShipClass (ShipClassID, ClassShippingLineID, AutomatedDesignID, ClassName, GameID, RaceID, ActiveSensorStrength, ArmourThickness, ArmourWidth, BaseFailureChance, CargoCapacity, ClassCrossSection, ClassThermal, Collier, ColonistCapacity, CommanderPriority, CommercialJumpDrive, MilitaryEngines, ControlRating, ConscriptOnly, \r\n                        Cost, Crew, CrewQuartersHS, TroopTransportType, DCRating, ECM, ELINTRating, DiplomacyRating, BioEnergyCapacity,\r\n                        EMSensorStrength, EnginePower, ESMaxDACRoll, FighterClass, Commercial, FuelCapacity, FuelEfficiency, FuelTanker, GeoSurvey, GravSurvey, Harvesters, HullDescriptionID, JGConstructionTime, JumpDistance, JumpRating, Locked, MagazineCapacity, MaxChance, MaxDACRoll, MaxSpeed, MaintModules, MinimumFuel, MinimumSupplies, MiningModules, NameThemeID, NoArmour, \r\n                        Notes, MainFunction, Obsolete, OtherRaceClassID, ParasiteCapacity, PassiveSensorStrength, PlannedDeployment, PreTNT, ProtectionValue, RankRequired, ReactorPower, RecreationalModule, MoraleCheckRequired, RefuelPriority, ResupplyPriority, RefuellingRate, RefuellingHub, RequiredPower, SalvageRate, SensorReduction, ShieldStrength, Size, MaintSupplies, STSTractor, \r\n                        SupplyShip, Terraformers, TotalNumber, CargoShuttleStrength, TroopCapacity, WorkerCapacity, MaintPriority, CommercialHangar, OrdnanceTransferRate, OrdnanceTransferHub, SeniorCO, PrefixName, SuffixName, RandomShipNameFromTheme) \r\n                        VALUES ( @ShipClassID, @ClassShippingLineID, @AutomatedDesignID, @ClassName, @GameID, @RaceID, @ActiveSensorStrength, @ArmourThickness, @ArmourWidth, @BaseFailureChance, @CargoCapacity, @ClassCrossSection, @ClassThermal, @Collier, @ColonistCapacity, @CommanderPriority, @CommercialJumpDrive, @MilitaryEngines, @ControlRating, @ConscriptOnly, \r\n                        @Cost, @Crew, @CrewQuartersHS, @TroopTransportType, @DCRating, @ECM, @ELINTRating, @DiplomacyRating, @BioEnergyCapacity,\r\n                        @EMSensorStrength, @EnginePower, @ESMaxDACRoll, @FighterClass, @Commercial, @FuelCapacity, @FuelEfficiency, @FuelTanker, @GeoSurvey, @GravSurvey, @Harvesters, @HullDescriptionID, @JGConstructionTime, @JumpDistance, @JumpRating, @Locked, @MagazineCapacity, @MaxChance, @MaxDACRoll, @MaxSpeed, @MaintModules, @MinimumFuel, @MinimumSupplies, @MiningModules, @NameThemeID, @NoArmour,\r\n                        @Notes, @MainFunction, @Obsolete, @OtherRaceClassID, @ParasiteCapacity, @PassiveSensorStrength, @PlannedDeployment, @PreTNT, @ProtectionValue, @RankRequired, @ReactorPower, @RecreationalModule, @MoraleCheckRequired, @RefuelPriority, @ResupplyPriority, @RefuellingRate, @RefuellingHub, @RequiredPower, @SalvageRate, @SensorReduction, @ShieldStrength, @Size, @MaintSupplies, @STSTractor, \r\n                        @SupplyShip, @Terraformers, @TotalNumber, @CargoShuttleStrength, @TroopCapacity, @WorkerCapacity, @MaintPriority, @CommercialHangar, @OrdnanceTransferRate, @OrdnanceTransferHub, @SeniorCO, @PrefixName, @SuffixName, @RandomShipNameFromTheme)";
						sqliteCommand.Parameters.AddWithValue("@ShipClassID", dataObj.ShipClassID);
						sqliteCommand.Parameters.AddWithValue("@ClassShippingLineID", dataObj.ClassShippingLineID);
						sqliteCommand.Parameters.AddWithValue("@AutomatedDesignID", dataObj.AutomatedDesignID);
						sqliteCommand.Parameters.AddWithValue("@ClassName", dataObj.ClassName);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@ActiveSensorStrength", dataObj.ActiveSensorStrength);
						sqliteCommand.Parameters.AddWithValue("@ArmourThickness", dataObj.ArmourThickness);
						sqliteCommand.Parameters.AddWithValue("@ArmourWidth", dataObj.ArmourWidth);
						sqliteCommand.Parameters.AddWithValue("@BaseFailureChance", dataObj.BaseFailureChance);
						sqliteCommand.Parameters.AddWithValue("@CargoCapacity", dataObj.CargoCapacity);
						sqliteCommand.Parameters.AddWithValue("@ClassCrossSection", dataObj.ClassCrossSection);
						sqliteCommand.Parameters.AddWithValue("@ClassThermal", dataObj.ClassThermal);
						sqliteCommand.Parameters.AddWithValue("@Collier", dataObj.Collier);
						sqliteCommand.Parameters.AddWithValue("@SeniorCO", dataObj.SeniorCO);
						sqliteCommand.Parameters.AddWithValue("@ColonistCapacity", dataObj.ColonistCapacity);
						sqliteCommand.Parameters.AddWithValue("@CommanderPriority", dataObj.CommanderPriority);
						sqliteCommand.Parameters.AddWithValue("@CommercialJumpDrive", dataObj.CommercialJumpDrive);
						sqliteCommand.Parameters.AddWithValue("@MilitaryEngines", dataObj.MilitaryEngines);
						sqliteCommand.Parameters.AddWithValue("@ControlRating", dataObj.ControlRating);
						sqliteCommand.Parameters.AddWithValue("@ConscriptOnly", dataObj.ConscriptOnly);
						sqliteCommand.Parameters.AddWithValue("@Cost", dataObj.Cost);
						sqliteCommand.Parameters.AddWithValue("@Crew", dataObj.Crew);
						sqliteCommand.Parameters.AddWithValue("@CrewQuartersHS", dataObj.CrewQuartersHS);
						sqliteCommand.Parameters.AddWithValue("@TroopTransportType", dataObj.TroopTransportType);
						sqliteCommand.Parameters.AddWithValue("@DCRating", dataObj.DCRating);
						sqliteCommand.Parameters.AddWithValue("@ECM", dataObj.ECM);
						sqliteCommand.Parameters.AddWithValue("@ELINTRating", dataObj.ELINTRating);
						sqliteCommand.Parameters.AddWithValue("@DiplomacyRating", dataObj.DiplomacyRating);
						sqliteCommand.Parameters.AddWithValue("@EMSensorStrength", dataObj.EMSensorStrength);
						sqliteCommand.Parameters.AddWithValue("@EnginePower", dataObj.EnginePower);
						sqliteCommand.Parameters.AddWithValue("@ESMaxDACRoll", dataObj.ESMaxDACRoll);
						sqliteCommand.Parameters.AddWithValue("@FighterClass", dataObj.FighterClass);
						sqliteCommand.Parameters.AddWithValue("@Commercial", dataObj.Commercial);
						sqliteCommand.Parameters.AddWithValue("@FuelCapacity", dataObj.FuelCapacity);
						sqliteCommand.Parameters.AddWithValue("@FuelEfficiency", dataObj.FuelEfficiency);
						sqliteCommand.Parameters.AddWithValue("@FuelTanker", dataObj.FuelTanker);
						sqliteCommand.Parameters.AddWithValue("@GeoSurvey", dataObj.GeoSurvey);
						sqliteCommand.Parameters.AddWithValue("@GravSurvey", dataObj.GravSurvey);
						sqliteCommand.Parameters.AddWithValue("@Harvesters", dataObj.Harvesters);
						sqliteCommand.Parameters.AddWithValue("@HullDescriptionID", dataObj.HullDescriptionID);
						sqliteCommand.Parameters.AddWithValue("@JGConstructionTime", dataObj.JGConstructionTime);
						sqliteCommand.Parameters.AddWithValue("@JumpDistance", dataObj.JumpDistance);
						sqliteCommand.Parameters.AddWithValue("@JumpRating", dataObj.JumpRating);
						sqliteCommand.Parameters.AddWithValue("@Locked", dataObj.Locked);
						sqliteCommand.Parameters.AddWithValue("@MagazineCapacity", dataObj.MagazineCapacity);
						sqliteCommand.Parameters.AddWithValue("@MaxChance", dataObj.MaxChance);
						sqliteCommand.Parameters.AddWithValue("@MaxDACRoll", dataObj.MaxDACRoll);
						sqliteCommand.Parameters.AddWithValue("@MaxSpeed", dataObj.MaxSpeed);
						sqliteCommand.Parameters.AddWithValue("@MaintModules", dataObj.MaintModules);
						sqliteCommand.Parameters.AddWithValue("@MinimumFuel", dataObj.MinimumFuel);
						sqliteCommand.Parameters.AddWithValue("@MinimumSupplies", dataObj.MinimumSupplies);
						sqliteCommand.Parameters.AddWithValue("@MiningModules", dataObj.MiningModules);
						sqliteCommand.Parameters.AddWithValue("@NameThemeID", dataObj.NameThemeID);
						sqliteCommand.Parameters.AddWithValue("@NoArmour", dataObj.NoArmour);
						sqliteCommand.Parameters.AddWithValue("@Notes", dataObj.Notes);
						sqliteCommand.Parameters.AddWithValue("@MainFunction", dataObj.MainFunction);
						sqliteCommand.Parameters.AddWithValue("@Obsolete", dataObj.Obsolete);
						sqliteCommand.Parameters.AddWithValue("@OtherRaceClassID", dataObj.OtherRaceClassID);
						sqliteCommand.Parameters.AddWithValue("@ParasiteCapacity", dataObj.ParasiteCapacity);
						sqliteCommand.Parameters.AddWithValue("@PassiveSensorStrength", dataObj.PassiveSensorStrength);
						sqliteCommand.Parameters.AddWithValue("@PlannedDeployment", dataObj.PlannedDeployment);
						sqliteCommand.Parameters.AddWithValue("@PreTNT", dataObj.PreTNT);
						sqliteCommand.Parameters.AddWithValue("@ProtectionValue", dataObj.ProtectionValue);
						sqliteCommand.Parameters.AddWithValue("@RankRequired", dataObj.RankRequired);
						sqliteCommand.Parameters.AddWithValue("@ReactorPower", dataObj.ReactorPower);
						sqliteCommand.Parameters.AddWithValue("@RecreationalModule", dataObj.RecreationalModule);
						sqliteCommand.Parameters.AddWithValue("@MoraleCheckRequired", dataObj.MoraleCheckRequired);
						sqliteCommand.Parameters.AddWithValue("@RefuelPriority", dataObj.RefuelPriority);
						sqliteCommand.Parameters.AddWithValue("@ResupplyPriority", dataObj.ResupplyPriority);
						sqliteCommand.Parameters.AddWithValue("@RefuellingRate", dataObj.RefuellingRate);
						sqliteCommand.Parameters.AddWithValue("@RefuellingHub", dataObj.RefuellingHub);
						sqliteCommand.Parameters.AddWithValue("@RequiredPower", dataObj.RequiredPower);
						sqliteCommand.Parameters.AddWithValue("@SalvageRate", dataObj.SalvageRate);
						sqliteCommand.Parameters.AddWithValue("@SensorReduction", dataObj.SensorReduction);
						sqliteCommand.Parameters.AddWithValue("@ShieldStrength", dataObj.ShieldStrength);
						sqliteCommand.Parameters.AddWithValue("@Size", dataObj.Size);
						sqliteCommand.Parameters.AddWithValue("@MaintSupplies", dataObj.MaintSupplies);
						sqliteCommand.Parameters.AddWithValue("@STSTractor", dataObj.STSTractor);
						sqliteCommand.Parameters.AddWithValue("@SupplyShip", dataObj.SupplyShip);
						sqliteCommand.Parameters.AddWithValue("@Terraformers", dataObj.Terraformers);
						sqliteCommand.Parameters.AddWithValue("@TotalNumber", dataObj.TotalNumber);
						sqliteCommand.Parameters.AddWithValue("@CargoShuttleStrength", dataObj.CargoShuttleStrength);
						sqliteCommand.Parameters.AddWithValue("@TroopCapacity", dataObj.TroopCapacity);
						sqliteCommand.Parameters.AddWithValue("@WorkerCapacity", dataObj.WorkerCapacity);
						sqliteCommand.Parameters.AddWithValue("@MaintPriority", dataObj.MaintPriority);
						sqliteCommand.Parameters.AddWithValue("@CommercialHangar", dataObj.CommercialHangar);
						sqliteCommand.Parameters.AddWithValue("@OrdnanceTransferRate", dataObj.OrdnanceTransferRate);
						sqliteCommand.Parameters.AddWithValue("@OrdnanceTransferHub", dataObj.OrdnanceTransferHub);
						sqliteCommand.Parameters.AddWithValue("@BioEnergyCapacity", dataObj.BioEnergyCapacity);
						sqliteCommand.Parameters.AddWithValue("@PrefixName", dataObj.PrefixName);
						sqliteCommand.Parameters.AddWithValue("@SuffixName", dataObj.SuffixName);
						sqliteCommand.Parameters.AddWithValue("@RandomShipNameFromTheme",
							dataObj.RandomShipNameFromTheme);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.ClassMaterialsStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ClassMaterials ( GameID, ClassID, MaterialID, Amount ) VALUES ( @GameID, @ClassID, @MaterialID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ClassID", dataObj1.ClassID);
							sqliteCommand.Parameters.AddWithValue("@MaterialID", dataObj1.MaterialID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ClassOrdnanceTemplateStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ClassOrdnanceTemplate (GameID, ShipClassID, MissileID, Amount ) VALUES ( @GameID, @ShipClassID, @MissileID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipClassID", dataObj1.ShipClassID);
							sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj1.MissileID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ClassComponentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ClassComponent (GameID, ClassID, ComponentID, NumComponent, ChanceToHit, ElectronicCTH ) VALUES ( @GameID, @ClassID, @ComponentID, @NumComponent, @ChanceToHit, @ElectronicCTH )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipClassID", dataObj1.ShipClassID);
							sqliteCommand.Parameters.AddWithValue("@ComponentID", dataObj1.ComponentID);
							sqliteCommand.Parameters.AddWithValue("@NumComponent", dataObj1.NumComponent);
							sqliteCommand.Parameters.AddWithValue("@ChanceToHit", dataObj1.ChanceToHit);
							sqliteCommand.Parameters.AddWithValue("@ElectronicCTH", dataObj1.ElectronicCTH);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ClassSCStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ClassSC (GameID, ShipClassID, FighterClassID, Number ) VALUES ( @GameID, @ShipClassID, @FighterClassID, @Number )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipClassID", dataObj1.ShipClassID);
							sqliteCommand.Parameters.AddWithValue("@FighterClassID", dataObj1.FighterClassID);
							sqliteCommand.Parameters.AddWithValue("@Number", dataObj1.Number);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1465);
			}
		}
	}


	/// <summary>
	/// method_90
	/// </summary>
	public class SaveSystemBodyName
	{
		public struct SystemBodyNameData
		{
			public int RaceID;
			public int SystemBodyID;
			public int SystemID;
			public string Name;
		}
		SystemBodyNameData[] SystemBodyNameDataStore;


		public SaveSystemBodyName(GClass0 game)
		{
			
			int i = 0;
			var list = game.dictionary_11.Values.SelectMany<GClass1, GClass194>((Func<GClass1, IEnumerable<GClass194>>)(x => (IEnumerable<GClass194>) x.dictionary_1.Values)).ToList<GClass194>();
			SystemBodyNameDataStore = new SystemBodyNameData[list.Count()];
			foreach(GClass194 gclass in list)
			{
				var dataObj = new SystemBodyNameData()
				{
					RaceID = gclass.gclass21_0.RaceID,
					SystemBodyID = gclass.int_0,
					SystemID = game.dictionary_11[gclass.int_0].int_1,
					Name = gclass.string_0,
				};
				SystemBodyNameDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_SystemBodyName WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var SystemBodyNameData in SystemBodyNameDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_SystemBodyName ( RaceID, SystemBodyID, GameID, SystemID, Name ) VALUES ( @RaceID, @SystemBodyID, @GameID, @SystemID, @Name )";
						sqliteCommand.Parameters.AddWithValue("@RaceID", SystemBodyNameData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", SystemBodyNameData.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", SystemBodyNameData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@Name", SystemBodyNameData.Name);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1466);
			}
		}
	}
	
	/// <summary>
	/// method_91
	/// </summary>
	public class SaveSurvivors
	{
		public struct SurvivorsData
		{
			public int RaceID;
			public int SpeciesID;
			public int ShipID;
			public string SurvivorsShipName;
			public int Crew;
			public int Wounded;
			public decimal RescueTime;
			public int RescueSystemID;
			public decimal GradePoints;
		}
		SurvivorsData[] SurvivorsDataStore;


		public SaveSurvivors(GClass0 game)
		{
			
			int i = 0;
			var list = game.dictionary_4.Values.SelectMany<GClass39, GClass161>((Func<GClass39, IEnumerable<GClass161>>)(x => (IEnumerable<GClass161>) x.list_1)).ToList<GClass161>();
			SurvivorsDataStore = new SurvivorsData[list.Count()];
			foreach(GClass161 gclass in list)
			{
				var dataObj = new SurvivorsData()
				{
					RaceID =  gclass.gclass21_0.RaceID,
					SpeciesID = gclass.gclass172_0.int_0,
					ShipID = gclass.gclass39_0.int_8,
					SurvivorsShipName = gclass.string_0,
					Crew = gclass.int_0,
					Wounded = gclass.int_1,
					RescueTime = gclass.decimal_0,
					RescueSystemID = gclass.gclass180_0.gclass178_0.int_0,
					GradePoints = gclass.decimal_1,
				};
				SurvivorsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Survivors WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var SurvivorsData in SurvivorsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Survivors ( GameID, RaceID, SpeciesID, ShipID, SurvivorsShipName, Crew, Wounded, RescueTime, RescueSystemID, GradePoints ) VALUES ( @GameID, @RaceID, @SpeciesID, @ShipID, @SurvivorsShipName, @Crew, @Wounded, @RescueTime, @RescueSystemID, @GradePoints )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", SurvivorsData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", SurvivorsData.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@ShipID", SurvivorsData.ShipID);
						sqliteCommand.Parameters.AddWithValue("@SurvivorsShipName", SurvivorsData.SurvivorsShipName);
						sqliteCommand.Parameters.AddWithValue("@Crew", SurvivorsData.Crew);
						sqliteCommand.Parameters.AddWithValue("@Wounded", SurvivorsData.Wounded);
						sqliteCommand.Parameters.AddWithValue("@RescueTime", SurvivorsData.RescueTime);
						sqliteCommand.Parameters.AddWithValue("@RescueSystemID", SurvivorsData.RescueSystemID);
						sqliteCommand.Parameters.AddWithValue("@GradePoints", SurvivorsData.GradePoints);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1467);
			}
		}
	}
	
	/// <summary>
	/// method_92
	/// </summary>
	public class SaveAncientConstruct
	{
		public struct AncientConstructData
		{
			public int AncientConstructID;
			public int SystemBodyID;
			public int AncientConstructTypeID;
			public GEnum43 ResearchField;
			public decimal ResearchBonus;
			public bool Active;
		}
		AncientConstructData[] AncientConstructDataStore;


		public SaveAncientConstruct(GClass0 game)
		{
			
			int i = 0;
			AncientConstructDataStore = new AncientConstructData[game.dictionary_25.Count()];
			foreach (GClass196 gclass in game.dictionary_25.Values)
			{
				var dataObj = new AncientConstructData()
				{
					AncientConstructID = gclass.int_0,
					SystemBodyID = gclass.gclass1_0.int_0,
					AncientConstructTypeID = gclass.int_1,
					ResearchField = gclass.gclass145_0.ResearchFieldID,
					ResearchBonus = gclass.decimal_0,
					Active = gclass.bool_0,
				};
				AncientConstructDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AncientConstruct WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AncientConstructData in AncientConstructDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AncientConstruct ( AncientConstructID, GameID, SystemBodyID, AncientConstructTypeID, ResearchField, ResearchBonus, Active ) VALUES ( @AncientConstructID, @GameID, @SystemBodyID, @AncientConstructTypeID, @ResearchField, @ResearchBonus, @Active )";
						sqliteCommand.Parameters.AddWithValue("@AncientConstructID", AncientConstructData.AncientConstructID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", AncientConstructData.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@AncientConstructTypeID", AncientConstructData.AncientConstructTypeID);
						sqliteCommand.Parameters.AddWithValue("@ResearchField", AncientConstructData.ResearchField);
						sqliteCommand.Parameters.AddWithValue("@ResearchBonus", AncientConstructData.ResearchBonus);
						sqliteCommand.Parameters.AddWithValue("@Active", AncientConstructData.Active);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
		{
			GClass202.smethod_68(exception_, 1468);
		}
		}
	}


	/// <summary>
	/// method_93
	/// </summary>
	public class SaveMissileGeoSurvey
	{
		public struct MissileGeoSurveyData
		{
			public int SystemBodyID;
			public int RaceID;
			public decimal SurveyPoints;
		}

		MissileGeoSurveyData[] MissileGeoSurveyDataStore;


		public SaveMissileGeoSurvey(GClass0 game)
		{
			MissileGeoSurveyDataStore = new MissileGeoSurveyData[game.list_7.Count()];
			int i = 0;
			foreach (GClass192 gclass in game.list_7)
			{
				var dataObj = new MissileGeoSurveyData()
				{
					SystemBodyID = gclass.int_0,
					RaceID = gclass.int_1,
					SurveyPoints = gclass.decimal_0,
				};
				MissileGeoSurveyDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MissileGeoSurvey WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var MissileGeoSurveyData in MissileGeoSurveyDataStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_MissileGeoSurvey ( SystemBodyID, RaceID, SurveyPoints, GameID ) VALUES ( @SystemBodyID, @RaceID, @SurveyPoints, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", MissileGeoSurveyData.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", MissileGeoSurveyData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SurveyPoints", MissileGeoSurveyData.SurveyPoints);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1469);
			}
		}
	}

	/// <summary>
	/// method_94
	/// </summary>
	public class SaveRuinRace
	{
		public struct RuinRaceData
		{
			public int RuinRaceID;
			public string Title;
			public string Name;
			public string RacePic;
			public string FlagPic;
			public int Level;
		}
		RuinRaceData[] RuinRaceDataStore;


		public SaveRuinRace(GClass0 game)
		{
			RuinRaceDataStore = new RuinRaceData[game.dictionary_24.Count()];
			int i = 0;
			foreach (GClass154 gclass in game.dictionary_24.Values)
			{
				var dataObj = new RuinRaceData()
				{
					RuinRaceID = gclass.int_0,
					Title = gclass.string_0,
					Name = gclass.string_1,
					RacePic = gclass.string_2,
					FlagPic = gclass.string_3,
					Level = gclass.int_1,
				};
				RuinRaceDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_RuinRace WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var RuinRaceData in RuinRaceDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_RuinRace (RuinRaceID, GameID, Title, Name, RacePic, FlagPic, Level) \r\n                        VALUES ( @RuinRaceID, @GameID, @Title, @Name, @RacePic, @FlagPic, @Level)";
						sqliteCommand.Parameters.AddWithValue("@RuinRaceID", RuinRaceData.RuinRaceID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@Title", RuinRaceData.Title);
						sqliteCommand.Parameters.AddWithValue("@Name", RuinRaceData.Name);
						sqliteCommand.Parameters.AddWithValue("@RacePic", RuinRaceData.RacePic);
						sqliteCommand.Parameters.AddWithValue("@FlagPic", RuinRaceData.FlagPic);
						sqliteCommand.Parameters.AddWithValue("@Level", RuinRaceData.Level);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1470);
			}
		}
	}
	
	/// <summary>
	/// method_95
	/// </summary>
	public class SaveShip
	{
		public struct ShipData
		{
			public int ShipID;
			public int FleetID;
			public string ShipName;
			public int SubFleetID;
			public bool ActiveSensorsOn;
			public int AssignedMSID;
			public int Autofire;
			public int BoardingCombatClock;
			public decimal Constructed;
			public decimal CrewMorale;
			public int CurrentCrew;
			public decimal CurrentShieldStrength;
			public decimal CurrentMaintSupplies;
			public int DamageControlID;
			public bool Destroyed;
			public int FireDelay;
			public decimal Fuel;
			public decimal GradePoints;
			public int HoldTechData;
			public int KillTonnageCommercial;
			public int KillTonnageMilitary;
			public decimal LastLaunchTime;
			public decimal LastMissileHitTime;
			public decimal LastBeamHitTime;
			public decimal LastDamageTime;
			public decimal LastPenetratingDamageTime;
			public decimal LastOverhaul;
			public decimal LastShoreLeave;
			public decimal LaunchMorale;
			public GEnum32 MaintenanceState;
			public int MothershipID;
			public int RaceID;
			public int RefuelPriority;
			public int ResupplyPriority;
			public AuroraRefuelStatus RefuelStatus;
			public AuroraOrdnanceTransferStatus OrdnanceTransferStatus;
			public bool ScrapFlag;
			public int SensorDelay;
			public bool ShieldsActive;
			public int ShipClassID;
			public string ShipNotes;
			public int ShippingLineID;
			public int SpeciesID;
			public int SquadronID;
			public int SyncFire;
			public decimal TFPoints;
			public int TractorTargetShipID;
			public int TractorParentShipID;
			public int TractorTargetShipyardID;
			public GEnum79 TransponderActive;
			public GEnum46 HangarLoadType;
			public bool AutomatedDamageControl;
			public decimal BioEnergy;
			public int AssignedFormationID;
			public decimal DistanceTravelled;
			public ShipOrdnanceTemplateData[] ShipOrdnanceTemplateStore;
			public ShipMeasurementData[] ShipMeasurementStore;
			public ShipWeaponData[] ShipWeaponStore;
			public DamagedComponentData[] DamagedComponentStore;
			public ShipHistoryData[] ShipHistoryStore;
			public WeaponRechargeData[] WeaponRechargeStore;
			public WeaponAssignmentData[] WeaponAssignmentStore;
			public ECCMAssignmentData[] ECCMAssignmentStore;
			public FireControlAssignmentData[] FireControlAssignmentStore;
			public MissileAssignmentData[] MissileAssignmentStore;
			public ArmourDamageData[] ArmourDamageStore;

		}
		ShipData[] ShipStore;

		public struct ShipOrdnanceTemplateData
		{
			public int ShipID;
			public int MissileID;
			public int Amount;
		}
		
		public struct ShipMeasurementData
		{
			public int ShipID;
			public AuroraMeasurementType MeasurementType;
			public decimal Amount;
			public bool StrikeGroup;
		}

		public struct ShipWeaponData
		{
			public int ShipID;
			public int MissileID;
			public int Amount;
		}

		public struct DamagedComponentData
		{
			public int ShipID;
			public int ComponentID;
			public int Number;
		}
		
		public struct ShipHistoryData
		{
			public int ShipID;
			public string Description;
			public decimal GameTime;
		}

		public struct WeaponRechargeData
		{
			public decimal RechargeRemaining;
			public int ShipID;
			public int WeaponID;
			public int WeaponNumber;
		}

		public struct WeaponAssignmentData
		{
			public int ShipID;
			public int WeaponID;
			public int WeaponNum;
			public int FCTypeID;
			public int FCNum;
		}

		public struct ECCMAssignmentData
		{
			public int ShipID;
			public int ECCMID;
			public int ECCMNum;
			public int FCTypeID;
			public int FCNum;
		}

		public struct FireControlAssignmentData
		{
			public int ShipID;
			public int FCTypeID;
			public int FCNum;
			public int TargetID;
			public AuroraContactType TargetType;
			public bool OpenFire;
			public bool NodeExpand;
			public AuroraPointDefenceMode PointDefenceMode;
			public int PointDefenceRange;
			public bool FiredThisPhase;
		}

		public struct MissileAssignmentData
		{
			public int ShipID;
			public int MissileID;
			public int WeaponID;
			public int WeaponNum;
		}

		public struct ArmourDamageData
		{
			public int ShipID;
			public int ArmourColumn;
			public int Damage;
		}


		public SaveShip(GClass0 game)
		{
			int i = 0;
			ShipStore = new ShipData[game.dictionary_4.Count];
			foreach (GClass39 gclass in game.dictionary_4.Values)
			{

				ShipOrdnanceTemplateData[] ShipOrdnanceTemplateStore = new ShipOrdnanceTemplateData[gclass.list_9.Count];

				int j = 0;
				foreach (GClass121 gclass2 in gclass.list_9)
				{
					var dataObj1 = new ShipOrdnanceTemplateData()
					{
						ShipID = gclass.int_8,
						MissileID = gclass2.gclass120_0.int_0,
						Amount = gclass2.int_0,
					};
					ShipOrdnanceTemplateStore[j] = dataObj1;
					j++;
				}

				ShipMeasurementData[] ShipMeasurementStore = new ShipMeasurementData[gclass.list_0.Count];
				j = 0;
				foreach (GClass53 gclass3 in gclass.list_0)
				{
					var dataObj1 = new ShipMeasurementData()
					{
						ShipID = gclass.int_8,
						MeasurementType = gclass3.auroraMeasurementType_0,
						Amount = gclass3.decimal_0,
						StrikeGroup = gclass3.bool_0,
					};
					ShipMeasurementStore[j] = dataObj1;
					j++;
				}
				
				ShipWeaponData[] ShipWeaponStore = new ShipWeaponData[gclass.list_10.Count];
				j = 0;
				foreach (GClass121 gclass4 in gclass.list_10)
				{
					var dataObj1 = new ShipWeaponData()
					{
						ShipID = gclass.int_8,
						MissileID = gclass4.gclass120_0.int_0,
						Amount = gclass4.int_0,
					};
					ShipWeaponStore[j] = dataObj1;
					j++;
				}
				
				DamagedComponentData[] DamagedComponentStore = new DamagedComponentData[gclass.list_12.Count];
				j = 0;
				foreach (GClass159 gclass5 in gclass.list_12)
				{
					var dataObj1 = new DamagedComponentData()
					{
						ShipID = gclass.int_8,
						ComponentID = gclass5.gclass206_0.int_0,
						Number = gclass5.int_0,
					};
					DamagedComponentStore[j] = dataObj1;
					j++;
				}
				
				ShipHistoryData[] ShipHistoryStore = new ShipHistoryData[gclass.list_11.Count];
				j = 0;
				foreach (GClass158 gclass6 in gclass.list_11)
				{
					var dataObj1 = new ShipHistoryData()
					{
						ShipID = gclass.int_8,
						Description = gclass6.Description,
						GameTime = gclass6.decimal_0,
					};
					ShipHistoryStore[j] = dataObj1;
					j++;
				}
				
				WeaponRechargeData[] WeaponRechargeStore = new WeaponRechargeData[gclass.list_7.Count];
				j = 0;
				foreach (GClass27 gclass7 in gclass.list_7)
				{
					var dataObj1 = new WeaponRechargeData()
					{
						RechargeRemaining = gclass7.decimal_0,
						ShipID = gclass.int_8,
						WeaponID = gclass7.gclass206_0.int_0,
						WeaponNumber = gclass7.int_0,
					};
					WeaponRechargeStore[j] = dataObj1;
					j++;
				}
				
				WeaponAssignmentData[] WeaponAssignmentStore = new WeaponAssignmentData[gclass.list_2.Count];
				j = 0;
				foreach (GClass31 gclass8 in gclass.list_2)
				{
					var dataObj1 = new WeaponAssignmentData()
					{
						ShipID = gclass.int_8,
						WeaponID = gclass8.gclass206_0.int_0,
						WeaponNum = gclass8.int_0,
						FCTypeID = gclass8.gclass206_1.int_0,
						FCNum = gclass8.int_1,
					};
					WeaponAssignmentStore[j] = dataObj1;
					j++;
				}
				
				ECCMAssignmentData[] ECCMAssignmentStore = new ECCMAssignmentData[gclass.list_3.Count];
				j = 0;
				foreach (GClass34 gclass9 in gclass.list_3)
				{
					var dataObj1 = new ECCMAssignmentData()
					{
						ShipID = gclass.int_8,
						ECCMID = gclass9.gclass206_0.int_0,
						ECCMNum = gclass9.int_0,
						FCTypeID = gclass9.gclass206_1.int_0,
						FCNum = gclass9.int_1,
					};
					ECCMAssignmentStore[j] = dataObj1;
					j++;
				}
				
				FireControlAssignmentData[] FireControlAssignmentStore = new FireControlAssignmentData[gclass.list_4.Count];
				j = 0;
				foreach (GClass35 gclass10 in gclass.list_4)
				{
					var dataObj1 = new FireControlAssignmentData()
					{
						ShipID = gclass.int_8,
						FCTypeID = gclass10.gclass206_0.int_0,
						FCNum = gclass10.int_0,
						TargetID = gclass10.int_1,
						TargetType = gclass10.auroraContactType_0,
						OpenFire = gclass10.bool_0,
						NodeExpand = gclass10.bool_2,
						PointDefenceMode = gclass10.auroraPointDefenceMode_0,
						PointDefenceRange = gclass10.int_2,
						FiredThisPhase = gclass10.bool_1,
					};
					FireControlAssignmentStore[j] = dataObj1;
					j++;
				}
				
				MissileAssignmentData[] MissileAssignmentStore = new MissileAssignmentData[gclass.list_5.Count];
				j = 0;
				foreach (GClass30 gclass11 in gclass.list_5)
				{
					var dataObj1 = new MissileAssignmentData()
					{
						ShipID = gclass.int_8,
						MissileID = gclass11.gclass120_0.int_0,
						WeaponID = gclass11.gclass206_0.int_0,
						WeaponNum = gclass11.int_0,
					};
					MissileAssignmentStore[j] = dataObj1;
					j++;
				}
				
				ArmourDamageData[] ArmourDamageStore = new ArmourDamageData[gclass.dictionary_3.Count];
				j = 0;
				foreach (KeyValuePair<int, int> keyValuePair in gclass.dictionary_3)
				{
					var dataObj1 = new ArmourDamageData()
					{
						ShipID = gclass.int_8,
						ArmourColumn = keyValuePair.Key,
						Damage = keyValuePair.Value,
					};
					ArmourDamageStore[j] = dataObj1;
					j++;
				}

				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				if (gclass.gclass77_0 != null)
				{
					num = gclass.gclass77_0.int_0;
				}
				if (gclass.gclass39_1 != null)
				{
					num2 = gclass.gclass39_1.int_8;
				}
				if (gclass.gclass39_0 != null)
				{
					num3 = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass165_0 != null)
				{
					num4 = gclass.gclass165_0.int_0;
				}
				if (gclass.gclass39_2 != null)
				{
					num6 = gclass.gclass39_2.int_8;
				}
				if (gclass.gclass171_0 != null)
				{
					num5 = gclass.gclass171_0.int_0;
				}
				if (gclass.gclass39_3 != null)
				{
					num7 = gclass.gclass39_3.int_8;
				}
				if (gclass.gclass95_0 != null)
				{
					num8 = gclass.gclass95_0.int_0;
				}
				var dataObj = new ShipData()
				{
					ShipID = gclass.int_8,
					FleetID = gclass.gclass78_0.int_3,
					ShipName = gclass.ShipName,
					SubFleetID = num,
					ActiveSensorsOn = gclass.bool_7,
					AssignedMSID = num2,
					Autofire = gclass.int_9,
					BoardingCombatClock = gclass.int_10,
					Constructed = gclass.decimal_0,
					CrewMorale = gclass.decimal_1,
					CurrentCrew = gclass.int_11,
					CurrentShieldStrength = gclass.decimal_2,
					CurrentMaintSupplies = gclass.decimal_3,
					DamageControlID = gclass.int_12,
					Destroyed = gclass.bool_8,
					FireDelay = gclass.int_13,
					Fuel = gclass.decimal_10,
					GradePoints = gclass.decimal_11,
					HoldTechData = gclass.int_14,
					KillTonnageCommercial = gclass.int_15,
					KillTonnageMilitary = gclass.int_16,
					LastLaunchTime = gclass.decimal_4,
					LastMissileHitTime = gclass.decimal_15,
					LastBeamHitTime = gclass.decimal_16,
					LastDamageTime = gclass.decimal_14,
					LastPenetratingDamageTime = gclass.decimal_17,
					LastOverhaul = gclass.decimal_5,
					LastShoreLeave = gclass.decimal_6,
					LaunchMorale = gclass.decimal_7,
					MaintenanceState = gclass.genum32_0,
					MothershipID = num3,
					RaceID = gclass.gclass21_0.RaceID,
					RefuelPriority = gclass.int_21,
					ResupplyPriority = gclass.int_22,
					RefuelStatus = gclass.auroraRefuelStatus_0,
					OrdnanceTransferStatus = gclass.auroraOrdnanceTransferStatus_0,
					ScrapFlag = gclass.bool_10,
					SensorDelay = gclass.int_17,
					ShieldsActive = gclass.bool_9,
					ShipClassID = gclass.gclass22_0.int_0,
					ShipNotes = gclass.string_0,
					ShippingLineID = num4,
					SpeciesID = gclass.gclass172_0.int_0,
					SquadronID = gclass.int_19,
					SyncFire = gclass.int_20,
					TFPoints = gclass.decimal_12,
					TractorTargetShipID = num6,
					TractorParentShipID = num7,
					TractorTargetShipyardID = num5,
					TransponderActive = gclass.genum79_0,
					HangarLoadType = gclass.genum46_0,
					AutomatedDamageControl = gclass.bool_13,
					BioEnergy = gclass.decimal_13,
					AssignedFormationID = num8,
					DistanceTravelled = gclass.decimal_9,
					ShipOrdnanceTemplateStore = ShipOrdnanceTemplateStore,
					ShipMeasurementStore = ShipMeasurementStore,
					ShipWeaponStore = ShipWeaponStore,
					DamagedComponentStore = DamagedComponentStore,
					ShipHistoryStore = ShipHistoryStore,
					WeaponRechargeStore = WeaponRechargeStore,
					WeaponAssignmentStore = WeaponAssignmentStore,
					ECCMAssignmentStore = ECCMAssignmentStore,
					FireControlAssignmentStore = FireControlAssignmentStore,
					MissileAssignmentStore = MissileAssignmentStore,
					ArmourDamageStore = ArmourDamageStore,
				};
				ShipStore[i] = dataObj;
				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Ship WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ShipWeapon WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ShipOrdnanceTemplate WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_DamagedComponent WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ShipHistory WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_WeaponRecharge WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_WeaponAssignment WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ECCMAssignment WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_FireControlAssignment WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_MissileAssignment WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ArmourDamage WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_ShipMeasurement WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in ShipStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Ship (ShipID, GameID, FleetID, ShipName, SubFleetID, ActiveSensorsOn, AssignedMSID, Autofire, BoardingCombatClock, Constructed, CrewMorale, CurrentCrew, CurrentShieldStrength, CurrentMaintSupplies, DamageControlID, Destroyed, FireDelay, Fuel, GradePoints, HoldTechData, KillTonnageCommercial, KillTonnageMilitary, LastLaunchTime, LastMissileHitTime, LastBeamHitTime, LastDamageTime, LastPenetratingDamageTime, LastOverhaul, BioEnergy,\r\n                        LastShoreLeave, LaunchMorale, MaintenanceState, MothershipID, RaceID, RefuelPriority, ResupplyPriority, RefuelStatus, OrdnanceTransferStatus, ScrapFlag, SensorDelay, ShieldsActive, ShipClassID, ShipNotes, ShippingLineID, SpeciesID, SquadronID, SyncFire, TFPoints, TractorTargetShipID, TractorTargetShipyardID, TractorParentShipID, TransponderActive, HangarLoadType, AutomatedDamageControl, AssignedFormationID, DistanceTravelled ) \r\n                        VALUES ( @ShipID, @GameID, @FleetID, @ShipName, @SubFleetID, @ActiveSensorsOn, @AssignedMSID, @Autofire, @BoardingCombatClock, @Constructed, @CrewMorale, @CurrentCrew, @CurrentShieldStrength, @CurrentMaintSupplies, @DamageControlID, @Destroyed, @FireDelay, @Fuel, @GradePoints, @HoldTechData, @KillTonnageCommercial, @KillTonnageMilitary, @LastLaunchTime, @LastMissileHitTime, @LastBeamHitTime, @LastDamageTime, @LastPenetratingDamageTime, @LastOverhaul, @BioEnergy,\r\n                        @LastShoreLeave, @LaunchMorale, @MaintenanceState, @MothershipID, @RaceID, @RefuelPriority, @ResupplyPriority, @RefuelStatus, @OrdnanceTransferStatus, @ScrapFlag, @SensorDelay, @ShieldsActive, @ShipClassID, @ShipNotes, @ShippingLineID, @SpeciesID, @SquadronID, @SyncFire, @TFPoints, @TractorTargetShipID, @TractorTargetShipyardID, @TractorParentShipID, @TransponderActive, @HangarLoadType, @AutomatedDamageControl, @AssignedFormationID, @DistanceTravelled )";
						sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj.ShipID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@FleetID", dataObj.FleetID);
						sqliteCommand.Parameters.AddWithValue("@ShipName", dataObj.ShipName);
						sqliteCommand.Parameters.AddWithValue("@SubFleetID", dataObj.SubFleetID);
						sqliteCommand.Parameters.AddWithValue("@ActiveSensorsOn", dataObj.ActiveSensorsOn);
						sqliteCommand.Parameters.AddWithValue("@AssignedMSID", dataObj.AssignedMSID);
						sqliteCommand.Parameters.AddWithValue("@Autofire", dataObj.Autofire);
						sqliteCommand.Parameters.AddWithValue("@BoardingCombatClock", dataObj.BoardingCombatClock);
						sqliteCommand.Parameters.AddWithValue("@Constructed", dataObj.Constructed);
						sqliteCommand.Parameters.AddWithValue("@CrewMorale", dataObj.CrewMorale);
						sqliteCommand.Parameters.AddWithValue("@CurrentCrew", dataObj.CurrentCrew);
						sqliteCommand.Parameters.AddWithValue("@CurrentShieldStrength", dataObj.CurrentShieldStrength);
						sqliteCommand.Parameters.AddWithValue("@CurrentMaintSupplies", dataObj.CurrentMaintSupplies);
						sqliteCommand.Parameters.AddWithValue("@DamageControlID", dataObj.DamageControlID);
						sqliteCommand.Parameters.AddWithValue("@Destroyed", dataObj.Destroyed);
						sqliteCommand.Parameters.AddWithValue("@FireDelay", dataObj.FireDelay);
						sqliteCommand.Parameters.AddWithValue("@Fuel", dataObj.Fuel);
						sqliteCommand.Parameters.AddWithValue("@GradePoints", dataObj.GradePoints);
						sqliteCommand.Parameters.AddWithValue("@HoldTechData", dataObj.HoldTechData);
						sqliteCommand.Parameters.AddWithValue("@KillTonnageCommercial", dataObj.KillTonnageCommercial);
						sqliteCommand.Parameters.AddWithValue("@KillTonnageMilitary", dataObj.KillTonnageMilitary);
						sqliteCommand.Parameters.AddWithValue("@LastLaunchTime", dataObj.LastLaunchTime);
						sqliteCommand.Parameters.AddWithValue("@LastMissileHitTime", dataObj.LastMissileHitTime);
						sqliteCommand.Parameters.AddWithValue("@LastBeamHitTime", dataObj.LastBeamHitTime);
						sqliteCommand.Parameters.AddWithValue("@LastDamageTime", dataObj.LastDamageTime);
						sqliteCommand.Parameters.AddWithValue("@LastPenetratingDamageTime",
							dataObj.LastPenetratingDamageTime);
						sqliteCommand.Parameters.AddWithValue("@LastOverhaul", dataObj.LastOverhaul);
						sqliteCommand.Parameters.AddWithValue("@LastShoreLeave", dataObj.LastShoreLeave);
						sqliteCommand.Parameters.AddWithValue("@LaunchMorale", dataObj.LaunchMorale);
						sqliteCommand.Parameters.AddWithValue("@MaintenanceState", dataObj.MaintenanceState);
						sqliteCommand.Parameters.AddWithValue("@MothershipID", dataObj.MothershipID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@RefuelPriority", dataObj.RefuelPriority);
						sqliteCommand.Parameters.AddWithValue("@ResupplyPriority", dataObj.ResupplyPriority);
						sqliteCommand.Parameters.AddWithValue("@RefuelStatus", dataObj.RefuelStatus);
						sqliteCommand.Parameters.AddWithValue("@OrdnanceTransferStatus",
							dataObj.OrdnanceTransferStatus);
						sqliteCommand.Parameters.AddWithValue("@ScrapFlag", dataObj.ScrapFlag);
						sqliteCommand.Parameters.AddWithValue("@SensorDelay", dataObj.SensorDelay);
						sqliteCommand.Parameters.AddWithValue("@ShieldsActive", dataObj.ShieldsActive);
						sqliteCommand.Parameters.AddWithValue("@ShipClassID", dataObj.ShipClassID);
						sqliteCommand.Parameters.AddWithValue("@ShipNotes", dataObj.ShipNotes);
						sqliteCommand.Parameters.AddWithValue("@ShippingLineID", dataObj.ShippingLineID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@SquadronID", dataObj.SquadronID);
						sqliteCommand.Parameters.AddWithValue("@SyncFire", dataObj.SyncFire);
						sqliteCommand.Parameters.AddWithValue("@TFPoints", dataObj.TFPoints);
						sqliteCommand.Parameters.AddWithValue("@TractorTargetShipID", dataObj.TractorTargetShipID);
						sqliteCommand.Parameters.AddWithValue("@TractorParentShipID", dataObj.TractorParentShipID);
						sqliteCommand.Parameters.AddWithValue("@TractorTargetShipyardID",
							dataObj.TractorTargetShipyardID);
						sqliteCommand.Parameters.AddWithValue("@TransponderActive", dataObj.TransponderActive);
						sqliteCommand.Parameters.AddWithValue("@HangarLoadType", dataObj.HangarLoadType);
						sqliteCommand.Parameters.AddWithValue("@AutomatedDamageControl",
							dataObj.AutomatedDamageControl);
						sqliteCommand.Parameters.AddWithValue("@BioEnergy", dataObj.BioEnergy);
						sqliteCommand.Parameters.AddWithValue("@AssignedFormationID", dataObj.AssignedFormationID);
						sqliteCommand.Parameters.AddWithValue("@DistanceTravelled", dataObj.DistanceTravelled);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.ShipOrdnanceTemplateStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipOrdnanceTemplate (GameID, ShipID, MissileID, Amount ) VALUES ( @GameID, @ShipID, @MissileID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj1.MissileID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ShipMeasurementStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipMeasurement ( ShipID, MeasurementType, Amount, StrikeGroup, GameID ) VALUES ( @ShipID, @MeasurementType, @Amount, @StrikeGroup, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@MeasurementType", dataObj1.MeasurementType);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.Parameters.AddWithValue("@StrikeGroup", dataObj1.StrikeGroup);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ShipWeaponStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipWeapon (GameID, ShipID, MissileID, Amount ) VALUES ( @GameID, @ShipID, @MissileID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj1.MissileID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.DamagedComponentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_DamagedComponent (ShipID, ComponentID, GameID, Number ) VALUES ( @ShipID, @ComponentID, @GameID, @Number )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@ComponentID", dataObj1.ComponentID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@Number", dataObj1.Number);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ShipHistoryStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ShipHistory ( GameID, ShipID, Description, GameTime ) VALUES ( @GameID, @ShipID, @Description, @GameTime )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@Description", dataObj1.Description);
							sqliteCommand.Parameters.AddWithValue("@GameTime", dataObj1.GameTime);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.WeaponRechargeStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_WeaponRecharge (GameID, RechargeRemaining, ShipID, WeaponID, WeaponNumber ) VALUES ( @GameID, @RechargeRemaining, @ShipID, @WeaponID, @WeaponNumber )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@RechargeRemaining", dataObj1.RechargeRemaining);
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@WeaponID", dataObj1.WeaponID);
							sqliteCommand.Parameters.AddWithValue("@WeaponNumber", dataObj1.WeaponNumber);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.WeaponAssignmentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_WeaponAssignment (ShipID, WeaponID, WeaponNum, FCTypeID, FCNum, GameID ) VALUES ( @ShipID, @WeaponID, @WeaponNum, @FCTypeID, @FCNum, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@WeaponID", dataObj1.WeaponID);
							sqliteCommand.Parameters.AddWithValue("@WeaponNum", dataObj1.WeaponNum);
							sqliteCommand.Parameters.AddWithValue("@FCTypeID", dataObj1.FCTypeID);
							sqliteCommand.Parameters.AddWithValue("@FCNum", dataObj1.FCNum);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ECCMAssignmentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ECCMAssignment (ShipID, ECCMID, ECCMNum, FCTypeID, FCNum, GameID ) VALUES ( @ShipID, @ECCMID, @ECCMNum, @FCTypeID, @FCNum, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@ECCMID", dataObj1.ECCMID);
							sqliteCommand.Parameters.AddWithValue("@ECCMNum", dataObj1.ECCMNum);
							sqliteCommand.Parameters.AddWithValue("@FCTypeID", dataObj1.FCTypeID);
							sqliteCommand.Parameters.AddWithValue("@FCNum", dataObj1.FCNum);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.FireControlAssignmentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_FireControlAssignment (ShipID, GameID, FCTypeID, FCNum, TargetID, TargetType, OpenFire, NodeExpand, PointDefenceMode, PointDefenceRange, FiredThisPhase ) VALUES ( @ShipID, @GameID, @FCTypeID, @FCNum, @TargetID, @TargetType, @OpenFire, @NodeExpand, @PointDefenceMode, @PointDefenceRange, @FiredThisPhase )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@FCTypeID", dataObj1.FCTypeID);
							sqliteCommand.Parameters.AddWithValue("@FCNum", dataObj1.FCNum);
							sqliteCommand.Parameters.AddWithValue("@TargetID", dataObj1.TargetID);
							sqliteCommand.Parameters.AddWithValue("@TargetType", dataObj1.TargetType);
							sqliteCommand.Parameters.AddWithValue("@OpenFire", dataObj1.OpenFire);
							sqliteCommand.Parameters.AddWithValue("@NodeExpand", dataObj1.NodeExpand);
							sqliteCommand.Parameters.AddWithValue("@PointDefenceMode", dataObj1.PointDefenceMode);
							sqliteCommand.Parameters.AddWithValue("@PointDefenceRange", dataObj1.PointDefenceRange);
							sqliteCommand.Parameters.AddWithValue("@FiredThisPhase", dataObj1.FiredThisPhase);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.MissileAssignmentStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_MissileAssignment (ShipID, MissileID, WeaponID, WeaponNum, GameID ) VALUES ( @ShipID, @MissileID, @WeaponID, @WeaponNum, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj1.MissileID);
							sqliteCommand.Parameters.AddWithValue("@WeaponID", dataObj1.WeaponID);
							sqliteCommand.Parameters.AddWithValue("@WeaponNum", dataObj1.WeaponNum);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj1 in dataObj.ArmourDamageStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_ArmourDamage (ShipID, ArmourColumn, Damage, GameID ) VALUES ( @ShipID, @ArmourColumn, @Damage, @GameID )";
							sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj1.ShipID);
							sqliteCommand.Parameters.AddWithValue("@ArmourColumn", dataObj1.ArmourColumn);
							sqliteCommand.Parameters.AddWithValue("@Damage", dataObj1.Damage);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1471);
			}
		}
	}

	/// <summary>
	/// method_96
	/// </summary>
	public class SaveContacts
	{
		public struct ContactsData
		{
			public int UniqueID;
			public int ContactID;
			public int SystemID;
			public int DetectRaceID;
			public int ContactRaceID;
			public string ContactName;
			public decimal CreationTime;
			public decimal Reestablished;
			public decimal LastUpdate;
			public GEnum9 ContactMethod;
			public AuroraContactType ContactType;
			public decimal ContactStrength;
			public int ContactNumber;
			public int Resolution;
			public int ContinualContactTime;
			public double Xcor;
			public double Ycor;
			public double LastXcor;
			public double LastYcor;
			public int Speed;
			public bool Msg;
			public double IncrementStartX;
			public double IncrementStartY;
		}

		ContactsData[] ContactsStore;
		
		public SaveContacts(GClass0 game)
		{
			int i = 0;
			ContactsStore = new ContactsData[game.dictionary_26.Count];
			foreach (GClass63 gclass in game.dictionary_26.Values)
			{
				switch (gclass.auroraContactType_0)
				{
					case AuroraContactType.Ship:
						if (game.dictionary_4.ContainsKey(gclass.int_1))
						{
							gclass.gclass39_0 = game.dictionary_4[gclass.int_1];
						}

						break;
					case AuroraContactType.Salvo:
						if (game.dictionary_6.ContainsKey(gclass.int_1))
						{
							gclass.gclass122_0 = game.dictionary_6[gclass.int_1];
						}

						break;
					case AuroraContactType.Population:
						if (game.dictionary_20.ContainsKey(gclass.int_1))
						{
							gclass.gclass132_0 = game.dictionary_20[gclass.int_1];
						}

						break;
				}

				var dataObj = new ContactsData()
				{
					UniqueID = gclass.int_0,
					ContactID = gclass.int_1,
					SystemID = gclass.gclass178_0.int_0,
					DetectRaceID = gclass.gclass21_1.RaceID,
					ContactRaceID = gclass.method_3(),
					ContactName = gclass.string_0,
					CreationTime = gclass.decimal_1,
					Reestablished = gclass.decimal_2,
					LastUpdate = gclass.decimal_3,
					ContactMethod = gclass.genum9_0,
					ContactType = gclass.auroraContactType_0,
					ContactStrength = gclass.decimal_0,
					ContactNumber = gclass.int_2,
					Resolution = gclass.int_3,
					ContinualContactTime = gclass.int_4,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					LastXcor = gclass.double_2,
					LastYcor = gclass.double_3,
					Speed = gclass.int_5,
					Msg = gclass.bool_0,
					IncrementStartX = gclass.double_4,
					IncrementStartY = gclass.double_5,
				};
				ContactsStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Contacts WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in ContactsStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Contacts (UniqueID, ContactID, GameID, SystemID, DetectRaceID, ContactRaceID, ContactName, CreationTime, Reestablished, LastUpdate, ContactMethod, ContactType, ContactStrength, ContactNumber, Resolution, ContinualContactTime, Xcor, Ycor, LastXcor, LastYcor, Speed, Msg, IncrementStartX, IncrementStartY ) \r\n                        VALUES ( @UniqueID, @ContactID, @GameID, @SystemID, @DetectRaceID, @ContactRaceID, @ContactName, @CreationTime, @Reestablished, @LastUpdate, @ContactMethod, @ContactType, @ContactStrength, @ContactNumber, @Resolution, @ContinualContactTime, @Xcor, @Ycor, @LastXcor, @LastYcor, @Speed, @Msg, @IncrementStartX, @IncrementStartY)";
						sqliteCommand.Parameters.AddWithValue("@UniqueID", dataObj.UniqueID);
						sqliteCommand.Parameters.AddWithValue("@ContactID", dataObj.ContactID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@DetectRaceID", dataObj.DetectRaceID);
						sqliteCommand.Parameters.AddWithValue("@ContactRaceID", dataObj.ContactRaceID);
						sqliteCommand.Parameters.AddWithValue("@ContactName", dataObj.ContactName);
						sqliteCommand.Parameters.AddWithValue("@CreationTime", dataObj.CreationTime);
						sqliteCommand.Parameters.AddWithValue("@Reestablished", dataObj.Reestablished);
						sqliteCommand.Parameters.AddWithValue("@LastUpdate", dataObj.LastUpdate);
						sqliteCommand.Parameters.AddWithValue("@ContactMethod", dataObj.ContactMethod);
						sqliteCommand.Parameters.AddWithValue("@ContactType", dataObj.ContactType);
						sqliteCommand.Parameters.AddWithValue("@ContactStrength", dataObj.ContactStrength);
						sqliteCommand.Parameters.AddWithValue("@ContactNumber", dataObj.ContactNumber);
						sqliteCommand.Parameters.AddWithValue("@Resolution", dataObj.Resolution);
						sqliteCommand.Parameters.AddWithValue("@ContinualContactTime", dataObj.ContinualContactTime);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.Parameters.AddWithValue("@LastXcor", dataObj.LastXcor);
						sqliteCommand.Parameters.AddWithValue("@LastYcor", dataObj.LastYcor);
						sqliteCommand.Parameters.AddWithValue("@Speed", dataObj.Speed);
						sqliteCommand.Parameters.AddWithValue("@Msg", dataObj.Msg);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartX", dataObj.IncrementStartX);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartY", dataObj.IncrementStartY);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1472);
			}
		}
	}

	/// <summary>
	/// method_97
	/// </summary>
	public class SaveWrecks
	{
		public struct WrecksData
		{
			public int WreckID;
			public int RaceID;
			public int SystemID;
			public int OrbitBodyID;
			public int ClassID;
			public decimal Size;
			public int EffectiveSize;
			public int StarSwarmHatching;
			public int QueenStatus;
			public double Xcor;
			public double Ycor;
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
			public WreckTechData[] WreckTechStore;
			public WreckComponentsData[] WreckComponentsStore;
		}
		WrecksData[] WrecksStore;

		public struct WreckTechData
		{
			public int WreckID;
			public int TechID;
			public decimal Percentage;
		}

		public struct WreckComponentsData
		{
			public int WreckID;
			public int ComponentID;
			public int Amount;
		}

		public SaveWrecks(GClass0 game)
		{
			int i = 0;
			WrecksStore = new WrecksData[game.dictionary_27.Count];
			foreach (GClass208 gclass in game.dictionary_27.Values)
			{

				WreckTechData[] WreckTechStore = new WreckTechData[gclass.list_0.Count];
				int j = 0;
				foreach (GClass209 gclass2 in gclass.list_0)
				{
					var dataOb1 = new WreckTechData()
					{
						WreckID = gclass.int_0,
						TechID = gclass2.gclass147_0.int_0,
						Percentage = gclass2.decimal_0,
					};
					WreckTechStore[j] = dataOb1;
					j++;
				}
				
				WreckComponentsData[] WreckComponentsStore = new WreckComponentsData[gclass.list_1.Count];
				j = 0;
				foreach (GClass210 gclass3 in gclass.list_1)
				{
					var dataOb1 = new WreckComponentsData()
					{
						WreckID = gclass.int_0,
						ComponentID = gclass3.gclass206_0.int_0,
						Amount = gclass3.int_0,
					};
					WreckComponentsStore[j] = dataOb1;
					j++;
				}

				
				int num = 0;
				if (gclass.gclass1_0 != null)
				{
					num = gclass.gclass1_0.int_0;
				}
				if (gclass.gclass1_0 != null)
				{
					num = gclass.gclass1_0.int_0;
				}
				var dataObj = new WrecksData()
				{
					WreckID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					SystemID = gclass.gclass178_0.int_0,
					OrbitBodyID = num,
					ClassID = gclass.gclass22_0.int_0,
					Size = gclass.decimal_0,
					EffectiveSize = gclass.int_1,
					StarSwarmHatching = gclass.int_2,
					QueenStatus = gclass.int_3,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
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
					WreckTechStore = WreckTechStore,
					WreckComponentsStore = WreckComponentsStore,
				};
				WrecksStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Wrecks WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_WreckTech WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_WreckComponents WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in WrecksStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Wrecks (WreckID, GameID, RaceID, SystemID, OrbitBodyID, ClassID, Size, EffectiveSize, StarSwarmHatching, QueenStatus, Xcor, Ycor, Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite ) \r\n                        VALUES ( @WreckID, @GameID, @RaceID, @SystemID, @OrbitBodyID, @ClassID, @Size, @EffectiveSize, @StarSwarmHatching, @QueenStatus, @Xcor, @Ycor, @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite )";
						sqliteCommand.Parameters.AddWithValue("@WreckID", dataObj.WreckID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@OrbitBodyID", dataObj.OrbitBodyID);
						sqliteCommand.Parameters.AddWithValue("@ClassID", dataObj.ClassID);
						sqliteCommand.Parameters.AddWithValue("@Size", dataObj.Size);
						sqliteCommand.Parameters.AddWithValue("@EffectiveSize", dataObj.EffectiveSize);
						sqliteCommand.Parameters.AddWithValue("@StarSwarmHatching", dataObj.StarSwarmHatching);
						sqliteCommand.Parameters.AddWithValue("@QueenStatus", dataObj.QueenStatus);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.Parameters.AddWithValue("@Duranium", dataObj.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", dataObj.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", dataObj.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", dataObj.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", dataObj.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", dataObj.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", dataObj.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", dataObj.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", dataObj.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", dataObj.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", dataObj.Gallicite);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataOb1 in dataObj.WreckTechStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_WreckTech ( GameID, WreckID, TechID, Percentage ) VALUES ( @GameID, @WreckID, @TechID, @Percentage )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@WreckID", dataOb1.WreckID);
							sqliteCommand.Parameters.AddWithValue("@TechID", dataOb1.TechID);
							sqliteCommand.Parameters.AddWithValue("@Percentage", dataOb1.Percentage);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataOb1 in dataObj.WreckComponentsStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_WreckComponents ( WreckID, ComponentID, GameID, Amount ) VALUES ( @WreckID, @ComponentID, @GameID, @Amount )";
							sqliteCommand.Parameters.AddWithValue("@WreckID", dataOb1.WreckID);
							sqliteCommand.Parameters.AddWithValue("@ComponentID", dataOb1.ComponentID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataOb1.Amount);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1473);
			}
		}
	}

	/// <summary>
	/// method_98
	/// </summary>
	public class SaveIndustrialProjects
	{
		public struct IndustrialProjectsData
		{
			public int ProjectID;
			public int RaceID;
			public int PopulationID;
			public int SpeciesID;
			public decimal Percentage;
			public AuroraProductionType ProductionType;
			public int ProductionID;
			public int RefitClassID;
			public GEnum21 WealthUse;
			public decimal Amount;
			public decimal PartialCompletion;
			public decimal ProdPerUnit;
			public string Description;
			public bool Pause;
			public int Queue;
			public int FuelRequired;
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
		IndustrialProjectsData[] IndustrialProjectsStore;


		public SaveIndustrialProjects(GClass0 game)
		{
			int i = 0;
			int num = 0;
			var list = game.dictionary_20.Values.SelectMany<GClass132, GClass140>((Func<GClass132, IEnumerable<GClass140>>)(x => (IEnumerable<GClass140>) x.dictionary_2.Values)).ToList<GClass140>();
			IndustrialProjectsStore = new IndustrialProjectsData[list.Count];
			foreach(GClass140 gclass in list)
			{
				int num2 = 0;
				if (gclass.gclass22_1 != null)
				{
					num2 = gclass.gclass22_1.int_0;
				}
				switch (gclass.auroraProductionType_0)
				{
					case AuroraProductionType.Construction:
					num = (int)gclass.gclass141_0.auroraInstallationType_0;
					break;
					case AuroraProductionType.Ordnance:
					num = gclass.gclass120_0.int_0;
					break;
					case AuroraProductionType.Fighter:
					case AuroraProductionType.SpaceStation:
					num = gclass.gclass22_0.int_0;
					break;
					case AuroraProductionType.Components:
					num = gclass.gclass206_0.int_0;
					break;
				}
				var dataObj = new IndustrialProjectsData()
				{
					ProjectID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					PopulationID = gclass.gclass132_0.int_5,
					SpeciesID = gclass.gclass172_0.int_0,
					Percentage = gclass.decimal_3,
					ProductionType = gclass.auroraProductionType_0,
					ProductionID = num,
					RefitClassID = num2,
					WealthUse = gclass.gclass136_0.genum21_0,
					Amount = gclass.decimal_0,
					PartialCompletion = gclass.decimal_1,
					ProdPerUnit = gclass.decimal_2,
					Description = gclass.string_0,
					Pause = gclass.bool_0,
					Queue = gclass.int_2,
					FuelRequired = gclass.int_1,
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
				IndustrialProjectsStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_IndustrialProjects WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in IndustrialProjectsStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_IndustrialProjects (ProjectID, GameID, RaceID, PopulationID, SpeciesID, Percentage, ProductionType, ProductionID, RefitClassID, WealthUse, Amount, PartialCompletion, ProdPerUnit, Description, Pause, Queue, FuelRequired, Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite) \r\n                        VALUES ( @ProjectID, @GameID, @RaceID, @PopulationID, @SpeciesID, @Percentage, @ProductionType, @ProductionID, @RefitClassID, @WealthUse, @Amount, @PartialCompletion, @ProdPerUnit, @Description, @Pause, @Queue, @FuelRequired, @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite)";
						sqliteCommand.Parameters.AddWithValue("@ProjectID", dataObj.ProjectID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", dataObj.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@Percentage", dataObj.Percentage);
						sqliteCommand.Parameters.AddWithValue("@ProductionType", dataObj.ProductionType);
						sqliteCommand.Parameters.AddWithValue("@ProductionID", dataObj.ProductionID);
						sqliteCommand.Parameters.AddWithValue("@RefitClassID", dataObj.RefitClassID);
						sqliteCommand.Parameters.AddWithValue("@WealthUse", dataObj.WealthUse);
						sqliteCommand.Parameters.AddWithValue("@Amount", dataObj.Amount);
						sqliteCommand.Parameters.AddWithValue("@PartialCompletion", dataObj.PartialCompletion);
						sqliteCommand.Parameters.AddWithValue("@ProdPerUnit", dataObj.ProdPerUnit);
						sqliteCommand.Parameters.AddWithValue("@Description", dataObj.Description);
						sqliteCommand.Parameters.AddWithValue("@Pause", dataObj.Pause);
						sqliteCommand.Parameters.AddWithValue("@Queue", dataObj.Queue);
						sqliteCommand.Parameters.AddWithValue("@FuelRequired", dataObj.FuelRequired);
						sqliteCommand.Parameters.AddWithValue("@Duranium", dataObj.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", dataObj.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", dataObj.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", dataObj.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", dataObj.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", dataObj.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", dataObj.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", dataObj.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", dataObj.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", dataObj.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", dataObj.Gallicite);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1474);
			}
		}
	}

	/// <summary>
	/// method_99
	/// </summary>
	public class SaveMissileSalvo
	{
		public struct MissileSalvoData
		{
			public int MissileSalvoID;
			public int ShipID;
			public int RaceID;
			public int SystemID;
			public int OrbitBodyID;
			public int MissileID;
			public int MissileNum;
			public decimal LaunchTime;
			public int FireControlID;
			public int FCNum;
			public int TargetID;
			public AuroraContactType TargetType;
			public double MissileSpeed;
			public double ModifierToHit;
			public decimal Endurance;
			public double Xcor;
			public double Ycor;
			public double LastXcor;
			public double LastYcor;
			public double LastTargetX;
			public double LastTargetY;
			public double IncrementStartX;
			public double IncrementStartY;
			public GEnum61 HomingMethod;
		}
		MissileSalvoData[] MissileSalvoStore;


		public SaveMissileSalvo(GClass0 game)
		{
			int i = 0;
			MissileSalvoStore = new MissileSalvoData[game.dictionary_6.Count];
			foreach (GClass122 gclass in game.dictionary_6.Values)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (gclass.gclass39_0 != null)
				{
					num = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass1_0 != null)
				{
					num2 = gclass.gclass1_0.int_0;
				}
				if (gclass.gclass206_0 != null)
				{
					num3 = gclass.gclass206_0.int_0;
				}
				if (gclass.gclass39_1 != null || gclass.gclass122_0 != null || gclass.gclass132_0 != null || gclass.gclass190_0 == null)
				{
				}
				var dataObj = new MissileSalvoData()
				{
					MissileSalvoID = gclass.int_1,
					ShipID = num,
					RaceID = gclass.gclass21_0.RaceID,
					SystemID = gclass.gclass178_0.int_0,
					OrbitBodyID = num2,
					MissileID = gclass.gclass120_0.int_0,
					MissileNum = gclass.int_2,
					LaunchTime = gclass.decimal_0,
					FireControlID = num3,
					FCNum = gclass.int_3,
					TargetID = gclass.int_0,
					TargetType = gclass.auroraContactType_0,
					MissileSpeed = gclass.double_9,
					ModifierToHit = gclass.double_8,
					Endurance = gclass.decimal_1,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					LastXcor = gclass.double_2,
					LastYcor = gclass.double_3,
					LastTargetX = gclass.double_4,
					LastTargetY = gclass.double_5,
					IncrementStartX = gclass.double_6,
					IncrementStartY = gclass.double_7,
					HomingMethod = gclass.genum61_0,
				};
				MissileSalvoStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_MissileSalvo WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in MissileSalvoStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_MissileSalvo (MissileSalvoID, GameID, ShipID, RaceID, SystemID, OrbitBodyID, MissileID, MissileNum, LaunchTime, FireControlID, FCNum, TargetID, TargetType, MissileSpeed, ModifierToHit, Endurance, Xcor, Ycor, LastXcor, LastYcor, LastTargetX, LastTargetY, IncrementStartX, IncrementStartY, HomingMethod ) \r\n                        VALUES ( @MissileSalvoID, @GameID, @ShipID, @RaceID, @SystemID, @OrbitBodyID, @MissileID, @MissileNum, @LaunchTime, @FireControlID, @FCNum, @TargetID, @TargetType, @MissileSpeed, @ModifierToHit, @Endurance, @Xcor, @Ycor, @LastXcor, @LastYcor, @LastTargetX, @LastTargetY, @IncrementStartX, @IncrementStartY, @HomingMethod )";
						sqliteCommand.Parameters.AddWithValue("@MissileSalvoID", dataObj.MissileSalvoID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ShipID", dataObj.ShipID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@OrbitBodyID", dataObj.OrbitBodyID);
						sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj.MissileID);
						sqliteCommand.Parameters.AddWithValue("@MissileNum", dataObj.MissileNum);
						sqliteCommand.Parameters.AddWithValue("@LaunchTime", dataObj.LaunchTime);
						sqliteCommand.Parameters.AddWithValue("@FireControlID", dataObj.FireControlID);
						sqliteCommand.Parameters.AddWithValue("@FCNum", dataObj.FCNum);
						sqliteCommand.Parameters.AddWithValue("@TargetID", dataObj.TargetID);
						sqliteCommand.Parameters.AddWithValue("@TargetType", dataObj.TargetType);
						sqliteCommand.Parameters.AddWithValue("@MissileSpeed", dataObj.MissileSpeed);
						sqliteCommand.Parameters.AddWithValue("@ModifierToHit", dataObj.ModifierToHit);
						sqliteCommand.Parameters.AddWithValue("@Endurance", dataObj.Endurance);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.Parameters.AddWithValue("@LastXcor", dataObj.LastXcor);
						sqliteCommand.Parameters.AddWithValue("@LastYcor", dataObj.LastYcor);
						sqliteCommand.Parameters.AddWithValue("@LastTargetX", dataObj.LastTargetX);
						sqliteCommand.Parameters.AddWithValue("@LastTargetY", dataObj.LastTargetY);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartX", dataObj.IncrementStartX);
						sqliteCommand.Parameters.AddWithValue("@IncrementStartY", dataObj.IncrementStartY);
						sqliteCommand.Parameters.AddWithValue("@HomingMethod", dataObj.HomingMethod);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1475);
			}
		}
	}

	/// <summary>
	/// method_100
	/// </summary>
	public class SaveMissiles
	{
		public struct MissilesData
		{
			public int MissileID;
			public string Name;
			public int MissileSeriesID;
			public int EngineID;
			public int NumEngines;
			public bool PrecursorMissile;
			public int MissilesRequired;
			public int MissilesAvailable;
			public decimal Cost;
			public decimal Size;
			public decimal Speed;
			public int RadDamage;
			public int FuelRequired;
			public decimal Endurance;
			public double MaxRange;
			public int WarheadStrength;
			public double SensorStrength;
			public int SensorResolution;
			public int SensorRange;
			public double ThermalStrength;
			public double EMStrength;
			public double EMSensitivity;
			public decimal GeoStrength;
			public decimal TotalFlightTime;
			public int ECM;
			public int MR;
			public int SecondStageID;
			public int NumSS;
			public int SeparationRange;
			public decimal Corbomite;
			public decimal Tritanium;
			public decimal Boronide;
			public decimal Uridium;
			public decimal Gallicite;
			public bool PreTNT;
			public decimal MSPReactor;
			public decimal MSPWarhead;
			public decimal MSPEngine;
			public decimal MSPFuel;
			public decimal MSPAgility;
			public decimal MSPActive;
			public decimal MSPThermal;
			public decimal MSPEM;
			public decimal MSPGeo;
			public int ECCM;
			public decimal GroundAP;
			public decimal GroundDamage;
			public decimal GroundBaseDamage;
			public decimal GroundShots;
			public decimal PowerMod;
		}
		MissilesData[] MissilesStore;


		public SaveMissiles(GClass0 game)
		{
			int i = 0;
			MissilesStore = new MissilesData[game.dictionary_5.Count];
			foreach (GClass120 gclass in game.dictionary_5.Values)
			{
				int num = 0;
				int num2 = 0;
				if (gclass.gclass206_0 != null)
				{
					num = gclass.gclass206_0.int_0;
				}
				if (gclass.gclass120_0 != null)
				{
					num2 = gclass.gclass120_0.int_0;
				}
				if (gclass.gclass206_0 != null)
				{
					num = gclass.gclass206_0.int_0;
				}
				if (gclass.gclass120_0 != null)
				{
					num2 = gclass.gclass120_0.int_0;
				}
				var dataObj = new MissilesData()
				{
					MissileID = gclass.int_0,
					Name = gclass.Name,
					MissileSeriesID = gclass.int_1,
					EngineID = num,
					NumEngines = gclass.int_12,
					PrecursorMissile = gclass.bool_0,
					MissilesRequired = gclass.int_2,
					MissilesAvailable = gclass.int_3,
					Cost = gclass.decimal_0,
					Size = gclass.decimal_1,
					Speed = gclass.decimal_2,
					RadDamage = gclass.int_4,
					FuelRequired = gclass.int_5,
					Endurance = gclass.decimal_17,
					MaxRange = gclass.double_5,
					WarheadStrength = gclass.int_6,
					SensorStrength = gclass.double_0,
					SensorResolution = gclass.int_7,
					SensorRange = gclass.int_8,
					ThermalStrength = gclass.double_1,
					EMStrength = gclass.double_2,
					EMSensitivity = gclass.double_3,
					GeoStrength = gclass.decimal_12,
					TotalFlightTime = gclass.decimal_18,
					ECM = gclass.int_9,
					MR = gclass.int_11,
					SecondStageID = num2,
					NumSS = gclass.int_14,
					SeparationRange = gclass.int_15,
					Corbomite = gclass.gclass114_0.decimal_2,
					Tritanium = gclass.gclass114_0.decimal_3,
					Boronide = gclass.gclass114_0.decimal_4,
					Uridium = gclass.gclass114_0.decimal_8,
					Gallicite = gclass.gclass114_0.decimal_10,
					PreTNT = gclass.bool_1,
					MSPReactor = gclass.decimal_3,
					MSPWarhead = gclass.decimal_4,
					MSPEngine = gclass.decimal_5,
					MSPFuel = gclass.decimal_6,
					MSPAgility = gclass.decimal_7,
					MSPActive = gclass.decimal_8,
					MSPThermal = gclass.decimal_9,
					MSPEM = gclass.decimal_10,
					MSPGeo = gclass.decimal_11,
					ECCM = gclass.int_10,
					GroundAP = gclass.decimal_13,
					GroundDamage = gclass.decimal_14,
					GroundBaseDamage = gclass.decimal_16,
					GroundShots = gclass.decimal_15,
					PowerMod = gclass.decimal_19,
				};
				MissilesStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Missiles WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in MissilesStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Missiles (MissileID, GameID,  Name, MissileSeriesID, EngineID, NumEngines, PrecursorMissile, MissilesRequired, MissilesAvailable, Cost, Size, Speed, RadDamage, FuelRequired, Endurance, MaxRange, WarheadStrength, SensorStrength, SensorResolution, SensorRange, ThermalStrength, EMStrength, EMSensitivity, GeoStrength, TotalFlightTime, \r\n                        ECM, MR, SecondStageID, NumSS, SeparationRange, Corbomite, Tritanium, Boronide, Uridium, Gallicite, PreTNT, MSPReactor, MSPWarhead, MSPEngine, MSPFuel, MSPAgility, MSPActive, MSPThermal, MSPEM, MSPGeo, ECCM, GroundAP, GroundDamage, GroundBaseDamage, GroundShots, PowerMod ) \r\n                        VALUES ( @MissileID, @GameID, @Name, @MissileSeriesID, @EngineID, @NumEngines, @PrecursorMissile, @MissilesRequired, @MissilesAvailable, @Cost, @Size, @Speed, @RadDamage, @FuelRequired, @Endurance, @MaxRange, @WarheadStrength, @SensorStrength, @SensorResolution, @SensorRange, @ThermalStrength, @EMStrength, @EMSensitivity, @GeoStrength, @TotalFlightTime, \r\n                        @ECM, @MR, @SecondStageID, @NumSS, @SeparationRange, @Corbomite, @Tritanium, @Boronide, @Uridium, @Gallicite, @PreTNT, @MSPReactor, @MSPWarhead, @MSPEngine, @MSPFuel, @MSPAgility, @MSPActive, @MSPThermal, @MSPEM, @MSPGeo, @ECCM, @GroundAP, @GroundDamage, @GroundBaseDamage, @GroundShots, @PowerMod )";
						sqliteCommand.Parameters.AddWithValue("@MissileID", dataObj.MissileID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@Name", dataObj.Name);
						sqliteCommand.Parameters.AddWithValue("@MissileSeriesID", dataObj.MissileSeriesID);
						sqliteCommand.Parameters.AddWithValue("@EngineID", dataObj.EngineID);
						sqliteCommand.Parameters.AddWithValue("@NumEngines", dataObj.NumEngines);
						sqliteCommand.Parameters.AddWithValue("@PrecursorMissile", dataObj.PrecursorMissile);
						sqliteCommand.Parameters.AddWithValue("@MissilesRequired", dataObj.MissilesRequired);
						sqliteCommand.Parameters.AddWithValue("@MissilesAvailable", dataObj.MissilesAvailable);
						sqliteCommand.Parameters.AddWithValue("@Cost", dataObj.Cost);
						sqliteCommand.Parameters.AddWithValue("@Size", dataObj.Size);
						sqliteCommand.Parameters.AddWithValue("@Speed", dataObj.Speed);
						sqliteCommand.Parameters.AddWithValue("@RadDamage", dataObj.RadDamage);
						sqliteCommand.Parameters.AddWithValue("@FuelRequired", dataObj.FuelRequired);
						sqliteCommand.Parameters.AddWithValue("@Endurance", dataObj.Endurance);
						sqliteCommand.Parameters.AddWithValue("@MaxRange", dataObj.MaxRange);
						sqliteCommand.Parameters.AddWithValue("@WarheadStrength", dataObj.WarheadStrength);
						sqliteCommand.Parameters.AddWithValue("@SensorStrength", dataObj.SensorStrength);
						sqliteCommand.Parameters.AddWithValue("@SensorResolution", dataObj.SensorResolution);
						sqliteCommand.Parameters.AddWithValue("@SensorRange", dataObj.SensorRange);
						sqliteCommand.Parameters.AddWithValue("@ThermalStrength", dataObj.ThermalStrength);
						sqliteCommand.Parameters.AddWithValue("@EMStrength", dataObj.EMStrength);
						sqliteCommand.Parameters.AddWithValue("@EMSensitivity", dataObj.EMSensitivity);
						sqliteCommand.Parameters.AddWithValue("@GeoStrength", dataObj.GeoStrength);
						sqliteCommand.Parameters.AddWithValue("@TotalFlightTime", dataObj.TotalFlightTime);
						sqliteCommand.Parameters.AddWithValue("@ECM", dataObj.ECM);
						sqliteCommand.Parameters.AddWithValue("@MR", dataObj.MR);
						sqliteCommand.Parameters.AddWithValue("@SecondStageID", dataObj.SecondStageID);
						sqliteCommand.Parameters.AddWithValue("@NumSS", dataObj.NumSS);
						sqliteCommand.Parameters.AddWithValue("@SeparationRange", dataObj.SeparationRange);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", dataObj.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", dataObj.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", dataObj.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Uridium", dataObj.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", dataObj.Gallicite);
						sqliteCommand.Parameters.AddWithValue("@PreTNT", dataObj.PreTNT);
						sqliteCommand.Parameters.AddWithValue("@MSPReactor", dataObj.MSPReactor);
						sqliteCommand.Parameters.AddWithValue("@MSPWarhead", dataObj.MSPWarhead);
						sqliteCommand.Parameters.AddWithValue("@MSPEngine", dataObj.MSPEngine);
						sqliteCommand.Parameters.AddWithValue("@MSPFuel", dataObj.MSPFuel);
						sqliteCommand.Parameters.AddWithValue("@MSPAgility", dataObj.MSPAgility);
						sqliteCommand.Parameters.AddWithValue("@MSPActive", dataObj.MSPActive);
						sqliteCommand.Parameters.AddWithValue("@MSPThermal", dataObj.MSPThermal);
						sqliteCommand.Parameters.AddWithValue("@MSPEM", dataObj.MSPEM);
						sqliteCommand.Parameters.AddWithValue("@MSPGeo", dataObj.MSPGeo);
						sqliteCommand.Parameters.AddWithValue("@ECCM", dataObj.ECCM);
						sqliteCommand.Parameters.AddWithValue("@GroundAP", dataObj.GroundAP);
						sqliteCommand.Parameters.AddWithValue("@GroundDamage", dataObj.GroundDamage);
						sqliteCommand.Parameters.AddWithValue("@GroundBaseDamage", dataObj.GroundBaseDamage);
						sqliteCommand.Parameters.AddWithValue("@GroundShots", dataObj.GroundShots);
						sqliteCommand.Parameters.AddWithValue("@PowerMod", dataObj.PowerMod);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1476);
			}
		}
	}


}


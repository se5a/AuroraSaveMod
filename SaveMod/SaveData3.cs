using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;

namespace Aurora
{

	/// <summary>
	/// method_101
	/// </summary>
	public class SaveAtmosphericGas
	{
		public struct AtmosphericGasData
		{
			public int SystemBodyID;
			public GEnum50 AtmosGasID;
			public double AtmosGasAmount;
			public double GasAtm;
			public bool FrozenOut;
		}
		AtmosphericGasData[] AtmosphericGasDataStore;


		public SaveAtmosphericGas(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_11.Values.SelectMany<GClass1, GClass200>((Func<GClass1, IEnumerable<GClass200>>)(x => (IEnumerable<GClass200>) x.list_0)).ToList<GClass200>();
			AtmosphericGasDataStore = new AtmosphericGasData[list.Count()];
			foreach(GClass200 gclass in list)
			{
				
				var dataObj = new AtmosphericGasData()
				{
					SystemBodyID = gclass.int_0,
					AtmosGasID = gclass.gclass199_0.genum50_0,
					AtmosGasAmount = gclass.double_0,
					GasAtm = gclass.double_1,
					FrozenOut = gclass.bool_0,
				};
				AtmosphericGasDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AtmosphericGas WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AtmosphericGasData in AtmosphericGasDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AtmosphericGas (GameID, SystemBodyID, AtmosGasID, AtmosGasAmount, GasAtm, FrozenOut ) VALUES ( @GameID, @SystemBodyID, @AtmosGasID, @AtmosGasAmount, @GasAtm, @FrozenOut )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@SystemBodyID", AtmosphericGasData.SystemBodyID);
						sqliteCommand.Parameters.AddWithValue("@AtmosGasID", AtmosphericGasData.AtmosGasID);
						sqliteCommand.Parameters.AddWithValue("@AtmosGasAmount", AtmosphericGasData.AtmosGasAmount);
						sqliteCommand.Parameters.AddWithValue("@GasAtm", AtmosphericGasData.GasAtm);
						sqliteCommand.Parameters.AddWithValue("@FrozenOut", AtmosphericGasData.FrozenOut);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1477);
			}
		}
	}	
		
	/// <summary>
	/// method_102
	/// </summary>
	public class SaveDamageControlQueue
	{
		public struct DamageControlQueueData
		{
			public int ComponentID;
			public int RepairOrder;
			public int ShipID;
		}
		DamageControlQueueData[] DamageControlQueueDataStore;


		public SaveDamageControlQueue(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_4.Values.SelectMany<GClass39, GClass26>((Func<GClass39, IEnumerable<GClass26>>)(x => (IEnumerable<GClass26>) x.list_13)).ToList<GClass26>();
			DamageControlQueueDataStore = new DamageControlQueueData[list.Count()];
			foreach(GClass26 gclass in list)
			{
				var dataObj = new DamageControlQueueData()
				{
					ComponentID = gclass.gclass206_0.int_0,
					RepairOrder = gclass.int_0,
					ShipID = gclass.gclass39_0.int_8,
				};
				DamageControlQueueDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_DamageControlQueue WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var DamageControlQueueData in DamageControlQueueDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_DamageControlQueue (ComponentID, GameID, RepairOrder, ShipID ) VALUES ( @ComponentID, @GameID, @RepairOrder, @ShipID )";
						sqliteCommand.Parameters.AddWithValue("@ComponentID", DamageControlQueueData.ComponentID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RepairOrder", DamageControlQueueData.RepairOrder);
						sqliteCommand.Parameters.AddWithValue("@ShipID", DamageControlQueueData.ShipID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1478);
			}
		}
	}	
	
	/// <summary>
	/// method_103
	/// </summary>
	public class SaveHullDescription
	{
		public struct HullDescriptionData
		{
			public int HullDescriptionID;
			public string Description;
			public string HullAbbr;
		}
		HullDescriptionData[] HullDescriptionDataStore;


		public SaveHullDescription(GClass0 game)
		{
			HullDescriptionDataStore = new HullDescriptionData[game.dictionary_22.Count()];
			int i = 0;
			foreach (GClass70 gclass in game.dictionary_22.Values)
			{
				var dataObj = new HullDescriptionData()
				{
					HullDescriptionID = gclass.int_0,
					Description = gclass.Description,
					HullAbbr = gclass.Abbreviation,
				};
				HullDescriptionDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_HullDescription", sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var HullDescriptionData in HullDescriptionDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_HullDescription (HullDescriptionID, Description, HullAbbr ) VALUES ( @HullDescriptionID, @Description, @HullAbbr )";
						sqliteCommand.Parameters.AddWithValue("@HullDescriptionID", HullDescriptionData.HullDescriptionID);
						sqliteCommand.Parameters.AddWithValue("@Description", HullDescriptionData.Description);
						sqliteCommand.Parameters.AddWithValue("@HullAbbr", HullDescriptionData.HullAbbr);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1479);
			}
		}
	}
	
	/// <summary>
	/// method_104
	/// </summary>
	public class SaveCommander
	{
		public struct CommanderData
		{
			public int CommanderID;
			public int RaceID;
			public int SpeciesID;
			public string Name;
			public GEnum43 ResSpecID;
			public AuroraCommanderType CommanderType;
			public string Title;
			public int RankID;
			public int PromotionScore;
			public int PopPromotionScore;
			public decimal GameTimePromoted;
			public decimal GameTimeAssigned;
			public int DoNotRelieve;
			public GEnum19 CommandType;
			public int CommandID;
			public int PopLocationID;
			public int TransportShipID;
			public int Seniority;
			public bool Retired;
			public bool DoNotPromote;
			public bool StoryCharacter;
			public int LifepodID;
			public string Orders;
			public string Notes;
			public int HomeworldID;
			public int POWRaceID;
			public decimal CareerStart;
			public bool Deceased;
			public int Loyalty;
			public int HealthRisk;
			public bool Female;
			public int KillTonnageCommercial;
			public int KillTonnageMilitary;
			public int EducationColony;
			public CommanderHistoryData[] CommanderHistoryStore;
			public CommanderMedalData[] CommanderMedalStore;
			public CommanderMeasurementData[] CommanderMeasurementStore;
			public CommanderBonusesData[] CommanderBonusesStore;
			public CommanderTraitsData[] CommanderTraitsStore;
		}
		CommanderData[] CommanderStore;

		public struct CommanderHistoryData
		{
			public int CommanderID;
			public string HistoryText;
			public decimal GameTime;
		}
		
		public struct CommanderMedalData
		{
			public int NumAwarded;
			public int MedalID;
			public int CommanderID;
			public string AwardReason;
		}

		public struct CommanderMeasurementData
		{
			public int CommanderID;
			public AuroraMeasurementType MeasurementType;
			public decimal Amount;
		}

		public struct CommanderBonusesData
		{
			public int CommanderID;
			public GEnum118 BonusID;
			public decimal BonusValue;
		}

		public struct CommanderTraitsData
		{
			public int CmdrID;
			public int TraitID;
		}
		
		public SaveCommander(GClass0 game)
		{

			
			int i = 0;
			CommanderStore = new CommanderData[game.dictionary_42.Count];
			foreach (GClass54 gclass in game.dictionary_42.Values)
			{
				CommanderHistoryData[] commanderHistoryStore = new CommanderHistoryData[gclass.list_0.Count];
				int j = 0;
				foreach (GClass158 gclass2 in gclass.list_0)
				{
					var dataObj1 = new CommanderHistoryData()
					{
						CommanderID = gclass.int_0,
						HistoryText = gclass2.Description,
						GameTime = gclass2.decimal_0,
					};
					commanderHistoryStore[j] = dataObj1;
					j++;
				}
				
				CommanderMedalData[] commanderMedalStore = new CommanderMedalData[gclass.dictionary_1.Count];
				j = 0;
				foreach (GClass52 gclass3 in gclass.dictionary_1.Values)
				{
					var dataObj2 = new CommanderMedalData()
					{
						NumAwarded = gclass3.int_0,
						MedalID = gclass3.gclass41_0.int_1,
						CommanderID = gclass.int_0,
						AwardReason = gclass3.string_0,
					};
					commanderMedalStore[j] = dataObj2;
					j++;
				}

				CommanderMeasurementData[] commanderMeasurementStore =
					new CommanderMeasurementData[gclass.dictionary_2.Count];
				j = 0;
				foreach (GClass53 gclass4 in gclass.dictionary_2.Values)
				{
					var dataObj3 = new CommanderMeasurementData()
					{
						CommanderID = gclass.int_0,
						MeasurementType = gclass4.auroraMeasurementType_0,
						Amount = gclass4.decimal_0,
					};
					commanderMeasurementStore[j] = dataObj3;
					j++;
				}

				CommanderBonusesData[] commanderBonusesStore = new CommanderBonusesData[gclass.dictionary_0.Count];
				j = 0;
				foreach (GClass51 gclass5 in gclass.dictionary_0.Values)
				{
					var dataObj4 = new CommanderBonusesData()
					{
						CommanderID = gclass.int_0,
						BonusID = gclass5.gclass49_0.genum118_0,
						BonusValue = gclass5.decimal_0,
					};
					commanderBonusesStore[j] = dataObj4;
					j++;
				}

				CommanderTraitsData[] commanderTraitsStore = new CommanderTraitsData[gclass.list_1.Count];
				j = 0;
				foreach (int num9 in gclass.list_1)
				{
					var dataObj5 = new CommanderTraitsData()
					{
						CmdrID = gclass.int_0,
						TraitID = num9,
					};
					commanderTraitsStore[j] = dataObj5;
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
				if (gclass.gclass132_0 != null)
				{
					num2 = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass132_2 != null)
				{
					num7 = gclass.gclass132_2.int_5;
				}
				if (gclass.gclass39_0 != null)
				{
					num3 = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass58_0 != null)
				{
					num4 = gclass.gclass58_0.int_0;
				}
				if (gclass.gclass1_0 != null)
				{
					num5 = gclass.gclass1_0.int_0;
				}
				if (gclass.gclass21_1 != null)
				{
					num6 = gclass.gclass21_1.RaceID;
				}
				if (gclass.gclass60_0 != null)
				{
					num8 = gclass.gclass60_0.int_0;
				}
				switch (gclass.genum19_0)
				{
					case GEnum19.const_1:
						num = gclass.gclass39_1.int_8;
						break;
					case GEnum19.const_2:
						num = gclass.gclass132_1.int_5;
						break;
					case GEnum19.const_3:
						num = gclass.gclass61_0.int_0;
						break;
					case GEnum19.const_4:
						num = gclass.gclass95_0.int_0;
						break;
					case GEnum19.const_5:
						num = gclass.gclass144_0.int_1;
						break;
					case GEnum19.const_6:
						num = gclass.gclass39_2.int_8;
						break;
					case GEnum19.const_7:
						num = gclass.gclass39_3.int_8;
						break;
					case GEnum19.const_8:
						num = gclass.gclass39_4.int_8;
						break;
					case GEnum19.const_9:
						num = gclass.gclass39_5.int_8;
						break;
					case GEnum19.const_10:
						num = gclass.gclass76_0.int_0;
						break;
					case GEnum19.const_11:
						num = gclass.gclass39_6.int_8;
						break;
					case GEnum19.const_12:
						num = gclass.gclass39_7.int_8;
						break;
					case GEnum19.const_13:
						num = gclass.gclass132_3.int_5;
						break;
				}
				if (gclass.gclass132_0 != null)
				{
					num2 = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass132_2 != null)
				{
					num7 = gclass.gclass132_2.int_5;
				}
				if (gclass.gclass39_0 != null)
				{
					num3 = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass58_0 != null)
				{
					num4 = gclass.gclass58_0.int_0;
				}
				if (gclass.gclass1_0 != null)
				{
					num5 = gclass.gclass1_0.int_0;
				}
				if (gclass.gclass21_1 != null)
				{
					num6 = gclass.gclass21_1.RaceID;
				}
				if (gclass.gclass60_0 != null)
				{
					num8 = gclass.gclass60_0.int_0;
				}
				switch (gclass.genum19_0)
				{
					case GEnum19.const_1:
					num = gclass.gclass39_1.int_8;
					break;
					case GEnum19.const_2:
					num = gclass.gclass132_1.int_5;
					break;
					case GEnum19.const_3:
					num = gclass.gclass61_0.int_0;
					break;
					case GEnum19.const_4:
					num = gclass.gclass95_0.int_0;
					break;
					case GEnum19.const_5:
					num = gclass.gclass144_0.int_1;
					break;
					case GEnum19.const_6:
					num = gclass.gclass39_2.int_8;
					break;
					case GEnum19.const_7:
					num = gclass.gclass39_3.int_8;
					break;
					case GEnum19.const_8:
					num = gclass.gclass39_4.int_8;
					break;
					case GEnum19.const_9:
					num = gclass.gclass39_5.int_8;
					break;
					case GEnum19.const_10:
					num = gclass.gclass76_0.int_0;
					break;
					case GEnum19.const_11:
					num = gclass.gclass39_6.int_8;
					break;
					case GEnum19.const_12:
					num = gclass.gclass39_7.int_8;
					break;
					case GEnum19.const_13:
					num = gclass.gclass132_3.int_5;
					break;
				}
				var dataObj = new CommanderData()
				{
					CommanderID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					SpeciesID = gclass.gclass172_0.int_0,
					Name = gclass.string_0,
					ResSpecID = gclass.gclass145_0.ResearchFieldID,
					CommanderType = gclass.auroraCommanderType_0,
					Title = gclass.string_1,
					RankID = num8,
					PromotionScore = gclass.int_1,
					PopPromotionScore = gclass.int_2,
					GameTimePromoted = gclass.decimal_0,
					GameTimeAssigned = gclass.decimal_1,
					DoNotRelieve = gclass.int_3,
					CommandType = gclass.genum19_0,
					CommandID = num,
					PopLocationID = num2,
					TransportShipID = num3,
					Seniority = gclass.int_4,
					Retired = gclass.bool_0,
					DoNotPromote = gclass.bool_2,
					StoryCharacter = gclass.bool_4,
					LifepodID = num4,
					Orders = gclass.string_2,
					Notes = gclass.string_3,
					HomeworldID = num5,
					POWRaceID = num6,
					CareerStart = gclass.decimal_2,
					Deceased = gclass.bool_1,
					Loyalty = gclass.int_5,
					HealthRisk = gclass.int_6,
					Female = gclass.bool_3,
					KillTonnageCommercial = gclass.int_7,
					KillTonnageMilitary = gclass.int_8,
					EducationColony = num7,
					CommanderHistoryStore = commanderHistoryStore,
					CommanderMedalStore = commanderMedalStore,
					CommanderMeasurementStore = commanderMeasurementStore,
					CommanderBonusesStore = commanderBonusesStore,
					CommanderTraitsStore = commanderTraitsStore,
				};
				CommanderStore[i] = dataObj;
				i++;
			}

		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Commander WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_CommanderHistory WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_CommanderMedal WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_CommanderMeasurement WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_CommanderBonuses WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_CommanderTraits WHERE GameID = " + gameID, sqliteConnection_0)
					.ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in CommanderStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Commander (CommanderID, GameID,  RaceID, SpeciesID, Name, ResSpecID, CommanderType, Title, RankID, PromotionScore, PopPromotionScore, GameTimePromoted, GameTimeAssigned, DoNotRelieve, CommandType, CommandID, PopLocationID, TransportShipID, Seniority, Retired, DoNotPromote, StoryCharacter, LifepodID, Orders, Notes, HomeworldID, \r\n                        POWRaceID, CareerStart, Deceased, Loyalty, HealthRisk, Female, KillTonnageCommercial, KillTonnageMilitary, EducationColony ) \r\n                        VALUES ( @CommanderID, @GameID, @RaceID, @SpeciesID, @Name, @ResSpecID, @CommanderType, @Title, @RankID, @PromotionScore, @PopPromotionScore, @GameTimePromoted, @GameTimeAssigned, @DoNotRelieve, @CommandType, @CommandID, @PopLocationID, @TransportShipID, @Seniority, @Retired, @DoNotPromote, @StoryCharacter, @LifepodID, @Orders, @Notes, @HomeworldID, \r\n                        @POWRaceID, @CareerStart, @Deceased, @Loyalty, @HealthRisk, @Female, @KillTonnageCommercial, @KillTonnageMilitary, @EducationColony )";
						sqliteCommand.Parameters.AddWithValue("@CommanderID", dataObj.CommanderID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@Name", dataObj.Name);
						sqliteCommand.Parameters.AddWithValue("@ResSpecID", dataObj.ResSpecID);
						sqliteCommand.Parameters.AddWithValue("@CommanderType", dataObj.CommanderType);
						sqliteCommand.Parameters.AddWithValue("@Title", dataObj.Title);
						sqliteCommand.Parameters.AddWithValue("@RankID", dataObj.RankID);
						sqliteCommand.Parameters.AddWithValue("@PromotionScore", dataObj.PromotionScore);
						sqliteCommand.Parameters.AddWithValue("@PopPromotionScore", dataObj.PopPromotionScore);
						sqliteCommand.Parameters.AddWithValue("@GameTimePromoted", dataObj.GameTimePromoted);
						sqliteCommand.Parameters.AddWithValue("@GameTimeAssigned", dataObj.GameTimeAssigned);
						sqliteCommand.Parameters.AddWithValue("@DoNotRelieve", dataObj.DoNotRelieve);
						sqliteCommand.Parameters.AddWithValue("@CommandType", dataObj.CommandType);
						sqliteCommand.Parameters.AddWithValue("@CommandID", dataObj.CommandID);
						sqliteCommand.Parameters.AddWithValue("@PopLocationID", dataObj.PopLocationID);
						sqliteCommand.Parameters.AddWithValue("@TransportShipID", dataObj.TransportShipID);
						sqliteCommand.Parameters.AddWithValue("@Seniority", dataObj.Seniority);
						sqliteCommand.Parameters.AddWithValue("@Retired", dataObj.Retired);
						sqliteCommand.Parameters.AddWithValue("@DoNotPromote", dataObj.DoNotPromote);
						sqliteCommand.Parameters.AddWithValue("@StoryCharacter", dataObj.StoryCharacter);
						sqliteCommand.Parameters.AddWithValue("@LifepodID", dataObj.LifepodID);
						sqliteCommand.Parameters.AddWithValue("@Orders", dataObj.Orders);
						sqliteCommand.Parameters.AddWithValue("@Notes", dataObj.Notes);
						sqliteCommand.Parameters.AddWithValue("@HomeworldID", dataObj.HomeworldID);
						sqliteCommand.Parameters.AddWithValue("@POWRaceID", dataObj.POWRaceID);
						sqliteCommand.Parameters.AddWithValue("@CareerStart", dataObj.CareerStart);
						sqliteCommand.Parameters.AddWithValue("@Deceased", dataObj.Deceased);
						sqliteCommand.Parameters.AddWithValue("@Loyalty", dataObj.Loyalty);
						sqliteCommand.Parameters.AddWithValue("@HealthRisk", dataObj.HealthRisk);
						sqliteCommand.Parameters.AddWithValue("@Female", dataObj.Female);
						sqliteCommand.Parameters.AddWithValue("@KillTonnageCommercial", dataObj.KillTonnageCommercial);
						sqliteCommand.Parameters.AddWithValue("@KillTonnageMilitary", dataObj.KillTonnageMilitary);
						sqliteCommand.Parameters.AddWithValue("@EducationColony", dataObj.EducationColony);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.CommanderHistoryStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_CommanderHistory ( GameID, CommanderID, HistoryText, GameTime ) VALUES ( @GameID, @CommanderID, @HistoryText, @GameTime )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@CommanderID", dataObj1.CommanderID);
							sqliteCommand.Parameters.AddWithValue("@HistoryText", dataObj1.HistoryText);
							sqliteCommand.Parameters.AddWithValue("@GameTime", dataObj1.GameTime);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj2 in dataObj.CommanderMedalStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_CommanderMedal ( NumAwarded, MedalID, CommanderID, GameID, AwardReason ) VALUES ( @NumAwarded, @MedalID, @CommanderID, @GameID, @AwardReason )";
							sqliteCommand.Parameters.AddWithValue("@NumAwarded", dataObj2.NumAwarded);
							sqliteCommand.Parameters.AddWithValue("@MedalID", dataObj2.MedalID);
							sqliteCommand.Parameters.AddWithValue("@CommanderID", dataObj2.CommanderID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@AwardReason", dataObj2.AwardReason);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj3 in dataObj.CommanderMeasurementStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_CommanderMeasurement ( CommanderID, MeasurementType, Amount, GameID ) VALUES ( @CommanderID, @MeasurementType, @Amount,  @GameID )";
							sqliteCommand.Parameters.AddWithValue("@CommanderID", dataObj3.CommanderID);
							sqliteCommand.Parameters.AddWithValue("@MeasurementType", dataObj3.MeasurementType);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj3.Amount);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj4 in dataObj.CommanderBonusesStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_CommanderBonuses ( GameID, CommanderID, BonusID, BonusValue ) VALUES ( @GameID, @CommanderID, @BonusID, @BonusValue )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@CommanderID", dataObj4.CommanderID);
							sqliteCommand.Parameters.AddWithValue("@BonusID", dataObj4.BonusID);
							sqliteCommand.Parameters.AddWithValue("@BonusValue", dataObj4.BonusValue);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj5 in dataObj.CommanderTraitsStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_CommanderTraits ( GameID, CmdrID, TraitID ) VALUES ( @GameID, @CmdrID, @TraitID )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@CmdrID", dataObj5.CmdrID);
							sqliteCommand.Parameters.AddWithValue("@TraitID", dataObj5.TraitID);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1480);
			}
		}
	}

	
	
	/// <summary>
	/// method_105
	/// </summary>
	public class SaveShipyard
	{
		public struct ShipyardData
		{
			public int ShipyardID;
			public int PopulationID;
			public AuroraShipyardType SYType;
			public string ShipyardName;
			public int Slipways;
			public decimal Capacity;
			public int  BuildClassID;
			public int  RetoolClassID;
			public AuroraShipyardUpgradeType TaskType;
			public decimal RequiredBP;
			public decimal CompletedBP;
			public bool PauseActivity;
			public int  DefaultFleetID;
			public double Xcor;
			public double Ycor;
			public int  RaceID;
			public int TractorParentShipID;
			public int CapacityTarget;
		}
		ShipyardData[] ShipyardDataStore;


		public SaveShipyard(GClass0 game)
		{
			ShipyardDataStore = new ShipyardData[game.dictionary_29.Count()];
			int i = 0;
			foreach (GClass171 gclass in game.dictionary_29.Values)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				if (gclass.gclass22_0 != null)
				{
					num = gclass.gclass22_0.int_0;
				}
				if (gclass.gclass22_1 != null)
				{
					num2 = gclass.gclass22_1.int_0;
				}
				if (gclass.gclass78_0 != null)
				{
					num3 = gclass.gclass78_0.int_3;
				}
				if (gclass.gclass39_0 != null)
				{
					num4 = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass132_0 != null)
				{
					num5 = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass22_0 != null)
				{
					num = gclass.gclass22_0.int_0;
				}
				if (gclass.gclass22_1 != null)
				{
					num2 = gclass.gclass22_1.int_0;
				}
				if (gclass.gclass78_0 != null)
				{
					num3 = gclass.gclass78_0.int_3;
				}
				if (gclass.gclass39_0 != null)
				{
					num4 = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass132_0 != null)
				{
					num5 = gclass.gclass132_0.int_5;
				}
				var dataObj = new ShipyardData()
				{
					ShipyardID = gclass.int_0,
					PopulationID = num5,
					SYType = gclass.auroraShipyardType_0,
					ShipyardName = gclass.string_0,
					Slipways = gclass.int_1,
					Capacity = gclass.decimal_0,
					BuildClassID = num,
					RetoolClassID = num2,
					TaskType = gclass.auroraShipyardUpgradeType_0,
					RequiredBP = gclass.decimal_1,
					CompletedBP = gclass.decimal_2,
					PauseActivity = gclass.bool_0,
					DefaultFleetID = num3,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					RaceID = gclass.gclass21_0.RaceID,
					TractorParentShipID = num4,
					CapacityTarget = gclass.int_2,
				};
				ShipyardDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Shipyard WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var ShipyardData in ShipyardDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Shipyard (ShipyardID, GameID, PopulationID, SYType, ShipyardName, Slipways, Capacity, BuildClassID, RetoolClassID, TaskType, RequiredBP, CompletedBP, PauseActivity, DefaultFleetID, Xcor, Ycor, RaceID, TractorParentShipID, CapacityTarget ) \r\n                        VALUES ( @ShipyardID, @GameID, @PopulationID, @SYType, @ShipyardName, @Slipways, @Capacity, @BuildClassID, @RetoolClassID, @TaskType, @RequiredBP, @CompletedBP, @PauseActivity, @DefaultFleetID, @Xcor, @Ycor, @RaceID, @TractorParentShipID, @CapacityTarget)";
						sqliteCommand.Parameters.AddWithValue("@ShipyardID", ShipyardData.ShipyardID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", ShipyardData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@SYType", ShipyardData.SYType);
						sqliteCommand.Parameters.AddWithValue("@ShipyardName", ShipyardData.ShipyardName);
						sqliteCommand.Parameters.AddWithValue("@Slipways", ShipyardData.Slipways);
						sqliteCommand.Parameters.AddWithValue("@Capacity", ShipyardData.Capacity);
						sqliteCommand.Parameters.AddWithValue("@BuildClassID", ShipyardData.BuildClassID);
						sqliteCommand.Parameters.AddWithValue("@RetoolClassID", ShipyardData.RetoolClassID);
						sqliteCommand.Parameters.AddWithValue("@TaskType", ShipyardData.TaskType);
						sqliteCommand.Parameters.AddWithValue("@RequiredBP", ShipyardData.RequiredBP);
						sqliteCommand.Parameters.AddWithValue("@CompletedBP", ShipyardData.CompletedBP);
						sqliteCommand.Parameters.AddWithValue("@PauseActivity", ShipyardData.PauseActivity);
						sqliteCommand.Parameters.AddWithValue("@DefaultFleetID", ShipyardData.DefaultFleetID);
						sqliteCommand.Parameters.AddWithValue("@Xcor", ShipyardData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", ShipyardData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@RaceID", ShipyardData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@TractorParentShipID", ShipyardData.TractorParentShipID);
						sqliteCommand.Parameters.AddWithValue("@CapacityTarget", ShipyardData.CapacityTarget);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1481);
			}
		}
	}
	
	/// <summary>
	/// method_106
	/// </summary>
	public class SaveShipyardTask
	{
		public struct ShipyardTaskData
		{
			public int TaskID;
			public int RaceID;
			public int PopulationID;
			public int ShipyardID;
			public AuroraSYTaskType TaskTypeID;
			public bool Freighter;
			public int  FleetID;
			public int  ShipID;
			public int  ClassID;
			public bool NPRShip;
			public int  RefitID;
			public decimal TotalBP;
			public decimal CompletedBP;
			public bool Paused;
			public string UnitName;
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
		ShipyardTaskData[] ShipyardTaskDataStore;


		public SaveShipyardTask(GClass0 game)
		{

			var list = game.dictionary_30.Values.Where(x => x != null).ToArray();
			int i = 0;
			ShipyardTaskDataStore = new ShipyardTaskData[list.Count()];
			foreach (GClass170 gclass in list)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (gclass.gclass39_0 != null)
				{
					num = gclass.gclass39_0.int_8;
				}
				if (gclass.gclass22_0 != null)
				{
					num2 = gclass.gclass22_0.int_0;
				}
				if (gclass.gclass22_1 != null)
				{
					num3 = gclass.gclass22_1.int_0;
				}
				if (gclass.gclass78_0 != null)
				{
					num4 = gclass.gclass78_0.int_3;
				}
				var dataObj = new ShipyardTaskData()
				{
					TaskID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					PopulationID = gclass.gclass132_0.int_5,
					ShipyardID = gclass.gclass171_0.int_0,
					TaskTypeID = gclass.auroraSYTaskType_0,
					Freighter = gclass.bool_0,
					FleetID = num4,
					ShipID = num,
					ClassID = num2,
					NPRShip = gclass.bool_1,
					RefitID = num3,
					TotalBP = gclass.decimal_0,
					CompletedBP = gclass.decimal_1,
					Paused = gclass.bool_2,
					UnitName = gclass.string_0,
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
				ShipyardTaskDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShipyardTask WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var ShipyardTaskData in ShipyardTaskDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_ShipyardTask (TaskID, GameID, RaceID, PopulationID, ShipyardID, TaskTypeID, Freighter, FleetID, ShipID, ClassID, NPRShip, RefitID, TotalBP, CompletedBP, Paused, UnitName, Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite ) \r\n                        VALUES ( @TaskID, @GameID, @RaceID, @PopulationID, @ShipyardID, @TaskTypeID, @Freighter, @FleetID, @ShipID, @ClassID, @NPRShip, @RefitID, @TotalBP, @CompletedBP, @Paused, @UnitName, @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite )";
						sqliteCommand.Parameters.AddWithValue("@TaskID", ShipyardTaskData.TaskID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", ShipyardTaskData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", ShipyardTaskData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@ShipyardID", ShipyardTaskData.ShipyardID);
						sqliteCommand.Parameters.AddWithValue("@TaskTypeID", ShipyardTaskData.TaskTypeID);
						sqliteCommand.Parameters.AddWithValue("@Freighter", ShipyardTaskData.Freighter);
						sqliteCommand.Parameters.AddWithValue("@FleetID", ShipyardTaskData.FleetID);
						sqliteCommand.Parameters.AddWithValue("@ShipID", ShipyardTaskData.ShipID);
						sqliteCommand.Parameters.AddWithValue("@ClassID", ShipyardTaskData.ClassID);
						sqliteCommand.Parameters.AddWithValue("@NPRShip", ShipyardTaskData.NPRShip);
						sqliteCommand.Parameters.AddWithValue("@RefitID", ShipyardTaskData.RefitID);
						sqliteCommand.Parameters.AddWithValue("@TotalBP", ShipyardTaskData.TotalBP);
						sqliteCommand.Parameters.AddWithValue("@CompletedBP", ShipyardTaskData.CompletedBP);
						sqliteCommand.Parameters.AddWithValue("@Paused", ShipyardTaskData.Paused);
						sqliteCommand.Parameters.AddWithValue("@UnitName", ShipyardTaskData.UnitName);
						sqliteCommand.Parameters.AddWithValue("@Duranium", ShipyardTaskData.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", ShipyardTaskData.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", ShipyardTaskData.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", ShipyardTaskData.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", ShipyardTaskData.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", ShipyardTaskData.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", ShipyardTaskData.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", ShipyardTaskData.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", ShipyardTaskData.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", ShipyardTaskData.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", ShipyardTaskData.Gallicite);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1482);
			}
		}
	}
	
	/// <summary>
	/// method_107
	/// </summary>
	public class SaveSpecies
	{
		public struct SpeciesData
		{
			public int SpeciesID;
			public int TechSystemID;
			public int HomeworldID;
			public int DerivedSpeciesID;
			public string SpeciesName;
			public string RacePic;
			public GEnum50 BreatheID;
			public double Oxygen;
			public double OxyDev;
			public double PressMax;
			public double Temperature;
			public double TempDev;
			public double Gravity;
			public double GravDev;
			public int Xenophobia;
			public int Diplomacy;
			public int Translation;
			public int Militancy;
			public int Expansionism;
			public int Determination;
			public int Trade;
			public decimal PopulationDensityModifier;
			public decimal PopulationGrowthModifier;
			public decimal ResearchRateModifier;
			public decimal ProductionRateModifier;
			public GEnum6 SpecialNPRID;
			public KnownSpeciesData[] KnownSpeciesStore;
		}
		SpeciesData[] SpeciesStore;

		public struct KnownSpeciesData
		{
			public int SpeciesID;
			public int ViewRaceID;
			public GEnum28 Status;
		}
		
		public SaveSpecies(GClass0 game)
		{
			int i = 0;
			SpeciesStore = new SpeciesData[game.dictionary_35.Count];
			foreach (GClass172 gclass in game.dictionary_35.Values)
			{

				KnownSpeciesData[] knownSpeciesStore = new KnownSpeciesData[gclass.dictionary_0.Count];
				int j = 0;
				foreach (GClass173 gclass2 in gclass.dictionary_0.Values)
				{
					var dataObj1 = new KnownSpeciesData()
					{
						SpeciesID = gclass.int_0,
						ViewRaceID = gclass2.gclass21_0.RaceID,
						Status = gclass2.genum28_0,
					};
					knownSpeciesStore[j] = dataObj1;
					j++;
				}
				
				int num = 0;
				int num2 = 0;
				if (gclass.gclass147_0 != null)
				{
					num = gclass.gclass147_0.int_0;
				}
				if (gclass.gclass1_0 != null)
				{
					num2 = gclass.gclass1_0.int_0;
				}
				if (gclass.gclass147_0 != null)
				{
					num = gclass.gclass147_0.int_0;
				}
				if (gclass.gclass1_0 != null)
				{
					num2 = gclass.gclass1_0.int_0;
				}
				var dataObj = new SpeciesData()
				{
					SpeciesID = gclass.int_0,
					TechSystemID = num,
					HomeworldID = num2,
					DerivedSpeciesID = gclass.int_1,
					SpeciesName = gclass.SpeciesName,
					RacePic = gclass.string_0,
					BreatheID = gclass.gclass199_0.genum50_0,
					Oxygen = gclass.double_0,
					OxyDev = gclass.double_1,
					PressMax = gclass.double_2,
					Temperature = gclass.double_3,
					TempDev = gclass.double_4,
					Gravity = gclass.double_5,
					GravDev = gclass.double_6,
					Xenophobia = gclass.int_2,
					Diplomacy = gclass.int_3,
					Translation = gclass.int_4,
					Militancy = gclass.int_5,
					Expansionism = gclass.int_6,
					Determination = gclass.int_7,
					Trade = gclass.int_8,
					PopulationDensityModifier = gclass.decimal_0,
					PopulationGrowthModifier = gclass.decimal_1,
					ResearchRateModifier = gclass.decimal_2,
					ProductionRateModifier = gclass.decimal_3,
					SpecialNPRID = gclass.genum6_0,
					KnownSpeciesStore = knownSpeciesStore,
				};
				SpeciesStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Species WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_KnownSpecies WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in SpeciesStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_Species (SpeciesID, GameID, TechSystemID, HomeworldID, DerivedSpeciesID, SpeciesName, RacePic, BreatheID, Oxygen, OxyDev, PressMax, Temperature, TempDev, Gravity, GravDev, Xenophobia, Diplomacy, Translation, Militancy, Expansionism, Determination, Trade, SpecialNPRID, PopulationDensityModifier, PopulationGrowthModifier, ResearchRateModifier, ProductionRateModifier ) \r\n                        VALUES ( @SpeciesID, @GameID, @TechSystemID, @HomeworldID, @DerivedSpeciesID, @SpeciesName, @RacePic, @BreatheID, @Oxygen, @OxyDev, @PressMax, @Temperature, @TempDev, @Gravity, @GravDev, @Xenophobia, @Diplomacy, @Translation, @Militancy, @Expansionism, @Determination, @Trade, @SpecialNPRID, @PopulationDensityModifier, @PopulationGrowthModifier, @ResearchRateModifier, @ProductionRateModifier)";
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@TechSystemID", dataObj.TechSystemID);
						sqliteCommand.Parameters.AddWithValue("@HomeworldID", dataObj.HomeworldID);
						sqliteCommand.Parameters.AddWithValue("@DerivedSpeciesID", dataObj.DerivedSpeciesID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesName", dataObj.SpeciesName);
						sqliteCommand.Parameters.AddWithValue("@RacePic", dataObj.RacePic);
						sqliteCommand.Parameters.AddWithValue("@BreatheID", dataObj.BreatheID);
						sqliteCommand.Parameters.AddWithValue("@Oxygen", dataObj.Oxygen);
						sqliteCommand.Parameters.AddWithValue("@OxyDev", dataObj.OxyDev);
						sqliteCommand.Parameters.AddWithValue("@PressMax", dataObj.PressMax);
						sqliteCommand.Parameters.AddWithValue("@Temperature", dataObj.Temperature);
						sqliteCommand.Parameters.AddWithValue("@TempDev", dataObj.TempDev);
						sqliteCommand.Parameters.AddWithValue("@Gravity", dataObj.Gravity);
						sqliteCommand.Parameters.AddWithValue("@GravDev", dataObj.GravDev);
						sqliteCommand.Parameters.AddWithValue("@Xenophobia", dataObj.Xenophobia);
						sqliteCommand.Parameters.AddWithValue("@Diplomacy", dataObj.Diplomacy);
						sqliteCommand.Parameters.AddWithValue("@Translation", dataObj.Translation);
						sqliteCommand.Parameters.AddWithValue("@Militancy", dataObj.Militancy);
						sqliteCommand.Parameters.AddWithValue("@Expansionism", dataObj.Expansionism);
						sqliteCommand.Parameters.AddWithValue("@Determination", dataObj.Determination);
						sqliteCommand.Parameters.AddWithValue("@Trade", dataObj.Trade);
						sqliteCommand.Parameters.AddWithValue("@PopulationDensityModifier",
							dataObj.PopulationDensityModifier);
						sqliteCommand.Parameters.AddWithValue("@PopulationGrowthModifier",
							dataObj.PopulationGrowthModifier);
						sqliteCommand.Parameters.AddWithValue("@ResearchRateModifier", dataObj.ResearchRateModifier);
						sqliteCommand.Parameters.AddWithValue("@ProductionRateModifier",
							dataObj.ProductionRateModifier);
						sqliteCommand.Parameters.AddWithValue("@SpecialNPRID", dataObj.SpecialNPRID);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.KnownSpeciesStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_KnownSpecies ( GameID, SpeciesID, ViewRaceID, Status ) VALUES ( @GameID, @SpeciesID, @ViewRaceID, @Status )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj1.SpeciesID);
							sqliteCommand.Parameters.AddWithValue("@ViewRaceID", dataObj1.ViewRaceID);
							sqliteCommand.Parameters.AddWithValue("@Status", dataObj1.Status);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1483);
			}
		}
	}
	
	/// <summary>
	/// method_108
	/// </summary>
	public class SaveWealthData
	{
		public struct WealthDataData
		{
			public int RaceID;
			public decimal Amount;
			public GEnum21 UseID;
			public decimal TimeUsed;
		}
		WealthDataData[] WealthDataDataStore;


		public SaveWealthData(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass137>((Func<GClass21, IEnumerable<GClass137>>)(x => (IEnumerable<GClass137>) x.list_3)).ToList<GClass137>();
			WealthDataDataStore = new WealthDataData[list.Count()];
			foreach(GClass137 gclass in list)
			{
				var dataObj = new WealthDataData()
				{
					RaceID = gclass.gclass21_0.RaceID,
					Amount = gclass.decimal_0,
					UseID = gclass.gclass136_0.genum21_0,
					TimeUsed = gclass.decimal_1,
				};
				WealthDataDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_WealthData WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var WealthDataData in WealthDataDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_WealthData (GameID, RaceID, Amount, UseID, TimeUsed ) VALUES ( @GameID, @RaceID, @Amount, @UseID, @TimeUsed )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", WealthDataData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@Amount", WealthDataData.Amount);
						sqliteCommand.Parameters.AddWithValue("@UseID", WealthDataData.UseID);
						sqliteCommand.Parameters.AddWithValue("@TimeUsed", WealthDataData.TimeUsed);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1484);
			}
		}
	}
	
	/// <summary>
	/// method_109
	/// </summary>
	public class SaveShippingLines
	{
		public struct ShippingLinesData
		{
			public int ShippingLineID;
			public int EmpireID;
			public bool NPRace;
			public string LineName;
			public string ShortName;
			public int ShipNum;
			public decimal WealthBalance;
			public decimal LastDividendPaid;
			public decimal LastDividendTime;
			public int CommEngineID;
			public int CommercialEngines;
		}
		ShippingLinesData[] ShippingLinesDataStore;


		public SaveShippingLines(GClass0 game)
		{
			ShippingLinesDataStore = new ShippingLinesData[game.dictionary_8.Count()];
			int i = 0;
			foreach (GClass165 gclass in game.dictionary_8.Values)
			{
				int num = 0;
				if (gclass.gclass206_0 != null)
				{
					num = gclass.gclass206_0.int_0;
				}
				if (gclass.gclass206_0 != null)
				{
					num = gclass.gclass206_0.int_0;
				}
				var dataObj = new ShippingLinesData()
				{
					ShippingLineID = gclass.int_0,
					EmpireID = gclass.gclass21_0.RaceID,
					NPRace = gclass.bool_0,
					LineName = gclass.string_0,
					ShortName = gclass.string_1,
					ShipNum = gclass.int_1,
					WealthBalance = gclass.decimal_0,
					LastDividendPaid = gclass.decimal_1,
					LastDividendTime = gclass.decimal_2,
					CommEngineID = num,
					CommercialEngines = gclass.int_2,
				};
				ShippingLinesDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShippingLines WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var ShippingLinesData in ShippingLinesDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_ShippingLines (ShippingLineID, GameID, EmpireID, NPRace, LineName, ShortName, ShipNum, WealthBalance, LastDividendPaid, LastDividendTime, CommEngineID, CommercialEngines ) \r\n                        VALUES ( @ShippingLineID, @GameID, @EmpireID, @NPRace, @LineName, @ShortName, @ShipNum, @WealthBalance, @LastDividendPaid, @LastDividendTime, @CommEngineID, @CommercialEngines )";
						sqliteCommand.Parameters.AddWithValue("@ShippingLineID", ShippingLinesData.ShippingLineID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@EmpireID", ShippingLinesData.EmpireID);
						sqliteCommand.Parameters.AddWithValue("@NPRace", ShippingLinesData.NPRace);
						sqliteCommand.Parameters.AddWithValue("@LineName", ShippingLinesData.LineName);
						sqliteCommand.Parameters.AddWithValue("@ShortName", ShippingLinesData.ShortName);
						sqliteCommand.Parameters.AddWithValue("@ShipNum", ShippingLinesData.ShipNum);
						sqliteCommand.Parameters.AddWithValue("@WealthBalance", ShippingLinesData.WealthBalance);
						sqliteCommand.Parameters.AddWithValue("@LastDividendPaid", ShippingLinesData.LastDividendPaid);
						sqliteCommand.Parameters.AddWithValue("@LastDividendTime", ShippingLinesData.LastDividendTime);
						sqliteCommand.Parameters.AddWithValue("@CommEngineID", ShippingLinesData.CommEngineID);
						sqliteCommand.Parameters.AddWithValue("@CommercialEngines", ShippingLinesData.CommercialEngines);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1485);
			}
		}
	}
	
	/// <summary>
	/// method_110
	/// </summary>
	public class SaveNavalAdminCommand
	{
		public struct NavalAdminCommandData
		{
			public int NavalAdminCommandID;
			public int RaceID;
			public int PopulationID;
			public int ParentAdminCommandID;
			public GEnum40 AdminCommandTypeID;
			public string AdminCommandName;
		}
		NavalAdminCommandData[] NavalAdminCommandDataStore;


		public SaveNavalAdminCommand(GClass0 game)
		{
			NavalAdminCommandDataStore = new NavalAdminCommandData[game.dictionary_0.Count()];
			int i = 0;
			foreach (GClass76 gclass in game.dictionary_0.Values)
			{
				int num = 0;
				int num2 = 0;
				if (gclass.gclass132_0 != null)
				{
					num = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass76_0 != null)
				{
					num2 = gclass.gclass76_0.int_0;
				}
				if (gclass.gclass132_0 != null)
				{
					num = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass76_0 != null)
				{
					num2 = gclass.gclass76_0.int_0;
				}
				var dataObj = new NavalAdminCommandData()
				{
					NavalAdminCommandID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					PopulationID = num,
					ParentAdminCommandID = num2,
					AdminCommandTypeID = gclass.gclass73_0.genum40_0,
					AdminCommandName = gclass.string_0,
				};
				NavalAdminCommandDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_NavalAdminCommand WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var NavalAdminCommandData in NavalAdminCommandDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_NavalAdminCommand (NavalAdminCommandID, GameID, RaceID, PopulationID, ParentAdminCommandID, AdminCommandTypeID, AdminCommandName ) VALUES ( @NavalAdminCommandID, @GameID, @RaceID, @PopulationID, @ParentAdminCommandID, @AdminCommandTypeID, @AdminCommandName )";
						sqliteCommand.Parameters.AddWithValue("@NavalAdminCommandID", NavalAdminCommandData.NavalAdminCommandID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", NavalAdminCommandData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", NavalAdminCommandData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@ParentAdminCommandID", NavalAdminCommandData.ParentAdminCommandID);
						sqliteCommand.Parameters.AddWithValue("@AdminCommandTypeID", NavalAdminCommandData.AdminCommandTypeID);
						sqliteCommand.Parameters.AddWithValue("@AdminCommandName", NavalAdminCommandData.AdminCommandName);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1486);
			}
		}
	}
	
	/// <summary>
	/// method_111
	/// </summary>
	public class SaveLifepods
	{
		public struct LifepodsData
		{
			public int LifepodID;
			public int RaceID;
			public int SpeciesID;
			public int SystemID;
			public int ClassID;
			public string ShipName;
			public int Crew;
			public double Xcor;
			public double Ycor;
			public decimal CreationTime;
			public decimal GradePoints;
		}
		LifepodsData[] LifepodsDataStore;


		public SaveLifepods(GClass0 game)
		{
			LifepodsDataStore = new LifepodsData[game.dictionary_28.Count()];
			int i = 0;
			foreach (GClass58 gclass in game.dictionary_28.Values)
			{
				int num = 0;
				if (gclass.gclass22_0 != null)
				{
					num = gclass.gclass22_0.int_0;
				}
				if (gclass.gclass22_0 != null)
				{
					num = gclass.gclass22_0.int_0;
				}
				var dataObj = new LifepodsData()
				{
					LifepodID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					SpeciesID = gclass.gclass172_0.int_0,
					SystemID = gclass.gclass178_0.int_0,
					ClassID = num,
					ShipName = gclass.string_0,
					Crew = gclass.int_1,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					CreationTime = gclass.decimal_0,
					GradePoints = gclass.decimal_1,
				};
				LifepodsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Lifepods WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var LifepodsData in LifepodsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Lifepods (LifepodID, GameID, RaceID, SpeciesID, SystemID, ClassID, ShipName, Crew, Xcor, Ycor, CreationTime, GradePoints ) VALUES ( @LifepodID, @GameID, @RaceID, @SpeciesID, @SystemID, @ClassID, @ShipName, @Crew, @Xcor, @Ycor, @CreationTime, @GradePoints )";
						sqliteCommand.Parameters.AddWithValue("@LifepodID", LifepodsData.LifepodID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", LifepodsData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SpeciesID", LifepodsData.SpeciesID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", LifepodsData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@ClassID", LifepodsData.ClassID);
						sqliteCommand.Parameters.AddWithValue("@ShipName", LifepodsData.ShipName);
						sqliteCommand.Parameters.AddWithValue("@Crew", LifepodsData.Crew);
						sqliteCommand.Parameters.AddWithValue("@Xcor", LifepodsData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", LifepodsData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@CreationTime", LifepodsData.CreationTime);
						sqliteCommand.Parameters.AddWithValue("@GradePoints", LifepodsData.GradePoints);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1488);
			}
		}
	}
    
	/// <summary>
	/// method_112
	/// </summary>
	public class SaveGroundUnitTraining
	{
		public struct GroundUnitTrainingData
		{
			public int TaskID;
			public int RaceID;
			public int PopulationID;
			public int FormationTemplateID;
			public decimal TotalBP;
			public decimal CompletedBP;
			public string FormationName;
		}
		GroundUnitTrainingData[] GroundUnitTrainingDataStore;


		public SaveGroundUnitTraining(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_20.Values.SelectMany<GClass132, GClass98>((Func<GClass132, IEnumerable<GClass98>>)(x => (IEnumerable<GClass98>) x.dictionary_3.Values)).ToList<GClass98>();
			GroundUnitTrainingDataStore = new GroundUnitTrainingData[list.Count()];
			foreach(GClass98 gclass in list)
			{
				var dataObj = new GroundUnitTrainingData()
				{
					TaskID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					PopulationID = gclass.gclass132_0.int_5,
					FormationTemplateID = gclass.gclass94_0.int_0,
					TotalBP = gclass.decimal_0,
					CompletedBP = gclass.decimal_1,
					FormationName = gclass.string_0,
				};
				GroundUnitTrainingDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GroundUnitTraining WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var GroundUnitTrainingData in GroundUnitTrainingDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_GroundUnitTraining (TaskID, GameID, RaceID, PopulationID, FormationTemplateID, TotalBP, CompletedBP, FormationName) VALUES ( @TaskID, @GameID, @RaceID, @PopulationID, @FormationTemplateID, @TotalBP, @CompletedBP, @FormationName )";
						sqliteCommand.Parameters.AddWithValue("@TaskID", GroundUnitTrainingData.TaskID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", GroundUnitTrainingData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", GroundUnitTrainingData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@FormationTemplateID", GroundUnitTrainingData.FormationTemplateID);
						sqliteCommand.Parameters.AddWithValue("@TotalBP", GroundUnitTrainingData.TotalBP);
						sqliteCommand.Parameters.AddWithValue("@CompletedBP", GroundUnitTrainingData.CompletedBP);
						sqliteCommand.Parameters.AddWithValue("@FormationName", GroundUnitTrainingData.FormationName);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1489);
			}
		}
	}
	
	/// <summary>
	/// method_113
	/// </summary>
	public class SaveGroundUnitTrainingQueue
	{
		public struct GroundUnitTrainingQueueData
		{
			public int QueueID;
			public int PopulationID;
			public int FormationTemplateID;
			public string FormationName;
		}
		GroundUnitTrainingQueueData[] GroundUnitTrainingQueueDataStore;


		public SaveGroundUnitTrainingQueue(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_20.Values.SelectMany<GClass132, GClass99>((Func<GClass132, IEnumerable<GClass99>>)(x => (IEnumerable<GClass99>) x.list_0)).ToList<GClass99>();
			GroundUnitTrainingQueueDataStore = new GroundUnitTrainingQueueData[list.Count()];
			foreach(GClass99 gclass in list)
			{
				var dataObj = new GroundUnitTrainingQueueData()
				{
					QueueID = gclass.int_0,
					PopulationID = gclass.gclass132_0.int_5,
					FormationTemplateID = gclass.gclass94_0.int_0,
					FormationName = gclass.string_0,
				};
				GroundUnitTrainingQueueDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GroundUnitTrainingQueue WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var GroundUnitTrainingQueueData in GroundUnitTrainingQueueDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_GroundUnitTrainingQueue (QueueID, GameID, PopulationID, FormationTemplateID, FormationName) VALUES ( @QueueID, @GameID, @PopulationID, @FormationTemplateID, @FormationName )";
						sqliteCommand.Parameters.AddWithValue("@QueueID", GroundUnitTrainingQueueData.QueueID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", GroundUnitTrainingQueueData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@FormationTemplateID", GroundUnitTrainingQueueData.FormationTemplateID);
						sqliteCommand.Parameters.AddWithValue("@FormationName", GroundUnitTrainingQueueData.FormationName);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1489);
			}
		}
	}
	
	/// <summary>
	/// method_114
	/// </summary>
	public class SaveResearchProject
	{
		public struct ResearchProjectData
		{
			public int ProjectID;
			public int TechID;
			public int RaceID;
			public int PopulationID;
			public int Facilities;
			public GEnum43 ResSpecID;
			public decimal ResearchPointsRequired;
			public bool Pause;
			public bool AssignNew;
		}
		ResearchProjectData[] ResearchProjectDataStore;


		public SaveResearchProject(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_20.Values.SelectMany<GClass132, GClass144>((Func<GClass132, IEnumerable<GClass144>>)(x => (IEnumerable<GClass144>) x.dictionary_1.Values)).ToList<GClass144>();
			ResearchProjectDataStore = new ResearchProjectData[list.Count()];
			foreach(GClass144 gclass in list)
			{
				var dataObj = new ResearchProjectData()
				{
					ProjectID = gclass.int_1,
					TechID = gclass.gclass147_0.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					PopulationID = gclass.gclass132_0.int_5,
					Facilities = gclass.int_0,
					ResSpecID = gclass.gclass145_0.ResearchFieldID,
					ResearchPointsRequired = gclass.decimal_0,
					Pause = gclass.bool_0,
					AssignNew = gclass.bool_1,
				};
				ResearchProjectDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ResearchProject WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var ResearchProjectData in ResearchProjectDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_ResearchProject (ProjectID, GameID, TechID, RaceID, PopulationID, Facilities, ResSpecID, ResearchPointsRequired, Pause, AssignNew) VALUES ( @ProjectID, @GameID, @TechID, @RaceID, @PopulationID, @Facilities, @ResSpecID, @ResearchPointsRequired, @Pause, @AssignNew )";
						sqliteCommand.Parameters.AddWithValue("@ProjectID", ResearchProjectData.ProjectID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@TechID", ResearchProjectData.TechID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", ResearchProjectData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", ResearchProjectData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@Facilities", ResearchProjectData.Facilities);
						sqliteCommand.Parameters.AddWithValue("@ResSpecID", ResearchProjectData.ResSpecID);
						sqliteCommand.Parameters.AddWithValue("@ResearchPointsRequired", ResearchProjectData.ResearchPointsRequired);
						sqliteCommand.Parameters.AddWithValue("@Pause", ResearchProjectData.Pause);
						sqliteCommand.Parameters.AddWithValue("@AssignNew", ResearchProjectData.AssignNew);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1490);
			}
		}
	}
	
	/// <summary>
	/// method_115
	/// </summary>
	public class SaveSectorCommand
	{
		public struct SectorCommandData
		{
			public int SectorCommandID;
			public int RaceID;
			public int PopulationID;
			public string SectorName;
			public int Colour;
		}
		SectorCommandData[] SectorCommandDataStore;


		public SaveSectorCommand(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass61>((Func<GClass21, IEnumerable<GClass61>>)(x => (IEnumerable<GClass61>) x.dictionary_2.Values)).ToList<GClass61>();
			SectorCommandDataStore = new SectorCommandData[list.Count()];
			foreach(GClass61 gclass in list)
			{
				var dataObj = new SectorCommandData()
				{
					SectorCommandID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					PopulationID = gclass.gclass132_0.int_5,
					SectorName = gclass.SectorName,
					Colour = gclass.color_0.ToArgb(),
				};
				SectorCommandDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_SectorCommand WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var SectorCommandData in SectorCommandDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_SectorCommand (SectorCommandID, RaceID, PopulationID, SectorName, Colour, GameID) VALUES ( @SectorCommandID, @RaceID, @PopulationID, @SectorName, @Colour, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@SectorCommandID", SectorCommandData.SectorCommandID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", SectorCommandData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", SectorCommandData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@SectorName", SectorCommandData.SectorName);
						sqliteCommand.Parameters.AddWithValue("@Colour", SectorCommandData.Colour);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1491);
			}
		}
	}
	
	/// <summary>
	/// method_116
	/// </summary>
	public class SaveAlienSystem
	{
		
		public struct AlienRaceData
		{
			public int AlienRaceID;
			public int ViewRaceID;
			public string AlienRaceName;
			public int FixedRelationship;
			public int ClassThemeID;
			public decimal FirstDetected;
			public AuroraContactStatus ContactStatus;
			public string Abbrev;
			public AuroraCommStatus CommStatus;
			public decimal CommModifier;
			public decimal CommEstablished;
			public decimal DiplomaticPoints;
			public double AlienRaceIntelligencePoints;
			public bool TradeTreaty;
			public bool TechTreaty;
			public bool GeoTreaty;
			public bool GravTreaty;
			public int RealClassNames;
			public AlienRaceSpeciesData[] AlienRaceSpeciesStore;
			public AlienSystemData[] AlienSystemStore;
			
		}
		AlienRaceData[] AlienRaceStore;

		public struct AlienRaceSpeciesData
		{
			public int AlienRaceID;
			public int SpeciesID;
			public int DetectRaceID;
		}
		
		public struct AlienSystemData
		{
			public int SystemID;
			public int AlienRaceID;
			public int DetectRaceID;
		}

		public SaveAlienSystem(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass102>((Func<GClass21, IEnumerable<GClass102>>)(x => (IEnumerable<GClass102>) x.dictionary_8.Values)).ToList<GClass102>();
			AlienRaceStore = new AlienRaceData[list.Count];
			foreach (GClass102 gclass in list)
			{
				AlienRaceSpeciesData[] alienRaceSpeciesStore = new AlienRaceSpeciesData[gclass.dictionary_0.Count];
				int j = 0;
				foreach (GClass172 gclass2 in gclass.dictionary_0.Values)
				{
					var dataObj0 = new AlienRaceSpeciesData()
					{
						AlienRaceID = gclass.int_0,
						SpeciesID = gclass2.int_0,
						DetectRaceID = gclass.gclass21_1.RaceID,
					};
					alienRaceSpeciesStore[j] = dataObj0;
					j++;
				}

				AlienSystemData[] alienSystemStore = new AlienSystemData[gclass.dictionary_1.Count];
				j = 0;
				foreach (GClass178 gclass3 in gclass.dictionary_1.Values)
				{
					var dataObj1 = new AlienSystemData()
					{
						SystemID = gclass3.int_0,
						AlienRaceID = gclass.int_0,
						DetectRaceID = gclass.gclass21_1.RaceID,
					};
					alienSystemStore[j] = dataObj1;
					j++;
				}
				
				int num = 0;
				if (gclass.gclass128_0 != null)
				{
					num = gclass.gclass128_0.int_0;
				}
				var dataObj = new AlienRaceData()
				{
					AlienRaceID = gclass.int_0,
					ViewRaceID = gclass.gclass21_1.RaceID,
					AlienRaceName = gclass.AlienRaceName,
					FixedRelationship = gclass.int_1,
					ClassThemeID = num,
					FirstDetected = gclass.decimal_0,
					ContactStatus = gclass.auroraContactStatus_0,
					Abbrev = gclass.string_0,
					CommStatus = gclass.auroraCommStatus_0,
					CommModifier = gclass.decimal_3,
					CommEstablished = gclass.decimal_1,
					DiplomaticPoints = gclass.decimal_2,
					AlienRaceIntelligencePoints = gclass.double_0,
					TradeTreaty = gclass.bool_0,
					TechTreaty = gclass.bool_1,
					GeoTreaty = gclass.bool_2,
					GravTreaty = gclass.bool_3,
					RealClassNames = gclass.int_2,
					AlienRaceSpeciesStore = alienRaceSpeciesStore,
					AlienSystemStore = alienSystemStore,
				};
				AlienRaceStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AlienRace WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_AlienRaceSpecies WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_AlienSystem WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in AlienRaceStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AlienRace (AlienRaceID, ViewRaceID, GameID, AlienRaceName, FixedRelationship, ClassThemeID, FirstDetected, ContactStatus, Abbrev, CommStatus, CommModifier, CommEstablished, DiplomaticPoints, AlienRaceIntelligencePoints, TradeTreaty, TechTreaty, GeoTreaty, GravTreaty, RealClassNames ) \r\n                        VALUES ( @AlienRaceID, @ViewRaceID, @GameID, @AlienRaceName, @FixedRelationship, @ClassThemeID, @FirstDetected, @ContactStatus, @Abbrev, @CommStatus, @CommModifier, @CommEstablished, @DiplomaticPoints, @AlienRaceIntelligencePoints, @TradeTreaty, @TechTreaty, @GeoTreaty, @GravTreaty, @RealClassNames )";
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", dataObj.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@ViewRaceID", dataObj.ViewRaceID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceName", dataObj.AlienRaceName);
						sqliteCommand.Parameters.AddWithValue("@FixedRelationship", dataObj.FixedRelationship);
						sqliteCommand.Parameters.AddWithValue("@ClassThemeID", dataObj.ClassThemeID);
						sqliteCommand.Parameters.AddWithValue("@FirstDetected", dataObj.FirstDetected);
						sqliteCommand.Parameters.AddWithValue("@ContactStatus", dataObj.ContactStatus);
						sqliteCommand.Parameters.AddWithValue("@Abbrev", dataObj.Abbrev);
						sqliteCommand.Parameters.AddWithValue("@CommStatus", dataObj.CommStatus);
						sqliteCommand.Parameters.AddWithValue("@CommModifier", dataObj.CommModifier);
						sqliteCommand.Parameters.AddWithValue("@CommEstablished", dataObj.CommEstablished);
						sqliteCommand.Parameters.AddWithValue("@DiplomaticPoints", dataObj.DiplomaticPoints);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceIntelligencePoints", dataObj.AlienRaceIntelligencePoints);
						sqliteCommand.Parameters.AddWithValue("@TradeTreaty", dataObj.TradeTreaty);
						sqliteCommand.Parameters.AddWithValue("@TechTreaty", dataObj.TechTreaty);
						sqliteCommand.Parameters.AddWithValue("@GeoTreaty", dataObj.GeoTreaty);
						sqliteCommand.Parameters.AddWithValue("@GravTreaty", dataObj.GravTreaty);
						sqliteCommand.Parameters.AddWithValue("@RealClassNames", dataObj.RealClassNames);
						sqliteCommand.ExecuteNonQuery();
						foreach (var dataObj0 in dataObj.AlienRaceSpeciesStore)
						{
							sqliteCommand.CommandText = "INSERT INTO FCT_AlienRaceSpecies (AlienRaceID, SpeciesID, DetectRaceID, GameID ) VALUES ( @AlienRaceID, @SpeciesID, @DetectRaceID, @GameID )";

							sqliteCommand.Parameters.AddWithValue("@AlienRaceID", dataObj0.AlienRaceID);
							sqliteCommand.Parameters.AddWithValue("@SpeciesID", dataObj0.SpeciesID);
							sqliteCommand.Parameters.AddWithValue("@DetectRaceID", dataObj0.DetectRaceID);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.ExecuteNonQuery();
						}
						foreach (var dataObj1 in dataObj.AlienSystemStore)
						{
							sqliteCommand.CommandText = "INSERT INTO FCT_AlienSystem (GameID, SystemID, AlienRaceID, DetectRaceID ) VALUES ( @GameID, @SystemID, @AlienRaceID, @DetectRaceID )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj1.SystemID);
							sqliteCommand.Parameters.AddWithValue("@AlienRaceID", dataObj1.AlienRaceID);
							sqliteCommand.Parameters.AddWithValue("@DetectRaceID", dataObj1.DetectRaceID);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1492);
			}
		}
	}

	/// <summary>
	/// method_117
	/// </summary>
	public class SaveAlienClass
	{
		public struct AlienClassData
		{
			public int AlienClassID;
			public int AlienRaceID;
			public int ActualClassID;
			public int ViewRaceID;
			public int HullID;
			public string ClassName;
			public decimal TCS;
			public decimal ThermalSignature;
			public decimal ShieldStrength;
			public decimal ShieldRecharge;
			public int ArmourStrength;
			public int MaxSpeed;
			public int JumpDistance;
			public int ECMStrength;
			public string Notes;
			public string Summary;
			public int ShipCount;
			public decimal FirstDetected;
			public int MaxEnergyPDShots;
			public int TotalEnergyPDShots;
			public int TotalEnergyPDHits;
			public GEnum60 AlienClassRole;
			public bool ObservedMissileDefence;
			public bool DiplomaticShip;
			public GEnum71 EngineType;
			public AlienClassSensorData[] AlienClassSensorStore;
			public AlienClassWeaponData[] AlienClassWeaponStore;
			public AlienClassTechData[] AlienClassTechStore;
		}
		AlienClassData[] AlienClassStore;

		public struct AlienClassSensorData
		{
			public int AlienClassID;
			public int AlienSensorID;
		}

		public struct AlienClassWeaponData
		{
			public int AlienClassID;
			public int Amount;
			public int ROF;
			public double Range;
			public int WeaponID;
			public decimal LastFired;
		}

		public struct AlienClassTechData
		{
			public int AlienClassID;
			public int TechID;
		}

		public SaveAlienClass(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass106>(
					(Func<GClass21, IEnumerable<GClass106>>)(x => (IEnumerable<GClass106>)x.dictionary_9.Values))
				.ToList<GClass106>();
			AlienClassStore = new AlienClassData[list.Count];
			foreach (GClass106 gclass in list)
			{

				AlienClassSensorData[] alienClassSensorStore = new AlienClassSensorData[gclass.list_0.Count];
				AlienClassWeaponData[] alienClassWeaponStore = new AlienClassWeaponData[gclass.list_1.Count];
				AlienClassTechData[] alienClassTechStore = new AlienClassTechData[gclass.list_2.Count];
				int j = 0;
				foreach (GClass109 gclass2 in gclass.list_0)
				{
					var dataObj1 = new AlienClassSensorData()
					{
						AlienClassID = gclass.int_0,
						AlienSensorID = gclass2.int_0,
					};
					alienClassSensorStore[j] = dataObj1;
					j++;
				}
				j = 0;
				foreach (GClass110 gclass3 in gclass.list_1)
				{
					var dataObj2 = new AlienClassWeaponData()
					{
						AlienClassID = gclass.int_0,
						Amount = gclass3.int_1,
						ROF = gclass3.int_0,
						Range = gclass3.double_0,
						WeaponID = gclass3.gclass206_0.int_0,
						LastFired = gclass3.decimal_0,
					};
					alienClassWeaponStore[j] = dataObj2;
					j++;
				}
				j = 0;
				foreach (GClass147 gclass4 in gclass.list_2)
				{
					var dataObj3 = new AlienClassTechData()
					{
						AlienClassID = gclass.int_0,
						TechID = gclass4.int_0,
					};
					alienClassTechStore[j] = dataObj3;
					j++;
				}


				var dataObj = new AlienClassData()
				{
					AlienClassID = gclass.int_0,
					AlienRaceID = gclass.gclass21_0.RaceID,
					ActualClassID = gclass.gclass22_0.int_0,
					ViewRaceID = gclass.gclass21_1.RaceID,
					HullID = gclass.gclass70_0.int_0,
					ClassName = gclass.ClassName,
					TCS = gclass.decimal_3,
					ThermalSignature = gclass.decimal_0,
					ShieldStrength = gclass.decimal_4,
					ShieldRecharge = gclass.decimal_1,
					ArmourStrength = gclass.int_1,
					MaxSpeed = gclass.int_2,
					JumpDistance = gclass.int_3,
					ECMStrength = gclass.int_4,
					Notes = gclass.string_0,
					Summary = gclass.string_1,
					ShipCount = gclass.int_5,
					FirstDetected = gclass.decimal_2,
					MaxEnergyPDShots = gclass.int_6,
					TotalEnergyPDShots = gclass.int_7,
					TotalEnergyPDHits = gclass.int_8,
					AlienClassRole = gclass.genum60_0,
					ObservedMissileDefence = gclass.bool_0,
					DiplomaticShip = gclass.bool_1,
					EngineType = gclass.genum71_0,
					AlienClassSensorStore = alienClassSensorStore,
					AlienClassTechStore = alienClassTechStore,
					AlienClassWeaponStore = alienClassWeaponStore,
				};
				AlienClassStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AlienClass WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_AlienClassSensor WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_AlienClassWeapon WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_AlienClassTech WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in AlienClassStore)
					{
						sqliteCommand.CommandText =
							"INSERT INTO FCT_AlienClass (AlienClassID, GameID, AlienRaceID, ActualClassID, ViewRaceID, HullID, ClassName, TCS, ThermalSignature, ShieldStrength, ShieldRecharge, ArmourStrength, MaxSpeed, JumpDistance, ECMStrength, Notes, Summary, ShipCount, FirstDetected, MaxEnergyPDShots, TotalEnergyPDShots, TotalEnergyPDHits, AlienClassRole, ObservedMissileDefence, DiplomaticShip, EngineType ) \r\n                        VALUES ( @AlienClassID, @GameID, @AlienRaceID, @ActualClassID, @ViewRaceID, @HullID, @ClassName, @TCS, @ThermalSignature, @ShieldStrength, @ShieldRecharge, @ArmourStrength, @MaxSpeed, @JumpDistance, @ECMStrength, @Notes, @Summary, @ShipCount, @FirstDetected, @MaxEnergyPDShots, @TotalEnergyPDShots, @TotalEnergyPDHits, @AlienClassRole, @ObservedMissileDefence, @DiplomaticShip, @EngineType )";
						sqliteCommand.Parameters.AddWithValue("@AlienClassID", dataObj.AlienClassID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", dataObj.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@ActualClassID", dataObj.ActualClassID);
						sqliteCommand.Parameters.AddWithValue("@ViewRaceID", dataObj.ViewRaceID);
						sqliteCommand.Parameters.AddWithValue("@HullID", dataObj.HullID);
						sqliteCommand.Parameters.AddWithValue("@ClassName", dataObj.ClassName);
						sqliteCommand.Parameters.AddWithValue("@TCS", dataObj.TCS);
						sqliteCommand.Parameters.AddWithValue("@ThermalSignature", dataObj.ThermalSignature);
						sqliteCommand.Parameters.AddWithValue("@ShieldStrength", dataObj.ShieldStrength);
						sqliteCommand.Parameters.AddWithValue("@ShieldRecharge", dataObj.ShieldRecharge);
						sqliteCommand.Parameters.AddWithValue("@ArmourStrength", dataObj.ArmourStrength);
						sqliteCommand.Parameters.AddWithValue("@MaxSpeed", dataObj.MaxSpeed);
						sqliteCommand.Parameters.AddWithValue("@JumpDistance", dataObj.JumpDistance);
						sqliteCommand.Parameters.AddWithValue("@ECMStrength", dataObj.ECMStrength);
						sqliteCommand.Parameters.AddWithValue("@Notes", dataObj.Notes);
						sqliteCommand.Parameters.AddWithValue("@Summary", dataObj.Summary);
						sqliteCommand.Parameters.AddWithValue("@ShipCount", dataObj.ShipCount);
						sqliteCommand.Parameters.AddWithValue("@FirstDetected", dataObj.FirstDetected);
						sqliteCommand.Parameters.AddWithValue("@MaxEnergyPDShots", dataObj.MaxEnergyPDShots);
						sqliteCommand.Parameters.AddWithValue("@TotalEnergyPDShots", dataObj.TotalEnergyPDShots);
						sqliteCommand.Parameters.AddWithValue("@TotalEnergyPDHits", dataObj.TotalEnergyPDHits);
						sqliteCommand.Parameters.AddWithValue("@AlienClassRole", dataObj.AlienClassRole);
						sqliteCommand.Parameters.AddWithValue("@ObservedMissileDefence",
							dataObj.ObservedMissileDefence);
						sqliteCommand.Parameters.AddWithValue("@DiplomaticShip", dataObj.DiplomaticShip);
						sqliteCommand.Parameters.AddWithValue("@EngineType", dataObj.EngineType);
						sqliteCommand.ExecuteNonQuery();

						foreach (var dataObj1 in dataObj.AlienClassSensorStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_AlienClassSensor (GameID, AlienClassID, AlienSensorID ) VALUES ( @GameID, @AlienClassID, @AlienSensorID )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@AlienClassID", dataObj1.AlienClassID);
							sqliteCommand.Parameters.AddWithValue("@AlienSensorID", dataObj1.AlienSensorID);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj2 in dataObj.AlienClassWeaponStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_AlienClassWeapon (AlienClassID, Amount, GameID, ROF, Range, WeaponID, LastFired) VALUES ( @AlienClassID, @Amount, @GameID, @ROF, @Range, @WeaponID, @LastFired )";
							sqliteCommand.Parameters.AddWithValue("@AlienClassID", dataObj2.AlienClassID);
							sqliteCommand.Parameters.AddWithValue("@Amount", dataObj2.Amount);
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@ROF", dataObj2.ROF);
							sqliteCommand.Parameters.AddWithValue("@Range", dataObj2.Range);
							sqliteCommand.Parameters.AddWithValue("@WeaponID", dataObj2.WeaponID);
							sqliteCommand.Parameters.AddWithValue("@LastFired", dataObj2.LastFired);
							sqliteCommand.ExecuteNonQuery();
						}

						foreach (var dataObj3 in dataObj.AlienClassTechStore)
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_AlienClassTech (GameID, AlienClassID, TechID ) VALUES ( @GameID, @AlienClassID, @TechID )";
							sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
							sqliteCommand.Parameters.AddWithValue("@AlienClassID", dataObj3.AlienClassID);
							sqliteCommand.Parameters.AddWithValue("@TechID", dataObj3.TechID);
							sqliteCommand.ExecuteNonQuery();
						}
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1493);
			}
		}
	}
	
	/// <summary>
	/// method_118
	/// </summary>
	public class SaveAlienRaceSensor
	{
		public struct AlienRaceSensorData
		{
			public int AlienSensorID;
			public decimal Strength;
			public int Resolution;
			public double Range;
			public double IntelligencePoints;
			public int ActualSensor;
			public int ActualMissile;
			public int ActualGroundUnitClass;
			public int AlienRaceID;
			public int ViewingRaceID;
			public string Name;
		}
		AlienRaceSensorData[] AlienRaceSensorDataStore;


		public SaveAlienRaceSensor(GClass0 game)
		{
			//          this.RacesList.Values.SelectMany<Race, AlienRace>((Func<Race, IEnumerable<AlienRace>>) (x => (IEnumerable<AlienRace>) x.AlienRaces.Values)).SelectMany<AlienRace, AlienRaceSensor>((Func<AlienRace, IEnumerable<AlienRaceSensor>>) (x => (IEnumerable<AlienRaceSensor>) x.KnownRaceSensors.Values)).Distinct<AlienRaceSensor>().ToList<AlienRaceSensor>())
			//Race GClass21
			//AlienRace Gclass102
			//Race.AlenRaces = Gclass21.dictionary_8
			//AlenRaceSensor Gclass109
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass102>((Func<GClass21, IEnumerable<GClass102>>)(x => (IEnumerable<GClass102>) x.dictionary_8.Values)).SelectMany<GClass102, GClass109>((Func<GClass102, IEnumerable<GClass109>>) (x => (IEnumerable<GClass109>) x.dictionary_2.Values)).Distinct<GClass109>().ToList<GClass109>();
			AlienRaceSensorDataStore = new AlienRaceSensorData[list.Count()];
			foreach(GClass109 gclass in list)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (gclass.gclass206_0 != null)
				{
					num = gclass.gclass206_0.int_0;
				}
				if (gclass.gclass120_0 != null)
				{
					num2 = gclass.gclass120_0.int_0;
				}
				if (gclass.gclass93_0 != null)
				{
					num3 = gclass.gclass93_0.int_0;
				}
				if (gclass.gclass206_0 != null)
				{
					num = gclass.gclass206_0.int_0;
				}
				if (gclass.gclass120_0 != null)
				{
					num2 = gclass.gclass120_0.int_0;
				}
				if (gclass.gclass93_0 != null)
				{
					num3 = gclass.gclass93_0.int_0;
				}
				var dataObj = new AlienRaceSensorData()
				{
					AlienSensorID = gclass.int_0,
					Strength = gclass.decimal_1,
					Resolution = gclass.int_1,
					Range = gclass.double_0,
					IntelligencePoints = gclass.double_1,
					ActualSensor = num,
					ActualMissile = num2,
					ActualGroundUnitClass = num3,
					AlienRaceID = gclass.gclass21_1.RaceID,
					ViewingRaceID = gclass.gclass21_0.RaceID,
					Name = gclass.string_0,
				};
				AlienRaceSensorDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AlienRaceSensor WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AlienRaceSensorData in AlienRaceSensorDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AlienRaceSensor (AlienSensorID, GameID, Strength, Resolution, Range, IntelligencePoints, ActualSensor, ActualMissile, ActualGroundUnitClass, AlienRaceID, ViewingRaceID, Name ) VALUES ( @AlienSensorID, @GameID, @Strength, @Resolution, @Range, @IntelligencePoints, @ActualSensor, @ActualMissile, @ActualGroundUnitClass, @AlienRaceID, @ViewingRaceID, @Name )";
						sqliteCommand.Parameters.AddWithValue("@AlienSensorID", AlienRaceSensorData.AlienSensorID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@Strength", AlienRaceSensorData.Strength);
						sqliteCommand.Parameters.AddWithValue("@Resolution", AlienRaceSensorData.Resolution);
						sqliteCommand.Parameters.AddWithValue("@Range", AlienRaceSensorData.Range);
						sqliteCommand.Parameters.AddWithValue("@IntelligencePoints", AlienRaceSensorData.IntelligencePoints);
						sqliteCommand.Parameters.AddWithValue("@ActualSensor", AlienRaceSensorData.ActualSensor);
						sqliteCommand.Parameters.AddWithValue("@ActualMissile", AlienRaceSensorData.ActualMissile);
						sqliteCommand.Parameters.AddWithValue("@ActualGroundUnitClass", AlienRaceSensorData.ActualGroundUnitClass);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", AlienRaceSensorData.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@ViewingRaceID", AlienRaceSensorData.ViewingRaceID);
						sqliteCommand.Parameters.AddWithValue("@Name", AlienRaceSensorData.Name);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1494);
			}
		}
	}
	
	/// <summary>
	/// method_119
	/// </summary>
	public class SaveAlienShip
	{
		public struct AlienShipData
		{
			public int AlienRaceID;
			public int AlienClassID;
			public int ViewRaceID;
			public int ShipID;
			public string Name;
			public int Speed;
			public double LastX;
			public double LastY;
			public int LastSysID;
			public decimal LastContactTime;
			public decimal FirstDetected;
			public bool Destroyed;
			public int DamageTaken;
			public int ArmourDamage;
			public int ShieldDamage;
			public int PenetratingDamage;
			public decimal GameTimeDamaged;
		}
		AlienShipData[] AlienShipDataStore;


		public SaveAlienShip(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass108>((Func<GClass21, IEnumerable<GClass108>>)(x => (IEnumerable<GClass108>) x.dictionary_10.Values)).ToList<GClass108>();
			AlienShipDataStore = new AlienShipData[list.Count()];
			foreach(GClass108 gclass in list)
			{
				var dataObj = new AlienShipData()
				{
					AlienRaceID = gclass.gclass21_0.RaceID,
					AlienClassID = gclass.gclass106_0.int_0,
					ViewRaceID = gclass.gclass21_1.RaceID,
					ShipID = gclass.int_0,
					Name = gclass.string_0,
					Speed = gclass.int_1,
					LastX = gclass.double_0,
					LastY = gclass.double_1,
					LastSysID = game.method_139(gclass.gclass178_0),
					LastContactTime = gclass.decimal_0,
					FirstDetected = gclass.decimal_1,
					Destroyed = gclass.bool_0,
					DamageTaken = gclass.int_2,
					ArmourDamage = gclass.int_3,
					ShieldDamage = gclass.int_4,
					PenetratingDamage = gclass.int_5,
					GameTimeDamaged = gclass.decimal_2,
				};
				AlienShipDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AlienShip WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AlienShipData in AlienShipDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AlienShip (GameID, AlienRaceID, AlienClassID, ViewRaceID, ShipID, Name, Speed, LastX, LastY, LastSysID, LastContactTime, FirstDetected, Destroyed, DamageTaken, GameTimeDamaged, ArmourDamage, ShieldDamage, PenetratingDamage ) \r\n                        VALUES ( @GameID, @AlienRaceID, @AlienClassID, @ViewRaceID, @ShipID, @Name, @Speed, @LastX, @LastY, @LastSysID, @LastContactTime, @FirstDetected, @Destroyed, @DamageTaken, @GameTimeDamaged, @ArmourDamage, @ShieldDamage, @PenetratingDamage )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", AlienShipData.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@AlienClassID", AlienShipData.AlienClassID);
						sqliteCommand.Parameters.AddWithValue("@ViewRaceID", AlienShipData.ViewRaceID);
						sqliteCommand.Parameters.AddWithValue("@ShipID", AlienShipData.ShipID);
						sqliteCommand.Parameters.AddWithValue("@Name", AlienShipData.Name);
						sqliteCommand.Parameters.AddWithValue("@Speed", AlienShipData.Speed);
						sqliteCommand.Parameters.AddWithValue("@LastX", AlienShipData.LastX);
						sqliteCommand.Parameters.AddWithValue("@LastY", AlienShipData.LastY);
						sqliteCommand.Parameters.AddWithValue("@LastSysID", AlienShipData.LastSysID);
						sqliteCommand.Parameters.AddWithValue("@LastContactTime", AlienShipData.LastContactTime);
						sqliteCommand.Parameters.AddWithValue("@FirstDetected", AlienShipData.FirstDetected);
						sqliteCommand.Parameters.AddWithValue("@Destroyed", AlienShipData.Destroyed);
						sqliteCommand.Parameters.AddWithValue("@DamageTaken", AlienShipData.DamageTaken);
						sqliteCommand.Parameters.AddWithValue("@ArmourDamage", AlienShipData.ArmourDamage);
						sqliteCommand.Parameters.AddWithValue("@ShieldDamage", AlienShipData.ShieldDamage);
						sqliteCommand.Parameters.AddWithValue("@PenetratingDamage", AlienShipData.PenetratingDamage);
						sqliteCommand.Parameters.AddWithValue("@GameTimeDamaged", AlienShipData.GameTimeDamaged);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1495);
			}
		}
	}
	
	/// <summary>
	/// method_120
	/// </summary>
	public class SaveAlienGroundUnitClass
	{
		public struct AlienGroundUnitClassData
		{
			public int AlienRaceID;
			public int ActualUnitClassID;
			public int ViewRaceID;
			public int AlienGroundUnitClassID;
			public string Name;
			public int Hits;
			public int Penetrated;
			public int Destroyed;
			public bool WeaponsKnown;
		}
		AlienGroundUnitClassData[] AlienGroundUnitClassDataStore;


		public SaveAlienGroundUnitClass(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass105>((Func<GClass21, IEnumerable<GClass105>>)(x => (IEnumerable<GClass105>) x.dictionary_12.Values)).ToList<GClass105>();
			AlienGroundUnitClassDataStore = new AlienGroundUnitClassData[list.Count()];
			foreach(GClass105 gclass in list)
			{
				int num = 0;
				if (gclass.gclass93_0 != null)
				{
					num = gclass.gclass93_0.int_0;
				}
				if (gclass.gclass93_0 != null)
				{
					num = gclass.gclass93_0.int_0;
				}
				var dataObj = new AlienGroundUnitClassData()
				{
					AlienRaceID = gclass.gclass21_1.RaceID,
					ActualUnitClassID = num,
					ViewRaceID = gclass.gclass21_0.RaceID,
					AlienGroundUnitClassID = gclass.int_0,
					Name = gclass.string_0,
					Hits = gclass.int_1,
					Penetrated = gclass.int_2,
					Destroyed = gclass.int_3,
					WeaponsKnown = gclass.bool_0,
				};
				AlienGroundUnitClassDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AlienGroundUnitClass WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AlienGroundUnitClassData in AlienGroundUnitClassDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AlienGroundUnitClass (GameID, AlienRaceID, ActualUnitClassID, ViewRaceID, AlienGroundUnitClassID, Name, Hits, Penetrated, Destroyed, WeaponsKnown ) \r\n                        VALUES ( @GameID, @AlienRaceID, @ActualUnitClassID, @ViewRaceID, @AlienGroundUnitClassID, @Name, @Hits, @Penetrated, @Destroyed, @WeaponsKnown )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", AlienGroundUnitClassData.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@ActualUnitClassID", AlienGroundUnitClassData.ActualUnitClassID);
						sqliteCommand.Parameters.AddWithValue("@ViewRaceID", AlienGroundUnitClassData.ViewRaceID);
						sqliteCommand.Parameters.AddWithValue("@AlienGroundUnitClassID", AlienGroundUnitClassData.AlienGroundUnitClassID);
						sqliteCommand.Parameters.AddWithValue("@Name", AlienGroundUnitClassData.Name);
						sqliteCommand.Parameters.AddWithValue("@Hits", AlienGroundUnitClassData.Hits);
						sqliteCommand.Parameters.AddWithValue("@Penetrated", AlienGroundUnitClassData.Penetrated);
						sqliteCommand.Parameters.AddWithValue("@Destroyed", AlienGroundUnitClassData.Destroyed);
						sqliteCommand.Parameters.AddWithValue("@WeaponsKnown", AlienGroundUnitClassData.WeaponsKnown);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1496);
			}
		}
	}
		
	/// <summary>
	/// method_121
	/// </summary>
	public class SaveAlienPopulation
	{
		public struct AlienPopulationData
		{
			public int AlienRaceID;
			public int PopulationID;
			public int ViewingRaceID;
			public int Installations;
			public int Mines;
			public int Factories;
			public int Refineries;
			public int ResearchFacilities;
			public int MaintenanceFacilities;
			public int GFTF;
			public bool Spaceport;
			public bool RefuellingStation;
			public bool NavalHeadquarters;
			public bool SectorCommand;
			public bool OrdnanceTransfer;
			public bool CargoStation;
			public decimal PopulationAmount;
			public double AlienPopulationIntelligencePoints;
			public double MaxIntelligence;
			public double PreviousMaxIntelligence;
			public string PopulationName;
			public decimal EMSignature;
			public decimal ThermalSignature;
		}
		AlienPopulationData[] AlienPopulationDataStore;


		public SaveAlienPopulation(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass104>((Func<GClass21, IEnumerable<GClass104>>)(x => (IEnumerable<GClass104>) x.dictionary_11.Values)).ToList<GClass104>();
			AlienPopulationDataStore = new AlienPopulationData[list.Count()];
			foreach(GClass104 gclass in list)
			{
				var dataObj = new AlienPopulationData()
				{
					AlienRaceID = gclass.gclass102_0.gclass21_0.RaceID,
					PopulationID = gclass.gclass132_0.int_5,
					ViewingRaceID = gclass.gclass21_0.RaceID,
					Installations = gclass.int_0,
					Mines = gclass.int_1,
					Factories = gclass.int_2,
					Refineries = gclass.int_3,
					ResearchFacilities = gclass.int_4,
					MaintenanceFacilities = gclass.int_5,
					GFTF = gclass.int_6,
					Spaceport = gclass.bool_0,
					RefuellingStation = gclass.bool_3,
					NavalHeadquarters = gclass.bool_1,
					SectorCommand = gclass.bool_2,
					OrdnanceTransfer = gclass.bool_4,
					CargoStation = gclass.bool_5,
					PopulationAmount = gclass.decimal_1,
					AlienPopulationIntelligencePoints = gclass.double_0,
					MaxIntelligence = gclass.double_1,
					PreviousMaxIntelligence = gclass.double_2,
					PopulationName = gclass.string_0,
					EMSignature = gclass.decimal_2,
					ThermalSignature = gclass.decimal_3,
				};
				AlienPopulationDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AlienPopulation WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AlienPopulationData in AlienPopulationDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AlienPopulation (GameID, AlienRaceID, ViewingRaceID, PopulationID, Installations, Mines, Factories, Refineries, ResearchFacilities, MaintenanceFacilities, GFTF, Spaceport, RefuellingStation, NavalHeadquarters, SectorCommand, OrdnanceTransfer, CargoStation, PopulationAmount, AlienPopulationIntelligencePoints, MaxIntelligence, PreviousMaxIntelligence, PopulationName, EMSignature, ThermalSignature ) \r\n                        VALUES ( @GameID, @AlienRaceID, @ViewingRaceID, @PopulationID, @Installations, @Mines, @Factories, @Refineries, @ResearchFacilities, @MaintenanceFacilities, @GFTF, @Spaceport, @RefuellingStation, @NavalHeadquarters, @SectorCommand, @OrdnanceTransfer, @CargoStation, @PopulationAmount, @AlienPopulationIntelligencePoints, @MaxIntelligence, @PreviousMaxIntelligence, @PopulationName, @EMSignature, @ThermalSignature )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", AlienPopulationData.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", AlienPopulationData.PopulationID);
						sqliteCommand.Parameters.AddWithValue("@ViewingRaceID", AlienPopulationData.ViewingRaceID);
						sqliteCommand.Parameters.AddWithValue("@Installations", AlienPopulationData.Installations);
						sqliteCommand.Parameters.AddWithValue("@Mines", AlienPopulationData.Mines);
						sqliteCommand.Parameters.AddWithValue("@Factories", AlienPopulationData.Factories);
						sqliteCommand.Parameters.AddWithValue("@Refineries", AlienPopulationData.Refineries);
						sqliteCommand.Parameters.AddWithValue("@ResearchFacilities", AlienPopulationData.ResearchFacilities);
						sqliteCommand.Parameters.AddWithValue("@MaintenanceFacilities", AlienPopulationData.MaintenanceFacilities);
						sqliteCommand.Parameters.AddWithValue("@GFTF", AlienPopulationData.GFTF);
						sqliteCommand.Parameters.AddWithValue("@Spaceport", AlienPopulationData.Spaceport);
						sqliteCommand.Parameters.AddWithValue("@RefuellingStation", AlienPopulationData.RefuellingStation);
						sqliteCommand.Parameters.AddWithValue("@NavalHeadquarters", AlienPopulationData.NavalHeadquarters);
						sqliteCommand.Parameters.AddWithValue("@SectorCommand", AlienPopulationData.SectorCommand);
						sqliteCommand.Parameters.AddWithValue("@OrdnanceTransfer", AlienPopulationData.OrdnanceTransfer);
						sqliteCommand.Parameters.AddWithValue("@CargoStation", AlienPopulationData.CargoStation);
						sqliteCommand.Parameters.AddWithValue("@PopulationAmount", AlienPopulationData.PopulationAmount);
						sqliteCommand.Parameters.AddWithValue("@AlienPopulationIntelligencePoints", AlienPopulationData.AlienPopulationIntelligencePoints);
						sqliteCommand.Parameters.AddWithValue("@MaxIntelligence", AlienPopulationData.MaxIntelligence);
						sqliteCommand.Parameters.AddWithValue("@PreviousMaxIntelligence", AlienPopulationData.PreviousMaxIntelligence);
						sqliteCommand.Parameters.AddWithValue("@PopulationName", AlienPopulationData.PopulationName);
						sqliteCommand.Parameters.AddWithValue("@EMSignature", AlienPopulationData.EMSignature);
						sqliteCommand.Parameters.AddWithValue("@ThermalSignature", AlienPopulationData.ThermalSignature);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1497);
			}
		}
	}		
	
	/// <summary>
	/// method_122
	/// </summary>
	public class SaveRaceTech
	{
		public struct RaceTechData
		{
			public int TechID;
			public int RaceID;
			public bool Obsolete;
		}
		RaceTechData[] RaceTechDataStore;


		public SaveRaceTech(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_50.Values.SelectMany<GClass147, GClass148>((Func<GClass147, IEnumerable<GClass148>>)(x => (IEnumerable<GClass148>) x.dictionary_0.Values)).ToList<GClass148>();
			RaceTechDataStore = new RaceTechData[list.Count()];
			foreach(GClass148 gclass in list)
			{
				var dataObj = new RaceTechData()
				{
					TechID = gclass.gclass147_0.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					Obsolete = gclass.bool_0,
				};
				RaceTechDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_RaceTech WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var RaceTechData in RaceTechDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_RaceTech (GameID, TechID, RaceID, Obsolete ) \r\n                        VALUES ( @GameID, @TechID, @RaceID, @Obsolete )";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@TechID", RaceTechData.TechID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", RaceTechData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@Obsolete", RaceTechData.Obsolete);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1498);
			}
		}
	}	

	/// <summary>
	/// method_123
	/// </summary>
	public class SaveTechSystem
	{
		public struct TechSystemData
		{
			public int TechSystemID;
			public int GameID;
			public string Name;
			public string ComponentName;
			public GEnum116 CategoryID;
			public int RaceID;
			public GEnum119 TechTypeID;
			public bool NoTechScan;
			public bool RuinOnly;
			public int Prerequisite1;
			public int Prerequisite2;
			public bool StartingSystem;
			public bool ConventionalSystem;
			public int DevelopCost;
			public decimal AdditionalInfo;
			public decimal AdditionalInfo2;
			public decimal AdditionalInfo3;
			public decimal AdditionalInfo4;
			public string TechDescription;
			public bool AutomaticResearch;
		}
		TechSystemData[] TechSystemStore;


		public SaveTechSystem(GClass0 game)
		{
			int i = 0;
			TechSystemStore = new TechSystemData[game.dictionary_50.Count];
			foreach (GClass147 gclass in game.dictionary_50.Values)
			{
				if (gclass.gclass21_0 != null)
				{
					gclass.int_1 = gclass.gclass21_0.RaceID;
				}
				var dataObj = new TechSystemData()
				{
					TechSystemID = gclass.int_0,
					GameID =  gclass.int_5,
					Name = gclass.Name,
					ComponentName = gclass.string_0,
					CategoryID = gclass.genum116_0,
					RaceID = gclass.int_1,
					TechTypeID = game.method_140(gclass.gclass146_0),
					NoTechScan = gclass.bool_0,
					RuinOnly = gclass.bool_1,
					Prerequisite1 = gclass.int_2,
					Prerequisite2 = gclass.int_3,
					StartingSystem = gclass.bool_2,
					ConventionalSystem = gclass.bool_3,
					DevelopCost = gclass.int_4,
					AdditionalInfo = gclass.decimal_0,
					AdditionalInfo2 = gclass.decimal_1,
					AdditionalInfo3 = gclass.decimal_2,
					AdditionalInfo4 = gclass.decimal_3,
					TechDescription = gclass.string_1,
					AutomaticResearch = gclass.bool_4,
				};
				TechSystemStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_TechSystem WHERE GameID = 0 OR GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in TechSystemStore)
					{
						try
						{
							sqliteCommand.CommandText =
								"INSERT INTO FCT_TechSystem (TechSystemID, GameID, Name, ComponentName, CategoryID, RaceID, TechTypeID, NoTechScan, RuinOnly, Prerequisite1, Prerequisite2, StartingSystem, ConventionalSystem, DevelopCost, AdditionalInfo, AdditionalInfo2, AdditionalInfo3, AdditionalInfo4, TechDescription, AutomaticResearch ) \r\n                        VALUES ( @TechSystemID, @GameID, @Name, @ComponentName, @CategoryID, @RaceID, @TechTypeID, @NoTechScan, @RuinOnly, @Prerequisite1, @Prerequisite2, @StartingSystem, @ConventionalSystem, @DevelopCost, @AdditionalInfo, @AdditionalInfo2, @AdditionalInfo3, @AdditionalInfo4, @TechDescription, @AutomaticResearch )";
							sqliteCommand.Parameters.AddWithValue("@TechSystemID", dataObj.TechSystemID);
							sqliteCommand.Parameters.AddWithValue("@GameID", dataObj.GameID);
							sqliteCommand.Parameters.AddWithValue("@Name", dataObj.Name);
							sqliteCommand.Parameters.AddWithValue("@ComponentName", dataObj.ComponentName);
							sqliteCommand.Parameters.AddWithValue("@CategoryID", dataObj.CategoryID);
							sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
							sqliteCommand.Parameters.AddWithValue("@TechTypeID", dataObj.TechTypeID);
							sqliteCommand.Parameters.AddWithValue("@NoTechScan", dataObj.NoTechScan);
							sqliteCommand.Parameters.AddWithValue("@RuinOnly", dataObj.RuinOnly);
							sqliteCommand.Parameters.AddWithValue("@Prerequisite1", dataObj.Prerequisite1);
							sqliteCommand.Parameters.AddWithValue("@Prerequisite2", dataObj.Prerequisite2);
							sqliteCommand.Parameters.AddWithValue("@StartingSystem", dataObj.StartingSystem);
							sqliteCommand.Parameters.AddWithValue("@ConventionalSystem", dataObj.ConventionalSystem);
							sqliteCommand.Parameters.AddWithValue("@DevelopCost", dataObj.DevelopCost);
							sqliteCommand.Parameters.AddWithValue("@AdditionalInfo", dataObj.AdditionalInfo);
							sqliteCommand.Parameters.AddWithValue("@AdditionalInfo2", dataObj.AdditionalInfo2);
							sqliteCommand.Parameters.AddWithValue("@AdditionalInfo3", dataObj.AdditionalInfo3);
							sqliteCommand.Parameters.AddWithValue("@AdditionalInfo4", dataObj.AdditionalInfo4);
							sqliteCommand.Parameters.AddWithValue("@TechDescription", dataObj.TechDescription);
							sqliteCommand.Parameters.AddWithValue("@AutomaticResearch", dataObj.AutomaticResearch);
							sqliteCommand.ExecuteNonQuery();


						}
						catch (Exception exception_)
						{
							GClass202.smethod_68(exception_, 3230);
							break;
						}
					}
				}
			}
			catch (Exception exception_2)
			{
				GClass202.smethod_68(exception_2, 1499);
			}
		}
	}
	
	/// <summary>
	/// method_124
	/// </summary>
	public class SaveShipDesignComponents
	{
		public struct ShipDesignComponentsData
		{
			public int SDComponentID;
			public int GameID;
			public string Name;
			public bool NoScrap;
			public bool MilitarySystem;
			public bool ShipyardRepairOnly;
			public bool ShippingLineSystem;
			public bool BeamWeapon;
			public int Crew;
			public decimal Size;
			public decimal Cost;
			public AuroraComponentType ComponentTypeID;
			public decimal ComponentValue;
			public decimal PowerRequirement;
			public decimal RechargeRate;
			public bool ElectronicSystem;
			public int ElectronicCTD;
			public int TrackingSpeed;
			public GEnum84 SpecialFunction;
			public decimal WeaponToHitModifier;
			public double MaxSensorRange;
			public decimal Resolution;
			public int HTK;
			public decimal FuelUse;
			public bool NoMaintFailure;
			public bool HangarReloadOnly;
			public decimal ExplosionChance;
			public int MaxExplosionSize;
			public int DamageOutput;
			public int NumberOfShots;
			public decimal RangeModifier;
			public int MaxWeaponRange;
			public bool SpinalWeapon;
			public int JumpDistance;
			public int JumpRating;
			public int RateOfFire;
			public int MaxPercentage;
			public decimal FuelEfficiency;
			public bool IgnoreShields;
			public bool IgnoreArmour;
			public bool ElectronicOnly;
			public decimal StealthRating;
			public decimal CloakRating;
			public bool Weapon;
			public int BGTech1;
			public int BGTech2;
			public int BGTech3;
			public int BGTech4;
			public int BGTech5;
			public int BGTech6;
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
			public bool SingleSystemOnly;
			public int ECCM;
			public decimal ArmourRetardation;
			public GEnum82 Prototype;
			public int TurretWeaponID;
		}
		ShipDesignComponentsData[] ShipDesignComponentsDataStore;


		public SaveShipDesignComponents(GClass0 game)
		{
			ShipDesignComponentsDataStore = new ShipDesignComponentsData[game.dictionary_52.Count()];
			int i = 0;
			foreach (GClass206 gclass in game.dictionary_52.Values)
			{
				var dataObj = new ShipDesignComponentsData()
				{
					SDComponentID = gclass.int_0,
					GameID = gclass.int_13,
					Name = gclass.Name,
					NoScrap = gclass.bool_0,
					MilitarySystem = gclass.bool_1,
					ShipyardRepairOnly = gclass.bool_2,
					ShippingLineSystem = gclass.bool_3,
					BeamWeapon = gclass.bool_4,
					Crew = gclass.int_1,
					Size = gclass.decimal_1,
					Cost = gclass.decimal_2,
					ComponentTypeID = gclass.gclass207_0.auroraComponentType_0,
					ComponentValue = gclass.decimal_3,
					PowerRequirement = gclass.decimal_0,
					RechargeRate = gclass.decimal_4,
					ElectronicSystem = gclass.bool_5,
					ElectronicCTD = gclass.int_2,
					TrackingSpeed = gclass.int_3,
					SpecialFunction = gclass.genum84_0,
					WeaponToHitModifier = gclass.decimal_5,
					MaxSensorRange = gclass.double_0,
					Resolution = gclass.decimal_6,
					HTK = gclass.int_4,
					FuelUse = gclass.decimal_7,
					NoMaintFailure = gclass.bool_6,
					HangarReloadOnly = gclass.bool_7,
					ExplosionChance = gclass.decimal_11,
					MaxExplosionSize = gclass.int_5,
					DamageOutput = gclass.int_6,
					NumberOfShots = gclass.int_7,
					RangeModifier = gclass.decimal_13,
					MaxWeaponRange = gclass.int_8,
					SpinalWeapon = gclass.bool_13,
					JumpDistance = gclass.int_9,
					JumpRating = gclass.int_10,
					RateOfFire = gclass.int_11,
					MaxPercentage = gclass.int_12,
					FuelEfficiency = gclass.decimal_8,
					IgnoreShields = gclass.bool_9,
					IgnoreArmour = gclass.bool_8,
					ElectronicOnly = gclass.bool_10,
					StealthRating = gclass.decimal_9,
					CloakRating = gclass.decimal_10,
					Weapon = gclass.bool_11,
					BGTech1 = gclass.int_16,
					BGTech2 = gclass.int_17,
					BGTech3 = gclass.int_18,
					BGTech4 = gclass.int_19,
					BGTech5 = gclass.int_20,
					BGTech6 = gclass.int_21,
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
					SingleSystemOnly = gclass.bool_12,
					ECCM = gclass.int_14,
					ArmourRetardation = gclass.decimal_12,
					Prototype = gclass.genum82_0,
					TurretWeaponID = gclass.int_15,
				};
				ShipDesignComponentsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_ShipDesignComponents WHERE GameID = 0 OR GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var ShipDesignComponentsData in ShipDesignComponentsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_ShipDesignComponents (SDComponentID, GameID, Name, NoScrap, MilitarySystem, ShipyardRepairOnly, ShippingLineSystem, BeamWeapon, Crew, Size, Cost, ComponentTypeID, ComponentValue, PowerRequirement, RechargeRate, ElectronicSystem, ElectronicCTD, TrackingSpeed, SpecialFunction, WeaponToHitModifier, MaxSensorRange, Resolution, HTK, FuelUse, NoMaintFailure, HangarReloadOnly,\r\n                        ExplosionChance, MaxExplosionSize, DamageOutput, NumberOfShots, RangeModifier, MaxWeaponRange, SpinalWeapon, JumpDistance, JumpRating, RateOfFire, MaxPercentage, FuelEfficiency, IgnoreShields, IgnoreArmour, ElectronicOnly, StealthRating, CloakRating, Weapon, BGTech1, BGTech2, BGTech3, BGTech4, BGTech5, BGTech6, \r\n                        Duranium, Neutronium, Corbomite, Tritanium, Boronide, Mercassium, Vendarite, Sorium, Uridium, Corundium, Gallicite, SingleSystemOnly, ECCM, ArmourRetardation, Prototype, TurretWeaponID ) \r\n                        VALUES ( @SDComponentID, @GameID, @Name, @NoScrap, @MilitarySystem, @ShipyardRepairOnly, @ShippingLineSystem, @BeamWeapon, @Crew, @Size, @Cost, @ComponentTypeID, @ComponentValue, @PowerRequirement, @RechargeRate, @ElectronicSystem, @ElectronicCTD, @TrackingSpeed, @SpecialFunction, @WeaponToHitModifier, @MaxSensorRange, @Resolution, @HTK, @FuelUse, @NoMaintFailure, @HangarReloadOnly,\r\n                        @ExplosionChance, @MaxExplosionSize, @DamageOutput, @NumberOfShots, @RangeModifier, @MaxWeaponRange, @SpinalWeapon, @JumpDistance, @JumpRating, @RateOfFire, @MaxPercentage, @FuelEfficiency, @IgnoreShields, @IgnoreArmour, @ElectronicOnly, @StealthRating, @CloakRating, @Weapon, @BGTech1, @BGTech2, @BGTech3, @BGTech4, @BGTech5, @BGTech6, \r\n                        @Duranium, @Neutronium, @Corbomite, @Tritanium, @Boronide, @Mercassium, @Vendarite, @Sorium, @Uridium, @Corundium, @Gallicite, @SingleSystemOnly, @ECCM, @ArmourRetardation, @Prototype, @TurretWeaponID )";
						sqliteCommand.Parameters.AddWithValue("@SDComponentID", ShipDesignComponentsData.SDComponentID);
						sqliteCommand.Parameters.AddWithValue("@GameID", ShipDesignComponentsData.GameID);
						sqliteCommand.Parameters.AddWithValue("@Name", ShipDesignComponentsData.Name);
						sqliteCommand.Parameters.AddWithValue("@NoScrap", ShipDesignComponentsData.NoScrap);
						sqliteCommand.Parameters.AddWithValue("@MilitarySystem", ShipDesignComponentsData.MilitarySystem);
						sqliteCommand.Parameters.AddWithValue("@ShipyardRepairOnly", ShipDesignComponentsData.ShipyardRepairOnly);
						sqliteCommand.Parameters.AddWithValue("@ShippingLineSystem", ShipDesignComponentsData.ShippingLineSystem);
						sqliteCommand.Parameters.AddWithValue("@BeamWeapon", ShipDesignComponentsData.BeamWeapon);
						sqliteCommand.Parameters.AddWithValue("@Crew", ShipDesignComponentsData.Crew);
						sqliteCommand.Parameters.AddWithValue("@Size", ShipDesignComponentsData.Size);
						sqliteCommand.Parameters.AddWithValue("@Cost", ShipDesignComponentsData.Cost);
						sqliteCommand.Parameters.AddWithValue("@ComponentTypeID", ShipDesignComponentsData.ComponentTypeID);
						sqliteCommand.Parameters.AddWithValue("@ComponentValue", ShipDesignComponentsData.ComponentValue);
						sqliteCommand.Parameters.AddWithValue("@PowerRequirement", ShipDesignComponentsData.PowerRequirement);
						sqliteCommand.Parameters.AddWithValue("@RechargeRate", ShipDesignComponentsData.RechargeRate);
						sqliteCommand.Parameters.AddWithValue("@ElectronicSystem", ShipDesignComponentsData.ElectronicSystem);
						sqliteCommand.Parameters.AddWithValue("@ElectronicCTD", ShipDesignComponentsData.ElectronicCTD);
						sqliteCommand.Parameters.AddWithValue("@TrackingSpeed", ShipDesignComponentsData.TrackingSpeed);
						sqliteCommand.Parameters.AddWithValue("@SpecialFunction", ShipDesignComponentsData.SpecialFunction);
						sqliteCommand.Parameters.AddWithValue("@WeaponToHitModifier", ShipDesignComponentsData.WeaponToHitModifier);
						sqliteCommand.Parameters.AddWithValue("@MaxSensorRange", ShipDesignComponentsData.MaxSensorRange);
						sqliteCommand.Parameters.AddWithValue("@Resolution", ShipDesignComponentsData.Resolution);
						sqliteCommand.Parameters.AddWithValue("@HTK", ShipDesignComponentsData.HTK);
						sqliteCommand.Parameters.AddWithValue("@FuelUse", ShipDesignComponentsData.FuelUse);
						sqliteCommand.Parameters.AddWithValue("@NoMaintFailure", ShipDesignComponentsData.NoMaintFailure);
						sqliteCommand.Parameters.AddWithValue("@HangarReloadOnly", ShipDesignComponentsData.HangarReloadOnly);
						sqliteCommand.Parameters.AddWithValue("@ExplosionChance", ShipDesignComponentsData.ExplosionChance);
						sqliteCommand.Parameters.AddWithValue("@MaxExplosionSize", ShipDesignComponentsData.MaxExplosionSize);
						sqliteCommand.Parameters.AddWithValue("@DamageOutput", ShipDesignComponentsData.DamageOutput);
						sqliteCommand.Parameters.AddWithValue("@NumberOfShots", ShipDesignComponentsData.NumberOfShots);
						sqliteCommand.Parameters.AddWithValue("@RangeModifier", ShipDesignComponentsData.RangeModifier);
						sqliteCommand.Parameters.AddWithValue("@MaxWeaponRange", ShipDesignComponentsData.MaxWeaponRange);
						sqliteCommand.Parameters.AddWithValue("@SpinalWeapon", ShipDesignComponentsData.SpinalWeapon);
						sqliteCommand.Parameters.AddWithValue("@JumpDistance", ShipDesignComponentsData.JumpDistance);
						sqliteCommand.Parameters.AddWithValue("@JumpRating", ShipDesignComponentsData.JumpRating);
						sqliteCommand.Parameters.AddWithValue("@RateOfFire", ShipDesignComponentsData.RateOfFire);
						sqliteCommand.Parameters.AddWithValue("@MaxPercentage", ShipDesignComponentsData.MaxPercentage);
						sqliteCommand.Parameters.AddWithValue("@FuelEfficiency", ShipDesignComponentsData.FuelEfficiency);
						sqliteCommand.Parameters.AddWithValue("@IgnoreShields", ShipDesignComponentsData.IgnoreShields);
						sqliteCommand.Parameters.AddWithValue("@IgnoreArmour", ShipDesignComponentsData.IgnoreArmour);
						sqliteCommand.Parameters.AddWithValue("@ElectronicOnly", ShipDesignComponentsData.ElectronicOnly);
						sqliteCommand.Parameters.AddWithValue("@StealthRating", ShipDesignComponentsData.StealthRating);
						sqliteCommand.Parameters.AddWithValue("@CloakRating", ShipDesignComponentsData.CloakRating);
						sqliteCommand.Parameters.AddWithValue("@Weapon", ShipDesignComponentsData.Weapon);
						sqliteCommand.Parameters.AddWithValue("@BGTech1", ShipDesignComponentsData.BGTech1);
						sqliteCommand.Parameters.AddWithValue("@BGTech2", ShipDesignComponentsData.BGTech2);
						sqliteCommand.Parameters.AddWithValue("@BGTech3", ShipDesignComponentsData.BGTech3);
						sqliteCommand.Parameters.AddWithValue("@BGTech4", ShipDesignComponentsData.BGTech4);
						sqliteCommand.Parameters.AddWithValue("@BGTech5", ShipDesignComponentsData.BGTech5);
						sqliteCommand.Parameters.AddWithValue("@BGTech6", ShipDesignComponentsData.BGTech6);
						sqliteCommand.Parameters.AddWithValue("@Duranium", ShipDesignComponentsData.Duranium);
						sqliteCommand.Parameters.AddWithValue("@Neutronium", ShipDesignComponentsData.Neutronium);
						sqliteCommand.Parameters.AddWithValue("@Corbomite", ShipDesignComponentsData.Corbomite);
						sqliteCommand.Parameters.AddWithValue("@Tritanium", ShipDesignComponentsData.Tritanium);
						sqliteCommand.Parameters.AddWithValue("@Boronide", ShipDesignComponentsData.Boronide);
						sqliteCommand.Parameters.AddWithValue("@Mercassium", ShipDesignComponentsData.Mercassium);
						sqliteCommand.Parameters.AddWithValue("@Vendarite", ShipDesignComponentsData.Vendarite);
						sqliteCommand.Parameters.AddWithValue("@Sorium", ShipDesignComponentsData.Sorium);
						sqliteCommand.Parameters.AddWithValue("@Uridium", ShipDesignComponentsData.Uridium);
						sqliteCommand.Parameters.AddWithValue("@Corundium", ShipDesignComponentsData.Corundium);
						sqliteCommand.Parameters.AddWithValue("@Gallicite", ShipDesignComponentsData.Gallicite);
						sqliteCommand.Parameters.AddWithValue("@SingleSystemOnly", ShipDesignComponentsData.SingleSystemOnly);
						sqliteCommand.Parameters.AddWithValue("@ECCM", ShipDesignComponentsData.ECCM);
						sqliteCommand.Parameters.AddWithValue("@ArmourRetardation", ShipDesignComponentsData.ArmourRetardation);
						sqliteCommand.Parameters.AddWithValue("@Prototype", ShipDesignComponentsData.Prototype);
						sqliteCommand.Parameters.AddWithValue("@TurretWeaponID", ShipDesignComponentsData.TurretWeaponID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1500);
			}
		}
	}
	
	/// <summary>
	/// method_125
	/// </summary>
	public class SaveIncrements
	{
		public struct IncrementsData
		{
			public int IncrementID;
			public decimal GameTime;
			public int Length;
		}
		IncrementsData[] IncrementsDataStore;


		public SaveIncrements(GClass0 game)
		{
			IncrementsDataStore = new IncrementsData[game.gclass85_0.dictionary_0.Count()];
			int i = 0;
			foreach (GClass84 gclass in game.gclass85_0.dictionary_0.Values)
			{
				var dataObj = new IncrementsData()
				{
					IncrementID = gclass.int_0,
					GameTime = gclass.decimal_0,
					Length = gclass.int_1,
				};
				IncrementsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Increments WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var IncrementsData in IncrementsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Increments (IncrementID, GameID, GameTime, Length ) \r\n                        VALUES ( @IncrementID, @GameID, @GameTime, @Length )";
						sqliteCommand.Parameters.AddWithValue("@IncrementID", IncrementsData.IncrementID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@GameTime", IncrementsData.GameTime);
						sqliteCommand.Parameters.AddWithValue("@Length", IncrementsData.Length);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1501);
			}
		}
	}
	
	/// <summary>
	/// method_126
	/// </summary>
	public class SaveGameLog
	{
		public struct GameLogData
		{
			public int IncrementID;
			public int RaceID;
			public bool SMOnly;
			public decimal Time;
			public GEnum123 EventType;
			public string MessageText;
			public int SystemID;
			public double Xcor;
			public double Ycor;
			public AuroraEventCategory IDType;
			public int PopulationID;
		}
		GameLogData[] GameLogDataStore;


		public SaveGameLog(GClass0 game)
		{
			int i = 0;
			var list = game.gclass85_0.dictionary_0.Values.SelectMany<GClass84, GClass80>((Func<GClass84, IEnumerable<GClass80>>)(x => (IEnumerable<GClass80>) x.list_0)).ToList<GClass80>();
			GameLogDataStore = new GameLogData[list.Count()];
			foreach(GClass80 gclass in list)
			{
				int num = 0;
				int num2 = 0;
				if (gclass.gclass178_0 != null)
				{
					num = gclass.gclass178_0.int_0;
				}
				if (gclass.gclass132_0 != null)
				{
					num2 = gclass.gclass132_0.int_5;
				}
				if (gclass.gclass178_0 != null)
				{
					num = gclass.gclass178_0.int_0;
				}
				if (gclass.gclass132_0 != null)
				{
					num2 = gclass.gclass132_0.int_5;
				}
				var dataObj = new GameLogData()
				{
					IncrementID = gclass.gclass84_0.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					SMOnly = gclass.bool_0,
					Time = gclass.decimal_0,
					EventType = gclass.gclass81_0.genum123_0,
					MessageText = gclass.string_0,
					SystemID = num,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
					IDType = gclass.auroraEventCategory_0,
					PopulationID = num2,
				};
				GameLogDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_GameLog WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var GameLogData in GameLogDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_GameLog (IncrementID, GameID, RaceID, SMOnly, Time, EventType, MessageText, SystemID, Xcor, Ycor, IDType, PopulationID ) \r\n                        VALUES ( @IncrementID, @GameID, @RaceID, @SMOnly, @Time, @EventType, @MessageText, @SystemID, @Xcor, @Ycor, @IDType, @PopulationID )";
						sqliteCommand.Parameters.AddWithValue("@IncrementID", GameLogData.IncrementID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", GameLogData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SMOnly", GameLogData.SMOnly);
						sqliteCommand.Parameters.AddWithValue("@Time", GameLogData.Time);
						sqliteCommand.Parameters.AddWithValue("@EventType", GameLogData.EventType);
						sqliteCommand.Parameters.AddWithValue("@MessageText", GameLogData.MessageText);
						sqliteCommand.Parameters.AddWithValue("@SystemID", GameLogData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@Xcor", GameLogData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", GameLogData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@IDType", GameLogData.IDType);
						sqliteCommand.Parameters.AddWithValue("@PopulationID", GameLogData.PopulationID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1502);
			}
		}
	}
	
	/// <summary>
	/// method_127
	/// </summary>
	public class SaveEventColour
	{
		public struct EventColourData
		{
			public GEnum123 EventTypeID;
			public int RaceID;
			public int AlertColour;
			public int TextColour;
		}
		EventColourData[] EventColourDataStore;


		public SaveEventColour(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass83>((Func<GClass21, IEnumerable<GClass83>>)(x => (IEnumerable<GClass83>) x.dictionary_3.Values)).ToList<GClass83>();
			EventColourDataStore = new EventColourData[list.Count()];
			foreach(GClass83 gclass in list)
			{
				var dataObj = new EventColourData()
				{
					EventTypeID = gclass.gclass81_0.genum123_0,
					RaceID = gclass.int_0,
					AlertColour = gclass.color_1.ToArgb(),
					TextColour = gclass.color_0.ToArgb(),
				};
				EventColourDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_EventColour WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var EventColourData in EventColourDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_EventColour (EventTypeID, RaceID, GameID, AlertColour, TextColour ) \r\n                        VALUES ( @EventTypeID, @RaceID, @GameID, @AlertColour, @TextColour )";
						sqliteCommand.Parameters.AddWithValue("@EventTypeID", EventColourData.EventTypeID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", EventColourData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@AlertColour", EventColourData.AlertColour);
						sqliteCommand.Parameters.AddWithValue("@TextColour", EventColourData.TextColour);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1503);
			}
		}
	}
	
	/// <summary>
	/// method_128
	/// </summary>
	public class SaveAetherRift
	{
		public struct AetherRiftData
		{
			public int SystemID;
			public double Diameter;
			public double Xcor;
			public double Ycor;
		}
		AetherRiftData[] AetherRiftDataStore;


		public SaveAetherRift(GClass0 game)
		{
			AetherRiftDataStore = new AetherRiftData[game.dictionary_15.Count()];
			int i = 0;
			foreach (GClass187 gclass in game.dictionary_15.Values)
			{
				var dataObj = new AetherRiftData()
				{
					SystemID = gclass.gclass178_0.int_0,
					Diameter = gclass.double_2,
					Xcor = gclass.double_0,
					Ycor = gclass.double_1,
				};
				AetherRiftDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_AetherRift WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var AetherRiftData in AetherRiftDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_AetherRift (SystemID, Diameter, Xcor, Ycor, GameID) \r\n                        VALUES ( @SystemID, @Diameter, @Xcor, @Ycor, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@SystemID", AetherRiftData.SystemID);
						sqliteCommand.Parameters.AddWithValue("@Diameter", AetherRiftData.Diameter);
						sqliteCommand.Parameters.AddWithValue("@Xcor", AetherRiftData.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", AetherRiftData.Ycor);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 3261);
			}
		}
	}
	
	/// <summary>
	/// method_129
	/// </summary>
	public class SaveRanks
	{
		public struct RanksData
		{
			public int RankID;
			public int RaceID;
			public string RankName;
			public int Priority;
			public AuroraCommanderType RankType;
			public string RankAbbrev;
		}
		RanksData[] RanksDataStore;


		public SaveRanks(GClass0 game)
		{
			int i = 0;
			var list = game.dictionary_34.Values.SelectMany<GClass21, GClass60>((Func<GClass21, IEnumerable<GClass60>>)(x => (IEnumerable<GClass60>) x.dictionary_1.Values)).ToList<GClass60>();
			RanksDataStore = new RanksData[list.Count()];
			foreach(GClass60 gclass in list)
			{
				var dataObj = new RanksData()
				{
					RankID = gclass.int_0,
					RaceID = gclass.gclass21_0.RaceID,
					RankName = gclass.RankName,
					Priority = gclass.int_1,
					RankType = gclass.auroraCommanderType_0,
					RankAbbrev = gclass.string_0,
				};
				RanksDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_Ranks WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var RanksData in RanksDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_Ranks (RankID, RaceID, RankName, Priority, RankType, RankAbbrev, GameID) \r\n                        VALUES ( @RankID, @RaceID, @RankName, @Priority, @RankType, @RankAbbrev, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@RankID", RanksData.RankID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", RanksData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@RankName", RanksData.RankName);
						sqliteCommand.Parameters.AddWithValue("@Priority", RanksData.Priority);
						sqliteCommand.Parameters.AddWithValue("@RankType", RanksData.RankType);
						sqliteCommand.Parameters.AddWithValue("@RankAbbrev", RanksData.RankAbbrev);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1504);
			}
		}
	}
	
	/// <summary>
	/// method_130
	/// </summary>
	public class SaveHideEvents
	{
		public struct HideEventsData
		{
			public int RaceID;
			public GEnum123 EventID;
		}
		HideEventsData[] HideEventsDataStore;


		public SaveHideEvents(GClass0 game)
		{
			HideEventsDataStore = new HideEventsData[game.list_8.Count()];
			int i = 0;
			foreach (GClass82 gclass in game.list_8)
			{
				var dataObj = new HideEventsData()
				{
					RaceID = gclass.int_0,
					EventID = gclass.genum123_0,
				};
				HideEventsDataStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_HideEvents WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach(var HideEventsData in HideEventsDataStore)
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_HideEvents (RaceID, EventID, GameID ) \r\n                        VALUES ( @RaceID, @EventID, @GameID )";
						sqliteCommand.Parameters.AddWithValue("@RaceID", HideEventsData.RaceID);
						sqliteCommand.Parameters.AddWithValue("@EventID", HideEventsData.EventID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1505);
			}
		}
	}
	
	/// <summary>
	/// method_131
	/// </summary>
	public class SaveAlienRaceSystemStatus
	{
		public struct RaceSysSurveyData
		{
			public int RaceID;
			public int SystemID;
			public string Name;
			public int DangerRating;
			public bool SysTPStatus;
			public int ControlRaceID;
			public int ForeignFleetRaceID;
			public int SectorID;
			public int NameThemeID;
			public string Discovered;
			public double Xcor;
			public double Ycor;
			public int ClosedWP;
			public bool SurveyDone;
			public bool GeoSurveyDefaultDone;
			public double SelectedBodyXcor;
			public double SelectedBodyYcor;
			public decimal DiscoveredTime;
			public double KmPerPixel;
			public bool NoAutoRoute;
			public bool MilitaryRestrictedSystem;
			public int SystemValue;
			public AuroraSystemProtectionStatus AutoProtectionStatus;
			public bool MineralSearchFlag;
		}
		RaceSysSurveyData[] RaceSysSurveyStore;

		public struct RaceJumpPointSurveyData
		{
			public int RaceID;
			public int WarpPointID;
			public int Explored;
			public int Charted;
			public bool AlienUnits;
			public int Hide;
		}
		RaceJumpPointSurveyData[] RaceJumpPointSurveyStore;

		public struct AlienRaceSystemStatusData
		{
			public int AlienRaceID;
			public int SystemID;
			public AuroraSystemProtectionStatus ProtectionStatusID;
			public int ViewingRaceID;
		}
		AlienRaceSystemStatusData[] AlienRaceSystemStatusStore;


		public SaveAlienRaceSystemStatus(GClass0 game)
		{
			int i = 0;
			var list1 = game.dictionary_34.Values.SelectMany<GClass21, GClass180>((Func<GClass21, IEnumerable<GClass180>>)(x => (IEnumerable<GClass180>) x.dictionary_0.Values)).ToList<GClass180>();
			RaceSysSurveyStore = new RaceSysSurveyData[list1.Count];
			foreach(GClass180 gclass in list1)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 1;
				if (gclass.gclass128_0 != null)
				{
					num = gclass.gclass128_0.int_0;
				}
				if (gclass.gclass102_0 != null)
				{
					num2 = gclass.gclass102_0.int_0;
				}
				if (gclass.gclass61_0 != null)
				{
					num3 = gclass.gclass61_0.int_0;
				}
				if (gclass.gclass3_0 != null)
				{
					num4 = (int)gclass.gclass3_0.genum92_0;
				}
				if (gclass.gclass128_0 != null)
				{
					num = gclass.gclass128_0.int_0;
				}
				if (gclass.gclass102_0 != null)
				{
					num2 = gclass.gclass102_0.int_0;
				}
				if (gclass.gclass61_0 != null)
				{
					num3 = gclass.gclass61_0.int_0;
				}
				if (gclass.gclass3_0 != null)
				{
					num4 = (int)gclass.gclass3_0.genum92_0;
				}
				var dataObj = new RaceSysSurveyData()
				{
					RaceID = gclass.gclass21_0.RaceID,
					SystemID = gclass.gclass178_0.int_0,
					Name = gclass.Name,
					DangerRating = gclass.int_0,
					SysTPStatus = gclass.bool_0,
					ControlRaceID = num2,
					ForeignFleetRaceID = gclass.int_1,
					SectorID = num3,
					NameThemeID = num,
					Discovered = gclass.string_0,
					Xcor = gclass.double_5,
					Ycor = gclass.double_6,
					ClosedWP = gclass.int_3,
					SurveyDone = gclass.bool_1,
					GeoSurveyDefaultDone = gclass.bool_2,
					SelectedBodyXcor = gclass.double_0,
					SelectedBodyYcor = gclass.double_1,
					DiscoveredTime = gclass.decimal_0,
					KmPerPixel = gclass.double_2 / GClass202.double_5,
					NoAutoRoute = gclass.bool_3,
					MilitaryRestrictedSystem = gclass.bool_4,
					SystemValue = num4,
					AutoProtectionStatus = gclass.auroraSystemProtectionStatus_0,
					MineralSearchFlag = gclass.bool_5,
				};
				RaceSysSurveyStore[i] = dataObj;
				i++;
			}
			i = 0;
			var list2 = game.dictionary_12.Values.SelectMany<GClass111, GClass181>((Func<GClass111, IEnumerable<GClass181>>)(x => (IEnumerable<GClass181>) x.dictionary_0.Values)).ToList<GClass181>();
			RaceJumpPointSurveyStore = new RaceJumpPointSurveyData[list2.Count];
			foreach(GClass181 gclass2 in list2)
			{
				var dataObj = new RaceJumpPointSurveyData()
				{
					RaceID = gclass2.gclass21_0.RaceID,
					WarpPointID = gclass2.gclass111_0.int_0,
					Explored = gclass2.int_1,
					Charted = gclass2.int_2,
					AlienUnits = gclass2.bool_0,
					Hide = gclass2.int_3,
				};
				RaceJumpPointSurveyStore[i] = dataObj;
				i++;
			}
			i = 0;
			var list3 = list1.SelectMany<GClass180, GClass103>((Func<GClass180, IEnumerable<GClass103>>)(x => (IEnumerable<GClass103>) x.dictionary_0.Values)).ToList<GClass103>();
			AlienRaceSystemStatusStore = new AlienRaceSystemStatusData[list3.Count];
			foreach(GClass103 gclass3 in list3)
			{
				var dataObj = new AlienRaceSystemStatusData()
				{
					AlienRaceID = gclass3.gclass102_0.int_0,
					SystemID = gclass3.gclass180_0.gclass178_0.int_0,
					ProtectionStatusID = gclass3.auroraSystemProtectionStatus_0,
					ViewingRaceID = gclass3.gclass180_0.gclass21_0.RaceID,
				};
				AlienRaceSystemStatusStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_RaceSysSurvey WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_RaceJumpPointSurvey WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				new SQLiteCommand("DELETE FROM FCT_AlienRaceSystemStatus WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in RaceSysSurveyStore )
					{
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@Name", dataObj.Name);
						sqliteCommand.Parameters.AddWithValue("@DangerRating", dataObj.DangerRating);
						sqliteCommand.Parameters.AddWithValue("@SysTPStatus", dataObj.SysTPStatus);
						sqliteCommand.Parameters.AddWithValue("@ControlRaceID", dataObj.ControlRaceID);
						sqliteCommand.Parameters.AddWithValue("@ForeignFleetRaceID", dataObj.ForeignFleetRaceID);
						sqliteCommand.Parameters.AddWithValue("@SectorID", dataObj.SectorID);
						sqliteCommand.Parameters.AddWithValue("@NameThemeID", dataObj.NameThemeID);
						sqliteCommand.Parameters.AddWithValue("@Discovered", dataObj.Discovered);
						sqliteCommand.Parameters.AddWithValue("@Xcor", dataObj.Xcor);
						sqliteCommand.Parameters.AddWithValue("@Ycor", dataObj.Ycor);
						sqliteCommand.Parameters.AddWithValue("@ClosedWP", dataObj.ClosedWP);
						sqliteCommand.Parameters.AddWithValue("@SurveyDone", dataObj.SurveyDone);
						sqliteCommand.Parameters.AddWithValue("@GeoSurveyDefaultDone", dataObj.GeoSurveyDefaultDone);
						sqliteCommand.Parameters.AddWithValue("@SelectedBodyXcor", dataObj.SelectedBodyXcor);
						sqliteCommand.Parameters.AddWithValue("@SelectedBodyYcor", dataObj.SelectedBodyYcor);
						sqliteCommand.Parameters.AddWithValue("@DiscoveredTime", dataObj.DiscoveredTime);
						sqliteCommand.Parameters.AddWithValue("@KmPerPixel", dataObj.KmPerPixel);
						sqliteCommand.Parameters.AddWithValue("@NoAutoRoute", dataObj.NoAutoRoute);
						sqliteCommand.Parameters.AddWithValue("@MilitaryRestrictedSystem", dataObj.MilitaryRestrictedSystem);
						sqliteCommand.Parameters.AddWithValue("@SystemValue", dataObj.SystemValue);
						sqliteCommand.Parameters.AddWithValue("@AutoProtectionStatus", dataObj.AutoProtectionStatus);
						sqliteCommand.Parameters.AddWithValue("@MineralSearchFlag", dataObj.MineralSearchFlag);
					}
					foreach (var dataObj in RaceJumpPointSurveyStore )
					{
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@WarpPointID", dataObj.WarpPointID);
						sqliteCommand.Parameters.AddWithValue("@Explored", dataObj.Explored);
						sqliteCommand.Parameters.AddWithValue("@Charted", dataObj.Charted);
						sqliteCommand.Parameters.AddWithValue("@AlienUnits", dataObj.AlienUnits);
						sqliteCommand.Parameters.AddWithValue("@Hide", dataObj.Hide);
					}
					foreach (var dataObj in AlienRaceSystemStatusStore )
					{
						sqliteCommand.Parameters.AddWithValue("@AlienRaceID", dataObj.AlienRaceID);
						sqliteCommand.Parameters.AddWithValue("@SystemID", dataObj.SystemID);
						sqliteCommand.Parameters.AddWithValue("@ProtectionStatusID", dataObj.ProtectionStatusID);
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@ViewingRaceID", dataObj.ViewingRaceID);
					}
					sqliteCommand.CommandText = "INSERT INTO FCT_RaceSysSurvey (GameID, RaceID, SystemID, Name, DangerRating, SysTPStatus, ControlRaceID, ForeignFleetRaceID, SectorID, NameThemeID, Discovered, Xcor, Ycor, ClosedWP, SurveyDone, GeoSurveyDefaultDone, SelectedBodyXcor, SelectedBodyYcor, KmPerPixel, DiscoveredTime, NoAutoRoute, MilitaryRestrictedSystem, SystemValue, AutoProtectionStatus, MineralSearchFlag ) \r\n                        VALUES (@GameID, @RaceID, @SystemID, @Name, @DangerRating, @SysTPStatus, @ControlRaceID, @ForeignFleetRaceID, @SectorID, @NameThemeID, @Discovered, @Xcor, @Ycor, @ClosedWP, @SurveyDone, @GeoSurveyDefaultDone, @SelectedBodyXcor, @SelectedBodyYcor, @KmPerPixel, @DiscoveredTime, @NoAutoRoute, @MilitaryRestrictedSystem, @SystemValue, @AutoProtectionStatus, @MineralSearchFlag )";
					sqliteCommand.ExecuteNonQuery();
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1506);
			}
		}
	}

	/// <summary>
	/// method_132
	/// </summary>
	public class SaveDesignPhilosophy
	{
		public struct DesignPhilosophyData
		{
			public int RaceID;
			public int ActiveAntiMissile;
			public int ActiveAntiFAC;
			public int ActiveAntiFighter;
			public int ActiveLarge;
			public int ActiveVeryLarge;
			public int ActiveNavigation;
			public int ActiveStandard;
			public int ActiveSmall;
			public int FireControlAntiMissile;
			public int FireControlAntiFAC;
			public int FireControlAntiFighter;
			public int FireControlStandardMissile;
			public int FireControlFACMissile;
			public int FireControlBeamRange;
			public int FireControlBeamSpeed;
			public int JumpDriveBattlecruiser;
			public int JumpDriveCruiser;
			public int JumpDriveDestroyer;
			public int JumpDriveSurvey;
			public int JumpDriveMediumHive;
			public int JumpDriveLargeHive;
			public int JumpDriveVeryLargeHive;
			public int PointDefenceWeapon;
			public int Carronade;
			public int CIWS;
			public int Gauss;
			public int Meson;
			public int MesonPointDefence;
			public int LaserLarge;
			public int LaserPointDefence;
			public int LaserSpinal;
			public int ParticleBeam;
			public int Railgun;
			public int HighPowerMicrowaveLarge;
			public int HighPowerMicrowaveSmall;
			public int LauncherFAC;
			public int LauncherStandard;
			public int LauncherMine;
			public int LauncherPointDefence;
			public int EngineCommercial;
			public int EngineMilitary;
			public int EngineFAC;
			public int EngineFighter;
			public int EngineSurvey;
			public decimal ShieldProportion;
			public int Cloak;
			public int EMSensorSize1;
			public int EMSensorSize2;
			public int EMSensorSize3;
			public int EMSensorSize6;
			public int ThermalSensorSize1;
			public int ThermalSensorSize2;
			public int ThermalSensorSize3;
			public int ThermalSensorSize6;
			public GEnum39 PrimaryBeamPreference;
			public GEnum39 SecondaryBeamPreference;
			public GEnum39 PointDefencePreference;
			public int MissileFAC;
			public int MissileCaptorMine;
			public int MissileMineSecondStage;
			public int MissilePointDefence;
			public int MissileStandard;
			public int NumCommercialEngines;
			public int EngineSizeMilitary;
			public int EngineSizeCommercial;
			public int LauncherSize;
			public int LauncherMineSize;
			public int NumSalvos;
			public int WarshipArmour;
			public int WarshipEngineering;
			public int WarshipEngineProportion;
			public int NumWarshipEngines;
			public int WarshipHullSize;
			public int ActiveResolution;
			public decimal SurveyEngineBoost;
			public int SurveySensors;
			public int TerraformModules;
			public int HarvesterModules;
			public int MiningModules;
		}
		DesignPhilosophyData[] DesignPhilosophyStore;


		public SaveDesignPhilosophy(GClass0 game)
		{
			//foreach (DesignPhilosophy designPhilosophy in this.RacesList.Values.Select<Race, DesignPhilosophy>((Func<Race, DesignPhilosophy>) (x => x.RaceDesignPhilosophy)).ToList<DesignPhilosophy>())
			//RaceList = dictionary_34
			//Race = Gclass21
			//DesignPhilosophy = GClass20
			var list = game.dictionary_34.Values.Select<GClass21, GClass20>((Func<GClass21, GClass20>) (x => x.gclass20_0)).Where(y => y != null).ToList<GClass20>();
			int i = 0;
			DesignPhilosophyStore = new DesignPhilosophyData[list.Count];
			foreach (GClass20 gclass in list)
			{
				var dataObj = new DesignPhilosophyData()
				{
					RaceID = gclass.gclass21_0.RaceID,
					ActiveAntiMissile = game.method_137(gclass.gclass206_0),
					ActiveAntiFAC = game.method_137(gclass.gclass206_1),
					ActiveAntiFighter = game.method_137(gclass.gclass206_2),
					ActiveLarge = game.method_137(gclass.gclass206_3),
					ActiveVeryLarge = game.method_137(gclass.gclass206_4),
					ActiveNavigation = game.method_137(gclass.gclass206_7),
					ActiveStandard = game.method_137(gclass.gclass206_5),
					ActiveSmall = game.method_137(gclass.gclass206_6),
					FireControlAntiMissile = game.method_137(gclass.gclass206_8),
					FireControlAntiFAC = game.method_137(gclass.gclass206_9),
					FireControlAntiFighter = game.method_137(gclass.gclass206_10),
					FireControlStandardMissile = game.method_137(gclass.gclass206_11),
					FireControlFACMissile = game.method_137(gclass.gclass206_12),
					FireControlBeamRange = game.method_137(gclass.gclass206_13),
					FireControlBeamSpeed = game.method_137(gclass.gclass206_14),
					JumpDriveBattlecruiser = game.method_137(gclass.gclass206_15),
					JumpDriveCruiser = game.method_137(gclass.gclass206_16),
					JumpDriveDestroyer = game.method_137(gclass.gclass206_17),
					JumpDriveSurvey = game.method_137(gclass.gclass206_18),
					JumpDriveMediumHive = game.method_137(gclass.gclass206_19),
					JumpDriveLargeHive = game.method_137(gclass.gclass206_20),
					JumpDriveVeryLargeHive = game.method_137(gclass.gclass206_21),
					PointDefenceWeapon = game.method_137(gclass.gclass206_22),
					Carronade = game.method_137(gclass.gclass206_23),
					CIWS = game.method_137(gclass.gclass206_24),
					Gauss = game.method_137(gclass.gclass206_25),
					Meson = game.method_137(gclass.gclass206_26),
					MesonPointDefence = game.method_137(gclass.gclass206_27),
					LaserLarge = game.method_137(gclass.gclass206_28),
					LaserPointDefence = game.method_137(gclass.gclass206_29),
					LaserSpinal = game.method_137(gclass.gclass206_30),
					ParticleBeam = game.method_137(gclass.gclass206_31),
					Railgun = game.method_137(gclass.gclass206_32),
					HighPowerMicrowaveLarge = game.method_137(gclass.gclass206_33),
					HighPowerMicrowaveSmall = game.method_137(gclass.gclass206_34),
					LauncherFAC = game.method_137(gclass.gclass206_35),
					LauncherStandard = game.method_137(gclass.gclass206_36),
					LauncherMine = game.method_137(gclass.gclass206_37),
					LauncherPointDefence = game.method_137(gclass.gclass206_38),
					EngineCommercial = game.method_137(gclass.gclass206_39),
					EngineMilitary = game.method_137(gclass.gclass206_40),
					EngineFAC = game.method_137(gclass.gclass206_41),
					EngineFighter = game.method_137(gclass.gclass206_42),
					EngineSurvey = game.method_137(gclass.gclass206_43),
					ShieldProportion = gclass.decimal_0,
					Cloak = game.method_137(gclass.gclass206_44),
					EMSensorSize1 = game.method_137(gclass.gclass206_45),
					EMSensorSize2 = game.method_137(gclass.gclass206_46),
					EMSensorSize3 = game.method_137(gclass.gclass206_47),
					EMSensorSize6 = game.method_137(gclass.gclass206_48),
					ThermalSensorSize1 = game.method_137(gclass.gclass206_49),
					ThermalSensorSize2 = game.method_137(gclass.gclass206_50),
					ThermalSensorSize3 = game.method_137(gclass.gclass206_51),
					ThermalSensorSize6 = game.method_137(gclass.gclass206_52),
					PrimaryBeamPreference = gclass.genum39_0,
					SecondaryBeamPreference = gclass.genum39_1,
					PointDefencePreference = gclass.genum39_2,
					MissileFAC = game.method_138(gclass.gclass120_0),
					MissileCaptorMine = game.method_138(gclass.gclass120_1),
					MissileMineSecondStage = game.method_138(gclass.gclass120_2),
					MissilePointDefence = game.method_138(gclass.gclass120_3),
					MissileStandard = game.method_138(gclass.gclass120_4),
					NumCommercialEngines = gclass.int_0,
					EngineSizeMilitary = gclass.int_1,
					EngineSizeCommercial = gclass.int_2,
					LauncherSize = gclass.int_3,
					LauncherMineSize = gclass.int_4,
					NumSalvos = gclass.int_5,
					WarshipArmour = gclass.int_6,
					WarshipEngineering = gclass.int_7,
					WarshipEngineProportion = gclass.int_8,
					NumWarshipEngines = gclass.int_11,
					WarshipHullSize = gclass.int_9,
					ActiveResolution = gclass.int_10,
					SurveyEngineBoost = gclass.decimal_1,
					SurveySensors = gclass.int_12,
					TerraformModules = gclass.int_13,
					HarvesterModules = gclass.int_14,
					MiningModules = gclass.int_15,
				};
				DesignPhilosophyStore[i] = dataObj;
				i++;
			}
		}

		public void SaveToSQL(SQLiteConnection sqliteConnection_0, int gameID)
		{
			try
			{
				new SQLiteCommand("DELETE FROM FCT_DesignPhilosophy WHERE GameID = " + gameID, sqliteConnection_0).ExecuteNonQuery();
				using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection_0))
				{
					foreach (var dataObj in DesignPhilosophyStore )
					{
						sqliteCommand.CommandText = "INSERT INTO FCT_DesignPhilosophy (GameID, RaceID, ActiveAntiMissile, ActiveAntiFAC, ActiveAntiFighter, ActiveLarge, ActiveVeryLarge, ActiveNavigation, ActiveStandard, ActiveSmall, FireControlAntiMissile, FireControlAntiFAC, FireControlAntiFighter, FireControlStandardMissile, FireControlFACMissile, FireControlBeamRange, FireControlBeamSpeed, \r\n                            JumpDriveBattlecruiser, JumpDriveCruiser, JumpDriveDestroyer, JumpDriveSurvey, PointDefenceWeapon, Carronade, CIWS, Gauss, Meson, MesonPointDefence, LaserLarge, LaserPointDefence, LaserSpinal, ParticleBeam, Railgun, LauncherFAC, LauncherStandard, LauncherMine, LauncherPointDefence, HighPowerMicrowaveLarge, HighPowerMicrowaveSmall,\r\n                            EngineCommercial, EngineMilitary, EngineFAC, EngineFighter, EngineSurvey, Cloak, ShieldProportion, EMSensorSize1, EMSensorSize2, EMSensorSize3, EMSensorSize6, ThermalSensorSize1, ThermalSensorSize2, ThermalSensorSize3, ThermalSensorSize6, PrimaryBeamPreference, SecondaryBeamPreference, PointDefencePreference, JumpDriveMediumHive, JumpDriveLargeHive, JumpDriveVeryLargeHive,\r\n                            MissileFAC, MissileCaptorMine, MissileMineSecondStage, MissilePointDefence, MissileStandard, NumCommercialEngines, EngineSizeMilitary, EngineSizeCommercial, LauncherSize, LauncherMineSize, NumSalvos, WarshipArmour, WarshipEngineering, WarshipEngineProportion, NumWarshipEngines, WarshipHullSize, ActiveResolution, SurveyEngineBoost, SurveySensors, TerraformModules, HarvesterModules, MiningModules) \r\n                        VALUES ( @GameID, @RaceID, @ActiveAntiMissile, @ActiveAntiFAC, @ActiveAntiFighter, @ActiveLarge, @ActiveVeryLarge, @ActiveNavigation, @ActiveStandard, @ActiveSmall, @FireControlAntiMissile, @FireControlAntiFAC, @FireControlAntiFighter, @FireControlStandardMissile, @FireControlFACMissile, @FireControlBeamRange, @FireControlBeamSpeed,\r\n                            @JumpDriveBattlecruiser, @JumpDriveCruiser, @JumpDriveDestroyer, @JumpDriveSurvey, @PointDefenceWeapon, @Carronade, @CIWS, @Gauss, @Meson, @MesonPointDefence, @LaserLarge, @LaserPointDefence, @LaserSpinal, @ParticleBeam, @Railgun, @LauncherFAC, @LauncherStandard, @LauncherMine, @LauncherPointDefence, @HighPowerMicrowaveLarge, @HighPowerMicrowaveSmall,\r\n                            @EngineCommercial, @EngineMilitary, @EngineFAC, @EngineFighter, @EngineSurvey, @Cloak, @ShieldProportion, @EMSensorSize1, @EMSensorSize2, @EMSensorSize3, @EMSensorSize6, @ThermalSensorSize1, @ThermalSensorSize2, @ThermalSensorSize3, @ThermalSensorSize6, @PrimaryBeamPreference, @SecondaryBeamPreference, @PointDefencePreference, @JumpDriveMediumHive, @JumpDriveLargeHive, @JumpDriveVeryLargeHive,\r\n                            @MissileFAC, @MissileCaptorMine, @MissileMineSecondStage, @MissilePointDefence, @MissileStandard, @NumCommercialEngines, @EngineSizeMilitary, @EngineSizeCommercial, @LauncherSize, @LauncherMineSize, @NumSalvos, @WarshipArmour, @WarshipEngineering, @WarshipEngineProportion, @NumWarshipEngines, @WarshipHullSize, @ActiveResolution, @SurveyEngineBoost, @SurveySensors, @TerraformModules, @HarvesterModules, @MiningModules)";
						sqliteCommand.Parameters.AddWithValue("@GameID", gameID);
						sqliteCommand.Parameters.AddWithValue("@RaceID", dataObj.RaceID);
						sqliteCommand.Parameters.AddWithValue("@ActiveAntiMissile", dataObj.ActiveAntiMissile);
						sqliteCommand.Parameters.AddWithValue("@ActiveAntiFAC", dataObj.ActiveAntiFAC);
						sqliteCommand.Parameters.AddWithValue("@ActiveAntiFighter", dataObj.ActiveAntiFighter);
						sqliteCommand.Parameters.AddWithValue("@ActiveLarge", dataObj.ActiveLarge);
						sqliteCommand.Parameters.AddWithValue("@ActiveVeryLarge", dataObj.ActiveVeryLarge);
						sqliteCommand.Parameters.AddWithValue("@ActiveNavigation", dataObj.ActiveNavigation);
						sqliteCommand.Parameters.AddWithValue("@ActiveStandard", dataObj.ActiveStandard);
						sqliteCommand.Parameters.AddWithValue("@ActiveSmall", dataObj.ActiveSmall);
						sqliteCommand.Parameters.AddWithValue("@FireControlAntiMissile", dataObj.FireControlAntiMissile);
						sqliteCommand.Parameters.AddWithValue("@FireControlAntiFAC", dataObj.FireControlAntiFAC);
						sqliteCommand.Parameters.AddWithValue("@FireControlAntiFighter", dataObj.FireControlAntiFighter);
						sqliteCommand.Parameters.AddWithValue("@FireControlStandardMissile", dataObj.FireControlStandardMissile);
						sqliteCommand.Parameters.AddWithValue("@FireControlFACMissile", dataObj.FireControlFACMissile);
						sqliteCommand.Parameters.AddWithValue("@FireControlBeamRange", dataObj.FireControlBeamRange);
						sqliteCommand.Parameters.AddWithValue("@FireControlBeamSpeed", dataObj.FireControlBeamSpeed);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveBattlecruiser", dataObj.JumpDriveBattlecruiser);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveCruiser", dataObj.JumpDriveCruiser);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveDestroyer", dataObj.JumpDriveDestroyer);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveSurvey", dataObj.JumpDriveSurvey);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveMediumHive", dataObj.JumpDriveMediumHive);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveLargeHive", dataObj.JumpDriveLargeHive);
						sqliteCommand.Parameters.AddWithValue("@JumpDriveVeryLargeHive", dataObj.JumpDriveVeryLargeHive);
						sqliteCommand.Parameters.AddWithValue("@PointDefenceWeapon", dataObj.PointDefenceWeapon);
						sqliteCommand.Parameters.AddWithValue("@Carronade", dataObj.Carronade);
						sqliteCommand.Parameters.AddWithValue("@CIWS", dataObj.CIWS);
						sqliteCommand.Parameters.AddWithValue("@Gauss", dataObj.Gauss);
						sqliteCommand.Parameters.AddWithValue("@Meson", dataObj.Meson);
						sqliteCommand.Parameters.AddWithValue("@MesonPointDefence", dataObj.MesonPointDefence);
						sqliteCommand.Parameters.AddWithValue("@LaserLarge", dataObj.LaserLarge);
						sqliteCommand.Parameters.AddWithValue("@LaserPointDefence", dataObj.LaserPointDefence);
						sqliteCommand.Parameters.AddWithValue("@LaserSpinal", dataObj.LaserSpinal);
						sqliteCommand.Parameters.AddWithValue("@ParticleBeam", dataObj.ParticleBeam);
						sqliteCommand.Parameters.AddWithValue("@Railgun", dataObj.Railgun);
						sqliteCommand.Parameters.AddWithValue("@HighPowerMicrowaveLarge", dataObj.HighPowerMicrowaveLarge);
						sqliteCommand.Parameters.AddWithValue("@HighPowerMicrowaveSmall", dataObj.HighPowerMicrowaveSmall);
						sqliteCommand.Parameters.AddWithValue("@LauncherFAC", dataObj.LauncherFAC);
						sqliteCommand.Parameters.AddWithValue("@LauncherStandard", dataObj.LauncherStandard);
						sqliteCommand.Parameters.AddWithValue("@LauncherMine", dataObj.LauncherMine);
						sqliteCommand.Parameters.AddWithValue("@LauncherPointDefence", dataObj.LauncherPointDefence);
						sqliteCommand.Parameters.AddWithValue("@EngineCommercial", dataObj.EngineCommercial);
						sqliteCommand.Parameters.AddWithValue("@EngineMilitary", dataObj.EngineMilitary);
						sqliteCommand.Parameters.AddWithValue("@EngineFAC", dataObj.EngineFAC);
						sqliteCommand.Parameters.AddWithValue("@EngineFighter", dataObj.EngineFighter);
						sqliteCommand.Parameters.AddWithValue("@EngineSurvey", dataObj.EngineSurvey);
						sqliteCommand.Parameters.AddWithValue("@ShieldProportion", dataObj.ShieldProportion);
						sqliteCommand.Parameters.AddWithValue("@Cloak", dataObj.Cloak);
						sqliteCommand.Parameters.AddWithValue("@EMSensorSize1", dataObj.EMSensorSize1);
						sqliteCommand.Parameters.AddWithValue("@EMSensorSize2", dataObj.EMSensorSize2);
						sqliteCommand.Parameters.AddWithValue("@EMSensorSize3", dataObj.EMSensorSize3);
						sqliteCommand.Parameters.AddWithValue("@EMSensorSize6", dataObj.EMSensorSize6);
						sqliteCommand.Parameters.AddWithValue("@ThermalSensorSize1", dataObj.ThermalSensorSize1);
						sqliteCommand.Parameters.AddWithValue("@ThermalSensorSize2", dataObj.ThermalSensorSize2);
						sqliteCommand.Parameters.AddWithValue("@ThermalSensorSize3", dataObj.ThermalSensorSize3);
						sqliteCommand.Parameters.AddWithValue("@ThermalSensorSize6", dataObj.ThermalSensorSize6);
						sqliteCommand.Parameters.AddWithValue("@PrimaryBeamPreference", dataObj.PrimaryBeamPreference);
						sqliteCommand.Parameters.AddWithValue("@SecondaryBeamPreference", dataObj.SecondaryBeamPreference);
						sqliteCommand.Parameters.AddWithValue("@PointDefencePreference", dataObj.PointDefencePreference);
						sqliteCommand.Parameters.AddWithValue("@MissileFAC", dataObj.MissileFAC);
						sqliteCommand.Parameters.AddWithValue("@MissileCaptorMine", dataObj.MissileCaptorMine);
						sqliteCommand.Parameters.AddWithValue("@MissileMineSecondStage", dataObj.MissileMineSecondStage);
						sqliteCommand.Parameters.AddWithValue("@MissilePointDefence", dataObj.MissilePointDefence);
						sqliteCommand.Parameters.AddWithValue("@MissileStandard", dataObj.MissileStandard);
						sqliteCommand.Parameters.AddWithValue("@NumCommercialEngines", dataObj.NumCommercialEngines);
						sqliteCommand.Parameters.AddWithValue("@EngineSizeMilitary", dataObj.EngineSizeMilitary);
						sqliteCommand.Parameters.AddWithValue("@EngineSizeCommercial", dataObj.EngineSizeCommercial);
						sqliteCommand.Parameters.AddWithValue("@LauncherSize", dataObj.LauncherSize);
						sqliteCommand.Parameters.AddWithValue("@LauncherMineSize", dataObj.LauncherMineSize);
						sqliteCommand.Parameters.AddWithValue("@NumSalvos", dataObj.NumSalvos);
						sqliteCommand.Parameters.AddWithValue("@WarshipArmour", dataObj.WarshipArmour);
						sqliteCommand.Parameters.AddWithValue("@WarshipEngineering", dataObj.WarshipEngineering);
						sqliteCommand.Parameters.AddWithValue("@WarshipEngineProportion", dataObj.WarshipEngineProportion);
						sqliteCommand.Parameters.AddWithValue("@NumWarshipEngines", dataObj.NumWarshipEngines);
						sqliteCommand.Parameters.AddWithValue("@WarshipHullSize", dataObj.WarshipHullSize);
						sqliteCommand.Parameters.AddWithValue("@ActiveResolution", dataObj.ActiveResolution);
						sqliteCommand.Parameters.AddWithValue("@SurveyEngineBoost", dataObj.SurveyEngineBoost);
						sqliteCommand.Parameters.AddWithValue("@SurveySensors", dataObj.SurveySensors);
						sqliteCommand.Parameters.AddWithValue("@TerraformModules", dataObj.TerraformModules);
						sqliteCommand.Parameters.AddWithValue("@HarvesterModules", dataObj.HarvesterModules);
						sqliteCommand.Parameters.AddWithValue("@MiningModules", dataObj.MiningModules);
						sqliteCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception exception_)
			{
				GClass202.smethod_68(exception_, 1507);
			}
		}
	}

	
	
}
// ---------------------------------------------------------------------------------------
// 
// FishFa30.CS                                                      Ulrich Müller 20.07.02
//                                                                  PCS1700       C# Final
//                                                                  Version         3.1.01
// Assembly FishFa30 mit Funktionen zum Betrieb der fischertechnik Interfaces
// Klasse FishFace           : Kapselung und Erweiterung der Funktionen von umFish30.DLL
// Klasse FishFaceException  : Raise FishFace Ausnahmen
// Klasse FishRobot          : Betrieb eines Robots mit Robotmotoren
// 
// 20.07.02 : Version 3.0 mit FishFace/FishException
// 30.05.04 : Erweiterung um Klasse FishRobot
// 
// ---------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FishFa30
{
	public enum Dir
	{
		Aus    = 0,   Off    = Aus,
		Ein    = 1,   On     = Ein,
		Links  = 1,   Left   = Links,
		Rechts = 2,   Right  = Rechts
	}

	public enum Nr
	{
		EX  = 0,  EY  = 1,
		E1  = 1,  E2  = 2,  E3  = 3,  E4  = 4,
		E5  = 5,  E6  = 6,	E7  = 7,  E8  = 8,
		E9  = 9,  E10 = 10,	E11 = 11, E12 = 12,
		E13 = 13, E14 = 14,	E15 = 15, E16 = 16,
		M1  = 1,  M2  = 2,	M3  = 3,  M4  = 4,
		M5  = 5,  M6  = 6,	M7  = 7,  M8  = 8
	}

	public enum Port
	{
		LPT  = 0, COM1 = 1, COM2 = 2, COM3 = 3, COM4 = 4,
		          COM5 = 5, COM6 = 6, COM7 = 7, COM8 = 8, LPT1, LPT2, LPT3
	}

	public enum Speed
	{
		Off = 0, L1 = 1, L2  = 2,  L3  = 3,  L4  = 4,  L5  = 5,  L6  = 6,  L7 = 7,   Half = 7,
		L8  = 8, L9 = 9, L10 = 10, L11 = 11, L12 = 12, L13 = 13, L14 = 14, L15 = 15, Full = 15  
	}

	public enum Wait
	{
		Ende = 0, Time = 1, NotHalt = 2, ESC = 3
	}

	public class FishFaceException: ApplicationException
	{
		public FishFaceException(){}
		public FishFaceException(string message) : base(message){}
		public FishFaceException(string message, Exception inner) : base(message, inner){}
	}

	// ----------------------------------------------------------------------------------
	//
	// Klasse FishFace
	// entspricht in der Funktionalität umFish20Ex, Basis umFish30.DLL cs-Variante
	//
	// ----------------------------------------------------------------------------------

	public class FishFace
	{
		// --- umFish30.DLL Deklarationen, cs-Variante ---------------------------------
	
		[DllImport("umFish30.DLL")]
		private static extern int csOpenInterface(int PortNr, int AnalogScan, int Slave, 
			int PollInterval);
		[DllImport("umFish30.DLL")]
		private static extern int csOpenInterfaceEx(int PortNr, int AnalogScan, 
			int Slave, int PollInterval, int LPTAnalog, int LPTDelay);
		[DllImport("umFish30.DLL")]
		private static extern int csCloseInterface(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csVersion();
		[DllImport("umFish30.DLL")]
		private static extern int csGetAnalogScan(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csGetLPTAnalog(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csGetLPTDelay(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csGetSlave(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csGetPollInterval(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csGetInput(int iHandle, int InputNr);
		[DllImport("umFish30.DLL")]
		private static extern int csGetInputs(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csGetAnalog(int iHandle, int AnalogNr);
		[DllImport("umFish30.DLL")]
		private static extern int csGetAnalogDirect(int iHandle, int AnalogNr);
		[DllImport("umFish30.DLL")]
		private static extern int csSetMotor(int iHandle, int MotorNr, int Direction);
		[DllImport("umFish30.DLL")]
		private static extern int csSetMotorEx(int iHandle, int MotorNr, int Direction, 
			int Speed);
		[DllImport("umFish30.DLL")]
		private static extern int csGetMotors(int iHandle);
		[DllImport("umFish30.DLL")]
		private static extern int csSetMotors(int iHandle, int MotorStatus);
		[DllImport("umFish30.DLL")]
		private static extern int csSetMotorsEx(int iHandle, int MotorStatus,
			int SpeedStatus);
		[DllImport("umFish30.DLL")]
		private static extern int csSetLamp(int iHandle, int LampNr, int OnOff);
		[DllImport("umFish30.DLL")]
		private static extern int csRobMotor(int iHandle, int MotorNr, int Direction, 
			int Speed, int Counter);
		[DllImport("umFish30.DLL")]
		private static extern int csRobMotors(int iHandle, int MotorStatus, 
			int SpeedStatus, int ModeStatus);
		[DllImport("umFish30.DLL")]
		private static extern int csGetModeStatus(int iHandle, int MotorNr);
		[DllImport("umFish30.DLL")]
		private static extern void csSetModeStatus(int iHandle, int MotorNr, int Value);
		[DllImport("umFish30.DLL")]
		private static extern int csGetCounter(int iHandle, int CounterNr);
		[DllImport("umFish30.DLL")]
		private static extern void csSetCounter(int iHandle, int CounterNr, int Value);
		[DllImport("umFish30.DLL")]
		private static extern void csClearCounters(int iHandle);
		
		// --- Win API Deklarationen ----------------------------------------------------

		[DllImport("kernel32")]
		protected static extern void Sleep(uint mSek);
		[DllImport("kernel32")]
		protected static extern uint GetTickCount();
		[DllImport("user32")]
		protected static extern uint GetAsyncKeyState(uint vKey);
		protected const uint ESCAPE = 0x1B;
		[DllImport("winmm.dll")]
		protected static extern uint timeKillEvent(uint uID);
		
		// --- private, global Variable -------------------------------------------------

    private int    ft;             // --- akt. ftiDCB-Nummer von umFish30.DLL
		private int    FPortNr = -1;   // --- akt. PortNr
		private bool   FOpen;          // --- OpenInterface erfolgreich
		private bool   FNotHalt;       // --- NotHalt
		private int    FLPTDelay;      // --- Ausgabeverzögerung beim LPT-Port
		private int    FLPTAnalog;     // --- Skalierung des Analogwertes beim LPT-Port
		private int    FSlave;         // --- mit/ohne Slave 1/0 
		private int    FPollInterval;  // --- PollInterval 0 / >0
		private int    FAnalogScan;    // --- AnalogScan 1/0
		private bool   FDoEvents;      // --- mit/ohne Application.DoEvents

		private string[] FPortName = {"LPT", "COM1", "COM2", "COM3", "COM4", 
                                     "COM5", "COM6", "COM7", "COM8", 
									 "LPT1", "LPT2", "LPT3"};
		public const int Fehler = 0x1FFFF, Error = Fehler;

		// --- FishFa30 : Constructor --------------------------------------------------

		public FishFace(){
			FLPTAnalog    = 0;
			FLPTDelay     = 0;
			FSlave        = 0;
			FPollInterval = 0;
			FAnalogScan   = 0;
		}
		public FishFace(bool AnalogScan, bool Slave, int PollInterval)
		{
			FLPTAnalog    = 0;
			FLPTDelay     = 0;
			FSlave        = Slave ? 1 : 0;
			FPollInterval = PollInterval;
			FAnalogScan   = AnalogScan ? 1 : 0;
		}
		public FishFace(bool AnalogScan, bool Slave, int PollInterval, 
			int LPTAnalog, int LPTDelay)
		{
			FLPTAnalog    = LPTAnalog;
			FLPTDelay     = LPTDelay;
			FSlave        = Slave ? 1 : 0;
			FPollInterval = PollInterval;
			FAnalogScan   = AnalogScan ? 1 : 0;
		}

		// --- AnalogScan : Get/Set ----------------------------------------------------

		public bool AnalogScan
		{
			get {return csGetAnalogScan(ft) == 0 ? false : true;}
		}

		// --- AnalogsEX : Get -------------------------------------------------------

		public int AnalogsEX
		{
			get {return csGetAnalog(ft, 0);}
		}

		// --- AnalogsEY : Get -----------------------------------------------------

		public int AnalogsEY
		{
			get {return csGetAnalog(ft, 1);}
		}

		// --- ClearCounter : Löschen eines Counters --------------------------------

		public void ClearCounter(Nr InputNr)
		{
			ClearCounter(Convert.ToInt32(InputNr));
		}
		public void ClearCounter(int InputNr)
		{
			csSetCounter(ft, InputNr, 0);
		}

		// --- ClearCounters : Löschen aller Counter --------------------------------

		public void ClearCounters()
		{
			csClearCounters(ft);
		}

		// --- ClearMotors : Löschen aller M-Ausgänge -----------------------------------

		public void ClearMotors()
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.ClearMotors"));
			if(csSetMotors(ft, 0) == Fehler)
				throw(new FishFaceException("InterfaceProblem.ClearMotors"));
		}

		// --- CloseInterface : Schließen Interface -------------------------------------
		
		public void CloseInterface()
		{
			csCloseInterface(ft);
			FPortNr = -1;
			FOpen     = false;
		}

		// --- Finish : Feststellen Endewunsch (NotHalt, Escape, InputNr) --------------

		public bool Finish()
		{
			return Finish(0);
		}
		public bool Finish(Nr InputNr)
		{
			return Finish(Convert.ToInt32(InputNr));
		}
		public bool Finish(int InputNr)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.Finish"));
			if (FNotHalt || GetAsyncKeyState(ESCAPE) !=0) return true;
			if(Inputs == Fehler) throw(new FishFaceException("InterfaceProblem.Finish"));
			if ((InputNr > 0) && GetInput(InputNr)) return true;
			return false;
		}

		// --- GetAnalog : Auslesen Analogwert -----------------------------------------

		public int GetAnalog(Nr AnalogNr)
		{
			return GetAnalog(Convert.ToInt32(AnalogNr));
		}
		public int GetAnalog(int AnalogNr)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.GetAnalog"));
			int res = csGetAnalog(ft, AnalogNr);
			if(res == Fehler) throw(new FishFaceException("InterfaceProblem.GetAnalog"));
			return res;
		}

		// --- GetAnalogDirect : Auslesen Analogwert ----------------------------------

		public int GetAnalogDirect(Nr AnalogNr)
		{
			return GetAnalogDirect(Convert.ToInt32(AnalogNr));
		}
		public int GetAnalogDirect(int AnalogNr)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.GetAnalogDirect"));
			int res = csGetAnalogDirect(ft, AnalogNr);
			if(res == Fehler) 
				throw(new FishFaceException("InterfaceProblem.GetAnalogDirect"));
			return res;
		}

		// --- GetCounter : Auslesen eines Counters ------------------------------------

		public int GetCounter(Nr InputNr)
		{
			return GetCounter(Convert.ToInt32(InputNr));
		}
		public int GetCounter(int InputNr)
		{
			return csGetCounter(ft, InputNr);
		}

		// --- GetInput : Bestimmen Status E-Eingang ------------------------------------

		public bool GetInput(Nr InputNr)
		{
			return GetInput(Convert.ToInt32(InputNr));
		}
		public bool GetInput(int InputNr)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.GetInput"));
			if (csGetInputs(ft) == Fehler) 
				throw(new FishFaceException("InterfaceProblem.GetInputs"));
			return (csGetInput(ft, InputNr) == 0) ? false : true;
		}

		// --- GetInputs : Auslesen Status E-Eingänge ----------------------------------

		public int GetInputs()
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.GetInputs"));
			int res = csGetInputs(ft);
			if(res == Fehler) throw(new FishFaceException("InterfaceProblem.GetInputs"));
			return res;
		}

		// --- Inputs : Lesen InputStatus ----------------------------------------------

		public int Inputs
		{
			get{return csGetInputs(ft);}
		}
		// --- LPTAnalog : Analogskalierung --------------------------------------------

		public int LPTAnalog
		{
			get{return csGetLPTAnalog(ft);}
		}
		// --- LPTDelay : Ausgabeverzögerung ------------------------------------------

		public int LPTDelay
		{
			get{return csGetLPTDelay(ft);}
		}
		// --- NotHalt : Abbruchwunsch ------------------------------------------------

		public bool NotHalt
		{
			get{return FNotHalt;}
			set{FNotHalt = value;}
		}

		// --- OpenInterface : Verbindung zum Interface herstellen ------------------

		public void OpenInterface(Port PortNr)
		{
			OpenInterface(Convert.ToInt32(PortNr), true);
		}
		public void OpenInterface(Port PortNr, bool DoEvents)
		{
			OpenInterface(Convert.ToInt32(PortNr), DoEvents);
		}
		public void OpenInterface(int PortNr)
		{
			OpenInterface(Convert.ToInt32(PortNr), true);
		}
		public void OpenInterface(int PortNr, bool DoEvents)
		{
			if(PortNr<0 || PortNr >8) PortNr = 0;
			ft = csOpenInterfaceEx(PortNr, FAnalogScan, FSlave, FPollInterval, 
				FLPTAnalog, FLPTDelay);
			if(ft == Fehler)
				throw(new FishFaceException("InterfaceProblem.Open"));
			FPortNr = PortNr;
			csSetMotors(ft, 0);
			FNotHalt = false;
			FOpen = true;
			FAnalogScan = csGetAnalogScan(ft);
			FLPTAnalog = csGetLPTAnalog(ft);
			FLPTDelay  = csGetLPTDelay(ft);
			FSlave = csGetSlave(ft);
			FPollInterval = csGetPollInterval(ft);
			FDoEvents = DoEvents;
		}

		// --- Outputs : Setzen / Lesen der M-ausgänge ------------------------------

		public int Outputs
		{
			get{return csGetMotors(ft);}
			set{csSetMotors(ft, value);}
		}

		// --- Pause : Programm anhalten --------------------------------------------

		public void Pause(int mSek)
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.Pause"));
			uint sZeit = GetTickCount();
			do
			{
				if(FDoEvents) Application.DoEvents();
				if (GetInputs() == Fehler)
					throw(new FishFaceException("InterfaceProblem.Pause"));
				if (FNotHalt || GetAsyncKeyState(ESCAPE) !=0)return;
				Sleep(Convert.ToUInt32(PollInterval));
			}	while (GetTickCount() - sZeit < mSek);
		}

		// --- PollInterval : Abfrage und Refresh des Interfaces -----------------

		public int PollInterval
		{
			get{return csGetPollInterval(ft);}
		}

		// --- PortName : aktueller Verbindungsname ---------------------------------

		public string PortName
		{
			get{if (FPortNr>=0) return FPortName[FPortNr]; else return "NONE";}
		}

		// --- SetCounter : Setzen Counter ------------------------------------------

		public void SetCounter(Nr InputNr, int Value)
		{
			SetCounter(Convert.ToInt32(InputNr), Value);
		}
		public void SetCounter(int InputNr, int Value)
		{
			csSetCounter(ft, InputNr, Value);
		}

		// --- SetLamp : Setzen eines 'halben' M-Ausganges -------------------------

		public void SetLamp(Nr LampNr, Dir OnOff)
		{
			SetLamp(Convert.ToInt32(LampNr), Convert.ToInt32(OnOff));
		}
		public void SetLamp(int LampNr, int OnOff)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetLamp"));
			if(csSetLamp(ft, LampNr, OnOff) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetLamp"));
		}

		// --- SetMotor : Setzen eines M-Ausganges ---------------------------------
		//                csSetMotor, csSetMotorEx, csRobMotor

		public void SetMotor(Nr MotorNr, Dir Direction, Speed Speed, int Counter)
		{
			SetMotor(Convert.ToInt32(MotorNr), Convert.ToInt32(Direction),
				Convert.ToInt32(Speed), Counter);
		}
		public void SetMotor(int MotorNr, int Direction, int Speed, int Counter)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetMotor"));
			if(csRobMotor(ft, MotorNr, Direction, Speed, Counter) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetMotor"));
		}
		public void SetMotor(Nr MotorNr, Dir Direction, Speed Speed)
		{
			SetMotor(Convert.ToInt32(MotorNr), Convert.ToInt32(Direction),
				Convert.ToInt32(Speed));
		}
		public void SetMotor(int MotorNr, int Direction, int Speed)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetMotor"));
			if(csSetMotorEx(ft, MotorNr, Direction, Speed) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetMotor"));
		}
		public void SetMotor(Nr MotorNr, Dir Direction)
		{
			SetMotor(Convert.ToInt32(MotorNr), Convert.ToInt32(Direction));
		}
		public void SetMotor(int MotorNr, int Direction)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetMotor"));
			if(csSetMotor(ft, MotorNr, Direction) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetMotor"));
		}

		// --- SetMotors : Setzen aller M-Ausgänge ---------------------------------
		//               : csSetMotors, csSetMotorsEx, csRobMotors

		public void SetMotors(int MotorStatus, int SpeedStatus, int ModeStatus)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetMotors"));
			if(csRobMotors(ft, MotorStatus, SpeedStatus, ModeStatus) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetMotors"));
		}
		public void SetMotors(int MotorStatus, int SpeedStatus)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetMotors"));
			if(csSetMotorsEx(ft, MotorStatus, SpeedStatus) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetMotors"));
		}
		public void SetMotors(int MotorStatus)
		{
			if(FDoEvents) Application.DoEvents();
			if (!FOpen) throw(new FishFaceException("KeinOpen.SetMotors"));
			if(csSetMotors(ft, MotorStatus) == Fehler)
				throw(new FishFaceException("InterfaceProblem.SetMotors"));
		}

		// --- Slave : Setzen Extension Module ------------------------------------

		public bool Slave
		{
			get{return csGetSlave(ft) == 0 ? false : true;}
		}

		// --- Version : aktuelle Version ---------------------------------------

		public static string Version
		{
			get{return "3.1.01";}
		}

		// --- WaitForChange : Warten auf NrOfChanges Impulse von InputNr ----------

		public void WaitForChange(Nr InputNr, int NrOfChanges, Nr TermInputNr)
		{
			WaitForChange(Convert.ToInt32(InputNr), 
				NrOfChanges, Convert.ToInt32(TermInputNr));
		}
		public void WaitForChange(Nr InputNr, int NrOfChanges)
		{
			WaitForChange(Convert.ToInt32(InputNr), NrOfChanges, 0);
		}
		public void WaitForChange(int InputNr, int NrOfChanges)
		{
			WaitForChange(InputNr, NrOfChanges, 0);
		}
		public void WaitForChange(int InputNr, int NrOfChanges, int TermInputNr)
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.WaitForChange"));
			csSetCounter(ft, InputNr, 0);

			while (csGetCounter(ft, InputNr) < NrOfChanges)
			{		
				if(FNotHalt || GetAsyncKeyState(ESCAPE) !=0) return;
				if(TermInputNr>0) if(GetInput(TermInputNr)) return;
				if (GetInputs() == Fehler)
					throw(new FishFaceException("InterfaceProblem.WaitForChange"));
				if(FDoEvents) Application.DoEvents();
			}
		}
		
		// --- WaitForHigh : Warten auf einen false/true-Durchgang von InputNr ------

		public void WaitForHigh(Nr InputNr)
		{
			WaitForHigh(Convert.ToInt32(InputNr));
		}
		public void WaitForHigh(int InputNr)
		{
			WaitForInput(InputNr, false);
			WaitForInput(InputNr, true);
		}

		// --- WaitForInput : Warten auf InputNr = OnOff ---------------------------

		public void WaitForInput(Nr InputNr, bool OnOff)
		{
			WaitForInput(Convert.ToInt32(InputNr), OnOff);
		}
		public void WaitForInput(Nr InputNr)
		{
			WaitForInput(Convert.ToInt32(InputNr), true);
		}
		public void WaitForInput(int InputNr)
		{
			WaitForInput(InputNr, true);
		}
		public void WaitForInput(int InputNr, bool OnOff)
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.WaitForInput"));
			do
			{
				if(FNotHalt || GetAsyncKeyState(ESCAPE) !=0) return;
				if (GetInputs() == Fehler)
					throw(new FishFaceException("InterfaceProblem.WaitForInput"));
				Sleep(Convert.ToUInt32(PollInterval));
				if(FDoEvents) Application.DoEvents();
			}   while(!GetInput(InputNr) == OnOff);

		}

		// --- WaitForLow : Warten auf einen true/false-Durchgang von InputNr -----

		public void WaitForLow(Nr InputNr)
		{
			WaitForLow(Convert.ToInt32(InputNr));
		}
		public void WaitForLow(int InputNr)
		{
			WaitForInput(InputNr, true);
			WaitForInput(InputNr, false);
		}

		// --- WaitForMotors : Warten auf MotorReady-Ereignis oder Ablauf mSek -------

		public Wait WaitForMotors(int mSek, params Nr[] MotorNrs)
		{
			int[] Mots = new int[MotorNrs.Length];
			for(int i = 0; i < MotorNrs.Length; i++)
				Mots[i] = (int)MotorNrs[i];
			return WaitForMotors(mSek, Mots);
		}
		public Wait WaitForMotors(int mSek, params int[] MotorNrs)
		{
			int c, i, j;
			uint t;

			if(!FOpen) throw(new FishFaceException("KeinOpen.WaitForMotors"));
			t = GetTickCount() + Convert.ToUInt32(mSek);
			do
			{
				c = 0;
				for(i=0; i<MotorNrs.Length; i++)
				{
					j = MotorNrs[i];
					c += csGetModeStatus(ft, j);
				}

				if(c == 0) return Wait.Ende;

				// --- Abschalten der Motoren der Parameterliste ------
				if(FNotHalt || (GetAsyncKeyState(ESCAPE) != 0))
					for(i=0; i<MotorNrs.Length; i++)
					{
						j = (int)MotorNrs[i];
						csSetMotor(ft, j, 0);
					};

				if(FNotHalt) return Wait.NotHalt;
				if(GetAsyncKeyState(ESCAPE) != 0)return Wait.ESC;
				if(FDoEvents) Application.DoEvents();
				Sleep(Convert.ToUInt32(PollInterval));
				if (GetInputs() == Fehler)
					throw(new FishFaceException("InterfaceProblem.WaitForMotors"));
			}   while((mSek == 0) || (t>GetTickCount()));
			return Wait.Time;
		}

		// --- WaitForPositionDown : Position = Counter - InputNr-Impulse ----------

		public void WaitForPositionDown(Nr InputNr, ref int Counter,
			int Position, Nr TermInputNr)
		{
			WaitForPositionDown(Convert.ToInt32(InputNr), ref Counter, 
				Position, Convert.ToInt32(TermInputNr));
		}
		public void WaitForPositionDown(Nr InputNr, ref int Counter, int Position)
		{
			WaitForPositionDown(Convert.ToInt32(InputNr), ref Counter, Position, 0);
		}
		public void WaitForPositionDown(int InputNr, ref int Counter, int Position)
		{
			WaitForPositionDown(InputNr, ref Counter, Position, 0);
		}
		public void WaitForPositionDown(int InputNr, ref int Counter, 
			int Position, int TermInputNr)
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.WaitForPositionDown"));
			WaitForChange(InputNr, Counter - Position, TermInputNr);
			Counter -= csGetCounter(ft, InputNr);
		}

		// --- WaitForPositionUp : Position = Counter + InputNr-Impulse ------------

		public void WaitForPositionUp(Nr InputNr, ref int Counter,
			int Position, Nr TermInputNr)
		{
			WaitForPositionUp(Convert.ToInt32(InputNr), ref Counter, 
				Position, Convert.ToInt32(TermInputNr));
		}
		public void WaitForPositionUp(Nr InputNr, ref int Counter, int Position)
		{
			WaitForPositionUp(Convert.ToInt32(InputNr), ref Counter, Position, 0);
		}
		public void WaitForPositionUp(int InputNr, ref int Counter, int Position)
		{
			WaitForPositionUp(InputNr, ref Counter, Position, 0);
		}
		public void WaitForPositionUp(int InputNr, ref int Counter, 
			int Position, int TermInputNr)
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.WaitForPositionUp"));
			WaitForChange(InputNr, Position - Counter, TermInputNr);
			Counter += csGetCounter(ft, InputNr);
		}

		// --- WaitForTime : Program mSek anhalten ----------------------------------

		public void WaitForTime(int mSek)
		{
			if (!FOpen) throw(new FishFaceException("KeinOpen.WaitForTime"));
			Pause(mSek);
		}
	}
  
  // -----------------------------------------------------------------------------------
  //
  // Klasse FishRobot : Betrieb von RobMotoren
  //
  // -----------------------------------------------------------------------------------

	public class FishRobot : FishFace
	{
		// --- globale Daten ------------------------------------------------
		public struct MotWerte {
			public int Nr;
			public int maxPos;
			public int actPos;
		}
		public MotWerte[] MotCntl;
		private int MotAnz;

		// --- Ereignisse ----------------------------------------

		public delegate void CommonDelegate(object sender, int[] ActPositions);
		public event CommonDelegate PositionChange;

		// --- FishRobot Konstruktor ------------------------------------

		public FishRobot(int[,] MotList) : base(){MotInit(MotList);}
		public FishRobot(int[,] MotList, bool AnalogScan, bool Slave, int PollInterval) : 
			base(AnalogScan, Slave, PollInterval){MotInit(MotList);}
		public FishRobot(int[,] MotList, bool AnalogScan, bool Slave, int PollInterval, 
	        int LPTAnalog, int LPTDelay) :
		base(AnalogScan, Slave, PollInterval, LPTAnalog, LPTDelay){MotInit(MotList);}

		private void MotInit(int[,] MotList) {
			MotAnz =MotList.GetLength(0);
			MotCntl = new MotWerte[MotAnz];
			for(int i = 0; i < MotAnz; i++) {
				MotCntl[i].Nr     = MotList[i,0];
				MotCntl[i].maxPos = MotList[i,1];
				MotCntl[i].actPos = 0;
			}
		}

		// --- MoveHome : Simultanes Anfahren der Home-Positon ---------

		public void MoveHome() {
			int[] Mots = new int[MotAnz];
			for(int i = 0; i < MotAnz; i++) Mots[i] = MotCntl[i].Nr;
			try {
				foreach(int m in Mots) SetMotor(m, (int)Dir.Links, (int)Speed.Full, 9999);
				WaitForMotors(0, Mots);
			}
			catch(FishFaceException){
				throw(new FishFaceException("InterfaceProblem.MoveHome"));
			}
		}

		// --- MoveTo : Simultanes Anfahren einer vorgegebenen Position -----

		public void MoveTo(params int[] PosList){

			// --- Starten der Motoren ------------
			int anzPos = PosList.Length;
			try 
			{
				for(int i = 0; i < anzPos; i++){
					int ZielPos = PosList[i];
					if(ZielPos > MotCntl[i].maxPos)ZielPos = MotCntl[i].maxPos;
					if(ZielPos > MotCntl[i].actPos) 
						SetMotor(MotCntl[i].Nr, (int)Dir.Rechts, (int)Speed.Full,
							ZielPos-MotCntl[i].actPos);
					else {
						int ct = MotCntl[i].actPos-ZielPos;
						if(ct > 0) SetMotor(MotCntl[i].Nr, (int)Dir.Links,
									   (int)Speed.Full, ct);
					}
				}
				// --- Warten auf MotorReady ----
				int[] Mots = new int[anzPos];
				for(int i = 0; i < anzPos; i++) Mots[i] = MotCntl[i].Nr;
				int res = (int)Wait.Time;
				
				// --- Ausgeben der aktuellen Position ----
				while (res == (int)Wait.Time){
					if(PositionChange != null){
						int[] actPos = new int[anzPos];
						for(int i = 0; i < anzPos; i++) {
							int iNr = (Mots[i]-1)*2+2;
							int ZielPos = PosList[i];
							if(ZielPos > MotCntl[i].actPos)
								actPos[i] = ZielPos - GetCounter(iNr);
							else actPos[i] = ZielPos + GetCounter(iNr);
						}
						PositionChange(this, actPos);
					}
					res = (int)WaitForMotors(8*PollInterval, Mots);
				}
				// --- Aktualisieren MotCntl -------
				for(int i = 0; i < anzPos; i++){
					int iNr = (Mots[i]-1)*2+2;
					int ZielPos = PosList[i];
					if(res == (int)Wait.Ende){ 
						if(ZielPos > MotCntl[i].actPos) 
							MotCntl[i].actPos = ZielPos + GetCounter(iNr);
						else MotCntl[i].actPos = ZielPos - GetCounter(iNr);
					}
					else {
						if(ZielPos > MotCntl[i].actPos)
							MotCntl[i].actPos = ZielPos - GetCounter(iNr);
						else MotCntl[i].actPos = ZielPos * GetCounter(iNr);
					}
				}
				// --- Abschließende Positionsausgabe --------
				if(PositionChange != null){
					int[] actPos = new int[anzPos];
					for(int i = 0; i < anzPos; i++) actPos[i] = MotCntl[i].actPos;
					PositionChange(this, actPos);					
				}
			}
			catch(FishFaceException){
				throw(new FishFaceException("InterfaceProblem.MoveTo/MoveDelta"));
			}
		}
		public void MoveDelta(params int[] DeltaList)
		{
			int anzPos = DeltaList.Length;
			int[] PosList = new int[anzPos];
			for(int i = 0; i < anzPos; i++)
				PosList[i] = MotCntl[i].actPos + DeltaList[i];
			MoveTo(PosList);
		}
	}

	// -----------------------------------------------------------------------------------
	// 
	// Klasse FishStep : Betrieb von Schrittmotoren, einzeln und im XY-Verbund
	//
	// -----------------------------------------------------------------------------------

	public class FishStep : FishFace 
	{
		// --- globale Daten ----------------------------------------
		public struct MotWerte
		{
			public int maxPos;
			public int actPos;
			public bool outPos;
		}
		public MotWerte[] MotCntl;
		private int MotAnz;

		private enum MotS {MotLLL, MotLLR, MotLRL, MotLRR, MotRLL, MotRLR, MotRRL, MotRRR}
		private int[] MotMaske = {0xFFC0, 0xFF03, 0xFF0F, 0xF03F, 0xC0FF};
		private int[] BefMaske = {0x15, 0x25, 0x19, 0x29, 0x16, 0x26, 0x1A, 0x2A};
		private int[] MotShift = {0, 2, 4, 8, 16};

		// ------- Inline Funktionen ----------------------------------
		private int  EndNrX(int MotNr)  {return (MotNr-1)*2+1;}
		private int  EndNrY(int MotNr)  {return (MotNr+1)*2+1;}
		private int  Xmax(int MotNr)    {return MotCntl[MotNr-1].maxPos;}
		private int  Ymax(int MotNr)    {return MotCntl[MotNr+1].maxPos;}
		private int  Xact(int MotNr)    {return MotCntl[MotNr-1].actPos;}
		private int  Yact(int MotNr)    {return MotCntl[MotNr+1].actPos;}
		private bool Xout(int MotNr)    {return MotCntl[MotNr-1].outPos;}
		private bool Yout(int MotNr)    {return MotCntl[MotNr+1].outPos;}
		private int  XactInc(int MotNr) {return MotCntl[MotNr-1].actPos++;}
		private int  XactDec(int MotNr) {return MotCntl[MotNr-1].actPos--;}
		private int  YactInc(int MotNr) {return MotCntl[MotNr+1].actPos++;}
		private int  YactDec(int MotNr) {return MotCntl[MotNr+1].actPos--;}

		// --- Ereignisse -------------------------------------------
		public delegate void StepDelegate(object sender, int MotNr, int ActPosition);
		public event StepDelegate StepChange;
		public delegate void PlotDelegate(object sender, int MotNr, int Xact, int Yact);
		public event PlotDelegate PlotChange;

		// --- FishStep Konstruktor ----------------------------------
		public FishStep(int[,] MotList) : base() {MotInit(MotList);}
		public FishStep(int[,] MotList, bool AnalogScan, bool Slave, int PollInterval) : 
			base(AnalogScan, Slave, PollInterval){MotInit(MotList);}
		public FishStep(int[,] MotList, bool AnalogScan, bool Slave, int PollInterval, 
			int LPTAnalog, int LPTDelay) :
			base(AnalogScan, Slave, PollInterval, LPTAnalog, LPTDelay){MotInit(MotList);}
		private void MotInit(int[,] MotList) 
		{
			MotAnz = MotList.GetLength(0);
			MotCntl = new MotWerte[MotAnz*2];
			for(int i = 0; i < MotAnz; i++) MotCntl[MotList[i,0]-1].maxPos = MotList[i,1];
		}
		
		// ---- interne Routinen Step ---------------------------------------

		// --- RechtsDreh : Fahren eines Rechtszyklus -------------------
		private void RechtsDreh(int MotNr) 
		{
			SetMotor(MotNr, (int)Dir.Links);
			Pause(PollInterval);
			SetMotor(MotNr+1, (int)Dir.Links);
			Pause(PollInterval);
			SetMotor(MotNr, (int)Dir.Rechts);
			Pause(PollInterval);
			SetMotor(MotNr+1, (int)Dir.Rechts);
			Pause(PollInterval);
		}

		// --- LinksDreh : Fahren eines Linkszyklus -----------------
		private void LinksDreh(int MotNr) 
		{
			SetMotor(MotNr, (int)Dir.Rechts);
			Pause(PollInterval);
			SetMotor(MotNr+1, (int)Dir.Links);
			Pause(PollInterval);
			SetMotor(MotNr, (int)Dir.Links);
			Pause(PollInterval);
			SetMotor(MotNr+1, (int)Dir.Rechts);
			Pause(PollInterval);
		}

		// --- Step-Methoden -----------------------------------------

		// --- StepHome : Fahren eines Motors auf Home-Position ------------
		public void StepHome(Nr MotNr)
		{
			StepHome((int)MotNr);
		}
		public void StepHome(int MotNr)
		{
			try
			{
				while(!GetInput(EndNrX(MotNr)))
				{
					if(GetAsyncKeyState(ESCAPE) != 0) return;
					LinksDreh(MotNr);
				}
				MotCntl[MotNr-1].actPos = 0;
				MotCntl[MotNr-1].outPos = false;
			}
			catch(FishFaceException) 
			{ throw(new FishFaceException("InterfaceProblem.MoveHome"));}
		}
		
		// --- StepTo : Fahren Motor auf Zielposition -----
		public void StepTo(Nr  MotNr, int Xabs)
		{
			StepTo((int)MotNr, Xabs);
		}
		public void StepTo(int MotNr, int Xabs) 
		{
			try
			{
				int PosCount = 0;
				if(Xabs > Xact(MotNr))
				{
					while(Xact(MotNr) < Xabs)
					{
						RechtsDreh(MotNr);
						XactInc(MotNr);
						if(Xabs >= MotCntl[MotNr-1].maxPos) return;
						if(GetAsyncKeyState(ESCAPE) !=0)    return;
						PosCount++;
						if(StepChange != null && PosCount == 10)
						{
							PosCount = 0;
							StepChange(this, MotNr, Xact(MotNr));
						}
					}
				}
				else
				{
					while(Xact(MotNr) > Xabs)
					{
						LinksDreh(MotNr);
						XactDec(MotNr);
						if(GetInput(EndNrX(MotNr)))      return;
						if(GetAsyncKeyState(ESCAPE) !=0) return;
						PosCount++;
						if(StepChange != null && PosCount == 10)
						{
							PosCount = 0;
							StepChange(this, MotNr, Xact(MotNr));
						}
					}
				}
				if(StepChange != null) StepChange(this, MotNr, Xact(MotNr));
			}
			catch(FishFaceException)
			{throw(new FishFaceException("InterfaceProblem.StepTo"));}
		}

		// --- StepDelta : Fahren Motor um Xrel-Zyklen ---------------
		public void StepDelta(Nr  MotNr, int Xrel)
		{
			StepDelta((int)MotNr, Xrel);
		}
		public void StepDelta(int MotNr, int Xrel)
		{
			try
			{
				StepTo(MotNr, Xact(MotNr)+Xrel);
			}
			catch(FishFaceException)
			{throw(new FishFaceException("InterfaceProblem.StepDelta"));}
		}

		// --- Interne Routinen Plot ------
		// --- Zyklen in die acht Richtungen -----

		// --- BereichsLimit : Prüfen, ob actKoordinaten im Plotbereich liegen ---
		private void BereichsLimit(int MotNr)
		{
			if(Xout(MotNr) || Yout(MotNr))
			{
				MotCntl[MotNr-1].outPos = Xact(MotNr) < 0 || Xact(MotNr) > Xmax(MotNr);
				MotCntl[MotNr+1].outPos = Yact(MotNr) < 0 || Yact(MotNr) > Ymax(MotNr);
				if(Xout(MotNr) || Yout(MotNr)) return;
			}
			else 
			{
				MotCntl[MotNr-1].outPos = Xact(MotNr) < 0 || Xact(MotNr) > Xmax(MotNr);
				MotCntl[MotNr+1].outPos = Yact(MotNr) < 0 || Yact(MotNr) > Ymax(MotNr);
			}
		}
		// --- Endtaster : Abfrage Endtaster im laufenden Betrieb ---
		private bool Endtaster(int MotNr)
		{
			if((GetInput(EndNrX(MotNr)) && (Xact(MotNr) != 0)) || 
				(GetInput(EndNrY(MotNr)) && (Yact(MotNr) != 0))) return true; 
			else return false;
		}

		// --- Zyklus : Abarbeiten des vorgegebenen Befehlszyklus ---
		private void Zyklus(int MotNr, params MotS[] Befehle){
			foreach(MotS mot in Befehle)
			{
				int MotStatus = Outputs & MotMaske[MotNr-1];
				Outputs = MotStatus | BefMaske[(int)mot] << MotShift[MotNr-1];
				Pause(PollInterval);
			}
		}

		// --- XMinusSchritt : Fahren in -X Richtung ---
		private void XMinusSchritt(int MotNr)
		{
			XactDec(MotNr);
			BereichsLimit(MotNr);
			if(Xout(MotNr)) return;
			Zyklus(MotNr, MotS.MotRLR, MotS.MotLLR, MotS.MotLRR, MotS.MotRRR);
		}

		// --- XPlusSchritt : Fahren in +X Richtung ---
		private void XPlusSchritt(int MotNr) 
		{
			XactInc(MotNr);
			BereichsLimit(MotNr);
			if(Xout(MotNr)) return;
			Zyklus(MotNr, MotS.MotLRR, MotS.MotLLR, MotS.MotRLR, MotS.MotRRR);
		}

		// --- XpYpDiagonale : Fahren in +X/+Y Richtung ------
		private void XpYpDiagonale(int MotNr)
		{
			XactInc(MotNr);
			YactInc(MotNr);
			BereichsLimit(MotNr);
			if(Xout(MotNr)) {YPlusSchritt(MotNr); return;}
			if(Yout(MotNr)) {XPlusSchritt(MotNr); return;}
			Zyklus(MotNr, MotS.MotLRR, MotS.MotLLL, MotS.MotRLL, MotS.MotRRR);
		}

		// --- XpYmDiagonale : Fahren in +X/-Y Richtung ------
		private void XpYmDiagonale(int MotNr)
		{
			XactInc(MotNr);
			YactDec(MotNr);
			BereichsLimit(MotNr);
			if(Xout(MotNr)) {YMinusSchritt(MotNr); return;}
			if(Yout(MotNr)) {XPlusSchritt(MotNr); return;}
			Zyklus(MotNr, MotS.MotRRL, MotS.MotLRL, MotS.MotLLR, MotS.MotRLR, MotS.MotRRR);
		}
		// --- XmYmDiagonale : Fahren in -X/-Y Richtung ------
		private void XmYmDiagonale(int MotNr)
		{
			XactDec(MotNr);
			YactDec(MotNr);
			BereichsLimit(MotNr);
			if(Xout(MotNr)) {YMinusSchritt(MotNr); return;}
			if(Yout(MotNr)) {XMinusSchritt(MotNr); return;}
			Zyklus(MotNr, MotS.MotRLL, MotS.MotLLL, MotS.MotLRR, MotS.MotRRR);
		}

		// --- XmYpDiagonale : Fahren in -X/+Y Richtung ------
		private void XmYpDiagonale(int MotNr)
		{
			XactDec(MotNr);
			YactInc(MotNr);
			BereichsLimit(MotNr);
			if(Xout(MotNr)) {YPlusSchritt(MotNr); return;}
			if(Yout(MotNr)) {XMinusSchritt(MotNr); return;}
			Zyklus(MotNr, MotS.MotRLR, MotS.MotLLR, MotS.MotLRL, MotS.MotRRL, MotS.MotRRR);
		}

		// --- YMinusSchritt : Fahren in -Y Richtung -----
		private void YMinusSchritt(int MotNr)
		{
			YactDec(MotNr);
			BereichsLimit(MotNr);
			if(Yout(MotNr)) return;
			Zyklus(MotNr, MotS.MotRRL, MotS.MotLRL, MotS.MotLRR, MotS.MotRRR);
		}

		// --- YPlusSchritt : Fahren in +Y Richtung -----
		private void YPlusSchritt(int MotNr)
		{
			YactInc(MotNr);
			BereichsLimit(MotNr);
			if(Yout(MotNr)) return;
			Zyklus(MotNr, MotS.MotLRR, MotS.MotLRL, MotS.MotRRL, MotS.MotRRR);
		}

		// --- Plot-Methoden --------------
		
		// --- PlotHome : Fahren Motoren auf Home-Position(native : linke untere Ecke)
		public void  PlotHome(Nr  MotNr) 
		{
			PlotHome((int)MotNr);
		}
		public void  PlotHome(int MotNr)
		{
			try
			{
				MotCntl[MotNr-1].outPos = false;
				MotCntl[MotNr+1].outPos = false;
				Outputs = 0;
				// --- Beide Motoren gegen Endtaster ----
				while (!(GetInput(EndNrX(MotNr)) || GetInput(EndNrY(MotNr))))
				{
					if(GetAsyncKeyState(ESCAPE) != 0) return;
					Zyklus(MotNr, MotS.MotRLL, MotS.MotLLL, MotS.MotLRR, MotS.MotRRR);
				}
				// --- Motor X gegen Endtaster -----------
				while(GetInput(EndNrX(MotNr)) == false)
				{
					if(GetAsyncKeyState(ESCAPE) != 0) return;
					Zyklus(MotNr, MotS.MotRLR, MotS.MotLLR, MotS.MotLRR, MotS.MotRRR);
				}
				// --- Motor X frei von Endtaster -------
				while(GetInput(EndNrX(MotNr)))
				{
					Zyklus(MotNr, MotS.MotLRR, MotS.MotLLR, MotS.MotRLR, MotS.MotRRR);
				}
				// --- Motor Y gegen Endtaster -----------------
				while(GetInput(EndNrY(MotNr)) == false)
				{
					if(GetAsyncKeyState(ESCAPE) != 0) return;
					Zyklus(MotNr, MotS.MotRRL, MotS.MotLRL, MotS.MotLRR, MotS.MotRRR);
				}
				// --- Motor Y frei von Endtaster ----
				while(GetInput(EndNrY(MotNr)))
				{
					Zyklus(MotNr, MotS.MotLRR, MotS.MotLRL, MotS.MotRRL, MotS.MotRRR);
				}
				// --- Motoren zwei Schritt Abstand vom Ende --------
				XpYpDiagonale(MotNr);
				XpYpDiagonale(MotNr);
				MotCntl[MotNr-1].actPos = 0;
				MotCntl[MotNr+1].actPos = 0;
			}
			catch(FishFaceException)
			{throw(new FishFaceException("InterfaceProblem.PlotHome"));}
		}
		
		// --- PlotDelta : Fahren Motoren um Xrel/Yrel-Werte(Plotzyklen ab links unten) ---
		public void PlotDelta(Nr  MotNr, int Xrel, int Yrel) 
		{
			PlotDelta((int)MotNr, Xrel, Yrel);
		}
		public void PlotDelta(int MotNr, int Xrel, int Yrel)
		{
			try
			{
				int Xdelta   = Math.Abs(Xrel);    // --- Anzahl Plotzyklen
				int Ydelta   = Math.Abs(Yrel);
				int Xnr      = 0;                 // --- Zyklenzähler
				int Ynr      = 0;
				int Delta    = 0;                 // --- Abstand zur optimalen Geraden
				int PosCount = 0;                 // --- Zähler für PosAusgabe

				while(Xnr != Xdelta || Ynr != Ydelta)
				{
					if(GetAsyncKeyState(ESCAPE) != 0) return;
					if(Endtaster(MotNr))              return;
					if(Math.Abs(Delta+Ydelta) < Math.Abs(Delta+Ydelta-Xdelta))
					{
						if(Xrel > 0) XPlusSchritt(MotNr); else XMinusSchritt(MotNr);
						Xnr++;
						Delta += Ydelta;
					}
					else
					{
						if(Math.Abs(Delta-Xdelta) < Math.Abs(Delta+Ydelta-Xdelta))
						{
							if(Yrel > 0) YPlusSchritt(MotNr); else YMinusSchritt(MotNr);
							Ynr++;
							Delta -= Xdelta;
						}
						else 
						{
							if(Xrel > 0) 
								if(Yrel > 0) XpYpDiagonale(MotNr); 
								else XpYmDiagonale(MotNr);
							else if(Yrel > 0) XmYpDiagonale(MotNr); 
								 else XmYmDiagonale(MotNr);
							Xnr++;
							Ynr++;
							Delta += Ydelta - Xdelta;
						}
					}
					PosCount++;
					if(PlotChange != null && PosCount == 10)
					{
						PosCount = 0;
						PlotChange(this, MotNr, Xact(MotNr), Yact(MotNr));
					}
				}
				if(PlotChange != null) PlotChange(this, MotNr, Xact(MotNr), Yact(MotNr));
			}
			catch(FishFaceException)
			{throw(new FishFaceException("InterfaceProblem.PlotDelta"));}
		}
				
		// --- PlotTo : Fahren Motoren auf Xabs/Yabs Position(Plotzyklen ab links unten)
		public void PlotTo(Nr  MotNr, int Xabs, int Yabs)
		{
			PlotTo((int)MotNr, Xabs, Yabs);
		}
		public void PlotTo(int MotNr, int Xabs, int Yabs)
		{
			try
			{
				PlotDelta(MotNr, Xabs-Xact(MotNr), Yabs-Yact(MotNr));
			}
			catch(FishFaceException)
			{throw(new FishFaceException("InterfaceProblem.PlotTo"));}
		}
	}
}

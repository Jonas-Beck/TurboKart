// See https://aka.ms/new-console-template for more information

using TurboKart.Presentation.Console.LapTimerSimulation;

LapTimeSimulator simulator = new LapTimeSimulator();

await simulator.SimulateRealTime(10, 20, 0.1);
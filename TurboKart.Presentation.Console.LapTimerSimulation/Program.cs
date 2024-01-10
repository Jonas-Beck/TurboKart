// See https://aka.ms/new-console-template for more information

using TurboKart.Presentation.Console.LapTimerSimulation;

LapTimeSimulator simulator = new LapTimeSimulator();

await simulator.SimulateRealTime(4, 1, 10);
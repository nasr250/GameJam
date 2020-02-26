using System;  
using System.Speech.Recognition;  
using System.Collections.Generic;  
using System.Threading;  
  
namespace SampleRecognition  
{  
  class Program  
  {  
    private static SpeechRecognitionEngine recognizer;  
    public static void Main(string[] args)  
    {  
  
      // Initialize an in-process speech recognition engine and configure its input.  
      using (recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))  
      {  
        recognizer.SetInputToDefaultAudioDevice();  
  
        // Create the first grammar - Menu.  
        Choices Difficulty = new Choices(new string[] { "easy", "normal", "hard" });  
        GrammarBuilder Menu = new GrammarBuilder(Difficulty);  
        Grammar Options = new Grammar(Menu);  
        Options.Name = "Difficulties";  
  
        // Create the second grammar - Shoot.  
        Choices Shoot = new Choices(new string[] { "shoot" });  
        GrammarBuilder Fire = new GrammarBuilder(Shoot);  
        Grammar Shoot_gun = new Grammar(Fire);  
        Shoot_gun.Name = "Shoot";  
  
        // Attach event handlers.  
        recognizer.SpeechRecognized +=  
          new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);  
        recognizer.RecognizerUpdateReached +=  
          new EventHandler<RecognizerUpdateReachedEventArgs>(recognizer_RecognizerUpdateReached);  
        recognizer.SpeechRecognitionRejected +=  
          new EventHandler<SpeechRecognitionRejectedEventArgs>(recognizer_SpeechRecognitionRejected);  
  
        // Load the Options grammar.  
        recognizer.LoadGrammar(Options);  
  
        // Start asynchronous, continuous recognition.  
        recognizer.RecognizeAsync(RecognizeMode.Multiple);  
        Console.WriteLine("Starting asynchronous, continuous recognition");  
        Console.WriteLine("  Options grammar is loaded and enabled.");  
  
        // Pause to recognize options.  
        Thread.Sleep(7000);  
        Console.WriteLine();  
  
        // Request an update and load the shoot grammar.  
        recognizer.RequestRecognizerUpdate();  
        recognizer.LoadGrammarAsync(Shoot_gun);  
        Thread.Sleep(7000);  
  
        // Request an update and unload the Options grammer.  
        recognizer.RequestRecognizerUpdate();  
        recognizer.UnloadGrammar(Options);  
        Thread.Sleep(7000);  
      }  
  
      // Keep the console window open.  
      Console.WriteLine();  
      Console.WriteLine("Press any key to exit...");  
      Console.ReadKey();  
    }  
  
    // At the update, get the names and enabled status of the currently loaded grammars.  
    public static void recognizer_RecognizerUpdateReached(  
      object sender, RecognizerUpdateReachedEventArgs e)  
    {  
      Console.WriteLine();  
      Console.WriteLine("Update reached:");  
      Thread.Sleep(1000);  
  
      string qualifier;  
      List<Grammar> grammars = new List<Grammar>(recognizer.Grammars);  
      foreach (Grammar g in grammars)  
      {  
        qualifier = (g.Enabled) ? "enabled" : "disabled";  
        Console.WriteLine("  {0} grammar is loaded and {1}.",  
        g.Name, qualifier);  
      }  
    }  
  
    // Write the text of the recognized phrase to the console.  
    static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)  
    {  
      Console.WriteLine("    Speech recognized: " + e.Result.Text);  
    }  
  
    // Write a message to the console when recognition fails.  
    static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)  
    {  
      Console.WriteLine("    Recognition attempt failed");  
    }  
  }  
}  
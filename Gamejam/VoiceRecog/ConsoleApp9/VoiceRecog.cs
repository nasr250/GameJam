using System;  
using System.Speech.Recognition;  
using System.Collections.Generic;  
using System.Threading;
using System.Windows.Forms;
  
namespace SampleRecognition  
{  
  class Program  
  {  
    private static SpeechRecognitionEngine recognizer;

    public static Grammar Options, Player_Option;
        

    public static void Main(string[] args)  
    {
            // Initialize an in-process speech recognition engine and configure its input.  
            using (recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))  
      {  
        recognizer.SetInputToDefaultAudioDevice();  
                
  
        // Create the first grammar - Menu.  
        Choices Difficulty = new Choices(new string[] { "start" });  
        GrammarBuilder Menu = new GrammarBuilder(Difficulty);  
        Options = new Grammar(Menu);  
        Options.Name = "Difficulties";  
  
        // Create the second grammar - InGame.  
        Choices InGame = new Choices(new string[] { "pew", "exit" });  
        GrammarBuilder Game_Option = new GrammarBuilder(InGame);  
        Player_Option = new Grammar(Game_Option);  
        Player_Option.Name = "InGame";  

        /* Create the third grammar - Transition.  
        Choices Transition = new Choices(new string[] { "exit" });  
        GrammarBuilder Paused = new GrammarBuilder(Transition);  
        PauseMenu = new Grammar(Paused);  
        PauseMenu.Name = "Transition";  
         */
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

        // Pause to recognize options.  
        while (true)
        {
          Console.ReadLine();
        }  
   


 }  
    

    }  
  
    // At the update, get the names and enabled status of the currently loaded grammars.  
    public static void recognizer_RecognizerUpdateReached(  
      object sender, RecognizerUpdateReachedEventArgs e)  
    {  
      Console.WriteLine();  
      Console.WriteLine("Update reached:");    
  
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
    public static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)  
    {  
          
      Console.WriteLine(e.Result.Text);
            if (e.Result.Text == "start")
            {
                recognizer.RequestRecognizerUpdate();
                recognizer.LoadGrammarAsync(Player_Option);
                recognizer.UnloadGrammar(Options);
                SendKeys.SendWait("{Enter}");
            }
            if (e.Result.Text == "exit")
            {
                recognizer.RequestRecognizerUpdate();
                recognizer.LoadGrammarAsync(Options);
                recognizer.UnloadGrammar(Player_Option);
                SendKeys.SendWait("{Backspace}");
            }
            if (e.Result.Text == "pew")
            {
                recognizer.RequestRecognizerUpdate();
                SendKeys.SendWait("{z}");
            }
        }  
    
    // Write a message to the console when recognition fails.  
    static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)  
    {  
      Console.WriteLine("    Recognition attempt failed");  
    }  
  }

}  

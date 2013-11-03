ScarlettSDK-CSharp
==================

Scarlett SDK-CSharp is a library that allows you to stream voice data or WAVE file from any .net CLR platform  to Beyond Verbal's API and get analysis of the speaker person mood.


Usage:

      var EmotionsSessionParameters = SessionParameters.Create().WithDataFormat(new WAVFormat());
      
      var session = new EmotionsAnalyzer().InitializeSession(EmotionsSessionParameters);
                
      session.NewAnalysis += session_NewAnalysis;
      session.ProcessingDone += session_ProcessingDone;
      
      session.AnalyzeAsync(voiceStream);
      

